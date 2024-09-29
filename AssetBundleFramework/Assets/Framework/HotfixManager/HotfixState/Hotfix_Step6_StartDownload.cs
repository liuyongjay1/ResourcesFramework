/*--------------------------------------------------
     * ��ʽ��ʼ���أ���һ���Ѿ��޳��������������Ѳ�����ɵ�AB��
     * 1���ȼ���Ƿ���������һ����ļ���ͳ����������
     * 2: ȫ��������ɺ�һ��Ҫ�Ա�ÿһ�������ļ���MD5����Ϊͬһ��AB�����ܻ������һ���汾��AB��������һ�룬�����°汾�������ص����
     * 3: ����������MD5�ԱȲ�һ�µ��������MD5��������ļ��������б��޳������뵽RepairDownloadList
     * 4��MD5û������ļ�ȫ���ƶ���Ŀ��·��
--------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Hotfix_Step6_StartDownload : StateBase
{
    private long totalDownloadSize;//��byteΪ��λ
    private bool useMultiRequest;//�Ƿ�������������
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

    //����ǰ�ȼ�����Ҫ���ص�����
    private void CheckNeedDownloadSize(List<PatchElement> list)
    {
        totalDownloadSize = 0;
        currentDownloadSizeKB = 0;
        currentDownloadCount = 0;

        foreach (var patchItem in list)
        {
            string ABDownloadPath = PathTool.MakeABDownloadPath(patchItem.Name);
            //���һ����û������һ����ļ�
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

        // ��ʼ�����б����������Դ
        var startTime = Time.realtimeSinceStartup;
        LogManager.LogProcedure($"������������,��������: {needDownloadList.Count} ,���ش�С: {totalDownloadSize} ,��ǰʱ��: {startTime}");

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

    //ȫ��������ɺ󣬼��ÿһ�����ļ���MD5�Ƿ����б����MD5��ͬ
    //��Ϊͬһ��AB�����ܻ������һ���汾��AB��������һ�룬�����°汾�������ص����,MD5һ�������
    void AllFinishCheckMd5()
    {
        List<PatchElement> needDownloadList = HotfixManager.Instance.NeedDownloadList;
        for (int i = needDownloadList.Count - 1; i >= 0; i--)
        {
            PatchElement element = needDownloadList[i];
            string downloadSavePath = PathTool.MakeABDownloadPath(element.Name);
            string downloadFileMD5 = HashUtility.FileMD5(downloadSavePath);
            //������Ա�MD5
            if (downloadFileMD5 != element.MD5)
            {
                File.Delete(downloadSavePath);//����ļ����ϣ�ֱ��ɾ��
                LogManager.LogProcedure("Download assetbundle md5 error,Add to RepairDownloadList,path: " + downloadSavePath);
                RepairDownloadList.Add(element);
            }
            else//���سɹ���MD5�Աȳɹ����������سɹ��б�
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
            PathTool.CreateFileDirectory(movePath);//Ŀ���ļ��п��ܲ����ڣ�����
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
            // ����������
            using (var download = new WebFileRequest(url, savePath, OnDownloadProgress))
            {
                yield return download.DownLoad();
                //PatchHelper.Log(ELogLevel.Log, $"Web file is download : {savePath}");
                // ����Ƿ�����ʧ��
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
#region ����������

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
        LogManager.LogProcedure($"����AB������,��С:{downloadedBytes},�ٶ�: {downloadSpeed}");
        //PatchEventDispatcher.SendDownloadFilesProgressMsg(totalDownloadCount, currentDownloadCount,
    }

    public override void OnExit()
    {

    }
}
