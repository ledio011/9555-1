using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200007B RID: 123
	[Flags]
	internal enum OpFlags : ushort
	{
		// Token: 0x040009D7 RID: 2519
		None = 0,
		// Token: 0x040009D8 RID: 2520
		Negate = 256,
		// Token: 0x040009D9 RID: 2521
		IgnoreCase = 512,
		// Token: 0x040009DA RID: 2522
		RightToLeft = 1024,
		// Token: 0x040009DB RID: 2523
		Lazy = 2048
	}
}
