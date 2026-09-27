using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	internal class MonoType : Type, ISerializable
	{
		// Token: 0x06000D29 RID: 3369
		[MethodImpl(4096)]
		private static extern TypeAttributes get_attributes(Type type);

		// Token: 0x06000D2A RID: 3370 RVA: 0x00032E14 File Offset: 0x00031014
		internal ConstructorInfo GetDefaultConstructor()
		{
			if (this.type_info == null)
			{
				this.type_info = new MonoTypeInfo();
			}
			ConstructorInfo result;
			if ((result = this.type_info.default_ctor) == null)
			{
				result = (this.type_info.default_ctor = this.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, Type.EmptyTypes, null));
			}
			return result;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00032E6C File Offset: 0x0003106C
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return MonoType.get_attributes(this);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00032E74 File Offset: 0x00031074
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (bindingAttr == BindingFlags.Default)
			{
				bindingAttr = (BindingFlags.Instance | BindingFlags.Public);
			}
			ConstructorInfo[] constructors = this.GetConstructors(bindingAttr);
			ConstructorInfo constructorInfo = null;
			int num = 0;
			foreach (ConstructorInfo constructorInfo2 in constructors)
			{
				if (callConvention == CallingConventions.Any || (constructorInfo2.CallingConvention & callConvention) == callConvention)
				{
					constructorInfo = constructorInfo2;
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
					array2[0] = constructorInfo;
				}
				else
				{
					num = 0;
					foreach (ConstructorInfo constructorInfo3 in constructors)
					{
						if (callConvention == CallingConventions.Any || (constructorInfo3.CallingConvention & callConvention) == callConvention)
						{
							array2[num++] = constructorInfo3;
						}
					}
				}
				if (binder == null)
				{
					binder = Binder.DefaultBinder;
				}
				return (ConstructorInfo)this.CheckMethodSecurity(binder.SelectMethod(bindingAttr, array2, types, modifiers));
			}
			if (num > 1)
			{
				throw new AmbiguousMatchException();
			}
			return (ConstructorInfo)this.CheckMethodSecurity(constructorInfo);
		}

		// Token: 0x06000D2D RID: 3373
		[MethodImpl(4096)]
		internal extern ConstructorInfo[] GetConstructors_internal(BindingFlags bindingAttr, Type reflected_type);

		// Token: 0x06000D2E RID: 3374 RVA: 0x00032F8C File Offset: 0x0003118C
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.GetConstructors_internal(bindingAttr, this);
		}

		// Token: 0x06000D2F RID: 3375
		[MethodImpl(4096)]
		private extern EventInfo InternalGetEvent(string name, BindingFlags bindingAttr);

		// Token: 0x06000D30 RID: 3376 RVA: 0x00032F98 File Offset: 0x00031198
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.InternalGetEvent(name, bindingAttr);
		}

		// Token: 0x06000D31 RID: 3377
		[MethodImpl(4096)]
		internal extern EventInfo[] GetEvents_internal(BindingFlags bindingAttr, Type reflected_type);

		// Token: 0x06000D32 RID: 3378 RVA: 0x00032FB4 File Offset: 0x000311B4
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.GetEvents_internal(bindingAttr, this);
		}

		// Token: 0x06000D33 RID: 3379
		[MethodImpl(4096)]
		public override extern FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06000D34 RID: 3380
		[MethodImpl(4096)]
		internal extern FieldInfo[] GetFields_internal(BindingFlags bindingAttr, Type reflected_type);

		// Token: 0x06000D35 RID: 3381 RVA: 0x00032FC0 File Offset: 0x000311C0
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.GetFields_internal(bindingAttr, this);
		}

		// Token: 0x06000D36 RID: 3382
		[MethodImpl(4096)]
		public override extern Type[] GetInterfaces();

		// Token: 0x06000D37 RID: 3383
		[MethodImpl(4096)]
		internal extern MethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, bool ignoreCase, Type reflected_type);

		// Token: 0x06000D38 RID: 3384 RVA: 0x00032FCC File Offset: 0x000311CC
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodsByName(null, bindingAttr, false, this);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00032FD8 File Offset: 0x000311D8
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			bool ignoreCase = (bindingAttr & BindingFlags.IgnoreCase) != BindingFlags.Default;
			MethodInfo[] methodsByName = this.GetMethodsByName(name, bindingAttr, ignoreCase, this);
			MethodInfo methodInfo = null;
			int num = 0;
			foreach (MethodInfo methodInfo2 in methodsByName)
			{
				if (callConvention == CallingConventions.Any || (methodInfo2.CallingConvention & callConvention) == callConvention)
				{
					methodInfo = methodInfo2;
					num++;
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (num == 1 && types == null)
			{
				return (MethodInfo)this.CheckMethodSecurity(methodInfo);
			}
			MethodBase[] array2 = new MethodBase[num];
			if (num == 1)
			{
				array2[0] = methodInfo;
			}
			else
			{
				num = 0;
				foreach (MethodInfo methodInfo3 in methodsByName)
				{
					if (callConvention == CallingConventions.Any || (methodInfo3.CallingConvention & callConvention) == callConvention)
					{
						array2[num++] = methodInfo3;
					}
				}
			}
			if (types == null)
			{
				return (MethodInfo)this.CheckMethodSecurity(Binder.FindMostDerivedMatch(array2));
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			return (MethodInfo)this.CheckMethodSecurity(binder.SelectMethod(bindingAttr, array2, types, modifiers));
		}

		// Token: 0x06000D3A RID: 3386
		[MethodImpl(4096)]
		private extern MethodInfo GetCorrespondingInflatedMethod(MethodInfo generic);

		// Token: 0x06000D3B RID: 3387
		[MethodImpl(4096)]
		private extern ConstructorInfo GetCorrespondingInflatedConstructor(ConstructorInfo generic);

		// Token: 0x06000D3C RID: 3388 RVA: 0x00033114 File Offset: 0x00031314
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedMethod(fromNoninstanciated);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00033130 File Offset: 0x00031330
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedConstructor(fromNoninstanciated);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003314C File Offset: 0x0003134C
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			BindingFlags bindingFlags = (!fromNoninstanciated.IsStatic) ? BindingFlags.Instance : BindingFlags.Static;
			bindingFlags |= ((!fromNoninstanciated.IsPublic) ? BindingFlags.NonPublic : BindingFlags.Public);
			return this.GetField(fromNoninstanciated.Name, bindingFlags);
		}

		// Token: 0x06000D3F RID: 3391
		[MethodImpl(4096)]
		internal extern PropertyInfo[] GetPropertiesByName(string name, BindingFlags bindingAttr, bool icase, Type reflected_type);

		// Token: 0x06000D40 RID: 3392 RVA: 0x00033190 File Offset: 0x00031390
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.GetPropertiesByName(null, bindingAttr, false, this);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0003319C File Offset: 0x0003139C
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			bool icase = (bindingAttr & BindingFlags.IgnoreCase) != BindingFlags.Default;
			PropertyInfo[] propertiesByName = this.GetPropertiesByName(name, bindingAttr, icase, this);
			int num = propertiesByName.Length;
			if (num == 0)
			{
				return null;
			}
			if (num == 1 && (types == null || types.Length == 0) && (returnType == null || returnType == propertiesByName[0].PropertyType))
			{
				return propertiesByName[0];
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			return binder.SelectProperty(bindingAttr, propertiesByName, returnType, types, modifiers);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00033218 File Offset: 0x00031418
		protected override bool HasElementTypeImpl()
		{
			return this.IsArrayImpl() || this.IsByRefImpl() || this.IsPointerImpl();
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0003323C File Offset: 0x0003143C
		protected override bool IsArrayImpl()
		{
			return Type.IsArrayImpl(this);
		}

		// Token: 0x06000D44 RID: 3396
		[MethodImpl(4096)]
		protected override extern bool IsByRefImpl();

		// Token: 0x06000D45 RID: 3397
		[MethodImpl(4096)]
		protected override extern bool IsPointerImpl();

		// Token: 0x06000D46 RID: 3398
		[MethodImpl(4096)]
		protected override extern bool IsPrimitiveImpl();

		// Token: 0x06000D47 RID: 3399 RVA: 0x00033244 File Offset: 0x00031444
		public override bool IsSubclassOf(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return base.IsSubclassOf(type);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00033260 File Offset: 0x00031460
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			if ((invokeAttr & BindingFlags.CreateInstance) != BindingFlags.Default)
			{
				if ((invokeAttr & (BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.SetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException("bindingFlags");
				}
			}
			else if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if ((invokeAttr & BindingFlags.GetField) != BindingFlags.Default && (invokeAttr & BindingFlags.SetField) != BindingFlags.Default)
			{
				throw new ArgumentException("Cannot specify both Get and Set on a field.", "bindingFlags");
			}
			if ((invokeAttr & BindingFlags.GetProperty) != BindingFlags.Default && (invokeAttr & BindingFlags.SetProperty) != BindingFlags.Default)
			{
				throw new ArgumentException("Cannot specify both Get and Set on a property.", "bindingFlags");
			}
			if ((invokeAttr & BindingFlags.InvokeMethod) != BindingFlags.Default)
			{
				if ((invokeAttr & BindingFlags.SetField) != BindingFlags.Default)
				{
					throw new ArgumentException("Cannot specify Set on a field and Invoke on a method.", "bindingFlags");
				}
				if ((invokeAttr & BindingFlags.SetProperty) != BindingFlags.Default)
				{
					throw new ArgumentException("Cannot specify Set on a property and Invoke on a method.", "bindingFlags");
				}
			}
			if (namedParameters != null && (args == null || args.Length < namedParameters.Length))
			{
				throw new ArgumentException("namedParameters cannot be more than named arguments in number");
			}
			if ((invokeAttr & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty)) == BindingFlags.Default)
			{
				throw new ArgumentException("Must specify binding flags describing the invoke operation required.", "bindingFlags");
			}
			if ((invokeAttr & (BindingFlags.Public | BindingFlags.NonPublic)) == BindingFlags.Default)
			{
				invokeAttr |= BindingFlags.Public;
			}
			if ((invokeAttr & (BindingFlags.Instance | BindingFlags.Static)) == BindingFlags.Default)
			{
				invokeAttr |= (BindingFlags.Instance | BindingFlags.Static);
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			if ((invokeAttr & BindingFlags.CreateInstance) != BindingFlags.Default)
			{
				invokeAttr |= BindingFlags.DeclaredOnly;
				ConstructorInfo[] constructors = this.GetConstructors(invokeAttr);
				object state = null;
				MethodBase methodBase = binder.BindToMethod(invokeAttr, constructors, ref args, modifiers, culture, namedParameters, out state);
				if (methodBase != null)
				{
					object result = methodBase.Invoke(target, invokeAttr, binder, args, culture);
					binder.ReorderArgumentArray(ref args, state);
					return result;
				}
				if (this.IsValueType && args == null)
				{
					return Activator.CreateInstanceInternal(this);
				}
				throw new MissingMethodException("Constructor on type '" + this.FullName + "' not found.");
			}
			else
			{
				if (name == string.Empty && Attribute.IsDefined(this, typeof(DefaultMemberAttribute)))
				{
					DefaultMemberAttribute defaultMemberAttribute = (DefaultMemberAttribute)Attribute.GetCustomAttribute(this, typeof(DefaultMemberAttribute));
					name = defaultMemberAttribute.MemberName;
				}
				bool flag = (invokeAttr & BindingFlags.IgnoreCase) != BindingFlags.Default;
				string text = null;
				bool flag2 = false;
				if ((invokeAttr & BindingFlags.InvokeMethod) != BindingFlags.Default)
				{
					MethodInfo[] methodsByName = this.GetMethodsByName(name, invokeAttr, flag, this);
					object state2 = null;
					if (args == null)
					{
						args = new object[0];
					}
					MethodBase methodBase2 = binder.BindToMethod(invokeAttr, methodsByName, ref args, modifiers, culture, namedParameters, out state2);
					if (methodBase2 != null)
					{
						ParameterInfo[] parameters = methodBase2.GetParameters();
						for (int i = 0; i < parameters.Length; i++)
						{
							if (System.Reflection.Missing.Value == args[i] && (parameters[i].Attributes & ParameterAttributes.HasDefault) != ParameterAttributes.HasDefault)
							{
								throw new ArgumentException("Used Missing.Value for argument without default value", "parameters");
							}
						}
						bool flag3 = parameters.Length > 0 && Attribute.IsDefined(parameters[parameters.Length - 1], typeof(ParamArrayAttribute));
						if (flag3)
						{
							this.ReorderParamArrayArguments(ref args, methodBase2);
						}
						object result2 = methodBase2.Invoke(target, invokeAttr, binder, args, culture);
						binder.ReorderArgumentArray(ref args, state2);
						return result2;
					}
					if (methodsByName.Length > 0)
					{
						text = "The best match for method " + name + " has some invalid parameter.";
					}
					else
					{
						text = "Cannot find method " + name + ".";
					}
				}
				if ((invokeAttr & BindingFlags.GetField) != BindingFlags.Default)
				{
					FieldInfo field = this.GetField(name, invokeAttr);
					if (field != null)
					{
						return field.GetValue(target);
					}
					if ((invokeAttr & BindingFlags.GetProperty) == BindingFlags.Default)
					{
						flag2 = true;
					}
				}
				else if ((invokeAttr & BindingFlags.SetField) != BindingFlags.Default)
				{
					FieldInfo field2 = this.GetField(name, invokeAttr);
					if (field2 != null)
					{
						if (args == null)
						{
							throw new ArgumentNullException("providedArgs");
						}
						if (args == null || args.Length != 1)
						{
							throw new ArgumentException("Only the field value can be specified to set a field value.", "bindingFlags");
						}
						field2.SetValue(target, args[0]);
						return null;
					}
					else if ((invokeAttr & BindingFlags.SetProperty) == BindingFlags.Default)
					{
						flag2 = true;
					}
				}
				if ((invokeAttr & BindingFlags.GetProperty) != BindingFlags.Default)
				{
					PropertyInfo[] propertiesByName = this.GetPropertiesByName(name, invokeAttr, flag, this);
					object state3 = null;
					int num = 0;
					for (int j = 0; j < propertiesByName.Length; j++)
					{
						if (propertiesByName[j].GetGetMethod(true) != null)
						{
							num++;
						}
					}
					MethodBase[] array = new MethodBase[num];
					num = 0;
					for (int j = 0; j < propertiesByName.Length; j++)
					{
						MethodBase getMethod = propertiesByName[j].GetGetMethod(true);
						if (getMethod != null)
						{
							array[num++] = getMethod;
						}
					}
					MethodBase methodBase3 = binder.BindToMethod(invokeAttr, array, ref args, modifiers, culture, namedParameters, out state3);
					if (methodBase3 != null)
					{
						ParameterInfo[] parameters2 = methodBase3.GetParameters();
						bool flag4 = parameters2.Length > 0 && Attribute.IsDefined(parameters2[parameters2.Length - 1], typeof(ParamArrayAttribute));
						if (flag4)
						{
							this.ReorderParamArrayArguments(ref args, methodBase3);
						}
						object result3 = methodBase3.Invoke(target, invokeAttr, binder, args, culture);
						binder.ReorderArgumentArray(ref args, state3);
						return result3;
					}
					flag2 = true;
				}
				else if ((invokeAttr & BindingFlags.SetProperty) != BindingFlags.Default)
				{
					PropertyInfo[] propertiesByName2 = this.GetPropertiesByName(name, invokeAttr, flag, this);
					object state4 = null;
					int num2 = 0;
					for (int k = 0; k < propertiesByName2.Length; k++)
					{
						if (propertiesByName2[k].GetSetMethod(true) != null)
						{
							num2++;
						}
					}
					MethodBase[] array2 = new MethodBase[num2];
					num2 = 0;
					for (int k = 0; k < propertiesByName2.Length; k++)
					{
						MethodBase setMethod = propertiesByName2[k].GetSetMethod(true);
						if (setMethod != null)
						{
							array2[num2++] = setMethod;
						}
					}
					MethodBase methodBase4 = binder.BindToMethod(invokeAttr, array2, ref args, modifiers, culture, namedParameters, out state4);
					if (methodBase4 != null)
					{
						ParameterInfo[] parameters3 = methodBase4.GetParameters();
						bool flag5 = parameters3.Length > 0 && Attribute.IsDefined(parameters3[parameters3.Length - 1], typeof(ParamArrayAttribute));
						if (flag5)
						{
							this.ReorderParamArrayArguments(ref args, methodBase4);
						}
						object result4 = methodBase4.Invoke(target, invokeAttr, binder, args, culture);
						binder.ReorderArgumentArray(ref args, state4);
						return result4;
					}
					flag2 = true;
				}
				if (text != null)
				{
					throw new MissingMethodException(text);
				}
				if (flag2)
				{
					throw new MissingFieldException("Cannot find variable " + name + ".");
				}
				return null;
			}
		}

		// Token: 0x06000D49 RID: 3401
		[MethodImpl(4096)]
		public override extern Type GetElementType();

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x000338C4 File Offset: 0x00031AC4
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000D4B RID: 3403
		public override extern Assembly Assembly { [MethodImpl(4096)] get; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x000338C8 File Offset: 0x00031AC8
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.getFullName(true, true);
			}
		}

		// Token: 0x06000D4D RID: 3405
		[MethodImpl(4096)]
		private extern string getFullName(bool full_name, bool assembly_qualified);

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000D4E RID: 3406
		public override extern Type BaseType { [MethodImpl(4096)] get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x000338D4 File Offset: 0x00031AD4
		public override string FullName
		{
			get
			{
				if (this.type_info == null)
				{
					this.type_info = new MonoTypeInfo();
				}
				string result;
				if ((result = this.type_info.full_name) == null)
				{
					result = (this.type_info.full_name = this.getFullName(true, false));
				}
				return result;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00033924 File Offset: 0x00031B24
		public override Guid GUID
		{
			get
			{
				object[] customAttributes = this.GetCustomAttributes(typeof(GuidAttribute), true);
				if (customAttributes.Length == 0)
				{
					return Guid.Empty;
				}
				return new Guid(((GuidAttribute)customAttributes[0]).Value);
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00033964 File Offset: 0x00031B64
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00033970 File Offset: 0x00031B70
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0003397C File Offset: 0x00031B7C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00033998 File Offset: 0x00031B98
		public override MemberTypes MemberType
		{
			get
			{
				if (this.DeclaringType != null && !this.IsGenericParameter)
				{
					return MemberTypes.NestedType;
				}
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000D55 RID: 3413
		public override extern string Name { [MethodImpl(4096)] get; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000D56 RID: 3414
		public override extern string Namespace { [MethodImpl(4096)] get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000D57 RID: 3415
		public override extern Module Module { [MethodImpl(4096)] get; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000D58 RID: 3416
		public override extern Type DeclaringType { [MethodImpl(4096)] get; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x000339B8 File Offset: 0x00031BB8
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x000339C0 File Offset: 0x00031BC0
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this._impl;
			}
		}

		// Token: 0x06000D5B RID: 3419
		[MethodImpl(4096)]
		public override extern int GetArrayRank();

		// Token: 0x06000D5C RID: 3420 RVA: 0x000339C8 File Offset: 0x00031BC8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetTypeData(this, info, context);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000339D4 File Offset: 0x00031BD4
		public override string ToString()
		{
			return this.getFullName(false, false);
		}

		// Token: 0x06000D5E RID: 3422
		[MethodImpl(4096)]
		public override extern Type[] GetGenericArguments();

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000339E0 File Offset: 0x00031BE0
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.IsGenericType)
				{
					foreach (Type type in this.GetGenericArguments())
					{
						if (type.ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return this.HasElementType && this.GetElementType().ContainsGenericParameters;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000D60 RID: 3424
		public override extern bool IsGenericParameter { [MethodImpl(4096)] get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000D61 RID: 3425
		public override extern MethodBase DeclaringMethod { [MethodImpl(4096)] get; }

		// Token: 0x06000D62 RID: 3426 RVA: 0x00033A4C File Offset: 0x00031C4C
		public override Type GetGenericTypeDefinition()
		{
			Type genericTypeDefinition_impl = base.GetGenericTypeDefinition_impl();
			if (genericTypeDefinition_impl == null)
			{
				throw new InvalidOperationException();
			}
			return genericTypeDefinition_impl;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00033A70 File Offset: 0x00031C70
		private MethodBase CheckMethodSecurity(MethodBase mb)
		{
			return mb;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00033A74 File Offset: 0x00031C74
		private void ReorderParamArrayArguments(ref object[] args, MethodBase method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			object[] array = new object[parameters.Length];
			Array array2 = Array.CreateInstance(parameters[parameters.Length - 1].ParameterType.GetElementType(), args.Length - (parameters.Length - 1));
			int num = 0;
			for (int i = 0; i < args.Length; i++)
			{
				if (i < parameters.Length - 1)
				{
					array[i] = args[i];
				}
				else
				{
					array2.SetValue(args[i], num);
					num++;
				}
			}
			array[parameters.Length - 1] = array2;
			args = array;
		}

		// Token: 0x0400057C RID: 1404
		[NonSerialized]
		private MonoTypeInfo type_info;
	}
}
