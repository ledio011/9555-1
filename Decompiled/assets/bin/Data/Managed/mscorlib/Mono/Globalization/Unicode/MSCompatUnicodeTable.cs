using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000034 RID: 52
	internal class MSCompatUnicodeTable
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003CE8 File Offset: 0x00001EE8
		unsafe static MSCompatUnicodeTable()
		{
			IntPtr resource = MSCompatUnicodeTable.GetResource("collation.core.bin");
			if (resource == IntPtr.Zero)
			{
				return;
			}
			byte* ptr = (byte*)((void*)resource);
			resource = MSCompatUnicodeTable.GetResource("collation.tailoring.bin");
			if (resource == IntPtr.Zero)
			{
				return;
			}
			byte* ptr2 = (byte*)((void*)resource);
			if (ptr == null || ptr2 == null)
			{
				return;
			}
			if (*ptr != 3 || *ptr2 != 3)
			{
				return;
			}
			uint num = 1U;
			uint num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.ignorableFlags = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.categories = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level1 = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level2 = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level3 = ptr + num;
			num += num2;
			num = 1U;
			uint num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
			num += 4U;
			MSCompatUnicodeTable.tailoringInfos = new TailoringInfo[num3];
			int num4 = 0;
			while ((long)num4 < (long)((ulong)num3))
			{
				int lcid = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				int tailoringIndex = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				int tailoringCount = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				TailoringInfo tailoringInfo = new TailoringInfo(lcid, tailoringIndex, tailoringCount, ptr2[(UIntPtr)(num++)] != 0);
				MSCompatUnicodeTable.tailoringInfos[num4] = tailoringInfo;
				num4++;
			}
			num += 2U;
			num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
			num += 4U;
			MSCompatUnicodeTable.tailoringArr = new char[num3];
			int num5 = 0;
			while ((long)num5 < (long)((ulong)num3))
			{
				MSCompatUnicodeTable.tailoringArr[num5] = (char)((int)ptr2[num] + ((int)ptr2[num + 1U] << 8));
				num5++;
				num += 2U;
			}
			MSCompatUnicodeTable.isReady = true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003ECC File Offset: 0x000020CC
		public static TailoringInfo GetTailoringInfo(int lcid)
		{
			for (int i = 0; i < MSCompatUnicodeTable.tailoringInfos.Length; i++)
			{
				if (MSCompatUnicodeTable.tailoringInfos[i].LCID == lcid)
				{
					return MSCompatUnicodeTable.tailoringInfos[i];
				}
			}
			return null;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003F0C File Offset: 0x0000210C
		public unsafe static void BuildTailoringTables(CultureInfo culture, TailoringInfo t, ref Contraction[] contractions, ref Level2Map[] diacriticals)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			fixed (char* ptr = ref (MSCompatUnicodeTable.tailoringArr != null && MSCompatUnicodeTable.tailoringArr.Length != 0) ? ref MSCompatUnicodeTable.tailoringArr[0] : ref *null)
			{
				int i = t.TailoringIndex;
				int num = i + t.TailoringCount;
				while (i < num)
				{
					int num2 = i + 1;
					switch (ptr[i])
					{
					case '\u0001':
					{
						i++;
						while (ptr[num2] != '\0')
						{
							num2++;
						}
						char[] array = new char[num2 - i];
						Marshal.Copy((IntPtr)((void*)(ptr + i)), array, 0, num2 - i);
						byte[] array2 = new byte[4];
						for (int j = 0; j < 4; j++)
						{
							array2[j] = (byte)ptr[num2 + 1 + j];
						}
						arrayList.Add(new Contraction(array, null, array2));
						i = num2 + 6;
						break;
					}
					case '\u0002':
						arrayList2.Add(new Level2Map((byte)ptr[i + 1], (byte)ptr[i + 2]));
						i += 3;
						break;
					case '\u0003':
					{
						i++;
						while (ptr[num2] != '\0')
						{
							num2++;
						}
						char[] array = new char[num2 - i];
						Marshal.Copy((IntPtr)((void*)(ptr + i)), array, 0, num2 - i);
						num2++;
						int num3 = num2;
						while (ptr[num3] != '\0')
						{
							num3++;
						}
						string replacement = new string(ptr, num2, num3 - num2);
						arrayList.Add(new Contraction(array, replacement, null));
						i = num3 + 1;
						break;
					}
					default:
						throw new NotImplementedException(string.Format("Mono INTERNAL ERROR (Should not happen): Collation tailoring table is broken for culture {0} ({1}) at 0x{2:X}", culture.LCID, culture.Name, i));
					}
				}
			}
			arrayList.Sort(ContractionComparer.Instance);
			arrayList2.Sort(Level2MapComparer.Instance);
			contractions = (arrayList.ToArray(typeof(Contraction)) as Contraction[]);
			diacriticals = (arrayList2.ToArray(typeof(Level2Map)) as Level2Map[]);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000413C File Offset: 0x0000233C
		private unsafe static void SetCJKReferences(string name, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer lv2Indexer, ref byte* lv2Table)
		{
			switch (name)
			{
			case "zh-CHS":
				catTable = MSCompatUnicodeTable.cjkCHScategory;
				lv1Table = MSCompatUnicodeTable.cjkCHSlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.CjkCHS;
				break;
			case "zh-CHT":
				catTable = MSCompatUnicodeTable.cjkCHTcategory;
				lv1Table = MSCompatUnicodeTable.cjkCHTlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				break;
			case "ja":
				catTable = MSCompatUnicodeTable.cjkJAcategory;
				lv1Table = MSCompatUnicodeTable.cjkJAlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				break;
			case "ko":
				catTable = MSCompatUnicodeTable.cjkKOcategory;
				lv1Table = MSCompatUnicodeTable.cjkKOlv1;
				lv2Table = MSCompatUnicodeTable.cjkKOlv2;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				lv2Indexer = MSCompatUnicodeTableUtil.Cjk;
				break;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004240 File Offset: 0x00002440
		public unsafe static byte Category(int cp)
		{
			return MSCompatUnicodeTable.categories[MSCompatUnicodeTableUtil.Category.ToIndex(cp)];
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004254 File Offset: 0x00002454
		public unsafe static byte Level1(int cp)
		{
			return MSCompatUnicodeTable.level1[MSCompatUnicodeTableUtil.Level1.ToIndex(cp)];
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004268 File Offset: 0x00002468
		public unsafe static byte Level2(int cp)
		{
			return MSCompatUnicodeTable.level2[MSCompatUnicodeTableUtil.Level2.ToIndex(cp)];
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000427C File Offset: 0x0000247C
		public unsafe static byte Level3(int cp)
		{
			return MSCompatUnicodeTable.level3[MSCompatUnicodeTableUtil.Level3.ToIndex(cp)];
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004290 File Offset: 0x00002490
		public unsafe static bool IsIgnorable(int cp, byte flag)
		{
			if (cp == 0)
			{
				return false;
			}
			if ((flag & 1) != 0)
			{
				UnicodeCategory unicodeCategory = char.GetUnicodeCategory((char)cp);
				if (unicodeCategory == UnicodeCategory.OtherNotAssigned)
				{
					return true;
				}
				if (55424 <= cp && cp < 56192)
				{
					return true;
				}
			}
			int num = MSCompatUnicodeTableUtil.Ignorable.ToIndex(cp);
			return num >= 0 && (MSCompatUnicodeTable.ignorableFlags[num] & flag) != 0;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004300 File Offset: 0x00002500
		public static bool IsIgnorableNonSpacing(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 4);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000430C File Offset: 0x0000250C
		public static int ToKanaTypeInsensitive(int i)
		{
			return (12353 > i || i > 12436) ? i : (i + 96);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004330 File Offset: 0x00002530
		public static int ToWidthCompat(int i)
		{
			if (i < 8592)
			{
				return i;
			}
			if (i > 65280)
			{
				if (i <= 65374)
				{
					return i - 65280 + 32;
				}
				switch (i)
				{
				case 65504:
					return 162;
				case 65505:
					return 163;
				case 65506:
					return 172;
				case 65507:
					return 175;
				case 65508:
					return 166;
				case 65509:
					return 165;
				case 65510:
					return 8361;
				}
			}
			if (i > 13054)
			{
				return i;
			}
			if (i <= 8595)
			{
				return 56921 + i;
			}
			if (i < 9474)
			{
				return i;
			}
			if (i <= 9675)
			{
				if (i == 9474)
				{
					return 65512;
				}
				if (i == 9632)
				{
					return 65517;
				}
				if (i != 9675)
				{
					return i;
				}
				return 65518;
			}
			else
			{
				if (i < 12288)
				{
					return i;
				}
				if (i < 12593)
				{
					switch (i)
					{
					case 12288:
						return 32;
					case 12289:
						return 65380;
					case 12290:
						return 65377;
					default:
						if (i == 12300)
						{
							return 65378;
						}
						if (i == 12301)
						{
							return 65379;
						}
						if (i != 12539)
						{
							return i;
						}
						return 65381;
					}
				}
				else
				{
					if (i < 12644)
					{
						return i - 12592 + 65440;
					}
					if (i == 12644)
					{
						return 65440;
					}
					return i;
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000044E0 File Offset: 0x000026E0
		public static bool HasSpecialWeight(char c)
		{
			if (c < 'ぁ')
			{
				return false;
			}
			if ('ｦ' <= c && c < 'ﾞ')
			{
				return true;
			}
			if ('㌀' <= c)
			{
				return false;
			}
			if (c < 'ゝ')
			{
				return c < '゙';
			}
			if (c < '㄀')
			{
				return c != '・';
			}
			return c >= '㋐' && c < '㋿';
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004568 File Offset: 0x00002768
		public static bool IsHalfWidthKana(char c)
		{
			return 'ｦ' <= c && c <= 'ﾝ';
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004584 File Offset: 0x00002784
		public static bool IsHiragana(char c)
		{
			return 'ぁ' <= c && c <= 'ゔ';
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000045A0 File Offset: 0x000027A0
		public static bool IsJapaneseSmallLetter(char c)
		{
			if ('ｧ' <= c && c <= 'ｯ')
			{
				return true;
			}
			if ('぀' < c && c < 'ヺ')
			{
				switch (c)
				{
				case 'ぁ':
				case 'ぃ':
				case 'ぅ':
				case 'ぇ':
				case 'ぉ':
					break;
				default:
					switch (c)
					{
					case 'ァ':
					case 'ィ':
					case 'ゥ':
					case 'ェ':
					case 'ォ':
						break;
					default:
						switch (c)
						{
						case 'ゃ':
						case 'ゅ':
						case 'ょ':
							break;
						default:
							switch (c)
							{
							case 'ャ':
							case 'ュ':
							case 'ョ':
								break;
							default:
								if (c != 'ヵ' && c != 'ヶ' && c != 'っ' && c != 'ゎ' && c != 'ッ' && c != 'ヮ')
								{
									return false;
								}
								break;
							}
							break;
						}
						break;
					}
					break;
				}
				return true;
			}
			return false;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000046C8 File Offset: 0x000028C8
		public static bool IsReady
		{
			get
			{
				return MSCompatUnicodeTable.isReady;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000046D0 File Offset: 0x000028D0
		private static IntPtr GetResource(string name)
		{
			int num;
			Module module;
			return Assembly.GetExecutingAssembly().GetManifestResourceInternal(name, out num, out module);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000046EC File Offset: 0x000028EC
		private unsafe static uint UInt32FromBytePtr(byte* raw, uint idx)
		{
			return (uint)((int)raw[idx] + ((int)raw[idx + 1U] << 8) + ((int)raw[idx + 2U] << 16) + ((int)raw[idx + 3U] << 24));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004714 File Offset: 0x00002914
		public unsafe static void FillCJK(string culture, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer lv2Indexer, ref byte* lv2Table)
		{
			object obj = MSCompatUnicodeTable.forLock;
			lock (obj)
			{
				MSCompatUnicodeTable.FillCJKCore(culture, ref cjkIndexer, ref catTable, ref lv1Table, ref lv2Indexer, ref lv2Table);
				MSCompatUnicodeTable.SetCJKReferences(culture, ref cjkIndexer, ref catTable, ref lv1Table, ref lv2Indexer, ref lv2Table);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004764 File Offset: 0x00002964
		private unsafe static void FillCJKCore(string culture, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer cjkLv2Indexer, ref byte* lv2Table)
		{
			if (!MSCompatUnicodeTable.IsReady)
			{
				return;
			}
			string text = null;
			switch (culture)
			{
			case "zh-CHS":
				text = "cjkCHS";
				catTable = MSCompatUnicodeTable.cjkCHScategory;
				lv1Table = MSCompatUnicodeTable.cjkCHSlv1;
				break;
			case "zh-CHT":
				text = "cjkCHT";
				catTable = MSCompatUnicodeTable.cjkCHTcategory;
				lv1Table = MSCompatUnicodeTable.cjkCHTlv1;
				break;
			case "ja":
				text = "cjkJA";
				catTable = MSCompatUnicodeTable.cjkJAcategory;
				lv1Table = MSCompatUnicodeTable.cjkJAlv1;
				break;
			case "ko":
				text = "cjkKO";
				catTable = MSCompatUnicodeTable.cjkKOcategory;
				lv1Table = MSCompatUnicodeTable.cjkKOlv1;
				break;
			}
			if (text == null || lv1Table != 0)
			{
				return;
			}
			uint num2 = 0U;
			string name = string.Format("collation.{0}.bin", text);
			IntPtr resource = MSCompatUnicodeTable.GetResource(name);
			if (resource == IntPtr.Zero)
			{
				return;
			}
			byte* ptr = (byte*)((void*)resource);
			num2 += 1U;
			uint num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num2);
			num2 += 4U;
			catTable = ptr + num2;
			lv1Table = ptr + num2 + num3;
			switch (culture)
			{
			case "zh-CHS":
				MSCompatUnicodeTable.cjkCHScategory = catTable;
				MSCompatUnicodeTable.cjkCHSlv1 = lv1Table;
				break;
			case "zh-CHT":
				MSCompatUnicodeTable.cjkCHTcategory = catTable;
				MSCompatUnicodeTable.cjkCHTlv1 = lv1Table;
				break;
			case "ja":
				MSCompatUnicodeTable.cjkJAcategory = catTable;
				MSCompatUnicodeTable.cjkJAlv1 = lv1Table;
				break;
			case "ko":
				MSCompatUnicodeTable.cjkKOcategory = catTable;
				MSCompatUnicodeTable.cjkKOlv1 = lv1Table;
				break;
			}
			if (text != "cjkKO")
			{
				return;
			}
			resource = MSCompatUnicodeTable.GetResource("collation.cjkKOlv2.bin");
			if (resource == IntPtr.Zero)
			{
				return;
			}
			ptr = (byte*)((void*)resource);
			num2 = 5U;
			MSCompatUnicodeTable.cjkKOlv2 = ptr + num2;
			lv2Table = MSCompatUnicodeTable.cjkKOlv2;
		}

		// Token: 0x04000084 RID: 132
		private const int ResourceVersionSize = 1;

		// Token: 0x04000085 RID: 133
		public static int MaxExpansionLength = 3;

		// Token: 0x04000086 RID: 134
		private unsafe static readonly byte* ignorableFlags;

		// Token: 0x04000087 RID: 135
		private unsafe static readonly byte* categories;

		// Token: 0x04000088 RID: 136
		private unsafe static readonly byte* level1;

		// Token: 0x04000089 RID: 137
		private unsafe static readonly byte* level2;

		// Token: 0x0400008A RID: 138
		private unsafe static readonly byte* level3;

		// Token: 0x0400008B RID: 139
		private unsafe static byte* cjkCHScategory;

		// Token: 0x0400008C RID: 140
		private unsafe static byte* cjkCHTcategory;

		// Token: 0x0400008D RID: 141
		private unsafe static byte* cjkJAcategory;

		// Token: 0x0400008E RID: 142
		private unsafe static byte* cjkKOcategory;

		// Token: 0x0400008F RID: 143
		private unsafe static byte* cjkCHSlv1;

		// Token: 0x04000090 RID: 144
		private unsafe static byte* cjkCHTlv1;

		// Token: 0x04000091 RID: 145
		private unsafe static byte* cjkJAlv1;

		// Token: 0x04000092 RID: 146
		private unsafe static byte* cjkKOlv1;

		// Token: 0x04000093 RID: 147
		private unsafe static byte* cjkKOlv2;

		// Token: 0x04000094 RID: 148
		private static readonly char[] tailoringArr;

		// Token: 0x04000095 RID: 149
		private static readonly TailoringInfo[] tailoringInfos;

		// Token: 0x04000096 RID: 150
		private static object forLock = new object();

		// Token: 0x04000097 RID: 151
		public static readonly bool isReady;
	}
}
