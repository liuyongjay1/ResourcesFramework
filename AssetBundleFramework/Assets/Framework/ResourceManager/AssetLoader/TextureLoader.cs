
using UnityEngine;

public class TextureLoader : AssetLoaderBase
{
	public Texture _Image;
	protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
		{
			_Image = asset as Texture;
			if (_Image == null)
			{
				LogManager.LogError(string.Format("Failed to instantiate _Image : {0}", _resEditorPath));
				_prepareCallback?.Invoke(this, false);
				return;
			}
			_LoadState = AssetLoadState.LoadSuccess;
			_prepareCallback?.Invoke(this, true);
		}
		else
		{
            _prepareCallback?.Invoke(this, false);
        }

		base.OnLoadTaskFinish(asset, result);
	}

    public Texture GetTexture()
	{
		if (_Image == null)
		{
			LogManager.LogError("Texture is null,resPath: " + _resEditorPath);
			return null;
		}
		return _Image;
	}
	
    public override void Release()
    {
        if (_Image != null)
        {
			Resources.UnloadAsset(_Image);
			_Image = null;
        }
        base.Release();
    }
}