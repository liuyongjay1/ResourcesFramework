

using pb = global::Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public partial class CSTable_UIConfig : TableBase
{
    internal override void Init()
    {
        Init<CSTable_UIConfig>("UIConfig.bytes");
    }
    protected override void Parser_Table(byte[] bytes)
    {
        var table = Table_UIConfig.Parser.ParseFrom(bytes);
        foreach (var item in table.Datas)
        {
            allRow.Add(item.Id, item);
        }
    }
}

public partial class TableManager
{
    private int InitAllTables()
    {
            RegisterTable<CSTable_UIConfig>();
        foreach (TableBase table in m_tableCache.Values)
            table.Init();
        return m_tableCache.Count;
    }
}

