using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FontLoader : AssetLoaderBase
{
	protected TMP_FontAsset _FontAsset;

	protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
        {
            _FontAsset = asset as TMP_FontAsset;
            if (_FontAsset == null)
            {
                LogManager.LogError(string.Format("Failed to instantiate TextFile : {0}", _resEditorPath));
                _prepareCallback?.Invoke(this, false);
                return;
            }
            _LoadState = AssetLoadState.LoadSuccess;
            _prepareCallback?.Invoke(this, true);
        }
        else
        {
            _prepareCallback?.Invoke(this, false);
            LogManager.LogError(string.Format(" FontLoader OnLoadTaskFinish Failed,TextFile : {0}", _resEditorPath));

        }

        base.OnLoadTaskFinish(asset, result);
    }

    public override void Release()
    {
        if (_FontAsset != null)
        {
            Resources.UnloadAsset(_FontAsset);
            _FontAsset = null;
        }
        base.Release();
    }

    public TMP_FontAsset GetFont()
    {
        return _FontAsset;
    }
  
}