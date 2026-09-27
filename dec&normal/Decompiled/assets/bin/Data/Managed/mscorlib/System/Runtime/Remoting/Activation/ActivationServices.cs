using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000256 RID: 598
	internal class ActivationServices
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0004662C File Offset: 0x0004482C
		private static IActivator ConstructionActivator
		{
			get
			{
				if (ActivationServices._constructionActivator == null)
				{
					ActivationServices._constructionActivator = new ConstructionLevelActivator();
				}
				return ActivationServices._constructionActivator;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00046648 File Offset: 0x00044848
		public static IMessage RemoteActivate(IConstructionCallMessage ctorCall)
		{
			IMessage result;
			try
			{
				result = ctorCall.Activator.Activate(ctorCall);
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, ctorCall);
			}
			return result;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00046690 File Offset: 0x00044890
		public static object CreateProxyFromAttributes(Type type, object[] activationAttributes)
		{
			string text = null;
			foreach (object obj in activationAttributes)
			{
				if (!(obj is IContextAttribute))
				{
					throw new RemotingException("Activation attribute does not implement the IContextAttribute interface");
				}
				if (obj is UrlAttribute)
				{
					text = ((UrlAttribute)obj).UrlValue;
				}
			}
			if (text != null)
			{
				return RemotingServices.CreateClientProxy(type, text, activationAttributes);
			}
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfiguration.IsRemotelyActivatedClientType(type);
			if (activatedClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(activatedClientTypeEntry, activationAttributes);
			}
			if (type.IsContextful)
			{
				return RemotingServices.CreateClientProxyForContextBound(type, activationAttributes);
			}
			return null;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00046724 File Offset: 0x00044924
		public static ConstructionCall CreateConstructionCall(Type type, string activationUrl, object[] activationAttributes)
		{
			ConstructionCall constructionCall = new ConstructionCall(type);
			if (!type.IsContextful)
			{
				constructionCall.Activator = new AppDomainLevelActivator(activationUrl, ActivationServices.ConstructionActivator);
				constructionCall.IsContextOk = false;
				return constructionCall;
			}
			IActivator activator = ActivationServices.ConstructionActivator;
			activator = new ContextLevelActivator(activator);
			ArrayList arrayList = new ArrayList();
			if (activationAttributes != null)
			{
				arrayList.AddRange(activationAttributes);
			}
			bool flag = activationUrl == ChannelServices.CrossContextUrl;
			Context currentContext = Thread.CurrentContext;
			if (flag)
			{
				foreach (object obj in arrayList)
				{
					IContextAttribute contextAttribute = (IContextAttribute)obj;
					if (!contextAttribute.IsContextOK(currentContext, constructionCall))
					{
						flag = false;
						break;
					}
				}
			}
			object[] customAttributes = type.GetCustomAttributes(true);
			foreach (object obj2 in customAttributes)
			{
				if (obj2 is IContextAttribute)
				{
					flag = (flag && ((IContextAttribute)obj2).IsContextOK(currentContext, constructionCall));
					arrayList.Add(obj2);
				}
			}
			if (!flag)
			{
				constructionCall.SetActivationAttributes(arrayList.ToArray());
				foreach (object obj3 in arrayList)
				{
					IContextAttribute contextAttribute2 = (IContextAttribute)obj3;
					contextAttribute2.GetPropertiesForNewContext(constructionCall);
				}
			}
			if (activationUrl != ChannelServices.CrossContextUrl)
			{
				activator = new AppDomainLevelActivator(activationUrl, activator);
			}
			constructionCall.Activator = activator;
			constructionCall.IsContextOk = flag;
			return constructionCall;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x000468E8 File Offset: 0x00044AE8
		public static IMessage CreateInstanceFromMessage(IConstructionCallMessage ctorCall)
		{
			object obj = ActivationServices.AllocateUninitializedClassInstance(ctorCall.ActivationType);
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(ctorCall);
			serverIdentity.AttachServerObject((MarshalByRefObject)obj, Thread.CurrentContext);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (ctorCall.ActivationType.IsContextful && constructionCall != null && constructionCall.SourceProxy != null)
			{
				constructionCall.SourceProxy.AttachIdentity(serverIdentity);
				MarshalByRefObject target = (MarshalByRefObject)constructionCall.SourceProxy.GetTransparentProxy();
				RemotingServices.InternalExecuteMessage(target, ctorCall);
			}
			else
			{
				ctorCall.MethodBase.Invoke(obj, ctorCall.Args);
			}
			return new ConstructionResponse(obj, null, ctorCall);
		}

		// Token: 0x0600141D RID: 5149
		[MethodImpl(4096)]
		public static extern object AllocateUninitializedClassInstance(Type type);

		// Token: 0x0600141E RID: 5150
		[MethodImpl(4096)]
		public static extern void EnableProxyActivation(Type type, bool enable);

		// Token: 0x04000A6E RID: 2670
		private static IActivator _constructionActivator;
	}
}
