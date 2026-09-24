using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002BD RID: 701
	[Serializable]
	internal class MonoMethodMessage : IInternalMessage, IMessage, IMethodCallMessage, IMethodMessage, IMethodReturnMessage
	{
		// Token: 0x06001608 RID: 5640 RVA: 0x0004D7DC File Offset: 0x0004B9DC
		public MonoMethodMessage(MethodBase method, object[] out_args)
		{
			if (method != null)
			{
				this.InitMessage((MonoMethod)method, out_args);
			}
			else
			{
				this.args = null;
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0004D804 File Offset: 0x0004BA04
		public MonoMethodMessage(Type type, string method_name, object[] in_args)
		{
			MethodInfo methodInfo = type.GetMethod(method_name);
			this.InitMessage((MonoMethod)methodInfo, null);
			int num = in_args.Length;
			for (int i = 0; i < num; i++)
			{
				this.args[i] = in_args[i];
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0004D850 File Offset: 0x0004BA50
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x0004D858 File Offset: 0x0004BA58
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x0600160C RID: 5644
		[MethodImpl(4096)]
		internal extern void InitMessage(MonoMethod method, object[] out_args);

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0004D864 File Offset: 0x0004BA64
		public IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new MethodCallDictionary(this);
				}
				return this.properties;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0004D884 File Offset: 0x0004BA84
		public int ArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				return this.args.Length;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0004D8AC File Offset: 0x0004BAAC
		public object[] Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0004D8B4 File Offset: 0x0004BAB4
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0004D8B8 File Offset: 0x0004BAB8
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x0004D8C0 File Offset: 0x0004BAC0
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this.ctx;
			}
			set
			{
				this.ctx = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0004D8CC File Offset: 0x0004BACC
		public MethodBase MethodBase
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0004D8D4 File Offset: 0x0004BAD4
		public string MethodName
		{
			get
			{
				if (this.method == null)
				{
					return string.Empty;
				}
				return this.method.Name;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0004D8F4 File Offset: 0x0004BAF4
		public object MethodSignature
		{
			get
			{
				if (this.methodSignature == null)
				{
					ParameterInfo[] parameters = this.method.GetParameters();
					this.methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this.methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this.methodSignature;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0004D950 File Offset: 0x0004BB50
		public string TypeName
		{
			get
			{
				if (this.method == null)
				{
					return string.Empty;
				}
				return this.method.DeclaringType.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0004D974 File Offset: 0x0004BB74
		// (set) Token: 0x06001618 RID: 5656 RVA: 0x0004D97C File Offset: 0x0004BB7C
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0004D988 File Offset: 0x0004BB88
		public object GetArg(int arg_num)
		{
			if (this.args == null)
			{
				return null;
			}
			return this.args[arg_num];
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0004D9A0 File Offset: 0x0004BBA0
		public string GetArgName(int arg_num)
		{
			if (this.args == null)
			{
				return string.Empty;
			}
			return this.names[arg_num];
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x0004D9BC File Offset: 0x0004BBBC
		public int InArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				foreach (byte b in this.arg_types)
				{
					if ((b & 1) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0004DA14 File Offset: 0x0004BC14
		public object[] InArgs
		{
			get
			{
				int inArgCount = this.InArgCount;
				object[] array = new object[inArgCount];
				int num2;
				int num = num2 = 0;
				foreach (byte b in this.arg_types)
				{
					if ((b & 1) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0004DA78 File Offset: 0x0004BC78
		public object GetInArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 1) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0004DAD0 File Offset: 0x0004BCD0
		public string GetInArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 1) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x0004DB28 File Offset: 0x0004BD28
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0004DB30 File Offset: 0x0004BD30
		public int OutArgCount
		{
			get
			{
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				foreach (byte b in this.arg_types)
				{
					if ((b & 2) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x0004DB78 File Offset: 0x0004BD78
		public object[] OutArgs
		{
			get
			{
				if (this.args == null)
				{
					return null;
				}
				int outArgCount = this.OutArgCount;
				object[] array = new object[outArgCount];
				int num2;
				int num = num2 = 0;
				foreach (byte b in this.arg_types)
				{
					if ((b & 2) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0004DBEC File Offset: 0x0004BDEC
		public object ReturnValue
		{
			get
			{
				return this.rval;
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0004DBF4 File Offset: 0x0004BDF4
		public object GetOutArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 2) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0004DC4C File Offset: 0x0004BE4C
		public string GetOutArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 2) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x0004DCA4 File Offset: 0x0004BEA4
		public bool IsAsync
		{
			get
			{
				return this.asyncResult != null;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0004DCB4 File Offset: 0x0004BEB4
		public AsyncResult AsyncResult
		{
			get
			{
				return this.asyncResult;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0004DCBC File Offset: 0x0004BEBC
		internal CallType CallType
		{
			get
			{
				if (this.call_type == CallType.Sync && RemotingServices.IsOneWay(this.method))
				{
					this.call_type = CallType.OneWay;
				}
				return this.call_type;
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0004DCE8 File Offset: 0x0004BEE8
		public bool NeedsOutProcessing(out int outCount)
		{
			bool flag = false;
			outCount = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 2) != 0)
				{
					outCount++;
				}
				else if ((b & 4) != 0)
				{
					flag = true;
				}
			}
			return outCount > 0 || flag;
		}

		// Token: 0x04000B4B RID: 2891
		private MonoMethod method;

		// Token: 0x04000B4C RID: 2892
		private object[] args;

		// Token: 0x04000B4D RID: 2893
		private string[] names;

		// Token: 0x04000B4E RID: 2894
		private byte[] arg_types;

		// Token: 0x04000B4F RID: 2895
		public LogicalCallContext ctx;

		// Token: 0x04000B50 RID: 2896
		public object rval;

		// Token: 0x04000B51 RID: 2897
		public Exception exc;

		// Token: 0x04000B52 RID: 2898
		private AsyncResult asyncResult;

		// Token: 0x04000B53 RID: 2899
		private CallType call_type;

		// Token: 0x04000B54 RID: 2900
		private string uri;

		// Token: 0x04000B55 RID: 2901
		private MethodCallDictionary properties;

		// Token: 0x04000B56 RID: 2902
		private Type[] methodSignature;

		// Token: 0x04000B57 RID: 2903
		private Identity identity;
	}
}
