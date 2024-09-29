using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Hotfix_Step1_LoadStreamingVersion : StateBase
{

    public override void OnEnter(object[] args)
    {
        StartUp.MonoStartCoroutine(LoadManifest());
    }

    IEnumerator LoadManifest()
    {
        //�ȼ���StreamingAsset��manifest
        string patchPath = PathTool.GetLocalWWWLoadPath($"{Application.streamingAssetsPath}/{PatchDefine.PatchManifestFileName}");
        UnityWebRequest uwr = UnityWebRequest.Get(patchPath);
        LogManager.LogProcedure("����streamingAssetsPath �ļ��嵥:" + patchPath);
        uwr.timeout = 5;
        yield return uwr.SendWebRequest();
        if (uwr.isDone)
        {
            if (uwr.result != UnityWebRequest.Result.Success)
                LogManager.LogError("����streamingAssetsPath �ļ��嵥ʧ��: " + patchPath);
            else
            {
                HotfixManager.Instance.SetStreamingManifest(uwr.downloadHandler.data);
                HotfixManager.Instance.EnterState(typeof(Hotfix_Step2_DownloadWebManifest));
            }
        }
    }

    public override void OnExit()
    {

    }
}
