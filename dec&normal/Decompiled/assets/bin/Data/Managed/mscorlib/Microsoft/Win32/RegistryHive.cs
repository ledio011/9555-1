using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	// Token: 0x02000025 RID: 37
	[ComVisible(true)]
	[Serializable]
	public enum RegistryHive
	{
		// Token: 0x04000057 RID: 87
		ClassesRoot = -2147483648,
		// Token: 0x04000058 RID: 88
		CurrentConfig = -2147483643,
		// Token: 0x04000059 RID: 89
		CurrentUser = -2147483647,
		// Token: 0x0400005A RID: 90
		DynData = -2147483642,
		// Token: 0x0400005B RID: 91
		LocalMachine = -2147483646,
		// Token: 0x0400005C RID: 92
		PerformanceData = -2147483644,
		// Token: 0x0400005D RID: 93
		Users = -2147483645
	}
}
