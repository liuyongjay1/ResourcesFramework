using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 补丁清单文件
/// </summary>
public class PatchManifest
{
	private bool _isParse = false;

    public int Version { private set; get; }

    /// <summary>
    /// 所有打包文件列表
    /// </summary>
    public readonly Dictionary<string, PatchElement> Elements = new Dictionary<string, PatchElement>();

	public PatchManifest()
	{
	}

	/// <summary>
	/// 解析数据
	/// </summary>
	public void Parse(byte[] data)
	{
		using (var ms = new MemoryStream(data))
        {
			using(var br = new BinaryReader(ms))
            {
				Parse(br);
            }
        }
	}

	public void ParseFile(string filePath)
    {
		using (var fs = File.OpenRead(filePath))
		{
			using (var br = new BinaryReader(fs))
			{
				Parse(br);
			}
		}
	}

	/// <summary>
	/// 解析数据
	/// </summary>
	public void Parse(BinaryReader br)
	{
		if (br == null)
			throw new Exception("Fatal error : Param is null.");
		if (_isParse)
			throw new Exception("Fatal error : Package is already parse.");

		_isParse = true;

		// 读取版本号			
		Version = br.ReadInt32();

		int fileCount = br.ReadInt32();
		// 读取所有Bundle的数据
		for(var i = 0; i < fileCount; i++)
		{
			var ele = PatchElement.Deserialize(br);
			if (Elements.ContainsKey(ele.Name))
				throw new Exception($"Fatal error : has same pack file : {ele.Name}");
			Elements.Add(ele.Name, ele);
		}
	}
}
