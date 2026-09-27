using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000111 RID: 273
	public sealed class SystemInfo
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009E7 RID: 2535
		public static extern int systemMemorySize { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009E8 RID: 2536
		public static extern int graphicsMemorySize { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009E9 RID: 2537
		public static extern int graphicsShaderLevel { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009EA RID: 2538
		public static extern bool supportsRenderTextures { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009EB RID: 2539
		public static extern bool supportsImageEffects { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x060009EC RID: 2540
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool SupportsRenderTextureFormat(RenderTextureFormat format);
	}
}
