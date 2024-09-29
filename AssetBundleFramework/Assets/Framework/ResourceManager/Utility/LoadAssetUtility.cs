using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public delegate void OnLoadCallback(AssetLoaderBase loader, bool state);

public class LoadAssetUtility
{
    // 加载预设
    public static GameObjectLoader LoadGameObject(string _url, OnLoadCallback callback, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetGameObjectLoader(_url, callback, AutoRelease);
    }

    //加载材质
    //Lua接口
    public static GameObjectLoader LoadGameObject(string _url, Action<LuaTable, AssetLoaderBase, bool> callback, LuaTable caller, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetGameObjectLoader(_url,
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller, loader, result);
            }
            , AutoRelease);
    }

    ////加载2D贴图
    public static TextureLoader LoadTexture2D(string _url, OnLoadCallback callback, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetTextureLoader(_url, callback, AutoRelease);
    }
    ////加载2D贴图
    //Lua接口
    public static TextureLoader LoadTexture2D(string _url, Action<LuaTable, AssetLoaderBase, bool> callback, LuaTable caller, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetTextureLoader(_url,
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller, loader, result);
            }
            , AutoRelease);
    }

    //加载文本
    //加载bytes/xml等文本文件也用该接口
    public static TextFileLoader LoadTextAsset(string _url, OnLoadCallback callback, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetTextFileLoader(_url, callback, AutoRelease);
    }

    //加载文本
    //Lua接口
    public static TextFileLoader LoadTextAsset(string _url, Action<LuaTable,AssetLoaderBase, bool> callback,LuaTable caller, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetTextFileLoader(_url,
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller,loader, result);
            }
            , AutoRelease);
    }

    ////加载文本
    ////加载bytes/xml等文本文件也用该接口
    //public static ByteLoader LoadByteAsset(string _url, OnLoadCallback callback, bool AutoRelease = false)
    //{
    //    return ResourceManager.Instance.GetByteLoader(_url, callback, AutoRelease);
    //}

    ////加载Byte
    ////Lua接口
    //public static ByteLoader LoadByteAsset(string _url, Action<LuaTable,AssetLoaderBase, bool> callback, LuaTable caller, bool AutoRelease = false)
    //{
    //    return ResourceManager.Instance.GetByteLoader(_url,
    //        delegate (AssetLoaderBase loader, bool result)
    //        {
    //            callback(caller,loader, result);
    //        }
    //        , AutoRelease);
    //}

    //加载图集
    public static AtlasLoader LoadAtlasAsset(string _url, OnLoadCallback callback, bool AutoRelease = false)
    {
        if (!_url.EndsWith(".spriteatlas"))
            _url = _url + ".spriteatlas";
        return ResourceManager.Instance.GetAtlasLoader(_url, callback, AutoRelease);
    }

    //加载图集
    //Lua接口
    public static AtlasLoader LoadAtlasAsset(string _url, Action<LuaTable, AssetLoaderBase, bool> callback, LuaTable caller, bool AutoRelease = false)
    {
        if (!_url.EndsWith(".spriteatlas"))
            _url = _url + ".spriteatlas";
        return ResourceManager.Instance.GetAtlasLoader(_url,
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller, loader, result);
            }
            , AutoRelease);
    }

    //加载音频
    public static AudioLoader LoadAudioAsset(string _url, OnLoadCallback callback, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetAudioLoader(_url, callback, AutoRelease);
    }

    ////加载音频
    //public static void LoadVedio(string _url, Action<string, VideoClip> callback)
    //{
    //    AA.LoadAsset<VideoClip>(_url, callback, _binded = null);
    //}

    //加载材质
    public static MaterialLoader LoadMaterialAsset(string _url, OnLoadCallback callback, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetMaterialLoader(_url, callback, AutoRelease);
    }

    //加载材质
    //Lua接口
    public static MaterialLoader LoadMaterialAsset(string _url, Action<LuaTable, AssetLoaderBase, bool> callback, LuaTable caller, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetMaterialLoader(_url,
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller, loader, result);
            }
            , AutoRelease);
    }

    //加载材质
    public static SceneLoader LoadSceneAsset(string _url, OnLoadCallback callback,bool isAdd, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetSceneLoader(_url, callback, isAdd, AutoRelease);
    }

    //加载材质
    //Lua接口
    public static SceneLoader LoadSceneAsset(string _url, Action<LuaTable, AssetLoaderBase, bool> callback, LuaTable caller, bool isAdd, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetSceneLoader(_url,
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller, loader, result);
            },
            isAdd, AutoRelease);
    }

    //加载UnityAsset
    public static ConfigLoader LoadUnityAsset(string _url, OnLoadCallback callback, Type targetType, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetConfigLoader(_url, callback, targetType, AutoRelease);
    }

    //加载UnityAsset
    //Lua接口
    public static ConfigLoader LoadUnityAsset(string _url, Action<LuaTable, AssetLoaderBase, bool> callback, LuaTable caller, Type targetType, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetConfigLoader(_url, 
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller, loader, result);
            }
            , targetType, AutoRelease);
    }

    //加载UnityAsset
    public static FontLoader LoadFontAsset(string _url, OnLoadCallback callback, Type targetType, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetFontLoader(_url, callback, targetType, AutoRelease);
    }

    //加载UnityAsset
    //Lua接口
    public static FontLoader LoadFontAsset(string _url, Action<LuaTable, AssetLoaderBase, bool> callback, LuaTable caller, Type targetType, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetFontLoader(_url,
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller, loader, result);
            }
            , targetType, AutoRelease);
    }

    //加载UnityAsset
    public static AnimationClipLoader LoadAnimationClipAsset(string _url, OnLoadCallback callback,  bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetAnimationClipLoader(_url, callback, typeof(AnimationClip), AutoRelease);
    }

    //加载UnityAsset
    //Lua接口
    public static AnimationClipLoader LoadAnimationClipAsset(string _url, Action<LuaTable, AssetLoaderBase, bool> callback, LuaTable caller, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetAnimationClipLoader(_url,
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller, loader, result);
            }
            , typeof(AnimationClip), AutoRelease);
    }

    //加载UnityAsset
    public static ShaderLoader LoadShaderAsset(string _url, OnLoadCallback callback, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetShaderLoader(_url, callback, typeof(AnimationClip), AutoRelease);
    }

    //加载UnityAsset
    //Lua接口
    public static ShaderLoader LoadShaderAsset(string _url, Action<LuaTable, AssetLoaderBase, bool> callback, LuaTable caller, bool AutoRelease = false)
    {
        return ResourceManager.Instance.GetShaderLoader(_url,
            delegate (AssetLoaderBase loader, bool result)
            {
                callback(caller, loader, result);
            }
            , typeof(AnimationClip), AutoRelease);
    }
}
