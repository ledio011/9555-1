using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	public enum Consistency
	{
		// Token: 0x040009CC RID: 2508
		MayCorruptAppDomain = 1,
		// Token: 0x040009CD RID: 2509
		MayCorruptInstance,
		// Token: 0x040009CE RID: 2510
		MayCorruptProcess = 0,
		// Token: 0x040009CF RID: 2511
		WillNotCorruptState = 3
	}
}
