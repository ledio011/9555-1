using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000DF RID: 223
	[Serializable]
	[StructLayout(0)]
	public sealed class RectOffset
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x000140C8 File Offset: 0x000122C8
		public RectOffset()
		{
			this.Init();
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x000140D8 File Offset: 0x000122D8
		internal RectOffset(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000140F0 File Offset: 0x000122F0
		public RectOffset(int left, int right, int top, int bottom)
		{
			this.Init();
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001411C File Offset: 0x0001231C
		~RectOffset()
		{
			if (this.m_SourceStyle == null)
			{
				this.Cleanup();
			}
		}

		// Token: 0x060008CB RID: 2251
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x060008CC RID: 2252
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008CD RID: 2253
		// (set) Token: 0x060008CE RID: 2254
		public extern int left { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008CF RID: 2255
		// (set) Token: 0x060008D0 RID: 2256
		public extern int right { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008D1 RID: 2257
		// (set) Token: 0x060008D2 RID: 2258
		public extern int top { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008D3 RID: 2259
		// (set) Token: 0x060008D4 RID: 2260
		public extern int bottom { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008D5 RID: 2261
		public extern int horizontal { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008D6 RID: 2262
		public extern int vertical { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x060008D7 RID: 2263 RVA: 0x00014158 File Offset: 0x00012358
		public Rect Add(Rect rect)
		{
			return RectOffset.INTERNAL_CALL_Add(this, ref rect);
		}

		// Token: 0x060008D8 RID: 2264
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Rect INTERNAL_CALL_Add(RectOffset self, ref Rect rect);

		// Token: 0x060008D9 RID: 2265 RVA: 0x00014164 File Offset: 0x00012364
		public Rect Remove(Rect rect)
		{
			return RectOffset.INTERNAL_CALL_Remove(this, ref rect);
		}

		// Token: 0x060008DA RID: 2266
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Rect INTERNAL_CALL_Remove(RectOffset self, ref Rect rect);

		// Token: 0x060008DB RID: 2267 RVA: 0x00014170 File Offset: 0x00012370
		public override string ToString()
		{
			return UnityString.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", new object[]
			{
				this.left,
				this.right,
				this.top,
				this.bottom
			});
		}

		// Token: 0x04000356 RID: 854
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000357 RID: 855
		private GUIStyle m_SourceStyle;
	}
}
