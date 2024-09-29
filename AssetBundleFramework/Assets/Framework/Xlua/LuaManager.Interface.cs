using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public partial class LuaManager : Singleton<LuaManager>
{
    [CSharpCallLua]
    public event Action<string, LuaTable> LuaEventFunc;

    /// <summary>
    /// ��LUA�����¼�
    /// </summary>
    public void CSSendEventToLua(string eventId, LuaTable param)
    {
        LuaEventFunc(eventId, param);
    }
}
