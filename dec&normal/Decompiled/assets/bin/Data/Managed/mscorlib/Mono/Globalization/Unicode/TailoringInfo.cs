using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200003F RID: 63
	internal class TailoringInfo
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000923C File Offset: 0x0000743C
		public TailoringInfo(int lcid, int tailoringIndex, int tailoringCount, bool frenchSort)
		{
			this.LCID = lcid;
			this.TailoringIndex = tailoringIndex;
			this.TailoringCount = tailoringCount;
			this.FrenchSort = frenchSort;
		}

		// Token: 0x040000FE RID: 254
		public readonly int LCID;

		// Token: 0x040000FF RID: 255
		public readonly int TailoringIndex;

		// Token: 0x04000100 RID: 256
		public readonly int TailoringCount;

		// Token: 0x04000101 RID: 257
		public readonly bool FrenchSort;
	}
}
