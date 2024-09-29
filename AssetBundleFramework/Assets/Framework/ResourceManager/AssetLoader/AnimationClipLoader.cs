using TMPro;
using UnityEngine;

public class AnimationClipLoader : AssetLoaderBase
{
	protected AnimationClip _AnimationClip;

	protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
		{
            _AnimationClip = asset as AnimationClip;
            if (_AnimationClip == null)
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
        if (_AnimationClip != null)
        {
            Resources.UnloadAsset(_AnimationClip);
            _AnimationClip = null;
        }
        base.Release();
    }

    public AnimationClip GetAnimationClip()
    {
        return _AnimationClip;
    }
  
}