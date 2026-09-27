using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000195 RID: 405
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ConstructorBuilder))]
	[ComVisible(true)]
	public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
	{
		// Token: 0x06000EFD RID: 3837 RVA: 0x0003B344 File Offset: 0x00039544
		internal ConstructorBuilder(TypeBuilder tb, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.attrs = (attributes | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			this.call_conv = callingConvention;
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
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			this.table_idx = this.get_next_table_index(this, 6, true);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0003B414 File Offset: 0x00039614
		[MonoTODO]
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003B41C File Offset: 0x0003961C
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003B424 File Offset: 0x00039624
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003B42C File Offset: 0x0003962C
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created && !this.IsCompilerContext)
			{
				throw this.not_created();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003B458 File Offset: 0x00039658
		internal ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return new ParameterInfo[0];
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				array[i] = new ParameterInfo((this.pinfo != null) ? this.pinfo[i + 1] : null, this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0003B4D0 File Offset: 0x000396D0
		internal override int GetParameterCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0003B4E8 File Offset: 0x000396E8
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0003B4F0 File Offset: 0x000396F0
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0003B4F8 File Offset: 0x000396F8
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0003B500 File Offset: 0x00039700
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0003B508 File Offset: 0x00039708
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0003B510 File Offset: 0x00039710
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0003B518 File Offset: 0x00039718
		public override string Name
		{
			get
			{
				return ((this.attrs & MethodAttributes.Static) == MethodAttributes.PrivateScope) ? ConstructorInfo.ConstructorName : ConstructorInfo.TypeConstructorName;
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0003B538 File Offset: 0x00039738
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003B540 File Offset: 0x00039740
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.type.is_created && this.IsCompilerContext)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.not_supported();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003B56C File Offset: 0x0003976C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.type.is_created && this.IsCompilerContext)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.not_supported();
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0003B598 File Offset: 0x00039798
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003B5A4 File Offset: 0x000397A4
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), streamSize);
			return this.ilgen;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003B5F8 File Offset: 0x000397F8
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0003B60C File Offset: 0x0003980C
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003B614 File Offset: 0x00039814
		public override string ToString()
		{
			return "ConstructorBuilder ['" + this.type.Name + "']";
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003B630 File Offset: 0x00039830
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || ILGenerator.Mono_GetCurrentOffset(this.ilgen) == 0))
			{
				throw new InvalidOperationException("Method '" + this.Name + "' does not have a method body.");
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup();
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003B6AC File Offset: 0x000398AC
		internal override int get_next_table_index(object obj, int table, bool inc)
		{
			return this.type.get_next_table_index(obj, table, inc);
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0003B6BC File Offset: 0x000398BC
		private bool IsCompilerContext
		{
			get
			{
				ModuleBuilder moduleBuilder = (ModuleBuilder)this.TypeBuilder.Module;
				AssemblyBuilder assemblyBuilder = (AssemblyBuilder)moduleBuilder.Assembly;
				return assemblyBuilder.IsCompilerContext;
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003B6EC File Offset: 0x000398EC
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003B6F8 File Offset: 0x000398F8
		private Exception not_created()
		{
			return new NotSupportedException("The type is not yet created.");
		}

		// Token: 0x04000658 RID: 1624
		private RuntimeMethodHandle mhandle;

		// Token: 0x04000659 RID: 1625
		private ILGenerator ilgen;

		// Token: 0x0400065A RID: 1626
		internal Type[] parameters;

		// Token: 0x0400065B RID: 1627
		private MethodAttributes attrs;

		// Token: 0x0400065C RID: 1628
		private MethodImplAttributes iattrs;

		// Token: 0x0400065D RID: 1629
		private int table_idx;

		// Token: 0x0400065E RID: 1630
		private CallingConventions call_conv;

		// Token: 0x0400065F RID: 1631
		private TypeBuilder type;

		// Token: 0x04000660 RID: 1632
		internal ParameterBuilder[] pinfo;

		// Token: 0x04000661 RID: 1633
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000662 RID: 1634
		private bool init_locals = true;

		// Token: 0x04000663 RID: 1635
		private Type[][] paramModReq;

		// Token: 0x04000664 RID: 1636
		private Type[][] paramModOpt;

		// Token: 0x04000665 RID: 1637
		private RefEmitPermissionSet[] permissions;
	}
}
