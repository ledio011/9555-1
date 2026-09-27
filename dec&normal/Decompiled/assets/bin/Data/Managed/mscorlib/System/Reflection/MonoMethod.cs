using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	internal class MonoMethod : MethodInfo, ISerializable
	{
		// Token: 0x060011EA RID: 4586 RVA: 0x00043F9C File Offset: 0x0004219C
		internal MonoMethod()
		{
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00043FA4 File Offset: 0x000421A4
		internal MonoMethod(RuntimeMethodHandle mhandle)
		{
			this.mhandle = mhandle.Value;
		}

		// Token: 0x060011EC RID: 4588
		[MethodImpl(4096)]
		internal static extern string get_name(MethodBase method);

		// Token: 0x060011ED RID: 4589
		[MethodImpl(4096)]
		internal static extern MonoMethod get_base_definition(MonoMethod method);

		// Token: 0x060011EE RID: 4590 RVA: 0x00043FBC File Offset: 0x000421BC
		public override MethodInfo GetBaseDefinition()
		{
			return MonoMethod.get_base_definition(this);
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00043FC4 File Offset: 0x000421C4
		public override Type ReturnType
		{
			get
			{
				return MonoMethodInfo.GetReturnType(this.mhandle);
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00043FD4 File Offset: 0x000421D4
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00043FE4 File Offset: 0x000421E4
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			ParameterInfo[] array = new ParameterInfo[parametersInfo.Length];
			parametersInfo.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060011F2 RID: 4594
		[MethodImpl(4096)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x060011F3 RID: 4595 RVA: 0x00044010 File Offset: 0x00042210
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if ((parameters == null && parametersInfo.Length != 0) || (parameters != null && parameters.Length != parametersInfo.Length))
			{
				throw new TargetParameterCountException("parameters do not match signature");
			}
			if ((invokeAttr & BindingFlags.ExactBinding) == BindingFlags.Default)
			{
				if (!Binder.ConvertArgs(binder, parameters, parametersInfo, culture))
				{
					throw new ArgumentException("failed to convert parameters");
				}
			}
			else
			{
				for (int i = 0; i < parametersInfo.Length; i++)
				{
					if (parameters[i].GetType() != parametersInfo[i].ParameterType)
					{
						throw new ArgumentException("parameters do not match signature");
					}
				}
			}
			if (this.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on types or methods for which ContainsGenericParameters is true.");
			}
			object result = null;
			Exception ex;
			try
			{
				result = this.InternalInvoke(obj, parameters, out ex);
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (MethodAccessException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new TargetInvocationException(inner);
			}
			if (ex != null)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00044140 File Offset: 0x00042340
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00044150 File Offset: 0x00042350
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00044160 File Offset: 0x00042360
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00044170 File Offset: 0x00042370
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00044178 File Offset: 0x00042378
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00044188 File Offset: 0x00042388
		public override string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return MonoMethod.get_name(this);
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000441A4 File Offset: 0x000423A4
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000441B0 File Offset: 0x000423B0
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x000441BC File Offset: 0x000423BC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060011FD RID: 4605
		[MethodImpl(4096)]
		internal static extern DllImportAttribute GetDllImportAttribute(IntPtr mhandle);

		// Token: 0x060011FE RID: 4606 RVA: 0x000441C8 File Offset: 0x000423C8
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new PreserveSigAttribute();
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				DllImportAttribute dllImportAttribute = MonoMethod.GetDllImportAttribute(this.mhandle);
				if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
				{
					dllImportAttribute.PreserveSig = true;
				}
				array[num++] = dllImportAttribute;
			}
			return array;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00044280 File Offset: 0x00042480
		private static bool ShouldPrintFullName(Type type)
		{
			return type.IsClass && (!type.IsPointer || (!type.GetElementType().IsPrimitive && !type.GetElementType().IsNested));
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000442C0 File Offset: 0x000424C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type returnType = this.ReturnType;
			if (MonoMethod.ShouldPrintFullName(returnType))
			{
				stringBuilder.Append(returnType.ToString());
			}
			else
			{
				stringBuilder.Append(returnType.Name);
			}
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Name);
			if (this.IsGenericMethod)
			{
				Type[] genericArguments = this.GetGenericArguments();
				stringBuilder.Append("[");
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(genericArguments[i].Name);
				}
				stringBuilder.Append("]");
			}
			stringBuilder.Append("(");
			ParameterInfo[] parameters = this.GetParameters();
			for (int j = 0; j < parameters.Length; j++)
			{
				if (j > 0)
				{
					stringBuilder.Append(", ");
				}
				Type type = parameters[j].ParameterType;
				bool isByRef = type.IsByRef;
				if (isByRef)
				{
					type = type.GetElementType();
				}
				if (MonoMethod.ShouldPrintFullName(type))
				{
					stringBuilder.Append(type.ToString());
				}
				else
				{
					stringBuilder.Append(type.Name);
				}
				if (isByRef)
				{
					stringBuilder.Append(" ByRef");
				}
			}
			if ((this.CallingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				if (parameters.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("...");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00044464 File Offset: 0x00042664
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Type[] genericArguments = (!this.IsGenericMethod || this.IsGenericMethodDefinition) ? null : this.GetGenericArguments();
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Method, genericArguments);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000444B0 File Offset: 0x000426B0
		public override MethodInfo MakeGenericMethod(Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			for (int i = 0; i < methodInstantiation.Length; i++)
			{
				if (methodInstantiation[i] == null)
				{
					throw new ArgumentNullException();
				}
			}
			MethodInfo methodInfo = this.MakeGenericMethod_impl(methodInstantiation);
			if (methodInfo == null)
			{
				throw new ArgumentException(string.Format("The method has {0} generic parameter(s) but {1} generic argument(s) were provided.", this.GetGenericArguments().Length, methodInstantiation.Length));
			}
			return methodInfo;
		}

		// Token: 0x06001203 RID: 4611
		[MethodImpl(4096)]
		private extern MethodInfo MakeGenericMethod_impl(Type[] types);

		// Token: 0x06001204 RID: 4612
		[MethodImpl(4096)]
		public override extern Type[] GetGenericArguments();

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001205 RID: 4613
		public override extern bool IsGenericMethodDefinition { [MethodImpl(4096)] get; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001206 RID: 4614
		public override extern bool IsGenericMethod { [MethodImpl(4096)] get; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x00044528 File Offset: 0x00042728
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericMethod)
				{
					foreach (Type type in this.GetGenericArguments())
					{
						if (type.ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x04000919 RID: 2329
		internal IntPtr mhandle;

		// Token: 0x0400091A RID: 2330
		private string name;

		// Token: 0x0400091B RID: 2331
		private Type reftype;
	}
}
