using System;

namespace System.Threading
{
	// Token: 0x020003BE RID: 958
	public static class ThreadPool
	{
		// Token: 0x06001D29 RID: 7465 RVA: 0x0006E71C File Offset: 0x0006C91C
		public static bool QueueUserWorkItem(WaitCallback callBack, object state)
		{
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			return callBack.BeginInvoke(state, null, null) != null;
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0006E750 File Offset: 0x0006C950
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout < -1");
			}
			if (millisecondsTimeOutInterval > 2147483647L)
			{
				throw new NotSupportedException("Timeout is too big. Maximum is Int32.MaxValue");
			}
			TimeSpan timeout = new TimeSpan(0, 0, 0, 0, (int)millisecondsTimeOutInterval);
			RegisteredWaitHandle registeredWaitHandle = new RegisteredWaitHandle(waitObject, callBack, state, timeout, executeOnlyOnce);
			ThreadPool.QueueUserWorkItem(new WaitCallback(registeredWaitHandle.Wait), null);
			return registeredWaitHandle;
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0006E7BC File Offset: 0x0006C9BC
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (long)timeout.TotalMilliseconds, executeOnlyOnce);
		}
	}
}
