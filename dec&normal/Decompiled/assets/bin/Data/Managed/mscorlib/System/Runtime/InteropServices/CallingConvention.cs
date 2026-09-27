using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200022E RID: 558
	[ComVisible(true)]
	[Serializable]
	public enum CallingConvention
	{
		// Token: 0x040009D7 RID: 2519
		Winapi = 1,
		// Token: 0x040009D8 RID: 2520
		Cdecl,
		// Token: 0x040009D9 RID: 2521
		StdCall,
		// Token: 0x040009DA RID: 2522
		ThisCall,
		// Token: 0x040009DB RID: 2523
		FastCall
	}
}
