using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000349 RID: 841
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum RegistryPermissionAccess
	{
		// Token: 0x04000DA9 RID: 3497
		NoAccess = 0,
		// Token: 0x04000DAA RID: 3498
		Read = 1,
		// Token: 0x04000DAB RID: 3499
		Write = 2,
		// Token: 0x04000DAC RID: 3500
		Create = 4,
		// Token: 0x04000DAD RID: 3501
		AllAccess = 7
	}
}
