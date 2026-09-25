using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020003C2 RID: 962
	[ComVisible(true)]
	[Serializable]
	public class ThreadStateException : SystemException
	{
		// Token: 0x06001D30 RID: 7472 RVA: 0x0006E7D0 File Offset: 0x0006C9D0
		public ThreadStateException() : base("Thread State Error")
		{
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0006E7E0 File Offset: 0x0006C9E0
		public ThreadStateException(string message) : base(message)
		{
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0006E7EC File Offset: 0x0006C9EC
		protected ThreadStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
