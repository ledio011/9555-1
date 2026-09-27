using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	public sealed class CapsuleCollider : Collider
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00007908 File Offset: 0x00005B08
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00007920 File Offset: 0x00005B20
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

		// Token: 0x0600031A RID: 794
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x0600031B RID: 795
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x17000097 RID: 151
		// (set) Token: 0x0600031C RID: 796
		public extern float radius { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600031D RID: 797
		// (set) Token: 0x0600031E RID: 798
		public extern float height { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000099 RID: 153
		// (set) Token: 0x0600031F RID: 799
		public extern int direction { [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
