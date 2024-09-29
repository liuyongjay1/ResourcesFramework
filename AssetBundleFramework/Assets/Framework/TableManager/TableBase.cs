using pb = global::Google.Protobuf;
using System.Collections.Generic;
using System;
using UnityEngine;
public abstract class TableBase
{
    protected Dictionary<int, pb::IMessage> allRow = new Dictionary<int, pb.IMessage>();
    protected Dictionary<string, int> keyidRelation = new Dictionary<string, int>();
    #region 对内接口
    internal abstract void Init();
    protected abstract void Parser_Table(byte[] data);
    protected virtual void Parser_TableEx(byte[] data) { }

    #endregion
    #region 内部方法
    protected void Init<T>(string byteFileName) where T : TableBase
    {
        LoadAssetUtility.LoadTextAsset("CS_Bytes/" + byteFileName, LoadBytesCallback, true);
    }

    void LoadBytesCallback(AssetLoaderBase loader, bool state)
    {
        LogManager.LogProcedure("LoadBytesCallback path: " + loader.GetResEditorPath());
        if (state == true)
        {
            TextFileLoader textLoader = (TextFileLoader)loader;
            if (textLoader == null)
            {
                LogManager.LogError("TableCtrl textLoader is null: " + loader.GetResEditorPath());
                return;
            }
            Parser_Table(textLoader.GetBytes());
            LoadTableState loadTable = FSMManager.Instance.GetState<LoadTableState>();
            loadTable.LoadTableCallback();
        }
        else
            LogManager.LogError("TableCtrl load table fail,_resEditorPath: " + loader.GetResEditorPath());
    }


    public T GetRowById<T>(int id) where T : class, pb::IMessage
    {
        pb::IMessage ret = null;
        allRow.TryGetValue(id, out ret);
        if (ret != null)
           return ret as T;
        LogManager.LogError("GetRowById is null,id: " + id);
        return null;
    }

    
    public T GetRowDataByKey<T>(int id) where T : class, pb::IMessage
    {
        //pb::IMessage ret = null;
        //m_cache.TryGetValue(id, out ret);
        //if (ret != null)
        //    return ret as T;
        return null;
    }

    public T[] GetAllRowData<T>() where T : class, pb::IMessage
    {
        T[] array = new T[allRow.Count];
        int i = 0;
        foreach (T info in allRow.Values)
        {
            array[i] = info;
            i++;
        }
        return array;
    }
    #endregion  
}
