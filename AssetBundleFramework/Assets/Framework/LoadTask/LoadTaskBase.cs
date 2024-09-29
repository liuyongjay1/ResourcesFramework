using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskLoadState
{
    None = 0,
    LoadingBundle = 1,
    LoadBundleSuccess = 2,
    LoadingAsset = 3,//LoadingAsset状态 //AB模式代表AB及依赖已全部加载完成,Editor模式下一帧加载
    LoadAssetSuccess = 4,
    LoadAssetFailed = 5,
    UnLoadBundle = 6,
}

/*
 * 同一个地址只可能存在一个Task
 */
public abstract class LoadTaskBase
{
    //编辑器资源路径
    protected string _resEditorPath;
    //加载状态
    protected TaskLoadState _loadState;
    //AssetLoader注册回调
    protected Action<UnityEngine.Object, bool> _callback;
    //目标资源类型
    protected Type _targetType;
    //加载后资源
    protected UnityEngine.Object _target;
    //加载后是否自动卸载
    protected bool _autoRelease;

    public void StartTask(string loadResPath,Action<UnityEngine.Object, bool> callback,Type targetType, bool autoRelease = false)
    {
        //加载任务已完成
        if (_loadState == TaskLoadState.LoadAssetSuccess || _loadState == TaskLoadState.LoadAssetFailed)
        {
            callback(_target, _loadState == TaskLoadState.LoadAssetSuccess);
            return;
        }
        //加载任务已存在但未完成，添加AssetLoader回调
        if (_loadState != TaskLoadState.None)
        {
            _callback += callback;
            return;
        }
        _callback += callback;
        _resEditorPath = loadResPath;
        _targetType = targetType;
        _loadState = TaskLoadState.LoadingBundle;
        _autoRelease = autoRelease;
        DoTask();
    }

    /// <summary>
    /// 交给子类去完成任务
    /// </summary>
    public abstract void DoTask();
    public abstract void Tick();
    public abstract void ReleaseTask();
    public abstract void UnLoadBundle();

    //获取资源编辑器路径
    public string GetResEditorPath()
    {
        return _resEditorPath;
    }

    public TaskLoadState GetTaskLoadState()
    {
        return _loadState;
    }
}
