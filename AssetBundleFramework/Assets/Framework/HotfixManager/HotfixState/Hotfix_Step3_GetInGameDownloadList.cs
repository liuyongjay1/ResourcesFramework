/*
 * ����Web�汾�ļ���ȡ��Ϸ�������б�
 * ���Ϊingame��ab��������б�
 * 1:���Persistant�Ƿ��Ѵ���InGame��������ļ����ټ���Ƿ������°汾���������
 * 2:����汾�������£�
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
            //��һ��������������ֻ�Ǹ�ǰ����ʾ�õģ��ȵ��������صĲ��軹���ٴμ�������������
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

        //�����û������һ���
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
