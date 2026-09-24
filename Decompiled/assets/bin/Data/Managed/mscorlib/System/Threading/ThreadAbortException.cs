using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020003BD RID: 957
	[ComVisible(true)]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x06001D27 RID: 7463 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		private ThreadAbortException() : base("Thread was being aborted")
		{
			base.HResult = -2146233040;
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0006E710 File Offset: 0x0006C910
		private ThreadAbortException(SerializationInfo info, StreamingContext sc) : base(info, sc)
		{
		}
	}
}
