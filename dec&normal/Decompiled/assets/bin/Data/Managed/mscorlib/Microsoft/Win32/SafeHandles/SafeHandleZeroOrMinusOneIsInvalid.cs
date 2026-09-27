using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200002A RID: 42
	public abstract class SafeHandleZeroOrMinusOneIsInvalid : SafeHandle, IDisposable
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003290 File Offset: 0x00001490
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandleZeroOrMinusOneIsInvalid(bool ownsHandle) : base((IntPtr)0, ownsHandle)
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000032A0 File Offset: 0x000014A0
		public override bool IsInvalid
		{
			get
			{
				return this.handle == (IntPtr)(-1) || this.handle == (IntPtr)0;
			}
		}
	}
}
