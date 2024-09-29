using System;
using UnityEngine;

public class GameResTool
{
    public const string BundlePathFormat = "assets/works/res/{0}.unity3d";
    public const string EditorPathFormat = "Assets/Works/Res/{0}";


    //根据编辑器路径获取AB包加载路径
    public static string GetBundlePathByEditorPath(string editorPath)
    {
        int pointPos = editorPath.LastIndexOf(".");
        if (pointPos < 0)
        {
            LogManager.LogError("GetBundlePathByEditorPath Error, loader._resEditorPath not contains .");
            return "";
        }
        string pathWithoutExt = editorPath.Substring(0, pointPos);
        string bundlePath = string.Format(BundlePathFormat, pathWithoutExt.ToLower());
        return bundlePath;
    }

    //游戏所有的Bundle一共分为4个模块
    /*
     * 1:游戏业务的AB 
     * 2:UI的AB
     * 3:Lua的AB
     */
    public static void ResetGameRes()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        ResourceManager.Instance.ReleaseAllLoader();//
        ResourceManager.Instance.ReleaseBundleCache();//清空LuaBundle

        LoadTaskManager.Instance.ReleaseAllTask();

        UIManager.Instance.ReleaseAllUI();
    }

    //
    public static void GetAssetBundlePath()
    { 
        
    }
}


