using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000272 RID: 626
	internal class ClientIdentity : Identity
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x00047D64 File Offset: 0x00045F64
		public ClientIdentity(string objectUri, ObjRef objRef) : base(objectUri)
		{
			this._objRef = objRef;
			IMessageSink envoySink;
			if (this._objRef.EnvoyInfo != null)
			{
				IMessageSink envoySinks = this._objRef.EnvoyInfo.EnvoySinks;
				envoySink = envoySinks;
			}
			else
			{
				envoySink = null;
			}
			this._envoySink = envoySink;
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00047DB0 File Offset: 0x00045FB0
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x00047DC4 File Offset: 0x00045FC4
		public MarshalByRefObject ClientProxy
		{
			get
			{
				return (MarshalByRefObject)this._proxyReference.Target;
			}
			set
			{
				this._proxyReference = new WeakReference(value);
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00047DD4 File Offset: 0x00045FD4
		public override ObjRef CreateObjRef(Type requestedType)
		{
			return this._objRef;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00047DDC File Offset: 0x00045FDC
		public string TargetUri
		{
			get
			{
				return this._objRef.URI;
			}
		}

		// Token: 0x04000A92 RID: 2706
		private WeakReference _proxyReference;
	}
}
