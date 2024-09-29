using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class StartLuaProjectState : StateBase
{
    private LuaFunction updateFunc;

    public override void OnEnter(object[] args)
    {
        StartLuaScript();
    }

    public void StartLuaScript()
    {
        LuaManager.Instance.LuaEnv.DoString("require 'RootScript'");
        LuaFunction luaRoot = LuaManager.Instance.LuaEnv.Global.Get<LuaFunction>("LuaStartUp");
        luaRoot.Call();
        updateFunc = LuaManager.Instance.LuaEnv.Global.Get<LuaFunction>("Update");
    }

    public override void OnExit()
    {
       
    }

    public override void Tick()
    {
        if (updateFunc != null)
            updateFunc.Call();
    }
}
