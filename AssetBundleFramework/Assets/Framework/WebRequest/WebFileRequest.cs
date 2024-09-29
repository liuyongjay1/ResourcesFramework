//--------------------------------------------------

//--------------------------------------------------
using MotionFramework.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public class WebFileRequest : WebRequestBase, IDisposable
{
	/// <summary>
	/// 文件存储路径
	/// </summary>
	public string SavePath { private set; get; }
	protected Action<long, float> onProgress;
	public WebFileRequest(string url, string savePath, Action<long, float> onProgressCallback=null) : base(url)
	{
		SavePath = savePath;
		onProgress = onProgressCallback;
	}
	public override IEnumerator DownLoad()
	{
		// Check fatal
		if (States != EWebRequestStates.None)
			throw new Exception($"{nameof(WebFileRequest)} is downloading yet : {URL}");

		States = EWebRequestStates.Loading;

        //获得文件下载长度
        var headRequest = UnityWebRequest.Head(URL);
        yield return headRequest.SendWebRequest();
        var totalLength = long.Parse(headRequest.GetResponseHeader("Content-Length"));
            
        // 下载文件
        using(var CacheRequest = UnityWebRequest.Get(URL))
        {
            using (var fs = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var fileLen = fs.Length;
                if (fileLen >= totalLength)
                {
                    LogManager.LogInfo($"{SavePath} download finished {fileLen}/{totalLength}");
                    States = EWebRequestStates.Success;
                    yield break;
                }
                CacheRequest.timeout = Timeout;
                CacheRequest.downloadHandler = new DownloadHandlerFileStream(fs, 40960, fileLen, onProgress);
                CacheRequest.disposeDownloadHandlerOnDispose = true;
                if (fileLen > 0)
                {
                    LogManager.LogInfo( $"resume download {URL.Substring(URL.LastIndexOf("/"))} from: {fileLen / 1024f}KB");
                    CacheRequest.SetRequestHeader("Range", $"bytes={fileLen}-");
                    fs.Seek(fileLen, SeekOrigin.Begin);
                }
                yield return CacheRequest.SendWebRequest();
                fs.Close();
                // Check error
                if (CacheRequest.isNetworkError || CacheRequest.isHttpError)
                {
                    LogManager.LogWarning($"Failed to download web file : {URL} Error : {CacheRequest.error}");
                    States = EWebRequestStates.Fail;
                }
                else
                {
                    States = EWebRequestStates.Success;
                }
            }
        }
    }
}
