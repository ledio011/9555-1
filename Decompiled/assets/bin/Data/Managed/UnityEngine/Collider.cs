using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	public class Collider : Component
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000335 RID: 821
		// (set) Token: 0x06000336 RID: 822
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000337 RID: 823
		public extern Rigidbody attachedRigidbody { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000338 RID: 824
		// (set) Token: 0x06000339 RID: 825
		public extern bool isTrigger { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000A8 RID: 168
		// (set) Token: 0x0600033A RID: 826
		public extern PhysicMaterial material { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000079AC File Offset: 0x00005BAC
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_bounds(out result);
				return result;
			}
		}

		// Token: 0x0600033C RID: 828
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x0600033D RID: 829 RVA: 0x000079C4 File Offset: 0x00005BC4
		private static bool Internal_Raycast(Collider col, Ray ray, out RaycastHit hitInfo, float distance)
		{
			return Collider.INTERNAL_CALL_Internal_Raycast(col, ref ray, out hitInfo, distance);
		}

		// Token: 0x0600033E RID: 830
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_Internal_Raycast(Collider col, ref Ray ray, out RaycastHit hitInfo, float distance);

		// Token: 0x0600033F RID: 831 RVA: 0x000079D0 File Offset: 0x00005BD0
		public bool Raycast(Ray ray, out RaycastHit hitInfo, float distance)
		{
			return Collider.Internal_Raycast(this, ray, out hitInfo, distance);
		}
	}
}
