using UnityEngine;
using System.Collections;

public class MaterialLoader : AssetLoaderBase
{
    private Material _mat;

	protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
        {
            _mat = asset as Material;
            if (_mat == null)
            {
                LogManager.LogError(string.Format("Failed to instantiate Material : {0}", _resEditorPath));
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

    public Material GetMaterial()
	{
		return _mat;	
	}
    public override void Release()
    {
        if (_mat != null)
        {
            Resources.UnloadAsset(_mat);
            _mat = null;
        }
        base.Release();
    }
}
