using System;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002A3 RID: 675
	internal class ClientContextReplySink : IMessageSink
	{
		// Token: 0x06001554 RID: 5460 RVA: 0x0004B574 File Offset: 0x00049774
		public ClientContextReplySink(Context ctx, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._context = ctx;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0004B58C File Offset: 0x0004978C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(false, msg, true, true);
			this._context.NotifyDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0004B5B4 File Offset: 0x000497B4
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000B0D RID: 2829
		private IMessageSink _replySink;

		// Token: 0x04000B0E RID: 2830
		private Context _context;
	}
}
