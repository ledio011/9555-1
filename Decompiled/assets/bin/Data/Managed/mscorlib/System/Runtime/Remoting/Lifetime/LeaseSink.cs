using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000295 RID: 661
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x06001515 RID: 5397 RVA: 0x0004A460 File Offset: 0x00048660
		public LeaseSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0004A470 File Offset: 0x00048670
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.RenewLease(msg);
			return this._nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0004A488 File Offset: 0x00048688
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.RenewLease(msg);
			return this._nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0004A4A0 File Offset: 0x000486A0
		private void RenewLease(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			ILease lease = serverIdentity.Lease;
			if (lease != null && lease.CurrentLeaseTime < lease.RenewOnCallTime)
			{
				lease.Renew(lease.RenewOnCallTime);
			}
		}

		// Token: 0x04000AD8 RID: 2776
		private IMessageSink _nextSink;
	}
}
