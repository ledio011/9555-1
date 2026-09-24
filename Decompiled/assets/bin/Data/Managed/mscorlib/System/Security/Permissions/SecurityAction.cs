using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200034A RID: 842
	[Obsolete("CAS support is not available with Silverlight applications.")]
	[ComVisible(true)]
	[Serializable]
	public enum SecurityAction
	{
		// Token: 0x04000DAF RID: 3503
		Demand = 2,
		// Token: 0x04000DB0 RID: 3504
		Assert,
		// Token: 0x04000DB1 RID: 3505
		Deny,
		// Token: 0x04000DB2 RID: 3506
		PermitOnly,
		// Token: 0x04000DB3 RID: 3507
		LinkDemand,
		// Token: 0x04000DB4 RID: 3508
		InheritanceDemand,
		// Token: 0x04000DB5 RID: 3509
		RequestMinimum,
		// Token: 0x04000DB6 RID: 3510
		RequestOptional,
		// Token: 0x04000DB7 RID: 3511
		RequestRefuse
	}
}
