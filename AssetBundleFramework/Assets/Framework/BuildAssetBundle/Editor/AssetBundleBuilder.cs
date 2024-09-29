//--------------------------------------------------

//--------------------------------------------------
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

using System.Runtime.Serialization.Formatters.Binary;


public class AssetBundleBuilder
{
	/// <summary>
	/// AssetBundle压缩选项
	/// </summary>
	public enum ECompressOption
	{
		Uncompressed = 0,
		StandardCompressionLZMA,
		ChunkBasedCompressionLZ4,
	}

	/// <summary>
	/// 输出的根目录
	/// </summary>
	private readonly string _outputRoot;

	// 构建相关
	public BuildTarget BuildTarget { private set; get; } = BuildTarget.NoTarget;
	public int BuildVersion = 1;
	public string OutputPath { private set; get; } = string.Empty;
	public string OutputLuaPath;
	//temp data
	Dictionary<string, List<AssetInfo>> _labelToAssets = new Dictionary<string, List<AssetInfo>>();
	private Dictionary<string, BundleInfo> _bundleMap = new Dictionary<string, BundleInfo>();
	private BundleRelation _bundleRelation = null;

	// 构建选项
	public ECompressOption CompressOption = ECompressOption.ChunkBasedCompressionLZ4;
	public bool IsForceRebuild = false;
	public bool IsAppendHash = false;
	public bool IsDisableWriteTypeTree = false;
	public bool IsIgnoreTypeTreeChanges = true;
	public bool UseFullPath = false;
	public bool IsNameByHash = true;
	public bool IsUseVFS = false;
	public bool MultiLang = false;
	public AssetBundleBuilder(BuildTarget buildTarget, int buildVersion)
	{
		_outputRoot = AssetBundleBuilderHelper.GetDefaultOutputRootPath();
		BuildTarget = BuildTarget.StandaloneWindows64;
		BuildVersion = buildVersion;
		OutputPath = GetOutputPath();
	}

	public void SetBuildTarget(BuildTarget buildTarget)
	{
		BuildTarget = buildTarget;
		OutputPath = GetOutputPath();
	}

	public string GetOutputPath()
	{
		return $"{_outputRoot}/{BuildTarget}/{PatchDefine.UnityManifestFolderName}";
	}
	/// <summary>
	/// 准备构建
	/// </summary>
	public void PreAssetBuild()
	{
		Debug.Log("------------------------------OnPreAssetBuild------------------------------");
		_bundleMap.Clear();
		_labelToAssets.Clear();

		// 检测构建平台是否合法
		if (BuildTarget == BuildTarget.NoTarget)
			throw new Exception("[BuildPatch] 请选择目标平台");


		if (BuildVersion < 0)
			throw new Exception("[BuildPatch] 请先设置版本号");

		// 检测输出目录是否为空
		if (string.IsNullOrEmpty(OutputPath))
			throw new Exception("[BuildPatch] 输出目录不能为空");

        //检测补丁包是否已经存在

        string packageFolderPath = GetPackageFolderPath();
        if (Directory.Exists(packageFolderPath))
        {
            throw new Exception($"[BuildPatch] 补丁包已经存在：{packageFolderPath}");
        }

        // 如果是强制重建
        if (IsForceRebuild)
		{
			// 删除总目录
			string parentPath = $"{_outputRoot}/{BuildTarget}";
			if (Directory.Exists(parentPath))
			{
				Directory.Delete(parentPath, true);
				Log($"删除平台总目录：{parentPath}");
			}
			AssetDatabase.DeleteAsset(PathTool.BundleRelationFilePath + ".asset");
			AssetDatabase.Refresh();
		}

		// 如果输出目录不存在
		if (!Directory.Exists(OutputPath))
		{
			Directory.CreateDirectory(OutputPath);
			Log($"创建输出目录：{OutputPath}");
		}
		OutputLuaPath = OutputPath + "/LuaBundle";
		// 如果输出目录不存在
		if (!Directory.Exists(OutputLuaPath))
        {
            Directory.CreateDirectory(OutputLuaPath);
            Log($"创建Lua输出目录：{OutputLuaPath}");
        }
        //ReplaceILRuntimeDLL();
        //EncryptLuaFiles();
    }

	/// <summary>
	/// 执行构建
	/// </summary>
	public void PostAssetBuild(bool isReview = false, bool useAAB = false)
	{
		Debug.Log("------------------------------OnPostAssetBuild------------------------------");
		// 准备工作
		List<AssetBundleBuild> buildInfoList = new List<AssetBundleBuild>();
		List<AssetInfo> buildMap = GetBuildMap();

		if (buildMap.Count == 0)
			throw new Exception("[BuildPatch] 构建列表不能为空");

		Log($"构建列表里总共有{buildMap.Count}个资源需要构建");

		for (int i = 0; i < buildMap.Count; i++)
		{
			AssetInfo assetInfo = buildMap[i];
			AssetBundleBuild buildInfo = new AssetBundleBuild();
			buildInfo.assetBundleName = assetInfo.AssetBundleLabel;
			buildInfo.assetBundleVariant = assetInfo.AssetBundleVariant;
			buildInfo.assetNames = new string[] { assetInfo.AssetPath };
			buildInfoList.Add(buildInfo);

			string createdLabel = $"{assetInfo.AssetBundleLabel}.{assetInfo.AssetBundleVariant}".ToLower();
			if (!_labelToAssets.TryGetValue(createdLabel, out var list))
			{
				list = new List<AssetInfo>();
				_labelToAssets.Add(createdLabel, list);
			}
			list.Add(assetInfo);
		}
		// 开始构建
		BuildAssetBundleOptions opt = MakeBuildOptions();
		Log($"开始打包，目标平台:" + BuildTarget.ToString());
		AssetBundleManifest buildManifest = BuildPipeline.BuildAssetBundles(OutputPath, buildInfoList.ToArray(), opt, BuildTarget);
		if (buildManifest == null)
			throw new Exception("[BuildPatch] 构建过程中发生错误！");
		// 清单列表
		string[] allAssetBundles = buildManifest.GetAllAssetBundles();
		Log($"资产清单里总共有{allAssetBundles.Length}个资产");

		//在Asset下创建一个序列化数据配置
		var bundleRelation = FillBundleRelation(buildMap, buildManifest);
		var manifestAssetInfo = new AssetInfo(AssetDatabase.GetAssetPath(bundleRelation));
		manifestAssetInfo.ReadableLabel = PathTool.BundleRelationFilePath;
		manifestAssetInfo.AssetBundleVariant = PatchDefine.AssetBundleDefaultVariant;
		manifestAssetInfo.AssetBundleLabel = HashUtility.BytesMD5(Encoding.UTF8.GetBytes(PathTool.BundleRelationFilePath));
		var manifestBundleName = $"{manifestAssetInfo.AssetBundleLabel}.{manifestAssetInfo.AssetBundleVariant}".ToLower();
		_labelToAssets.Add(manifestBundleName, new List<AssetInfo>() { manifestAssetInfo });
		//build BundleRelation bundle
		buildInfoList.Clear();

		buildInfoList.Add(new AssetBundleBuild()
		{
			assetBundleName = manifestAssetInfo.AssetBundleLabel,
			//assetBundleName = manifestAssetInfo.AssetBundleLabel,
			assetBundleVariant = manifestAssetInfo.AssetBundleVariant,
			assetNames = new[] { manifestAssetInfo.AssetPath }
		});

		var bundleRelationManifest = BuildPipeline.BuildAssetBundles(OutputPath, buildInfoList.ToArray(), opt, BuildTarget);
		if (bundleRelationManifest == null)
			throw new Exception("[Build BundleRelation] 过程发生错误！");

		PackVideo(buildMap);
		//包加密
		Log($"After Encryption 资产清单里总共有{allAssetBundles.Length}个资产");
		// 创建补丁文件
		CreatePatchManifestFile(allAssetBundles);
		//CreatePatchManifestFile(allAssetBundles, true, useAAB);

		//输出文件夹和最终使用的并不是一个文件夹，输出文件夹包含了所有文件，
		CopyUpdateFiles();

		Log("构建完成");
	}

	private EEncryptMethod ResolveEncryptRule(string bundle)
	{
		EEncryptMethod ret = EEncryptMethod.None;
		if (!_labelToAssets.TryGetValue(bundle, out var list))
		{
			Debug.LogError($"!!!!!!! {bundle} not found in buildMap");
			return ret;
		}

		foreach (var assetInfo in list)
		{
			if (assetInfo.EncryptMethod != EEncryptMethod.None)
				return assetInfo.EncryptMethod;
		}
		return ret;
	}

	private void ApplyEncryptRules(ref string[] allBundles, AssetBundleManifest bundleRelationManifest)
	{
		//add res manifest bundle to bundle collections
		var resBundleName = bundleRelationManifest.GetAllAssetBundles()[0];
		var resBundleHash = bundleRelationManifest.GetAssetBundleHash(resBundleName).ToString();
		var resBundleInfo = new BundleInfo()
		{
			Name = resBundleName,
			Deps = new int[0],
			Hash = resBundleHash,
			EncryptMethod = EEncryptMethod.Quick
			// LoadMode = ELoadMode.None
		};
		ArrayUtility.Add(ref allBundles, resBundleName);

		var bundleRelation = GetBundleRelationFile();
		ArrayUtility.Add(ref bundleRelation.Bundles, resBundleInfo);
		var bundleInfoMap = (bundleRelation.Bundles as IEnumerable<BundleInfo>).ToDictionary(_ => _.Name);

		for (int i = 0; i < allBundles.Length; i++)
		{
			var bundleName = allBundles[i];
			EditorUtility.DisplayCancelableProgressBar($"Postprocess Bundle {i + 1}/{allBundles.Length}", bundleName, (i + 1) * 1f / allBundles.Length);

			if (!bundleInfoMap.TryGetValue(bundleName, out var info))
			{
				Debug.LogError($"Cannot found buildInfo for {bundleName}");
				continue;
			}
			var hash = info.Hash;
			var lastBuildHash = GetBundleHashOfLastBuild(bundleName);
			//is new or updated bundle
			if (hash != lastBuildHash)
			{
				var path = $"{OutputPath}/{bundleName}";
				EncryptBundle(info.EncryptMethod, path, info, i == allBundles.Length - 1);
			}
		}
		EditorUtility.ClearProgressBar();
		SaveBuildInfo(bundleRelation.Bundles);
	}

	private void ApplyLangEncryptRules(ref string[] allBundles, AssetBundleManifest bundleRelationManifest)
	{
		//add res manifest bundle to bundle collections
		// var resBundleName = bundleRelationManifest.GetAllAssetBundles()[0];
		// var resBundleHash = bundleRelationManifest.GetAssetBundleHash(resBundleName).ToString();
		// // var resBundleInfo = new BundleInfo()
		// {
		// 	Name = resBundleName,
		// 	Deps = new int[0],
		// 	Hash = resBundleHash,
		// 	EncryptMethod = EEncryptMethod.Quick
		// };
		// ArrayUtility.Add(ref allBundles, resBundleName);

		var bundleRelation = GetBundleRelationFile();
		// ArrayUtility.Add(ref bundleRelation.Bundles, resBundleInfo);
		var bundleInfoMap = (bundleRelation.Bundles as IEnumerable<BundleInfo>).ToDictionary(_ => _.Name);

		for (int i = 0; i < allBundles.Length; i++)
		{
			var bundleName = allBundles[i];
			EditorUtility.DisplayCancelableProgressBar($"Postprocess Bundle {i + 1}/{allBundles.Length}", bundleName, (i + 1) * 1f / allBundles.Length);

			if (!bundleInfoMap.TryGetValue(bundleName, out var info))
			{
				Debug.LogError($"Cannot found buildInfo for {bundleName}");
				continue;
			}
			var hash = info.Hash;
			var lastBuildHash = GetBundleHashOfLastBuild(bundleName);
			//is new or updated bundle
			if (hash != lastBuildHash)
			{
				var path = $"{OutputPath}/{bundleName}";
				EncryptBundle(info.EncryptMethod, path, info, i == allBundles.Length - 1);
			}
		}
		EditorUtility.ClearProgressBar();
		// SaveBuildInfo(bundleRelation.Bundles);
	}

	//填充Bundle之间依赖关系数据，并保存到Asset文件中
	private BundleRelation FillBundleRelation(List<AssetInfo> assetList, AssetBundleManifest buildManifest)
	{
		string[] bundles = buildManifest.GetAllAssetBundles();
		var bundleToId = new Dictionary<string, int>();
		for (int i = 0; i < bundles.Length; i++)
		{
			if (bundles[i] == "assets/Works/Res/font/textmesh pro/res/shaders/tmpro.unity3d")
				Debug.LogError("##");
			bundleToId[bundles[i]] = i;
		}

		var bundleList = new List<BundleInfo>();
		for (int i = 0; i < bundles.Length; i++)
		{
			var bundle = bundles[i];
			var deps = buildManifest.GetAllDependencies(bundle);
			var hash = buildManifest.GetAssetBundleHash(bundle).ToString();

			var encryptMethod = ResolveEncryptRule(bundle);
			bundleList.Add(new BundleInfo()
			{
				Name = bundle,
				Deps = Array.ConvertAll(deps, _ => bundleToId[_]),
				Hash = hash,
				EncryptMethod = encryptMethod
			});
		}

		var assetRefs = new List<AssetRef>();
		var dirs = new List<string>();
		foreach (var assetInfo in assetList)
		{
			if (!assetInfo.IsCollectAsset) continue;
			var dir = Path.GetDirectoryName(assetInfo.AssetPath).Replace("\\", "/");
			CollectionSettingData.ApplyReplaceRules(ref dir);
			var foundIdx = dirs.FindIndex(_ => _.Equals(dir));
			if (foundIdx == -1)
			{
				dirs.Add(dir);
				foundIdx = dirs.Count - 1;
			}

			var nameStr = $"{assetInfo.AssetBundleLabel}.{assetInfo.AssetBundleVariant}".ToLower();
			AssetRef newAssetRef = new AssetRef();
			newAssetRef.Name = Path.GetFileNameWithoutExtension(assetInfo.AssetPath);
			string id = $"{assetInfo.AssetBundleLabel}.{assetInfo.AssetBundleVariant}".ToLower();
			newAssetRef.BundleId = bundleToId[id];
			newAssetRef.DirIdx = foundIdx;
			assetRefs.Add(newAssetRef);
			//assetRefs.Add(new AssetRef()
			//	{
			//		Name = Path.GetFileNameWithoutExtension(assetInfo.AssetPath),
			//		BundleId = bundleToId[$"{assetInfo.AssetBundleLabel}.{assetInfo.AssetBundleVariant}".ToLower()],
			//		DirIdx = foundIdx
			//	});
		}

		var bundleRelation = GetBundleRelationFile();
		bundleRelation.Dirs = dirs.ToArray();
		bundleRelation.Bundles = bundleList.ToArray();
		bundleRelation.AssetRefs = assetRefs.ToArray();
		EditorUtility.SetDirty(bundleRelation);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		return bundleRelation;
	}

	private void SaveBuildInfo(BundleInfo[] bundles)
	{
		var dataPath = $"{OutputPath}/bundles.dat";
		using (var fs = File.OpenWrite(dataPath))
		{
			var bf = new BinaryFormatter();
			var data = new AssetBundleBuildInfo()
			{
				Bundles = bundles
			};
			bf.Serialize(fs, data);
		}
	}
	private void LoadSavedBuildInfo()
	{
		var dataPath = $"{OutputPath}/bundles.dat";
		if (!File.Exists(dataPath)) return;
		using (var fs = File.OpenRead(dataPath))
		{
			var bf = new BinaryFormatter();
			var buildInfo = bf.Deserialize(fs) as AssetBundleBuildInfo;
			_bundleMap.Clear();
			foreach (var b in buildInfo.Bundles)
			{
				_bundleMap[b.Name] = b;
			}
		}
	}

	private BundleRelation GetBundleRelationFile()
	{
		if (_bundleRelation == null)
			_bundleRelation = AssetDatabase.LoadAssetAtPath<BundleRelation>(PathTool.BundleRelationFilePath + ".asset");

		if (_bundleRelation == null)
		{
			_bundleRelation = ScriptableObject.CreateInstance<BundleRelation>();
			AssetDatabase.CreateAsset(_bundleRelation, PathTool.BundleRelationFilePath + ".asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		return _bundleRelation;
	}

	private string GetBundleHashOfLastBuild(string bundleName)
	{
		if (_bundleMap == null || _bundleMap.Count == 0)
		{
			LoadSavedBuildInfo();
		}

		if (_bundleMap.TryGetValue(bundleName, out var bundleInfo))
		{
			return bundleInfo.Hash;
		}

		return "";
	}
	/// <summary>
	/// 获取构建选项
	/// </summary>
	private BuildAssetBundleOptions MakeBuildOptions()
	{
		// For the new build system, unity always need BuildAssetBundleOptions.CollectDependencies and BuildAssetBundleOptions.DeterministicAssetBundle
		// 除非设置ForceRebuildAssetBundle标记，否则会进行增量打包

		BuildAssetBundleOptions opt = BuildAssetBundleOptions.None;
		opt |= BuildAssetBundleOptions.StrictMode; //Do not allow the build to succeed if any errors are reporting during it.
		opt |= BuildAssetBundleOptions.DeterministicAssetBundle;

		if (CompressOption == ECompressOption.Uncompressed)
			opt |= BuildAssetBundleOptions.UncompressedAssetBundle;
		else if (CompressOption == ECompressOption.ChunkBasedCompressionLZ4)
			opt |= BuildAssetBundleOptions.ChunkBasedCompression;
		if (IsForceRebuild)
			opt |= BuildAssetBundleOptions.ForceRebuildAssetBundle; //Force rebuild the asset bundles
		if (IsAppendHash)
			opt |= BuildAssetBundleOptions.AppendHashToAssetBundleName; //Append the hash to the assetBundle name
		if (IsDisableWriteTypeTree)
			opt |= BuildAssetBundleOptions.DisableWriteTypeTree; //Do not include type information within the asset bundle (don't write type tree).
		if (IsIgnoreTypeTreeChanges)
			opt |= BuildAssetBundleOptions.IgnoreTypeTreeChanges; //Ignore the type tree changes when doing the incremental build check.
		if (UseFullPath)
			opt |= (BuildAssetBundleOptions.DisableLoadAssetByFileName | BuildAssetBundleOptions.DisableLoadAssetByFileNameWithExtension);

		return opt;
	}

	private void Log(string log)
	{
		Debug.Log($"[BuildPatch] {log}");
	}

	public string GetPackageFolderPath()
	{
		return $"{_outputRoot}/{BuildTarget}/{BuildVersion}";
	}


	public enum BuildMapType { 
		AllRes = 0,
		Lua = 1,
	}
	#region 准备工作
	/// <summary>
	/// 准备工作
	/// </summary>
	private List<AssetInfo> GetBuildMap()
	{
		int progressBarCount = 0;
		Dictionary<string, AssetInfo> allAsset = new Dictionary<string, AssetInfo>();
		List<string> collectPathList = null;
		// 获取所有的收集路径
		collectPathList = CollectionSettingData.GetAllCollectPath();

        if (collectPathList.Count == 0)
			throw new Exception("[BuildPatch] 配置的打包路径列表为空");

		// 获取所有资源
		string[] guids = AssetDatabase.FindAssets(string.Empty, collectPathList.ToArray());
		foreach (string guid in guids)
		{
			string mainAssetPath = AssetDatabase.GUIDToAssetPath(guid);
			if (CollectionSettingData.IsIgnoreAsset(mainAssetPath))
				continue;
			if (ValidateAsset(mainAssetPath) == false)
				continue;

			List<AssetInfo> depends = GetDependencies(mainAssetPath);
			for (int i = 0; i < depends.Count; i++)
			{
				AssetInfo assetInfo = depends[i];
                if (CollectionSettingData.IsIgnoreAsset(assetInfo.AssetPath))
                    continue;
                if (allAsset.ContainsKey(assetInfo.AssetPath))
				{
					AssetInfo cacheInfo = allAsset[assetInfo.AssetPath];
					cacheInfo.DependCount++;
				}
				else
				{
					allAsset.Add(assetInfo.AssetPath, assetInfo);
				}
			}

			// 进度条
			progressBarCount++;
			EditorUtility.DisplayProgressBar("进度", $"依赖文件分析：{progressBarCount}/{guids.Length}", (float)progressBarCount / guids.Length);
		}
		EditorUtility.ClearProgressBar();
		progressBarCount = 0;

		// 移除零依赖的资源
		List<string> removeList = new List<string>();
		foreach (KeyValuePair<string, AssetInfo> pair in allAsset)
		{
			if (pair.Value.IsCollectAsset)
				continue;
			if (pair.Value.DependCount == 0)
				removeList.Add(pair.Value.AssetPath);
		}
		for (int i = 0; i < removeList.Count; i++)
		{
			allAsset.Remove(removeList[i]);
		}

		// 设置资源标签
		foreach (KeyValuePair<string, AssetInfo> pair in allAsset)
		{
			SetAssetBundleLabelAndVariant(pair.Value);
			SetAssetEncrypt(pair.Value);
			// 进度条
			progressBarCount++;
			EditorUtility.DisplayProgressBar("进度", $"设置资源标签：{progressBarCount}/{allAsset.Count}", (float)progressBarCount / allAsset.Count);
		}
		EditorUtility.ClearProgressBar();
		progressBarCount = 0;

		// Dictionary<string, long> dicSortedBySize = allAsset.OrderBy(o => o.Value.sizeKB).ToDictionary(p => p.Key, o => o.Value.sizeKB);
		// foreach (KeyValuePair<string, long> pair in dicSortedBySize)
		// {
		// 	Log($"AB asset: {pair.Key}, sizeKB： {pair.Value}");
		// }

		// 返回结果
		return allAsset.Values.ToList();
	}
	/// <summary>
	/// 获取指定资源依赖的资源列表
	/// 注意：返回列表里已经包括主资源自己
	/// </summary>
	private List<AssetInfo> GetDependencies(string assetPath)
	{
		List<AssetInfo> depends = new List<AssetInfo>();
		string[] dependArray = AssetDatabase.GetDependencies(assetPath, true);
		foreach (string dependPath in dependArray)
		{
			if (ValidateAsset(dependPath))
			{
				AssetInfo assetInfo = new AssetInfo(dependPath);
				depends.Add(assetInfo);
			}
		}
		return depends;
	}

	/// <summary>
	/// 检测资源是否有效
	/// </summary>
	private bool ValidateAsset(string assetPath)
	{
		// 不是Assets开头的无效
		if (!assetPath.StartsWith("Assets/"))
			return false;

		// 文件夹无效
		if (AssetDatabase.IsValidFolder(assetPath))
			return false;

		// 特定扩展名无效
		string ext = System.IO.Path.GetExtension(assetPath);
		if (ext == "" || ext == ".dll" || ext == ".cs" || ext == ".js" || ext == ".boo" || ext == ".meta")
			return false;

		return true;
	}

	private void SetAssetEncrypt(AssetInfo assetInfo)
	{
		assetInfo.EncryptMethod = CollectionSettingData.GetEncryptRule(assetInfo.AssetPath);
	}
	/// <summary>
	/// 设置资源的标签和变种
	/// </summary>
	private void SetAssetBundleLabelAndVariant(AssetInfo assetInfo)
	{
		string label = CollectionSettingData.GetAssetBundleLabel(assetInfo.AssetPath);
		string variant = VariantCollector.GetVariantByAssetPath(label);
		if (string.IsNullOrEmpty(variant))
		{
			variant = PatchDefine.AssetBundleDefaultVariant;
		}
		else
		{
			label = label.Replace(variant, "");
			variant = variant.Substring(1);
		}
		if (IsNameByHash)
			assetInfo.AssetBundleLabel = HashUtility.BytesMD5(Encoding.UTF8.GetBytes(label));
		else
			assetInfo.AssetBundleLabel = label;

		assetInfo.ReadableLabel = label;
		assetInfo.AssetBundleVariant = variant;
		assetInfo.bundlePos = CollectionSettingData.GetAssetBundlePos(assetInfo.AssetPath);
		// assetInfo.sizeKB = EditorTools.GetFileSize(assetInfo.AssetPath);
	}
	#endregion

	#region 视频相关
	private void PackVideo(List<AssetInfo> buildMap)
	{
		// 注意：在Unity2018.4截止的版本里，安卓还不支持压缩的视频Bundle
		if (BuildTarget == BuildTarget.Android)
		{
			Log($"开始视频单独打包（安卓平台）");
			for (int i = 0; i < buildMap.Count; i++)
			{
				AssetInfo assetInfo = buildMap[i];
				if (assetInfo.IsVideoAsset)
				{
					BuildAssetBundleOptions opt = BuildAssetBundleOptions.None;
					opt |= BuildAssetBundleOptions.DeterministicAssetBundle;
					opt |= BuildAssetBundleOptions.StrictMode;
					opt |= BuildAssetBundleOptions.UncompressedAssetBundle;
					var videoObj = AssetDatabase.LoadAssetAtPath<UnityEngine.Video.VideoClip>(assetInfo.AssetPath);
					string outPath = OutputPath + "/" + assetInfo.AssetBundleLabel.ToLower();
					bool result = BuildPipeline.BuildAssetBundle(videoObj, new[] { videoObj }, outPath, opt, BuildTarget);
					if (result == false)
						throw new Exception($"视频单独打包失败：{assetInfo.AssetPath}");
				}
			}
		}
	}
	#endregion

	#region 文件加密
	private void EncryptBundle(EEncryptMethod method, string srcPath, BundleInfo info, bool isManifest)
	{
		if (method == EEncryptMethod.None) return;

		var content = File.ReadAllBytes(srcPath);
		if (method == EEncryptMethod.Quick)
		{
			//cannot get bundle-hash when loading BundleRelation, use NameHash instead
			var key = isManifest ? info.Name : info.Hash;
			byte[] hashBytes = Encoding.UTF8.GetBytes(key);
			int ones = 0;
			for (int i = 0; i < 4; i++)
			{
				var v = hashBytes[i];
				ones += ((v >> 6) & 1) + ((v >> 5) & 1) + ((v >> 4) & 1) + ((v >> 3) & 1) + ((v >> 2) & 1) + ((v >> 1) & 1) + (v & 1);
			}
			var len = content.Length + ones;
			var buffer = new byte[len];
			using (var fs = File.OpenWrite(srcPath))
			{
				Array.Copy(content, 0, buffer, 0, ones);
				Array.Copy(content, 0, buffer, ones, content.Length);
				fs.Write(buffer, 0, len);
			}
		}
		else if (method == EEncryptMethod.Simple)
		{
			var buffer = XXTEA.Encrypt(content, info.Hash);
			using (var fs = File.OpenWrite(srcPath))
			{
				fs.Write(buffer, 0, buffer.Length);
			}
		}
		else if (method == EEncryptMethod.X)
		{
			AssetEncrypter.XEncryptInplace(content, Encoding.UTF8.GetBytes(info.Hash));
			using (var fs = File.OpenWrite(srcPath))
			{
				fs.Write(content, 0, content.Length);
			}
		}
		else if (method == EEncryptMethod.QuickX)
		{
			AssetEncrypter.QuickXEncryptInplace(content, Encoding.UTF8.GetBytes(info.Hash));
			using (var fs = File.OpenWrite(srcPath))
			{
				fs.Write(content, 0, content.Length);
			}
		}
	}

	private void EncryptFiles(List<string> filePaths)
	{
		Log($"开始加密资源文件");

		int progressBarCount = 0;
		foreach (string assetName in filePaths)
		{
			string path = assetName;
			string outPath = path.Replace(".txt", ".bytes");

			string assetPath = assetName.Replace("\\", "/");
			assetPath = assetPath.Substring(assetPath.IndexOf("Assets/", StringComparison.Ordinal));

			TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath);
			if (textAsset == null)
			{
				Log($"Failed to load {assetPath}");
			}

			//Log($"开始加密：{path}");
			assetPath = assetPath.Replace("TempLuaCode", "Lua").Replace(".txt", "");
			var startIndex = assetPath.IndexOf("/Lua/") + 1;
			string key = assetPath.Substring(startIndex);
			var bytes = AssetEncrypter.XEncrypt(textAsset.bytes, Encoding.UTF8.GetBytes(key));
			File.WriteAllBytes(outPath, bytes);
			//Log($"文件加密完成：{path}");

			// 进度条
			progressBarCount++;
			EditorUtility.DisplayProgressBar("进度", $"加密LUA: {progressBarCount}/{filePaths.Count}", (float)progressBarCount / filePaths.Count);
		}
		EditorUtility.ClearProgressBar();
		progressBarCount = 0;
	}

	private void InitAssetEncrypter()
	{
		//_encrypterType = Type.GetType("AssetEncrypter");
	}
	private bool AssetEncrypterCheck(string filePath)
	{
		return AssetEncrypter.Check(filePath);
	}
	private string AssetEncrypterEncrypt(string data)
	{
		return AssetEncrypter.Encrypt(data);
	}
	#endregion

	#region 文件相关

	/// <summary>
	/// 1. 创建补丁清单文件到输出目录
	/// params: isInit 创建的是否是包内的补丁清单
	///         useAAB 创建的是否是aab包使用的补丁清单
	/// </summary>
	private void CreatePatchManifestFile(string[] allAssetBundles)
	{
		// 加载旧文件
		PatchManifest patchManifest = LoadPatchManifestFile();

		// 删除旧文件
		string filePath = OutputPath + $"/{PatchDefine.PatchManifestFileName}";
		if (File.Exists(filePath))
			File.Delete(filePath);

		// 创建新文件
		Log($"创建补丁清单文件：{filePath}");
		var sb = new StringBuilder();
		using (FileStream fs = File.OpenWrite(filePath))
		{
			using (var bw = new BinaryWriter(fs))
			{
				int ver = BuildVersion;
				// 写入版本号
				bw.Write(ver);
				sb.AppendLine(ver.ToString());

				// 写入所有AssetBundle文件的信息
				var fileCount = allAssetBundles.Length;
				bw.Write(fileCount);
				for (var i = 0; i < fileCount; i++)
				{
					var assetName = allAssetBundles[i];
					string path = $"{OutputPath}/{assetName}";
					string md5 = HashUtility.FileMD5(path);
					long fileSize = EditorTools.GetFileSize(path);
					int version = BuildVersion;
					EBundlePos tag = EBundlePos.buildin;
					string ResEditorPath = "undefined";//查看编辑器路径
					if (_labelToAssets.TryGetValue(assetName, out var list))
					{
						ResEditorPath = list[0].ReadableLabel;
						tag = list[0].bundlePos;
					}

					// 注意：如果文件没有变化使用旧版本号
					PatchElement element;
					if (patchManifest.Elements.TryGetValue(assetName, out element))
					{
						if (element.MD5 == md5)
							version = element.Version;
					}
					var curEle = new PatchElement(assetName, md5, version, fileSize, tag.ToString());
					curEle.Serialize(bw);

					sb.AppendLine($"{assetName}={md5}={version}={fileSize}={tag.ToString()}");
				}
			}

			string txtName = "PatchManifest.txt";
			File.WriteAllText(OutputPath + "/" + txtName, sb.ToString());
			Debug.Log($"{OutputPath}/{txtName} OK");
		}
	}

	/// <summary>
	/// 3. 复制更新文件到补丁包目录
	/// </summary>
	private void CopyUpdateFiles()
	{
		string packageFolderPath = GetPackageFolderPath();
		Log($"开始复制更新文件到补丁包目录：{packageFolderPath}");

		// 复制所有更新文件
		PatchManifest patchFile = LoadPatchManifestFile();
		foreach (var pair in patchFile.Elements)
		{
            //if (pair.Value.Version == BuildVersion)
            string sourcePath = $"{OutputPath}/{pair.Key}";
            string destPath = $"{packageFolderPath}/{pair.Key}";
            EditorTools.CopyFile(sourcePath, destPath, true);
		}
		Log($"复制更新文件个数：{patchFile.Elements.Count}");

		//bundleRelation的名字是用文件路径生成的MD5
		string BundleRelationFileName = HashUtility.BytesMD5(Encoding.UTF8.GetBytes(PathTool.BundleRelationFilePath)) + ".unity3d";
		//复制bundleRelation到版本文件夹
		string bundleRelationSou = $"{OutputPath}/{PatchDefine.PatchManifestFileName}";
		string bundleRelationDes = $"{packageFolderPath}/{PatchDefine.PatchManifestFileName}";
		EditorTools.CopyFile(bundleRelationSou, bundleRelationDes, true);

		//复制AB包清单到版本文件夹
        string patchManifestSou = $"{OutputPath}/{BundleRelationFileName}";
        string patchManifestDes = $"{packageFolderPath}/{BundleRelationFileName}";
        EditorTools.CopyFile(patchManifestSou, patchManifestDes, true);
	}

	/// <summary>
	/// 从输出目录加载补丁清单文件
	/// </summary>
	private PatchManifest LoadPatchManifestFile()
	{
		string filePath = PatchDefine.PatchManifestFileName;

		filePath = $"{OutputPath}/{filePath}";

		PatchManifest patchFile = new PatchManifest();

		// 如果文件不存在
		if (File.Exists(filePath) == false)
			return patchFile;

		patchFile.ParseFile(filePath);
		return patchFile;
	}
	#endregion

	//加密Lua文件
	private void EncryptLuaFiles()
	{
		//把lua代码拷贝到临时目录
		string tempLuaPath = Application.dataPath + "/Works/Res/TempLuaCode";
		string luaPath = Application.dataPath + "/Works/Res/Lua";
		// FileUtils.DeleteDir(tempLuaPath);
		List<string> includeTypes = new List<string>();
		includeTypes.Add(".lua.txt");//只复制代码文件
		Log("Copy Lua to LuaTemp.");
		FileUtils.CopyDirectInfo(luaPath, tempLuaPath, includeTypes);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		if (AssetEncrypter.IsEncrypt)
		{
			List<string> allFiles = new List<string>();
			List<string> allLuaFiles = new List<string>();
			FileUtils.GetAllFilePaths(tempLuaPath, ref allFiles);
			for (int i = 0; i < allFiles.Count; i++)
			{
				if (allFiles[i].EndsWith(".lua.txt"))
				{
					allLuaFiles.Add(allFiles[i]);
				}
			}

			// 加密lua代码
			EncryptFiles(allLuaFiles);

			// 删除原文件
			for (int i = 0; i < allLuaFiles.Count; i++)
			{
				File.Delete(allLuaFiles[i]);
				File.Delete(allLuaFiles[i] + ".meta");
			}
		}

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
}

[Serializable]
public class AssetBundleBuildInfo
{
	public BundleInfo[] Bundles = new BundleInfo[0];
}
