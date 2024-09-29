/*--------------------------------------------------
    * ��ȡ�����б�,������һ����֤���϶���Ҫ�ȸ�
    * 1�����ļ��嵥��汾��PersistantVersion�µĶ����������б�
    * 2: ��������б�����ļ��Ƿ��Ѿ���Persistant����Ŀ¼���Ѵ��ڣ�����Ѵ�����Ա�MD5�����MD5һ����������б����޳�
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
            //�Ȱ�buildin���ϵ�ȫ�����룬�ټ���޳�
            foreach (var item in elements)
            {
                //��ȫ�������б�Ȼ�������������޳�
                if (item.Value.Version > PersistantVersion)
                {
                    if (item.Value.Tag == buildin)
                        needDownloadList.Add(item.Value);
                }
            }

            //�ȼ���Ƿ����������Ѽ���
            for (int i = needDownloadList.Count - 1; i >= 0; i--)
            {
                PatchElement element = needDownloadList[i];
                string ABPersistantPath = PathTool.MakePersistentLoadPath(element.Name);
                if (File.Exists(ABPersistantPath))
                {
                    string PersistantMD5 = HashUtility.FileMD5(ABPersistantPath);
                    //������Ա�MD5
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
            LogManager.LogError("�ߵ�Hotfix_Step4��PersistantVersion��WebVersion��һ�£������б������϶�����Ϊ0��һ����BUG");
        }

        HotfixManager.Instance.EnterState(typeof(Hotfix_Step6_StartDownload), new object[] {args[0]});

        //CheckAlreadyFinishFile();
    }

    //����ɾ���������Ҳ���õ�AB��
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
