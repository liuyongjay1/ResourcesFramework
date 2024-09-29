using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BundleRelationManager
{
    private BundleRelation _BundleRelation;
    private Dictionary<string, int> _Name_ID_Map = new Dictionary<string, int>();

    protected struct LoadPathCacheItem
    {
        public int BundleId;
        public string LoadPath;
    }

    //bundId => actual load path
    protected Dictionary<int, LoadPathCacheItem> _loadPathCache = new Dictionary<int, LoadPathCacheItem>(200);

    public void SetBundleRelation(BundleRelation relation)
    {
        _BundleRelation = relation;
        for (int i = 0; i < relation.Bundles.Length; i++)
        {
            _Name_ID_Map.Add(relation.Bundles[i].Name, i);
        }
        //for (var i = 0; i < relation.Dirs.Length; i++)
        //{
        //    var dir = relation.Dirs[i];
        //    _Dir_ID_Map[dir] = i;
        //}

        //for (var i = 0; i < relation.Bundles.Length; i++)
        //{
        //    var info = relation.Bundles[i];
        //    _Name_ID_Map[info.Name] = i;
        //}

        //foreach (var assetRef in relation.AssetRefs)
        //{
        //    var path = string.Format("{0}/{1}", relation.Dirs[assetRef.DirIdx], assetRef.Name);
        //    // MotionLog.Log(ELogLevel.Log, $"path is {path}");
        //    if (!_assetToBundleMap.TryGetValue(assetRef.DirIdx, out var assetNameToBundleId))
        //    {
        //        assetNameToBundleId = new Dictionary<string, int>();
        //        _assetToBundleMap.Add(assetRef.DirIdx, assetNameToBundleId);
        //    }
        //    assetNameToBundleId.Add(assetRef.Name, assetRef.BundleId);
        //}



    }

    //public string GetAssetBundleLoadPath(string manifestPath)
    //{
    //    if (_PatchManifest.Elements.TryGetValue(manifestPath, out var appElement))
    //    {
    //        return string.Format("{0}/{1}", Application.streamingAssetsPath, manifestPath);
    //    }
    //    else
    //    {
    //        LogManager.LogError("manifestPath is null，manifestPath" + manifestPath);
    //    }
    //    return "";
    //}

    //public int[] GetAllDependenciesById(int bundleId)
    //{
    //    return _BundleRelation.Bundles[bundleId].Deps;
    //}

    public int GetBundleIdByName(string name)
    {
        if (_Name_ID_Map.TryGetValue(name, out var id))
            return id;
        else
        LogManager.LogError($"cannot find bundle for {name}");
        return 0;
    }

    //public BundleInfo GetBundleInfoById(int id)
    //{
    //    return _BundleRelation.Bundles[id];
    //}

    //public string GetBundleNameById(int id)
    //{
    //    return _BundleRelation.Bundles[id].Name;
    //}
    //public int GetBundleId(string bundlePath)
    //{
    //    int id = -1;
    //    if (!_Name_ID_Map.TryGetValue(bundlePath, out id))
    //    {
    //        LogManager.LogError("GetBundleId Error,bundlePath: " + bundlePath);
    //    }
    //    return id;
    //}

    public string[] GetDependsByPath(string path)
    {
        int bundleId = -1;
        if (_Name_ID_Map.ContainsKey(path))
            bundleId = _Name_ID_Map[path];
        else
            LogManager.LogError("_Name_ID_Map does not contains key：" + path);
        int[] allDependId = _BundleRelation.Bundles[bundleId].Deps;

        string[] allDependsPath = new string[allDependId.Length];

        for (int i = 0; i < allDependId.Length; i++)
        {
            int depBundleId = allDependId[i];
            allDependsPath[i] = _BundleRelation.Bundles[depBundleId].Name;
        }
        return allDependsPath;
    }

    //public int GetBundleId(string location)
    //{
    //    if (_assetToBundleIdCache.TryGetValue(location, out var cachedId))
    //    {
    //        return cachedId;
    //    }
    //    string p = string.Format("{0}/{1}.unity3d",Application.streamingAssetsPath, Path.GetDirectoryName(location)).Replace("\\", "/");
    //    if (!_Dir_ID_Map.TryGetValue(p, out var dirId))
    //         LogManager.LogError($"cannot found Dir Id for request {location}");
    //    if (!_assetToBundleMap.TryGetValue(dirId, out var assetNameToBundleId))
    //         LogManager.LogError($"cannot found assetNameMap for dirId:{dirId}");
    //    var assetName = Path.GetFileName(location);
    //    if (!assetNameToBundleId.TryGetValue(assetName, out var bundleId))
    //         LogManager.LogError($"cannot found bundleId for {assetName} in dir:{p}");

    //    _assetToBundleIdCache.Add(location, bundleId);
    //    return bundleId;
    //}

    //public string GetBundleLoadPathById(ref int bundleId, string variant)
    //{
    //    if (_loadPathCache.TryGetValue(bundleId, out var cachedItem))
    //    {
    //        bundleId = cachedItem.BundleId;
    //        return cachedItem.LoadPath;
    //    }
    //    string inPath = _BundleRelation.Bundles[bundleId].Name;
    //    var outPath = GetAssetBundleLoadPath(inPath);
    //    int originId = bundleId;
    //    bundleId = GetBundleIdByName(inPath);
    //    _loadPathCache.Add(originId, new LoadPathCacheItem { BundleId = bundleId, LoadPath = outPath });
    //    return outPath;
    //}

    //public AssetLocation GetLocation(string path)
    //{
    //    return path.StartsWith(Application.streamingAssetsPath, StringComparison.OrdinalIgnoreCase) ?
    //        AssetLocation.App : AssetLocation.Sandbox;
    //}


    //public AssetBundle LoadAssetBundle(string location)
    //{
    //    int bundleId = GetBundleId(location);
    //    BundleInfo bundleInfo = GetBundleInfoById(bundleId);
    //    string loadPath = GetBundleLoadPathById(ref bundleId, PatchDefine.AssetBundleDefaultVariant);
    //    ulong offset = 0;
    //    //MotionLog.Log(ELogLevel.Log, $"<color=#00ffff>asset load from file:{loadPath}</color>");
    //    AssetBundle bundle = null;
    //    if (bundleInfo.EncryptMethod == EEncryptMethod.None)
    //    {
    //        bundle = AssetBundle.LoadFromFile(loadPath, 0, offset);
    //    }
    //    else if (bundleInfo.EncryptMethod == EEncryptMethod.Quick)
    //    {
    //        //offset += AssetSystem.DecryptServices.GetDecryptOffset(bundleInfo.Hash);
    //        //bundle = AssetBundle.LoadFromFile(loadPath, 0, offset);
    //    }
    //    //else if (bundleInfo.EncryptMethod == EEncryptMethod.Simple)
    //    //{
    //    //    LogManager.LogError("LoadBytesFromFileSystem");
    //    //    //AssetBundle.LoadFromStream	
    //    //}
    //    else
    //    {
    //        LogManager.LogError($"Bundle Content Type {bundleInfo.EncryptMethod.ToString()} is not supported yet");
    //    }
    //    return bundle;
    //}
}
