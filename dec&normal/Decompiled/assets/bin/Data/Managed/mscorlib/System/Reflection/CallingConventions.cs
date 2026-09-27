using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200018E RID: 398
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CallingConventions
	{
		// Token: 0x04000623 RID: 1571
		Standard = 1,
		// Token: 0x04000624 RID: 1572
		VarArgs = 2,
		// Token: 0x04000625 RID: 1573
		Any = 3,
		// Token: 0x04000626 RID: 1574
		HasThis = 32,
		// Token: 0x04000627 RID: 1575
		ExplicitThis = 64
	}
}
