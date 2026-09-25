using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001E5 RID: 485
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum ParameterAttributes
	{
		// Token: 0x0400092C RID: 2348
		None = 0,
		// Token: 0x0400092D RID: 2349
		In = 1,
		// Token: 0x0400092E RID: 2350
		Out = 2,
		// Token: 0x0400092F RID: 2351
		Lcid = 4,
		// Token: 0x04000930 RID: 2352
		Retval = 8,
		// Token: 0x04000931 RID: 2353
		Optional = 16,
		// Token: 0x04000932 RID: 2354
		ReservedMask = 61440,
		// Token: 0x04000933 RID: 2355
		HasDefault = 4096,
		// Token: 0x04000934 RID: 2356
		HasFieldMarshal = 8192,
		// Token: 0x04000935 RID: 2357
		Reserved3 = 16384,
		// Token: 0x04000936 RID: 2358
		Reserved4 = 32768
	}
}
