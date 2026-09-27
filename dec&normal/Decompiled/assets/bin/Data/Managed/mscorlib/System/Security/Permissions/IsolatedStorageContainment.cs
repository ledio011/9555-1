using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200033D RID: 829
	[ComVisible(true)]
	[Serializable]
	public enum IsolatedStorageContainment
	{
		// Token: 0x04000D73 RID: 3443
		None,
		// Token: 0x04000D74 RID: 3444
		DomainIsolationByUser = 16,
		// Token: 0x04000D75 RID: 3445
		AssemblyIsolationByUser = 32,
		// Token: 0x04000D76 RID: 3446
		DomainIsolationByRoamingUser = 80,
		// Token: 0x04000D77 RID: 3447
		AssemblyIsolationByRoamingUser = 96,
		// Token: 0x04000D78 RID: 3448
		AdministerIsolatedStorageByUser = 112,
		// Token: 0x04000D79 RID: 3449
		UnrestrictedIsolatedStorage = 240,
		// Token: 0x04000D7A RID: 3450
		ApplicationIsolationByUser = 21,
		// Token: 0x04000D7B RID: 3451
		DomainIsolationByMachine = 48,
		// Token: 0x04000D7C RID: 3452
		AssemblyIsolationByMachine = 64,
		// Token: 0x04000D7D RID: 3453
		ApplicationIsolationByMachine = 69,
		// Token: 0x04000D7E RID: 3454
		ApplicationIsolationByRoamingUser = 101
	}
}
