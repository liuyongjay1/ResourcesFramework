/*
 * 遍历Web版本文件获取游戏内下载列表
 * 标记为ingame的ab包会加入列表
 * 1:检查Persistant是否已存在InGame下载完成文件，再检查是否是最新版本的下载完成
 * 2:如果版本不是最新，
 */
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Hotfix_Step3_GetInGameDownloadList : StateBase
{
    public override void OnEnter(object[] args)
    {
        bool downloadFinish = false;
        downloadFinish = HotfixManager.Instance.CheckInGameDownloadFinish();

        AddToInGameList();
        if (HotfixManager.Instance.InGameDownloadList.Count == 0)
        {
            HotfixManager.Instance.SaveInGameFinish();
            downloadFinish = true;
        }
        if (!downloadFinish)
        {
            //这一步计算下载总量只是给前端显示用的，等到真正下载的步骤还会再次计算下载总量。
            GetDownloadTotalSize();
        }
        
        HotfixManager.Instance.EnterState(typeof(Hotfix_Step4_CheckNeedHotfix));
    }

    private void AddToInGameList()
    {
        Dictionary<string, PatchElement> elements = HotfixManager.Instance.GetWebPatchFileList();
        string inGame = EBundlePos.ingame.ToString();
        foreach (var item in elements)
        {
            if (item.Value.Tag == inGame)
                HotfixManager.Instance.InGameDownloadList.Add(item.Value);
        }
    }

    private void GetDownloadTotalSize()
    {
        List<PatchElement> inGameDownloadList = HotfixManager.Instance.InGameDownloadList;
        long totalDownloadSize = 0;

        //检查有没有下载一半的
        foreach (var patchItem in inGameDownloadList)
        {
            string ABDownloadPath = PathTool.MakeABDownloadPath(patchItem.Name);
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
        HotfixManager.Instance.InGameDownloadSize = totalDownloadSize;
    }
    public override void OnExit()
    {

    }
}
