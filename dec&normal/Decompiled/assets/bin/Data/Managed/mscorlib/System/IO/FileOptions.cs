using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000124 RID: 292
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum FileOptions
	{
		// Token: 0x04000497 RID: 1175
		None = 0,
		// Token: 0x04000498 RID: 1176
		Encrypted = 16384,
		// Token: 0x04000499 RID: 1177
		DeleteOnClose = 67108864,
		// Token: 0x0400049A RID: 1178
		SequentialScan = 134217728,
		// Token: 0x0400049B RID: 1179
		RandomAccess = 268435456,
		// Token: 0x0400049C RID: 1180
		Asynchronous = 1073741824,
		// Token: 0x0400049D RID: 1181
		WriteThrough = -2147483648
	}
}
