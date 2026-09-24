using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020003B7 RID: 951
	internal sealed class NativeEventCalls
	{
		// Token: 0x06001C9E RID: 7326
		[MethodImpl(4096)]
		public static extern IntPtr CreateEvent_internal(bool manual, bool initial, string name, out bool created);

		// Token: 0x06001C9F RID: 7327
		[MethodImpl(4096)]
		public static extern bool SetEvent_internal(IntPtr handle);

		// Token: 0x06001CA0 RID: 7328
		[MethodImpl(4096)]
		public static extern void CloseEvent_internal(IntPtr handle);
	}
}
