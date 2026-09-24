using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000193 RID: 403
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum AssemblyBuilderAccess
	{
		// Token: 0x04000654 RID: 1620
		Run = 1,
		// Token: 0x04000655 RID: 1621
		Save = 2,
		// Token: 0x04000656 RID: 1622
		RunAndSave = 3,
		// Token: 0x04000657 RID: 1623
		ReflectionOnly = 6
	}
}
