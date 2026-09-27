using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200033B RID: 827
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileIOPermissionAccess
	{
		// Token: 0x04000D6C RID: 3436
		NoAccess = 0,
		// Token: 0x04000D6D RID: 3437
		Read = 1,
		// Token: 0x04000D6E RID: 3438
		Write = 2,
		// Token: 0x04000D6F RID: 3439
		Append = 4,
		// Token: 0x04000D70 RID: 3440
		PathDiscovery = 8,
		// Token: 0x04000D71 RID: 3441
		AllAccess = 15
	}
}
