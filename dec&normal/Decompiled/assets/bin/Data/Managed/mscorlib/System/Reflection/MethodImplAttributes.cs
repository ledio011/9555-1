using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001D4 RID: 468
	[ComVisible(true)]
	[Serializable]
	public enum MethodImplAttributes
	{
		// Token: 0x040008E6 RID: 2278
		CodeTypeMask = 3,
		// Token: 0x040008E7 RID: 2279
		IL = 0,
		// Token: 0x040008E8 RID: 2280
		Native,
		// Token: 0x040008E9 RID: 2281
		OPTIL,
		// Token: 0x040008EA RID: 2282
		Runtime,
		// Token: 0x040008EB RID: 2283
		ManagedMask,
		// Token: 0x040008EC RID: 2284
		Unmanaged = 4,
		// Token: 0x040008ED RID: 2285
		Managed = 0,
		// Token: 0x040008EE RID: 2286
		ForwardRef = 16,
		// Token: 0x040008EF RID: 2287
		PreserveSig = 128,
		// Token: 0x040008F0 RID: 2288
		InternalCall = 4096,
		// Token: 0x040008F1 RID: 2289
		Synchronized = 32,
		// Token: 0x040008F2 RID: 2290
		NoInlining = 8,
		// Token: 0x040008F3 RID: 2291
		MaxMethodImplVal = 65535
	}
}
