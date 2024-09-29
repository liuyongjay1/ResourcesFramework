using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
/// <summary>
/// Í¼¼¯Í¼Æ¬
/// </summary>
[RequireComponent(typeof(RectTransform), typeof(Image))]
public class UISpriteCom : MonoBehaviour
{
    [SerializeField]
    public string SpriteName;
    [SerializeField]
    private SpriteAtlas Atlas;

    private Image UI_Image;
    public string AtlasPath;
 
    // Start is called before the first frame update
    void Start()
    {
        if (UI_Image == null)
            UI_Image = gameObject.GetComponent<Image>();
        if (UI_Image == null)
        {
            LogManager.LogError("GetComponent<Image> is null,gameObject: " + gameObject.name);
            return;
        }
        LoadAssetUtility.LoadAtlasAsset(AtlasPath, LoadAtlasCallBack, false);
    }

    private void LoadAtlasCallBack(AssetLoaderBase loader, bool state)
    {
        if (state == true)
        {
            AtlasLoader atlasLoader = (AtlasLoader)loader;
            UI_Image.sprite = Atlas.GetSprite(SpriteName);
        }
    }
}
