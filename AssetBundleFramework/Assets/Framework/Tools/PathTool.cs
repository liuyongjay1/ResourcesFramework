/*
 * 所有路径配置
 */
using System.IO;
using UnityEngine;

public static class PathTool
{
    //lua文件路径
    public const string LuaFilePath = "Works/Res/Lua";
    //bundle关系asset文件路径
    public const string BundleRelationFilePath = "Assets/BuildAssetBundle/BundleRelation";
    //资源包收集工具的配置文件存储路径
    public const string CollectorSettingFilePath = "Assets/BuildAssetBundle/Config.asset";

    public static string GetBundleRelationName()
    { 
        var bundleRelationName = $"{HashUtility.BytesMD5(System.Text.Encoding.UTF8.GetBytes(PathTool.BundleRelationFilePath))}.unity3d";
        return bundleRelationName;
    }
    public static string GetLocalWWWLoadPath(string path)
    {
        // 注意：WWW加载方式，必须要在路径前面加file://
#if UNITY_EDITOR
        return string.Format("file:///{0}", path);
#elif UNITY_IPHONE
			return string.Format("file://{0}", path);
#elif UNITY_ANDROID
			return path;
#elif UNITY_STANDALONE
			return string.Format("file:///{0}", path);
#endif
    }

    /// <summary>
    /// 获取规范化的路径
    /// </summary>
    public static string GetRegularPath(string path)
    {
        return path.Replace('\\', '/').Replace("\\", "/"); //替换为Linux路径格式
    }

    /// <summary>
    /// 获取基于流文件夹的加载路径
    /// </summary>
    public static string MakeStreamingLoadPath(string path)
    {
        return string.Format("{0}/{1}", Application.streamingAssetsPath, path);
    }

    /// <summary>
    /// 获取基于沙盒文件夹的加载路径
    /// </summary>
    public static string MakePersistentLoadPath(string path)
    {
#if UNITY_EDITOR
        // 注意：为了方便调试查看，编辑器下把存储目录放到项目里
        string projectPath = Path.GetDirectoryName(Application.dataPath).Replace("\\", "/");
        projectPath = GetRegularPath(projectPath);
        return string.Format("{0}/PersistentData/{1}", projectPath, path);
#else
		return string.Format("{0}/{1}", Application.persistentDataPath, path);
#endif
    }

    /// <summary>
    /// 获取基于沙盒文件夹的加载路径
    /// </summary>
    public static string MakeABDownloadPath(string path)
    {
#if UNITY_EDITOR
        // 注意：为了方便调试查看，编辑器下把存储目录放到项目里
        string projectPath = Path.GetDirectoryName(Application.dataPath).Replace("\\", "/");
        projectPath = GetRegularPath(projectPath);
        return string.Format("{0}/Download/{1}", projectPath, path);
#else
		return string.Format("{0}/Download/{1}", Application.persistentDataPath, path);
#endif
    }

    public static void CreateFileDirectory(string filePath)
    {
        string destDirectory = Path.GetDirectoryName(filePath);
        if (Directory.Exists(destDirectory) == false)
            Directory.CreateDirectory(destDirectory);
    }

    public static void ClearFolder(string folderPath)
    {
        string destDirectory = Path.GetDirectoryName(folderPath);
        if (Directory.Exists(destDirectory) == true)
            Directory.Delete(destDirectory,true);
    }
}
