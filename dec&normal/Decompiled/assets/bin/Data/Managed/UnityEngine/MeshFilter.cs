using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B4 RID: 180
	public sealed class MeshFilter : Component
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007D1 RID: 2001
		// (set) Token: 0x060007D2 RID: 2002
		public extern Mesh mesh { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007D3 RID: 2003
		// (set) Token: 0x060007D4 RID: 2004
		public extern Mesh sharedMesh { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
