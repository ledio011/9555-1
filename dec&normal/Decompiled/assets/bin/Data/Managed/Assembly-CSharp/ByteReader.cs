using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class ByteReader
{
	// Token: 0x060002B4 RID: 692 RVA: 0x00012E94 File Offset: 0x00011094
	public ByteReader(byte[] bytes)
	{
		this.mBuffer = bytes;
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00012EA4 File Offset: 0x000110A4
	public ByteReader(TextAsset asset)
	{
		this.mBuffer = asset.bytes;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00012EC4 File Offset: 0x000110C4
	public static ByteReader Open(string path)
	{
		FileStream fileStream = File.OpenRead(path);
		if (fileStream != null)
		{
			fileStream.Seek(0L, 2);
			byte[] array = new byte[fileStream.Position];
			fileStream.Seek(0L, 0);
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return new ByteReader(array);
		}
		return null;
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060002B8 RID: 696 RVA: 0x00012F1C File Offset: 0x0001111C
	public bool canRead
	{
		get
		{
			return this.mBuffer != null && this.mOffset < this.mBuffer.Length;
		}
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00012F3C File Offset: 0x0001113C
	private static string ReadLine(byte[] buffer, int start, int count)
	{
		return Encoding.UTF8.GetString(buffer, start, count);
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00012F4C File Offset: 0x0001114C
	public string ReadLine()
	{
		return this.ReadLine(true);
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00012F58 File Offset: 0x00011158
	public string ReadLine(bool skipEmptyLines)
	{
		int num = this.mBuffer.Length;
		if (skipEmptyLines)
		{
			while (this.mOffset < num && this.mBuffer[this.mOffset] < 32)
			{
				this.mOffset++;
			}
		}
		int i = this.mOffset;
		if (i < num)
		{
			while (i < num)
			{
				int num2 = (int)this.mBuffer[i++];
				if (num2 == 10 || num2 == 13)
				{
					IL_87:
					string result = ByteReader.ReadLine(this.mBuffer, this.mOffset, i - this.mOffset - 1);
					this.mOffset = i;
					return result;
				}
			}
			i++;
			goto IL_87;
		}
		this.mOffset = num;
		return null;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00013020 File Offset: 0x00011220
	public Dictionary<string, string> ReadDictionary()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		char[] array = new char[]
		{
			'='
		};
		while (this.canRead)
		{
			string text = this.ReadLine();
			if (text == null)
			{
				break;
			}
			if (!text.StartsWith("//"))
			{
				string[] array2 = text.Split(array, 2, 1);
				if (array2.Length == 2)
				{
					string text2 = array2[0].Trim();
					string text3 = array2[1].Trim().Replace("\\n", "\n");
					dictionary[text2] = text3;
				}
			}
		}
		return dictionary;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x000130B8 File Offset: 0x000112B8
	public BetterList<string> ReadCSV()
	{
		ByteReader.mTemp.Clear();
		string text = string.Empty;
		bool flag = false;
		int num = 0;
		while (this.canRead)
		{
			if (flag)
			{
				string text2 = this.ReadLine(false);
				if (text2 == null)
				{
					return null;
				}
				text2 = text2.Replace("\\n", "\n");
				text = text + "\n" + text2;
			}
			else
			{
				text = this.ReadLine(true);
				if (text == null)
				{
					return null;
				}
				text = text.Replace("\\n", "\n");
				num = 0;
			}
			int i = num;
			int length = text.Length;
			while (i < length)
			{
				char c = text.get_Chars(i);
				if (c == ',')
				{
					if (!flag)
					{
						ByteReader.mTemp.Add(text.Substring(num, i - num));
						num = i + 1;
					}
				}
				else if (c == '"')
				{
					if (flag)
					{
						if (i + 1 >= length)
						{
							ByteReader.mTemp.Add(text.Substring(num, i - num).Replace("\"\"", "\""));
							return ByteReader.mTemp;
						}
						if (text.get_Chars(i + 1) != '"')
						{
							ByteReader.mTemp.Add(text.Substring(num, i - num));
							flag = false;
							if (text.get_Chars(i + 1) == ',')
							{
								i++;
								num = i + 1;
							}
						}
						else
						{
							i++;
						}
					}
					else
					{
						num = i + 1;
						flag = true;
					}
				}
				i++;
			}
			if (num < text.Length)
			{
				if (flag)
				{
					continue;
				}
				ByteReader.mTemp.Add(text.Substring(num, text.Length - num));
			}
			return ByteReader.mTemp;
		}
		return null;
	}

	// Token: 0x04000306 RID: 774
	private byte[] mBuffer;

	// Token: 0x04000307 RID: 775
	private int mOffset;

	// Token: 0x04000308 RID: 776
	private static BetterList<string> mTemp = new BetterList<string>();
}
