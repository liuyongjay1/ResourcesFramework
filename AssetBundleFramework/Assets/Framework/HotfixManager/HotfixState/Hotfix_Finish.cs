/*--------------------------------------------------
     
--------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum HotfixFinishType 
{
    Web_Streaming_Match = 0,    //�������汾��Streaming�汾һ�£������ȸ�
    Web_Persistant_Match = 1,   //�������汾��Persistant�汾һ�£������ȸ�
    AllABDownloadFinsih = 2,    //�����ȸ����
    InGameDownloadFinsih = 3,   //��Ϸ���ȸ����
}

public class Hotfix_Finish : StateBase
{
    public override void OnEnter(object[] args)
    {
        HotfixFinishType t = (HotfixFinishType)args[0];
        //��Ϸ�����ؽ���
        if (t == HotfixFinishType.InGameDownloadFinsih)
        {
            HotfixManager.Instance.InGameDownloadList.Clear();
            HotfixManager.Instance.InGameDownloadSize = 0;
            HotfixManager.Instance.SaveInGameFinish();
            //֪ͨ��Ϸ�����ؽ���

            //�������Ŀ¼
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
            
            //�������Ŀ¼
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
        //�����������
        FSMManager.Instance.EnterState(typeof(LoadTableState));
    }
}
