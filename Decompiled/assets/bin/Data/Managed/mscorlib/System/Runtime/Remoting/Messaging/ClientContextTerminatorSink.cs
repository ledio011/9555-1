using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002A4 RID: 676
	internal class ClientContextTerminatorSink : IMessageSink
	{
		// Token: 0x06001557 RID: 5463 RVA: 0x0004B5BC File Offset: 0x000497BC
		public ClientContextTerminatorSink(Context ctx)
		{
			this._context = ctx;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0004B5CC File Offset: 0x000497CC
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(true, msg, true, false);
			this._context.NotifyDynamicSinks(true, msg, true, false);
			IMessage result;
			if (msg is IConstructionCallMessage)
			{
				result = ActivationServices.RemoteActivate((IConstructionCallMessage)msg);
			}
			else
			{
				Identity messageTargetIdentity = RemotingServices.GetMessageTargetIdentity(msg);
				result = messageTargetIdentity.ChannelSink.SyncProcessMessage(msg);
			}
			Context.NotifyGlobalDynamicSinks(false, msg, true, false);
			this._context.NotifyDynamicSinks(false, msg, true, false);
			return result;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0004B63C File Offset: 0x0004983C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks)
			{
				Context.NotifyGlobalDynamicSinks(true, msg, true, true);
				this._context.NotifyDynamicSinks(true, msg, true, true);
				if (replySink != null)
				{
					replySink = new ClientContextReplySink(this._context, replySink);
				}
			}
			Identity messageTargetIdentity = RemotingServices.GetMessageTargetIdentity(msg);
			IMessageCtrl result = messageTargetIdentity.ChannelSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null && (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks))
			{
				Context.NotifyGlobalDynamicSinks(false, msg, true, true);
				this._context.NotifyDynamicSinks(false, msg, true, true);
			}
			return result;
		}

		// Token: 0x04000B0F RID: 2831
		private Context _context;
	}
}
