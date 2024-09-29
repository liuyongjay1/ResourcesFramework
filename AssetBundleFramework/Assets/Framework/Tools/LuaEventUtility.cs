using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LuaEventUtility:Singleton<LuaEventUtility>
{
    [CSharpCallLua]
    public delegate void NoTableDelegate(int param1);

    [CSharpCallLua]
    public delegate void TableDelegate(LuaTable tab, int param1);

    [LuaCallCSharp]
    public event Action<int> ActionTest3;

    [LuaCallCSharp]
    public event Action<LuaTable,int, int> ActionTest4;

    public static void DoTest1(LuaTable tab, string functionName)
    {
        NoTableDelegate callback = tab.Get<NoTableDelegate>(functionName);
        callback(1111);
    }
    public static void DoTest2(LuaTable tab, string functionName)
    {
        TableDelegate callback = tab.Get<TableDelegate>(functionName);
        callback(tab,2222);
    }
    public void DoTest3()
    {
        ActionTest3.Invoke(3333);
    }

    public void DoTest4(LuaTable tab)
    {
        ActionTest4.Invoke(tab, 4444, 5555);
    }

    public void Update()
    { 
         //ActionTest3.Invoke(3333);
    }
}
