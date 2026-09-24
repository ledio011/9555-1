using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000296 RID: 662
	[ComVisible(true)]
	[Serializable]
	public enum LeaseState
	{
		// Token: 0x04000ADA RID: 2778
		Null,
		// Token: 0x04000ADB RID: 2779
		Initial,
		// Token: 0x04000ADC RID: 2780
		Active,
		// Token: 0x04000ADD RID: 2781
		Renewing,
		// Token: 0x04000ADE RID: 2782
		Expired
	}
}
