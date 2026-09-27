using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000122 RID: 290
	[ComVisible(true)]
	[Serializable]
	public enum FileMode
	{
		// Token: 0x0400048D RID: 1165
		CreateNew = 1,
		// Token: 0x0400048E RID: 1166
		Create,
		// Token: 0x0400048F RID: 1167
		Open,
		// Token: 0x04000490 RID: 1168
		OpenOrCreate,
		// Token: 0x04000491 RID: 1169
		Truncate,
		// Token: 0x04000492 RID: 1170
		Append
	}
}
