using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002C2 RID: 706
	[ComVisible(true)]
	public class ReturnMessage : IInternalMessage, IMessage, IMethodMessage, IMethodReturnMessage
	{
		// Token: 0x06001632 RID: 5682 RVA: 0x0004DE68 File Offset: 0x0004C068
		public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
		{
			this._returnValue = ret;
			this._args = outArgs;
			this._outArgsCount = outArgsCount;
			this._callCtx = callCtx;
			if (mcm != null)
			{
				this._uri = mcm.Uri;
				this._methodBase = mcm.MethodBase;
			}
			if (this._args == null)
			{
				this._args = new object[outArgsCount];
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0004DED0 File Offset: 0x0004C0D0
		public ReturnMessage(Exception e, IMethodCallMessage mcm)
		{
			this._exception = e;
			if (mcm != null)
			{
				this._methodBase = mcm.MethodBase;
				this._callCtx = mcm.LogicalCallContext;
			}
			this._args = new object[0];
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0004DF0C File Offset: 0x0004C10C
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x0004DF14 File Offset: 0x0004C114
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

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0004DF20 File Offset: 0x0004C120
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x0004DF28 File Offset: 0x0004C128
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

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0004DF34 File Offset: 0x0004C134
		public int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0004DF40 File Offset: 0x0004C140
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0004DF48 File Offset: 0x0004C148
		public bool HasVarArgs
		{
			get
			{
				return this._methodBase != null && (this._methodBase.CallingConvention | CallingConventions.VarArgs) != (CallingConventions)0;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x0004DF6C File Offset: 0x0004C16C
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				if (this._callCtx == null)
				{
					this._callCtx = new LogicalCallContext();
				}
				return this._callCtx;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0004DF8C File Offset: 0x0004C18C
		public MethodBase MethodBase
		{
			get
			{
				return this._methodBase;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0004DF94 File Offset: 0x0004C194
		public string MethodName
		{
			get
			{
				if (this._methodBase != null && this._methodName == null)
				{
					this._methodName = this._methodBase.Name;
				}
				return this._methodName;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0004DFC4 File Offset: 0x0004C1C4
		public object MethodSignature
		{
			get
			{
				if (this._methodBase != null && this._methodSignature == null)
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

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0004E02C File Offset: 0x0004C22C
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnDictionary(this);
				}
				return this._properties;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0004E04C File Offset: 0x0004C24C
		public string TypeName
		{
			get
			{
				if (this._methodBase != null && this._typeName == null)
				{
					this._typeName = this._methodBase.DeclaringType.AssemblyQualifiedName;
				}
				return this._typeName;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0004E080 File Offset: 0x0004C280
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x0004E088 File Offset: 0x0004C288
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

		// Token: 0x06001643 RID: 5699 RVA: 0x0004E094 File Offset: 0x0004C294
		public object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0004E0A0 File Offset: 0x0004C2A0
		public string GetArgName(int index)
		{
			return this._methodBase.GetParameters()[index].Name;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0004E0B4 File Offset: 0x0004C2B4
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0004E0BC File Offset: 0x0004C2BC
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

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0004E10C File Offset: 0x0004C30C
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0004E16C File Offset: 0x0004C36C
		public virtual object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0004E174 File Offset: 0x0004C374
		public object GetOutArg(int argNum)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0004E1A8 File Offset: 0x0004C3A8
		public string GetOutArgName(int index)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x04000B5E RID: 2910
		private object[] _outArgs;

		// Token: 0x04000B5F RID: 2911
		private object[] _args;

		// Token: 0x04000B60 RID: 2912
		private int _outArgsCount;

		// Token: 0x04000B61 RID: 2913
		private LogicalCallContext _callCtx;

		// Token: 0x04000B62 RID: 2914
		private object _returnValue;

		// Token: 0x04000B63 RID: 2915
		private string _uri;

		// Token: 0x04000B64 RID: 2916
		private Exception _exception;

		// Token: 0x04000B65 RID: 2917
		private MethodBase _methodBase;

		// Token: 0x04000B66 RID: 2918
		private string _methodName;

		// Token: 0x04000B67 RID: 2919
		private Type[] _methodSignature;

		// Token: 0x04000B68 RID: 2920
		private string _typeName;

		// Token: 0x04000B69 RID: 2921
		private MethodReturnDictionary _properties;

		// Token: 0x04000B6A RID: 2922
		private Identity _targetIdentity;

		// Token: 0x04000B6B RID: 2923
		private ArgInfo _inArgInfo;
	}
}
