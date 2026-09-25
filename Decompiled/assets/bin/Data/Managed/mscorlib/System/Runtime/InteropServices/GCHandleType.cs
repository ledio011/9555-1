using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000241 RID: 577
	[ComVisible(true)]
	[Serializable]
	public enum GCHandleType
	{
		// Token: 0x040009FF RID: 2559
		Weak,
		// Token: 0x04000A00 RID: 2560
		WeakTrackResurrection,
		// Token: 0x04000A01 RID: 2561
		Normal,
		// Token: 0x04000A02 RID: 2562
		Pinned
	}
}
