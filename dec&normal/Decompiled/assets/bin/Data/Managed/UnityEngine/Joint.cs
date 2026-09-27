using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200009E RID: 158
	public class Joint : Component
	{
		// Token: 0x17000179 RID: 377
		// (set) Token: 0x0600070D RID: 1805
		public extern Rigidbody connectedBody { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700017A RID: 378
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00011948 File Offset: 0x0000FB48
		public Vector3 axis
		{
			set
			{
				this.INTERNAL_set_axis(ref value);
			}
		}

		// Token: 0x0600070F RID: 1807
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_axis(ref Vector3 value);

		// Token: 0x1700017B RID: 379
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00011954 File Offset: 0x0000FB54
		public Vector3 anchor
		{
			set
			{
				this.INTERNAL_set_anchor(ref value);
			}
		}

		// Token: 0x06000711 RID: 1809
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_anchor(ref Vector3 value);
	}
}
