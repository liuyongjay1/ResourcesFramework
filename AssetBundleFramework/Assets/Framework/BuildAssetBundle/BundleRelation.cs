using System;
using UnityEngine;
using System.Collections.Generic;

    /// <summary>
    /// design based on Google.Android.AppBundle AssetPackDeliveryMode
    /// </summary>
    [Serializable]
    public enum EAssetDeliveryMode
    {
        // ===> AssetPackDeliveryMode.InstallTime
        Main = 1,
        // ====> AssetPackDeliveryMode.FastFollow
        FastFollow = 2,
        // ====> AssetPackDeliveryMode.OnDemand
        OnDemand = 3
    }

    /// <summary>
    /// AssetBundle打包位置
    /// </summary>
    [Serializable]
    public enum EBundlePos
    {
        /// 启动时热更
        buildin,
        /// 游戏内热更
        ingame,
    }

    [Serializable]
    public enum EEncryptMethod
    {
        None = 0,
        Quick, //padding header
        Simple, 
        X, //xor
        QuickX //partial xor
    }

    [Serializable]
    public struct AssetRef
    {
        public string Name;

        public int BundleId;

        public int DirIdx;
    }

    [Serializable]
    public enum ELoadMode
    {
        None,
        LoadFromStreaming,
        LoadFromCache,
        LoadFromRemote,
    }
     

    [Serializable]
    public struct BundleInfo
    {
        public string Name;

        public int[] Deps;

        public string Hash;

        public EEncryptMethod EncryptMethod;
    }
    
    public class BundleRelation : ScriptableObject
    {
        public string[] Dirs = new string[0];
        public AssetRef[] AssetRefs = new AssetRef[0];
        public BundleInfo[] Bundles = new BundleInfo[0];
    }
