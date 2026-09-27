using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000035 RID: 53
	internal class MSCompatUnicodeTableUtil
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000049E4 File Offset: 0x00002BE4
		static MSCompatUnicodeTableUtil()
		{
			int[] starts = new int[]
			{
				0,
				40960,
				63744
			};
			int[] ends = new int[]
			{
				13312,
				42240,
				65536
			};
			int[] starts2 = new int[]
			{
				0,
				7680,
				12288,
				19968,
				44032,
				63744
			};
			int[] ends2 = new int[]
			{
				4608,
				10240,
				13312,
				40960,
				55216,
				65536
			};
			int[] starts3 = new int[]
			{
				0,
				7680,
				12288,
				19968,
				44032,
				63744
			};
			int[] ends3 = new int[]
			{
				4608,
				10240,
				13312,
				40960,
				55216,
				65536
			};
			int[] starts4 = new int[]
			{
				0,
				7680,
				12288,
				64256
			};
			int[] ends4 = new int[]
			{
				3840,
				10240,
				13312,
				65536
			};
			int[] starts5 = new int[]
			{
				0,
				7680,
				12288,
				64256
			};
			int[] ends5 = new int[]
			{
				4608,
				10240,
				13312,
				65536
			};
			int[] starts6 = new int[]
			{
				12544,
				19968,
				59392
			};
			int[] ends6 = new int[]
			{
				13312,
				40960,
				65536
			};
			int[] starts7 = new int[]
			{
				12544,
				19968,
				63744
			};
			int[] ends7 = new int[]
			{
				13312,
				40960,
				64256
			};
			MSCompatUnicodeTableUtil.Ignorable = new CodePointIndexer(starts, ends, -1, -1);
			MSCompatUnicodeTableUtil.Category = new CodePointIndexer(starts2, ends2, 0, 0);
			MSCompatUnicodeTableUtil.Level1 = new CodePointIndexer(starts3, ends3, 0, 0);
			MSCompatUnicodeTableUtil.Level2 = new CodePointIndexer(starts4, ends4, 0, 0);
			MSCompatUnicodeTableUtil.Level3 = new CodePointIndexer(starts5, ends5, 0, 0);
			MSCompatUnicodeTableUtil.CjkCHS = new CodePointIndexer(starts6, ends6, -1, -1);
			MSCompatUnicodeTableUtil.Cjk = new CodePointIndexer(starts7, ends7, -1, -1);
		}

		// Token: 0x0400009B RID: 155
		public const byte ResourceVersion = 3;

		// Token: 0x0400009C RID: 156
		public static readonly CodePointIndexer Ignorable;

		// Token: 0x0400009D RID: 157
		public static readonly CodePointIndexer Category;

		// Token: 0x0400009E RID: 158
		public static readonly CodePointIndexer Level1;

		// Token: 0x0400009F RID: 159
		public static readonly CodePointIndexer Level2;

		// Token: 0x040000A0 RID: 160
		public static readonly CodePointIndexer Level3;

		// Token: 0x040000A1 RID: 161
		public static readonly CodePointIndexer CjkCHS;

		// Token: 0x040000A2 RID: 162
		public static readonly CodePointIndexer Cjk;
	}
}
