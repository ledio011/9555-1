using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200002E RID: 46
	public sealed class Canvas : Behaviour
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002E4 RID: 740 RVA: 0x00007800 File Offset: 0x00005A00
		// (remove) Token: 0x060002E5 RID: 741 RVA: 0x00007818 File Offset: 0x00005A18
		public static event Canvas.WillRenderCanvases willRenderCanvases;

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002E6 RID: 742
		// (set) Token: 0x060002E7 RID: 743
		public extern RenderMode renderMode { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002E8 RID: 744
		public extern bool isRootCanvas { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002E9 RID: 745
		// (set) Token: 0x060002EA RID: 746
		public extern Camera worldCamera { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00007830 File Offset: 0x00005A30
		public Rect pixelRect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_pixelRect(out result);
				return result;
			}
		}

		// Token: 0x060002EC RID: 748
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_pixelRect(out Rect value);

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002ED RID: 749
		// (set) Token: 0x060002EE RID: 750
		public extern float scaleFactor { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002EF RID: 751
		// (set) Token: 0x060002F0 RID: 752
		public extern float referencePixelsPerUnit { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002F1 RID: 753
		// (set) Token: 0x060002F2 RID: 754
		public extern bool overridePixelPerfect { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002F3 RID: 755
		// (set) Token: 0x060002F4 RID: 756
		public extern bool pixelPerfect { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002F5 RID: 757
		// (set) Token: 0x060002F6 RID: 758
		public extern float planeDistance { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002F7 RID: 759
		public extern int renderOrder { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002F8 RID: 760
		// (set) Token: 0x060002F9 RID: 761
		public extern bool overrideSorting { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002FA RID: 762
		// (set) Token: 0x060002FB RID: 763
		public extern int sortingOrder { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002FC RID: 764
		// (set) Token: 0x060002FD RID: 765
		public extern int sortingLayerID { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002FE RID: 766
		public extern int cachedSortingLayerValue { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002FF RID: 767
		// (set) Token: 0x06000300 RID: 768
		public extern string sortingLayerName { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000301 RID: 769
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Material GetDefaultCanvasMaterial();

		// Token: 0x06000302 RID: 770
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Material GetDefaultCanvasTextMaterial();

		// Token: 0x06000303 RID: 771 RVA: 0x00007848 File Offset: 0x00005A48
		private static void SendWillRenderCanvases()
		{
			if (Canvas.willRenderCanvases != null)
			{
				Canvas.willRenderCanvases();
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00007860 File Offset: 0x00005A60
		public static void ForceUpdateCanvases()
		{
			Canvas.SendWillRenderCanvases();
		}

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x06000306 RID: 774
		public delegate void WillRenderCanvases();
	}
}
