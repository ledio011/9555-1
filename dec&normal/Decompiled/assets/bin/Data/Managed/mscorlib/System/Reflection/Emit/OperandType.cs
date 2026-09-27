using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001B5 RID: 437
	[ComVisible(true)]
	[Serializable]
	public enum OperandType
	{
		// Token: 0x04000809 RID: 2057
		InlineBrTarget,
		// Token: 0x0400080A RID: 2058
		InlineField,
		// Token: 0x0400080B RID: 2059
		InlineI,
		// Token: 0x0400080C RID: 2060
		InlineI8,
		// Token: 0x0400080D RID: 2061
		InlineMethod,
		// Token: 0x0400080E RID: 2062
		InlineNone,
		// Token: 0x0400080F RID: 2063
		[Obsolete("This API has been deprecated.")]
		InlinePhi,
		// Token: 0x04000810 RID: 2064
		InlineR,
		// Token: 0x04000811 RID: 2065
		InlineSig = 9,
		// Token: 0x04000812 RID: 2066
		InlineString,
		// Token: 0x04000813 RID: 2067
		InlineSwitch,
		// Token: 0x04000814 RID: 2068
		InlineTok,
		// Token: 0x04000815 RID: 2069
		InlineType,
		// Token: 0x04000816 RID: 2070
		InlineVar,
		// Token: 0x04000817 RID: 2071
		ShortInlineBrTarget,
		// Token: 0x04000818 RID: 2072
		ShortInlineI,
		// Token: 0x04000819 RID: 2073
		ShortInlineR,
		// Token: 0x0400081A RID: 2074
		ShortInlineVar
	}
}
