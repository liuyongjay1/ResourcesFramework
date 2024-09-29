using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
using XLua;

/// <summary>
/// 资源加载结果
/// </summary>
public enum AssetLoadState
{
    None = 0,
    Loading = 1,
    LoadSuccess = 2,
    LoadFailed = 3,
}

/// <summary>
/// Loader使用状态
/// Using是出池的时候设置
/// Recycle是调用Unload接口
/// Release是调用Release接口
/// </summary>
public enum AssetUseState
{
    Using = 0,
    Recycle = 1,
    Release = 2,
}
public partial class ResourceManager:Singleton<ResourceManager>{
    /*
        1、一个Loader对应一个实例化资源
        2、只有GameObjectLoader有UnLoad接口，因为只有预设有回收利用的价值，其他类型的Loader直接释放即可
        3、只有GameObjectLoader有定时检查Release功能，比如释放5分钟一直处于Recycle状态
        4、所有Loader都有Release功能，释放实例化资源
     */

    /*
     *      提示：
     *      如果Lua层一直持有Loader对象，就算在ResourceManager里移除这个Loader，托管堆上依然不会释放它的地址
     *      这个Loader可能会变成野指针
     *      移除Loader前会释放资源，如果Lua使用该Loader，里面的实例化对象一定为空，会报错。
     */


    //Prefab对象池 key:资源地址 value：同一地址资源的对象池
    Dictionary<string, TObjectPool<GameObjectLoader>> _GameObjectPool = new Dictionary<string, TObjectPool<GameObjectLoader>>();
    //非Prefab对象池一个地址只可能存在一个Loader
    Dictionary<string, AssetLoaderBase> _AssetLoaderPool  = new Dictionary<string, AssetLoaderBase>();

    #region GameObjectLoader相关
    //回收预设后保存的根
    private static Transform _RecycleRoot;
    private string _RecycleRootName = "RecycleRoot";

    /// <summary>
    /// 初始化接口
    /// </summary>
    /// <param name="root"></param>
    public void Init(GameObject root)
    {
#if UNITY_EDITOR
        //编辑器模式会创建一个根物体，用于观察回收GameObject情况
        GameObject _newob = new GameObject(_RecycleRootName);
        _newob.transform.SetParent(root.transform);
        _RecycleRoot = _newob.transform;
#endif
    }

    /// <summary>
    /// 获取GameObject对象，一个Asset对应一个实例化资源
    /// </summary>
    /// <param name="url">资源地址</param>
    /// <param name="callback">该回调是业务层传给Asset的回调，并不是Asset向AA注册的回调</param>
    /// <returns></returns>
    public GameObjectLoader GetGameObjectLoader(string url, OnLoadCallback callback, bool AutoRelease)
	{
        GameObjectLoader loader = null;
        TObjectPool<GameObjectLoader> pool = null;
        if (!_GameObjectPool.ContainsKey(url))
        {
            _GameObjectPool[url] = new TObjectPool<GameObjectLoader>();
        }
        pool = _GameObjectPool[url];
        loader = pool.Pop();
        loader.LoadAsync(url, callback,typeof(GameObject), AutoRelease);
        return loader;
    }

    /// <summary>
    /// 回收API，回收不是卸载，实例化资源还在
    /// 当业务层需要加载相同Url资源时，会返回该Loader
    /// </summary>
    /// <param name="loader"></param>
    public void RecycleGameObjectLoader(GameObjectLoader loader)
    {
        ResetGameObject(loader);
        if (_GameObjectPool[loader.GetResEditorPath()] == null)
            LogManager.LogError("Pool not Exist,please Check GameObjectPool URL: " + loader.GetResEditorPath());
        TObjectPool<GameObjectLoader> pool = _GameObjectPool[loader.GetResEditorPath()];
        pool.Push(loader);
    }

    /// <summary>
    /// 重置GameObject实例接口
    /// 有两个地方调用，1，回收时调用， 2，加载完时判断非使用状态调用。
    /// </summary>
    /// <param name="loader"></param>
    public void ResetGameObject(GameObjectLoader loader)
    {
        if (loader.LoadFinish())
            loader.GetGameObject().SetActive(false);
#if UNITY_EDITOR
        //创建一个根物体，把相同URL的物体全部回收到一个根下
        Transform UrlRoot = null;

        string rootName = MakeRootName(loader.GetResEditorPath());
        UrlRoot = _RecycleRoot.Find(rootName);
        if (UrlRoot == null)
        {
            GameObject _newob = new GameObject(rootName);
            _newob.transform.SetParent(_RecycleRoot.transform);
            UrlRoot = _newob.transform;
            UrlRoot.name = rootName;
        }

        if (loader.LoadFinish())
            loader.GetGameObject().transform.SetParent(UrlRoot);
#endif
    }


#if UNITY_EDITOR
    /// <summary>
    /// 创建一个根物体，把相同URL的物体全部回收到一个根下
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private string MakeRootName(string path)
    {
        string name = path.Replace('/', '-');
        return name;
    }
#endif
#endregion

    #region TextureLoader相关
    /// <summary>
    /// 加载Texture资源
    /// 请注意：TextureLoader没有回收接口，只有释放接口，加载一次，向该对象注册加载回调即可使用
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public TextureLoader GetTextureLoader(string url, OnLoadCallback callback, bool AutoRelease)
    {
        TextureLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (TextureLoader)_AssetLoaderPool[url];
        else
        {
            loader = new TextureLoader();
            _AssetLoaderPool[url] = loader;
        }   
        loader.LoadAsync(url, callback, typeof(Texture), AutoRelease);
        return loader;
    }

#endregion

    #region TextFileLoader相关
    /// <summary>
    /// 加载文本、XML、bytes文件通用接口
    /// 请注意：TextFileLoader没有回收接口，只有释放接口，加载一次，向该对象注册加载回调即可使用
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public TextFileLoader GetTextFileLoader(string url, OnLoadCallback callback, bool AutoRelease)
    {
        TextFileLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (TextFileLoader)_AssetLoaderPool[url];
        else
        {
            loader = new TextFileLoader();
            _AssetLoaderPool[url] = (TextFileLoader)loader;
        }
        loader.LoadAsync(url, callback, typeof(TextAsset), AutoRelease);
        return loader;
    }

    #endregion

    #region AtlasLoader相关
    /// <summary>
    /// 加载文本、XML、bytes文件通用接口
    /// 请注意：TextFileLoader没有回收接口，只有释放接口，加载一次，向该对象注册加载回调即可使用
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public AtlasLoader GetAtlasLoader(string url, OnLoadCallback callback,bool AutoRelease)
    {
        AtlasLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (AtlasLoader)_AssetLoaderPool[url];
        else
        {
            loader = new AtlasLoader();
            _AssetLoaderPool[url] = (AtlasLoader)loader;
        }
        loader.LoadAsync(url, callback, typeof(SpriteAtlas), AutoRelease);
        return loader;
    }

#endregion

    #region MaterialLoader相关
    /// <summary>
    /// 加载文本、XML、bytes文件通用接口
    /// 请注意：TextFileLoader没有回收接口，只有释放接口，加载一次，向该对象注册加载回调即可使用
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public MaterialLoader GetMaterialLoader(string url, OnLoadCallback callback, bool AutoRelease)
    {
        MaterialLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (MaterialLoader)_AssetLoaderPool[url];
        else
        {
            loader = new MaterialLoader();
            _AssetLoaderPool[url] = (MaterialLoader)loader;
        }
        loader.LoadAsync(url, callback, typeof(Material), AutoRelease);
        return loader;
    }
    #endregion

    #region AudioLoader
    /// <summary>
    /// 加载文本、XML、bytes文件通用接口
    /// 请注意：TextFileLoader没有回收接口，只有释放接口，加载一次，向该对象注册加载回调即可使用
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public AudioLoader GetAudioLoader(string url, OnLoadCallback callback, bool AutoRelease)
    {
        AudioLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (AudioLoader)_AssetLoaderPool[url];
        else
        {
            loader = new AudioLoader();
            _AssetLoaderPool[url] = (AudioLoader)loader;
        }
        loader.LoadAsync(url, callback, typeof(AudioClip), AutoRelease);
        return loader;
    }
    #endregion

    #region SceneLoader
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">是否是叠加场景</param>
    /// <returns></returns>
    public SceneLoader GetSceneLoader(string url, OnLoadCallback callback, bool isAdd,bool AutoRelease = false)
    {
        SceneLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (SceneLoader)_AssetLoaderPool[url];
        else
        {
            loader = new SceneLoader();
            _AssetLoaderPool[url] = (SceneLoader)loader;
        }
        loader.LoadAsync(url, callback, typeof(Scene), AutoRelease);
        loader.SetSceneLoadMode(isAdd);
        return loader;
    }
    #endregion

    #region ConfigLoader
    /// <summary>
    /// 加载序列化的UnityAsset
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">是否是叠加场景</param>
    /// <returns></returns>
    public ConfigLoader GetConfigLoader(string url, OnLoadCallback callback, Type targetType, bool AutoRelease = false)
    {
        ConfigLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (ConfigLoader)_AssetLoaderPool[url];
        else
        {
            loader = new ConfigLoader();
            _AssetLoaderPool[url] = (ConfigLoader)loader;
        }
        loader.LoadAsync(url, callback, targetType, AutoRelease);
        return loader;
    }
    #endregion

    #region FontLoader
    /// <summary>
    /// 加载序列化的UnityAsset
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">是否是叠加场景</param>
    /// <returns></returns>
    public FontLoader GetFontLoader(string url, OnLoadCallback callback, Type targetType, bool AutoRelease = false)
    {
        FontLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (FontLoader)_AssetLoaderPool[url];
        else
        {
            loader = new FontLoader();
            _AssetLoaderPool[url] = (FontLoader)loader;
        }
        loader.LoadAsync(url, callback, targetType, AutoRelease);
        return loader;
    }
    #endregion


    #region AnimationLoader
    /// <summary>
    /// 加载序列化的UnityAsset
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">是否是叠加场景</param>
    /// <returns></returns>
    public AnimationClipLoader GetAnimationClipLoader(string url, OnLoadCallback callback, Type targetType, bool AutoRelease = false)
    {
        AnimationClipLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (AnimationClipLoader)_AssetLoaderPool[url];
        else
        {
            loader = new AnimationClipLoader();
            _AssetLoaderPool[url] = (AnimationClipLoader)loader;
        }
        loader.LoadAsync(url, callback, targetType, AutoRelease);
        return loader;
    }
    #endregion


    #region ShaderLoader
    /// <summary>
    /// 加载序列化的UnityAsset
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">是否是叠加场景</param>
    /// <returns></returns>
    public ShaderLoader GetShaderLoader(string url, OnLoadCallback callback, Type targetType, bool AutoRelease = false)
    {
        ShaderLoader loader = null;
        if (_AssetLoaderPool.ContainsKey(url))
            loader = (ShaderLoader)_AssetLoaderPool[url];
        else
        {
            loader = new ShaderLoader();
            _AssetLoaderPool[url] = (ShaderLoader)loader;
        }
        loader.LoadAsync(url, callback, targetType, AutoRelease);
        return loader;
    }
    #endregion

    /// <summary>
    /// 如果Loader标记成AutoRelease，加载结束后卸载Loader
    /// </summary>
    /// <param name="loader"></param>
    public void ReleaseLoader(AssetLoaderBase loader)
    {
        if (loader is GameObjectLoader)
        {
            //暂时GameObjectLoader不会主动释放
        }
        else {
            loader.Release();
            if (!_AssetLoaderPool.Remove(loader.GetResEditorPath()))
            {
                LogManager.LogError("_AssetLoaderPool not contain key:" + loader.GetResEditorPath());
            }
        }
       
    }

    #region 释放所有资源，但不包括UI的预设，UIManager管理UI加载释放
    /// <summary>
    /// 该API会释放掉所有Loader以及资源，慎用。一般切换场景时调用
    /// </summary>
    [LuaCallCSharp]
    public void ReleaseAllLoader()
    {
        foreach(var item in _GameObjectPool)
        {
            TObjectPool<GameObjectLoader> pool = _GameObjectPool[item.Key];
            List<GameObjectLoader> unusedList = pool.GetUnusedList();
            for (int i = 0; i < unusedList.Count; i++)
                unusedList[i].Release();
            List<GameObjectLoader> usedList = pool.GetUsedList();
            for (int i = 0; i < usedList.Count; i++)
                usedList[i].Release();
            usedList.Clear();
            unusedList.Clear();
        }
        _GameObjectPool.Clear();
        foreach (var item in _AssetLoaderPool)
        {
            item.Value.Release();
        }
        _AssetLoaderPool.Clear();
    }
    #endregion

    #region 计

    private float _LoaderReleaseTimer;
    public void Tick(float deltaTime)
    {
        _LoaderReleaseTimer += deltaTime;
        //每N秒检查一次释放计时
        if (_LoaderReleaseTimer >= ResourceConfig.LoaderReleaseCheckTime)
        {
            _LoaderReleaseTimer = 0;
            foreach (var item in _GameObjectPool)
            {
                TObjectPool<GameObjectLoader> pool = _GameObjectPool[item.Key];
                //只释放未使用的资源
                List<GameObjectLoader> unusedList = pool.GetUnusedList();
                for (int i = 0; i < unusedList.Count; i++)
                {
                    if (unusedList[i].CheckCanRelease())
                    { 
                        unusedList[i].Release();//调用子类的释放接口
                        unusedList.RemoveAt(i);
                    }
                }
            }
#if UNITY_EDITOR
            for (int i = 0; i < _RecycleRoot.childCount; i++)
            {
                Transform recycleURLRoot = _RecycleRoot.GetChild(i);
                if (recycleURLRoot.childCount == 0)
                {
                    UnityEngine.Object.Destroy(recycleURLRoot.gameObject);
                }
            }
#endif
        }
    }
    #endregion


}

