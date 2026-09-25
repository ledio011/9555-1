using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020002D1 RID: 721
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	public class ProxyAttribute : Attribute, IContextAttribute
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x0004EBE8 File Offset: 0x0004CDE8
		public virtual MarshalByRefObject CreateInstance(Type serverType)
		{
			RemotingProxy remotingProxy = new RemotingProxy(serverType, ChannelServices.CrossContextUrl, null);
			return (MarshalByRefObject)remotingProxy.GetTransparentProxy();
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0004EC10 File Offset: 0x0004CE10
		public virtual RealProxy CreateProxy(ObjRef objRef, Type serverType, object serverObject, Context serverContext)
		{
			return RemotingServices.GetRealProxy(RemotingServices.GetProxyForRemoteObject(objRef, serverType));
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0004EC20 File Offset: 0x0004CE20
		[ComVisible(true)]
		public void GetPropertiesForNewContext(IConstructionCallMessage msg)
		{
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0004EC24 File Offset: 0x0004CE24
		[ComVisible(true)]
		public bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}
	}
}
