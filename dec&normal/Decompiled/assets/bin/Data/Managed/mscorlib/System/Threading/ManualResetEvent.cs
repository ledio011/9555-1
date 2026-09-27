using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020003B4 RID: 948
	[ComVisible(true)]
	public sealed class ManualResetEvent : EventWaitHandle
	{
		// Token: 0x06001C92 RID: 7314 RVA: 0x0006D860 File Offset: 0x0006BA60
		public ManualResetEvent(bool initialState) : base(initialState, EventResetMode.ManualReset)
		{
		}
	}
}
