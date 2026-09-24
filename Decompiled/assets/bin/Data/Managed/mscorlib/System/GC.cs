using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System
{
	// Token: 0x020000EC RID: 236
	public static class GC
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000946 RID: 2374
		public static extern int MaxGeneration { [MethodImpl(4096)] get; }

		// Token: 0x06000947 RID: 2375
		[MethodImpl(4096)]
		private static extern void InternalCollect(int generation);

		// Token: 0x06000948 RID: 2376 RVA: 0x000241F8 File Offset: 0x000223F8
		public static void Collect()
		{
			GC.InternalCollect(GC.MaxGeneration);
		}

		// Token: 0x06000949 RID: 2377
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern void SuppressFinalize(object obj);
	}
}
