using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D6 RID: 214
	public sealed class QualitySettings : Object
	{
		// Token: 0x06000876 RID: 2166
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetQualityLevel();

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000877 RID: 2167
		public static extern ColorSpace activeColorSpace { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
