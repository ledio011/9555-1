using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200004A RID: 74
	public sealed class Display
	{
		// Token: 0x0600039A RID: 922 RVA: 0x00008200 File Offset: 0x00006400
		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00008214 File Offset: 0x00006414
		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00008224 File Offset: 0x00006424
		// Note: this type is marked as 'beforefieldinit'.
		static Display()
		{
			Display.onDisplaysUpdated = null;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600039D RID: 925 RVA: 0x0000824C File Offset: 0x0000644C
		// (remove) Token: 0x0600039E RID: 926 RVA: 0x00008264 File Offset: 0x00006464
		public static event Display.DisplaysUpdatedDelegate onDisplaysUpdated;

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000827C File Offset: 0x0000647C
		public int renderingWidth
		{
			get
			{
				int result = 0;
				int num = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out result, out num);
				return result;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000082A0 File Offset: 0x000064A0
		public int renderingHeight
		{
			get
			{
				int num = 0;
				int result = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out result);
				return result;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000082C4 File Offset: 0x000064C4
		public int systemWidth
		{
			get
			{
				int result = 0;
				int num = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out result, out num);
				return result;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000082E8 File Offset: 0x000064E8
		public int systemHeight
		{
			get
			{
				int num = 0;
				int result = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out result);
				return result;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000830C File Offset: 0x0000650C
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer result;
				RenderBuffer renderBuffer;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out result, out renderBuffer);
				return result;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000832C File Offset: 0x0000652C
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer result;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out result);
				return result;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000834C File Offset: 0x0000654C
		public void Activate()
		{
			Display.ActivateDisplayImpl(this.nativeDisplay);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000835C File Offset: 0x0000655C
		public void SetRenderingResolution(int w, int h)
		{
			Display.SetRenderingResolutionImpl(this.nativeDisplay, w, h);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000836C File Offset: 0x0000656C
		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00008374 File Offset: 0x00006574
		private static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			Display.displays = new Display[nativeDisplay.Length];
			for (int i = 0; i < nativeDisplay.Length; i++)
			{
				Display.displays[i] = new Display(nativeDisplay[i]);
			}
			Display._mainDisplay = Display.displays[0];
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000083C0 File Offset: 0x000065C0
		private static void FireDisplaysUpdated()
		{
			if (Display.onDisplaysUpdated != null)
			{
				Display.onDisplaysUpdated();
			}
		}

		// Token: 0x060003AA RID: 938
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x060003AB RID: 939
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x060003AC RID: 940
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x060003AD RID: 941
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		// Token: 0x060003AE RID: 942
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void ActivateDisplayImpl(IntPtr nativeDisplay);

		// Token: 0x04000075 RID: 117
		internal IntPtr nativeDisplay;

		// Token: 0x04000076 RID: 118
		public static Display[] displays = new Display[]
		{
			new Display()
		};

		// Token: 0x04000077 RID: 119
		private static Display _mainDisplay = Display.displays[0];

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x060003B0 RID: 944
		public delegate void DisplaysUpdatedDelegate();
	}
}
