using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	public class Behaviour : Component
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600027C RID: 636
		// (set) Token: 0x0600027D RID: 637
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600027E RID: 638
		public extern bool isActiveAndEnabled { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
