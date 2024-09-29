using UnityEngine;

[CreateAssetMenu]
public class GameSetting : ScriptableObject
{
    public static GameSetting Instance;
    public bool AssetbundleMode;
    public bool LoadBundleAsync;
    public bool LoadAssetAsync;

}

