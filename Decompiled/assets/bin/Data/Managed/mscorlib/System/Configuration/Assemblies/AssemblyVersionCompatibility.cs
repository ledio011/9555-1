using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
	// Token: 0x020000BE RID: 190
	[ComVisible(true)]
	[Serializable]
	public enum AssemblyVersionCompatibility
	{
		// Token: 0x04000265 RID: 613
		SameMachine = 1,
		// Token: 0x04000266 RID: 614
		SameProcess,
		// Token: 0x04000267 RID: 615
		SameDomain
	}
}
