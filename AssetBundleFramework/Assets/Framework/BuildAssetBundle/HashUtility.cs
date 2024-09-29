using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;


public static class HashUtility
{
	private static readonly MD5CryptoServiceProvider _provider = new MD5CryptoServiceProvider();
	private static readonly CRC32 crc32 = new CRC32();

	private static string ToString(byte[] hashBytes)
	{
		var sb = new StringBuilder();
		foreach (var t in hashBytes)
			sb.Append(t.ToString("x2"));
		return sb.ToString();
	}

	#region SHA1
	/// <summary>
	/// 获取字符串的Hash值
	/// </summary>
	public static string StringSHA1(string str)
	{
		byte[] buffer = Encoding.UTF8.GetBytes(str);
		return BytesSHA1(buffer);
	}

	/// <summary>
	/// 获取文件的Hash值
	/// </summary>
	public static string FileSHA1(string filePath)
	{
		try
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return StreamSHA1(fs);
			}
		}
		catch (Exception e)
		{
			return string.Empty;
		}
	}

	/// <summary>
	/// 获取数据流的Hash值
	/// </summary>
	public static string StreamSHA1(Stream fs)
	{
		// 说明：创建的是SHA1类的实例，生成的是160位的散列码
		HashAlgorithm hash = HashAlgorithm.Create();
		byte[] hashBytes = hash.ComputeHash(fs);
		return ToString(hashBytes);
	}

	/// <summary>
	/// 获取字节数组的Hash值
	/// </summary>
	public static string BytesSHA1(byte[] buffer)
	{
		// 说明：创建的是SHA1类的实例，生成的是160位的散列码
		HashAlgorithm hash = HashAlgorithm.Create();
		byte[] hashBytes = hash.ComputeHash(buffer);
		return ToString(hashBytes);
	}
	#endregion

	#region MD5
	/// <summary>
	/// 获取文件的MD5值
	/// </summary>
	public static string FileMD5(string filePath)
	{
		try
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return StreamMD5(fs);
			}
		}
		catch (Exception e)
		{
#if UNITY_EDITOR
			UnityEngine.Debug.LogException(e);
#else
			throw new Exception( e.ToString());
#endif

			return string.Empty;
		}
	}

		
	/// <summary>
	/// 获取数据流的MD5值
	/// </summary>
	public static string StreamMD5(Stream stream)
	{
		byte[] hashBytes = _provider.ComputeHash(stream);
		return ToString(hashBytes);
	}

	/// <summary>
	/// 获取字节数组的MD5值
	/// </summary>
	public static string BytesMD5(byte[] buffer)
	{
		byte[] hashBytes = _provider.ComputeHash(buffer);
		return ToString(hashBytes);
	}
	#endregion

	#region CRC32
	public static string GetCRC32Hash(Stream input)
	{
		var data = crc32.ComputeHash(input);
		return ToString(data);
	}

	public static uint GetCrc(byte[] bytes)
	{
		return CRC32.Compute(bytes);
	}

	public static string GetCRC32Hash(byte[] bytes)
	{
		var data = crc32.ComputeHash(bytes);
		return ToString(data);
	}
	public static string GetCRC32Hash(string input)
	{
		var data = crc32.ComputeHash(Encoding.UTF8.GetBytes(input));
		return ToString(data);
	}

	public static bool VerifyCrc32Hash(string input, string hash)
	{
		var comparer = StringComparer.OrdinalIgnoreCase;
		return 0 == comparer.Compare(input, hash);
	}

    #endregion

    #region XOR
		
    #endregion
}

sealed class CRC32 : HashAlgorithm
{
	private const UInt32 DefaultPolynomial = 0xedb88320u;
	private const UInt32 DefaultSeed = 0xffffffffu;

	static UInt32[] _defaultTable;

	readonly UInt32 _seed;
	readonly UInt32[] _table;
	UInt32 _hash;

	public CRC32()
		: this(DefaultPolynomial, DefaultSeed)
	{
	}

	public CRC32(UInt32 polynomial, UInt32 seed)
	{
		if (!BitConverter.IsLittleEndian)
			throw new PlatformNotSupportedException("Not supported on Big Endian processors");

		_table = InitializeTable(polynomial);
		this._seed = _hash = seed;
	}

	public override void Initialize()
	{
		_hash = _seed;
	}

	protected override void HashCore(byte[] array, int ibStart, int cbSize)
	{
		_hash = CalculateHash(_table, _hash, array, ibStart, cbSize);
	}

	protected override byte[] HashFinal()
	{
		var hashBuffer = UInt32ToBigEndianBytes(~_hash);
		HashValue = hashBuffer;
		return hashBuffer;
	}

	public override int HashSize
	{
		get { return 32; }
	}

	public static UInt32 Compute(byte[] buffer)
	{
		return Compute(DefaultSeed, buffer);
	}

	public static UInt32 Compute(UInt32 seed, byte[] buffer)
	{
		return Compute(DefaultPolynomial, seed, buffer);
	}

	public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
	{
		return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
	}

	static UInt32[] InitializeTable(UInt32 polynomial)
	{
		if (polynomial == DefaultPolynomial && _defaultTable != null)
			return _defaultTable;

		var createTable = new UInt32[256];
		for (var i = 0; i < 256; i++)
		{
			var entry = (UInt32)i;
			for (var j = 0; j < 8; j++)
				if ((entry & 1) == 1)
					entry = (entry >> 1) ^ polynomial;
				else
					entry >>= 1;
			createTable[i] = entry;
		}

		if (polynomial == DefaultPolynomial)
			_defaultTable = createTable;

		return createTable;
	}

	static UInt32 CalculateHash(UInt32[] table, UInt32 seed, IList<byte> buffer, int start, int size)
	{
		var hash = seed;
		for (var i = start; i < start + size; i++)
			hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
		return hash;
	}

	static byte[] UInt32ToBigEndianBytes(UInt32 uint32)
	{
		var result = BitConverter.GetBytes(uint32);

		if (BitConverter.IsLittleEndian)
			Array.Reverse(result);

		return result;
	}
}