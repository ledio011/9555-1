using System;
using System.Runtime.InteropServices;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x0200020C RID: 524
	[ComVisible(true)]
	public abstract class CriticalFinalizerObject
	{
		// Token: 0x06001295 RID: 4757 RVA: 0x00045308 File Offset: 0x00043508
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalFinalizerObject()
		{
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00045310 File Offset: 0x00043510
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalFinalizerObject()
		{
		}
	}
}
