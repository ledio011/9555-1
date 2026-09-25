using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x020001BB RID: 443
	internal class PropertyOnTypeBuilderInst : PropertyInfo
	{
		// Token: 0x060010A2 RID: 4258 RVA: 0x00040538 File Offset: 0x0003E738
		internal PropertyOnTypeBuilderInst(MonoGenericClass instantiation, PropertyInfo prop)
		{
			this.instantiation = instantiation;
			this.prop = prop;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00040550 File Offset: 0x0003E750
		public override PropertyAttributes Attributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00040558 File Offset: 0x0003E758
		public override bool CanRead
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00040560 File Offset: 0x0003E760
		public override bool CanWrite
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00040568 File Offset: 0x0003E768
		public override Type PropertyType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.PropertyType);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00040580 File Offset: 0x0003E780
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.DeclaringType);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00040598 File Offset: 0x0003E798
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x000405A0 File Offset: 0x0003E7A0
		public override string Name
		{
			get
			{
				return this.prop.Name;
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000405B0 File Offset: 0x0003E7B0
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetGetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000405FC File Offset: 0x0003E7FC
		public override ParameterInfo[] GetIndexParameters()
		{
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod != null)
			{
				return getMethod.GetParameters();
			}
			return new ParameterInfo[0];
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00040624 File Offset: 0x0003E824
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetSetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00040670 File Offset: 0x0003E870
		public override string ToString()
		{
			return string.Format("{0} {1}", this.PropertyType, this.Name);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00040688 File Offset: 0x0003E888
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00040690 File Offset: 0x0003E890
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00040698 File Offset: 0x0003E898
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000406A0 File Offset: 0x0003E8A0
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000406A8 File Offset: 0x0003E8A8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400083F RID: 2111
		private MonoGenericClass instantiation;

		// Token: 0x04000840 RID: 2112
		private PropertyInfo prop;
	}
}
