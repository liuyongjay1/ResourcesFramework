/*--------------------------------------------------
				AssetBundleFramework
--------------------------------------------------
AB包收集规则文件
Elements
	收集规则
InGames
	如果收集规则的路径里包含InGame的路径，则出包后的清单文件会标记为ingame下载模式
--------------------------------------------------*/
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollectionSetting : ScriptableObject
{
    [Serializable]
	public class Wrapper
	{
		public string FolderPath = string.Empty;
		public EFolderPackRule PackRule = EFolderPackRule.Collect;
		public EBundleLabelRule LabelRule = EBundleLabelRule.LabelByFilePath;
		public EEncryptMethod EncryptRule = EEncryptMethod.Quick;
		public EBundlePos BundlePos = EBundlePos.buildin;
	}

	/// <summary>
	/// 打包路径列表
	/// </summary>
	[SerializeField]
	public List<Wrapper> Elements = new List<Wrapper>();

	/// <summary>
	/// 文件夹打包规则
	/// </summary>
	[Serializable]
	public enum EFolderPackRule
	{
		/// 收集该文件夹
		Collect,
		/// 忽略该文件夹
		Ignore,
		/// 仅在被依赖的时候收集
		Passive
	}

	/// <summary>
	/// AssetBundle标签规则
	/// </summary>
	[Serializable]
	public enum EBundleLabelRule
	{
		None,
		/// 以文件路径命名
		LabelByFilePath,
		/// 以文件名称命名
		LabelByFileName,
		/// 以文件夹路径命名（该文件夹下所有资源被打到一个AssetBundle文件里）
		LabelByFolderPath,
		/// 文件大小小于1M的，以文件夹路径命名（该文件夹下所有资源被打到一个AssetBundle文件里）
		LabelByFolderPathExceptBig,
		/// 以文件夹名称命名（该文件夹下所有资源被打到一个AssetBundle文件里）
		LabelByFolderName,
		/// 以根节点路径命名 (该文件夹下所有资源打到一个AB，包括文件夹）
		LabelByRootFolderPath,
	}

    #region IngameDownload
    [SerializeField]
    public List<string> InGames = new List<string>();
    #endregion
}
