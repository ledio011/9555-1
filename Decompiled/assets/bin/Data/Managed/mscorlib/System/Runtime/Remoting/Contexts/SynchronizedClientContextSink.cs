using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000284 RID: 644
	internal class SynchronizedClientContextSink : IMessageSink
	{
		// Token: 0x060014D0 RID: 5328 RVA: 0x00049A64 File Offset: 0x00047C64
		public SynchronizedClientContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00049A7C File Offset: 0x00047C7C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
				replySink = new SynchronizedContextReplySink(replySink, this._att, true);
			}
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00049AB8 File Offset: 0x00047CB8
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
			}
			IMessage result;
			try
			{
				result = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				if (this._att.IsReEntrant)
				{
					this._att.AcquireLock();
				}
			}
			return result;
		}

		// Token: 0x04000ABC RID: 2748
		private IMessageSink _next;

		// Token: 0x04000ABD RID: 2749
		private SynchronizationAttribute _att;
	}
}
