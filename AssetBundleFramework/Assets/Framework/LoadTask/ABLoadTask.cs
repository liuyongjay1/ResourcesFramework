using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ABLoadTask:LoadTaskBase
{
    public ABLoadTask(string bundlePath)
    {
        _bundleLoadPath = bundlePath;
    }

    //该AB包依赖的AB包列表
    private List<ABLoadTask> _depends = new List<ABLoadTask>(10);
    //AB包路径
    private string _bundleLoadPath;
	//AB包加载请求
    private AssetBundleCreateRequest _bundleRequest;
    //AB内资源加载请求
    private AssetBundleRequest _assetRequest;
    //AB包
    private AssetBundle _bundle;
	//引用次数
	private int RefCount;
    //资源名
    protected string _assetName;

    public override void DoTask()
    {
        //_assetName带扩展名，例如.prefab .jpg .mp3 .txt
        _assetName = System.IO.Path.GetFileName(_resEditorPath);
        StartLoadBundle(_bundleLoadPath);
    }

    public void StartLoadBundle(string localBundlePath)
    {
        string fullPath = AssetBundleManager.Instance.GetBundleLoadPath(localBundlePath);
        if (GameSetting.Instance.LoadBundleAsync)//异步加载Bundle
        {
            LogManager.LogProcedure("StartLoadBundle Async:" + fullPath);
            _bundleRequest = AssetBundle.LoadFromFileAsync(fullPath);
            _bundleRequest.completed += LoadBundleComplete;
            _loadState = TaskLoadState.LoadingBundle;
        }
        else//同步加载Bundle
        {
            LogManager.LogProcedure("StartLoadBundle:" + fullPath);
            _bundle = AssetBundle.LoadFromFile(fullPath);
            _loadState = TaskLoadState.LoadBundleSuccess;

            LoadBundleAsset();
        }
        SetDependencies();
    }

    //异步加载Bundle回调
    private void LoadBundleComplete(AsyncOperation op)
    {
        if (op.isDone)
        {
            //注意，如果调用了AssetBundle.UnloadAllAssetBundles(false);释放所有已加载AB包，是不会中断异步加载的
            if (_loadState == TaskLoadState.UnLoadBundle)//看注意
            {
                _bundleRequest.assetBundle.Unload(false);
                return;
            }
            //LogManager.LogInfo("LoadBundleComplete,path:" + _bundleLoadPath);
            _bundle = _bundleRequest.assetBundle;
            //string[] assetNames = _bundle.GetAllAssetNames();
            //for (int i = 0; i < assetNames.Length; i++)
            //{
            //    Debug.LogError("AssetName:" + assetNames[i]);
            //}
            _loadState = TaskLoadState.LoadBundleSuccess;
        }
    }

    //设置依赖Loader
    public void SetDependencies()
    {
        string[] allDepends = AssetBundleManager.Instance.GetBundleDepends(_bundleLoadPath);
        for (int i = 0; i < allDepends.Length; i++)
        {
            string dependBundlePath = allDepends[i];
            //LogManager.LogProcedure($"_bundleLoadPath:{_bundleLoadPath} 存在依赖: {allDepends[i]}");
            //获取AB任务
            ABLoadTask task = LoadTaskManager.Instance.GetABLoadTask(dependBundlePath, out bool bundleExist);
            //依赖计数+1
            task.Reference();
            //上级记录依赖包
            _depends.Add(task);
            //保存依赖包地址
            task._bundleLoadPath = dependBundlePath;
            //依赖包不存在，开启加载
            if (bundleExist == false)
                task.StartLoadBundle(dependBundlePath);
        }
    }

	//递归检查依赖包是否全部完成
	public bool IsDone()
	{
        if (_depends.Count == 0)//无依赖证明是最底层的Bundle
            return _loadState == TaskLoadState.LoadBundleSuccess;
        else
        {
            //检查本包的所有依赖，只要有一个未完成，则直接返回未完成
            for (int i = 0; i < _depends.Count; i++)
            {
                if (_depends[i].IsDone() == false)
                {
                    return false;
                }
                return true;
            }
        }
        return false;
	}

    /// <summary>
    /// 引用（引用计数递加）
    /// </summary>
    public void Reference()
    {
        RefCount++;
    }

    /// <summary>
    /// 释放（引用计数递减）
    /// </summary>
    public void DeReference()
    {
        RefCount--;
    }

    public int GetRefCount()
    {
        return RefCount;
    }

    //卸载此包,递归删除引用
    public override void ReleaseTask()
    {
        _loadState = TaskLoadState.UnLoadBundle;
        _bundleRequest.assetBundle.Unload(false);
    }

    //卸载此包,递归删除引用
    public override void UnLoadBundle()
    {
        _loadState = TaskLoadState.UnLoadBundle;
        for (int i = 0; i < _depends.Count; i++)
        {
            _depends[i].DeReference();
            _depends[i].UnLoadBundle();
        }
        //请注意，非依赖包RefCount的数量一直为0
        if (RefCount <= 0)
        {
            ReleaseTask();
        }
    }

    public void LoadBundleAsset()
    {
        //_assetName不空表示是主动加载的AB包，非依赖包
        if (_loadState == TaskLoadState.LoadBundleSuccess && !string.IsNullOrEmpty(_assetName))
        {
            if (IsDone() == true)//递归检查依赖是否完成
            {
                if (_targetType == typeof(Scene))
                {
                    _loadState = TaskLoadState.LoadAssetSuccess;
                    _callback?.Invoke(null, true);
                }
                else
                {
                    _loadState = TaskLoadState.LoadingAsset;
                    if (GameSetting.Instance.LoadAssetAsync)
                    {
                        if (_targetType == null)
                            _assetRequest = _bundle.LoadAssetAsync(_assetName);
                        else
                            _assetRequest = _bundle.LoadAssetAsync(_assetName, _targetType);
                        _assetRequest.completed += LoadAssetCallback;
                    }
                    else
                    {
                        if (_targetType == null)
                            _target = _bundle.LoadAsset(_assetName);
                        else
                            _target = _bundle.LoadAsset(_assetName, _targetType);
                        NotifyResult();
                    }
                }
            }
        }
    }
    public override void Tick()
	{
        if(_loadState == TaskLoadState.LoadBundleSuccess)
            LoadBundleAsset();
    }

    /// <summary>
    /// 异步加载Asset资源回调
    /// </summary>
    /// <param name="op"></param>
    private void LoadAssetCallback(AsyncOperation op)
    {
        if (op.isDone)
        {
            //LogManager.LogProcedure("LoadAssetCallback,assetName:" + _assetName);
            _target = _assetRequest.asset;
            NotifyResult();
        }
    }

    //加载Asset后，通知加载结果
    private void NotifyResult()
    {
        _loadState = _target == null ? TaskLoadState.LoadAssetFailed:TaskLoadState.LoadAssetSuccess;
        // 通知Loader加载结果
        _callback?.Invoke(_target, _target != null);

        if (_autoRelease)
        {
            LoadTaskManager.Instance.ReleaseLoader(this);
        }
    }

    public List<ABLoadTask> GetABDepends()
    {
        return _depends;
    }

    public string GetABPath()
    {
        return _bundleLoadPath;
    }
}
