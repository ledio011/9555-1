using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000384 RID: 900
	[ComVisible(true)]
	[Serializable]
	public enum SecurityZone
	{
		// Token: 0x04000E93 RID: 3731
		MyComputer,
		// Token: 0x04000E94 RID: 3732
		Intranet,
		// Token: 0x04000E95 RID: 3733
		Trusted,
		// Token: 0x04000E96 RID: 3734
		Internet,
		// Token: 0x04000E97 RID: 3735
		Untrusted,
		// Token: 0x04000E98 RID: 3736
		NoZone = -1
	}
}
