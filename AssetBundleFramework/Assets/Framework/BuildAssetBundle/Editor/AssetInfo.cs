﻿//--------------------------------------------------

//--------------------------------------------------

using UnityEditor;

	/// <summary>
	/// 资源信息类
	/// </summary>
	public class AssetInfo
	{
		public string AssetPath { private set; get; }
		public bool IsCollectAsset { private set; get; }
		public bool IsSceneAsset { private set; get; }
		public bool IsVideoAsset { private set; get; }
		
		/// <summary>
		/// 被依赖次数
		/// </summary>
		public int DependCount = 0;

		/// <summary>
		/// AssetBundle标签
		/// </summary>
		public string AssetBundleLabel = null;

		/// <summary>
		/// AssetBundle变体
		/// </summary>
		public string AssetBundleVariant = null;

		/// <summary>
		/// AssetBundle存放位置
		/// </summary>
		public EBundlePos bundlePos = EBundlePos.buildin;

		public string ReadableLabel = "undefined";

		public EEncryptMethod EncryptMethod;

		public AssetInfo(string assetPath)
		{
			AssetPath = assetPath;
			IsCollectAsset = CollectionSettingData.IsCollectAsset(assetPath);
			IsSceneAsset = AssetDatabase.GetMainAssetTypeAtPath(assetPath) == typeof(SceneAsset);
			IsVideoAsset = AssetDatabase.GetMainAssetTypeAtPath(assetPath) == typeof(UnityEngine.Video.VideoClip);
		}
	}
