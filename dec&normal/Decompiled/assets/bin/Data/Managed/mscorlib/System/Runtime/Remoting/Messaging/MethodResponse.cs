using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002BB RID: 699
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public class MethodResponse : IInternalMessage, IMessage, IMethodMessage, IMethodReturnMessage, ISerializationRootObject, ISerializable
	{
		// Token: 0x060015E9 RID: 5609 RVA: 0x0004CFE0 File Offset: 0x0004B1E0
		internal MethodResponse(Exception e, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			if (msg != null)
			{
				this._uri = msg.Uri;
			}
			else
			{
				this._uri = string.Empty;
			}
			this._exception = e;
			this._returnValue = null;
			this._outArgs = new object[0];
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0004D038 File Offset: 0x0004B238
		internal MethodResponse(object returnValue, object[] outArgs, LogicalCallContext callCtx, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			this._uri = msg.Uri;
			this._exception = null;
			this._returnValue = returnValue;
			this._args = outArgs;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0004D06C File Offset: 0x0004B26C
		internal MethodResponse(IMethodCallMessage msg, CADMethodReturnMessage retmsg)
		{
			this._callMsg = msg;
			this._methodBase = msg.MethodBase;
			this._uri = msg.Uri;
			this._methodName = msg.MethodName;
			ArrayList arguments = retmsg.GetArguments();
			this._exception = retmsg.GetException(arguments);
			this._returnValue = retmsg.GetReturnValue(arguments);
			this._args = retmsg.GetArgs(arguments);
			this._callContext = retmsg.GetLogicalCallContext(arguments);
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			if (retmsg.PropertiesCount > 0)
			{
				CADMessageBase.UnmarshalProperties(this.Properties, retmsg.PropertiesCount, arguments);
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0004D11C File Offset: 0x0004B31C
		internal MethodResponse(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InitMethodProperty(serializationEntry.Name, serializationEntry.Value);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0004D164 File Offset: 0x0004B364
		// (set) Token: 0x060015EE RID: 5614 RVA: 0x0004D16C File Offset: 0x0004B36C
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0004D178 File Offset: 0x0004B378
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x0004D180 File Offset: 0x0004B380
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

		// Token: 0x060015F1 RID: 5617 RVA: 0x0004D18C File Offset: 0x0004B38C
		internal void InitMethodProperty(string key, object value)
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
			case "__Uri":
				this._uri = (string)value;
				return;
			case "__Return":
				this._returnValue = value;
				return;
			case "__OutArgs":
				this._args = (object[])value;
				return;
			case "__fault":
				this._exception = (Exception)value;
				return;
			case "__CallContext":
				this._callContext = (LogicalCallContext)value;
				return;
			}
			this.Properties[key] = value;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0004D2EC File Offset: 0x0004B4EC
		public int ArgCount
		{
			get
			{
				if (this._args == null)
				{
					return 0;
				}
				return this._args.Length;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0004D304 File Offset: 0x0004B504
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0004D30C File Offset: 0x0004B50C
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0004D314 File Offset: 0x0004B514
		public bool HasVarArgs
		{
			get
			{
				return (this.MethodBase.CallingConvention | CallingConventions.VarArgs) != (CallingConventions)0;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0004D32C File Offset: 0x0004B52C
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0004D34C File Offset: 0x0004B54C
		public MethodBase MethodBase
		{
			get
			{
				if (this._methodBase == null)
				{
					if (this._callMsg != null)
					{
						this._methodBase = this._callMsg.MethodBase;
					}
					else if (this.MethodName != null && this.TypeName != null)
					{
						this._methodBase = RemotingServices.GetMethodBaseFromMethodMessage(this);
					}
				}
				return this._methodBase;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0004D3B0 File Offset: 0x0004B5B0
		public string MethodName
		{
			get
			{
				if (this._methodName == null && this._callMsg != null)
				{
					this._methodName = this._callMsg.MethodName;
				}
				return this._methodName;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x0004D3E0 File Offset: 0x0004B5E0
		public object MethodSignature
		{
			get
			{
				if (this._methodSignature == null && this._callMsg != null)
				{
					this._methodSignature = (Type[])this._callMsg.MethodSignature;
				}
				return this._methodSignature;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0004D414 File Offset: 0x0004B614
		public int OutArgCount
		{
			get
			{
				if (this._args == null || this._args.Length == 0)
				{
					return 0;
				}
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
				}
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0004D464 File Offset: 0x0004B664
		public object[] OutArgs
		{
			get
			{
				if (this._outArgs == null && this._args != null)
				{
					if (this._inArgInfo == null)
					{
						this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
					}
					this._outArgs = this._inArgInfo.GetInOutArgs(this._args);
				}
				return this._outArgs;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x0004D4C4 File Offset: 0x0004B6C4
		public virtual IDictionary Properties
		{
			get
			{
				if (this.ExternalProperties == null)
				{
					MethodReturnDictionary methodReturnDictionary = new MethodReturnDictionary(this);
					this.ExternalProperties = methodReturnDictionary;
					this.InternalProperties = methodReturnDictionary.GetInternalProperties();
				}
				return this.ExternalProperties;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0004D4FC File Offset: 0x0004B6FC
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x0004D504 File Offset: 0x0004B704
		public string TypeName
		{
			get
			{
				if (this._typeName == null && this._callMsg != null)
				{
					this._typeName = this._callMsg.TypeName;
				}
				return this._typeName;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0004D534 File Offset: 0x0004B734
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0004D564 File Offset: 0x0004B764
		public string Uri
		{
			get
			{
				if (this._uri == null && this._callMsg != null)
				{
					this._uri = this._callMsg.Uri;
				}
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0004D570 File Offset: 0x0004B770
		public object GetArg(int argNum)
		{
			if (this._args == null)
			{
				return null;
			}
			return this._args[argNum];
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0004D588 File Offset: 0x0004B788
		public string GetArgName(int index)
		{
			return this.MethodBase.GetParameters()[index].Name;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0004D59C File Offset: 0x0004B79C
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this._exception == null)
			{
				info.AddValue("__TypeName", this._typeName);
				info.AddValue("__MethodName", this._methodName);
				info.AddValue("__MethodSignature", this._methodSignature);
				info.AddValue("__Uri", this._uri);
				info.AddValue("__Return", this._returnValue);
				info.AddValue("__OutArgs", this._args);
			}
			else
			{
				info.AddValue("__fault", this._exception);
			}
			info.AddValue("__CallContext", this._callContext);
			if (this.InternalProperties != null)
			{
				foreach (object obj in this.InternalProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
		public object GetOutArg(int argNum)
		{
			if (this._args == null)
			{
				return null;
			}
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0004D6F4 File Offset: 0x0004B8F4
		public string GetOutArgName(int index)
		{
			if (this._methodBase == null)
			{
				return "__method_" + index;
			}
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x04000B39 RID: 2873
		private string _methodName;

		// Token: 0x04000B3A RID: 2874
		private string _uri;

		// Token: 0x04000B3B RID: 2875
		private string _typeName;

		// Token: 0x04000B3C RID: 2876
		private MethodBase _methodBase;

		// Token: 0x04000B3D RID: 2877
		private object _returnValue;

		// Token: 0x04000B3E RID: 2878
		private Exception _exception;

		// Token: 0x04000B3F RID: 2879
		private Type[] _methodSignature;

		// Token: 0x04000B40 RID: 2880
		private ArgInfo _inArgInfo;

		// Token: 0x04000B41 RID: 2881
		private object[] _args;

		// Token: 0x04000B42 RID: 2882
		private object[] _outArgs;

		// Token: 0x04000B43 RID: 2883
		private IMethodCallMessage _callMsg;

		// Token: 0x04000B44 RID: 2884
		private LogicalCallContext _callContext;

		// Token: 0x04000B45 RID: 2885
		private Identity _targetIdentity;

		// Token: 0x04000B46 RID: 2886
		protected IDictionary ExternalProperties;

		// Token: 0x04000B47 RID: 2887
		protected IDictionary InternalProperties;
	}
}
