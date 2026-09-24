using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000344 RID: 836
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum KeyContainerPermissionFlags
	{
		// Token: 0x04000D8C RID: 3468
		NoFlags = 0,
		// Token: 0x04000D8D RID: 3469
		Create = 1,
		// Token: 0x04000D8E RID: 3470
		Open = 2,
		// Token: 0x04000D8F RID: 3471
		Delete = 4,
		// Token: 0x04000D90 RID: 3472
		Import = 16,
		// Token: 0x04000D91 RID: 3473
		Export = 32,
		// Token: 0x04000D92 RID: 3474
		Sign = 256,
		// Token: 0x04000D93 RID: 3475
		Decrypt = 512,
		// Token: 0x04000D94 RID: 3476
		ViewAcl = 4096,
		// Token: 0x04000D95 RID: 3477
		ChangeAcl = 8192,
		// Token: 0x04000D96 RID: 3478
		AllFlags = 13111
	}
}
