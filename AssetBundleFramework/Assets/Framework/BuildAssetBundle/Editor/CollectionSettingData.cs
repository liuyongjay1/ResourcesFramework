//--------------------------------------------------

//--------------------------------------------------
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public static class CollectionSettingData
{
	public static CollectionSetting Setting;
	private static long bigSizeMB = 1024;

	static CollectionSettingData()
	{
        // 加载配置文件
        Setting = AssetDatabase.LoadAssetAtPath<CollectionSetting>(PathTool.CollectorSettingFilePath);
        if (Setting == null)
		{
			Debug.LogWarning($"Create new CollectionSetting.asset : {PathTool.CollectorSettingFilePath}");
			Setting = ScriptableObject.CreateInstance<CollectionSetting>();
			EditorTools.CreateFileDirectory(PathTool.CollectorSettingFilePath);
			AssetDatabase.CreateAsset(Setting, PathTool.CollectorSettingFilePath);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
		else
		{
			Debug.Log("Load CollectionSetting.asset ok");
		}
	}

	/// <summary>
	/// 存储文件
	/// </summary>
	public static void SaveFile()
	{
		if (Setting != null)
		{
			EditorUtility.SetDirty(Setting);
			AssetDatabase.SaveAssets();
		}
	}

	/// <summary>
	/// 添加元素
	/// </summary>
	public static void AddElement(string folderPath)
	{
		if (IsContainsElement(folderPath) == false)
		{
			CollectionSetting.Wrapper element = new CollectionSetting.Wrapper();
			element.FolderPath = folderPath;
			Setting.Elements.Add(element);
			SaveFile();
		}
	}

	/// <summary>
	/// 移除元素
	/// </summary>
	public static void RemoveElement(string folderPath)
	{
		for (int i = 0; i < Setting.Elements.Count; i++)
		{
			if (Setting.Elements[i].FolderPath == folderPath)
			{
				Setting.Elements.RemoveAt(i);
				break;
			}
		}
		SaveFile();
	}


	public static void AddInGameDir(string folderPath)
    {
		if (!Setting.InGames.Contains(folderPath))
        {
			Setting.InGames.Add(folderPath);
			SaveFile();
		}
	}

	public static void RemoveInGameDir(string folderPath)
	{
		for (int i = 0; i < Setting.InGames.Count; i++)
		{
			if (Setting.InGames[i] == folderPath)
			{
				Setting.InGames.RemoveAt(i);
				break;
			}
		}
		SaveFile();
	}

	/// <summary>
	/// 编辑元素
	/// </summary>
	public static void ModifyElement(string folderPath, CollectionSetting.EFolderPackRule packRule, CollectionSetting.EBundleLabelRule labelRule, EEncryptMethod encryptMethod, EAssetDeliveryMode deliveryMode, EBundlePos bundlePos)
	{
		// 注意：这里强制修改忽略文件夹的命名规则为None
		if (packRule == CollectionSetting.EFolderPackRule.Ignore)
			labelRule = CollectionSetting.EBundleLabelRule.None;
		else if (labelRule == CollectionSetting.EBundleLabelRule.None)
			labelRule = CollectionSetting.EBundleLabelRule.LabelByFilePath;

		for (int i = 0; i < Setting.Elements.Count; i++)
		{
			if (Setting.Elements[i].FolderPath == folderPath)
			{
				Setting.Elements[i].PackRule = packRule;
				Setting.Elements[i].LabelRule = labelRule;
				Setting.Elements[i].EncryptRule = encryptMethod;
				//Setting.Elements[i].DeliveryMode = deliveryMode;
				Setting.Elements[i].BundlePos = bundlePos;
				break;
			}
		}
		SaveFile();
	}

	/// <summary>
	/// 是否包含元素
	/// </summary>
	public static bool IsContainsElement(string folderPath)
	{
		for (int i = 0; i < Setting.Elements.Count; i++)
			if (Setting.Elements[i].FolderPath == folderPath) return true;

		return false;
	}

	public static CollectionSetting.Wrapper GetElement(string folderPath)
    {
		for (int i = 0; i < Setting.Elements.Count; i++)
			if (Setting.Elements[i].FolderPath == folderPath) return Setting.Elements[i];

		return null;
	}

	/// <summary>
	/// 获取所有的打包路径
	/// </summary>
	public static List<string> GetAllCollectPath()
	{
        if (Setting == null)
        {
			Debug.LogError("Setting is null");
        }
        List<string> result = new List<string>();
		for (int i = 0; i < Setting.Elements.Count; i++)
		{
			CollectionSetting.Wrapper wrapper = Setting.Elements[i];
			if (wrapper.PackRule == CollectionSetting.EFolderPackRule.Collect)
				result.Add(wrapper.FolderPath);
		}
		return result;
	}

	public static List<string> GetLuaCollectPath()
	{
		List<string> result = new List<string>();
		result.Add("Assets/Works/Res/Lua");
		return result;
	}

	/// <summary>
	/// 是否收集该资源
	/// </summary>
	public static bool IsCollectAsset(string assetPath)
	{
		for (int i = 0; i < Setting.Elements.Count; i++)
		{
			CollectionSetting.Wrapper wrapper = Setting.Elements[i];
			if (wrapper.PackRule == CollectionSetting.EFolderPackRule.Collect && assetPath.StartsWith(wrapper.FolderPath))
				return true;
		}

		return false;
	}

	/// <summary>
	/// 是否忽略该资源
	/// </summary>
	public static bool IsIgnoreAsset(string assetPath)
	{
		for (int i = 0; i < Setting.Elements.Count; i++)
		{
			CollectionSetting.Wrapper wrapper = Setting.Elements[i];
			if (wrapper.PackRule == CollectionSetting.EFolderPackRule.Ignore && assetPath.StartsWith(wrapper.FolderPath))
				return true;
		}

		return false;
	}

		
	private static bool IsSubPath(string folderA, string assetPath)
    {
		if(assetPath.StartsWith(folderA))
			return assetPath.Replace(folderA, "").StartsWith("/");

		return false;
    }

	public static EEncryptMethod GetEncryptRule(string assetPath)
    {
		// 注意：一个资源有可能被多个规则覆盖
		List<CollectionSetting.Wrapper> filterWrappers = new List<CollectionSetting.Wrapper>();
		for (int i = 0; i < Setting.Elements.Count; i++)
		{
			CollectionSetting.Wrapper wrapper = Setting.Elements[i];
			if (IsSubPath(wrapper.FolderPath, assetPath))
				filterWrappers.Add(wrapper);
		}

		// 我们使用路径最深层的规则
		CollectionSetting.Wrapper findWrapper = null;
		for (int i = 0; i < filterWrappers.Count; i++)
		{
			CollectionSetting.Wrapper wrapper = filterWrappers[i];
			if (findWrapper == null)
			{
				findWrapper = wrapper;
				continue;
			}
			if (wrapper.FolderPath.Length > findWrapper.FolderPath.Length)
				findWrapper = wrapper;
		}

		// 如果没有找到命名规则
		if (findWrapper == null) return EEncryptMethod.None;

		return findWrapper.EncryptRule;
	}

	/// <summary>
	/// 获取资源的打包标签
	/// </summary>
	public static string GetAssetBundleLabel(string assetPath)
	{
		// 注意：一个资源有可能被多个规则覆盖
		List<CollectionSetting.Wrapper> filterWrappers = new List<CollectionSetting.Wrapper>();
		for (int i = 0; i < Setting.Elements.Count; i++)
		{
			CollectionSetting.Wrapper wrapper = Setting.Elements[i];
			if (IsSubPath(wrapper.FolderPath, assetPath))
				filterWrappers.Add(wrapper);
        }

        // 我们使用路径最深层的规则
        CollectionSetting.Wrapper findWrapper = null;
		for (int i = 0; i < filterWrappers.Count; i++)
		{
			CollectionSetting.Wrapper wrapper = filterWrappers[i];
			if (findWrapper == null)
			{
				findWrapper = wrapper;
				continue;
			}
			if (wrapper.FolderPath.Length > findWrapper.FolderPath.Length)
				findWrapper = wrapper;
		}

		// 如果没有找到命名规则
		if (findWrapper == null) return assetPath.Remove(assetPath.LastIndexOf("."));
			
		string labelName = "";
		// 根据规则设置获取标签名称
		if (findWrapper.LabelRule == CollectionSetting.EBundleLabelRule.None)
		{
			// 注意：如果依赖资源来自于忽略文件夹，那么会触发这个异常
			throw new Exception($"CollectionSetting has depend asset in ignore folder : {findWrapper.FolderPath}, asset : {assetPath}");
			// Debug.Log(ELogLevel.Log, $"CollectionSetting has depend asset in ignore folder : {findWrapper.FolderPath}, asset : {assetPath}");
			// labelName = assetPath.Remove(assetPath.LastIndexOf("."));
		}
		else if (findWrapper.LabelRule == CollectionSetting.EBundleLabelRule.LabelByFileName)
		{
			labelName = Path.GetFileNameWithoutExtension(assetPath); // "C:\Demo\Assets\Config\test.txt" --> "test"
		}
		else if (findWrapper.LabelRule == CollectionSetting.EBundleLabelRule.LabelByFilePath)
		{
			labelName = assetPath.Remove(assetPath.LastIndexOf(".")); // "C:\Demo\Assets\Config\test.txt" --> "C:\Demo\Assets\Config\test"
		}
		else if (findWrapper.LabelRule == CollectionSetting.EBundleLabelRule.LabelByFolderName)
		{
			string temp = Path.GetDirectoryName(assetPath); // "C:\Demo\Assets\Config\test.txt" --> "C:\Demo\Assets\Config"
			labelName = Path.GetFileName(temp); // "C:\Demo\Assets\Config" --> "Config"
		}
		else if (findWrapper.LabelRule == CollectionSetting.EBundleLabelRule.LabelByFolderPath)
		{
			labelName = Path.GetDirectoryName(assetPath); // "C:\Demo\Assets\Config\test.txt" --> "C:\Demo\Assets\Config"
		}
		else if (findWrapper.LabelRule == CollectionSetting.EBundleLabelRule.LabelByFolderPathExceptBig)
		{
			long sizeMB = EditorTools.GetFileSize(assetPath) / 1024;
			if (sizeMB < bigSizeMB)
				labelName = Path.GetDirectoryName(assetPath); // "C:\Demo\Assets\Config\test.txt" --> "C:\Demo\Assets\Config"
			else
				labelName = assetPath.Remove(assetPath.LastIndexOf(".")); // "C:\Demo\Assets\Config\test.txt" --> "C:\Demo\Assets\Config\test"
		}
		else if(findWrapper.LabelRule == CollectionSetting.EBundleLabelRule.LabelByRootFolderPath)
        {
			labelName = findWrapper.FolderPath;
        }
		else
		{
			throw new NotImplementedException($"{findWrapper.LabelRule}");
		}

		ApplyReplaceRules(ref labelName);

		return labelName.Replace("\\", "/");
	}
		
	/// <summary>
	/// 获取资源的打包位置
	/// </summary>
	public static EBundlePos GetAssetBundlePos(string assetPath)
	{
		//List<string> buildInList = ConfigParser.GetBuildInResList();

		foreach(string path in Setting.InGames)
		{
			if (assetPath.Contains(path))
			{
				return EBundlePos.ingame;
			}
		}
		return EBundlePos.buildin;
	}

	public static void ApplyReplaceRules(ref string name)
    {
		if (name.Contains("TempLuaCode"))
		{
			name = name.Replace("TempLuaCode", "Lua");
		}
		if (name.Contains("Resource_min"))
		{
			name = name.Replace("Resource_min", "Resource");
		}
	}

	


	//private static void IgnorePath(string folderPath)
	//{
	//	var found = GetElement(folderPath);
	//	if (found == null)
	//	{
	//		AddElement(folderPath);
	//		ModifyElement(folderPath, CollectionSetting.EFolderPackRule.Ignore, CollectionSetting.EBundleLabelRule.LabelByFolderPath, EEncryptMethod.None, EAssetDeliveryMode.Main, EBundlePos.buildin);
	//	}
	//	else
//          {
	//		ModifyElement(folderPath, CollectionSetting.EFolderPackRule.Ignore, found.LabelRule, found.EncryptRule, found.DeliveryMode, found.BundlePos);
//          }
			
	//}

}
