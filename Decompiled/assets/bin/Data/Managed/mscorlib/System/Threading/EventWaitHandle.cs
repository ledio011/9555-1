using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020003B1 RID: 945
	[ComVisible(true)]
	public class EventWaitHandle : WaitHandle
	{
		// Token: 0x06001C84 RID: 7300 RVA: 0x0006D70C File Offset: 0x0006B90C
		public EventWaitHandle(bool initialState, EventResetMode mode)
		{
			bool manual = this.IsManualReset(mode);
			bool flag;
			this.Handle = NativeEventCalls.CreateEvent_internal(manual, initialState, null, out flag);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0006D738 File Offset: 0x0006B938
		private bool IsManualReset(EventResetMode mode)
		{
			if (mode < EventResetMode.AutoReset || mode > EventResetMode.ManualReset)
			{
				throw new ArgumentException("mode");
			}
			return mode == EventResetMode.ManualReset;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0006D758 File Offset: 0x0006B958
		public bool Set()
		{
			base.CheckDisposed();
			return NativeEventCalls.SetEvent_internal(this.Handle);
		}
	}
}
