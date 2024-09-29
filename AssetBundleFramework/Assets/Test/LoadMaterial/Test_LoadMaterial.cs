using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LoadMaterial : MonoBehaviour
{
    MaterialLoader ObjLoader;
    // Start is called before the first frame update
    void Start()
    {
        ResourceManager.Instance.Init(gameObject);
        string panelPath = "Prefabs/UIPanel/UI_Start.prefab";
        //loader要存下来，卸载时使用
        ObjLoader = LoadAssetUtility.LoadMaterialAsset(panelPath, LoadUIPrefabCallBack);
    }
    private void LoadUIPrefabCallBack(AssetLoaderBase loader, bool state)
    {
        GameObjectLoader objLoader = (GameObjectLoader)loader;
        GameObject obj = objLoader.GetGameObject();
    }


    private void Update()
    {
        float time = Time.deltaTime;
        ResourceManager.Instance.Tick(time);
        LoadTaskManager.Instance.Tick(time);
    }
}
