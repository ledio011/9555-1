using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x02000100 RID: 256
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum NumberStyles
	{
		// Token: 0x04000403 RID: 1027
		None = 0,
		// Token: 0x04000404 RID: 1028
		AllowLeadingWhite = 1,
		// Token: 0x04000405 RID: 1029
		AllowTrailingWhite = 2,
		// Token: 0x04000406 RID: 1030
		AllowLeadingSign = 4,
		// Token: 0x04000407 RID: 1031
		AllowTrailingSign = 8,
		// Token: 0x04000408 RID: 1032
		AllowParentheses = 16,
		// Token: 0x04000409 RID: 1033
		AllowDecimalPoint = 32,
		// Token: 0x0400040A RID: 1034
		AllowThousands = 64,
		// Token: 0x0400040B RID: 1035
		AllowExponent = 128,
		// Token: 0x0400040C RID: 1036
		AllowCurrencySymbol = 256,
		// Token: 0x0400040D RID: 1037
		AllowHexSpecifier = 512,
		// Token: 0x0400040E RID: 1038
		Integer = 7,
		// Token: 0x0400040F RID: 1039
		HexNumber = 515,
		// Token: 0x04000410 RID: 1040
		Number = 111,
		// Token: 0x04000411 RID: 1041
		Float = 167,
		// Token: 0x04000412 RID: 1042
		Currency = 383,
		// Token: 0x04000413 RID: 1043
		Any = 511
	}
}
