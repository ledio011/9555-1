using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200002B RID: 43
	public sealed class SafeWaitHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000032CC File Offset: 0x000014CC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public SafeWaitHandle(IntPtr existingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000032DC File Offset: 0x000014DC
		protected override bool ReleaseHandle()
		{
			NativeEventCalls.CloseEvent_internal(this.handle);
			return true;
		}
	}
}
