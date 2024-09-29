//**************************************************
// Copyright©2018 何冠峰
// Licensed under the MIT license
//**************************************************
using UnityEngine;
using UnityEngine.Video;

public class VideoLoader : AssetLoaderBase
{
    private VideoClip _VideoClip;

	protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
        {
            _VideoClip = asset as VideoClip;
            if (_VideoClip == null)
            {
                LogManager.LogError(string.Format("Failed to instantiate TextFile : {0}", _resEditorPath));
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
    public override void Release()
    {
        if (_VideoClip != null)
        {
            Resources.UnloadAsset(_VideoClip);
            _VideoClip = null;
        }
        base.Release();
    }
}