using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000187 RID: 391
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AssemblyNameFlags
	{
		// Token: 0x04000604 RID: 1540
		None = 0,
		// Token: 0x04000605 RID: 1541
		PublicKey = 1,
		// Token: 0x04000606 RID: 1542
		Retargetable = 256,
		// Token: 0x04000607 RID: 1543
		EnableJITcompileOptimizer = 16384,
		// Token: 0x04000608 RID: 1544
		EnableJITcompileTracking = 32768
	}
}
