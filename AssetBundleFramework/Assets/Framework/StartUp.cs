using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    public bool AssetbundleMode;
    public static MonoBehaviour _Mono;
    void Awake()
    {
        _Mono = this;
        Debug.Log("streamingAssetsPath: " + Application.streamingAssetsPath);
        Debug.Log("persistentDataPath: " + Application.persistentDataPath);
        LoadGameSetting();
    }

    public static void MonoStartCoroutine(IEnumerator Ienumerator)
    {
        _Mono.StartCoroutine(Ienumerator);
    }


#if UNITY_EDITOR
    GUIStyle fontStyle = new GUIStyle();

    private void OnGUI()
    {
        fontStyle.normal.textColor = new Color(1, 0, 0);   //设置字体颜色
        fontStyle.fontSize = 40;       //字体大小
        
        if (AssetbundleMode)
            GUI.Label(new Rect(0, 0, 200, 200), "AssetBundle Mode", fontStyle);
        else
            GUI.Label(new Rect(0, 0, 200, 200), "Editor Mode", fontStyle);
    }
#endif

    public void LoadGameSetting()
    {
#if UNITY_EDITOR
        //编辑器模式
        if (!AssetbundleMode)
        {
            GameSetting.Instance = AssetDatabase.LoadAssetAtPath<GameSetting>("Assets/Works/Res/AllGameSetting/GameSetting.asset");
            GameSetting.Instance.AssetbundleMode = AssetbundleMode;
            InitAllManager();
            return;
        }
#endif
        string loadPath = Application.streamingAssetsPath + "/assets/works/res/allgamesetting/gamesetting.unity3d";
        AssetBundle bundle = AssetBundle.LoadFromFile(loadPath);
        GameSetting.Instance = bundle.LoadAsset<GameSetting>("gamesetting.asset");
        GameSetting.Instance.AssetbundleMode = AssetbundleMode;
        bundle.Unload(false);
        InitAllManager();
    }


    void InitAllManager()
    {
        HotfixManager.Instance.Init();

        FSMManager.Instance.Init();

        ResourceManager.Instance.Init(this.gameObject);

        LuaManager.Instance.Init();

        InputManager.Instance.Init();

        TouchMgr.Instance.InitExternal();

        AudioManager.Instance.Init(this.gameObject);

        AllManagerInitFinish();
    }

    void AllManagerInitFinish()
    {
        //编辑器模式,绕过热更，直接开始游戏
        if (!GameSetting.Instance.AssetbundleMode)
        {
            FSMManager.Instance.EnterState(typeof(LoadTableState));
        }
        else//AB模式，开启热更流程
        {
            HotfixManager.Instance.StartHotfixProcedure();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        ResourceManager.Instance.Tick(time);
        LoadTaskManager.Instance.Tick(time);

        InputManager.Instance.Tick(time);
        Input_Keyboard.Instance.Tick(time);
        FSMManager.Instance.Tick(time);

    }
}
