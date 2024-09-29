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
        //先加载StreamingAsset的manifest
        string patchPath = PathTool.GetLocalWWWLoadPath($"{Application.streamingAssetsPath}/{PatchDefine.PatchManifestFileName}");
        UnityWebRequest uwr = UnityWebRequest.Get(patchPath);
        LogManager.LogProcedure("加载streamingAssetsPath 文件清单:" + patchPath);
        uwr.timeout = 5;
        yield return uwr.SendWebRequest();
        if (uwr.isDone)
        {
            if (uwr.result != UnityWebRequest.Result.Success)
                LogManager.LogError("加载streamingAssetsPath 文件清单失败: " + patchPath);
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
