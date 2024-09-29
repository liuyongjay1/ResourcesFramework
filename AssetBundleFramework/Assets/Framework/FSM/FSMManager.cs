using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMManager:Singleton<FSMManager>
{
    public Dictionary<Type, StateBase> allState = new Dictionary<Type, StateBase>();
    private StateBase curState;

    public void Init()
    {
        RegisterState(typeof(LoadTableState));
        RegisterState(typeof(StartLuaProjectState));
    }

    public void RegisterState(Type t)
    {
        object obj = Activator.CreateInstance(t);
        allState.Add(t,(StateBase)obj);
    }

    public void EnterState(Type t, object[] args = null)
    {
        StateBase state = null;
        if (!allState.TryGetValue(t, out state))
        {
            LogManager.LogError(string.Format("FSMManager EnterState Error,state:{0} not exist", t.ToString()));
            return;
        }
        if (curState != null)
            curState.OnExit();
        if(curState != null)
            LogManager.LogProcedure(string.Format("FSM Exit:{0} ,Enter:{1}", curState, state.ToString()));
        else
            LogManager.LogProcedure(string.Format("FSM Enter:{0}", t.ToString()));
        curState = state;
        state.OnEnter(args);

    }

    public T GetState<T>() where T: StateBase
    {
        StateBase state = null;
        Type t = typeof(T);
        if (!allState.TryGetValue(t, out state))
        {
            LogManager.LogError(string.Format("FSMManager EnterState Error,state:{0} not exist", t.ToString()));
        }
        return state as T;
    }

    public void Tick(float timeScale)
    {
        if (curState != null)
            curState.Tick();
    }
}
