using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000F6 RID: 246
	public sealed class Screen
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000977 RID: 2423
		public static extern int width { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000978 RID: 2424
		public static extern int height { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000979 RID: 2425
		public static extern float dpi { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
