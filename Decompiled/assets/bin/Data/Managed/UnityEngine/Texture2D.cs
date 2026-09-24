using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200011D RID: 285
	public sealed class Texture2D : Texture
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x00019694 File Offset: 0x00017894
		public Texture2D(int width, int height, TextureFormat format, bool mipmap)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, false, IntPtr.Zero);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000196B0 File Offset: 0x000178B0
		public Texture2D(int width, int height, TextureFormat format, bool mipmap, bool linear)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, linear, IntPtr.Zero);
		}

		// Token: 0x06000A75 RID: 2677
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] Texture2D mono, int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex);

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A76 RID: 2678
		public static extern Texture2D whiteTexture { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000A77 RID: 2679 RVA: 0x000196CC File Offset: 0x000178CC
		public void SetPixel(int x, int y, Color color)
		{
			Texture2D.INTERNAL_CALL_SetPixel(this, x, y, ref color);
		}

		// Token: 0x06000A78 RID: 2680
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetPixel(Texture2D self, int x, int y, ref Color color);

		// Token: 0x06000A79 RID: 2681
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Color GetPixelBilinear(float u, float v);

		// Token: 0x06000A7A RID: 2682 RVA: 0x000196D8 File Offset: 0x000178D8
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			int miplevel = 0;
			this.SetPixels(colors, miplevel);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000196F0 File Offset: 0x000178F0
		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			this.SetPixels(0, 0, num, num2, colors, miplevel);
		}

		// Token: 0x06000A7C RID: 2684
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x06000A7D RID: 2685
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable);

		// Token: 0x06000A7E RID: 2686 RVA: 0x00019734 File Offset: 0x00017934
		[ExcludeFromDocs]
		public void Apply()
		{
			bool makeNoLongerReadable = false;
			bool updateMipmaps = true;
			this.Apply(updateMipmaps, makeNoLongerReadable);
		}
	}
}
