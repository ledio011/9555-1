using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000285 RID: 645
	internal class SynchronizedContextReplySink : IMessageSink
	{
		// Token: 0x060014D3 RID: 5331 RVA: 0x00049B24 File Offset: 0x00047D24
		public SynchronizedContextReplySink(IMessageSink next, SynchronizationAttribute att, bool newLock)
		{
			this._newLock = newLock;
			this._next = next;
			this._att = att;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00049B44 File Offset: 0x00047D44
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00049B4C File Offset: 0x00047D4C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._newLock)
			{
				this._att.AcquireLock();
			}
			else
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
				if (this._newLock)
				{
					this._att.ReleaseLock();
				}
			}
			return result;
		}

		// Token: 0x04000ABE RID: 2750
		private IMessageSink _next;

		// Token: 0x04000ABF RID: 2751
		private bool _newLock;

		// Token: 0x04000AC0 RID: 2752
		private SynchronizationAttribute _att;
	}
}
