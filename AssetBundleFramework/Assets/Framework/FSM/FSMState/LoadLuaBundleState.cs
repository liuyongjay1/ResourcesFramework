using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LoadLuaBundleState : StateBase
{
    public override void OnEnter(object[] args)
    {
#if UNITY_EDITOR
        //GameSetting.Instance == null˵�������ļ���û����
        if (GameSetting.Instance.AssetbundleMode)//����AB��ֱ�Ӽ���
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
