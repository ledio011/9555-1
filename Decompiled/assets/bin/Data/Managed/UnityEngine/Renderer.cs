using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000E7 RID: 231
	public class Renderer : Component
	{
		// Token: 0x170001F8 RID: 504
		// (set) Token: 0x06000918 RID: 2328
		internal extern Transform staticBatchRootTransform { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000919 RID: 2329
		internal extern int staticBatchIndex { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x0600091A RID: 2330
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern void SetSubsetIndex(int index, int subSetIndexForMaterial);

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600091B RID: 2331
		// (set) Token: 0x0600091C RID: 2332
		public extern bool enabled { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600091D RID: 2333
		// (set) Token: 0x0600091E RID: 2334
		public extern Material material { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600091F RID: 2335
		// (set) Token: 0x06000920 RID: 2336
		public extern Material sharedMaterial { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000921 RID: 2337
		// (set) Token: 0x06000922 RID: 2338
		public extern Material[] sharedMaterials { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000923 RID: 2339
		// (set) Token: 0x06000924 RID: 2340
		public extern Material[] materials { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00014AA4 File Offset: 0x00012CA4
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_bounds(out result);
				return result;
			}
		}

		// Token: 0x06000926 RID: 2342
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000927 RID: 2343
		public extern int lightmapIndex { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00014ABC File Offset: 0x00012CBC
		public Vector4 lightmapTilingOffset
		{
			get
			{
				Vector4 result;
				this.INTERNAL_get_lightmapTilingOffset(out result);
				return result;
			}
		}

		// Token: 0x06000929 RID: 2345
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_lightmapTilingOffset(out Vector4 value);

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600092A RID: 2346
		public extern int sortingLayerID { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600092B RID: 2347
		// (set) Token: 0x0600092C RID: 2348
		public extern int sortingOrder { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
