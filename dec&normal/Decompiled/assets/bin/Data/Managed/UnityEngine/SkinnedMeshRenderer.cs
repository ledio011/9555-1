using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000102 RID: 258
	public class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000993 RID: 2451
		// (set) Token: 0x06000994 RID: 2452
		public extern Transform[] bones { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000995 RID: 2453
		// (set) Token: 0x06000996 RID: 2454
		public extern Transform rootBone { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000997 RID: 2455
		// (set) Token: 0x06000998 RID: 2456
		public extern SkinQuality quality { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000999 RID: 2457
		// (set) Token: 0x0600099A RID: 2458
		public extern Mesh sharedMesh { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001547C File Offset: 0x0001367C
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x00015480 File Offset: 0x00013680
		[Obsolete("Has no effect.")]
		public bool skinNormals
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600099D RID: 2461
		// (set) Token: 0x0600099E RID: 2462
		public extern bool updateWhenOffscreen { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00015484 File Offset: 0x00013684
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0001549C File Offset: 0x0001369C
		public Bounds localBounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_localBounds(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_localBounds(ref value);
			}
		}

		// Token: 0x060009A1 RID: 2465
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x060009A2 RID: 2466
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x060009A3 RID: 2467
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void BakeMesh(Mesh mesh);

		// Token: 0x060009A4 RID: 2468
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern float GetBlendShapeWeight(int index);

		// Token: 0x060009A5 RID: 2469
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetBlendShapeWeight(int index, float value);
	}
}
