using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000029 RID: 41
	public sealed class BoxCollider : Collider
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000076DC File Offset: 0x000058DC
		// (set) Token: 0x0600029D RID: 669 RVA: 0x000076F4 File Offset: 0x000058F4
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_center(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x0600029E RID: 670
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x0600029F RID: 671
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00007700 File Offset: 0x00005900
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00007718 File Offset: 0x00005918
		public Vector3 size
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_size(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}

		// Token: 0x060002A2 RID: 674
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x060002A3 RID: 675
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_size(ref Vector3 value);
	}
}
