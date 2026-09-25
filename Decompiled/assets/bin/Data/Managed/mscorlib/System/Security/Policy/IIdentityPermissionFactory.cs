using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000362 RID: 866
	[ComVisible(true)]
	public interface IIdentityPermissionFactory
	{
		// Token: 0x060019B4 RID: 6580
		IPermission CreateIdentityPermission(Evidence evidence);
	}
}
