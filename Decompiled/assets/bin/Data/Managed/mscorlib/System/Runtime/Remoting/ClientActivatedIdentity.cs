using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000271 RID: 625
	internal class ClientActivatedIdentity : ServerIdentity
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x00047C7C File Offset: 0x00045E7C
		public ClientActivatedIdentity(string objectUri, Type objectType) : base(objectUri, null, objectType)
		{
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x00047C88 File Offset: 0x00045E88
		public MarshalByRefObject GetServerObject()
		{
			return this._serverObject;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00047C90 File Offset: 0x00045E90
		public void SetClientProxy(MarshalByRefObject obj)
		{
			this._targetThis = obj;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00047C9C File Offset: 0x00045E9C
		public override void OnLifetimeExpired()
		{
			base.OnLifetimeExpired();
			RemotingServices.DisposeIdentity(this);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00047CAC File Offset: 0x00045EAC
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain((!flag) ? this._serverObject : this._targetThis, flag);
			}
			return this._serverSink.SyncProcessMessage(msg);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00047D08 File Offset: 0x00045F08
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain((!flag) ? this._serverObject : this._targetThis, flag);
			}
			return this._serverSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x04000A91 RID: 2705
		private MarshalByRefObject _targetThis;
	}
}
