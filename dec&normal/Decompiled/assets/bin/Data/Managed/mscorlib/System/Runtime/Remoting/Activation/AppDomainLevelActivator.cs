using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000257 RID: 599
	internal class AppDomainLevelActivator : IActivator
	{
		// Token: 0x0600141F RID: 5151 RVA: 0x0004698C File Offset: 0x00044B8C
		public AppDomainLevelActivator(string activationUrl, IActivator next)
		{
			this._activationUrl = activationUrl;
			this._next = next;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x000469A4 File Offset: 0x00044BA4
		public IActivator NextActivator
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000469AC File Offset: 0x00044BAC
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			IActivator activator = (IActivator)RemotingServices.Connect(typeof(IActivator), this._activationUrl);
			ctorCall.Activator = ctorCall.Activator.NextActivator;
			IConstructionReturnMessage constructionReturnMessage;
			try
			{
				constructionReturnMessage = activator.Activate(ctorCall);
			}
			catch (Exception e)
			{
				return new ConstructionResponse(e, ctorCall);
			}
			ObjRef objRef = (ObjRef)constructionReturnMessage.ReturnValue;
			if (RemotingServices.GetIdentityForUri(objRef.URI) != null)
			{
				throw new RemotingException("Inconsistent state during activation; there may be two proxies for the same object");
			}
			object obj;
			Identity orCreateClientIdentity = RemotingServices.GetOrCreateClientIdentity(objRef, null, out obj);
			RemotingServices.SetMessageTargetIdentity(ctorCall, orCreateClientIdentity);
			return constructionReturnMessage;
		}

		// Token: 0x04000A6F RID: 2671
		private string _activationUrl;

		// Token: 0x04000A70 RID: 2672
		private IActivator _next;
	}
}
