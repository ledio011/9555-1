using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000198 RID: 408
	internal abstract class DerivedType : Type
	{
		// Token: 0x06000F30 RID: 3888 RVA: 0x0003B980 File Offset: 0x00039B80
		internal DerivedType(Type elementType)
		{
			this.elementType = elementType;
		}

		// Token: 0x06000F31 RID: 3889
		[MethodImpl(4096)]
		internal static extern void create_unmanaged_type(Type type);

		// Token: 0x06000F32 RID: 3890
		internal abstract string FormatName(string elementName);

		// Token: 0x06000F33 RID: 3891 RVA: 0x0003B990 File Offset: 0x00039B90
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003B998 File Offset: 0x00039B98
		public override Type GetElementType()
		{
			return this.elementType;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0003B9A0 File Offset: 0x00039BA0
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0003B9A8 File Offset: 0x00039BA8
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0003B9B0 File Offset: 0x00039BB0
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003B9B8 File Offset: 0x00039BB8
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003B9C0 File Offset: 0x00039BC0
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003B9C8 File Offset: 0x00039BC8
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0003B9D0 File Offset: 0x00039BD0
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003B9D8 File Offset: 0x00039BD8
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003B9E0 File Offset: 0x00039BE0
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003B9E8 File Offset: 0x00039BE8
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.elementType.Attributes;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0003B9F8 File Offset: 0x00039BF8
		protected override bool HasElementTypeImpl()
		{
			return true;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003B9FC File Offset: 0x00039BFC
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0003BA00 File Offset: 0x00039C00
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003BA04 File Offset: 0x00039C04
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003BA08 File Offset: 0x00039C08
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003BA0C File Offset: 0x00039C0C
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003BA14 File Offset: 0x00039C14
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0003BA1C File Offset: 0x00039C1C
		public override bool IsInstanceOfType(object o)
		{
			return false;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003BA20 File Offset: 0x00039C20
		public override bool IsAssignableFrom(Type c)
		{
			return false;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0003BA24 File Offset: 0x00039C24
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.elementType.ContainsGenericParameters;
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003BA34 File Offset: 0x00039C34
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003BA3C File Offset: 0x00039C3C
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003BA48 File Offset: 0x00039C48
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0003BA60 File Offset: 0x00039C60
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003BA68 File Offset: 0x00039C68
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003BA70 File Offset: 0x00039C70
		public override string ToString()
		{
			return this.FormatName(this.elementType.ToString());
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0003BA84 File Offset: 0x00039C84
		public override Assembly Assembly
		{
			get
			{
				return this.elementType.Assembly;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0003BA94 File Offset: 0x00039C94
		public override string AssemblyQualifiedName
		{
			get
			{
				string text = this.FormatName(this.elementType.FullName);
				if (text == null)
				{
					return null;
				}
				return text + ", " + this.elementType.Assembly.FullName;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0003BAD8 File Offset: 0x00039CD8
		public override string FullName
		{
			get
			{
				return this.FormatName(this.elementType.FullName);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003BAEC File Offset: 0x00039CEC
		public override string Name
		{
			get
			{
				return this.FormatName(this.elementType.Name);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0003BB00 File Offset: 0x00039D00
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0003BB08 File Offset: 0x00039D08
		public override Module Module
		{
			get
			{
				return this.elementType.Module;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003BB18 File Offset: 0x00039D18
		public override string Namespace
		{
			get
			{
				return this.elementType.Namespace;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003BB28 File Offset: 0x00039D28
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003BB30 File Offset: 0x00039D30
		public override Type UnderlyingSystemType
		{
			get
			{
				DerivedType.create_unmanaged_type(this);
				return this;
			}
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003BB3C File Offset: 0x00039D3C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003BB44 File Offset: 0x00039D44
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003BB4C File Offset: 0x00039D4C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400066B RID: 1643
		internal Type elementType;
	}
}
