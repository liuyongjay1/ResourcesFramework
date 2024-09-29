/*--------------------------------------------------
     
--------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum HotfixFinishType 
{
    Web_Streaming_Match = 0,    //服务器版本和Streaming版本一致，无需热更
    Web_Persistant_Match = 1,   //服务器版本和Persistant版本一致，无需热更
    AllABDownloadFinsih = 2,    //启动热更完成
    InGameDownloadFinsih = 3,   //游戏内热更完成
}

public class Hotfix_Finish : StateBase
{
    public override void OnEnter(object[] args)
    {
        HotfixFinishType t = (HotfixFinishType)args[0];
        //游戏内下载结束
        if (t == HotfixFinishType.InGameDownloadFinsih)
        {
            HotfixManager.Instance.InGameDownloadList.Clear();
            HotfixManager.Instance.InGameDownloadSize = 0;
            HotfixManager.Instance.SaveInGameFinish();
            //通知游戏内下载结束

            //清空下载目录
            string DownloadFolder = PathTool.MakeABDownloadPath("");
            PathTool.ClearFolder(DownloadFolder);
            return;
        }
        bool persistantMode = false;
        if (t == HotfixFinishType.Web_Streaming_Match)
        {
            persistantMode = false;
        }
        else if (t == HotfixFinishType.Web_Persistant_Match)
        {
            persistantMode = true;
        }
        else if (t == HotfixFinishType.AllABDownloadFinsih)
        {
            persistantMode = true;

            string webPatchPersistantPath = PathTool.MakePersistentLoadPath(PatchDefine.PatchManifestFileName);
            using (FileStream fs = new FileStream(webPatchPersistantPath, FileMode.CreateNew))
            {
                fs.Write(HotfixManager.Instance.WebPatchDatas, 0, HotfixManager.Instance.WebPatchDatas.Length);
                fs.Close();
            }

            var bundleRelationName = PathTool.GetBundleRelationName();
            string bundleRelationPersistantPath = PathTool.MakePersistentLoadPath(bundleRelationName);
            using (FileStream fs = new FileStream(bundleRelationPersistantPath, FileMode.CreateNew))
            {
                fs.Write(HotfixManager.Instance.WebBundleRelationDatas, 0, HotfixManager.Instance.WebBundleRelationDatas.Length);
                fs.Close();
            }
            
            //清空下载目录
            string DownloadFolder = PathTool.MakeABDownloadPath("");
            PathTool.ClearFolder(DownloadFolder);
        }

        if (persistantMode)
        {
            HotfixManager.Instance.SetPersistantManifest(HotfixManager.Instance.WebPatchDatas);
            AssetBundleManager.Instance.SetPatchInfo(HotfixManager.Instance.PersistantManifest, HotfixManager.Instance.StreamingAssetManifest.Version);
        }
        else
        {
            AssetBundleManager.Instance.SetPatchInfo(HotfixManager.Instance.StreamingAssetManifest, HotfixManager.Instance.StreamingAssetManifest.Version);
        }
        LogManager.LogProcedure($"Hotfix_Finish persistantMode: {persistantMode}  ,HotfixFinishType: {t}");
        HotfixManager.Instance.WebPatchDatas = null;
        HotfixManager.Instance.WebBundleRelationDatas = null;

        HotfixManager.Instance.NeedDownloadList.Clear();

        AssetBundleManager.Instance.LoadBundleRelation(persistantMode);
        //继续框架流程
        FSMManager.Instance.EnterState(typeof(LoadTableState));
    }
}
