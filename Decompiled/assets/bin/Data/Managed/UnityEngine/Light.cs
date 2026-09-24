using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000A4 RID: 164
	public sealed class Light : Behaviour
	{
		// Token: 0x17000182 RID: 386
		// (set) Token: 0x0600071E RID: 1822
		public extern LightType type { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00011A08 File Offset: 0x0000FC08
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x00011A20 File Offset: 0x0000FC20
		public Color color
		{
			get
			{
				Color result;
				this.INTERNAL_get_color(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x06000721 RID: 1825
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x06000722 RID: 1826
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_color(ref Color value);

		// Token: 0x17000184 RID: 388
		// (set) Token: 0x06000723 RID: 1827
		public extern float intensity { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000185 RID: 389
		// (set) Token: 0x06000724 RID: 1828
		public extern float range { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000186 RID: 390
		// (set) Token: 0x06000725 RID: 1829
		public extern float spotAngle { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000187 RID: 391
		// (set) Token: 0x06000726 RID: 1830
		public extern LightRenderMode renderMode { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000188 RID: 392
		// (set) Token: 0x06000727 RID: 1831
		public extern int cullingMask { [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
