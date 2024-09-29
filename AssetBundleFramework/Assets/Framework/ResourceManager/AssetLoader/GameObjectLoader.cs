using UnityEngine;

public class GameObjectLoader : AssetLoaderBase
{
    /// <summary>
    /// 实例化的游戏对象
    /// </summary>
    private GameObject _GameObj;

    /// <summary>
    /// 调用Release时记录回收时间
    /// </summary>
    public float _CallUnLoadTime;

    protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
        {
            _GameObj = Object.Instantiate(asset) as GameObject;
            if (_GameObj == null)
            {
                LogManager.LogError(string.Format("Failed to instantiate GameObject : {0}", _resEditorPath));
                _prepareCallback?.Invoke(this, false);
                return;
            }
            _LoadState = AssetLoadState.LoadSuccess;
            _prepareCallback?.Invoke(this, true);
        }
        else
            _prepareCallback?.Invoke(this, false);

         base.OnLoadTaskFinish(asset, result);
    }

    //只有GameObjectLoader可以回收
    //其他资源一个地址只可能生成一份实例，但预设同一地址可能复制多份
    public void UnLoad()
    {
        _prepareCallback = null;
        _CallUnLoadTime = Time.realtimeSinceStartup;
        _useState = AssetUseState.Recycle;
        ResourceManager.Instance.RecycleGameObjectLoader(this);
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual bool CheckCanRelease()
    {
        //回收池资源可被释放
        if (_useState != AssetUseState.Recycle)
            LogManager.LogError("_useState != Recycle,please Cheak");
        return Time.realtimeSinceStartup - _CallUnLoadTime >= ResourceConfig.LoaderReleaseLimitTime;
        
    }

    public override void Release()
    {
        if (_GameObj != null)
        {
            GameObject.Destroy(_GameObj);
            _GameObj = null;
        }

        base.Release();
    }

    public GameObject GetGameObject()
    {
        return _GameObj;
    }
}