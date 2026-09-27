using System;
using System.Collections;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020001DD RID: 477
	internal class MonoGenericClass : MonoType
	{
		// Token: 0x060011B8 RID: 4536
		[MethodImpl(4096)]
		private extern void initialize(MethodInfo[] methods, ConstructorInfo[] ctors, FieldInfo[] fields, PropertyInfo[] properties, EventInfo[] events);

		// Token: 0x060011B9 RID: 4537 RVA: 0x00042FDC File Offset: 0x000411DC
		private void initialize()
		{
			if (this.initialized)
			{
				return;
			}
			MonoGenericClass monoGenericClass = this.GetParentType() as MonoGenericClass;
			if (monoGenericClass != null)
			{
				monoGenericClass.initialize();
			}
			EventInfo[] events_internal = this.generic_type.GetEvents_internal(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			this.event_count = events_internal.Length;
			this.initialize(this.generic_type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), this.generic_type.GetConstructorsInternal(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), this.generic_type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), this.generic_type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), events_internal);
			this.initialized = true;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00043068 File Offset: 0x00041268
		private Type GetParentType()
		{
			return this.InflateType(this.generic_type.BaseType);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004307C File Offset: 0x0004127C
		internal Type InflateType(Type type)
		{
			return this.InflateType(type, null);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00043088 File Offset: 0x00041288
		internal Type InflateType(Type type, Type[] method_args)
		{
			if (type == null)
			{
				return null;
			}
			if (!type.IsGenericParameter && !type.ContainsGenericParameters)
			{
				return type;
			}
			if (type.IsGenericParameter)
			{
				if (type.DeclaringMethod == null)
				{
					return this.type_arguments[type.GenericParameterPosition];
				}
				if (method_args != null)
				{
					return method_args[type.GenericParameterPosition];
				}
				return type;
			}
			else
			{
				if (type.IsPointer)
				{
					return this.InflateType(type.GetElementType(), method_args).MakePointerType();
				}
				if (type.IsByRef)
				{
					return this.InflateType(type.GetElementType(), method_args).MakeByRefType();
				}
				if (!type.IsArray)
				{
					Type[] genericArguments = type.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						genericArguments[i] = this.InflateType(genericArguments[i], method_args);
					}
					Type type2 = (!type.IsGenericTypeDefinition) ? type.GetGenericTypeDefinition() : type;
					return type2.MakeGenericType(genericArguments);
				}
				if (type.GetArrayRank() > 1)
				{
					return this.InflateType(type.GetElementType(), method_args).MakeArrayType(type.GetArrayRank());
				}
				if (type.ToString().EndsWith("[*]", StringComparison.Ordinal))
				{
					return this.InflateType(type.GetElementType(), method_args).MakeArrayType(1);
				}
				return this.InflateType(type.GetElementType(), method_args).MakeArrayType();
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x000431DC File Offset: 0x000413DC
		public override Type BaseType
		{
			get
			{
				Type parentType = this.GetParentType();
				return (parentType == null) ? this.generic_type.BaseType : parentType;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00043208 File Offset: 0x00041408
		private Type[] GetInterfacesInternal()
		{
			if (this.generic_type.interfaces == null)
			{
				return new Type[0];
			}
			Type[] array = new Type[this.generic_type.interfaces.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.InflateType(this.generic_type.interfaces[i]);
			}
			return array;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004326C File Offset: 0x0004146C
		public override Type[] GetInterfaces()
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			return this.GetInterfacesInternal();
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004328C File Offset: 0x0004148C
		protected override bool IsValueTypeImpl()
		{
			return this.generic_type.IsValueType;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004329C File Offset: 0x0004149C
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			this.initialize();
			if (!(fromNoninstanciated is MethodBuilder))
			{
				throw new InvalidOperationException("Inflating non MethodBuilder objects is not supported: " + fromNoninstanciated.GetType());
			}
			MethodBuilder methodBuilder = (MethodBuilder)fromNoninstanciated;
			if (this.methods == null)
			{
				this.methods = new Hashtable();
			}
			if (!this.methods.ContainsKey(methodBuilder))
			{
				this.methods[methodBuilder] = new MethodOnTypeBuilderInst(this, methodBuilder);
			}
			return (MethodInfo)this.methods[methodBuilder];
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00043324 File Offset: 0x00041524
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			this.initialize();
			if (!(fromNoninstanciated is ConstructorBuilder))
			{
				throw new InvalidOperationException("Inflating non ConstructorBuilder objects is not supported: " + fromNoninstanciated.GetType());
			}
			ConstructorBuilder constructorBuilder = (ConstructorBuilder)fromNoninstanciated;
			if (this.ctors == null)
			{
				this.ctors = new Hashtable();
			}
			if (!this.ctors.ContainsKey(constructorBuilder))
			{
				this.ctors[constructorBuilder] = new ConstructorOnTypeBuilderInst(this, constructorBuilder);
			}
			return (ConstructorInfo)this.ctors[constructorBuilder];
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000433AC File Offset: 0x000415AC
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			this.initialize();
			if (!(fromNoninstanciated is FieldBuilder))
			{
				throw new InvalidOperationException("Inflating non FieldBuilder objects is not supported: " + fromNoninstanciated.GetType());
			}
			FieldBuilder fieldBuilder = (FieldBuilder)fromNoninstanciated;
			if (this.fields == null)
			{
				this.fields = new Hashtable();
			}
			if (!this.fields.ContainsKey(fieldBuilder))
			{
				this.fields[fieldBuilder] = new FieldOnTypeBuilderInst(this, fieldBuilder);
			}
			return (FieldInfo)this.fields[fieldBuilder];
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00043434 File Offset: 0x00041634
		public override MethodInfo[] GetMethods(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetMethodsInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetMethods(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_91;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetMethodsByName(null, bf, false, this));
			Block_4:
			IL_91:
			MethodInfo[] array = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x000434EC File Offset: 0x000416EC
		private MethodInfo[] GetMethodsInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.num_methods == 0)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			this.initialize();
			for (int i = 0; i < this.generic_type.num_methods; i++)
			{
				MethodInfo methodInfo = this.generic_type.methods[i];
				bool flag = false;
				MethodAttributes attributes = methodInfo.Attributes;
				if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
				{
					if ((bf & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
					{
						if ((bf & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						methodInfo = TypeBuilder.GetMethod(this, methodInfo);
						arrayList.Add(methodInfo);
					}
				}
			}
			MethodInfo[] array = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000435E0 File Offset: 0x000417E0
		public override ConstructorInfo[] GetConstructors(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetConstructorsInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetConstructors(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_8F;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetConstructors_internal(bf, this));
			Block_4:
			IL_8F:
			ConstructorInfo[] array = new ConstructorInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00043694 File Offset: 0x00041894
		private ConstructorInfo[] GetConstructorsInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.ctors == null)
			{
				return new ConstructorInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			this.initialize();
			for (int i = 0; i < this.generic_type.ctors.Length; i++)
			{
				ConstructorInfo constructorInfo = this.generic_type.ctors[i];
				bool flag = false;
				MethodAttributes attributes = constructorInfo.Attributes;
				if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
				{
					if ((bf & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
					{
						if ((bf & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						arrayList.Add(TypeBuilder.GetConstructor(this, constructorInfo));
					}
				}
			}
			ConstructorInfo[] array = new ConstructorInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00043780 File Offset: 0x00041980
		public override FieldInfo[] GetFields(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetFieldsInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetFields(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_8F;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetFields_internal(bf, this));
			Block_4:
			IL_8F:
			FieldInfo[] array = new FieldInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00043834 File Offset: 0x00041A34
		private FieldInfo[] GetFieldsInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.num_fields == 0)
			{
				return new FieldInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			this.initialize();
			for (int i = 0; i < this.generic_type.num_fields; i++)
			{
				FieldInfo fieldInfo = this.generic_type.fields[i];
				bool flag = false;
				FieldAttributes attributes = fieldInfo.Attributes;
				if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
				{
					if ((bf & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
					{
						if ((bf & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						arrayList.Add(TypeBuilder.GetField(this, fieldInfo));
					}
				}
			}
			FieldInfo[] array = new FieldInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00043920 File Offset: 0x00041B20
		public override PropertyInfo[] GetProperties(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetPropertiesInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetProperties(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_91;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetPropertiesByName(null, bf, false, this));
			Block_4:
			IL_91:
			PropertyInfo[] array = new PropertyInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000439D8 File Offset: 0x00041BD8
		private PropertyInfo[] GetPropertiesInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.properties == null)
			{
				return new PropertyInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			this.initialize();
			foreach (PropertyBuilder propertyInfo in this.generic_type.properties)
			{
				bool flag = false;
				MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
				if (methodInfo == null)
				{
					methodInfo = propertyInfo.GetSetMethod(true);
				}
				if (methodInfo != null)
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bf & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bf & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(new PropertyOnTypeBuilderInst(reftype, propertyInfo));
						}
					}
				}
			}
			PropertyInfo[] array = new PropertyInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00043AE8 File Offset: 0x00041CE8
		public override EventInfo[] GetEvents(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetEventsInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetEvents(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_8E;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetEvents(bf));
			Block_4:
			IL_8E:
			EventInfo[] array = new EventInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00043B9C File Offset: 0x00041D9C
		private EventInfo[] GetEventsInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.events == null)
			{
				return new EventInfo[0];
			}
			this.initialize();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.event_count; i++)
			{
				EventBuilder eventBuilder = this.generic_type.events[i];
				bool flag = false;
				MethodInfo methodInfo = eventBuilder.add_method;
				if (methodInfo == null)
				{
					methodInfo = eventBuilder.remove_method;
				}
				if (methodInfo != null)
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bf & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bf & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(new EventOnTypeBuilderInst(this, eventBuilder));
						}
					}
				}
			}
			EventInfo[] array = new EventInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00043CA8 File Offset: 0x00041EA8
		public override bool IsAssignableFrom(Type c)
		{
			if (c == this)
			{
				return true;
			}
			Type[] interfacesInternal = this.GetInterfacesInternal();
			if (c.IsInterface)
			{
				if (interfacesInternal == null)
				{
					return false;
				}
				foreach (Type c2 in interfacesInternal)
				{
					if (c.IsAssignableFrom(c2))
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				Type parentType = this.GetParentType();
				if (parentType == null)
				{
					return c == typeof(object);
				}
				return c.IsAssignableFrom(parentType);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00043D28 File Offset: 0x00041F28
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00043D2C File Offset: 0x00041F2C
		public override string Name
		{
			get
			{
				return this.generic_type.Name;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00043D3C File Offset: 0x00041F3C
		public override string Namespace
		{
			get
			{
				return this.generic_type.Namespace;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00043D4C File Offset: 0x00041F4C
		public override string FullName
		{
			get
			{
				return this.format_name(true, false);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00043D58 File Offset: 0x00041F58
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.format_name(true, true);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00043D64 File Offset: 0x00041F64
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00043D6C File Offset: 0x00041F6C
		private string format_name(bool full_name, bool assembly_qualified)
		{
			StringBuilder stringBuilder = new StringBuilder(this.generic_type.FullName);
			bool isCompilerContext = this.generic_type.IsCompilerContext;
			stringBuilder.Append("[");
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				string text = (!full_name) ? this.type_arguments[i].ToString() : this.type_arguments[i].AssemblyQualifiedName;
				if (text == null)
				{
					if (!isCompilerContext || !this.type_arguments[i].IsGenericParameter)
					{
						return null;
					}
					text = this.type_arguments[i].Name;
				}
				if (full_name)
				{
					stringBuilder.Append("[");
				}
				stringBuilder.Append(text);
				if (full_name)
				{
					stringBuilder.Append("]");
				}
			}
			stringBuilder.Append("]");
			if (assembly_qualified)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.generic_type.Assembly.FullName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00043E90 File Offset: 0x00042090
		public override string ToString()
		{
			return this.format_name(false, false);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00043E9C File Offset: 0x0004209C
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00043EA8 File Offset: 0x000420A8
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00043EC0 File Offset: 0x000420C0
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00043EC8 File Offset: 0x000420C8
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00043ED0 File Offset: 0x000420D0
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00043ED4 File Offset: 0x000420D4
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.generic_type.Attributes;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00043EE4 File Offset: 0x000420E4
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			foreach (EventInfo eventInfo in this.GetEvents(bindingAttr))
			{
				if (eventInfo.Name == name)
				{
					return eventInfo;
				}
			}
			return null;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00043F3C File Offset: 0x0004213C
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00043F44 File Offset: 0x00042144
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00043F4C File Offset: 0x0004214C
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00043F54 File Offset: 0x00042154
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00043F5C File Offset: 0x0004215C
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00043F64 File Offset: 0x00042164
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00043F6C File Offset: 0x0004216C
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00043F74 File Offset: 0x00042174
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000911 RID: 2321
		private const BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000912 RID: 2322
		internal TypeBuilder generic_type;

		// Token: 0x04000913 RID: 2323
		private Type[] type_arguments;

		// Token: 0x04000914 RID: 2324
		private bool initialized;

		// Token: 0x04000915 RID: 2325
		private Hashtable fields;

		// Token: 0x04000916 RID: 2326
		private Hashtable ctors;

		// Token: 0x04000917 RID: 2327
		private Hashtable methods;

		// Token: 0x04000918 RID: 2328
		private int event_count;
	}
}
