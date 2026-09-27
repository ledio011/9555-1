using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001C1 RID: 449
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_TypeBuilder))]
	public sealed class TypeBuilder : Type, _TypeBuilder
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x000406B0 File Offset: 0x0003E8B0
		internal TypeBuilder(ModuleBuilder mb, TypeAttributes attr, int table_idx)
		{
			this.parent = null;
			this.attrs = attr;
			this.class_size = 0;
			this.table_idx = table_idx;
			this.fullname = (this.tname = ((table_idx != 1) ? ("type_" + table_idx) : "<Module>"));
			this.nspace = string.Empty;
			this.pmodule = mb;
			this.setup_internal_class(this);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00040728 File Offset: 0x0003E928
		internal TypeBuilder(ModuleBuilder mb, string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packing_size, int type_size, Type nesting_type)
		{
			this.parent = parent;
			this.attrs = attr;
			this.class_size = type_size;
			this.packing_size = packing_size;
			this.nesting_type = nesting_type;
			this.check_name("fullname", name);
			if (parent == null && (attr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
			{
				throw new InvalidOperationException("Interface must be declared abstract.");
			}
			int num = name.LastIndexOf('.');
			if (num != -1)
			{
				this.tname = name.Substring(num + 1);
				this.nspace = name.Substring(0, num);
			}
			else
			{
				this.tname = name;
				this.nspace = string.Empty;
			}
			if (interfaces != null)
			{
				this.interfaces = new Type[interfaces.Length];
				Array.Copy(interfaces, this.interfaces, interfaces.Length);
			}
			this.pmodule = mb;
			if ((attr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && parent == null && !this.IsCompilerContext)
			{
				this.parent = typeof(object);
			}
			this.table_idx = mb.get_next_table_index(this, 2, true);
			this.setup_internal_class(this);
			this.fullname = this.GetFullName();
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00040854 File Offset: 0x0003EA54
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.attrs;
		}

		// Token: 0x060010B9 RID: 4281
		[MethodImpl(4096)]
		private extern void setup_internal_class(TypeBuilder tb);

		// Token: 0x060010BA RID: 4282
		[MethodImpl(4096)]
		private extern void create_generic_class();

		// Token: 0x060010BB RID: 4283
		[MethodImpl(4096)]
		private extern EventInfo get_event_info(EventBuilder eb);

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0004085C File Offset: 0x0003EA5C
		public override Assembly Assembly
		{
			get
			{
				return this.pmodule.Assembly;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0004086C File Offset: 0x0003EA6C
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.fullname + ", " + this.Assembly.FullName;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0004088C File Offset: 0x0003EA8C
		public override Type BaseType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00040894 File Offset: 0x0003EA94
		public override Type DeclaringType
		{
			get
			{
				return this.nesting_type;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0004089C File Offset: 0x0003EA9C
		public override Type UnderlyingSystemType
		{
			get
			{
				if (this.is_created)
				{
					return this.created.UnderlyingSystemType;
				}
				if (!this.IsEnum || this.IsCompilerContext)
				{
					return this;
				}
				if (this.underlying_type != null)
				{
					return this.underlying_type;
				}
				throw new InvalidOperationException("Enumeration type is not defined.");
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000408F4 File Offset: 0x0003EAF4
		private string GetFullName()
		{
			if (this.nesting_type != null)
			{
				return this.nesting_type.FullName + "+" + this.tname;
			}
			if (this.nspace != null && this.nspace.Length > 0)
			{
				return this.nspace + "." + this.tname;
			}
			return this.tname;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00040964 File Offset: 0x0003EB64
		public override string FullName
		{
			get
			{
				return this.fullname;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0004096C File Offset: 0x0003EB6C
		public override Guid GUID
		{
			get
			{
				this.check_created();
				return this.created.GUID;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x00040980 File Offset: 0x0003EB80
		public override Module Module
		{
			get
			{
				return this.pmodule;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00040988 File Offset: 0x0003EB88
		public override string Name
		{
			get
			{
				return this.tname;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00040990 File Offset: 0x0003EB90
		public override string Namespace
		{
			get
			{
				return this.nspace;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00040998 File Offset: 0x0003EB98
		public override Type ReflectedType
		{
			get
			{
				return this.nesting_type;
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000409A0 File Offset: 0x0003EBA0
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			if (this.created != typeof(object))
			{
				return this.created.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
			}
			if (this.ctors == null)
			{
				return null;
			}
			ConstructorBuilder constructorBuilder = null;
			int num = 0;
			foreach (ConstructorBuilder constructorBuilder2 in this.ctors)
			{
				if (callConvention == CallingConventions.Any || constructorBuilder2.CallingConvention == callConvention)
				{
					constructorBuilder = constructorBuilder2;
					num++;
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (types != null)
			{
				MethodBase[] array2 = new MethodBase[num];
				if (num == 1)
				{
					array2[0] = constructorBuilder;
				}
				else
				{
					num = 0;
					foreach (ConstructorBuilder constructorInfo in this.ctors)
					{
						if (callConvention == CallingConventions.Any || constructorInfo.CallingConvention == callConvention)
						{
							array2[num++] = constructorInfo;
						}
					}
				}
				if (binder == null)
				{
					binder = Binder.DefaultBinder;
				}
				return (ConstructorInfo)binder.SelectMethod(bindingAttr, array2, types, modifiers);
			}
			if (num > 1)
			{
				throw new AmbiguousMatchException();
			}
			return constructorBuilder;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00040AD4 File Offset: 0x0003ECD4
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (!this.is_created && !this.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00040AFC File Offset: 0x0003ECFC
		public override object[] GetCustomAttributes(bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(inherit);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00040B10 File Offset: 0x0003ED10
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00040B28 File Offset: 0x0003ED28
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes)
		{
			return this.DefineConstructor(attributes, callingConvention, parameterTypes, null, null);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00040B38 File Offset: 0x0003ED38
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			this.check_not_created();
			ConstructorBuilder constructorBuilder = new ConstructorBuilder(this, attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = new ConstructorBuilder[this.ctors.Length + 1];
				Array.Copy(this.ctors, array, this.ctors.Length);
				array[this.ctors.Length] = constructorBuilder;
				this.ctors = array;
			}
			else
			{
				this.ctors = new ConstructorBuilder[1];
				this.ctors[0] = constructorBuilder;
			}
			return constructorBuilder;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00040BB8 File Offset: 0x0003EDB8
		[ComVisible(true)]
		public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
		{
			Type corlib_object_type;
			if (this.parent != null)
			{
				corlib_object_type = this.parent;
			}
			else
			{
				corlib_object_type = this.pmodule.assemblyb.corlib_object_type;
			}
			ConstructorInfo constructor = corlib_object_type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (constructor == null)
			{
				throw new NotSupportedException("Parent does not have a default constructor. The default constructor must be explicitly defined.");
			}
			ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, Type.EmptyTypes);
			ILGenerator ilgenerator = constructorBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return constructorBuilder;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00040C48 File Offset: 0x0003EE48
		private void append_method(MethodBuilder mb)
		{
			if (this.methods != null)
			{
				if (this.methods.Length == this.num_methods)
				{
					MethodBuilder[] destinationArray = new MethodBuilder[this.methods.Length * 2];
					Array.Copy(this.methods, destinationArray, this.num_methods);
					this.methods = destinationArray;
				}
			}
			else
			{
				this.methods = new MethodBuilder[1];
			}
			this.methods[this.num_methods] = mb;
			this.num_methods++;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00040CCC File Offset: 0x0003EECC
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00040CDC File Offset: 0x0003EEDC
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00040CFC File Offset: 0x0003EEFC
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.check_name("name", name);
			this.check_not_created();
			if (this.IsInterface && ((attributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (attributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("Interface method must be abstract and virtual.");
			}
			if (returnType == null)
			{
				returnType = this.pmodule.assemblyb.corlib_void_type;
			}
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			this.append_method(methodBuilder);
			return methodBuilder;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00040D88 File Offset: 0x0003EF88
		public void DefineMethodOverride(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
		{
			if (methodInfoBody == null)
			{
				throw new ArgumentNullException("methodInfoBody");
			}
			if (methodInfoDeclaration == null)
			{
				throw new ArgumentNullException("methodInfoDeclaration");
			}
			this.check_not_created();
			if (methodInfoBody.DeclaringType != this)
			{
				throw new ArgumentException("method body must belong to this type");
			}
			if (methodInfoBody is MethodBuilder)
			{
				MethodBuilder methodBuilder = (MethodBuilder)methodInfoBody;
				methodBuilder.set_override(methodInfoDeclaration);
			}
		}

		// Token: 0x060010D4 RID: 4308
		[MethodImpl(4096)]
		private extern Type create_runtime_class(TypeBuilder tb);

		// Token: 0x060010D5 RID: 4309 RVA: 0x00040DF0 File Offset: 0x0003EFF0
		private bool is_nested_in(Type t)
		{
			while (t != null)
			{
				if (t == this)
				{
					return true;
				}
				t = t.DeclaringType;
			}
			return false;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00040E10 File Offset: 0x0003F010
		private bool has_ctor_method()
		{
			MethodAttributes methodAttributes = MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
			for (int i = 0; i < this.num_methods; i++)
			{
				MethodBuilder methodBuilder = this.methods[i];
				if (methodBuilder.Name == ConstructorInfo.ConstructorName && (methodBuilder.Attributes & methodAttributes) == methodAttributes)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00040E6C File Offset: 0x0003F06C
		public Type CreateType()
		{
			if (this.createTypeCalled)
			{
				return this.created;
			}
			if (!this.IsInterface && this.parent == null && this != this.pmodule.assemblyb.corlib_object_type && this.FullName != "<Module>")
			{
				this.SetParent(this.pmodule.assemblyb.corlib_object_type);
			}
			this.create_generic_class();
			if (this.fields != null)
			{
				foreach (FieldBuilder fieldBuilder in this.fields)
				{
					if (fieldBuilder != null)
					{
						Type fieldType = fieldBuilder.FieldType;
						if (!fieldBuilder.IsStatic && fieldType is TypeBuilder && fieldType.IsValueType && fieldType != this && this.is_nested_in(fieldType))
						{
							TypeBuilder typeBuilder = (TypeBuilder)fieldType;
							if (!typeBuilder.is_created)
							{
								AppDomain.CurrentDomain.DoTypeResolve(typeBuilder);
								if (!typeBuilder.is_created)
								{
								}
							}
						}
					}
				}
			}
			if (this.parent != null && this.parent.IsSealed)
			{
				throw new TypeLoadException(string.Concat(new object[]
				{
					"Could not load type '",
					this.FullName,
					"' from assembly '",
					this.Assembly,
					"' because the parent type is sealed."
				}));
			}
			if (this.parent == this.pmodule.assemblyb.corlib_enum_type && this.methods != null)
			{
				throw new TypeLoadException(string.Concat(new object[]
				{
					"Could not load type '",
					this.FullName,
					"' from assembly '",
					this.Assembly,
					"' because it is an enum with methods."
				}));
			}
			if (this.methods != null)
			{
				bool flag = !this.IsAbstract;
				for (int j = 0; j < this.num_methods; j++)
				{
					MethodBuilder methodBuilder = this.methods[j];
					if (flag && methodBuilder.IsAbstract)
					{
						throw new InvalidOperationException("Type is concrete but has abstract method " + methodBuilder);
					}
					methodBuilder.check_override();
					methodBuilder.fixup();
				}
			}
			if (!this.IsInterface && !this.IsValueType && this.ctors == null && this.tname != "<Module>" && ((this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) | TypeAttributes.Sealed) != (TypeAttributes.Abstract | TypeAttributes.Sealed) && !this.has_ctor_method())
			{
				this.DefineDefaultConstructor(MethodAttributes.Public);
			}
			if (this.ctors != null)
			{
				foreach (ConstructorBuilder constructorBuilder in this.ctors)
				{
					constructorBuilder.fixup();
				}
			}
			this.createTypeCalled = true;
			this.created = this.create_runtime_class(this);
			if (this.created != null)
			{
				return this.created;
			}
			return this;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00041174 File Offset: 0x0003F374
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetConstructors(bindingAttr);
			}
			if (!this.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			return this.GetConstructorsInternal(bindingAttr);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000411A8 File Offset: 0x0003F3A8
		internal ConstructorInfo[] GetConstructorsInternal(BindingFlags bindingAttr)
		{
			if (this.ctors == null)
			{
				return new ConstructorInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (ConstructorBuilder constructorBuilder in this.ctors)
			{
				bool flag = false;
				MethodAttributes attributes = constructorBuilder.Attributes;
				if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
				{
					if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
					{
						if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						arrayList.Add(constructorBuilder);
					}
				}
			}
			ConstructorInfo[] array2 = new ConstructorInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0004127C File Offset: 0x0003F47C
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00041284 File Offset: 0x0003F484
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetEvent(name, bindingAttr);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0004129C File Offset: 0x0003F49C
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetEvents(bindingAttr);
			}
			if (!this.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			return new EventInfo[0];
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000412D0 File Offset: 0x0003F4D0
		internal EventInfo[] GetEvents_internal(BindingFlags bindingAttr)
		{
			if (this.events == null)
			{
				return new EventInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (EventBuilder eventBuilder in this.events)
			{
				if (eventBuilder != null)
				{
					EventInfo eventInfo = this.get_event_info(eventBuilder);
					bool flag = false;
					MethodInfo methodInfo = eventInfo.GetAddMethod(true);
					if (methodInfo == null)
					{
						methodInfo = eventInfo.GetRemoveMethod(true);
					}
					if (methodInfo != null)
					{
						MethodAttributes attributes = methodInfo.Attributes;
						if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
						{
							if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							flag = false;
							if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
							{
								if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
								{
									flag = true;
								}
							}
							else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
							{
								flag = true;
							}
							if (flag)
							{
								arrayList.Add(eventInfo);
							}
						}
					}
				}
			}
			EventInfo[] array2 = new EventInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000413E0 File Offset: 0x0003F5E0
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetField(name, bindingAttr);
			}
			if (this.fields == null)
			{
				return null;
			}
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (fieldInfo != null)
				{
					if (!(fieldInfo.Name != name))
					{
						bool flag = false;
						FieldAttributes attributes = fieldInfo.Attributes;
						if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
						{
							if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							flag = false;
							if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
							{
								if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
								{
									flag = true;
								}
							}
							else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
							{
								flag = true;
							}
							if (flag)
							{
								return fieldInfo;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000414C4 File Offset: 0x0003F6C4
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetFields(bindingAttr);
			}
			if (this.fields == null)
			{
				return new FieldInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (fieldInfo != null)
				{
					bool flag = false;
					FieldAttributes attributes = fieldInfo.Attributes;
					if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(fieldInfo);
						}
					}
				}
			}
			FieldInfo[] array2 = new FieldInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000415BC File Offset: 0x0003F7BC
		public override Type[] GetInterfaces()
		{
			if (this.is_created)
			{
				return this.created.GetInterfaces();
			}
			if (this.interfaces != null)
			{
				Type[] array = new Type[this.interfaces.Length];
				this.interfaces.CopyTo(array, 0);
				return array;
			}
			return Type.EmptyTypes;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00041610 File Offset: 0x0003F810
		private MethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, bool ignoreCase, Type reflected_type)
		{
			MethodInfo[] array2;
			if ((bindingAttr & BindingFlags.DeclaredOnly) == BindingFlags.Default && this.parent != null)
			{
				MethodInfo[] array = this.parent.GetMethods(bindingAttr);
				ArrayList arrayList = new ArrayList(array.Length);
				bool flag = (bindingAttr & BindingFlags.FlattenHierarchy) != BindingFlags.Default;
				foreach (MethodInfo methodInfo in array)
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if (!methodInfo.IsStatic || flag)
					{
						bool flag2;
						switch (attributes & MethodAttributes.MemberAccessMask)
						{
						case MethodAttributes.Private:
							flag2 = false;
							break;
						case MethodAttributes.FamANDAssem:
						case MethodAttributes.Family:
						case MethodAttributes.FamORAssem:
							goto IL_B6;
						case MethodAttributes.Assembly:
							flag2 = ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default);
							break;
						case MethodAttributes.Public:
							flag2 = ((bindingAttr & BindingFlags.Public) != BindingFlags.Default);
							break;
						default:
							goto IL_B6;
						}
						IL_C6:
						if (flag2)
						{
							arrayList.Add(methodInfo);
							goto IL_D6;
						}
						goto IL_D6;
						IL_B6:
						flag2 = ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default);
						goto IL_C6;
					}
					IL_D6:;
				}
				if (this.methods == null)
				{
					array2 = new MethodInfo[arrayList.Count];
					arrayList.CopyTo(array2);
				}
				else
				{
					array2 = new MethodInfo[this.methods.Length + arrayList.Count];
					arrayList.CopyTo(array2, 0);
					this.methods.CopyTo(array2, arrayList.Count);
				}
			}
			else
			{
				array2 = this.methods;
			}
			if (array2 == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList2 = new ArrayList();
			foreach (MethodInfo methodInfo2 in array2)
			{
				if (methodInfo2 != null)
				{
					if (name == null || string.Compare(methodInfo2.Name, name, ignoreCase) == 0)
					{
						bool flag2 = false;
						MethodAttributes attributes = methodInfo2.Attributes;
						if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
						{
							if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
							{
								flag2 = true;
							}
						}
						else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
						{
							flag2 = true;
						}
						if (flag2)
						{
							flag2 = false;
							if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
							{
								if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
								{
									flag2 = true;
								}
							}
							else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
							{
								flag2 = true;
							}
							if (flag2)
							{
								arrayList2.Add(methodInfo2);
							}
						}
					}
				}
			}
			MethodInfo[] array4 = new MethodInfo[arrayList2.Count];
			arrayList2.CopyTo(array4);
			return array4;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00041854 File Offset: 0x0003FA54
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodsByName(null, bindingAttr, false, this);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00041860 File Offset: 0x0003FA60
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			bool ignoreCase = (bindingAttr & BindingFlags.IgnoreCase) != BindingFlags.Default;
			MethodInfo[] methodsByName = this.GetMethodsByName(name, bindingAttr, ignoreCase, this);
			MethodInfo methodInfo = null;
			int num = (types == null) ? 0 : types.Length;
			int num2 = 0;
			foreach (MethodInfo methodInfo2 in methodsByName)
			{
				if (callConvention == CallingConventions.Any || (methodInfo2.CallingConvention & callConvention) == callConvention)
				{
					methodInfo = methodInfo2;
					num2++;
				}
			}
			if (num2 == 0)
			{
				return null;
			}
			if (num2 == 1 && num == 0)
			{
				return methodInfo;
			}
			MethodBase[] array2 = new MethodBase[num2];
			if (num2 == 1)
			{
				array2[0] = methodInfo;
			}
			else
			{
				num2 = 0;
				foreach (MethodInfo methodInfo3 in methodsByName)
				{
					if (callConvention == CallingConventions.Any || (methodInfo3.CallingConvention & callConvention) == callConvention)
					{
						array2[num2++] = methodInfo3;
					}
				}
			}
			if (types == null)
			{
				return (MethodInfo)Binder.FindMostDerivedMatch(array2);
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			return (MethodInfo)binder.SelectMethod(bindingAttr, array2, types, modifiers);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000419A0 File Offset: 0x0003FBA0
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetProperties(bindingAttr);
			}
			if (this.properties == null)
			{
				return new PropertyInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (PropertyBuilder propertyInfo in this.properties)
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
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(propertyInfo);
						}
					}
				}
			}
			PropertyInfo[] array2 = new PropertyInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00041AB0 File Offset: 0x0003FCB0
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00041AB8 File Offset: 0x0003FCB8
		protected override bool HasElementTypeImpl()
		{
			return this.is_created && this.created.HasElementType;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00041AD4 File Offset: 0x0003FCD4
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			this.check_created();
			return this.created.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00041B00 File Offset: 0x0003FD00
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00041B04 File Offset: 0x0003FD04
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00041B08 File Offset: 0x0003FD08
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00041B0C File Offset: 0x0003FD0C
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00041B10 File Offset: 0x0003FD10
		protected override bool IsValueTypeImpl()
		{
			return (Type.type_is_subtype_of(this, this.pmodule.assemblyb.corlib_value_type, false) || Type.type_is_subtype_of(this, typeof(ValueType), false)) && this != this.pmodule.assemblyb.corlib_value_type && this != this.pmodule.assemblyb.corlib_enum_type;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00041B80 File Offset: 0x0003FD80
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00041B8C File Offset: 0x0003FD8C
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00041BA4 File Offset: 0x0003FDA4
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00041BAC File Offset: 0x0003FDAC
		[MonoTODO]
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			return base.MakeGenericType(typeArguments);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00041BB8 File Offset: 0x0003FDB8
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00041BC0 File Offset: 0x0003FDC0
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				this.check_created();
				return this.created.TypeHandle;
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00041BD4 File Offset: 0x0003FDD4
		public void SetParent(Type parent)
		{
			this.check_not_created();
			if (parent == null)
			{
				if ((this.attrs & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
				{
					if ((this.attrs & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
					{
						throw new InvalidOperationException("Interface must be declared abstract.");
					}
					this.parent = null;
				}
				else
				{
					this.parent = typeof(object);
				}
			}
			else
			{
				this.parent = parent;
			}
			this.setup_internal_class(this);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00041C48 File Offset: 0x0003FE48
		internal int get_next_table_index(object obj, int table, bool inc)
		{
			return this.pmodule.get_next_table_index(obj, table, inc);
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00041C58 File Offset: 0x0003FE58
		internal bool IsCompilerContext
		{
			get
			{
				return this.pmodule.assemblyb.IsCompilerContext;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x00041C6C File Offset: 0x0003FE6C
		internal bool is_created
		{
			get
			{
				return this.created != null;
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00041C7C File Offset: 0x0003FE7C
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00041C88 File Offset: 0x0003FE88
		private void check_not_created()
		{
			if (this.is_created)
			{
				throw new InvalidOperationException("Unable to change after type has been created.");
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00041CA0 File Offset: 0x0003FEA0
		private void check_created()
		{
			if (!this.is_created)
			{
				throw this.not_supported();
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00041CB4 File Offset: 0x0003FEB4
		private void check_name(string argName, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(argName);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal", argName);
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException("Illegal name", argName);
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00041CF4 File Offset: 0x0003FEF4
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00041CFC File Offset: 0x0003FEFC
		[MonoTODO]
		public override bool IsAssignableFrom(Type c)
		{
			return base.IsAssignableFrom(c);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00041D08 File Offset: 0x0003FF08
		[ComVisible(true)]
		[MonoTODO]
		public override bool IsSubclassOf(Type c)
		{
			return base.IsSubclassOf(c);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00041D14 File Offset: 0x0003FF14
		[MonoTODO("arrays")]
		internal bool IsAssignableTo(Type c)
		{
			if (c == this)
			{
				return true;
			}
			if (c.IsInterface)
			{
				if (this.parent != null && this.is_created && c.IsAssignableFrom(this.parent))
				{
					return true;
				}
				if (this.interfaces == null)
				{
					return false;
				}
				foreach (Type c2 in this.interfaces)
				{
					if (c.IsAssignableFrom(c2))
					{
						return true;
					}
				}
				if (!this.is_created)
				{
					return false;
				}
			}
			if (this.parent == null)
			{
				return c == typeof(object);
			}
			return c.IsAssignableFrom(this.parent);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00041DCC File Offset: 0x0003FFCC
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return null;
			}
			Type[] array = new Type[this.generic_params.Length];
			this.generic_params.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00041E04 File Offset: 0x00040004
		public override Type GetGenericTypeDefinition()
		{
			if (this.generic_params == null)
			{
				throw new InvalidOperationException("Type is not generic");
			}
			return this;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00041E20 File Offset: 0x00040020
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.generic_params != null;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001102 RID: 4354
		public override extern bool IsGenericParameter { [MethodImpl(4096)] get; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00041E30 File Offset: 0x00040030
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00041E40 File Offset: 0x00040040
		public override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00041E48 File Offset: 0x00040048
		[MonoTODO]
		public override int GenericParameterPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00041E4C File Offset: 0x0004004C
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00041E50 File Offset: 0x00040050
		public static ConstructorInfo GetConstructor(Type type, ConstructorInfo constructor)
		{
			if (type == null)
			{
				throw new ArgumentException("Type is not generic", "type");
			}
			ConstructorInfo constructor2 = type.GetConstructor(constructor);
			if (constructor2 == null)
			{
				throw new ArgumentException("constructor not found");
			}
			return constructor2;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00041E90 File Offset: 0x00040090
		private static bool IsValidGetMethodType(Type type)
		{
			if (type is TypeBuilder || type is MonoGenericClass)
			{
				return true;
			}
			if (type.Module is ModuleBuilder)
			{
				return true;
			}
			if (type.IsGenericParameter)
			{
				return false;
			}
			Type[] genericArguments = type.GetGenericArguments();
			if (genericArguments == null)
			{
				return false;
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (TypeBuilder.IsValidGetMethodType(genericArguments[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00041F08 File Offset: 0x00040108
		public static MethodInfo GetMethod(Type type, MethodInfo method)
		{
			if (!TypeBuilder.IsValidGetMethodType(type))
			{
				throw new ArgumentException("type is not TypeBuilder but " + type.GetType(), "type");
			}
			if (!type.IsGenericType)
			{
				throw new ArgumentException("type is not a generic type", "type");
			}
			if (!method.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException("method declaring type is not a generic type definition", "method");
			}
			if (method.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("method declaring type is not the generic type definition of type", "method");
			}
			MethodInfo method2 = type.GetMethod(method);
			if (method2 == null)
			{
				throw new ArgumentException(string.Format("method {0} not found in type {1}", method.Name, type));
			}
			return method2;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00041FC0 File Offset: 0x000401C0
		public static FieldInfo GetField(Type type, FieldInfo field)
		{
			FieldInfo field2 = type.GetField(field);
			if (field2 == null)
			{
				throw new Exception("field not found");
			}
			return field2;
		}

		// Token: 0x0400086A RID: 2154
		public const int UnspecifiedTypeSize = 0;

		// Token: 0x0400086B RID: 2155
		private string tname;

		// Token: 0x0400086C RID: 2156
		private string nspace;

		// Token: 0x0400086D RID: 2157
		private Type parent;

		// Token: 0x0400086E RID: 2158
		private Type nesting_type;

		// Token: 0x0400086F RID: 2159
		internal Type[] interfaces;

		// Token: 0x04000870 RID: 2160
		internal int num_methods;

		// Token: 0x04000871 RID: 2161
		internal MethodBuilder[] methods;

		// Token: 0x04000872 RID: 2162
		internal ConstructorBuilder[] ctors;

		// Token: 0x04000873 RID: 2163
		internal PropertyBuilder[] properties;

		// Token: 0x04000874 RID: 2164
		internal int num_fields;

		// Token: 0x04000875 RID: 2165
		internal FieldBuilder[] fields;

		// Token: 0x04000876 RID: 2166
		internal EventBuilder[] events;

		// Token: 0x04000877 RID: 2167
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000878 RID: 2168
		internal TypeBuilder[] subtypes;

		// Token: 0x04000879 RID: 2169
		internal TypeAttributes attrs;

		// Token: 0x0400087A RID: 2170
		private int table_idx;

		// Token: 0x0400087B RID: 2171
		private ModuleBuilder pmodule;

		// Token: 0x0400087C RID: 2172
		private int class_size;

		// Token: 0x0400087D RID: 2173
		private PackingSize packing_size;

		// Token: 0x0400087E RID: 2174
		private IntPtr generic_container;

		// Token: 0x0400087F RID: 2175
		private GenericTypeParameterBuilder[] generic_params;

		// Token: 0x04000880 RID: 2176
		private RefEmitPermissionSet[] permissions;

		// Token: 0x04000881 RID: 2177
		private Type created;

		// Token: 0x04000882 RID: 2178
		private string fullname;

		// Token: 0x04000883 RID: 2179
		private bool createTypeCalled;

		// Token: 0x04000884 RID: 2180
		private Type underlying_type;
	}
}
