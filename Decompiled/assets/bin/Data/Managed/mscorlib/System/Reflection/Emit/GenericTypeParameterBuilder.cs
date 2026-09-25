using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001A1 RID: 417
	[ComVisible(true)]
	public sealed class GenericTypeParameterBuilder : Type
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x0003C694 File Offset: 0x0003A894
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			if (!((ModuleBuilder)this.tbuilder.Module).assemblyb.IsCompilerContext)
			{
				throw this.not_supported();
			}
			return this.BaseType != null && (this.BaseType == c || this.BaseType.IsSubclassOf(c));
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003C6F0 File Offset: 0x0003A8F0
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			if (((ModuleBuilder)this.tbuilder.Module).assemblyb.IsCompilerContext)
			{
				return TypeAttributes.Public;
			}
			throw this.not_supported();
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0003C71C File Offset: 0x0003A91C
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0003C724 File Offset: 0x0003A924
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003C72C File Offset: 0x0003A92C
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003C734 File Offset: 0x0003A934
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003C73C File Offset: 0x0003A93C
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003C744 File Offset: 0x0003A944
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003C74C File Offset: 0x0003A94C
		public override Type[] GetInterfaces()
		{
			throw this.not_supported();
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003C754 File Offset: 0x0003A954
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003C75C File Offset: 0x0003A95C
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003C764 File Offset: 0x0003A964
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0003C76C File Offset: 0x0003A96C
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0003C774 File Offset: 0x0003A974
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003C778 File Offset: 0x0003A978
		public override bool IsAssignableFrom(Type c)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003C780 File Offset: 0x0003A980
		public override bool IsInstanceOfType(object o)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0003C788 File Offset: 0x0003A988
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003C78C File Offset: 0x0003A98C
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003C790 File Offset: 0x0003A990
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0003C794 File Offset: 0x0003A994
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0003C798 File Offset: 0x0003A998
		protected override bool IsValueTypeImpl()
		{
			return this.base_type != null && this.base_type.IsValueType;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0003C7B8 File Offset: 0x0003A9B8
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0003C7C0 File Offset: 0x0003A9C0
		public override Type GetElementType()
		{
			throw this.not_supported();
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0003C7C8 File Offset: 0x0003A9C8
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0003C7CC File Offset: 0x0003A9CC
		public override Assembly Assembly
		{
			get
			{
				return this.tbuilder.Assembly;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0003C7DC File Offset: 0x0003A9DC
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0003C7E0 File Offset: 0x0003A9E0
		public override Type BaseType
		{
			get
			{
				return this.base_type;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0003C7E8 File Offset: 0x0003A9E8
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0003C7EC File Offset: 0x0003A9EC
		public override Guid GUID
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003C7F4 File Offset: 0x0003A9F4
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003C7FC File Offset: 0x0003A9FC
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003C804 File Offset: 0x0003AA04
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0003C80C File Offset: 0x0003AA0C
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0003C814 File Offset: 0x0003AA14
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0003C818 File Offset: 0x0003AA18
		public override Module Module
		{
			get
			{
				return this.tbuilder.Module;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0003C828 File Offset: 0x0003AA28
		public override Type DeclaringType
		{
			get
			{
				return (this.mbuilder == null) ? this.tbuilder : this.mbuilder.DeclaringType;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0003C84C File Offset: 0x0003AA4C
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0003C854 File Offset: 0x0003AA54
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003C85C File Offset: 0x0003AA5C
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003C864 File Offset: 0x0003AA64
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0003C86C File Offset: 0x0003AA6C
		public override bool ContainsGenericParameters
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0003C870 File Offset: 0x0003AA70
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0003C874 File Offset: 0x0003AA74
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0003C878 File Offset: 0x0003AA78
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0003C87C File Offset: 0x0003AA7C
		public override int GenericParameterPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0003C884 File Offset: 0x0003AA84
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.mbuilder;
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003C88C File Offset: 0x0003AA8C
		private Exception not_supported()
		{
			return new NotSupportedException();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003C894 File Offset: 0x0003AA94
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003C89C File Offset: 0x0003AA9C
		[MonoTODO]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0003C8A8 File Offset: 0x0003AAA8
		[MonoTODO]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003C8B0 File Offset: 0x0003AAB0
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0003C8BC File Offset: 0x0003AABC
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0003C8D4 File Offset: 0x0003AAD4
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0003C8DC File Offset: 0x0003AADC
		[MonoTODO]
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			return base.MakeGenericType(typeArguments);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0003C8E8 File Offset: 0x0003AAE8
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x0400069E RID: 1694
		private TypeBuilder tbuilder;

		// Token: 0x0400069F RID: 1695
		private MethodBuilder mbuilder;

		// Token: 0x040006A0 RID: 1696
		private string name;

		// Token: 0x040006A1 RID: 1697
		private int index;

		// Token: 0x040006A2 RID: 1698
		private Type base_type;

		// Token: 0x040006A3 RID: 1699
		private Type[] iface_constraints;

		// Token: 0x040006A4 RID: 1700
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040006A5 RID: 1701
		private GenericParameterAttributes attrs;
	}
}
