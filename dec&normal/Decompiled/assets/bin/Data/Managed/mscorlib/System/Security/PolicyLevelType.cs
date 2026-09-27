using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000370 RID: 880
	[ComVisible(true)]
	[Serializable]
	public enum PolicyLevelType
	{
		// Token: 0x04000E41 RID: 3649
		User,
		// Token: 0x04000E42 RID: 3650
		Machine,
		// Token: 0x04000E43 RID: 3651
		Enterprise,
		// Token: 0x04000E44 RID: 3652
		AppDomain
	}
}
