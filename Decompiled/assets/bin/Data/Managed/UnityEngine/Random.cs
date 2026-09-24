using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D9 RID: 217
	public sealed class Random
	{
		// Token: 0x06000893 RID: 2195
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float Range(float min, float max);

		// Token: 0x06000894 RID: 2196 RVA: 0x00013AE4 File Offset: 0x00011CE4
		public static int Range(int min, int max)
		{
			return Random.RandomRangeInt(min, max);
		}

		// Token: 0x06000895 RID: 2197
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int RandomRangeInt(int min, int max);

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000896 RID: 2198
		public static extern float value { [WrapperlessIcall] [MethodImpl(4096)] get; }
	}
}
