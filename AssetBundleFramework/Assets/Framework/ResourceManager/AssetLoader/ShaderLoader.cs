
using UnityEngine;

public class ShaderLoader : AssetLoaderBase
{
	public Shader _Shader;
	protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
		{
			_Shader = asset as Shader;
			if (_Shader == null)
			{
				LogManager.LogError(string.Format("Failed to instantiate _Shader : {0}", _resEditorPath));
				_prepareCallback?.Invoke(this, false);
				return;
			}
			_LoadState = AssetLoadState.LoadSuccess;
			_prepareCallback?.Invoke(this, true);
		}
		else
		{
            LogManager.LogError(string.Format("Failed to Load _Shader : {0}", _resEditorPath));
            _prepareCallback?.Invoke(this, false);
        }

		base.OnLoadTaskFinish(asset, result);
	}

    public Shader GetShader()
	{
		if (_Shader == null)
		{
			LogManager.LogError("Texture is null,resPath: " + _resEditorPath);
			return null;
		}
		return _Shader;
	}
	
    public override void Release()
    {
        if (_Shader != null)
        {
			Resources.UnloadAsset(_Shader);
			_Shader = null;
        }
        base.Release();
    }
}