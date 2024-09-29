using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class InputManager : Singleton<InputManager>
{
    public bool Enable
    {
        get; set;
    }

    public void Init()
    {
        Enable = true;
    }
    class CMActionProcess
    {
        public LuaTable table;
        public KeyAction keyAction;
        public TouchCallback touchfunc;
        //public AxisCallback axisfunc;
        public AxisAction axisAction;
    }
    class CMActionProcessGroup
    {
        public List<CMActionProcess> ProcessList = new List<CMActionProcess>();
    }

    //Dictionary<KeyCode, CMInputAction> ActionMap = new Dictionary<KeyCode, CMInputAction>();
    Dictionary<string, CMActionProcessGroup> ProcessMap = new Dictionary<string, CMActionProcessGroup>();
    public void Tick(float fFrameTime)
    {
        if (!Enable)
            return;
        TickTouch();
    }

#region Action相关
    /// <summary>
    /// 按键回调
    /// </summary>
    /// <param name="t">Lua对象</param>
    /// <param name="type">0按下，1抬起</param>
    /// <param name="time">按钮事件时间</param>
    [CSharpCallLua]
    public delegate void KeyCallback(LuaTable t, string actionName, int type, float time);
    class KeyAction
    {
        public string ActionName;
        public float ActionTime;
        public KeyCallback keyfunc;
    }

    static public void LuaBindAction(string actionName, LuaTable tab, string func)
    {
        if (tab == null)
            return;
        LuaRemoveAction(actionName, tab);
        CMActionProcessGroup grp;
        if (!InputManager.Instance.ProcessMap.TryGetValue(actionName, out grp))
        {
            grp = new CMActionProcessGroup();
            InputManager.Instance.ProcessMap.Add(actionName, grp);
        }

        CMActionProcess proc = new CMActionProcess();
        proc.table = tab;
        grp.ProcessList.Add(proc);
        proc.keyAction = new KeyAction();
        proc.keyAction.ActionName = actionName;
        proc.keyAction.keyfunc = tab.Get<KeyCallback>(func); ;
    }

    static public void LuaRemoveAction(string actionName, LuaTable tab)
    {
        if (tab == null)
            return;
        CMActionProcessGroup grp;
        if (!InputManager.Instance.ProcessMap.TryGetValue(actionName, out grp))
        {
            return;
        }
        CMActionProcess p = null;
        foreach (CMActionProcess proce in grp.ProcessList)
        {
            if (proce.table == tab)
                p = proce;
        }
        if (p != null)
        {
            grp.ProcessList.Remove(p);
        }
    }

    static public void BindAction(string actionName, KeyCallback func)
    {
        RemoveAction(actionName, func);
        CMActionProcessGroup grp;
        if (!InputManager.Instance.ProcessMap.TryGetValue(actionName, out grp))
        {
            grp = new CMActionProcessGroup();
            InputManager.Instance.ProcessMap.Add(actionName, grp);
        }

        CMActionProcess proc = new CMActionProcess();
        proc.table = null;
        grp.ProcessList.Add(proc);
        proc.keyAction = new KeyAction();
        proc.keyAction.keyfunc = func;
    }

    static public void RemoveAction(string actionName, KeyCallback func)
    {
        CMActionProcessGroup grp;
        if (!InputManager.Instance.ProcessMap.TryGetValue(actionName, out grp))
        {
            return;
        }
        CMActionProcess p = null;
        foreach (CMActionProcess proce in grp.ProcessList)
        {
            if (proce.keyAction.keyfunc == func)
                p = proce;
        }
        if (p != null)
        {
            grp.ProcessList.Remove(p);
        }
    }
  

    /// <summary>
    /// Action执行
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="type">0按下，1抬起</param>
    /// <param name="time">按下抬起间隔时间</param>
    public void ExcuteAction(string actionName, int type)
    {
        CMActionProcessGroup grp;
        if (ProcessMap.TryGetValue(actionName, out grp))
        {
            foreach (CMActionProcess proc in grp.ProcessList)
            {
                if (proc.keyAction.keyfunc != null)
                {
                    proc.keyAction.keyfunc(proc.table, actionName,type, Time.realtimeSinceStartup);
                }
            }
        }
    }
#endregion
#region 手指相关

    [CSharpCallLua]
    public delegate void TouchCallback(LuaTable t, int fingerId, float x, float y, float dx, float dy, float dTime);

    CMActionProcessGroup TouchFunc = new CMActionProcessGroup();

    static public void LuaBindTouch(LuaTable tab, string func)
    {
        if (tab == null)
            return;
        LuaRemoveTouch(tab);
        CMActionProcess proc = new CMActionProcess();
        InputManager.Instance.TouchFunc.ProcessList.Add(proc);
        proc.table = tab;
        proc.touchfunc = tab.Get<TouchCallback>(func);
    }
    static public void BindTouch(TouchCallback func)
    {
        RemoveTouch(func);
        if (func == null)
            return;
        CMActionProcess proc = new CMActionProcess();
        InputManager.Instance.TouchFunc.ProcessList.Add(proc);
        proc.table = null;
        proc.touchfunc = func;
    }
    static public void LuaRemoveTouch(LuaTable tab)
    {
        if (tab == null)
            return;
        CMActionProcess p = null;
        foreach (CMActionProcess proce in InputManager.Instance.TouchFunc.ProcessList)
        {
            if (proce.table == tab)
                p = proce;
        }
        if (p != null)
        {
            InputManager.Instance.TouchFunc.ProcessList.Remove(p);
        }
    }

    static public void RemoveTouch(TouchCallback func)
    {
        CMActionProcess p = null;
        foreach (CMActionProcess proce in InputManager.Instance.AxisFunc.ProcessList)
        {
            if (proce.touchfunc == func)
                p = proce;
        }
        if (p != null)
        {
            InputManager.Instance.AxisFunc.ProcessList.Remove(p);
        }
    }

    //手指
    void TickTouch()
    {
        if (TouchFunc == null || !Input.touchPressureSupported || Input.touchCount < 0)
            return;
        Touch[] ts = Input.touches;
        foreach (Touch t in ts)
        {
            foreach (CMActionProcess proce in TouchFunc.ProcessList)
            {
                if (proce.touchfunc != null)
                    proce.touchfunc(null, t.fingerId, t.position.x, t.position.y, t.deltaPosition.x, t.deltaPosition.y, t.deltaTime);
            }
        }
    }
#endregion
#region 摇杆相关

    /// <summary>
    /// 
    /// </summary>
    /// <param name="t">Lua对象</param>
    /// <param name="index">摇杆编号</param>
    /// <param name="x">横向偏移-1到1</param>
    /// <param name="y">纵向偏移-1到1</param>
    [CSharpCallLua]
    public delegate void AxisCallback(LuaTable t, int index, float x, float y);


    CMActionProcessGroup AxisFunc = new CMActionProcessGroup();

    class AxisAction
    {
        public int JoystickIndex = 0;
        public AxisCallback axisfunc;
    }

    static public void LuaBindAxis(int joyIndex, LuaTable tab, string func)
    {
        if (tab == null)
            return;
        LuaRemoveAxis(tab);
        CMActionProcess proc = new CMActionProcess();
        InputManager.Instance.AxisFunc.ProcessList.Add(proc);
        proc.table = tab;
        proc.axisAction = new AxisAction();
        proc.axisAction.axisfunc = tab.Get<AxisCallback>(func);
        proc.axisAction.JoystickIndex = joyIndex;
    }

    static public void BindAxis(int joyIndex, AxisCallback func)
    {
        RemoveAxis(joyIndex, func);
        if (func == null)
            return;
        CMActionProcess proc = new CMActionProcess();
        InputManager.Instance.AxisFunc.ProcessList.Add(proc);
        proc.table = null;
        proc.axisAction = new AxisAction();
        proc.axisAction.axisfunc = func;
        proc.axisAction.JoystickIndex = joyIndex;
    }

    static public void LuaRemoveAxis(LuaTable tab)
    {
        if (tab == null)
            return;
        CMActionProcess p = null;
        foreach (CMActionProcess proce in InputManager.Instance.AxisFunc.ProcessList)
        {
            if (proce.table == tab)
                p = proce;
        }
        if (p != null)
        {
            InputManager.Instance.AxisFunc.ProcessList.Remove(p);
        }
    }

    static public void RemoveAxis(int joyIndex, AxisCallback func)
    {
        CMActionProcess p = null;
        foreach (CMActionProcess proce in InputManager.Instance.AxisFunc.ProcessList)
        {
            if (proce.axisAction.JoystickIndex == joyIndex && proce.axisAction.axisfunc == func)
                p = proce;
        }
        if (p != null)
        {
            InputManager.Instance.AxisFunc.ProcessList.Remove(p);
        }
    }

    public void AxisDrag(int index, float x, float y)
    {
        foreach (CMActionProcess proce in AxisFunc.ProcessList)
        {
            if (proce.axisAction.JoystickIndex == index && proce.axisAction.axisfunc != null)
                if (proce.table != null)
                    proce.axisAction.axisfunc(proce.table, index, x, y);
                else
                    proce.axisAction.axisfunc(null, index, x, y);
        }
    }
#endregion

}//class


