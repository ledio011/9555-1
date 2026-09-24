using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020003B9 RID: 953
	[ComVisible(true)]
	public sealed class RegisteredWaitHandle : MarshalByRefObject
	{
		// Token: 0x06001CA5 RID: 7333 RVA: 0x0006D940 File Offset: 0x0006BB40
		internal RegisteredWaitHandle(WaitHandle waitObject, WaitOrTimerCallback callback, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			this._waitObject = waitObject;
			this._callback = callback;
			this._state = state;
			this._timeout = timeout;
			this._executeOnlyOnce = executeOnlyOnce;
			this._finalEvent = null;
			this._cancelEvent = new ManualResetEvent(false);
			this._callsInProcess = 0;
			this._unregistered = false;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0006D99C File Offset: 0x0006BB9C
		internal void Wait(object state)
		{
			try
			{
				WaitHandle[] waitHandles = new WaitHandle[]
				{
					this._waitObject,
					this._cancelEvent
				};
				do
				{
					int num = WaitHandle.WaitAny(waitHandles, this._timeout, false);
					if (!this._unregistered)
					{
						lock (this)
						{
							this._callsInProcess++;
						}
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoCallBack), num == 258);
					}
				}
				while (!this._unregistered && !this._executeOnlyOnce);
			}
			catch
			{
			}
			lock (this)
			{
				this._unregistered = true;
				if (this._callsInProcess == 0 && this._finalEvent != null)
				{
					NativeEventCalls.SetEvent_internal(this._finalEvent.Handle);
				}
			}
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0006DAA8 File Offset: 0x0006BCA8
		private void DoCallBack(object timedOut)
		{
			if (this._callback != null)
			{
				this._callback(this._state, (bool)timedOut);
			}
			lock (this)
			{
				this._callsInProcess--;
				if (this._unregistered && this._callsInProcess == 0 && this._finalEvent != null)
				{
					NativeEventCalls.SetEvent_internal(this._finalEvent.Handle);
				}
			}
		}

		// Token: 0x04000F1B RID: 3867
		private WaitHandle _waitObject;

		// Token: 0x04000F1C RID: 3868
		private WaitOrTimerCallback _callback;

		// Token: 0x04000F1D RID: 3869
		private TimeSpan _timeout;

		// Token: 0x04000F1E RID: 3870
		private object _state;

		// Token: 0x04000F1F RID: 3871
		private bool _executeOnlyOnce;

		// Token: 0x04000F20 RID: 3872
		private WaitHandle _finalEvent;

		// Token: 0x04000F21 RID: 3873
		private ManualResetEvent _cancelEvent;

		// Token: 0x04000F22 RID: 3874
		private int _callsInProcess;

		// Token: 0x04000F23 RID: 3875
		private bool _unregistered;
	}
}
