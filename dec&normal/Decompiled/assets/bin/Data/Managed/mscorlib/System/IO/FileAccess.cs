using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000120 RID: 288
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileAccess
	{
		// Token: 0x0400047A RID: 1146
		Read = 1,
		// Token: 0x0400047B RID: 1147
		Write = 2,
		// Token: 0x0400047C RID: 1148
		ReadWrite = 3
	}
}
