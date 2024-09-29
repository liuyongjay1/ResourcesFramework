using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.Video;
using System;

public abstract class AssetLoaderBase
{
    //资源编辑器路径
    protected string _resEditorPath { private set; get; }
    //当前加载状态
    protected AssetLoadState _LoadState;
    //当前使用状态
    protected AssetUseState _useState;
    //业务层的加载成功回调
	protected OnLoadCallback _prepareCallback;
    //通知业务层结果后是否卸载
    private bool _autoRelease;

	public AssetLoaderBase()
	{
	}

    /// <summary>
    /// 异步加载
    /// </summary>
    public void LoadAsync(string path, OnLoadCallback callback, Type targetType, bool autoRelease = false)
    {
        _useState = AssetUseState.Using;
        if (callback == null)
        {
            LogManager.LogError("load callback cant be null, return");
            return;
        }
        //如果该Loader已有结果，则不重复开启加载任务
        if (_LoadState == AssetLoadState.LoadSuccess ||_LoadState == AssetLoadState.LoadFailed)
        {
            callback?.Invoke(this,_LoadState == AssetLoadState.LoadSuccess);
            return;
        }
        _prepareCallback += callback;
		_resEditorPath = path;
        _LoadState = AssetLoadState.Loading;
        _autoRelease = autoRelease;
        LoadTaskManager.Instance.CreateLoadTask(this, OnLoadTaskFinish, targetType);
	}

    protected virtual void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
    {
        //实例化资源后无需维护回调。
        _prepareCallback = null;
        if (_autoRelease)
            ResourceManager.Instance.ReleaseLoader(this);
    }

    ///资源已实例化成功
    public bool LoadFinish()
    {
        return _LoadState == AssetLoadState.LoadSuccess;
    }

    #region 资源释放相关

    public virtual void Release()
    {
        _prepareCallback = null;
    }

    //获取资源编辑器路径
    public string GetResEditorPath()
    {
        return _resEditorPath;
    }

    public AssetUseState GetAssetUseState()
    {
        return _useState;
    }
    #endregion
}