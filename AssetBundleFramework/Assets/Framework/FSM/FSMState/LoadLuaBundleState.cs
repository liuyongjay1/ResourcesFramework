using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LoadLuaBundleState : StateBase
{
    public override void OnEnter(object[] args)
    {
#if UNITY_EDITOR
        //GameSetting.Instance == null说明配置文件还没加载
        if (GameSetting.Instance.AssetbundleMode)//不用AB，直接加载
        {

        }
        else 
        { 
        
        }
#endif
    }

    public void StartLuaScript()
    {
      
    }
    public override void OnExit()
    {
       
    }
}
