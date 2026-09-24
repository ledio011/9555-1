using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000083 RID: 131
	[Flags]
	public enum RegexOptions
	{
		// Token: 0x04000A02 RID: 2562
		None = 0,
		// Token: 0x04000A03 RID: 2563
		IgnoreCase = 1,
		// Token: 0x04000A04 RID: 2564
		Multiline = 2,
		// Token: 0x04000A05 RID: 2565
		ExplicitCapture = 4,
		// Token: 0x04000A06 RID: 2566
		Singleline = 16,
		// Token: 0x04000A07 RID: 2567
		IgnorePatternWhitespace = 32,
		// Token: 0x04000A08 RID: 2568
		RightToLeft = 64,
		// Token: 0x04000A09 RID: 2569
		ECMAScript = 256,
		// Token: 0x04000A0A RID: 2570
		CultureInvariant = 512
	}
}
