using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000EB RID: 235
	public sealed class RenderTexture : Texture
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x00014AF8 File Offset: 0x00012CF8
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = format;
			RenderTexture.Internal_SetSRGBReadWrite(this, QualitySettings.activeColorSpace == ColorSpace.Linear);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00014B34 File Offset: 0x00012D34
		public RenderTexture(int width, int height, int depth)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = RenderTextureFormat.Default;
			RenderTexture.Internal_SetSRGBReadWrite(this, QualitySettings.activeColorSpace == ColorSpace.Linear);
		}

		// Token: 0x06000935 RID: 2357
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateRenderTexture([Writable] RenderTexture rt);

		// Token: 0x06000936 RID: 2358
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern RenderTexture GetTemporary(int width, int height, [DefaultValue("0")] int depthBuffer, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [DefaultValue("1")] int antiAliasing);

		// Token: 0x06000937 RID: 2359 RVA: 0x00014B6C File Offset: 0x00012D6C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			int antiAliasing = 1;
			RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00014B88 File Offset: 0x00012D88
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			int antiAliasing = 1;
			RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default;
			RenderTextureFormat format = RenderTextureFormat.Default;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00014BA8 File Offset: 0x00012DA8
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			int antiAliasing = 1;
			RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default;
			RenderTextureFormat format = RenderTextureFormat.Default;
			int depthBuffer = 0;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing);
		}

		// Token: 0x0600093A RID: 2362
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void ReleaseTemporary(RenderTexture temp);

		// Token: 0x0600093B RID: 2363
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetWidth(RenderTexture mono);

		// Token: 0x0600093C RID: 2364
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetWidth(RenderTexture mono, int width);

		// Token: 0x0600093D RID: 2365
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetHeight(RenderTexture mono);

		// Token: 0x0600093E RID: 2366
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetHeight(RenderTexture mono, int width);

		// Token: 0x0600093F RID: 2367
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetSRGBReadWrite(RenderTexture mono, bool sRGB);

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00014BC8 File Offset: 0x00012DC8
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00014BD0 File Offset: 0x00012DD0
		public override int width
		{
			get
			{
				return RenderTexture.Internal_GetWidth(this);
			}
			set
			{
				RenderTexture.Internal_SetWidth(this, value);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00014BDC File Offset: 0x00012DDC
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00014BE4 File Offset: 0x00012DE4
		public override int height
		{
			get
			{
				return RenderTexture.Internal_GetHeight(this);
			}
			set
			{
				RenderTexture.Internal_SetHeight(this, value);
			}
		}

		// Token: 0x17000208 RID: 520
		// (set) Token: 0x06000944 RID: 2372
		public extern int depth { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000209 RID: 521
		// (set) Token: 0x06000945 RID: 2373
		public extern bool isPowerOfTwo { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000946 RID: 2374
		// (set) Token: 0x06000947 RID: 2375
		public extern RenderTextureFormat format { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700020B RID: 523
		// (set) Token: 0x06000948 RID: 2376
		public extern bool useMipMap { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000949 RID: 2377 RVA: 0x00014BF0 File Offset: 0x00012DF0
		public void Release()
		{
			RenderTexture.INTERNAL_CALL_Release(this);
		}

		// Token: 0x0600094A RID: 2378
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Release(RenderTexture self);

		// Token: 0x0600094B RID: 2379 RVA: 0x00014BF8 File Offset: 0x00012DF8
		public bool IsCreated()
		{
			return RenderTexture.INTERNAL_CALL_IsCreated(this);
		}

		// Token: 0x0600094C RID: 2380
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_IsCreated(RenderTexture self);

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600094D RID: 2381
		// (set) Token: 0x0600094E RID: 2382
		public static extern RenderTexture active { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
