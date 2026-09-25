using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000A6 RID: 166
	public sealed class LightmapSettings : Object
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600072B RID: 1835
		// (set) Token: 0x0600072C RID: 1836
		public static extern LightmapData[] lightmaps { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600072D RID: 1837
		// (set) Token: 0x0600072E RID: 1838
		public static extern LightProbes lightProbes { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
