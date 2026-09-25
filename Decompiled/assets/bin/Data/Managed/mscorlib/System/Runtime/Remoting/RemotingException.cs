using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x020002D6 RID: 726
	[ComVisible(true)]
	[Serializable]
	public class RemotingException : SystemException
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x0004F908 File Offset: 0x0004DB08
		public RemotingException()
		{
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0004F910 File Offset: 0x0004DB10
		public RemotingException(string message) : base(message)
		{
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0004F91C File Offset: 0x0004DB1C
		protected RemotingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0004F928 File Offset: 0x0004DB28
		public RemotingException(string message, Exception InnerException) : base(message, InnerException)
		{
		}
	}
}
