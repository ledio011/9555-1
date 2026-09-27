using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020003B6 RID: 950
	[ComVisible(true)]
	public sealed class Mutex : WaitHandle
	{
		// Token: 0x06001C9A RID: 7322 RVA: 0x0006D8F0 File Offset: 0x0006BAF0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Mutex(bool initiallyOwned)
		{
			bool flag;
			this.Handle = Mutex.CreateMutex_internal(initiallyOwned, null, out flag);
		}

		// Token: 0x06001C9B RID: 7323
		[MethodImpl(4096)]
		private static extern IntPtr CreateMutex_internal(bool initiallyOwned, string name, out bool created);

		// Token: 0x06001C9C RID: 7324
		[MethodImpl(4096)]
		private static extern bool ReleaseMutex_internal(IntPtr handle);

		// Token: 0x06001C9D RID: 7325 RVA: 0x0006D914 File Offset: 0x0006BB14
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void ReleaseMutex()
		{
			if (!Mutex.ReleaseMutex_internal(this.Handle))
			{
				throw new ApplicationException("Mutex is not owned");
			}
		}
	}
}
