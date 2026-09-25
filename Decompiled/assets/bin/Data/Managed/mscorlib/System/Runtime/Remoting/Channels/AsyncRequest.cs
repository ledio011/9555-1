using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000262 RID: 610
	internal class AsyncRequest
	{
		// Token: 0x06001443 RID: 5187 RVA: 0x00046F8C File Offset: 0x0004518C
		public AsyncRequest(IMessage msgRequest, IMessageSink replySink)
		{
			this.ReplySink = replySink;
			this.MsgRequest = msgRequest;
		}

		// Token: 0x04000A7C RID: 2684
		internal IMessageSink ReplySink;

		// Token: 0x04000A7D RID: 2685
		internal IMessage MsgRequest;
	}
}
