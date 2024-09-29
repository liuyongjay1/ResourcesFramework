//--------------------------------------------------

//--------------------------------------------------
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class AssetBundleBuilderWindow : EditorWindow
{
	static AssetBundleBuilderWindow _thisInstance;

	[MenuItem("MotionTools/AssetBundle Builder", false, 104)]
	static void ShowWindow()
	{
		if (_thisInstance == null)
		{
			_thisInstance = EditorWindow.GetWindow(typeof(AssetBundleBuilderWindow), false, "资源包构建工具", true) as AssetBundleBuilderWindow;
			_thisInstance.minSize = new Vector2(800, 600);
		}
		_thisInstance.Show();
	}

	// GUI相关
	private GUIStyle _leftStyle;
	private bool _showSettingFoldout = true;
	private bool _showToolsFoldout = true;

	/// <summary>
	/// 构建器
	/// </summary>
	private AssetBundleBuilder _assetBuilder = null;


	private void InitInternal()
	{
		if (_assetBuilder != null)
			return;


		_leftStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
		_leftStyle.alignment = TextAnchor.MiddleLeft;

		// 创建资源打包器
		var buildVersion = 1;
		var buildTarget = EditorUserBuildSettings.activeBuildTarget;
		_assetBuilder = new AssetBundleBuilder(buildTarget, buildVersion);

		// 读取配置
		LoadSettingsFromPlayerPrefs(_assetBuilder);
	}
	private void OnGUI()
	{
        // 初始化
        InitInternal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Android", GUILayout.MaxWidth(200), GUILayout.MaxHeight(40)))
        {
			_assetBuilder.SetBuildTarget(BuildTarget.Android);
		}
		GUILayout.Space(40);

		if (GUILayout.Button("IOS", GUILayout.MaxWidth(200), GUILayout.MaxHeight(40)))
        {
            _assetBuilder.SetBuildTarget(BuildTarget.iOS);
        }
        GUILayout.Space(40);

        if (GUILayout.Button("Windows", GUILayout.MaxWidth(200), GUILayout.MaxHeight(40)))
        {
            _assetBuilder.SetBuildTarget(BuildTarget.StandaloneWindows);
        }

        GUILayout.EndHorizontal();

		GUILayout.BeginVertical();


		// 标题
		EditorGUILayout.Space();

		// 构建版本
		_assetBuilder.BuildVersion = EditorGUILayout.IntField("Build Version", _assetBuilder.BuildVersion, GUILayout.MaxWidth(250));

		// 输出路径
		EditorGUILayout.LabelField("Build Output", _assetBuilder.OutputPath);
			

		// 构建选项
		EditorGUILayout.Space();
		_assetBuilder.IsForceRebuild = GUILayout.Toggle(_assetBuilder.IsForceRebuild, "Froce Rebuild", GUILayout.MaxWidth(120));

		// 高级选项
		using (new EditorGUI.DisabledScope(false))
		{
			EditorGUILayout.Space();
			_showSettingFoldout = EditorGUILayout.Foldout(_showSettingFoldout, "Advanced Settings");
			if (_showSettingFoldout)
			{
				int indent = EditorGUI.indentLevel;
				EditorGUI.indentLevel = 1;
				_assetBuilder.CompressOption = (AssetBundleBuilder.ECompressOption)EditorGUILayout.EnumPopup("Compression", _assetBuilder.CompressOption);
				_assetBuilder.IsAppendHash = EditorGUILayout.ToggleLeft("Append Hash", _assetBuilder.IsAppendHash, GUILayout.MaxWidth(120));
				_assetBuilder.IsDisableWriteTypeTree = EditorGUILayout.ToggleLeft("Disable Write Type Tree", _assetBuilder.IsDisableWriteTypeTree, GUILayout.MaxWidth(200));
				_assetBuilder.IsIgnoreTypeTreeChanges = EditorGUILayout.ToggleLeft("Ignore Type Tree Changes", _assetBuilder.IsIgnoreTypeTreeChanges, GUILayout.MaxWidth(200));
				_assetBuilder.IsNameByHash = EditorGUILayout.ToggleLeft("Name By Hash", _assetBuilder.IsNameByHash, GUILayout.MaxWidth(120));
				//_assetBuilder.EncryptMethod = (EEncryptMethod)EditorGUILayout.EnumPopup("PackMode", _assetBuilder.EncryptMethod);
				_assetBuilder.IsUseVFS = EditorGUILayout.ToggleLeft("Use VFS", _assetBuilder.IsUseVFS, GUILayout.MaxWidth(120));
				EditorGUI.indentLevel = indent;
			}
		}

		// 构建按钮
		EditorGUILayout.Space();
		//全资源打包
		if (GUILayout.Button("Build--All", GUILayout.MaxWidth(300), GUILayout.MaxHeight(40)))
		{
			string title;
			string content;
			if (_assetBuilder.IsForceRebuild)
			{
				title = "警告";
				content = "确定开始强制构建吗，这样会删除所有已有构建的文件";
			}
			else
			{
				title = "提示";
				content = "确定开始增量构建吗";
			}
			if (EditorUtility.DisplayDialog(title, content, "Yes", "No"))
			{
				// 清空控制台
				EditorTools.ClearUnityConsole();

				// 存储配置
				SaveSettingsToPlayerPrefs(_assetBuilder);

				EditorApplication.delayCall += ExecuteBuild;
			}
		}
      
        // 绘制工具栏部分
        OnDrawGUITools();

        GUILayout.EndVertical();
    }
    private void OnDrawGUITools()
	{
		GUILayout.Space(50);
		using (new EditorGUI.DisabledScope(false))
		{
			_showToolsFoldout = EditorGUILayout.Foldout(_showToolsFoldout, "工具");
			if (_showToolsFoldout)
			{
				EditorGUILayout.Space();

				if (GUILayout.Button("检测损坏预制件", GUILayout.MaxWidth(120), GUILayout.MaxHeight(40)))
				{
					EditorApplication.delayCall += CheckAllPrefabValid;
				}

				if (GUILayout.Button("拷贝版本到StreamingAssets目录", GUILayout.MaxWidth(260), GUILayout.MaxHeight(40)))
				{
					EditorApplication.delayCall += RefreshStreammingFolder;
				}

				//if (GUILayout.Button("清空并拷贝所有补丁包到UnityManifest目录", GUILayout.MaxWidth(260), GUILayout.MaxHeight(40)))
				//{
				//	EditorApplication.delayCall += RefreshOutputMainFolder;
				//}

				//if (GUILayout.Button("清空并拷贝VFS到StreamingAssets目录", GUILayout.MaxWidth(260), GUILayout.MaxHeight(40)))
				//{
				//	EditorApplication.delayCall += RefreshStreammingFolderWithVFS;
    //            }
                if (GUILayout.Button("显示打包根目录", GUILayout.MaxWidth(260), GUILayout.MaxHeight(40)))
                {
                    EditorApplication.delayCall += OpenBuildFolder;
                }
            }
		}
	}

	/// <summary>
	/// 执行构建
	/// </summary>
	private void ExecuteBuild()
	{
		_assetBuilder.PreAssetBuild();
		_assetBuilder.PostAssetBuild();
	}

    /// <summary>
    /// 检测预制件是否损坏
    /// </summary>
    private void CheckAllPrefabValid()
	{
		// 获取所有的打包路径
		List<string> packPathList = CollectionSettingData.GetAllCollectPath();
		if (packPathList.Count == 0)
			throw new Exception("[BuildPackage] 打包路径列表不能为空");

		// 获取所有资源列表
		int checkCount = 0;
		int invalidCount = 0;
		string[] guids = AssetDatabase.FindAssets(string.Empty, packPathList.ToArray());
		foreach (string guid in guids)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guid);
			string ext = System.IO.Path.GetExtension(assetPath);
			if (ext == ".prefab")
			{
				UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));
				if (prefab == null)
				{
					invalidCount++;
					Debug.LogError($"[Build] 发现损坏预制件：{assetPath}");
				}
			}

			// 进度条相关
			checkCount++;
			EditorUtility.DisplayProgressBar("进度", $"检测预制件文件是否损坏：{checkCount}/{guids.Length}", (float)checkCount / guids.Length);
		}

		EditorUtility.ClearProgressBar();
		if (invalidCount == 0)
			Debug.Log($"没有发现损坏预制件");
	}

	/// <summary>
	/// 刷新流目录
	/// </summary>
	private void RefreshStreammingFolder()
	{
		string streamingPath = Application.dataPath + "/StreamingAssets";
		EditorTools.ClearFolder(streamingPath);

		string outputRoot = AssetBundleBuilderHelper.GetDefaultOutputRootPath();
		AssetBundleBuilderHelper.CopyPackageToStreamingFolder(_assetBuilder.BuildTarget, outputRoot);
	}
	//private void RefreshStreammingFolderWithVFS()
	//{
	//	string streamingPath = Application.dataPath + "/StreamingAssets";
	//	EditorTools.ClearFolder(streamingPath);

	//	string outputRoot = AssetBundleBuilderHelper.GetDefaultOutputRootPath();
	//	AssetBundleBuilderHelper.CopyPakToStreamingFolder(_assetBuilder.BuildTarget, outputRoot);
	//}

	/// <summary>
	/// 刷新输出目录
	/// </summary>
	private void RefreshOutputMainFolder()
	{
		string outputPath = _assetBuilder.OutputPath;
		EditorTools.ClearFolder(outputPath);

		string outputRoot = AssetBundleBuilderHelper.GetDefaultOutputRootPath();
		AssetBundleBuilderHelper.CopyPackageToUnityManifestFolder(_assetBuilder.BuildTarget, outputRoot);
	}

	//开启打包目录
    private void OpenBuildFolder()
    {
		string outputRoot = AssetBundleBuilderHelper.GetDefaultOutputRootPath();
		//System.Diagnostics.Process.Start(outputRoot);

        var fol = new System.Diagnostics.ProcessStartInfo(outputRoot);
		fol.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
		System.Diagnostics.Process.Start(fol);
    }

    #region 设置相关
    private const string StrEditorCompressOption = "StrEditorCompressOption";
	private const string StrEditorIsForceRebuild = "StrEditorIsForceRebuild";
	private const string StrEditorIsAppendHash = "StrEditorIsAppendHash";
	private const string StrEditorIsDisableWriteTypeTree = "StrEditorIsDisableWriteTypeTree";
	private const string StrEditorIsIgnoreTypeTreeChanges = "StrEditorIsIgnoreTypeTreeChanges";
	private const string StrEditorIsUsePlayerSettingVersion = "StrEditorIsUsePlayerSettingVersion";
	private const string StrEditorIsNameByHash = "StrEditorIsNameByHash";
	private const string StrEditorIsUseVFS = "StrEditorIsUseVFS";

	/// <summary>
	/// 存储配置
	/// </summary>
	private static void SaveSettingsToPlayerPrefs(AssetBundleBuilder builder)
	{
		EditorTools.PlayerSetEnum<AssetBundleBuilder.ECompressOption>(StrEditorCompressOption, builder.CompressOption);
		EditorTools.PlayerSetBool(StrEditorIsForceRebuild, builder.IsForceRebuild);
		EditorTools.PlayerSetBool(StrEditorIsAppendHash, builder.IsAppendHash);
		EditorTools.PlayerSetBool(StrEditorIsDisableWriteTypeTree, builder.IsDisableWriteTypeTree);
		EditorTools.PlayerSetBool(StrEditorIsIgnoreTypeTreeChanges, builder.IsIgnoreTypeTreeChanges);
		EditorTools.PlayerSetBool(StrEditorIsAppendHash, builder.IsNameByHash);
		EditorTools.PlayerSetBool(StrEditorIsUseVFS, builder.IsUseVFS);
	}

	/// <summary>
	/// 读取配置
	/// </summary>
	private static void LoadSettingsFromPlayerPrefs(AssetBundleBuilder builder)
	{
		builder.CompressOption = EditorTools.PlayerGetEnum<AssetBundleBuilder.ECompressOption>(StrEditorCompressOption, AssetBundleBuilder.ECompressOption.Uncompressed);
		builder.IsForceRebuild = EditorTools.PlayerGetBool(StrEditorIsForceRebuild, false);
		builder.IsAppendHash = EditorTools.PlayerGetBool(StrEditorIsAppendHash, false);
		builder.IsDisableWriteTypeTree = EditorTools.PlayerGetBool(StrEditorIsDisableWriteTypeTree, false);
		builder.IsIgnoreTypeTreeChanges = EditorTools.PlayerGetBool(StrEditorIsIgnoreTypeTreeChanges, false);
		builder.IsNameByHash = EditorTools.PlayerGetBool(StrEditorIsNameByHash, false);
		builder.IsUseVFS = EditorTools.PlayerGetBool(StrEditorIsUseVFS, false);
	}
	#endregion
}
