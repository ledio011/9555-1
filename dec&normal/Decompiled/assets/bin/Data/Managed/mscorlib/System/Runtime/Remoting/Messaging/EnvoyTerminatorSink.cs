using System;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002A8 RID: 680
	[Serializable]
	internal class EnvoyTerminatorSink : IMessageSink
	{
		// Token: 0x06001574 RID: 5492 RVA: 0x0004BBB0 File Offset: 0x00049DB0
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().SyncProcessMessage(msg);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0004BBC4 File Offset: 0x00049DC4
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x04000B1B RID: 2843
		public static EnvoyTerminatorSink Instance = new EnvoyTerminatorSink();
	}
}
