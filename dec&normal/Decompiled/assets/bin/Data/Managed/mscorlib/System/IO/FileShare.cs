using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000125 RID: 293
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum FileShare
	{
		// Token: 0x0400049F RID: 1183
		None = 0,
		// Token: 0x040004A0 RID: 1184
		Read = 1,
		// Token: 0x040004A1 RID: 1185
		Write = 2,
		// Token: 0x040004A2 RID: 1186
		ReadWrite = 3,
		// Token: 0x040004A3 RID: 1187
		Delete = 4,
		// Token: 0x040004A4 RID: 1188
		Inheritable = 16
	}
}
