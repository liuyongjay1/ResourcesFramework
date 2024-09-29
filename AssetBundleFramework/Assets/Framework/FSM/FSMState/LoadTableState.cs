/*
 * =============状态机===============
 * 加载C#配表
 * =============状态机===============
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTableState :StateBase
{
    public int _WaitLoadTableCount;
    public override void OnEnter(object[] args)
    {
        _WaitLoadTableCount = TableManager.Instance.Init();
    }

    public void LoadTableCallback()
    {
        _WaitLoadTableCount--;
        if (_WaitLoadTableCount == 0)
        {
            FSMManager.Instance.EnterState(typeof(StartLuaProjectState));
        }
    }

    public override void OnExit()
    {

    }
}
