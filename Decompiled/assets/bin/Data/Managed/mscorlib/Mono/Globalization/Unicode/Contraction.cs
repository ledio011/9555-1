using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000030 RID: 48
	internal class Contraction
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public Contraction(char[] source, string replacement, byte[] sortkey)
		{
			this.Source = source;
			this.Replacement = replacement;
			this.SortKey = sortkey;
		}

		// Token: 0x0400007D RID: 125
		public readonly char[] Source;

		// Token: 0x0400007E RID: 126
		public readonly string Replacement;

		// Token: 0x0400007F RID: 127
		public readonly byte[] SortKey;
	}
}
