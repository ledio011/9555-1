using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000196 RID: 406
	internal class ConstructorOnTypeBuilderInst : ConstructorInfo
	{
		// Token: 0x06000F18 RID: 3864 RVA: 0x0003B704 File Offset: 0x00039904
		public ConstructorOnTypeBuilderInst(MonoGenericClass instantiation, ConstructorBuilder cb)
		{
			this.instantiation = instantiation;
			this.cb = cb;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0003B71C File Offset: 0x0003991C
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0003B724 File Offset: 0x00039924
		public override string Name
		{
			get
			{
				return this.cb.Name;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0003B734 File Offset: 0x00039934
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003B73C File Offset: 0x0003993C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.cb.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003B74C File Offset: 0x0003994C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.cb.GetCustomAttributes(inherit);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003B75C File Offset: 0x0003995C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.cb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003B76C File Offset: 0x0003996C
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.cb.GetMethodImplementationFlags();
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003B77C File Offset: 0x0003997C
		public override ParameterInfo[] GetParameters()
		{
			if (!((ModuleBuilder)this.cb.Module).assemblyb.IsCompilerContext && !this.instantiation.generic_type.is_created)
			{
				throw new NotSupportedException();
			}
			ParameterInfo[] array = new ParameterInfo[this.cb.parameters.Length];
			for (int i = 0; i < this.cb.parameters.Length; i++)
			{
				Type type = this.instantiation.InflateType(this.cb.parameters[i]);
				array[i] = new ParameterInfo((this.cb.pinfo != null) ? this.cb.pinfo[i] : null, type, this, i + 1);
			}
			return array;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0003B840 File Offset: 0x00039A40
		internal override int GetParameterCount()
		{
			return this.cb.GetParameterCount();
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003B850 File Offset: 0x00039A50
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.cb.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0003B864 File Offset: 0x00039A64
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.cb.MethodHandle;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0003B874 File Offset: 0x00039A74
		public override MethodAttributes Attributes
		{
			get
			{
				return this.cb.Attributes;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0003B884 File Offset: 0x00039A84
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.cb.CallingConvention;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003B894 File Offset: 0x00039A94
		public override Type[] GetGenericArguments()
		{
			return this.cb.GetGenericArguments();
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0003B8A4 File Offset: 0x00039AA4
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0003B8A8 File Offset: 0x00039AA8
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0003B8AC File Offset: 0x00039AAC
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003B8B0 File Offset: 0x00039AB0
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000666 RID: 1638
		private MonoGenericClass instantiation;

		// Token: 0x04000667 RID: 1639
		private ConstructorBuilder cb;
	}
}
