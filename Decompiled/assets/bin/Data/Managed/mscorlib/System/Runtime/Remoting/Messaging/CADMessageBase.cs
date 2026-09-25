using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200029C RID: 668
	internal class CADMessageBase
	{
		// Token: 0x06001530 RID: 5424 RVA: 0x0004A80C File Offset: 0x00048A0C
		internal static int MarshalProperties(IDictionary dict, ref ArrayList args)
		{
			int num = 0;
			MethodDictionary methodDictionary = dict as MethodDictionary;
			if (methodDictionary != null)
			{
				if (methodDictionary.HasInternalProperties)
				{
					IDictionary internalProperties = methodDictionary.InternalProperties;
					if (internalProperties != null)
					{
						foreach (object obj in internalProperties)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							if (args == null)
							{
								args = new ArrayList();
							}
							args.Add(dictionaryEntry);
							num++;
						}
					}
				}
			}
			else if (dict != null)
			{
				foreach (object obj2 in dict)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (args == null)
					{
						args = new ArrayList();
					}
					args.Add(dictionaryEntry2);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0004A930 File Offset: 0x00048B30
		internal static void UnmarshalProperties(IDictionary dict, int count, ArrayList args)
		{
			for (int i = 0; i < count; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)args[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0004A970 File Offset: 0x00048B70
		private static bool IsPossibleToIgnoreMarshal(object obj)
		{
			Type type = obj.GetType();
			return type.IsPrimitive || type == typeof(void) || (type.IsArray && type.GetElementType().IsPrimitive && ((Array)obj).Rank == 1) || (obj is string || obj is DateTime || obj is TimeSpan);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0004A9F4 File Offset: 0x00048BF4
		protected object MarshalArgument(object arg, ref ArrayList args)
		{
			if (arg == null)
			{
				return null;
			}
			if (CADMessageBase.IsPossibleToIgnoreMarshal(arg))
			{
				return arg;
			}
			MarshalByRefObject marshalByRefObject = arg as MarshalByRefObject;
			if (marshalByRefObject != null)
			{
				if (!RemotingServices.IsTransparentProxy(marshalByRefObject))
				{
					ObjRef o = RemotingServices.Marshal(marshalByRefObject);
					return new CADObjRef(o, Thread.GetDomainID());
				}
			}
			if (args == null)
			{
				args = new ArrayList();
			}
			args.Add(arg);
			return new CADArgHolder(args.Count - 1);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0004AA6C File Offset: 0x00048C6C
		protected object UnmarshalArgument(object arg, ArrayList args)
		{
			if (arg == null)
			{
				return null;
			}
			CADArgHolder cadargHolder = arg as CADArgHolder;
			if (cadargHolder != null)
			{
				return args[cadargHolder.index];
			}
			CADObjRef cadobjRef = arg as CADObjRef;
			if (cadobjRef != null)
			{
				string typeName = string.Copy(cadobjRef.TypeName);
				string uri = string.Copy(cadobjRef.URI);
				int sourceDomain = cadobjRef.SourceDomain;
				ChannelInfo cinfo = new ChannelInfo(new CrossAppDomainData(sourceDomain));
				ObjRef objectRef = new ObjRef(typeName, uri, cinfo);
				return RemotingServices.Unmarshal(objectRef);
			}
			if (arg is Array)
			{
				Array array = (Array)arg;
				Array array2;
				switch (Type.GetTypeCode(arg.GetType().GetElementType()))
				{
				case TypeCode.Boolean:
					array2 = new bool[array.Length];
					break;
				case TypeCode.Char:
					array2 = new char[array.Length];
					break;
				case TypeCode.SByte:
					array2 = new sbyte[array.Length];
					break;
				case TypeCode.Byte:
					array2 = new byte[array.Length];
					break;
				case TypeCode.Int16:
					array2 = new short[array.Length];
					break;
				case TypeCode.UInt16:
					array2 = new ushort[array.Length];
					break;
				case TypeCode.Int32:
					array2 = new int[array.Length];
					break;
				case TypeCode.UInt32:
					array2 = new uint[array.Length];
					break;
				case TypeCode.Int64:
					array2 = new long[array.Length];
					break;
				case TypeCode.UInt64:
					array2 = new ulong[array.Length];
					break;
				case TypeCode.Single:
					array2 = new float[array.Length];
					break;
				case TypeCode.Double:
					array2 = new double[array.Length];
					break;
				case TypeCode.Decimal:
					array2 = new decimal[array.Length];
					break;
				default:
					throw new NotSupportedException();
				}
				array.CopyTo(array2, 0);
				return array2;
			}
			switch (Type.GetTypeCode(arg.GetType()))
			{
			case TypeCode.Boolean:
				return (bool)arg;
			case TypeCode.Char:
				return (char)arg;
			case TypeCode.SByte:
				return (sbyte)arg;
			case TypeCode.Byte:
				return (byte)arg;
			case TypeCode.Int16:
				return (short)arg;
			case TypeCode.UInt16:
				return (ushort)arg;
			case TypeCode.Int32:
				return (int)arg;
			case TypeCode.UInt32:
				return (uint)arg;
			case TypeCode.Int64:
				return (long)arg;
			case TypeCode.UInt64:
				return (ulong)arg;
			case TypeCode.Single:
				return (float)arg;
			case TypeCode.Double:
				return (double)arg;
			case TypeCode.Decimal:
				return (decimal)arg;
			case TypeCode.DateTime:
				return new DateTime(((DateTime)arg).Ticks);
			case TypeCode.String:
				return string.Copy((string)arg);
			}
			if (arg is TimeSpan)
			{
				return new TimeSpan(((TimeSpan)arg).Ticks);
			}
			if (arg is IntPtr)
			{
				return (IntPtr)arg;
			}
			throw new NotSupportedException("Parameter of type " + arg.GetType() + " cannot be unmarshalled");
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0004ADD4 File Offset: 0x00048FD4
		internal object[] MarshalArguments(object[] arguments, ref ArrayList args)
		{
			object[] array = new object[arguments.Length];
			int num = arguments.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.MarshalArgument(arguments[i], ref args);
			}
			return array;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0004AE10 File Offset: 0x00049010
		internal object[] UnmarshalArguments(object[] arguments, ArrayList args)
		{
			object[] array = new object[arguments.Length];
			int num = arguments.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.UnmarshalArgument(arguments[i], args);
			}
			return array;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0004AE4C File Offset: 0x0004904C
		protected void SaveLogicalCallContext(IMethodMessage msg, ref ArrayList serializeList)
		{
			if (msg.LogicalCallContext != null && msg.LogicalCallContext.HasInfo)
			{
				if (serializeList == null)
				{
					serializeList = new ArrayList();
				}
				this._callContext = new CADArgHolder(serializeList.Count);
				serializeList.Add(msg.LogicalCallContext);
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0004AEA4 File Offset: 0x000490A4
		internal LogicalCallContext GetLogicalCallContext(ArrayList args)
		{
			if (this._callContext == null)
			{
				return null;
			}
			return (LogicalCallContext)args[this._callContext.index];
		}

		// Token: 0x04000AFA RID: 2810
		protected object[] _args;

		// Token: 0x04000AFB RID: 2811
		protected byte[] _serializedArgs;

		// Token: 0x04000AFC RID: 2812
		protected int _propertyCount;

		// Token: 0x04000AFD RID: 2813
		protected CADArgHolder _callContext;
	}
}
