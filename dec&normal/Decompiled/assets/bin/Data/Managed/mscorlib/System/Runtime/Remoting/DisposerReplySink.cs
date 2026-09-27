using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000287 RID: 647
	internal class DisposerReplySink : IMessageSink
	{
		// Token: 0x060014D9 RID: 5337 RVA: 0x00049C50 File Offset: 0x00047E50
		public DisposerReplySink(IMessageSink next, IDisposable disposable)
		{
			this._next = next;
			this._disposable = disposable;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00049C68 File Offset: 0x00047E68
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._disposable.Dispose();
			return this._next.SyncProcessMessage(msg);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00049C84 File Offset: 0x00047E84
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000AC3 RID: 2755
		private IMessageSink _next;

		// Token: 0x04000AC4 RID: 2756
		private IDisposable _disposable;
	}
}
