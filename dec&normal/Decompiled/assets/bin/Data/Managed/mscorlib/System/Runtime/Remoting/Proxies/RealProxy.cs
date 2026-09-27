using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020002D2 RID: 722
	[ComVisible(true)]
	public abstract class RealProxy
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x0004EC28 File Offset: 0x0004CE28
		protected RealProxy(Type classToProxy) : this(classToProxy, IntPtr.Zero, null)
		{
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0004EC38 File Offset: 0x0004CE38
		internal RealProxy(Type classToProxy, ClientIdentity identity) : this(classToProxy, IntPtr.Zero, null)
		{
			this._objectIdentity = identity;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0004EC50 File Offset: 0x0004CE50
		protected RealProxy(Type classToProxy, IntPtr stub, object stubData)
		{
			if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
			{
				throw new ArgumentException("object must be MarshalByRef");
			}
			this.class_to_proxy = classToProxy;
			if (stub != IntPtr.Zero)
			{
				throw new NotSupportedException("stub is not used in Mono");
			}
		}

		// Token: 0x0600168C RID: 5772
		[MethodImpl(4096)]
		private static extern Type InternalGetProxyType(object transparentProxy);

		// Token: 0x0600168D RID: 5773 RVA: 0x0004ECB0 File Offset: 0x0004CEB0
		public Type GetProxiedType()
		{
			if (this._objTP != null)
			{
				return RealProxy.InternalGetProxyType(this._objTP);
			}
			if (this.class_to_proxy.IsInterface)
			{
				return typeof(MarshalByRefObject);
			}
			return this.class_to_proxy;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0004ECEC File Offset: 0x0004CEEC
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			object transparentProxy = this.GetTransparentProxy();
			RemotingServices.GetObjectData(transparentProxy, info, context);
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0004ED08 File Offset: 0x0004CF08
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x0004ED10 File Offset: 0x0004CF10
		internal Identity ObjectIdentity
		{
			get
			{
				return this._objectIdentity;
			}
			set
			{
				this._objectIdentity = value;
			}
		}

		// Token: 0x06001691 RID: 5777
		public abstract IMessage Invoke(IMessage msg);

		// Token: 0x06001692 RID: 5778
		[MethodImpl(4096)]
		internal virtual extern object InternalGetTransparentProxy(string className);

		// Token: 0x06001693 RID: 5779 RVA: 0x0004ED1C File Offset: 0x0004CF1C
		public virtual object GetTransparentProxy()
		{
			if (this._objTP == null)
			{
				IRemotingTypeInfo remotingTypeInfo = this as IRemotingTypeInfo;
				string text;
				if (remotingTypeInfo != null)
				{
					text = remotingTypeInfo.TypeName;
					if (text == null || text == typeof(MarshalByRefObject).AssemblyQualifiedName)
					{
						text = this.class_to_proxy.AssemblyQualifiedName;
					}
				}
				else
				{
					text = this.class_to_proxy.AssemblyQualifiedName;
				}
				this._objTP = this.InternalGetTransparentProxy(text);
			}
			return this._objTP;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0004ED98 File Offset: 0x0004CF98
		protected void AttachServer(MarshalByRefObject s)
		{
			this._server = s;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0004EDA4 File Offset: 0x0004CFA4
		internal void SetTargetDomain(int domainId)
		{
			this._targetDomainId = domainId;
		}

		// Token: 0x04000BA1 RID: 2977
		private Type class_to_proxy;

		// Token: 0x04000BA2 RID: 2978
		internal Context _targetContext;

		// Token: 0x04000BA3 RID: 2979
		private MarshalByRefObject _server;

		// Token: 0x04000BA4 RID: 2980
		private int _targetDomainId = -1;

		// Token: 0x04000BA5 RID: 2981
		internal string _targetUri;

		// Token: 0x04000BA6 RID: 2982
		internal Identity _objectIdentity;

		// Token: 0x04000BA7 RID: 2983
		private object _objTP;

		// Token: 0x04000BA8 RID: 2984
		private object _stubData;
	}
}
