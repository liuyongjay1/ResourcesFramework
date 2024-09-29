/*--------------------------------------------------
    * 获取下载列表,到了这一步，证明肯定需要热更
    * 1：把文件清单里版本比PersistantVersion新的都加入下载列表
    * 2: 检查下载列表里的文件是否已经在Persistant加载目录里已存在，如果已存在则对比MD5，如果MD5一致则从下载列表里剔除
--------------------------------------------------*/
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Hotfix_Step5_GetDownloadList : StateBase
{
    public override void OnEnter(object[] args)
    {
        EBundlePos BundlePosType = (EBundlePos)args[0];
        LogManager.LogProcedure("Hotfix_Step5_GetDownloadList BundlePosType: " + BundlePosType);
        if (BundlePosType == EBundlePos.buildin)
        {
            int PersistantVersion = (int)args[1];
            Dictionary<string, PatchElement> elements = HotfixManager.Instance.GetWebPatchFileList();
            List<PatchElement> needDownloadList = HotfixManager.Instance.NeedDownloadList;
            string buildin = EBundlePos.buildin.ToString();
            //先把buildin符合的全部加入，再检查剔除
            foreach (var item in elements)
            {
                //先全部加入列表，然后把已下载完的剔除
                if (item.Value.Version > PersistantVersion)
                {
                    if (item.Value.Tag == buildin)
                        needDownloadList.Add(item.Value);
                }
            }

            //先检查是否下载完且已剪切
            for (int i = needDownloadList.Count - 1; i >= 0; i--)
            {
                PatchElement element = needDownloadList[i];
                string ABPersistantPath = PathTool.MakePersistentLoadPath(element.Name);
                if (File.Exists(ABPersistantPath))
                {
                    string PersistantMD5 = HashUtility.FileMD5(ABPersistantPath);
                    //下载完对比MD5
                    if (PersistantMD5 == element.MD5)
                    {
                        needDownloadList.RemoveAt(i);
                    }
                }
            }

        }
        else if (BundlePosType == EBundlePos.ingame)
        {
            HotfixManager.Instance.NeedDownloadList = HotfixManager.Instance.InGameDownloadList;
        }


        if (HotfixManager.Instance.NeedDownloadList.Count == 0)
        {
            LogManager.LogError("走到Hotfix_Step4，PersistantVersion和WebVersion不一致，下载列表数量肯定不能为0，一定有BUG");
        }

        HotfixManager.Instance.EnterState(typeof(Hotfix_Step6_StartDownload), new object[] {args[0]});

        //CheckAlreadyFinishFile();
    }

    //倒序删除已下载且部署好的AB包
    /*
    private void CheckAlreadyFinishFile()
    {
        List<PatchElement> needDownloadList = HotfixManager.Instance.NeedDownloadList;
        List<PatchElement> inGameDownloadList = HotfixManager.Instance.InGameDownloadList;
        for (int i = needDownloadList.Count - 1; i >=0; i--)
        {
            string persistantFilePath = PathTool.MakePersistentLoadPath(needDownloadList[i].Name);
            if (File.Exists(persistantFilePath))
            {
                string persistantFileMD5 = HashUtility.FileMD5(persistantFilePath);
                if (persistantFileMD5 == needDownloadList[i].MD5)
                {
                    needDownloadList.RemoveAt(i);
                }
            }
        }

        for (int i = inGameDownloadList.Count - 1; i >= 0; i--)
        {
            string persistantFilePath = PathTool.MakePersistentLoadPath(needDownloadList[i].Name);
            if (File.Exists(persistantFilePath))
            {
                string persistantFileMD5 = HashUtility.FileMD5(persistantFilePath);
                if (persistantFileMD5 == needDownloadList[i].MD5)
                {
                    needDownloadList.RemoveAt(i);
                }
            }
        }

        if (needDownloadList.Count == 0)
            HotfixManager.Instance.EnterState(typeof(Hotfix_Finish));
        else
            HotfixManager.Instance.EnterState(typeof(Hotfix_Step6_StartDownload));
    }
    */

    public override void OnExit()
    {

    }
}
