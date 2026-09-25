using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	public sealed class CharacterController : Collider
	{
		// Token: 0x06000320 RID: 800 RVA: 0x0000792C File Offset: 0x00005B2C
		public CollisionFlags Move(Vector3 motion)
		{
			return CharacterController.INTERNAL_CALL_Move(this, ref motion);
		}

		// Token: 0x06000321 RID: 801
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern CollisionFlags INTERNAL_CALL_Move(CharacterController self, ref Vector3 motion);

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00007938 File Offset: 0x00005B38
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_velocity(out result);
				return result;
			}
		}

		// Token: 0x06000323 RID: 803
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000324 RID: 804
		public extern float radius { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000325 RID: 805
		public extern float height { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00007950 File Offset: 0x00005B50
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_center(out result);
				return result;
			}
		}

		// Token: 0x06000327 RID: 807
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000328 RID: 808
		public extern float slopeLimit { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000329 RID: 809
		public extern float stepOffset { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
