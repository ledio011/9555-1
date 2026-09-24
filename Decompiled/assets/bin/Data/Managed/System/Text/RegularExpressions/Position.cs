using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200007F RID: 127
	internal enum Position : ushort
	{
		// Token: 0x040009E1 RID: 2529
		Any,
		// Token: 0x040009E2 RID: 2530
		Start,
		// Token: 0x040009E3 RID: 2531
		StartOfString,
		// Token: 0x040009E4 RID: 2532
		StartOfLine,
		// Token: 0x040009E5 RID: 2533
		StartOfScan,
		// Token: 0x040009E6 RID: 2534
		End,
		// Token: 0x040009E7 RID: 2535
		EndOfString,
		// Token: 0x040009E8 RID: 2536
		EndOfLine,
		// Token: 0x040009E9 RID: 2537
		Boundary,
		// Token: 0x040009EA RID: 2538
		NonBoundary
	}
}
