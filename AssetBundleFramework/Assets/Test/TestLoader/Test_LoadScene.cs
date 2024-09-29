using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test_LoadScene : MonoBehaviour
{
    public bool AssetbundleMode;

    public SceneLoader _sceneLoader;

    void Awake()
    {
        //AssetBundleManager.InitMonoSingleton(transform);
        LoadGameSetting();
    }
    public void LoadGameSetting()
    {
#if UNITY_EDITOR
        //±à¼­Æ÷Ä£Ê½
        if (!AssetbundleMode)
        {
            GameSetting.Instance = AssetDatabase.LoadAssetAtPath<GameSetting>("Assets/Works/Res/AllGameSetting/GameSetting.asset");
            GameSetting.Instance.AssetbundleMode = AssetbundleMode;
            StartLoadSceneAsync();
            return;
        }
#endif
        string loadPath = Application.streamingAssetsPath + "/assets/Works/Res/allgamesetting/gamesetting.unity3d";
        AssetBundle bundle = AssetBundle.LoadFromFile(loadPath);
        GameSetting.Instance = bundle.LoadAsset<GameSetting>("assets/Works/Res/allgamesetting/gamesetting.asset");
        GameSetting.Instance.AssetbundleMode = AssetbundleMode;
        bundle.Unload(false);
        StartLoadSceneAsync();
    }

    void StartLoadSceneAsync()
    {
        _sceneLoader = LoadAssetUtility.LoadSceneAsset("Scenes/TestScene.unity", LoadSceneCallBack, false);
    }

    private void LoadSceneCallBack(AssetLoaderBase loader, bool state)
    {
        Debug.LogError("LoadSceneCallBack!!   result: " + state);
    }

    // Update is called once per frame
    private void Update()
    {
        float time = Time.deltaTime;
        ResourceManager.Instance.Tick(time);
        LoadTaskManager.Instance.Tick(time);
    }
}
