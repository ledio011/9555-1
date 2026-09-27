using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200025E RID: 606
	internal class RemoteActivator : MarshalByRefObject, IActivator
	{
		// Token: 0x06001433 RID: 5171 RVA: 0x00046BA4 File Offset: 0x00044DA4
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			if (!RemotingConfiguration.IsActivationAllowed(msg.ActivationType))
			{
				throw new RemotingException("The type " + msg.ActivationTypeName + " is not allowed to be client activated");
			}
			object[] activationAttributes = new object[]
			{
				new RemoteActivationAttribute(msg.ContextProperties)
			};
			MarshalByRefObject obj = (MarshalByRefObject)Activator.CreateInstance(msg.ActivationType, msg.Args, activationAttributes);
			ObjRef resultObject = RemotingServices.Marshal(obj);
			return new ConstructionResponse(resultObject, null, msg);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00046C18 File Offset: 0x00044E18
		public override object InitializeLifetimeService()
		{
			ILease lease = (ILease)base.InitializeLifetimeService();
			if (lease.CurrentState == LeaseState.Initial)
			{
				lease.InitialLeaseTime = TimeSpan.FromMinutes(30.0);
				lease.SponsorshipTimeout = TimeSpan.FromMinutes(1.0);
				lease.RenewOnCallTime = TimeSpan.FromMinutes(10.0);
			}
			return lease;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00046C7C File Offset: 0x00044E7C
		public IActivator NextActivator
		{
			get
			{
				throw new NotSupportedException();
			}
		}
	}
}
