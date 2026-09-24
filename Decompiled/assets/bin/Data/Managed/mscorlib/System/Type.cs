using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020003CD RID: 973
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Type))]
	[Serializable]
	public abstract class Type : MemberInfo, IReflect, _Type
	{
		// Token: 0x06001D94 RID: 7572 RVA: 0x0006FC68 File Offset: 0x0006DE68
		private static bool FilterName_impl(MemberInfo m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text == null || text.Length == 0)
			{
				return false;
			}
			if (text[text.Length - 1] == '*')
			{
				return string.Compare(text, 0, m.Name, 0, text.Length - 1, false, CultureInfo.InvariantCulture) == 0;
			}
			return text.Equals(m.Name);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0006FCD0 File Offset: 0x0006DED0
		private static bool FilterNameIgnoreCase_impl(MemberInfo m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text == null || text.Length == 0)
			{
				return false;
			}
			if (text[text.Length - 1] == '*')
			{
				return string.Compare(text, 0, m.Name, 0, text.Length - 1, true, CultureInfo.InvariantCulture) == 0;
			}
			return string.Compare(text, m.Name, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0006FD44 File Offset: 0x0006DF44
		private static bool FilterAttribute_impl(MemberInfo m, object filterCriteria)
		{
			int num = ((IConvertible)filterCriteria).ToInt32(null);
			if (m is MethodInfo)
			{
				return (((MethodInfo)m).Attributes & (MethodAttributes)num) != MethodAttributes.PrivateScope;
			}
			if (m is FieldInfo)
			{
				return (((FieldInfo)m).Attributes & (FieldAttributes)num) != FieldAttributes.PrivateScope;
			}
			if (m is PropertyInfo)
			{
				return (((PropertyInfo)m).Attributes & (PropertyAttributes)num) != PropertyAttributes.None;
			}
			return m is EventInfo && (((EventInfo)m).Attributes & (EventAttributes)num) != EventAttributes.None;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001D97 RID: 7575
		public abstract Assembly Assembly { get; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001D98 RID: 7576
		public abstract string AssemblyQualifiedName { get; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0006FDDC File Offset: 0x0006DFDC
		public TypeAttributes Attributes
		{
			get
			{
				return this.GetAttributeFlagsImpl();
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001D9A RID: 7578
		public abstract Type BaseType { get; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0006FDE4 File Offset: 0x0006DFE4
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001D9C RID: 7580
		public abstract string FullName { get; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001D9D RID: 7581
		public abstract Guid GUID { get; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0006FDE8 File Offset: 0x0006DFE8
		public bool HasElementType
		{
			get
			{
				return this.HasElementTypeImpl();
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0006FDF0 File Offset: 0x0006DFF0
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & TypeAttributes.Abstract) != TypeAttributes.NotPublic;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0006FE04 File Offset: 0x0006E004
		public bool IsArray
		{
			get
			{
				return this.IsArrayImpl();
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x0006FE0C File Offset: 0x0006E00C
		public bool IsByRef
		{
			get
			{
				return this.IsByRefImpl();
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0006FE14 File Offset: 0x0006E014
		public bool IsClass
		{
			get
			{
				return !this.IsInterface && !this.IsValueType;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0006FE2C File Offset: 0x0006E02C
		public bool IsContextful
		{
			get
			{
				return this.IsContextfulImpl();
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0006FE34 File Offset: 0x0006E034
		public bool IsEnum
		{
			get
			{
				return this.IsSubclassOf(typeof(Enum));
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0006FE48 File Offset: 0x0006E048
		public bool IsExplicitLayout
		{
			get
			{
				return (this.Attributes & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0006FE58 File Offset: 0x0006E058
		public bool IsImport
		{
			get
			{
				return (this.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x0006FE6C File Offset: 0x0006E06C
		public bool IsInterface
		{
			get
			{
				return (this.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0006FE7C File Offset: 0x0006E07C
		public bool IsMarshalByRef
		{
			get
			{
				return this.IsMarshalByRefImpl();
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0006FE84 File Offset: 0x0006E084
		public bool IsPointer
		{
			get
			{
				return this.IsPointerImpl();
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0006FE8C File Offset: 0x0006E08C
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitiveImpl();
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0006FE94 File Offset: 0x0006E094
		public bool IsSealed
		{
			get
			{
				return (this.Attributes & TypeAttributes.Sealed) != TypeAttributes.NotPublic;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0006FEA8 File Offset: 0x0006E0A8
		public bool IsSerializable
		{
			get
			{
				if ((this.Attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
				{
					return true;
				}
				Type type = this.UnderlyingSystemType;
				if (type == null)
				{
					return false;
				}
				if (type.IsSystemType)
				{
					return Type.type_is_subtype_of(type, typeof(Enum), false) || Type.type_is_subtype_of(type, typeof(Delegate), false);
				}
				while (type != typeof(Enum) && type != typeof(Delegate))
				{
					type = type.BaseType;
					if (type == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0006FF40 File Offset: 0x0006E140
		public bool IsValueType
		{
			get
			{
				return this.IsValueTypeImpl();
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0006FF48 File Offset: 0x0006E148
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001DAF RID: 7599
		public abstract override Module Module { get; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001DB0 RID: 7600
		public abstract string Namespace { get; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x0006FF4C File Offset: 0x0006E14C
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x0006FF50 File Offset: 0x0006E150
		public virtual RuntimeTypeHandle TypeHandle
		{
			get
			{
				return default(RuntimeTypeHandle);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001DB3 RID: 7603
		public abstract Type UnderlyingSystemType { get; }

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0006FF68 File Offset: 0x0006E168
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			Type type = o as Type;
			return type != null && this.Equals(type);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0006FF94 File Offset: 0x0006E194
		public bool Equals(Type o)
		{
			return o != null && this.UnderlyingSystemType.EqualsInternal(o.UnderlyingSystemType);
		}

		// Token: 0x06001DB6 RID: 7606
		[MethodImpl(4096)]
		internal extern bool EqualsInternal(Type type);

		// Token: 0x06001DB7 RID: 7607
		[MethodImpl(4096)]
		private static extern Type internal_from_handle(IntPtr handle);

		// Token: 0x06001DB8 RID: 7608
		[MethodImpl(4096)]
		private static extern Type internal_from_name(string name, bool throwOnError, bool ignoreCase);

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0006FFB0 File Offset: 0x0006E1B0
		public static Type GetType(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("TypeName");
			}
			return Type.internal_from_name(typeName, false, false);
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0006FFCC File Offset: 0x0006E1CC
		public static Type GetType(string typeName, bool throwOnError)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("TypeName");
			}
			Type type = Type.internal_from_name(typeName, throwOnError, false);
			if (throwOnError && type == null)
			{
				throw new TypeLoadException("Error loading '" + typeName + "'");
			}
			return type;
		}

		// Token: 0x06001DBB RID: 7611
		[MethodImpl(4096)]
		internal static extern TypeCode GetTypeCodeInternal(Type type);

		// Token: 0x06001DBC RID: 7612 RVA: 0x00070018 File Offset: 0x0006E218
		public static TypeCode GetTypeCode(Type type)
		{
			if (type is MonoType)
			{
				return Type.GetTypeCodeInternal(type);
			}
			if (type == null)
			{
				return TypeCode.Empty;
			}
			type = type.UnderlyingSystemType;
			if (!type.IsSystemType)
			{
				return TypeCode.Object;
			}
			return Type.GetTypeCodeInternal(type);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00070050 File Offset: 0x0006E250
		public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				return null;
			}
			return Type.internal_from_handle(handle.Value);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00070078 File Offset: 0x0006E278
		public static RuntimeTypeHandle GetTypeHandle(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException();
			}
			return o.GetType().TypeHandle;
		}

		// Token: 0x06001DBF RID: 7615
		[MethodImpl(4096)]
		internal static extern bool type_is_subtype_of(Type a, Type b, bool check_interfaces);

		// Token: 0x06001DC0 RID: 7616
		[MethodImpl(4096)]
		internal static extern bool type_is_assignable_from(Type a, Type b);

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00070094 File Offset: 0x0006E294
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0007009C File Offset: 0x0006E29C
		[ComVisible(true)]
		public virtual bool IsSubclassOf(Type c)
		{
			if (c == null || c == this)
			{
				return false;
			}
			if (this.IsSystemType)
			{
				return c.IsSystemType && Type.type_is_subtype_of(this, c, false);
			}
			for (Type baseType = this.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				if (baseType == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001DC3 RID: 7619
		public abstract Type[] GetInterfaces();

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00070100 File Offset: 0x0006E300
		public virtual bool IsAssignableFrom(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (this.Equals(c))
			{
				return true;
			}
			if (c is TypeBuilder)
			{
				return ((TypeBuilder)c).IsAssignableTo(this);
			}
			if (!this.IsSystemType)
			{
				Type underlyingSystemType = this.UnderlyingSystemType;
				return underlyingSystemType.IsSystemType && underlyingSystemType.IsAssignableFrom(c);
			}
			if (!c.IsSystemType)
			{
				Type underlyingSystemType2 = c.UnderlyingSystemType;
				return underlyingSystemType2.IsSystemType && this.IsAssignableFrom(underlyingSystemType2);
			}
			return Type.type_is_assignable_from(this, c);
		}

		// Token: 0x06001DC5 RID: 7621
		[MethodImpl(4096)]
		public virtual extern bool IsInstanceOfType(object o);

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00070190 File Offset: 0x0006E390
		public virtual int GetArrayRank()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001DC7 RID: 7623
		public abstract Type GetElementType();

		// Token: 0x06001DC8 RID: 7624
		public abstract EventInfo GetEvent(string name, BindingFlags bindingAttr);

		// Token: 0x06001DC9 RID: 7625
		public abstract EventInfo[] GetEvents(BindingFlags bindingAttr);

		// Token: 0x06001DCA RID: 7626 RVA: 0x00070198 File Offset: 0x0006E398
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06001DCB RID: 7627
		public abstract FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06001DCC RID: 7628
		public abstract FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x06001DCD RID: 7629 RVA: 0x000701A4 File Offset: 0x0006E3A4
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != null && underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return (int)this._impl.Value;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000701DC File Offset: 0x0006E3DC
		public MethodInfo GetMethod(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, null, null);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000701FC File Offset: 0x0006E3FC
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, bindingAttr, null, CallingConventions.Any, null, null);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0007021C File Offset: 0x0006E41C
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0007022C File Offset: 0x0006E42C
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0007023C File Offset: 0x0006E43C
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06001DD3 RID: 7635
		protected abstract MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000702A4 File Offset: 0x0006E4A4
		internal MethodInfo GetMethodImplInternal(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x000702B8 File Offset: 0x0006E4B8
		internal virtual MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000702C4 File Offset: 0x0006E4C4
		internal virtual ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000702D0 File Offset: 0x0006E4D0
		internal virtual FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x06001DD8 RID: 7640
		public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06001DD9 RID: 7641
		public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06001DDA RID: 7642 RVA: 0x000702DC File Offset: 0x0006E4DC
		public PropertyInfo GetProperty(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, null, null);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x000702FC File Offset: 0x0006E4FC
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, bindingAttr, null, null, null, null);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0007031C File Offset: 0x0006E51C
		public PropertyInfo GetProperty(string name, Type returnType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, null, null);
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x0007033C File Offset: 0x0006E53C
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, null);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0007034C File Offset: 0x0006E54C
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06001DDF RID: 7647
		protected abstract PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06001DE0 RID: 7648 RVA: 0x000703B8 File Offset: 0x0006E5B8
		internal PropertyInfo GetPropertyImplInternal(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06001DE1 RID: 7649
		protected abstract ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06001DE2 RID: 7650
		protected abstract TypeAttributes GetAttributeFlagsImpl();

		// Token: 0x06001DE3 RID: 7651
		protected abstract bool HasElementTypeImpl();

		// Token: 0x06001DE4 RID: 7652
		protected abstract bool IsArrayImpl();

		// Token: 0x06001DE5 RID: 7653
		protected abstract bool IsByRefImpl();

		// Token: 0x06001DE6 RID: 7654
		protected abstract bool IsPointerImpl();

		// Token: 0x06001DE7 RID: 7655
		protected abstract bool IsPrimitiveImpl();

		// Token: 0x06001DE8 RID: 7656
		[MethodImpl(4096)]
		internal static extern bool IsArrayImpl(Type type);

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000703CC File Offset: 0x0006E5CC
		protected virtual bool IsValueTypeImpl()
		{
			return this != typeof(ValueType) && this != typeof(Enum) && this.IsSubclassOf(typeof(ValueType));
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x00070400 File Offset: 0x0006E600
		protected virtual bool IsContextfulImpl()
		{
			return typeof(ContextBoundObject).IsAssignableFrom(this);
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00070414 File Offset: 0x0006E614
		protected virtual bool IsMarshalByRefImpl()
		{
			return typeof(MarshalByRefObject).IsAssignableFrom(this);
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00070428 File Offset: 0x0006E628
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(Type[] types)
		{
			return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00070438 File Offset: 0x0006E638
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetConstructor(bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00070448 File Offset: 0x0006E648
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetConstructorImpl(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06001DEF RID: 7663
		[ComVisible(true)]
		public abstract ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		// Token: 0x06001DF0 RID: 7664
		public abstract object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000704A0 File Offset: 0x0006E6A0
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x000704A8 File Offset: 0x0006E6A8
		internal bool IsSystemType
		{
			get
			{
				return this._impl.Value != IntPtr.Zero;
			}
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000704C0 File Offset: 0x0006E6C0
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x000704C8 File Offset: 0x0006E6C8
		public virtual bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001DF5 RID: 7669
		public virtual extern bool IsGenericTypeDefinition { [MethodImpl(4096)] get; }

		// Token: 0x06001DF6 RID: 7670
		[MethodImpl(4096)]
		internal extern Type GetGenericTypeDefinition_impl();

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000704CC File Offset: 0x0006E6CC
		public virtual Type GetGenericTypeDefinition()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001DF8 RID: 7672
		public virtual extern bool IsGenericType { [MethodImpl(4096)] get; }

		// Token: 0x06001DF9 RID: 7673
		[MethodImpl(4096)]
		private static extern Type MakeGenericType(Type gt, Type[] types);

		// Token: 0x06001DFA RID: 7674 RVA: 0x000704D8 File Offset: 0x0006E6D8
		public virtual Type MakeGenericType(params Type[] typeArguments)
		{
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException("not a generic type definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.GetGenericArguments().Length != typeArguments.Length)
			{
				throw new ArgumentException(string.Format("The type or method has {0} generic parameter(s) but {1} generic argument(s) where provided. A generic argument must be provided for each generic parameter.", this.GetGenericArguments().Length, typeArguments.Length), "typeArguments");
			}
			Type[] array = new Type[typeArguments.Length];
			for (int i = 0; i < typeArguments.Length; i++)
			{
				Type type = typeArguments[i];
				if (type == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
				if (!(type is EnumBuilder) && !(type is TypeBuilder))
				{
					type = type.UnderlyingSystemType;
				}
				if (type == null || !type.IsSystemType)
				{
					throw new ArgumentNullException("typeArguments");
				}
				array[i] = type;
			}
			Type type2 = Type.MakeGenericType(this, array);
			if (type2 == null)
			{
				throw new TypeLoadException();
			}
			return type2;
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000705CC File Offset: 0x0006E7CC
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x000705D0 File Offset: 0x0006E7D0
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x06001DFD RID: 7677
		[MethodImpl(4096)]
		private extern int GetGenericParameterPosition();

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x000705E0 File Offset: 0x0006E7E0
		public virtual int GenericParameterPosition
		{
			get
			{
				int genericParameterPosition = this.GetGenericParameterPosition();
				if (genericParameterPosition < 0)
				{
					throw new InvalidOperationException();
				}
				return genericParameterPosition;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x00070604 File Offset: 0x0006E804
		public virtual MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001E00 RID: 7680
		[MethodImpl(4096)]
		private extern Type make_array_type(int rank);

		// Token: 0x06001E01 RID: 7681 RVA: 0x00070608 File Offset: 0x0006E808
		public virtual Type MakeArrayType()
		{
			return this.make_array_type(0);
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00070614 File Offset: 0x0006E814
		public virtual Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.make_array_type(rank);
		}

		// Token: 0x06001E03 RID: 7683
		[MethodImpl(4096)]
		private extern Type make_byref_type();

		// Token: 0x06001E04 RID: 7684 RVA: 0x0007062C File Offset: 0x0006E82C
		public virtual Type MakeByRefType()
		{
			return this.make_byref_type();
		}

		// Token: 0x06001E05 RID: 7685
		[MethodImpl(4096)]
		public virtual extern Type MakePointerType();

		// Token: 0x06001E06 RID: 7686 RVA: 0x00070634 File Offset: 0x0006E834
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if ((this.Attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
			{
				num++;
			}
			if ((this.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((this.Attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
			{
				array[num++] = new SerializableAttribute();
			}
			if ((this.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic)
			{
				array[num++] = new ComImportAttribute();
			}
			return array;
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x000706BC File Offset: 0x0006E8BC
		internal bool IsUserType
		{
			get
			{
				return this._impl.Value == IntPtr.Zero && (this.GetType().Assembly != typeof(Type).Assembly || this.GetType() == typeof(TypeDelegator));
			}
		}

		// Token: 0x04000F83 RID: 3971
		internal const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x04000F84 RID: 3972
		internal RuntimeTypeHandle _impl;

		// Token: 0x04000F85 RID: 3973
		public static readonly char Delimiter = '.';

		// Token: 0x04000F86 RID: 3974
		public static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x04000F87 RID: 3975
		public static readonly MemberFilter FilterAttribute = new MemberFilter(Type.FilterAttribute_impl);

		// Token: 0x04000F88 RID: 3976
		public static readonly MemberFilter FilterName = new MemberFilter(Type.FilterName_impl);

		// Token: 0x04000F89 RID: 3977
		public static readonly MemberFilter FilterNameIgnoreCase = new MemberFilter(Type.FilterNameIgnoreCase_impl);

		// Token: 0x04000F8A RID: 3978
		public static readonly object Missing = System.Reflection.Missing.Value;
	}
}
