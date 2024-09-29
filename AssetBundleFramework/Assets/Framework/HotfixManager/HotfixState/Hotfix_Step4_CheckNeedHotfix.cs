/*--------------------------------------------------
    检查是否需要热更
    1：StreamingVersion vs WebVersion，如果版本一致，热更流程结束，版本不一致，继续下一步
    2: 读取Persistant版本文件，(不存在则一定需要热更，PersistantVersion = StreamingVersion)(存在则加载，PersistantVersion与WebVersion对比，如果版本一致，热更流程结束，版本不一致，继续下一步)
    3: PersistantVersion传给下一流程，把文件清单里版本比PersistantVersion新的都加入下载列表
    4: 版本关系 StreamingVersion < PersistantVersion <= WebVersion
--------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Hotfix_Step4_CheckNeedHotfix : StateBase
{
    public override void OnEnter(object[] args)
    {
        int StreamingVersion = HotfixManager.Instance.GetStreamingVersion();
        int WebVersion = HotfixManager.Instance.GetWebVersion();
        //Persistant可能不存在，先给个初始值
        int PersistantVersion = StreamingVersion;
        //StreamingVersion<PersistantVersion<=WebVersion
        string PersistentPatchPath = PathTool.MakePersistentLoadPath(PatchDefine.PatchManifestFileName);
        //Persistent文件夹有权限用IO API
        if (File.Exists(PersistentPatchPath))
        {
            using (FileStream fs = new FileStream(PersistentPatchPath, FileMode.Open))
            {
                byte[] byteArray = new byte[fs.Length];
                fs.Read(byteArray, 0, byteArray.Length);
                PatchManifest persistantManifest = new PatchManifest();
                persistantManifest.Parse(byteArray);
                //此处只读取PersistantVersion的版本，后面还可能会更新PersistantVersion，放在最后设置
                PersistantVersion = persistantManifest.Version;
            }
        }
        LogManager.LogProcedure($"Hotfix_Step4_CheckNeedHotfix: StreamingVersion:{StreamingVersion} , WebVersion: {WebVersion} , PersistantVersion:{PersistantVersion}");
        //本地版本大于热更版本,证明覆盖安装了APP，清空
        if (StreamingVersion > PersistantVersion)
        {
            LogManager.LogProcedure($"Hotfix_Step4_CheckNeedHotfix:本地版本大于热更版本，清空Persistant文件夹");
        }

        //Web版本和StreamingVersion版本不一致，证明可能需要热更
        //Web版本和PersistantVersion版本不一致，一定需要热更
        if (PersistantVersion < WebVersion)
        {
            //先下载BundleRelation，再下载AB包
            StartUp.MonoStartCoroutine(DownloadBundleRelation(PersistantVersion));
        }
        else //服务器和StreamingVersion版本一致,跳过热更流程
        {
            LogManager.LogProcedure($"服务器和本地版本一致,跳过热更流程: Version: " + PersistantVersion);
            HotfixManager.Instance.EnterState(typeof(Hotfix_Finish), new object[] { HotfixFinishType.Web_Persistant_Match });
        }
    }

    IEnumerator DownloadBundleRelation(int PersistantVersion)
    {
        var bundleRelationName = PathTool.GetBundleRelationName();
        string downloadUrl = HotfixManager.Instance.GetWebDownloadURL(bundleRelationName);
        UnityWebRequest uwr = UnityWebRequest.Get(downloadUrl);
        uwr.timeout = 15;
        yield return uwr.SendWebRequest();
        if (uwr.isDone)
        {
            if (uwr.result != UnityWebRequest.Result.Success)
                LogManager.LogError(uwr.error);
            else
            {
                HotfixManager.Instance.WebBundleRelationDatas = uwr.downloadHandler.data;
                LogManager.LogProcedure("Web BundleRelation File Download Success");
                HotfixManager.Instance.EnterState(typeof(Hotfix_Step5_GetDownloadList), new object[] { EBundlePos.buildin, PersistantVersion });
            }
        }
    }

    public override void OnExit()
    {

    }
}
