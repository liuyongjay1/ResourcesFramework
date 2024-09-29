using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XLua;
using LuaAPI = XLua.LuaDLL.Lua;
public partial class LuaManager : Singleton<LuaManager>
{
    Dictionary<string, LuaTable> MetaList = new Dictionary<string, LuaTable>();

    /// <param name="scriptName"></param>
    /// <param name="isNewTable">class类需要多个对象的，传个true</param>
    /// <param name="baseClass">设置元表当class的基类</param>
    /// <returns></returns>
    public LuaTable GetLuaTable(string scriptName, System.Type baseClass = null)
    {
        LuaTable meta;
        if (!MetaList.TryGetValue(scriptName, out meta))
        {
            string[] tmpList = scriptName.Split('/');
            string tabName = tmpList[tmpList.Length - 1];
            //require
            string cmd = "require '" + scriptName + "'";
            object[] tRes = LuaEnv.DoString(cmd);
            meta = LuaEnv.Global.Get<LuaTable>(tabName);
            if (meta == null)
            {
                Debug.LogError("Load MetaTable Fail, Table Name:" + scriptName);
                return null;
            }
            if (baseClass != null)
            {
                LuaTable mt = GetXLuaMetaTable(baseClass);
                if (mt != null)
                {
                    meta.SetMetaTable(mt);
                }
            }
            MetaList.Add(scriptName, meta);
        }
        return meta;
    }
    public LuaTable GetXLuaMetaTable(System.Type t)
    {
        LuaAPI.luaL_getmetatable(LuaEnv.L, t.FullName);
        if (LuaAPI.lua_isnil(LuaEnv.L, -1))
        {
            return null;
        }
        LuaTable res;
        LuaEnv.translator.Get(LuaEnv.L, -1, out res);
        return res;
    }

}
