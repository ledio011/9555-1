using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001EB RID: 491
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum PropertyAttributes
	{
		// Token: 0x04000953 RID: 2387
		None = 0,
		// Token: 0x04000954 RID: 2388
		SpecialName = 512,
		// Token: 0x04000955 RID: 2389
		ReservedMask = 62464,
		// Token: 0x04000956 RID: 2390
		RTSpecialName = 1024,
		// Token: 0x04000957 RID: 2391
		HasDefault = 4096,
		// Token: 0x04000958 RID: 2392
		Reserved2 = 8192,
		// Token: 0x04000959 RID: 2393
		Reserved3 = 16384,
		// Token: 0x0400095A RID: 2394
		Reserved4 = 32768
	}
}
