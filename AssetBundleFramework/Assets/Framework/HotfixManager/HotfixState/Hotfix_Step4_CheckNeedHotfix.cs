/*--------------------------------------------------
    ����Ƿ���Ҫ�ȸ�
    1��StreamingVersion vs WebVersion������汾һ�£��ȸ����̽������汾��һ�£�������һ��
    2: ��ȡPersistant�汾�ļ���(��������һ����Ҫ�ȸ���PersistantVersion = StreamingVersion)(��������أ�PersistantVersion��WebVersion�Աȣ�����汾һ�£��ȸ����̽������汾��һ�£�������һ��)
    3: PersistantVersion������һ���̣����ļ��嵥��汾��PersistantVersion�µĶ����������б�
    4: �汾��ϵ StreamingVersion < PersistantVersion <= WebVersion
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
        //Persistant���ܲ����ڣ��ȸ�����ʼֵ
        int PersistantVersion = StreamingVersion;
        //StreamingVersion<PersistantVersion<=WebVersion
        string PersistentPatchPath = PathTool.MakePersistentLoadPath(PatchDefine.PatchManifestFileName);
        //Persistent�ļ�����Ȩ����IO API
        if (File.Exists(PersistentPatchPath))
        {
            using (FileStream fs = new FileStream(PersistentPatchPath, FileMode.Open))
            {
                byte[] byteArray = new byte[fs.Length];
                fs.Read(byteArray, 0, byteArray.Length);
                PatchManifest persistantManifest = new PatchManifest();
                persistantManifest.Parse(byteArray);
                //�˴�ֻ��ȡPersistantVersion�İ汾�����滹���ܻ����PersistantVersion�������������
                PersistantVersion = persistantManifest.Version;
            }
        }
        LogManager.LogProcedure($"Hotfix_Step4_CheckNeedHotfix: StreamingVersion:{StreamingVersion} , WebVersion: {WebVersion} , PersistantVersion:{PersistantVersion}");
        //���ذ汾�����ȸ��汾,֤�����ǰ�װ��APP�����
        if (StreamingVersion > PersistantVersion)
        {
            LogManager.LogProcedure($"Hotfix_Step4_CheckNeedHotfix:���ذ汾�����ȸ��汾�����Persistant�ļ���");
        }

        //Web�汾��StreamingVersion�汾��һ�£�֤��������Ҫ�ȸ�
        //Web�汾��PersistantVersion�汾��һ�£�һ����Ҫ�ȸ�
        if (PersistantVersion < WebVersion)
        {
            //������BundleRelation��������AB��
            StartUp.MonoStartCoroutine(DownloadBundleRelation(PersistantVersion));
        }
        else //��������StreamingVersion�汾һ��,�����ȸ�����
        {
            LogManager.LogProcedure($"�������ͱ��ذ汾һ��,�����ȸ�����: Version: " + PersistantVersion);
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
