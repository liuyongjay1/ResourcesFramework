using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XLua;

public partial class LuaManager : Singleton<LuaManager>
{
    private LuaEnv _luaEnv = new LuaEnv();
    public LuaEnv LuaEnv { get { return _luaEnv; } } 
    private LuaTable _gameTable;
    private Action _funStart;
    private Action _funUpdate;
    public void Init()
    {
        _luaEnv.AddLoader(CustomLoaderMethod);
        _luaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
        _luaEnv.AddBuildin("lpeg", XLua.LuaDLL.Lua.LoadLpeg);
        _luaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadPb);
    }

    public object ExecuteScript(byte[] scriptCode, string fileName, string chunkName = "code", LuaTable env = null)
    {
        byte[] codeByte = scriptCode;

        var results = _luaEnv.DoString(Encoding.UTF8.GetString(codeByte), chunkName, env);
        if (results == null) return null;
        if (results.Length == 1)
        {
            return results[0];
        }
        else
        {
            return results;
        }
    }
    
    private byte[] CustomLoaderMethod(ref string fileName)
    {
        string path = "";
        if (fileName.Contains("Framework"))
        {
            path = string.Format("Lua/{0}.lua", fileName);
        }
        else
        {
            path = string.Format("Lua/Code/{0}.lua", fileName);
        }
        byte[] data = ResourceManager.Instance.LoadLua(path);
        return data;
    }
}
