using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ResourceManager
{
    /// <summary>
    /// Lua必须同步加载，无论是Editor还是AB
    /// </summary>
    public Dictionary<string, AssetBundle> luaBundleCahe = new Dictionary<string, AssetBundle>();

    public byte[] LoadLua(string luaFilePath)
    {
        //编辑器有两种模式可选
#if UNITY_EDITOR
        if (!GameSetting.Instance.AssetbundleMode)//不用AB模式，直接加载
        {
            string fullPath = "Assets/Works/Res/" + luaFilePath;
            var result = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(fullPath);
            if (result == null)
            {
                LogManager.LogError($"Failed to load {fullPath}");
                return null;
            }
            return result.bytes;
        }
#endif
        AssetBundle bundle = null;
        if (luaBundleCahe.ContainsKey(luaFilePath))
        {
            bundle = luaBundleCahe[luaFilePath];
            if (bundle == null)
                LogManager.LogError("luaBundleCahe bundle is null,path: " + luaFilePath);
        }
        else
        {
            string luaBundlePath = GameResTool.GetBundlePathByEditorPath(luaFilePath);
            bundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + luaBundlePath);
            if (bundle == null)
                LogManager.LogError("LoadBundleFail,path: " + Application.streamingAssetsPath + "/" + luaBundlePath);
            luaBundleCahe.Add(luaFilePath, bundle);
        }
        string fileName = System.IO.Path.GetFileName(luaFilePath);
        TextAsset asset = bundle.LoadAsset<TextAsset>(fileName);
        byte[] bytes = asset.bytes;
        Resources.UnloadAsset(asset);
        return bytes;
    }

    //请注意，改API只有在调用
    public void ReleaseBundleCache()
    {
        luaBundleCahe.Clear();
    }
}
