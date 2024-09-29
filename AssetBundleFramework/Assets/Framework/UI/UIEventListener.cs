using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using XLua;
using System;

/// <summary>
/// UI组件事件监听
/// </summary>
[LuaCallCSharp]

public class UIEventListener : UnityEngine.EventSystems.EventTrigger
{
    public Action<LuaTable,GameObject> onClick;
    public Action<LuaTable, GameObject> onDown;
    public Action<LuaTable, GameObject> onEnter;
    public Action<LuaTable, GameObject> onExit;
    public Action<LuaTable, GameObject> onUp;
    public Action<LuaTable, GameObject> onSelect;
    public Action<LuaTable, GameObject> onUpdateSelect;

    private static LuaTable _SelfTable;
    static public UIEventListener Get(LuaTable self,GameObject go)
    {
        _SelfTable = self;
        UIEventListener listener = go.GetComponent<UIEventListener>();
        if (listener == null) listener = go.AddComponent<UIEventListener>();
        return listener;
    }

    //进入UI
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(_SelfTable,gameObject);
    }

    //离开UI
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(_SelfTable,gameObject);
    }

    //点击UI
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(_SelfTable,gameObject);
    }

    //按下UI
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(_SelfTable,gameObject);
    }

    //抬起UI
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(_SelfTable,gameObject);
    }



        //public override void OnSelect(BaseEventData eventData)
        //{
        //    if (onSelect != null) onSelect(gameObject);
        //}

        //public override void OnUpdateSelected(BaseEventData eventData)
        //{
        //    if (onUpdateSelect != null) onUpdateSelect(gameObject);
        //}
    }
