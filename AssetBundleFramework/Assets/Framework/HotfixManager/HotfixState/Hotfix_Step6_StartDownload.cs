/*--------------------------------------------------
     * 正式开始下载，上一步已经剔除了已下载完且已部署完成的AB包
     * 1：先检查是否有下载了一半的文件，统计下载总量
     * 2: 全部下载完成后，一定要对比每一个下载文件的MD5，因为同一个AB包可能会出现上一个版本的AB包下载了一半，到了新版本继续下载的情况
     * 3: 如果真出现了MD5对比不一致的情况，把MD5出问题的文件从下载列表剔除，加入到RepairDownloadList
     * 4：MD5没问题的文件全部移动到目标路径
--------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Hotfix_Step6_StartDownload : StateBase
{
    private long totalDownloadSize;//以byte为单位
    private bool useMultiRequest;//是否开启多下载任务
    private bool failedOnDownload;

    private long currentDownloadSizeKB ;
    private int currentDownloadCount;

    public List<PatchElement> RepairDownloadList = new List<PatchElement>();
    public List<PatchElement> DownloadSuccessList = new List<PatchElement>();

    EBundlePos BundlePosType;

    public override void OnEnter(object[] args)
    {
        LogManager.LogProcedure("Hotfix_Step6_StartDownload");
        BundlePosType = (EBundlePos)args[0];
        StartDownload();
    }


    private void StartDownload()
    {
        StartUp.MonoStartCoroutine(Download());

    }

    //下载前先计算需要下载的总量
    private void CheckNeedDownloadSize(List<PatchElement> list)
    {
        totalDownloadSize = 0;
        currentDownloadSizeKB = 0;
        currentDownloadCount = 0;

        foreach (var patchItem in list)
        {
            string ABDownloadPath = PathTool.MakeABDownloadPath(patchItem.Name);
            //检查一下有没有下载一半的文件
            if (File.Exists(ABDownloadPath))
            {
                using (FileStream fs = new FileStream(ABDownloadPath, FileMode.Open))
                {
                    totalDownloadSize += (patchItem.FileSize - fs.Length);
                }
            }
            else
            {
                totalDownloadSize += patchItem.FileSize;
            }
        }
    }

    private IEnumerator Download()
    {
        List<PatchElement> needDownloadList = HotfixManager.Instance.NeedDownloadList;

        CheckNeedDownloadSize(needDownloadList);

        // 开始下载列表里的所有资源
        var startTime = Time.realtimeSinceStartup;
        LogManager.LogProcedure($"开启下载任务,下载数量: {needDownloadList.Count} ,下载大小: {totalDownloadSize} ,当前时间: {startTime}");

        if (useMultiRequest)
        {
            yield return DownloadWithMultiTask(needDownloadList);
        }
        else
        {
            yield return DownloadWithSingleTask(needDownloadList);
        }
        if (failedOnDownload)
        {
            yield break;
        }

        AllFinishCheckMd5();
    }

    //全部下载完成后，检查每一个新文件的MD5是否与列表里的MD5相同
    //因为同一个AB包可能会出现上一个版本的AB包下载了一半，到了新版本继续下载的情况,MD5一定会出错
    void AllFinishCheckMd5()
    {
        List<PatchElement> needDownloadList = HotfixManager.Instance.NeedDownloadList;
        for (int i = needDownloadList.Count - 1; i >= 0; i--)
        {
            PatchElement element = needDownloadList[i];
            string downloadSavePath = PathTool.MakeABDownloadPath(element.Name);
            string downloadFileMD5 = HashUtility.FileMD5(downloadSavePath);
            //下载完对比MD5
            if (downloadFileMD5 != element.MD5)
            {
                File.Delete(downloadSavePath);//这个文件作废，直接删掉
                LogManager.LogProcedure("Download assetbundle md5 error,Add to RepairDownloadList,path: " + downloadSavePath);
                RepairDownloadList.Add(element);
            }
            else//下载成功且MD5对比成功，加入下载成功列表
            {
                DownloadSuccessList.Add(element);
            }
        }

        if (RepairDownloadList.Count > 0)
        {
            HotfixManager.Instance.NeedDownloadList.Clear();
            HotfixManager.Instance.NeedDownloadList = RepairDownloadList;
            StartDownload();
        }
        else
            MoveDownloadFiles();
    }

    void MoveDownloadFiles()
    {
        foreach (var element in DownloadSuccessList)
        {
            string downloadSavePath = PathTool.MakeABDownloadPath(element.Name);
            string movePath = PathTool.MakePersistentLoadPath(element.Name);
            PathTool.CreateFileDirectory(movePath);//目标文件夹可能不存在，创建
            File.Copy(downloadSavePath, movePath,true);
        }
        LogManager.LogProcedure("Download assetbundle Deploy Success!!!! ");
        DownloadSuccessList.Clear();
        RepairDownloadList.Clear();
        if (BundlePosType == EBundlePos.buildin)
            HotfixManager.Instance.EnterState(typeof(Hotfix_Finish), new object[] { HotfixFinishType.AllABDownloadFinsih });
        else
            HotfixManager.Instance.EnterState(typeof(Hotfix_Finish), new object[] { HotfixFinishType.InGameDownloadFinsih });


    }

    IEnumerator DownloadWithSingleTask(List<PatchElement> newDownloaded)
    {
        foreach (var element in newDownloaded)
        {
            string url = HotfixManager.Instance.GetWebDownloadURL(element.Name);
            string savePath = PathTool.MakeABDownloadPath(element.Name);
            PathTool.CreateFileDirectory(savePath);
            // 创建下载器
            using (var download = new WebFileRequest(url, savePath, OnDownloadProgress))
            {
                yield return download.DownLoad();
                //PatchHelper.Log(ELogLevel.Log, $"Web file is download : {savePath}");
                // 检测是否下载失败
                if (download.States != EWebRequestStates.Success)
                {
                    //PatchEventDispatcher.SendWebFileDownloadFailedMsg(url, element.Name);
                    failedOnDownload = true;
                    yield break;
                }
            }
            currentDownloadSizeKB += element.FileSize;
            currentDownloadCount++;
        }
    }
#region 多任务下载

    private class DownloadJob
    {
        public UnityWebRequest req;
        public PatchElement info;
    }

    IEnumerator DownloadWithMultiTask(List<PatchElement> downloadList)
    {
        var maxJobs = 5;
        var runningJobs = new List<DownloadJob>(maxJobs);
        var lastRecordTime = Time.realtimeSinceStartup;
        int assignedIdx = 0;
        while (assignedIdx < downloadList.Count)
        {
            while (runningJobs.Count < maxJobs && assignedIdx < downloadList.Count)
            {
                var element = downloadList[assignedIdx];
                var url = HotfixManager.Instance.GetWebDownloadURL(element.Name);
                string savePath = PathTool.MakeABDownloadPath(element.Name);
                PathTool.CreateFileDirectory(savePath);

                var job = UnityWebRequest.Get(url);
                job.downloadHandler = new DownloadHandlerFile(savePath) { removeFileOnAbort = true };
                job.SendWebRequest();
                runningJobs.Add(new DownloadJob()
                {
                    req = job,
                    info = element
                });
                assignedIdx++;
            }
            yield return new WaitUntil(() => CheckAnyDownloadFinished(runningJobs, ref lastRecordTime));
            if (failedOnDownload) yield break;
        }
        yield return new WaitUntil(() => CheckAllDownloadFinished(runningJobs, ref lastRecordTime));

        //clear up all request jobs			
        foreach (var job in runningJobs)
        {
            LogIfFailed(job);
            job.req.Dispose();
        }
        runningJobs.Clear();
    }

    bool CheckAnyDownloadFinished(List<DownloadJob> jobs, ref float lastRecordTime)
    {

        long downloadedBytes = 0;
        int finished = 0;
        for (var i = jobs.Count - 1; i >= 0; i--)
        {
            var job = jobs[i];
            if (job.req.isDone)
            {
                finished++;
                LogIfFailed(job);
                jobs.RemoveAt(i);
                downloadedBytes += job.info.FileSize;
                job.req.Dispose();
            }
        }
        if (finished > 0)
        {
            var deltaTime = Time.realtimeSinceStartup - lastRecordTime;
            currentDownloadCount += finished;
            currentDownloadSizeKB += downloadedBytes;
            OnDownloadProgress(downloadedBytes, downloadedBytes / deltaTime);
            lastRecordTime += deltaTime;
        }
        return finished > 0;
    }

    bool CheckAllDownloadFinished(List<DownloadJob> jobs, ref float lastRecordTime)
    {
        long downloadedBytes = 0;
        foreach (var job in jobs)
        {
            if (!job.req.isDone) return false;
            downloadedBytes += job.info.FileSize;
        }
        var deltaTime = Time.realtimeSinceStartup - lastRecordTime;
        if (deltaTime > 0)
        {
            currentDownloadCount += jobs.Count;
            currentDownloadSizeKB += downloadedBytes;
            OnDownloadProgress(downloadedBytes, downloadedBytes / deltaTime);
            lastRecordTime += deltaTime;
        }
        return true;
    }

    void LogIfFailed(DownloadJob job)
    {
        if (job.req.isHttpError || job.req.isNetworkError)
        {
            //PatchEventDispatcher.SendWebFileDownloadFailedMsg(job.req.url, job.info.Name);
            failedOnDownload = true;
            LogManager.LogWarning($"Failed to download file: {job.info.Name}.");
        }
    }
    #endregion

    void OnDownloadProgress(long downloadedBytes, float downloadSpeed)
    {
        LogManager.LogProcedure($"下载AB包数据,大小:{downloadedBytes},速度: {downloadSpeed}");
        //PatchEventDispatcher.SendDownloadFilesProgressMsg(totalDownloadCount, currentDownloadCount,
    }

    public override void OnExit()
    {

    }
}
