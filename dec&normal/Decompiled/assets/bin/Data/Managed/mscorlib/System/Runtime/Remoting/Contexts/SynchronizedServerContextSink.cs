using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000286 RID: 646
	internal class SynchronizedServerContextSink : IMessageSink
	{
		// Token: 0x060014D6 RID: 5334 RVA: 0x00049BC0 File Offset: 0x00047DC0
		public SynchronizedServerContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00049BD8 File Offset: 0x00047DD8
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this._att.AcquireLock();
			replySink = new SynchronizedContextReplySink(replySink, this._att, false);
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00049C04 File Offset: 0x00047E04
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._att.AcquireLock();
			IMessage result;
			try
			{
				result = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				this._att.ReleaseLock();
			}
			return result;
		}

		// Token: 0x04000AC1 RID: 2753
		private IMessageSink _next;

		// Token: 0x04000AC2 RID: 2754
		private SynchronizationAttribute _att;
	}
}
