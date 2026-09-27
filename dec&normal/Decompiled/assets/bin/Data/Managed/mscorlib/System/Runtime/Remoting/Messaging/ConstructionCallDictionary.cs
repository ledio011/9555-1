using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002A6 RID: 678
	internal class ConstructionCallDictionary : MethodDictionary
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x0004B930 File Offset: 0x00049B30
		public ConstructionCallDictionary(IConstructionCallMessage message) : base(message)
		{
			base.MethodKeys = ConstructionCallDictionary.InternalKeys;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0004B9B8 File Offset: 0x00049BB8
		protected override object GetMethodProperty(string key)
		{
			switch (key)
			{
			case "__Activator":
				return ((IConstructionCallMessage)this._message).Activator;
			case "__CallSiteActivationAttributes":
				return ((IConstructionCallMessage)this._message).CallSiteActivationAttributes;
			case "__ActivationType":
				return ((IConstructionCallMessage)this._message).ActivationType;
			case "__ContextProperties":
				return ((IConstructionCallMessage)this._message).ContextProperties;
			case "__ActivationTypeName":
				return ((IConstructionCallMessage)this._message).ActivationTypeName;
			}
			return base.GetMethodProperty(key);
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0004BAB0 File Offset: 0x00049CB0
		protected override void SetMethodProperty(string key, object value)
		{
			if (key != null)
			{
				if (ConstructionCallDictionary.<>f__switch$map24 == null)
				{
					ConstructionCallDictionary.<>f__switch$map24 = new Dictionary<string, int>(5)
					{
						{
							"__Activator",
							0
						},
						{
							"__CallSiteActivationAttributes",
							1
						},
						{
							"__ActivationType",
							1
						},
						{
							"__ContextProperties",
							1
						},
						{
							"__ActivationTypeName",
							1
						}
					};
				}
				int num;
				if (ConstructionCallDictionary.<>f__switch$map24.TryGetValue(key, out num))
				{
					if (num == 0)
					{
						((IConstructionCallMessage)this._message).Activator = (IActivator)value;
						return;
					}
					if (num == 1)
					{
						throw new ArgumentException("key was invalid");
					}
				}
			}
			base.SetMethodProperty(key, value);
		}

		// Token: 0x04000B18 RID: 2840
		public static string[] InternalKeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__TypeName",
			"__MethodSignature",
			"__Args",
			"__CallContext",
			"__CallSiteActivationAttributes",
			"__ActivationType",
			"__ContextProperties",
			"__Activator",
			"__ActivationTypeName"
		};
	}
}
