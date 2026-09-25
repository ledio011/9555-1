using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200029D RID: 669
	internal class CADMethodCallMessage : CADMessageBase
	{
		// Token: 0x06001539 RID: 5433 RVA: 0x0004AECC File Offset: 0x000490CC
		internal CADMethodCallMessage(IMethodCallMessage callMsg)
		{
			this._uri = callMsg.Uri;
			this.MethodHandle = callMsg.MethodBase.MethodHandle;
			this.FullTypeName = callMsg.MethodBase.DeclaringType.AssemblyQualifiedName;
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(callMsg.Properties, ref arrayList);
			this._args = base.MarshalArguments(callMsg.Args, ref arrayList);
			base.SaveLogicalCallContext(callMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0004AF64 File Offset: 0x00049164
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0004AF6C File Offset: 0x0004916C
		internal static CADMethodCallMessage Create(IMessage callMsg)
		{
			IMethodCallMessage methodCallMessage = callMsg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new CADMethodCallMessage(methodCallMessage);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0004AF90 File Offset: 0x00049190
		internal ArrayList GetArguments()
		{
			ArrayList result = null;
			if (this._serializedArgs != null)
			{
				object[] c = (object[])CADSerializer.DeserializeObject(new MemoryStream(this._serializedArgs));
				result = new ArrayList(c);
				this._serializedArgs = null;
			}
			return result;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0004AFD0 File Offset: 0x000491D0
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0004AFE0 File Offset: 0x000491E0
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0004AFE8 File Offset: 0x000491E8
		private static Type[] GetSignature(MethodBase methodBase, bool load)
		{
			ParameterInfo[] parameters = methodBase.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				if (load)
				{
					array[i] = Type.GetType(parameters[i].ParameterType.AssemblyQualifiedName, true);
				}
				else
				{
					array[i] = parameters[i].ParameterType;
				}
			}
			return array;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0004B048 File Offset: 0x00049248
		internal MethodBase GetMethod()
		{
			Type type = Type.GetType(this.FullTypeName);
			MethodBase methodBase;
			if (type.IsGenericType || type.IsGenericTypeDefinition)
			{
				methodBase = MethodBase.GetMethodFromHandleNoGenericCheck(this.MethodHandle);
			}
			else
			{
				methodBase = MethodBase.GetMethodFromHandle(this.MethodHandle);
			}
			if (type == methodBase.DeclaringType)
			{
				return methodBase;
			}
			Type[] signature = CADMethodCallMessage.GetSignature(methodBase, true);
			if (methodBase.IsGenericMethod)
			{
				MethodBase[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				Type[] genericArguments = methodBase.GetGenericArguments();
				foreach (MethodBase methodBase2 in methods)
				{
					if (methodBase2.IsGenericMethod && !(methodBase2.Name != methodBase.Name))
					{
						Type[] genericArguments2 = methodBase2.GetGenericArguments();
						if (genericArguments.Length == genericArguments2.Length)
						{
							MethodInfo methodInfo = ((MethodInfo)methodBase2).MakeGenericMethod(genericArguments);
							Type[] signature2 = CADMethodCallMessage.GetSignature(methodInfo, false);
							if (signature2.Length == signature.Length)
							{
								bool flag = false;
								for (int j = signature2.Length - 1; j >= 0; j--)
								{
									if (signature2[j] != signature[j])
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									return methodInfo;
								}
							}
						}
					}
				}
				return methodBase;
			}
			MethodBase method = type.GetMethod(methodBase.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
			if (method == null)
			{
				throw new RemotingException(string.Concat(new object[]
				{
					"Method '",
					methodBase.Name,
					"' not found in type '",
					type,
					"'"
				}));
			}
			return method;
		}

		// Token: 0x04000AFE RID: 2814
		private string _uri;

		// Token: 0x04000AFF RID: 2815
		internal RuntimeMethodHandle MethodHandle;

		// Token: 0x04000B00 RID: 2816
		internal string FullTypeName;
	}
}
