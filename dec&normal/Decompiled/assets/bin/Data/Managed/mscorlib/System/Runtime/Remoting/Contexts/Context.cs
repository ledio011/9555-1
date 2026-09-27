using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000274 RID: 628
	[ComVisible(true)]
	public class Context
	{
		// Token: 0x06001495 RID: 5269 RVA: 0x00048CF8 File Offset: 0x00046EF8
		public Context()
		{
			this.domain_id = Thread.GetDomainID();
			this.context_id = 1 + Context.global_count++;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00048D2C File Offset: 0x00046F2C
		~Context()
		{
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00048D58 File Offset: 0x00046F58
		public static Context DefaultContext
		{
			get
			{
				return AppDomain.InternalGetDefaultContext();
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x00048D60 File Offset: 0x00046F60
		internal bool IsDefaultContext
		{
			get
			{
				return this.context_id == 0;
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00048D6C File Offset: 0x00046F6C
		internal static void NotifyGlobalDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties)
			{
				Context.global_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00048D98 File Offset: 0x00046F98
		internal static bool HasGlobalDynamicSinks
		{
			get
			{
				return Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00048DB4 File Offset: 0x00046FB4
		internal void NotifyDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties)
			{
				this.context_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00048DE4 File Offset: 0x00046FE4
		internal bool HasDynamicSinks
		{
			get
			{
				return this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00048E00 File Offset: 0x00047000
		internal bool HasExitSinks
		{
			get
			{
				return !(this.GetClientContextSinkChain() is ClientContextTerminatorSink) || this.HasDynamicSinks || Context.HasGlobalDynamicSinks;
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00048E28 File Offset: 0x00047028
		public virtual IContextProperty GetProperty(string name)
		{
			if (this.context_properties == null)
			{
				return null;
			}
			foreach (object obj in this.context_properties)
			{
				IContextProperty contextProperty = (IContextProperty)obj;
				if (contextProperty.Name == name)
				{
					return contextProperty;
				}
			}
			return null;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00048EAC File Offset: 0x000470AC
		public virtual void SetProperty(IContextProperty prop)
		{
			if (prop == null)
			{
				throw new ArgumentNullException("IContextProperty");
			}
			if (this == Context.DefaultContext)
			{
				throw new InvalidOperationException("Can not add properties to default context");
			}
			if (this.frozen)
			{
				throw new InvalidOperationException("Context is Frozen");
			}
			if (this.context_properties == null)
			{
				this.context_properties = new ArrayList();
			}
			this.context_properties.Add(prop);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00048F1C File Offset: 0x0004711C
		public virtual void Freeze()
		{
			if (this.context_properties != null)
			{
				foreach (object obj in this.context_properties)
				{
					IContextProperty contextProperty = (IContextProperty)obj;
					contextProperty.Freeze(this);
				}
			}
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00048F8C File Offset: 0x0004718C
		public override string ToString()
		{
			return "ContextID: " + this.context_id;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00048FA4 File Offset: 0x000471A4
		internal IMessageSink GetServerContextSinkChain()
		{
			if (this.server_context_sink_chain == null)
			{
				if (Context.default_server_context_sink == null)
				{
					Context.default_server_context_sink = new ServerContextTerminatorSink();
				}
				this.server_context_sink_chain = Context.default_server_context_sink;
				if (this.context_properties != null)
				{
					for (int i = this.context_properties.Count - 1; i >= 0; i--)
					{
						IContributeServerContextSink contributeServerContextSink = this.context_properties[i] as IContributeServerContextSink;
						if (contributeServerContextSink != null)
						{
							this.server_context_sink_chain = contributeServerContextSink.GetServerContextSink(this.server_context_sink_chain);
						}
					}
				}
			}
			return this.server_context_sink_chain;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00049034 File Offset: 0x00047234
		internal IMessageSink GetClientContextSinkChain()
		{
			if (this.client_context_sink_chain == null)
			{
				this.client_context_sink_chain = new ClientContextTerminatorSink(this);
				if (this.context_properties != null)
				{
					foreach (object obj in this.context_properties)
					{
						IContextProperty contextProperty = (IContextProperty)obj;
						IContributeClientContextSink contributeClientContextSink = contextProperty as IContributeClientContextSink;
						if (contributeClientContextSink != null)
						{
							this.client_context_sink_chain = contributeClientContextSink.GetClientContextSink(this.client_context_sink_chain);
						}
					}
				}
			}
			return this.client_context_sink_chain;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000490D8 File Offset: 0x000472D8
		internal IMessageSink CreateServerObjectSinkChain(MarshalByRefObject obj, bool forceInternalExecute)
		{
			IMessageSink messageSink = new StackBuilderSink(obj, forceInternalExecute);
			messageSink = new ServerObjectTerminatorSink(messageSink);
			messageSink = new LeaseSink(messageSink);
			if (this.context_properties != null)
			{
				for (int i = this.context_properties.Count - 1; i >= 0; i--)
				{
					IContextProperty contextProperty = (IContextProperty)this.context_properties[i];
					IContributeObjectSink contributeObjectSink = contextProperty as IContributeObjectSink;
					if (contributeObjectSink != null)
					{
						messageSink = contributeObjectSink.GetObjectSink(obj, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00049150 File Offset: 0x00047350
		internal IMessageSink CreateEnvoySink(MarshalByRefObject serverObject)
		{
			IMessageSink messageSink = EnvoyTerminatorSink.Instance;
			if (this.context_properties != null)
			{
				foreach (object obj in this.context_properties)
				{
					IContextProperty contextProperty = (IContextProperty)obj;
					IContributeEnvoySink contributeEnvoySink = contextProperty as IContributeEnvoySink;
					if (contributeEnvoySink != null)
					{
						messageSink = contributeEnvoySink.GetEnvoySink(serverObject, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000491D8 File Offset: 0x000473D8
		internal static Context SwitchToContext(Context newContext)
		{
			return AppDomain.InternalSetContext(newContext);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000491E0 File Offset: 0x000473E0
		internal static Context CreateNewContext(IConstructionCallMessage msg)
		{
			Context context = new Context();
			foreach (object obj in msg.ContextProperties)
			{
				IContextProperty contextProperty = (IContextProperty)obj;
				if (context.GetProperty(contextProperty.Name) == null)
				{
					context.SetProperty(contextProperty);
				}
			}
			context.Freeze();
			foreach (object obj2 in msg.ContextProperties)
			{
				IContextProperty contextProperty2 = (IContextProperty)obj2;
				if (!contextProperty2.IsNewContextOK(context))
				{
					throw new RemotingException("A context property did not approve the candidate context for activating the object");
				}
			}
			return context;
		}

		// Token: 0x04000A9D RID: 2717
		private int domain_id;

		// Token: 0x04000A9E RID: 2718
		private int context_id;

		// Token: 0x04000A9F RID: 2719
		private UIntPtr static_data;

		// Token: 0x04000AA0 RID: 2720
		private static IMessageSink default_server_context_sink;

		// Token: 0x04000AA1 RID: 2721
		private IMessageSink server_context_sink_chain;

		// Token: 0x04000AA2 RID: 2722
		private IMessageSink client_context_sink_chain;

		// Token: 0x04000AA3 RID: 2723
		private object[] datastore;

		// Token: 0x04000AA4 RID: 2724
		private ArrayList context_properties;

		// Token: 0x04000AA5 RID: 2725
		private bool frozen;

		// Token: 0x04000AA6 RID: 2726
		private static int global_count;

		// Token: 0x04000AA7 RID: 2727
		private static Hashtable namedSlots = new Hashtable();

		// Token: 0x04000AA8 RID: 2728
		private static DynamicPropertyCollection global_dynamic_properties;

		// Token: 0x04000AA9 RID: 2729
		private DynamicPropertyCollection context_dynamic_properties;

		// Token: 0x04000AAA RID: 2730
		private ContextCallbackObject callback_object;
	}
}
