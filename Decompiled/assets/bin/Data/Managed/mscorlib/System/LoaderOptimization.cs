using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200014A RID: 330
	[ComVisible(true)]
	[Serializable]
	public enum LoaderOptimization
	{
		// Token: 0x0400054B RID: 1355
		NotSpecified,
		// Token: 0x0400054C RID: 1356
		SingleDomain,
		// Token: 0x0400054D RID: 1357
		MultiDomain,
		// Token: 0x0400054E RID: 1358
		MultiDomainHost,
		// Token: 0x0400054F RID: 1359
		[Obsolete]
		DomainMask = 3,
		// Token: 0x04000550 RID: 1360
		[Obsolete]
		DisallowBindings
	}
}
