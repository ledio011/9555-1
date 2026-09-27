using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x020001A0 RID: 416
	internal class FieldOnTypeBuilderInst : FieldInfo
	{
		// Token: 0x06000FBD RID: 4029 RVA: 0x0003C55C File Offset: 0x0003A75C
		public FieldOnTypeBuilderInst(MonoGenericClass instantiation, FieldBuilder fb)
		{
			this.instantiation = instantiation;
			this.fb = fb;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0003C574 File Offset: 0x0003A774
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0003C57C File Offset: 0x0003A77C
		public override string Name
		{
			get
			{
				return this.fb.Name;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0003C58C File Offset: 0x0003A78C
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003C594 File Offset: 0x0003A794
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003C59C File Offset: 0x0003A79C
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003C5A4 File Offset: 0x0003A7A4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003C5AC File Offset: 0x0003A7AC
		public override string ToString()
		{
			if (!((ModuleBuilder)this.instantiation.generic_type.Module).assemblyb.IsCompilerContext)
			{
				return this.fb.FieldType.ToString() + " " + this.Name;
			}
			return this.FieldType.ToString() + " " + this.Name;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0003C61C File Offset: 0x0003A81C
		public override FieldAttributes Attributes
		{
			get
			{
				return this.fb.Attributes;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0003C62C File Offset: 0x0003A82C
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003C634 File Offset: 0x0003A834
		public override Type FieldType
		{
			get
			{
				if (!((ModuleBuilder)this.instantiation.generic_type.Module).assemblyb.IsCompilerContext)
				{
					throw new NotSupportedException();
				}
				return this.instantiation.InflateType(this.fb.FieldType);
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003C684 File Offset: 0x0003A884
		public override object GetValue(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003C68C File Offset: 0x0003A88C
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400069C RID: 1692
		internal MonoGenericClass instantiation;

		// Token: 0x0400069D RID: 1693
		internal FieldBuilder fb;
	}
}
