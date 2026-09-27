using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020000F5 RID: 245
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CompareOptions
	{
		// Token: 0x04000342 RID: 834
		None = 0,
		// Token: 0x04000343 RID: 835
		IgnoreCase = 1,
		// Token: 0x04000344 RID: 836
		IgnoreNonSpace = 2,
		// Token: 0x04000345 RID: 837
		IgnoreSymbols = 4,
		// Token: 0x04000346 RID: 838
		IgnoreKanaType = 8,
		// Token: 0x04000347 RID: 839
		IgnoreWidth = 16,
		// Token: 0x04000348 RID: 840
		StringSort = 536870912,
		// Token: 0x04000349 RID: 841
		Ordinal = 1073741824,
		// Token: 0x0400034A RID: 842
		OrdinalIgnoreCase = 268435456
	}
}
