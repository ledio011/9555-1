using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000108 RID: 264
	[ComVisible(true)]
	public interface IAsyncResult
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000A7F RID: 2687
		WaitHandle AsyncWaitHandle { get; }
	}
}
