using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Hotfix_Step2_DownloadWebManifest : StateBase
{

    public override void OnEnter(object[] args)
    {
        PathTool.CreateFileDirectory(PathTool.MakeABDownloadPath(""));
        StartUp.MonoStartCoroutine(DownLoadWebManifest());
    }

    IEnumerator DownLoadWebManifest()
    {
        string downloadUrl = HotfixManager.Instance.GetWebDownloadURL(PatchDefine.PatchManifestFileName);
        UnityWebRequest uwr = UnityWebRequest.Get(downloadUrl);
        uwr.timeout = 15;
        yield return uwr.SendWebRequest();
        if (uwr.isDone)
        {
            if (uwr.result != UnityWebRequest.Result.Success)
                LogManager.LogError(uwr.error);
            else
            {
                HotfixManager.Instance.SetWebManifest(uwr.downloadHandler.data);
                int WebVersion = HotfixManager.Instance.GetWebVersion();
                int streamingVersion = HotfixManager.Instance.GetStreamingVersion();
                if (WebVersion == streamingVersion)
                {
                    HotfixManager.Instance.EnterState(typeof(Hotfix_Finish),new object[]{ HotfixFinishType.Web_Streaming_Match});
                    yield break;
                }
                else
                {
                    HotfixManager.Instance.WebPatchDatas = uwr.downloadHandler.data;
                }
                HotfixManager.Instance.EnterState(typeof(Hotfix_Step3_GetInGameDownloadList));
            }
        }
    }



    public override void OnExit()
    {

    }
}
