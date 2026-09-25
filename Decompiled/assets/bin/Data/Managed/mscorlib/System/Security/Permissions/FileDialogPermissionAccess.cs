using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000339 RID: 825
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileDialogPermissionAccess
	{
		// Token: 0x04000D5D RID: 3421
		None = 0,
		// Token: 0x04000D5E RID: 3422
		Open = 1,
		// Token: 0x04000D5F RID: 3423
		Save = 2,
		// Token: 0x04000D60 RID: 3424
		OpenSave = 3
	}
}
