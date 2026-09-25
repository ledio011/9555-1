using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000CE RID: 206
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Serializable]
	public abstract class Delegate : ICloneable, ISerializable
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000203D0 File Offset: 0x0001E5D0
		public MethodInfo Method
		{
			get
			{
				if (this.method_info != null)
				{
					return this.method_info;
				}
				if (this.method != IntPtr.Zero)
				{
					this.method_info = (MethodInfo)MethodBase.GetMethodFromHandleNoGenericCheck(new RuntimeMethodHandle(this.method));
				}
				return this.method_info;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00020428 File Offset: 0x0001E628
		public object Target
		{
			get
			{
				return this.m_target;
			}
		}

		// Token: 0x0600085C RID: 2140
		[MethodImpl(4096)]
		internal static extern Delegate CreateDelegate_internal(Type type, object target, MethodInfo info, bool throwOnBindFailure);

		// Token: 0x0600085D RID: 2141
		[MethodImpl(4096)]
		internal extern void SetMulticastInvoke();

		// Token: 0x0600085E RID: 2142 RVA: 0x00020430 File Offset: 0x0001E630
		private static bool arg_type_match(Type delArgType, Type argType)
		{
			bool flag = delArgType == argType;
			if (!flag && !argType.IsValueType && argType.IsAssignableFrom(delArgType))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00020464 File Offset: 0x0001E664
		private static bool return_type_match(Type delReturnType, Type returnType)
		{
			bool flag = returnType == delReturnType;
			if (!flag && !returnType.IsValueType && delReturnType.IsAssignableFrom(returnType))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00020498 File Offset: 0x0001E698
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!type.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw new ArgumentException("type is not a subclass of Multicastdelegate");
			}
			MethodInfo methodInfo = type.GetMethod("Invoke");
			if (!Delegate.return_type_match(methodInfo.ReturnType, method.ReturnType))
			{
				if (throwOnBindFailure)
				{
					throw new ArgumentException("method return type is incompatible");
				}
				return null;
			}
			else
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				ParameterInfo[] parameters2 = method.GetParameters();
				bool flag;
				if (firstArgument != null)
				{
					if (!method.IsStatic)
					{
						flag = (parameters2.Length == parameters.Length);
					}
					else
					{
						flag = (parameters2.Length == parameters.Length + 1);
					}
				}
				else if (!method.IsStatic)
				{
					flag = (parameters2.Length + 1 == parameters.Length);
				}
				else
				{
					flag = (parameters2.Length == parameters.Length);
					if (!flag)
					{
						flag = (parameters2.Length == parameters.Length + 1);
					}
				}
				if (!flag)
				{
					if (throwOnBindFailure)
					{
						throw new ArgumentException("method argument length mismatch");
					}
					return null;
				}
				else
				{
					bool flag2;
					if (firstArgument != null)
					{
						if (!method.IsStatic)
						{
							flag2 = Delegate.arg_type_match(firstArgument.GetType(), method.DeclaringType);
							for (int i = 0; i < parameters2.Length; i++)
							{
								flag2 &= Delegate.arg_type_match(parameters[i].ParameterType, parameters2[i].ParameterType);
							}
						}
						else
						{
							flag2 = Delegate.arg_type_match(firstArgument.GetType(), parameters2[0].ParameterType);
							for (int j = 1; j < parameters2.Length; j++)
							{
								flag2 &= Delegate.arg_type_match(parameters[j - 1].ParameterType, parameters2[j].ParameterType);
							}
						}
					}
					else if (!method.IsStatic)
					{
						flag2 = Delegate.arg_type_match(parameters[0].ParameterType, method.DeclaringType);
						for (int k = 0; k < parameters2.Length; k++)
						{
							flag2 &= Delegate.arg_type_match(parameters[k + 1].ParameterType, parameters2[k].ParameterType);
						}
					}
					else if (parameters.Length + 1 == parameters2.Length)
					{
						flag2 = !parameters2[0].ParameterType.IsValueType;
						for (int l = 0; l < parameters.Length; l++)
						{
							flag2 &= Delegate.arg_type_match(parameters[l].ParameterType, parameters2[l + 1].ParameterType);
						}
					}
					else
					{
						flag2 = true;
						for (int m = 0; m < parameters2.Length; m++)
						{
							flag2 &= Delegate.arg_type_match(parameters[m].ParameterType, parameters2[m].ParameterType);
						}
					}
					if (flag2)
					{
						Delegate @delegate = Delegate.CreateDelegate_internal(type, firstArgument, method, throwOnBindFailure);
						if (@delegate != null)
						{
							@delegate.original_method_info = method;
						}
						return @delegate;
					}
					if (throwOnBindFailure)
					{
						throw new ArgumentException("method arguments are incompatible");
					}
					return null;
				}
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00020780 File Offset: 0x0001E980
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, firstArgument, method, true);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002078C File Offset: 0x0001E98C
		public static Delegate CreateDelegate(Type type, MethodInfo method, bool throwOnBindFailure)
		{
			return Delegate.CreateDelegate(type, null, method, throwOnBindFailure);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00020798 File Offset: 0x0001E998
		public static Delegate CreateDelegate(Type type, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, method, true);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000207A4 File Offset: 0x0001E9A4
		public static Delegate CreateDelegate(Type type, object target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000207B0 File Offset: 0x0001E9B0
		private static MethodInfo GetCandidateMethod(Type type, Type target, string method, BindingFlags bflags, bool ignoreCase, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!type.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw new ArgumentException("type is not subclass of MulticastDelegate.");
			}
			MethodInfo methodInfo = type.GetMethod("Invoke");
			ParameterInfo[] parameters = methodInfo.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.ExactBinding | bflags;
			if (ignoreCase)
			{
				bindingFlags |= BindingFlags.IgnoreCase;
			}
			MethodInfo methodInfo2 = null;
			for (Type type2 = target; type2 != null; type2 = type2.BaseType)
			{
				MethodInfo methodInfo3 = type2.GetMethod(method, bindingFlags, null, array, new ParameterModifier[0]);
				if (methodInfo3 != null && Delegate.return_type_match(methodInfo.ReturnType, methodInfo3.ReturnType))
				{
					methodInfo2 = methodInfo3;
					break;
				}
			}
			if (methodInfo2 != null)
			{
				return methodInfo2;
			}
			if (throwOnBindFailure)
			{
				throw new ArgumentException("Couldn't bind to method '" + method + "'.");
			}
			return null;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000208D0 File Offset: 0x0001EAD0
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			MethodInfo candidateMethod = Delegate.GetCandidateMethod(type, target, method, BindingFlags.Static, ignoreCase, throwOnBindFailure);
			if (candidateMethod == null)
			{
				return null;
			}
			return Delegate.CreateDelegate_internal(type, null, candidateMethod, throwOnBindFailure);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00020910 File Offset: 0x0001EB10
		public static Delegate CreateDelegate(Type type, Type target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false, true);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002091C File Offset: 0x0001EB1C
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			MethodInfo candidateMethod = Delegate.GetCandidateMethod(type, target.GetType(), method, BindingFlags.Instance, ignoreCase, throwOnBindFailure);
			if (candidateMethod == null)
			{
				return null;
			}
			return Delegate.CreateDelegate_internal(type, target, candidateMethod, throwOnBindFailure);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00020960 File Offset: 0x0001EB60
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002096C File Offset: 0x0001EB6C
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00020974 File Offset: 0x0001EB74
		public override bool Equals(object obj)
		{
			Delegate @delegate = obj as Delegate;
			return @delegate != null && (@delegate.m_target == this.m_target && @delegate.method == this.method) && ((@delegate.data == null && this.data == null) || (@delegate.data != null && this.data != null && @delegate.data.target_type == this.data.target_type && @delegate.data.method_name == this.data.method_name));
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00020A24 File Offset: 0x0001EC24
		public override int GetHashCode()
		{
			return this.method.GetHashCode() ^ ((this.m_target == null) ? 0 : this.m_target.GetHashCode());
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00020A50 File Offset: 0x0001EC50
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			DelegateSerializationHolder.GetDelegateData(this, info, context);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00020A5C File Offset: 0x0001EC5C
		public virtual Delegate[] GetInvocationList()
		{
			return new Delegate[]
			{
				this
			};
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00020A68 File Offset: 0x0001EC68
		public static Delegate Combine(Delegate a, Delegate b)
		{
			if (a == null)
			{
				if (b == null)
				{
					return null;
				}
				return b;
			}
			else
			{
				if (b == null)
				{
					return a;
				}
				if (a.GetType() != b.GetType())
				{
					throw new ArgumentException(Locale.GetText("Incompatible Delegate Types."));
				}
				return a.CombineImpl(b);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00020AB8 File Offset: 0x0001ECB8
		[ComVisible(true)]
		public static Delegate Combine(params Delegate[] delegates)
		{
			if (delegates == null)
			{
				return null;
			}
			Delegate @delegate = null;
			foreach (Delegate b in delegates)
			{
				@delegate = Delegate.Combine(@delegate, b);
			}
			return @delegate;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00020AF4 File Offset: 0x0001ECF4
		protected virtual Delegate CombineImpl(Delegate d)
		{
			throw new MulticastNotSupportedException(string.Empty);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00020B00 File Offset: 0x0001ED00
		public static Delegate Remove(Delegate source, Delegate value)
		{
			if (source == null)
			{
				return null;
			}
			return source.RemoveImpl(value);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00020B14 File Offset: 0x0001ED14
		protected virtual Delegate RemoveImpl(Delegate d)
		{
			if (this.Equals(d))
			{
				return null;
			}
			return this;
		}

		// Token: 0x040002BA RID: 698
		private IntPtr method_ptr;

		// Token: 0x040002BB RID: 699
		private IntPtr invoke_impl;

		// Token: 0x040002BC RID: 700
		private object m_target;

		// Token: 0x040002BD RID: 701
		private IntPtr method;

		// Token: 0x040002BE RID: 702
		private IntPtr delegate_trampoline;

		// Token: 0x040002BF RID: 703
		private IntPtr method_code;

		// Token: 0x040002C0 RID: 704
		private MethodInfo method_info;

		// Token: 0x040002C1 RID: 705
		private MethodInfo original_method_info;

		// Token: 0x040002C2 RID: 706
		private DelegateData data;
	}
}
