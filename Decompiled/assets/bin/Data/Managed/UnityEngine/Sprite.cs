using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200010B RID: 267
	public sealed class Sprite : Object
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00015F44 File Offset: 0x00014144
		public Rect rect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_rect(out result);
				return result;
			}
		}

		// Token: 0x060009CC RID: 2508
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060009CD RID: 2509
		public extern float pixelsPerUnit { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060009CE RID: 2510
		public extern Texture2D texture { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00015F5C File Offset: 0x0001415C
		public Rect textureRect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_textureRect(out result);
				return result;
			}
		}

		// Token: 0x060009D0 RID: 2512
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_textureRect(out Rect value);

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00015F74 File Offset: 0x00014174
		public Vector4 border
		{
			get
			{
				Vector4 result;
				this.INTERNAL_get_border(out result);
				return result;
			}
		}

		// Token: 0x060009D2 RID: 2514
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_border(out Vector4 value);
	}
}
