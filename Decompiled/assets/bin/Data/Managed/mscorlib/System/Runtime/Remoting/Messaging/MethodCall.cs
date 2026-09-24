using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002B7 RID: 695
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class MethodCall : IInternalMessage, IMessage, IMethodCallMessage, IMethodMessage, ISerializationRootObject, ISerializable
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x0004BE38 File Offset: 0x0004A038
		public MethodCall(Header[] h1)
		{
			this.Init();
			if (h1 == null || h1.Length == 0)
			{
				return;
			}
			foreach (Header header in h1)
			{
				this.InitMethodProperty(header.Name, header.Value);
			}
			this.ResolveMethod();
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0004BE94 File Offset: 0x0004A094
		internal MethodCall(SerializationInfo info, StreamingContext context)
		{
			this.Init();
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InitMethodProperty(serializationEntry.Name, serializationEntry.Value);
			}
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0004BEE0 File Offset: 0x0004A0E0
		internal MethodCall(CADMethodCallMessage msg)
		{
			this._uri = string.Copy(msg.Uri);
			ArrayList arguments = msg.GetArguments();
			this._args = msg.GetArgs(arguments);
			this._callContext = msg.GetLogicalCallContext(arguments);
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			this._methodBase = msg.GetMethod();
			this.Init();
			if (msg.PropertiesCount > 0)
			{
				CADMessageBase.UnmarshalProperties(this.Properties, msg.PropertiesCount, arguments);
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0004BF6C File Offset: 0x0004A16C
		internal MethodCall()
		{
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x0004BF74 File Offset: 0x0004A174
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x0004BF7C File Offset: 0x0004A17C
		string IInternalMessage.Uri
		{
			get
			{
				return this.Uri;
			}
			set
			{
				this.Uri = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x0004BF88 File Offset: 0x0004A188
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x0004BF90 File Offset: 0x0004A190
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this._targetIdentity;
			}
			set
			{
				this._targetIdentity = value;
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0004BF9C File Offset: 0x0004A19C
		internal virtual void InitMethodProperty(string key, object value)
		{
			switch (key)
			{
			case "__TypeName":
				this._typeName = (string)value;
				return;
			case "__MethodName":
				this._methodName = (string)value;
				return;
			case "__MethodSignature":
				this._methodSignature = (Type[])value;
				return;
			case "__Args":
				this._args = (object[])value;
				return;
			case "__CallContext":
				this._callContext = (LogicalCallContext)value;
				return;
			case "__Uri":
				this._uri = (string)value;
				return;
			case "__GenericArguments":
				this._genericArguments = (Type[])value;
				return;
			}
			this.Properties[key] = value;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0004C0C0 File Offset: 0x0004A2C0
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("__TypeName", this._typeName);
			info.AddValue("__MethodName", this._methodName);
			info.AddValue("__MethodSignature", this._methodSignature);
			info.AddValue("__Args", this._args);
			info.AddValue("__CallContext", this._callContext);
			info.AddValue("__Uri", this._uri);
			info.AddValue("__GenericArguments", this._genericArguments);
			if (this.InternalProperties != null)
			{
				foreach (object obj in this.InternalProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0004C1B8 File Offset: 0x0004A3B8
		public int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0004C1C4 File Offset: 0x0004A3C4
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0004C1CC File Offset: 0x0004A3CC
		public bool HasVarArgs
		{
			get
			{
				return (this.MethodBase.CallingConvention | CallingConventions.VarArgs) != (CallingConventions)0;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x0004C1E4 File Offset: 0x0004A3E4
		public int InArgCount
		{
			get
			{
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
				}
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0004C210 File Offset: 0x0004A410
		public object[] InArgs
		{
			get
			{
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
				}
				return this._inArgInfo.GetInOutArgs(this._args);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x0004C240 File Offset: 0x0004A440
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				if (this._callContext == null)
				{
					this._callContext = new LogicalCallContext();
				}
				return this._callContext;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0004C260 File Offset: 0x0004A460
		public MethodBase MethodBase
		{
			get
			{
				if (this._methodBase == null)
				{
					this.ResolveMethod();
				}
				return this._methodBase;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0004C27C File Offset: 0x0004A47C
		public string MethodName
		{
			get
			{
				if (this._methodName == null)
				{
					this._methodName = this._methodBase.Name;
				}
				return this._methodName;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0004C2A0 File Offset: 0x0004A4A0
		public object MethodSignature
		{
			get
			{
				if (this._methodSignature == null && this._methodBase != null)
				{
					ParameterInfo[] parameters = this._methodBase.GetParameters();
					this._methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this._methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this._methodSignature;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0004C308 File Offset: 0x0004A508
		public virtual IDictionary Properties
		{
			get
			{
				if (this.ExternalProperties == null)
				{
					this.InitDictionary();
				}
				return this.ExternalProperties;
			}
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0004C324 File Offset: 0x0004A524
		internal virtual void InitDictionary()
		{
			MethodCallDictionary methodCallDictionary = new MethodCallDictionary(this);
			this.ExternalProperties = methodCallDictionary;
			this.InternalProperties = methodCallDictionary.GetInternalProperties();
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x0004C34C File Offset: 0x0004A54C
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					this._typeName = this._methodBase.DeclaringType.AssemblyQualifiedName;
				}
				return this._typeName;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0004C378 File Offset: 0x0004A578
		// (set) Token: 0x060015BD RID: 5565 RVA: 0x0004C380 File Offset: 0x0004A580
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0004C38C File Offset: 0x0004A58C
		public object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0004C398 File Offset: 0x0004A598
		public string GetArgName(int index)
		{
			return this._methodBase.GetParameters()[index].Name;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0004C3AC File Offset: 0x0004A5AC
		public object GetInArg(int argNum)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0004C3E0 File Offset: 0x0004A5E0
		public string GetInArgName(int index)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0004C40C File Offset: 0x0004A60C
		public virtual void Init()
		{
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0004C410 File Offset: 0x0004A610
		public void ResolveMethod()
		{
			if (this._uri != null)
			{
				Type serverTypeForUri = RemotingServices.GetServerTypeForUri(this._uri);
				if (serverTypeForUri == null)
				{
					string str = (this._typeName == null) ? string.Empty : (" (" + this._typeName + ")");
					throw new RemotingException("Requested service not found" + str + ". No receiver for uri " + this._uri);
				}
				Type type = this.CastTo(this._typeName, serverTypeForUri);
				if (type == null)
				{
					throw new RemotingException(string.Concat(new string[]
					{
						"Cannot cast from client type '",
						this._typeName,
						"' to server type '",
						serverTypeForUri.FullName,
						"'"
					}));
				}
				this._methodBase = RemotingServices.GetMethodBaseFromName(type, this._methodName, this._methodSignature);
				if (this._methodBase == null)
				{
					throw new RemotingException(string.Concat(new object[]
					{
						"Method ",
						this._methodName,
						" not found in ",
						type
					}));
				}
				if (type != serverTypeForUri && type.IsInterface && !serverTypeForUri.IsInterface)
				{
					this._methodBase = RemotingServices.GetVirtualMethod(serverTypeForUri, this._methodBase);
					if (this._methodBase == null)
					{
						throw new RemotingException(string.Concat(new object[]
						{
							"Method ",
							this._methodName,
							" not found in ",
							serverTypeForUri
						}));
					}
				}
			}
			else
			{
				this._methodBase = RemotingServices.GetMethodBaseFromMethodMessage(this);
				if (this._methodBase == null)
				{
					throw new RemotingException("Method " + this._methodName + " not found in " + this.TypeName);
				}
			}
			if (this._methodBase.IsGenericMethod && this._methodBase.ContainsGenericParameters)
			{
				if (this.GenericArguments == null)
				{
					throw new RemotingException("The remoting infrastructure does not support open generic methods.");
				}
				this._methodBase = ((MethodInfo)this._methodBase).MakeGenericMethod(this.GenericArguments);
			}
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0004C618 File Offset: 0x0004A818
		private Type CastTo(string clientType, Type serverType)
		{
			clientType = MethodCall.GetTypeNameFromAssemblyQualifiedName(clientType);
			if (clientType == serverType.FullName)
			{
				return serverType;
			}
			for (Type baseType = serverType.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				if (clientType == baseType.FullName)
				{
					return baseType;
				}
			}
			Type[] interfaces = serverType.GetInterfaces();
			foreach (Type type in interfaces)
			{
				if (clientType == type.FullName)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0004C6A8 File Offset: 0x0004A8A8
		private static string GetTypeNameFromAssemblyQualifiedName(string aqname)
		{
			int num = aqname.IndexOf("]]");
			int num2 = aqname.IndexOf(',', (num != -1) ? (num + 2) : 0);
			if (num2 != -1)
			{
				aqname = aqname.Substring(0, num2).Trim();
			}
			return aqname;
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0004C6F4 File Offset: 0x0004A8F4
		private Type[] GenericArguments
		{
			get
			{
				if (this._genericArguments != null)
				{
					return this._genericArguments;
				}
				return this._genericArguments = this.MethodBase.GetGenericArguments();
			}
		}

		// Token: 0x04000B22 RID: 2850
		private string _uri;

		// Token: 0x04000B23 RID: 2851
		private string _typeName;

		// Token: 0x04000B24 RID: 2852
		private string _methodName;

		// Token: 0x04000B25 RID: 2853
		private object[] _args;

		// Token: 0x04000B26 RID: 2854
		private Type[] _methodSignature;

		// Token: 0x04000B27 RID: 2855
		private MethodBase _methodBase;

		// Token: 0x04000B28 RID: 2856
		private LogicalCallContext _callContext;

		// Token: 0x04000B29 RID: 2857
		private ArgInfo _inArgInfo;

		// Token: 0x04000B2A RID: 2858
		private Identity _targetIdentity;

		// Token: 0x04000B2B RID: 2859
		private Type[] _genericArguments;

		// Token: 0x04000B2C RID: 2860
		protected IDictionary ExternalProperties;

		// Token: 0x04000B2D RID: 2861
		protected IDictionary InternalProperties;
	}
}
