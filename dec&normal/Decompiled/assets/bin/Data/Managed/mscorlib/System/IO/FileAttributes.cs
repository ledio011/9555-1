using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000121 RID: 289
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum FileAttributes
	{
		// Token: 0x0400047E RID: 1150
		Archive = 32,
		// Token: 0x0400047F RID: 1151
		Compressed = 2048,
		// Token: 0x04000480 RID: 1152
		Device = 64,
		// Token: 0x04000481 RID: 1153
		Directory = 16,
		// Token: 0x04000482 RID: 1154
		Encrypted = 16384,
		// Token: 0x04000483 RID: 1155
		Hidden = 2,
		// Token: 0x04000484 RID: 1156
		Normal = 128,
		// Token: 0x04000485 RID: 1157
		NotContentIndexed = 8192,
		// Token: 0x04000486 RID: 1158
		Offline = 4096,
		// Token: 0x04000487 RID: 1159
		ReadOnly = 1,
		// Token: 0x04000488 RID: 1160
		ReparsePoint = 1024,
		// Token: 0x04000489 RID: 1161
		SparseFile = 512,
		// Token: 0x0400048A RID: 1162
		System = 4,
		// Token: 0x0400048B RID: 1163
		Temporary = 256
	}
}
