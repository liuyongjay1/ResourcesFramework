using UnityEngine;
using System.Collections;
using System;

public class ConfigLoader : AssetLoaderBase
{
    private UnityEngine.Object _target;
	protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
        {
            _target = asset;
            if (_target == null)
            {
                LogManager.LogError(string.Format("Failed to instantiate UnityEngine.Object : {0}", _resEditorPath));
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

    public UnityEngine.Object GetObject()
	{
		return _target;	
	}
    public override void Release()
    {
        if (_target != null)
        {
            Resources.UnloadAsset(_target);
            _target = null;
        }
        base.Release();
    }
}
