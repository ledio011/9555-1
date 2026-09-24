using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Threading
{
	// Token: 0x020003B3 RID: 947
	public static class Interlocked
	{
		// Token: 0x06001C90 RID: 7312
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern int CompareExchange(ref int location1, int value, int comparand);

		// Token: 0x06001C91 RID: 7313
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern int Increment(ref int location);
	}
}
