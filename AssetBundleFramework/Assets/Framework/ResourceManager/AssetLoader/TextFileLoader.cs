using UnityEngine;

public class TextFileLoader : AssetLoaderBase
{
	protected TextAsset _textAsset;

	protected override void OnLoadTaskFinish(UnityEngine.Object asset, bool result)
	{
        _LoadState = AssetLoadState.LoadFailed;
        if (result == true)
		{
            _textAsset = asset as TextAsset;
            if (_textAsset == null)
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
        if (_textAsset != null)
        {
            Resources.UnloadAsset(_textAsset);
            _textAsset = null;
        }
        base.Release();
    }

    public string GetText()
	{
		return _textAsset.text;
    }
    public byte[] GetBytes()
    {
        return _textAsset.bytes;
    }
}