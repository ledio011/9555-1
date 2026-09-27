using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002C4 RID: 708
	internal class ServerObjectReplySink : IMessageSink
	{
		// Token: 0x0600164E RID: 5710 RVA: 0x0004E238 File Offset: 0x0004C438
		public ServerObjectReplySink(ServerIdentity identity, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._identity = identity;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0004E250 File Offset: 0x0004C450
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._identity.NotifyServerDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0004E270 File Offset: 0x0004C470
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000B6C RID: 2924
		private IMessageSink _replySink;

		// Token: 0x04000B6D RID: 2925
		private ServerIdentity _identity;
	}
}
