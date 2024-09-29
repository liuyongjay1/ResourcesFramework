﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


public class VariantCollector
{
	private readonly Dictionary<string, string> _variantRuleCollection = new Dictionary<string, string>();
	private readonly Dictionary<string, string> _cacheFiles = new Dictionary<string, string>(1000);
	private static Regex _regex = new Regex(@"(\.[a-z]{2})([^a-z]|$)");
	/// <summary>
	/// 注册变体规则
	/// </summary>
	/// <param name="variantGroup">变体组</param>
	/// <param name="targetVariant">目标变体</param>
	public void RegisterVariantRule(List<string> variantGroup, string targetVariant)
	{
		if (variantGroup == null || variantGroup.Count == 0)
			throw new Exception("VariantGroup is null or empty.");
		if (string.IsNullOrEmpty(targetVariant))
			throw new Exception("TargetVariant is invalid.");

		// 需要包含点前缀
		for (int i=0; i< variantGroup.Count; i++)
		{
			string variant = variantGroup[i];
			if (variant.Contains(".") == false)
				variantGroup[i] = $".{variant}";
		}
		if (targetVariant.Contains(".") == false)
			targetVariant = $".{targetVariant}";

		// 目标变体需要在变体组列表里
		if (variantGroup.Contains(targetVariant) == false)
			throw new Exception($"Variant group not contains target variant : {targetVariant} ");

		// 转换为小写形式
		targetVariant = targetVariant.ToLower();
		for (int i = 0; i < variantGroup.Count; i++)
		{
			variantGroup[i] = variantGroup[i].ToLower();
		}

		foreach (var variant in variantGroup)
		{
			if (variant == targetVariant)
				continue;
			if (_variantRuleCollection.ContainsKey(variant) == false)
				_variantRuleCollection.Add(variant, targetVariant);
			//else
				//Debug.LogWarning($"Variant key {variant} is already existed.");
		}
	}

	/// <summary>
	/// 尝试获取注册的变体资源清单路径
	/// </summary>
	/// <returns>如果没有注册，返回原路径</returns>
	public string TryGetVariantManifestPath(string manifestPath)
	{
        // 如果不是变体资源
        if (manifestPath.EndsWith(PatchDefine.AssetBundleDefaultVariantWithPoint))
            return manifestPath;

        if (_cacheFiles.ContainsKey(manifestPath))
			return _cacheFiles[manifestPath];

		// 获取变体资源格式
		string targetManifestPath = manifestPath;
		var match = _regex.Match(manifestPath);
		if (match.Success)
		{
			var curVariant = match.Groups[0].Value.Substring(0,3);
            if (_variantRuleCollection.ContainsKey(curVariant))
            {
				string targetExtension = _variantRuleCollection[curVariant];
				targetManifestPath = targetManifestPath.Replace(curVariant, targetExtension);
			}
		}

		// 添加到缓存列表
		_cacheFiles.Add(manifestPath, targetManifestPath);
		return targetManifestPath;
	}

	public static string GetVariantByAssetPath(string path)
	{
		var match = _regex.Match(path.ToLower());
		if (match.Success)
		{
			return match.Groups[0].Value.Substring(0, 3).ToUpper();
		}
		return "";
	}
}
