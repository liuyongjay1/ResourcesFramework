using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
using XLua;

/// <summary>
/// ��Դ���ؽ��
/// </summary>
public enum AssetLoadState
{
    None = 0,
    Loading = 1,
    LoadSuccess = 2,
    LoadFailed = 3,
}

/// <summary>
/// Loaderʹ��״̬
/// Using�ǳ��ص�ʱ������
/// Recycle�ǵ���Unload�ӿ�
/// Release�ǵ���Release�ӿ�
/// </summary>
public enum AssetUseState
{
    Using = 0,
    Recycle = 1,
    Release = 2,
}
public partial class ResourceManager:Singleton<ResourceManager>{
    /*
        1��һ��Loader��Ӧһ��ʵ������Դ
        2��ֻ��GameObjectLoader��UnLoad�ӿڣ���Ϊֻ��Ԥ���л������õļ�ֵ���������͵�Loaderֱ���ͷż���
        3��ֻ��GameObjectLoader�ж�ʱ���Release���ܣ������ͷ�5����һֱ����Recycle״̬
        4������Loader����Release���ܣ��ͷ�ʵ������Դ
     */

    /*
     *      ��ʾ��
     *      ���Lua��һֱ����Loader���󣬾�����ResourceManager���Ƴ����Loader���йܶ�����Ȼ�����ͷ����ĵ�ַ
     *      ���Loader���ܻ���Ұָ��
     *      �Ƴ�Loaderǰ���ͷ���Դ�����Luaʹ�ø�Loader�������ʵ��������һ��Ϊ�գ��ᱨ��
     */


    //Prefab����� key:��Դ��ַ value��ͬһ��ַ��Դ�Ķ����
    Dictionary<string, TObjectPool<GameObjectLoader>> _GameObjectPool = new Dictionary<string, TObjectPool<GameObjectLoader>>();
    //��Prefab�����һ����ַֻ���ܴ���һ��Loader
    Dictionary<string, AssetLoaderBase> _AssetLoaderPool  = new Dictionary<string, AssetLoaderBase>();

    #region GameObjectLoader���
    //����Ԥ��󱣴�ĸ�
    private static Transform _RecycleRoot;
    private string _RecycleRootName = "RecycleRoot";

    /// <summary>
    /// ��ʼ���ӿ�
    /// </summary>
    /// <param name="root"></param>
    public void Init(GameObject root)
    {
#if UNITY_EDITOR
        //�༭��ģʽ�ᴴ��һ�������壬���ڹ۲����GameObject���
        GameObject _newob = new GameObject(_RecycleRootName);
        _newob.transform.SetParent(root.transform);
        _RecycleRoot = _newob.transform;
#endif
    }

    /// <summary>
    /// ��ȡGameObject����һ��Asset��Ӧһ��ʵ������Դ
    /// </summary>
    /// <param name="url">��Դ��ַ</param>
    /// <param name="callback">�ûص���ҵ��㴫��Asset�Ļص���������Asset��AAע��Ļص�</param>
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
    /// ����API�����ղ���ж�أ�ʵ������Դ����
    /// ��ҵ�����Ҫ������ͬUrl��Դʱ���᷵�ظ�Loader
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
    /// ����GameObjectʵ���ӿ�
    /// �������ط����ã�1������ʱ���ã� 2��������ʱ�жϷ�ʹ��״̬���á�
    /// </summary>
    /// <param name="loader"></param>
    public void ResetGameObject(GameObjectLoader loader)
    {
        if (loader.LoadFinish())
            loader.GetGameObject().SetActive(false);
#if UNITY_EDITOR
        //����һ�������壬����ͬURL������ȫ�����յ�һ������
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
    /// ����һ�������壬����ͬURL������ȫ�����յ�һ������
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

    #region TextureLoader���
    /// <summary>
    /// ����Texture��Դ
    /// ��ע�⣺TextureLoaderû�л��սӿڣ�ֻ���ͷŽӿڣ�����һ�Σ���ö���ע����ػص�����ʹ��
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

    #region TextFileLoader���
    /// <summary>
    /// �����ı���XML��bytes�ļ�ͨ�ýӿ�
    /// ��ע�⣺TextFileLoaderû�л��սӿڣ�ֻ���ͷŽӿڣ�����һ�Σ���ö���ע����ػص�����ʹ��
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

    #region AtlasLoader���
    /// <summary>
    /// �����ı���XML��bytes�ļ�ͨ�ýӿ�
    /// ��ע�⣺TextFileLoaderû�л��սӿڣ�ֻ���ͷŽӿڣ�����һ�Σ���ö���ע����ػص�����ʹ��
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

    #region MaterialLoader���
    /// <summary>
    /// �����ı���XML��bytes�ļ�ͨ�ýӿ�
    /// ��ע�⣺TextFileLoaderû�л��սӿڣ�ֻ���ͷŽӿڣ�����һ�Σ���ö���ע����ػص�����ʹ��
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
    /// �����ı���XML��bytes�ļ�ͨ�ýӿ�
    /// ��ע�⣺TextFileLoaderû�л��սӿڣ�ֻ���ͷŽӿڣ�����һ�Σ���ö���ע����ػص�����ʹ��
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
    /// ���س���
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">�Ƿ��ǵ��ӳ���</param>
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
    /// �������л���UnityAsset
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">�Ƿ��ǵ��ӳ���</param>
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
    /// �������л���UnityAsset
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">�Ƿ��ǵ��ӳ���</param>
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
    /// �������л���UnityAsset
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">�Ƿ��ǵ��ӳ���</param>
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
    /// �������л���UnityAsset
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="isAdd">�Ƿ��ǵ��ӳ���</param>
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
    /// ���Loader��ǳ�AutoRelease�����ؽ�����ж��Loader
    /// </summary>
    /// <param name="loader"></param>
    public void ReleaseLoader(AssetLoaderBase loader)
    {
        if (loader is GameObjectLoader)
        {
            //��ʱGameObjectLoader���������ͷ�
        }
        else {
            loader.Release();
            if (!_AssetLoaderPool.Remove(loader.GetResEditorPath()))
            {
                LogManager.LogError("_AssetLoaderPool not contain key:" + loader.GetResEditorPath());
            }
        }
       
    }

    #region �ͷ�������Դ����������UI��Ԥ�裬UIManager����UI�����ͷ�
    /// <summary>
    /// ��API���ͷŵ�����Loader�Լ���Դ�����á�һ���л�����ʱ����
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

    #region ��

    private float _LoaderReleaseTimer;
    public void Tick(float deltaTime)
    {
        _LoaderReleaseTimer += deltaTime;
        //ÿN����һ���ͷż�ʱ
        if (_LoaderReleaseTimer >= ResourceConfig.LoaderReleaseCheckTime)
        {
            _LoaderReleaseTimer = 0;
            foreach (var item in _GameObjectPool)
            {
                TObjectPool<GameObjectLoader> pool = _GameObjectPool[item.Key];
                //ֻ�ͷ�δʹ�õ���Դ
                List<GameObjectLoader> unusedList = pool.GetUnusedList();
                for (int i = 0; i < unusedList.Count; i++)
                {
                    if (unusedList[i].CheckCanRelease())
                    { 
                        unusedList[i].Release();//����������ͷŽӿ�
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

