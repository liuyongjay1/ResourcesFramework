using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ResourceManager
{
    /// <summary>
    /// Lua����ͬ�����أ�������Editor����AB
    /// </summary>
    public Dictionary<string, AssetBundle> luaBundleCahe = new Dictionary<string, AssetBundle>();

    public byte[] LoadLua(string luaFilePath)
    {
        //�༭��������ģʽ��ѡ
#if UNITY_EDITOR
        if (!GameSetting.Instance.AssetbundleMode)//����ABģʽ��ֱ�Ӽ���
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

    //��ע�⣬��APIֻ���ڵ���
    public void ReleaseBundleCache()
    {
        luaBundleCahe.Clear();
    }
}
