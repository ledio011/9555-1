using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020003AE RID: 942
	[ComVisible(true)]
	[Serializable]
	public enum ApartmentState
	{
		// Token: 0x04000F11 RID: 3857
		STA,
		// Token: 0x04000F12 RID: 3858
		MTA,
		// Token: 0x04000F13 RID: 3859
		Unknown
	}
}
