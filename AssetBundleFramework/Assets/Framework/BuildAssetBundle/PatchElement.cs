using System.IO;
public class PatchElement
{
	//文件名称
	public string Name { private set; get; }
	//文件MD5
	public string MD5 { private set; get; }
	//文件版本
	public int Version { private set; get; }
	//文件大小
	public long FileSize { private set; get; }
	//构建类型,[buildin 在安装包中下载][ingame  游戏中下载]
	public string Tag { private set; get; }
	//已下载且已部署完成
	public bool SkipDownload;
	//在临时文件夹里下载完成
	public bool DownloadFinish;
	public PatchElement(string name, string md5, int version, long fileSize, string tag, bool isInit = false)
	{
		Name = name;
		MD5 = md5;
		Version = version;
		FileSize = fileSize;
		Tag = tag;
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(Name);
		bw.Write(MD5);
		bw.Write(FileSize);
		bw.Write(Version);
        bw.Write(Tag);
	}

	public static PatchElement Deserialize(BinaryReader br)
    {
		var name = br.ReadString();
		var md5 = br.ReadString();
        var fileSize = br.ReadInt64();
		var version = br.ReadInt32();
		var tag = br.ReadString();
		return new PatchElement(name, md5, version, fileSize, tag);
    }
}
