using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleManager : Singleton<AssetBundleManager>
{
    //bundle关系Asset
    private BundleRelationManager bundleRelationManager = new BundleRelationManager();
    private int streamingPatchVersion;

    //当前使用哪个清单文件
    private PatchManifest currentUseManifest = new PatchManifest();
    #region 对外接口
  

    //根据AB的相对路径，获取加载全路径
    public string GetBundleLoadPath(string path)
    {
        string fullPath = "";
        PatchElement element = null;
        if (currentUseManifest.Elements.TryGetValue(path, out element))
        {
            //从清单里拿到这个ab的版本，ab可能从StreamingAsset中加载，也可能从Persistant中加载
            if (element.Version <= streamingPatchVersion)
                fullPath = PathTool.MakeStreamingLoadPath(path);
            else
                fullPath = PathTool.MakePersistentLoadPath(path);
        }
        else
            LogManager.LogError("Manifest Not Exist Key: " + path);
        return fullPath;
    }

    public string[] GetBundleDepends(string path)
    {
        return bundleRelationManager.GetDependsByPath(path);
    }

    #endregion// 对外接口

    #region 启动流程
    //设置当前使用的是本地还是Web文件清单
    public void SetPatchInfo(PatchManifest curPatchManifest,int streamingVersion)
    {
        currentUseManifest = curPatchManifest;
        streamingPatchVersion = streamingVersion;
    }

    //加载Bundle关系文件，AB包之间的依赖都在这了。
    public void LoadBundleRelation(bool isPersistant)
    {
        bundleRelationManager = new BundleRelationManager();
        var bundleRelationBundleName = PathTool.GetBundleRelationName();
        string loadPath = "";
        if (isPersistant)
            loadPath = PathTool.MakePersistentLoadPath(bundleRelationBundleName);
        else
            loadPath = PathTool.MakeStreamingLoadPath(bundleRelationBundleName);

        AssetBundle bundle = AssetBundle.LoadFromFile(loadPath);
        if (bundle == null)
            LogManager.LogError("Cannot load BundleRelation bundle");

        var relation = bundle.LoadAsset<BundleRelation>("BundleRelation.asset");
        if (relation == null)
            LogManager.LogError("Cannot load Assets/BundleRelation.asset");
        bundleRelationManager.SetBundleRelation(relation);
        bundle.Unload(false);
    }

    #endregion//启动流程

}
