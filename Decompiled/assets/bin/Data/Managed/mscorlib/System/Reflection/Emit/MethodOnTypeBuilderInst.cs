using System;
using System.Globalization;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x020001AB RID: 427
	internal class MethodOnTypeBuilderInst : MethodInfo
	{
		// Token: 0x06001040 RID: 4160 RVA: 0x0003DC98 File Offset: 0x0003BE98
		public MethodOnTypeBuilderInst(MonoGenericClass instantiation, MethodBuilder mb)
		{
			this.instantiation = instantiation;
			this.mb = mb;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0003DCB0 File Offset: 0x0003BEB0
		internal MethodOnTypeBuilderInst(MethodOnTypeBuilderInst gmd, Type[] typeArguments)
		{
			this.instantiation = gmd.instantiation;
			this.mb = gmd.mb;
			this.method_arguments = new Type[typeArguments.Length];
			typeArguments.CopyTo(this.method_arguments, 0);
			this.generic_method_definition = gmd;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0003DD00 File Offset: 0x0003BF00
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0003DD08 File Offset: 0x0003BF08
		public override string Name
		{
			get
			{
				return this.mb.Name;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0003DD18 File Offset: 0x0003BF18
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0003DD20 File Offset: 0x0003BF20
		public override Type ReturnType
		{
			get
			{
				if (!((ModuleBuilder)this.mb.Module).assemblyb.IsCompilerContext)
				{
					return this.mb.ReturnType;
				}
				return this.instantiation.InflateType(this.mb.ReturnType, this.method_arguments);
			}
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0003DD74 File Offset: 0x0003BF74
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003DD7C File Offset: 0x0003BF7C
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0003DD84 File Offset: 0x0003BF84
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0003DD8C File Offset: 0x0003BF8C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.ReturnType.ToString());
			stringBuilder.Append(" ");
			stringBuilder.Append(this.mb.Name);
			stringBuilder.Append("(");
			if (((ModuleBuilder)this.mb.Module).assemblyb.IsCompilerContext)
			{
				ParameterInfo[] parameters = this.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(parameters[i].ParameterType);
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0003DE44 File Offset: 0x0003C044
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.mb.GetMethodImplementationFlags();
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0003DE54 File Offset: 0x0003C054
		public override ParameterInfo[] GetParameters()
		{
			if (!((ModuleBuilder)this.mb.Module).assemblyb.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ParameterInfo[] array = new ParameterInfo[this.mb.parameters.Length];
			for (int i = 0; i < this.mb.parameters.Length; i++)
			{
				Type type = this.instantiation.InflateType(this.mb.parameters[i], this.method_arguments);
				array[i] = new ParameterInfo((this.mb.pinfo != null) ? this.mb.pinfo[i + 1] : null, type, this, i + 1);
			}
			return array;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003DF0C File Offset: 0x0003C10C
		internal override int GetParameterCount()
		{
			return this.mb.GetParameterCount();
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0003DF1C File Offset: 0x0003C11C
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0003DF24 File Offset: 0x0003C124
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0003DF2C File Offset: 0x0003C12C
		public override MethodAttributes Attributes
		{
			get
			{
				return this.mb.Attributes;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0003DF3C File Offset: 0x0003C13C
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.mb.CallingConvention;
			}
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0003DF4C File Offset: 0x0003C14C
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			if (this.mb.generic_params == null || this.method_arguments != null)
			{
				throw new NotSupportedException();
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			if (this.mb.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException("Invalid argument array length");
			}
			return new MethodOnTypeBuilderInst(this, typeArguments);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0003DFE0 File Offset: 0x0003C1E0
		public override Type[] GetGenericArguments()
		{
			if (this.mb.generic_params == null)
			{
				return null;
			}
			Type[] array = this.method_arguments ?? this.mb.generic_params;
			Type[] array2 = new Type[array.Length];
			array.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0003E02C File Offset: 0x0003C22C
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.mb.generic_params == null)
				{
					throw new NotSupportedException();
				}
				if (this.method_arguments == null)
				{
					return true;
				}
				foreach (Type type in this.method_arguments)
				{
					if (type.ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0003E08C File Offset: 0x0003C28C
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.mb.generic_params != null && this.method_arguments == null;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0003E0AC File Offset: 0x0003C2AC
		public override bool IsGenericMethod
		{
			get
			{
				return this.mb.generic_params != null;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0003E0C0 File Offset: 0x0003C2C0
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040006F2 RID: 1778
		private MonoGenericClass instantiation;

		// Token: 0x040006F3 RID: 1779
		internal MethodBuilder mb;

		// Token: 0x040006F4 RID: 1780
		private Type[] method_arguments;

		// Token: 0x040006F5 RID: 1781
		private MethodOnTypeBuilderInst generic_method_definition;
	}
}
