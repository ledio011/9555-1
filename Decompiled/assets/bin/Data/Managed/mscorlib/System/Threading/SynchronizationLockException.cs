using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020003BB RID: 955
	[ComVisible(true)]
	[Serializable]
	public class SynchronizationLockException : SystemException
	{
		// Token: 0x06001CA9 RID: 7337 RVA: 0x0006DB40 File Offset: 0x0006BD40
		public SynchronizationLockException() : base("Synchronization Error")
		{
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0006DB50 File Offset: 0x0006BD50
		public SynchronizationLockException(string message) : base(message)
		{
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0006DB5C File Offset: 0x0006BD5C
		protected SynchronizationLockException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
