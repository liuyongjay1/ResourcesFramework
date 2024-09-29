using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDownloadWebFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string abPath = PathTool.MakeABDownloadPath("");
        PathTool.CreateFileDirectory(abPath);
        PathTool.CreateFileDirectory(abPath + "Test/Test.txt");
        PathTool.CreateFileDirectory(abPath + "Test/BBB/Test.txt");

        //StartCoroutine(DownloadWithSingleTask());
    }
    IEnumerator DownloadWithSingleTask()
    {
        string abName = "assets/works/res/texture/bg/bg_2.unity3d";
        string url = "https://gitcode.net/liuyongjie1992/assetbundle_server/-/raw/master/" + abName;
        string savePath = PathTool.MakeABDownloadPath(abName);
        PathTool.CreateFileDirectory(savePath);
        // ´´½¨ÏÂÔØÆ÷
        using (var download = new WebFileRequest(url, savePath, DownloadProgress))
        {
            yield return download.DownLoad();
            //PatchHelper.Log(ELogLevel.Log, $"Web file is download : {savePath}");

            Debug.LogError("download result: " + download.States.ToString());
           
        }
    }

    private void DownloadProgress(long value1, float value2)
    {
        Debug.LogError($"DownloadProgress value1:{value1} ,value2: {value2}");

    }

    
}
