using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001BA RID: 442
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_PropertyBuilder))]
	[ComVisible(true)]
	public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00040498 File Offset: 0x0003E698
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x000404A0 File Offset: 0x0003E6A0
		public override bool CanRead
		{
			get
			{
				return this.get_method != null;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x000404B0 File Offset: 0x0003E6B0
		public override bool CanWrite
		{
			get
			{
				return this.set_method != null;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x000404C0 File Offset: 0x0003E6C0
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x000404C8 File Offset: 0x0003E6C8
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x000404D0 File Offset: 0x0003E6D0
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x000404D8 File Offset: 0x0003E6D8
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000404E0 File Offset: 0x0003E6E0
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000404E8 File Offset: 0x0003E6E8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000404F0 File Offset: 0x0003E6F0
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return this.get_method;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000404F8 File Offset: 0x0003E6F8
		public override ParameterInfo[] GetIndexParameters()
		{
			throw this.not_supported();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00040500 File Offset: 0x0003E700
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			return this.set_method;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00040508 File Offset: 0x0003E708
		public override object GetValue(object obj, object[] index)
		{
			return null;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0004050C File Offset: 0x0003E70C
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw this.not_supported();
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00040514 File Offset: 0x0003E714
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0004051C File Offset: 0x0003E71C
		public override void SetValue(object obj, object value, object[] index)
		{
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00040520 File Offset: 0x0003E720
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x00040524 File Offset: 0x0003E724
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0004052C File Offset: 0x0003E72C
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x04000831 RID: 2097
		private PropertyAttributes attrs;

		// Token: 0x04000832 RID: 2098
		private string name;

		// Token: 0x04000833 RID: 2099
		private Type type;

		// Token: 0x04000834 RID: 2100
		private Type[] parameters;

		// Token: 0x04000835 RID: 2101
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000836 RID: 2102
		private object def_value;

		// Token: 0x04000837 RID: 2103
		private MethodBuilder set_method;

		// Token: 0x04000838 RID: 2104
		private MethodBuilder get_method;

		// Token: 0x04000839 RID: 2105
		private int table_idx;

		// Token: 0x0400083A RID: 2106
		internal TypeBuilder typeb;

		// Token: 0x0400083B RID: 2107
		private Type[] returnModReq;

		// Token: 0x0400083C RID: 2108
		private Type[] returnModOpt;

		// Token: 0x0400083D RID: 2109
		private Type[][] paramModReq;

		// Token: 0x0400083E RID: 2110
		private Type[][] paramModOpt;
	}
}
