/*
 * 热更模块
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class HotfixManager : Singleton<HotfixManager>
{
    //安装后版本文件
    public PatchManifest StreamingAssetManifest = new PatchManifest();
    //服务器版本文件
    public PatchManifest WebPatchManifest = new PatchManifest();
    //热更版本文件
    public PatchManifest PersistantManifest = new PatchManifest();

    //热更下载列表
    public List<PatchElement> NeedDownloadList = new List<PatchElement>();

    //游戏内下载列表
    public List<PatchElement> InGameDownloadList = new List<PatchElement>();
    //游戏内下载数据量
    public long InGameDownloadSize = 0;

    public byte[] WebPatchDatas;
    public byte[] WebBundleRelationDatas;

    #region 状态机
    public Dictionary<Type, StateBase> allState = new Dictionary<Type, StateBase>();
    private StateBase curState;

    public void Init()
    {
        RegisterState(typeof(Hotfix_Step1_LoadStreamingVersion));
        RegisterState(typeof(Hotfix_Step2_DownloadWebManifest));
        RegisterState(typeof(Hotfix_Step3_GetInGameDownloadList));
        RegisterState(typeof(Hotfix_Step4_CheckNeedHotfix));
        RegisterState(typeof(Hotfix_Step5_GetDownloadList));
        RegisterState(typeof(Hotfix_Step6_StartDownload));
        
        RegisterState(typeof(Hotfix_Finish));
    }

    //UI界面调用
    public void StartInGameDownload()
    {
        if (InGameDownloadSize == 0)
        {
            LogManager.LogError("InGameDownloadSize == 0,请检查大小");
            return;
        }
        HotfixManager.Instance.EnterState(typeof(Hotfix_Step5_GetDownloadList), new object[] { EBundlePos.ingame });
        
    }

    public void RegisterState(Type t)
    {
        object obj = Activator.CreateInstance(t);
        allState.Add(t, (StateBase)obj);
    }

    public void StartHotfixProcedure()
    {
        EnterState(typeof(Hotfix_Step1_LoadStreamingVersion));
    }

    public void EnterState(Type t, object[] args = null)
    {
        StateBase state = null;
        if (!allState.TryGetValue(t, out state))
        {
            LogManager.LogError(string.Format("FSMManager EnterState Error,state:{0} not exist", t.ToString()));
            return;
        }
        if (curState != null)
            curState.OnExit();
        if (curState != null)
            LogManager.LogProcedure(string.Format("FSM Exit:{0} ,Enter:{1}", curState, state.ToString()));
        else
            LogManager.LogProcedure(string.Format("FSM Enter:{0}", t.ToString()));
        curState = state;
        state.OnEnter(args);

    }

    public T GetState<T>() where T : StateBase
    {
        StateBase state = null;
        Type t = typeof(T);
        if (!allState.TryGetValue(t, out state))
        {
            LogManager.LogError(string.Format("FSMManager EnterState Error,state:{0} not exist", t.ToString()));
        }
        return state as T;
    }

    public void Tick(float timeScale)
    {
        if (curState != null)
            curState.Tick();
    }
    #endregion
    public int GetStreamingVersion()
    {
        return StreamingAssetManifest.Version;
    }

    public int GetWebVersion()
    {
        return WebPatchManifest.Version;
    }

    public int GetPersistentVersion()
    { 
        if(PersistantManifest.Version == 0)
            return StreamingAssetManifest.Version;
        return PersistantManifest.Version;
    }


    //加载StreamingAsset--AB包清单
    public void SetStreamingManifest(byte[] datas)
    {
        StreamingAssetManifest.Parse(datas);
    }

    public void SetPersistantManifest(byte[] datas)
    {
        PersistantManifest.Parse(datas);
    }

    public void SetWebManifest(byte[] datas)
    {
        WebPatchManifest.Parse(datas);
    }

    public Dictionary<string, PatchElement> GetWebPatchFileList()
    {
        return WebPatchManifest.Elements;
    }

    public void DownloadInGameAB()
    { 
        
    }

    public string GetWebDownloadURL(string fileName)
    {
        string path = $"https://gitcode.net/liuyongjie1992/assetbundle_server/-/raw/master/" + fileName;
        return path;
    }
    public void ReleaseAll()
    {
        StreamingAssetManifest = null;
        WebPatchManifest = null;
        PersistantManifest = null;
        NeedDownloadList = null;
    }

    public bool CheckInGameDownloadFinish()
    {
        //游戏内热更完成，会保存一个文件标志
        string inGameFile = PathTool.MakePersistentLoadPath("InGame");
        if (File.Exists(inGameFile))
        {
            using (FileStream fs = new FileStream(inGameFile, FileMode.Open))
            {
                using (var bw = new BinaryReader(fs))
                {
                    int saveVersion = bw.ReadInt32();
                    int webVersion = GetWebVersion();
                    if (saveVersion == webVersion)
                    {
                        HotfixManager.Instance.InGameDownloadSize = 0;
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public void SaveInGameFinish()
    {
        //游戏内热更完成，会保存一个文件标志
        string inGameFile = PathTool.MakePersistentLoadPath("InGame");
        using (FileStream fs = new FileStream(inGameFile, FileMode.CreateNew))
        {
            using (var bw = new BinaryWriter(fs))
            {
                bw.Write(HotfixManager.Instance.GetWebVersion());
            }
        }
    }
}
