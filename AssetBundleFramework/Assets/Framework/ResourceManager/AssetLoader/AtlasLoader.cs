//**************************************************
// Copyright©2018 何冠峰
// Licensed under the MIT license
//**************************************************
using UnityEngine;
using UnityEngine.U2D;

public class AtlasLoader : AssetLoaderBase
{
    private SpriteAtlas _Atlas;

	protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
        {
            _Atlas = asset as SpriteAtlas;
            if (_Atlas == null)
            {
                LogManager.LogError(string.Format("Failed to instantiate Atlas Path: {0}", _resEditorPath));
                _prepareCallback?.Invoke(this, false);
                return;
            }
            _LoadState = AssetLoadState.LoadSuccess;
            _prepareCallback?.Invoke(this, true);
        }
        else
        {
            LogManager.LogError(string.Format("Failed to LoadAtlas Path: {0}", _resEditorPath));
            _prepareCallback?.Invoke(this, false);
        }

        base.OnLoadTaskFinish(asset, result);
    }

    public override void Release()
    {
        if (_Atlas != null)
        {
            Resources.UnloadAsset(_Atlas);
            _Atlas = null;
        }
        base.Release();
    }

    public UnityEngine.Sprite GetSpriteByName(string name)
    {
        if (_Atlas == null)
        {
            LogManager.LogError(string.Format("GetSpriteByName is null spriteName: {0} ,resPath: {1} ",name,_resEditorPath));
            return null;
        }
        Sprite sp = _Atlas.GetSprite(name);
        if (sp == null)
            LogManager.LogError("_Atlas.GetSprite is nil ,Atlas : {0} spriteName: {1}", _Atlas.name, name);
        return sp;
        //return _Atlas.GetSprite(name);
	}
}