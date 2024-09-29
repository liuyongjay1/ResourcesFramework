using UnityEngine;

public class AudioLoader : AssetLoaderBase
{
    private AudioClip _AudioClip;

    protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
        {
            _AudioClip = asset as AudioClip;
            if (_AudioClip == null)
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

    public AudioClip GetAudioClip()
    {
        return _AudioClip;
    }
    public override void Release()
    {
        if (_AudioClip != null)
        {
            //UnityEngine.Object.Destroy(_AudioClip);
            //_AudioClip = null;
        }
        base.Release();
    }
}