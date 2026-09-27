using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000259 RID: 601
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x06001425 RID: 5157 RVA: 0x00046A78 File Offset: 0x00044C78
		public ContextLevelActivator(IActivator next)
		{
			this.m_NextActivator = next;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00046A88 File Offset: 0x00044C88
		public IActivator NextActivator
		{
			get
			{
				return this.m_NextActivator;
			}
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00046A90 File Offset: 0x00044C90
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			ServerIdentity serverIdentity = RemotingServices.CreateContextBoundObjectIdentity(ctorCall.ActivationType);
			RemotingServices.SetMessageTargetIdentity(ctorCall, serverIdentity);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (constructionCall == null || !constructionCall.IsContextOk)
			{
				serverIdentity.Context = Context.CreateNewContext(ctorCall);
				Context newContext = Context.SwitchToContext(serverIdentity.Context);
				try
				{
					return this.m_NextActivator.Activate(ctorCall);
				}
				finally
				{
					Context.SwitchToContext(newContext);
				}
			}
			return this.m_NextActivator.Activate(ctorCall);
		}

		// Token: 0x04000A71 RID: 2673
		private IActivator m_NextActivator;
	}
}
