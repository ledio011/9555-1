using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002C5 RID: 709
	internal class ServerObjectTerminatorSink : IMessageSink
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x0004E278 File Offset: 0x0004C478
		public ServerObjectTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0004E288 File Offset: 0x0004C488
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			serverIdentity.NotifyServerDynamicSinks(true, msg, false, false);
			IMessage result = this._nextSink.SyncProcessMessage(msg);
			serverIdentity.NotifyServerDynamicSinks(false, msg, false, false);
			return result;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0004E2C4 File Offset: 0x0004C4C4
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			if (serverIdentity.HasServerDynamicSinks)
			{
				serverIdentity.NotifyServerDynamicSinks(true, msg, false, true);
				if (replySink != null)
				{
					replySink = new ServerObjectReplySink(serverIdentity, replySink);
				}
			}
			IMessageCtrl result = this._nextSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null)
			{
				serverIdentity.NotifyServerDynamicSinks(false, msg, true, true);
			}
			return result;
		}

		// Token: 0x04000B6E RID: 2926
		private IMessageSink _nextSink;
	}
}
