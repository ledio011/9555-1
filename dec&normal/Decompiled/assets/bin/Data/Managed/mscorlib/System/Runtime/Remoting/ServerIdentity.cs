using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;

namespace System.Runtime.Remoting
{
	// Token: 0x020002D9 RID: 729
	internal abstract class ServerIdentity : Identity
	{
		// Token: 0x060016F3 RID: 5875 RVA: 0x00050BFC File Offset: 0x0004EDFC
		public ServerIdentity(string objectUri, Context context, Type objectType) : base(objectUri)
		{
			this._objectType = objectType;
			this._context = context;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00050C14 File Offset: 0x0004EE14
		public Type ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00050C1C File Offset: 0x0004EE1C
		public void StartTrackingLifetime(ILease lease)
		{
			if (lease != null && lease.CurrentState == LeaseState.Null)
			{
				lease = null;
			}
			if (lease != null)
			{
				if (!(lease is Lease))
				{
					lease = new Lease();
				}
				this._lease = (Lease)lease;
				LifetimeServices.TrackLifetime(this);
			}
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00050C5C File Offset: 0x0004EE5C
		public virtual void OnLifetimeExpired()
		{
			this.DisposeServerObject();
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00050C64 File Offset: 0x0004EE64
		public override ObjRef CreateObjRef(Type requestedType)
		{
			if (this._objRef != null)
			{
				this._objRef.UpdateChannelInfo();
				return this._objRef;
			}
			if (requestedType == null)
			{
				requestedType = this._objectType;
			}
			this._objRef = new ObjRef();
			this._objRef.TypeInfo = new TypeInfo(requestedType);
			this._objRef.URI = this._objectUri;
			if (this._envoySink != null && !(this._envoySink is EnvoyTerminatorSink))
			{
				this._objRef.EnvoyInfo = new EnvoyInfo(this._envoySink);
			}
			return this._objRef;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00050D00 File Offset: 0x0004EF00
		public void AttachServerObject(MarshalByRefObject serverObject, Context context)
		{
			this.DisposeServerObject();
			this._context = context;
			this._serverObject = serverObject;
			if (RemotingServices.IsTransparentProxy(serverObject))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(serverObject);
				if (realProxy.ObjectIdentity == null)
				{
					realProxy.ObjectIdentity = this;
				}
			}
			else
			{
				if (this._objectType.IsContextful)
				{
					this._envoySink = context.CreateEnvoySink(serverObject);
				}
				this._serverObject.ObjectIdentity = this;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00050D74 File Offset: 0x0004EF74
		public Lease Lease
		{
			get
			{
				return this._lease;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x00050D7C File Offset: 0x0004EF7C
		// (set) Token: 0x060016FB RID: 5883 RVA: 0x00050D84 File Offset: 0x0004EF84
		public Context Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x060016FC RID: 5884
		public abstract IMessage SyncObjectProcessMessage(IMessage msg);

		// Token: 0x060016FD RID: 5885
		public abstract IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x060016FE RID: 5886 RVA: 0x00050D90 File Offset: 0x0004EF90
		protected void DisposeServerObject()
		{
			if (this._serverObject != null)
			{
				MarshalByRefObject serverObject = this._serverObject;
				this._serverObject.ObjectIdentity = null;
				this._serverObject = null;
				this._serverSink = null;
				TrackingServices.NotifyDisconnectedObject(serverObject);
			}
		}

		// Token: 0x04000BC8 RID: 3016
		protected Type _objectType;

		// Token: 0x04000BC9 RID: 3017
		protected MarshalByRefObject _serverObject;

		// Token: 0x04000BCA RID: 3018
		protected IMessageSink _serverSink;

		// Token: 0x04000BCB RID: 3019
		protected Context _context;

		// Token: 0x04000BCC RID: 3020
		protected Lease _lease;
	}
}
