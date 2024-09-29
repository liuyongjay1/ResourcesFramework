
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using XLua;


public partial class TableManager : Singleton<TableManager>
{
    private Dictionary<System.Type, TableBase> m_tableCache = new Dictionary<System.Type, TableBase>();

    private List<string> m_parserEx = new List<string>();

    private void RegisterTable<T>() where T : TableBase, new()
    {
        m_tableCache.Add(typeof(T), new T());
    }

    public int Init()
    {
        InitParserEx();
        int tableCount = InitAllTables();
        return tableCount;
    }

    public T GetTable<T>() where T : TableBase
    {
        Type t =  typeof(T);
        TableBase table = null;
        if (m_tableCache.TryGetValue(t, out table))
            return table as T;
        else
            LogManager.LogError("GetRowById table is Null");
        return null;
    }


    private void InitParserEx()
    {
        m_parserEx.Add("CameraStatic");
    }

    private bool NeedParserEx(string tableName)
    {
        return m_parserEx.Contains(tableName);
    }
}

