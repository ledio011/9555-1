using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000086 RID: 134
	public sealed class GUITexture : GUIElement
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00010CFC File Offset: 0x0000EEFC
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00010D14 File Offset: 0x0000EF14
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

		// Token: 0x0600069F RID: 1695
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x060006A0 RID: 1696
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_color(ref Color value);

		// Token: 0x17000163 RID: 355
		// (set) Token: 0x060006A1 RID: 1697
		public extern Texture texture { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00010D20 File Offset: 0x0000EF20
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00010D38 File Offset: 0x0000EF38
		public Rect pixelInset
		{
			get
			{
				Rect result;
				this.INTERNAL_get_pixelInset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_pixelInset(ref value);
			}
		}

		// Token: 0x060006A4 RID: 1700
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_pixelInset(out Rect value);

		// Token: 0x060006A5 RID: 1701
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_pixelInset(ref Rect value);
	}
}
