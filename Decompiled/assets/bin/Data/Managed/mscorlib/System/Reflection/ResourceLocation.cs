using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001EE RID: 494
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum ResourceLocation
	{
		// Token: 0x0400095F RID: 2399
		Embedded = 1,
		// Token: 0x04000960 RID: 2400
		ContainedInAnotherAssembly = 2,
		// Token: 0x04000961 RID: 2401
		ContainedInManifestFile = 4
	}
}
