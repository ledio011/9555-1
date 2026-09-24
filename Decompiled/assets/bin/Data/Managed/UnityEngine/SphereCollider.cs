using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200010A RID: 266
	public sealed class SphereCollider : Collider
	{
		// Token: 0x1700022B RID: 555
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00015F38 File Offset: 0x00014138
		public Vector3 center
		{
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x060009C9 RID: 2505
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x1700022C RID: 556
		// (set) Token: 0x060009CA RID: 2506
		public extern float radius { [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
