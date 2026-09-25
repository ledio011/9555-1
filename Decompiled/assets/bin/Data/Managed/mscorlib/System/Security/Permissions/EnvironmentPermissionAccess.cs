using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000337 RID: 823
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum EnvironmentPermissionAccess
	{
		// Token: 0x04000D56 RID: 3414
		NoAccess = 0,
		// Token: 0x04000D57 RID: 3415
		Read = 1,
		// Token: 0x04000D58 RID: 3416
		Write = 2,
		// Token: 0x04000D59 RID: 3417
		AllAccess = 3
	}
}
