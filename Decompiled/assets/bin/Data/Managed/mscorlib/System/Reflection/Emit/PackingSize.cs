using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001B6 RID: 438
	[ComVisible(true)]
	[Serializable]
	public enum PackingSize
	{
		// Token: 0x0400081C RID: 2076
		Unspecified,
		// Token: 0x0400081D RID: 2077
		Size1,
		// Token: 0x0400081E RID: 2078
		Size2,
		// Token: 0x0400081F RID: 2079
		Size4 = 4,
		// Token: 0x04000820 RID: 2080
		Size8 = 8,
		// Token: 0x04000821 RID: 2081
		Size16 = 16,
		// Token: 0x04000822 RID: 2082
		Size32 = 32,
		// Token: 0x04000823 RID: 2083
		Size64 = 64,
		// Token: 0x04000824 RID: 2084
		Size128 = 128
	}
}
