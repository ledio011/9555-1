using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200011C RID: 284
	public class Texture : Object
	{
		// Token: 0x06000A68 RID: 2664
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetWidth(Texture mono);

		// Token: 0x06000A69 RID: 2665
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetHeight(Texture mono);

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00019654 File Offset: 0x00017854
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x0001965C File Offset: 0x0001785C
		public virtual int width
		{
			get
			{
				return Texture.Internal_GetWidth(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00019668 File Offset: 0x00017868
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00019670 File Offset: 0x00017870
		public virtual int height
		{
			get
			{
				return Texture.Internal_GetHeight(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		// Token: 0x17000248 RID: 584
		// (set) Token: 0x06000A6E RID: 2670
		public extern FilterMode filterMode { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000249 RID: 585
		// (set) Token: 0x06000A6F RID: 2671
		public extern int anisoLevel { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700024A RID: 586
		// (set) Token: 0x06000A70 RID: 2672
		public extern TextureWrapMode wrapMode { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x06000A71 RID: 2673
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_GetTexelSize(Texture tex, out Vector2 output);

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001967C File Offset: 0x0001787C
		public Vector2 texelSize
		{
			get
			{
				Vector2 result;
				Texture.Internal_GetTexelSize(this, out result);
				return result;
			}
		}
	}
}
