using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020003D7 RID: 983
	[ComVisible(true)]
	[Serializable]
	public class UnhandledExceptionEventArgs : EventArgs
	{
		// Token: 0x06001E87 RID: 7815 RVA: 0x00071E4C File Offset: 0x0007004C
		public UnhandledExceptionEventArgs(object exception, bool isTerminating)
		{
			this.exception = exception;
			this.m_isTerminating = isTerminating;
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x00071E64 File Offset: 0x00070064
		public object ExceptionObject
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00071E6C File Offset: 0x0007006C
		public bool IsTerminating
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_isTerminating;
			}
		}

		// Token: 0x04000FB1 RID: 4017
		private object exception;

		// Token: 0x04000FB2 RID: 4018
		private bool m_isTerminating;
	}
}
