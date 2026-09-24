using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020003B5 RID: 949
	[ComVisible(true)]
	public static class Monitor
	{
		// Token: 0x06001C93 RID: 7315
		[MethodImpl(4096)]
		public static extern void Enter(object obj);

		// Token: 0x06001C94 RID: 7316
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(4096)]
		public static extern void Exit(object obj);

		// Token: 0x06001C95 RID: 7317
		[MethodImpl(4096)]
		private static extern void Monitor_pulse(object obj);

		// Token: 0x06001C96 RID: 7318
		[MethodImpl(4096)]
		private static extern bool Monitor_test_synchronised(object obj);

		// Token: 0x06001C97 RID: 7319 RVA: 0x0006D86C File Offset: 0x0006BA6C
		public static void Pulse(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (!Monitor.Monitor_test_synchronised(obj))
			{
				throw new SynchronizationLockException("Object is not synchronized");
			}
			Monitor.Monitor_pulse(obj);
		}

		// Token: 0x06001C98 RID: 7320
		[MethodImpl(4096)]
		private static extern bool Monitor_wait(object obj, int ms);

		// Token: 0x06001C99 RID: 7321 RVA: 0x0006D89C File Offset: 0x0006BA9C
		public static bool Wait(object obj, int millisecondsTimeout)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", "timeout out of range");
			}
			if (!Monitor.Monitor_test_synchronised(obj))
			{
				throw new SynchronizationLockException("Object is not synchronized");
			}
			return Monitor.Monitor_wait(obj, millisecondsTimeout);
		}
	}
}
