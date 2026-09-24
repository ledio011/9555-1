using System;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002C3 RID: 707
	internal class ServerContextTerminatorSink : IMessageSink
	{
		// Token: 0x0600164C RID: 5708 RVA: 0x0004E1DC File Offset: 0x0004C3DC
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (msg is IConstructionCallMessage)
			{
				return ActivationServices.CreateInstanceFromMessage((IConstructionCallMessage)msg);
			}
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			return serverIdentity.SyncObjectProcessMessage(msg);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0004E214 File Offset: 0x0004C414
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			return serverIdentity.AsyncObjectProcessMessage(msg, replySink);
		}
	}
}
