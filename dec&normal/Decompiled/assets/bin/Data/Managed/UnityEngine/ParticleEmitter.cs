using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000C9 RID: 201
	public sealed class ParticleEmitter : Component
	{
		// Token: 0x170001B8 RID: 440
		// (set) Token: 0x06000823 RID: 2083
		public extern bool emit { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000824 RID: 2084
		// (set) Token: 0x06000825 RID: 2085
		public extern float minEmission { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000826 RID: 2086
		// (set) Token: 0x06000827 RID: 2087
		public extern float maxEmission { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x000130EC File Offset: 0x000112EC
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00013104 File Offset: 0x00011304
		public Vector3 localVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_localVelocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localVelocity(ref value);
			}
		}

		// Token: 0x0600082A RID: 2090
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localVelocity(out Vector3 value);

		// Token: 0x0600082B RID: 2091
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localVelocity(ref Vector3 value);

		// Token: 0x0600082C RID: 2092 RVA: 0x00013110 File Offset: 0x00011310
		public void Emit()
		{
			this.Emit2((int)Random.Range(this.minEmission, this.maxEmission));
		}

		// Token: 0x0600082D RID: 2093
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Emit2(int count);
	}
}
