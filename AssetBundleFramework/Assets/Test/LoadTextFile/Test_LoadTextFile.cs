#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test_LoadTextFile : MonoBehaviour
{
    TextFileLoader ObjLoader;
    // Start is called before the first frame update
    void Start()
    {
        ResourceManager.Instance.Init(gameObject);
        string path = "Assets/Works/Res/Lua_Bytes/Chapter1.bytes";
        //string path = "Assets/Works/Res/Lua_Bytes/Chapter1Dialog.bytes";

        UnityEngine.Object _target = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
        TextAsset text = (TextAsset)_target;
        //string panelPath = "Lua_Bytes/Chapter1.bytes";
        ////loaderҪ��������ж��ʱʹ��
        //ObjLoader = LoadAssetUtility.LoadTextAsset(panelPath, LoadBytesCallBack);
    }
    private void LoadBytesCallBack(AssetLoaderBase loader, bool state)
    {
        TextFileLoader objLoader = (TextFileLoader)loader;
        Debug.LogError("LoadBytesCallBack :" + ObjLoader.GetBytes());
    }


    private void Update()
    {
        float time = Time.deltaTime;
        ResourceManager.Instance.Tick(time);
        LoadTaskManager.Instance.Tick(time);
    }
}
#endif