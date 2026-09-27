using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001AA RID: 426
	[ComDefaultInterface(typeof(_MethodBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public sealed class MethodBuilder : MethodInfo, _MethodBuilder
	{
		// Token: 0x0600101F RID: 4127 RVA: 0x0003D7DC File Offset: 0x0003B9DC
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.name = name;
			this.attrs = attributes;
			this.call_conv = callingConvention;
			this.rtype = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				this.call_conv |= CallingConventions.HasThis;
			}
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Elements of the parameterTypes array cannot be null", "parameterTypes");
					}
				}
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, parameterTypes.Length);
			}
			this.type = tb;
			this.table_idx = this.get_next_table_index(this, 6, true);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0003D8D8 File Offset: 0x0003BAD8
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.NotSupported();
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0003D8E8 File Offset: 0x0003BAE8
		public override Type ReturnType
		{
			get
			{
				return this.rtype;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0003D8F0 File Offset: 0x0003BAF0
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003D8F8 File Offset: 0x0003BAF8
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0003D900 File Offset: 0x0003BB00
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0003D908 File Offset: 0x0003BB08
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0003D910 File Offset: 0x0003BB10
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0003D918 File Offset: 0x0003BB18
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0003D92C File Offset: 0x0003BB2C
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0003D930 File Offset: 0x0003BB30
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0003D938 File Offset: 0x0003BB38
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.NotSupported();
			}
			if (this.parameters == null)
			{
				return null;
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				array[i] = new ParameterInfo((this.pinfo != null) ? this.pinfo[i + 1] : null, this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
		internal override int GetParameterCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003D9D8 File Offset: 0x0003BBD8
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.NotSupported();
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003D9E0 File Offset: 0x0003BBE0
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.NotSupported();
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003D9E8 File Offset: 0x0003BBE8
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.NotSupported();
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0003DA08 File Offset: 0x0003BC08
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.NotSupported();
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0003DA2C File Offset: 0x0003BC2C
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0003DA38 File Offset: 0x0003BC38
		public ILGenerator GetILGenerator(int size)
		{
			if ((this.iattrs & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.iattrs & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), size);
			return this.ilgen;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0003DAB0 File Offset: 0x0003BCB0
		internal void check_override()
		{
			if (this.override_method != null && this.override_method.IsVirtual && !this.IsVirtual)
			{
				throw new TypeLoadException(string.Format("Method '{0}' override '{1}' but it is not virtual", this.name, this.override_method));
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0003DB00 File Offset: 0x0003BD00
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || ILGenerator.Mono_GetCurrentOffset(this.ilgen) == 0) && (this.code == null || this.code.Length == 0))
			{
				throw new InvalidOperationException(string.Format("Method '{0}.{1}' does not have a method body.", this.DeclaringType.FullName, this.Name));
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup();
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0003DB9C File Offset: 0x0003BD9C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"MethodBuilder [",
				this.type.Name,
				"::",
				this.name,
				"]"
			});
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0003DBD8 File Offset: 0x0003BDD8
		[MonoTODO]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0003DBE4 File Offset: 0x0003BDE4
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0003DBF4 File Offset: 0x0003BDF4
		internal override int get_next_table_index(object obj, int table, bool inc)
		{
			return this.type.get_next_table_index(obj, table, inc);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0003DC04 File Offset: 0x0003BE04
		internal void set_override(MethodInfo mdecl)
		{
			this.override_method = mdecl;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0003DC10 File Offset: 0x0003BE10
		private Exception NotSupported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x0600103B RID: 4155
		[MethodImpl(4096)]
		public override extern MethodInfo MakeGenericMethod(params Type[] typeArguments);

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0003DC1C File Offset: 0x0003BE1C
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003DC2C File Offset: 0x0003BE2C
		public override bool IsGenericMethod
		{
			get
			{
				return this.generic_params != null;
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0003DC3C File Offset: 0x0003BE3C
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return Type.EmptyTypes;
			}
			Type[] array = new Type[this.generic_params.Length];
			for (int i = 0; i < this.generic_params.Length; i++)
			{
				array[i] = this.generic_params[i];
			}
			return array;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0003DC90 File Offset: 0x0003BE90
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x040006D6 RID: 1750
		private RuntimeMethodHandle mhandle;

		// Token: 0x040006D7 RID: 1751
		private Type rtype;

		// Token: 0x040006D8 RID: 1752
		internal Type[] parameters;

		// Token: 0x040006D9 RID: 1753
		private MethodAttributes attrs;

		// Token: 0x040006DA RID: 1754
		private MethodImplAttributes iattrs;

		// Token: 0x040006DB RID: 1755
		private string name;

		// Token: 0x040006DC RID: 1756
		private int table_idx;

		// Token: 0x040006DD RID: 1757
		private byte[] code;

		// Token: 0x040006DE RID: 1758
		private ILGenerator ilgen;

		// Token: 0x040006DF RID: 1759
		private TypeBuilder type;

		// Token: 0x040006E0 RID: 1760
		internal ParameterBuilder[] pinfo;

		// Token: 0x040006E1 RID: 1761
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040006E2 RID: 1762
		private MethodInfo override_method;

		// Token: 0x040006E3 RID: 1763
		private string pi_dll;

		// Token: 0x040006E4 RID: 1764
		private string pi_entry;

		// Token: 0x040006E5 RID: 1765
		private CharSet charset;

		// Token: 0x040006E6 RID: 1766
		private uint extra_flags;

		// Token: 0x040006E7 RID: 1767
		private CallingConvention native_cc;

		// Token: 0x040006E8 RID: 1768
		private CallingConventions call_conv;

		// Token: 0x040006E9 RID: 1769
		private bool init_locals = true;

		// Token: 0x040006EA RID: 1770
		private IntPtr generic_container;

		// Token: 0x040006EB RID: 1771
		internal GenericTypeParameterBuilder[] generic_params;

		// Token: 0x040006EC RID: 1772
		private Type[] returnModReq;

		// Token: 0x040006ED RID: 1773
		private Type[] returnModOpt;

		// Token: 0x040006EE RID: 1774
		private Type[][] paramModReq;

		// Token: 0x040006EF RID: 1775
		private Type[][] paramModOpt;

		// Token: 0x040006F0 RID: 1776
		private RefEmitPermissionSet[] permissions;
	}
}
