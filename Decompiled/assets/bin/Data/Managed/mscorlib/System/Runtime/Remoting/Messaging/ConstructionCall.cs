using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002A5 RID: 677
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionCall : MethodCall, IConstructionCallMessage, IMessage, IMethodCallMessage, IMethodMessage
	{
		// Token: 0x0600155A RID: 5466 RVA: 0x0004B6E0 File Offset: 0x000498E0
		internal ConstructionCall(Type type)
		{
			this._activationType = type;
			this._activationTypeName = type.AssemblyQualifiedName;
			this._isContextOk = true;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0004B704 File Offset: 0x00049904
		internal ConstructionCall(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0004B710 File Offset: 0x00049910
		internal override void InitDictionary()
		{
			ConstructionCallDictionary constructionCallDictionary = new ConstructionCallDictionary(this);
			this.ExternalProperties = constructionCallDictionary;
			this.InternalProperties = constructionCallDictionary.GetInternalProperties();
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0004B738 File Offset: 0x00049938
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x0004B740 File Offset: 0x00049940
		internal bool IsContextOk
		{
			get
			{
				return this._isContextOk;
			}
			set
			{
				this._isContextOk = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0004B74C File Offset: 0x0004994C
		public Type ActivationType
		{
			get
			{
				if (this._activationType == null)
				{
					this._activationType = Type.GetType(this._activationTypeName);
				}
				return this._activationType;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0004B770 File Offset: 0x00049970
		public string ActivationTypeName
		{
			get
			{
				return this._activationTypeName;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0004B778 File Offset: 0x00049978
		// (set) Token: 0x06001562 RID: 5474 RVA: 0x0004B780 File Offset: 0x00049980
		public IActivator Activator
		{
			get
			{
				return this._activator;
			}
			set
			{
				this._activator = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0004B78C File Offset: 0x0004998C
		public object[] CallSiteActivationAttributes
		{
			get
			{
				return this._activationAttributes;
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0004B794 File Offset: 0x00049994
		internal void SetActivationAttributes(object[] attributes)
		{
			this._activationAttributes = attributes;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0004B7A0 File Offset: 0x000499A0
		public IList ContextProperties
		{
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0004B7C0 File Offset: 0x000499C0
		internal override void InitMethodProperty(string key, object value)
		{
			switch (key)
			{
			case "__Activator":
				this._activator = (IActivator)value;
				return;
			case "__CallSiteActivationAttributes":
				this._activationAttributes = (object[])value;
				return;
			case "__ActivationType":
				this._activationType = (Type)value;
				return;
			case "__ContextProperties":
				this._contextProperties = (IList)value;
				return;
			case "__ActivationTypeName":
				this._activationTypeName = (string)value;
				return;
			}
			base.InitMethodProperty(key, value);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0004B8A4 File Offset: 0x00049AA4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IList list = this._contextProperties;
			if (list != null && list.Count == 0)
			{
				list = null;
			}
			info.AddValue("__Activator", this._activator);
			info.AddValue("__CallSiteActivationAttributes", this._activationAttributes);
			info.AddValue("__ActivationType", null);
			info.AddValue("__ContextProperties", list);
			info.AddValue("__ActivationTypeName", this._activationTypeName);
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0004B920 File Offset: 0x00049B20
		public override IDictionary Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0004B928 File Offset: 0x00049B28
		internal RemotingProxy SourceProxy
		{
			get
			{
				return this._sourceProxy;
			}
		}

		// Token: 0x04000B10 RID: 2832
		private IActivator _activator;

		// Token: 0x04000B11 RID: 2833
		private object[] _activationAttributes;

		// Token: 0x04000B12 RID: 2834
		private IList _contextProperties;

		// Token: 0x04000B13 RID: 2835
		private Type _activationType;

		// Token: 0x04000B14 RID: 2836
		private string _activationTypeName;

		// Token: 0x04000B15 RID: 2837
		private bool _isContextOk;

		// Token: 0x04000B16 RID: 2838
		[NonSerialized]
		private RemotingProxy _sourceProxy;
	}
}
