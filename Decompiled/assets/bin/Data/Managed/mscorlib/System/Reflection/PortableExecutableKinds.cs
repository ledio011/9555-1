using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001E9 RID: 489
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum PortableExecutableKinds
	{
		// Token: 0x04000947 RID: 2375
		NotAPortableExecutableImage = 0,
		// Token: 0x04000948 RID: 2376
		ILOnly = 1,
		// Token: 0x04000949 RID: 2377
		Required32Bit = 2,
		// Token: 0x0400094A RID: 2378
		PE32Plus = 4,
		// Token: 0x0400094B RID: 2379
		Unmanaged32Bit = 8
	}
}
