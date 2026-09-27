using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x0200036A RID: 874
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum PolicyStatementAttribute
	{
		// Token: 0x04000E32 RID: 3634
		Nothing = 0,
		// Token: 0x04000E33 RID: 3635
		Exclusive = 1,
		// Token: 0x04000E34 RID: 3636
		LevelFinal = 2,
		// Token: 0x04000E35 RID: 3637
		All = 3
	}
}
