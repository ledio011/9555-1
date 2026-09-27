using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200019C RID: 412
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_EnumBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class EnumBuilder : Type, _EnumBuilder
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0003C154 File Offset: 0x0003A354
		public override Assembly Assembly
		{
			get
			{
				return this._tb.Assembly;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0003C164 File Offset: 0x0003A364
		public override string AssemblyQualifiedName
		{
			get
			{
				return this._tb.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003C174 File Offset: 0x0003A374
		public override Type BaseType
		{
			get
			{
				return this._tb.BaseType;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0003C184 File Offset: 0x0003A384
		public override Type DeclaringType
		{
			get
			{
				return this._tb.DeclaringType;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0003C194 File Offset: 0x0003A394
		public override string FullName
		{
			get
			{
				return this._tb.FullName;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0003C1A4 File Offset: 0x0003A3A4
		public override Guid GUID
		{
			get
			{
				return this._tb.GUID;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0003C1B4 File Offset: 0x0003A3B4
		public override Module Module
		{
			get
			{
				return this._tb.Module;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003C1C4 File Offset: 0x0003A3C4
		public override string Name
		{
			get
			{
				return this._tb.Name;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0003C1D4 File Offset: 0x0003A3D4
		public override string Namespace
		{
			get
			{
				return this._tb.Namespace;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0003C1E4 File Offset: 0x0003A3E4
		public override Type ReflectedType
		{
			get
			{
				return this._tb.ReflectedType;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0003C1F4 File Offset: 0x0003A3F4
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this._tb.TypeHandle;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003C204 File Offset: 0x0003A404
		public override Type UnderlyingSystemType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0003C20C File Offset: 0x0003A40C
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this._tb.attrs;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003C21C File Offset: 0x0003A41C
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this._tb.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0003C230 File Offset: 0x0003A430
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this._tb.GetConstructors(bindingAttr);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003C240 File Offset: 0x0003A440
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._tb.GetCustomAttributes(inherit);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003C250 File Offset: 0x0003A450
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._tb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003C260 File Offset: 0x0003A460
		public override Type GetElementType()
		{
			return this._tb.GetElementType();
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003C270 File Offset: 0x0003A470
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetEvent(name, bindingAttr);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003C280 File Offset: 0x0003A480
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this._tb.GetEvents(bindingAttr);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003C290 File Offset: 0x0003A490
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetField(name, bindingAttr);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this._tb.GetFields(bindingAttr);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003C2B0 File Offset: 0x0003A4B0
		public override Type[] GetInterfaces()
		{
			return this._tb.GetInterfaces();
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003C2C0 File Offset: 0x0003A4C0
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this._tb.GetMethod(name, bindingAttr);
			}
			return this._tb.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003C2EC File Offset: 0x0003A4EC
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this._tb.GetMethods(bindingAttr);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003C2FC File Offset: 0x0003A4FC
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this._tb.GetProperties(bindingAttr);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0003C30C File Offset: 0x0003A50C
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003C314 File Offset: 0x0003A514
		protected override bool HasElementTypeImpl()
		{
			return this._tb.HasElementType;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003C324 File Offset: 0x0003A524
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this._tb.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003C34C File Offset: 0x0003A54C
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003C350 File Offset: 0x0003A550
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003C354 File Offset: 0x0003A554
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0003C358 File Offset: 0x0003A558
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0003C35C File Offset: 0x0003A55C
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0003C360 File Offset: 0x0003A560
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._tb.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0003C370 File Offset: 0x0003A570
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003C37C File Offset: 0x0003A57C
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003C394 File Offset: 0x0003A594
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003C39C File Offset: 0x0003A59C
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003C3A4 File Offset: 0x0003A5A4
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x04000680 RID: 1664
		private TypeBuilder _tb;

		// Token: 0x04000681 RID: 1665
		private FieldBuilder _underlyingField;

		// Token: 0x04000682 RID: 1666
		private Type _underlyingType;
	}
}
