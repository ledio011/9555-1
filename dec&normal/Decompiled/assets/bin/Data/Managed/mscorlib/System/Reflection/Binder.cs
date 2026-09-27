using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200018B RID: 395
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Serializable]
	public abstract class Binder
	{
		// Token: 0x06000EB1 RID: 3761
		public abstract MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state);

		// Token: 0x06000EB2 RID: 3762
		public abstract object ChangeType(object value, Type type, CultureInfo culture);

		// Token: 0x06000EB3 RID: 3763
		public abstract void ReorderArgumentArray(ref object[] args, object state);

		// Token: 0x06000EB4 RID: 3764
		public abstract MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06000EB5 RID: 3765
		public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers);

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00039620 File Offset: 0x00037820
		internal static Binder DefaultBinder
		{
			get
			{
				return Binder.default_binder;
			}
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00039628 File Offset: 0x00037828
		internal static bool ConvertArgs(Binder binder, object[] args, ParameterInfo[] pinfo, CultureInfo culture)
		{
			if (args == null)
			{
				if (pinfo.Length == 0)
				{
					return true;
				}
				throw new TargetParameterCountException();
			}
			else
			{
				if (pinfo.Length != args.Length)
				{
					throw new TargetParameterCountException();
				}
				for (int i = 0; i < args.Length; i++)
				{
					object obj = binder.ChangeType(args[i], pinfo[i].ParameterType, culture);
					if (obj == null && args[i] != null)
					{
						return false;
					}
					args[i] = obj;
				}
				return true;
			}
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00039698 File Offset: 0x00037898
		internal static int GetDerivedLevel(Type type)
		{
			Type type2 = type;
			int num = 1;
			while (type2.BaseType != null)
			{
				num++;
				type2 = type2.BaseType;
			}
			return num;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x000396C8 File Offset: 0x000378C8
		internal static MethodBase FindMostDerivedMatch(MethodBase[] match)
		{
			int num = 0;
			int num2 = -1;
			int num3 = match.Length;
			for (int i = 0; i < num3; i++)
			{
				MethodBase methodBase = match[i];
				int derivedLevel = Binder.GetDerivedLevel(methodBase.DeclaringType);
				if (derivedLevel == num)
				{
					throw new AmbiguousMatchException();
				}
				if (num2 >= 0)
				{
					ParameterInfo[] parameters = methodBase.GetParameters();
					ParameterInfo[] parameters2 = match[num2].GetParameters();
					bool flag = true;
					if (parameters.Length != parameters2.Length)
					{
						flag = false;
					}
					else
					{
						for (int j = 0; j < parameters.Length; j++)
						{
							if (parameters[j].ParameterType != parameters2[j].ParameterType)
							{
								flag = false;
								break;
							}
						}
					}
					if (!flag)
					{
						throw new AmbiguousMatchException();
					}
				}
				if (derivedLevel > num)
				{
					num = derivedLevel;
					num2 = i;
				}
			}
			return match[num2];
		}

		// Token: 0x0400060C RID: 1548
		private static Binder default_binder = new Binder.Default();

		// Token: 0x0200018C RID: 396
		internal sealed class Default : Binder
		{
			// Token: 0x06000EBB RID: 3771 RVA: 0x000397A4 File Offset: 0x000379A4
			public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state)
			{
				Type[] array;
				if (args == null)
				{
					array = Type.EmptyTypes;
				}
				else
				{
					array = new Type[args.Length];
					for (int i = 0; i < args.Length; i++)
					{
						if (args[i] != null)
						{
							array[i] = args[i].GetType();
						}
					}
				}
				MethodBase methodBase = this.SelectMethod(bindingAttr, match, array, modifiers, true);
				state = null;
				if (names != null)
				{
					this.ReorderParameters(names, ref args, methodBase);
				}
				return methodBase;
			}

			// Token: 0x06000EBC RID: 3772 RVA: 0x0003981C File Offset: 0x00037A1C
			private void ReorderParameters(string[] names, ref object[] args, MethodBase selected)
			{
				object[] array = new object[args.Length];
				Array.Copy(args, array, args.Length);
				ParameterInfo[] parameters = selected.GetParameters();
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < parameters.Length; j++)
					{
						if (names[i] == parameters[j].Name)
						{
							array[j] = args[i];
							break;
						}
					}
				}
				Array.Copy(array, args, args.Length);
			}

			// Token: 0x06000EBD RID: 3773 RVA: 0x0003989C File Offset: 0x00037A9C
			private static bool IsArrayAssignable(Type object_type, Type target_type)
			{
				if (object_type.IsArray && target_type.IsArray)
				{
					return Binder.Default.IsArrayAssignable(object_type.GetElementType(), target_type.GetElementType());
				}
				return target_type.IsAssignableFrom(object_type);
			}

			// Token: 0x06000EBE RID: 3774 RVA: 0x000398D8 File Offset: 0x00037AD8
			public override object ChangeType(object value, Type type, CultureInfo culture)
			{
				if (value == null)
				{
					return null;
				}
				Type type2 = value.GetType();
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				if (type2 == type || type.IsInstanceOfType(value))
				{
					return value;
				}
				if (type2.IsArray && type.IsArray && Binder.Default.IsArrayAssignable(type2.GetElementType(), type.GetElementType()))
				{
					return value;
				}
				if (!Binder.Default.check_type(type2, type))
				{
					return null;
				}
				if (type.IsEnum)
				{
					return Enum.ToObject(type, value);
				}
				if (type2 == typeof(char))
				{
					if (type == typeof(double))
					{
						return (double)((char)value);
					}
					if (type == typeof(float))
					{
						return (float)((char)value);
					}
				}
				if (type2 == typeof(IntPtr) && type.IsPointer)
				{
					return value;
				}
				return Convert.ChangeType(value, type);
			}

			// Token: 0x06000EBF RID: 3775 RVA: 0x000399DC File Offset: 0x00037BDC
			[MonoTODO("This method does not do anything in Mono")]
			public override void ReorderArgumentArray(ref object[] args, object state)
			{
			}

			// Token: 0x06000EC0 RID: 3776 RVA: 0x000399E0 File Offset: 0x00037BE0
			private static bool check_type(Type from, Type to)
			{
				if (from == to)
				{
					return true;
				}
				if (from == null)
				{
					return true;
				}
				if (to.IsByRef != from.IsByRef)
				{
					return false;
				}
				if (to.IsInterface)
				{
					return to.IsAssignableFrom(from);
				}
				if (to.IsEnum)
				{
					to = Enum.GetUnderlyingType(to);
					if (from == to)
					{
						return true;
					}
				}
				if (to.IsGenericType && to.GetGenericTypeDefinition() == typeof(Nullable<>) && to.GetGenericArguments()[0] == from)
				{
					return true;
				}
				TypeCode typeCode = Type.GetTypeCode(from);
				TypeCode typeCode2 = Type.GetTypeCode(to);
				switch (typeCode)
				{
				case TypeCode.Char:
					switch (typeCode2)
					{
					case TypeCode.UInt16:
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
					case TypeCode.Single:
					case TypeCode.Double:
						return true;
					default:
						return to == typeof(object);
					}
					break;
				case TypeCode.SByte:
					switch (typeCode2)
					{
					case TypeCode.Int16:
					case TypeCode.Int32:
					case TypeCode.Int64:
					case TypeCode.Single:
					case TypeCode.Double:
						return true;
					}
					return to == typeof(object) || (from.IsEnum && to == typeof(Enum));
				case TypeCode.Byte:
					switch (typeCode2)
					{
					case TypeCode.Char:
					case TypeCode.Int16:
					case TypeCode.UInt16:
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
					case TypeCode.Single:
					case TypeCode.Double:
						return true;
					}
					return to == typeof(object) || (from.IsEnum && to == typeof(Enum));
				case TypeCode.Int16:
					switch (typeCode2)
					{
					case TypeCode.Int32:
					case TypeCode.Int64:
					case TypeCode.Single:
					case TypeCode.Double:
						return true;
					}
					return to == typeof(object) || (from.IsEnum && to == typeof(Enum));
				case TypeCode.UInt16:
					switch (typeCode2)
					{
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
					case TypeCode.Single:
					case TypeCode.Double:
						return true;
					default:
						return to == typeof(object) || (from.IsEnum && to == typeof(Enum));
					}
					break;
				case TypeCode.Int32:
					switch (typeCode2)
					{
					case TypeCode.Int64:
					case TypeCode.Single:
					case TypeCode.Double:
						return true;
					}
					return to == typeof(object) || (from.IsEnum && to == typeof(Enum));
				case TypeCode.UInt32:
					switch (typeCode2)
					{
					case TypeCode.Int64:
					case TypeCode.UInt64:
					case TypeCode.Single:
					case TypeCode.Double:
						return true;
					default:
						return to == typeof(object) || (from.IsEnum && to == typeof(Enum));
					}
					break;
				case TypeCode.Int64:
				case TypeCode.UInt64:
				{
					TypeCode typeCode3 = typeCode2;
					return typeCode3 == TypeCode.Single || typeCode3 == TypeCode.Double || to == typeof(object) || (from.IsEnum && to == typeof(Enum));
				}
				case TypeCode.Single:
					return typeCode2 == TypeCode.Double || to == typeof(object);
				default:
					return (to == typeof(object) && from.IsValueType) || (to.IsPointer && from == typeof(IntPtr)) || to.IsAssignableFrom(from);
				}
			}

			// Token: 0x06000EC1 RID: 3777 RVA: 0x00039DB4 File Offset: 0x00037FB4
			private static bool check_arguments(Type[] types, ParameterInfo[] args, bool allowByRefMatch)
			{
				for (int i = 0; i < types.Length; i++)
				{
					bool flag = Binder.Default.check_type(types[i], args[i].ParameterType);
					if (!flag && allowByRefMatch)
					{
						Type parameterType = args[i].ParameterType;
						if (parameterType.IsByRef)
						{
							flag = Binder.Default.check_type(types[i], parameterType.GetElementType());
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000EC2 RID: 3778 RVA: 0x00039E20 File Offset: 0x00038020
			public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
			{
				return this.SelectMethod(bindingAttr, match, types, modifiers, false);
			}

			// Token: 0x06000EC3 RID: 3779 RVA: 0x00039E30 File Offset: 0x00038030
			private MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers, bool allowByRefMatch)
			{
				if (match == null)
				{
					throw new ArgumentNullException("match");
				}
				foreach (MethodBase methodBase in match)
				{
					ParameterInfo[] parameters = methodBase.GetParameters();
					if (parameters.Length == types.Length)
					{
						int j;
						for (j = 0; j < types.Length; j++)
						{
							if (types[j] != parameters[j].ParameterType)
							{
								break;
							}
						}
						if (j == types.Length)
						{
							return methodBase;
						}
					}
				}
				foreach (MethodBase methodBase in match)
				{
					ParameterInfo[] parameters2 = methodBase.GetParameters();
					if (parameters2.Length <= types.Length)
					{
						if (parameters2.Length != 0)
						{
							if (Attribute.IsDefined(parameters2[parameters2.Length - 1], typeof(ParamArrayAttribute)))
							{
								Type elementType = parameters2[parameters2.Length - 1].ParameterType.GetElementType();
								int j;
								for (j = 0; j < types.Length; j++)
								{
									if (j < parameters2.Length - 1 && types[j] != parameters2[j].ParameterType)
									{
										break;
									}
									if (j >= parameters2.Length - 1 && types[j] != elementType)
									{
										break;
									}
								}
								if (j == types.Length)
								{
									return methodBase;
								}
							}
						}
					}
				}
				if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
				{
					return null;
				}
				MethodBase methodBase2 = null;
				foreach (MethodBase methodBase in match)
				{
					ParameterInfo[] parameters3 = methodBase.GetParameters();
					if (parameters3.Length == types.Length)
					{
						if (Binder.Default.check_arguments(types, parameters3, allowByRefMatch))
						{
							if (methodBase2 != null)
							{
								methodBase2 = this.GetBetterMethod(methodBase2, methodBase, types);
							}
							else
							{
								methodBase2 = methodBase;
							}
						}
					}
				}
				return methodBase2;
			}

			// Token: 0x06000EC4 RID: 3780 RVA: 0x0003A000 File Offset: 0x00038200
			private MethodBase GetBetterMethod(MethodBase m1, MethodBase m2, Type[] types)
			{
				if (m1.IsGenericMethodDefinition && !m2.IsGenericMethodDefinition)
				{
					return m2;
				}
				if (m2.IsGenericMethodDefinition && !m1.IsGenericMethodDefinition)
				{
					return m1;
				}
				ParameterInfo[] parameters = m1.GetParameters();
				ParameterInfo[] parameters2 = m2.GetParameters();
				int num = 0;
				for (int i = 0; i < parameters.Length; i++)
				{
					int num2 = this.CompareCloserType(parameters[i].ParameterType, parameters2[i].ParameterType);
					if (num2 != 0 && num != 0 && num != num2)
					{
						throw new AmbiguousMatchException();
					}
					if (num2 != 0)
					{
						num = num2;
					}
				}
				if (num != 0)
				{
					return (num <= 0) ? m1 : m2;
				}
				Type declaringType = m1.DeclaringType;
				Type declaringType2 = m2.DeclaringType;
				if (declaringType != declaringType2)
				{
					if (declaringType.IsSubclassOf(declaringType2))
					{
						return m1;
					}
					if (declaringType2.IsSubclassOf(declaringType))
					{
						return m2;
					}
				}
				bool flag = (m1.CallingConvention & CallingConventions.VarArgs) != (CallingConventions)0;
				bool flag2 = (m2.CallingConvention & CallingConventions.VarArgs) != (CallingConventions)0;
				if (flag && !flag2)
				{
					return m2;
				}
				if (flag2 && !flag)
				{
					return m1;
				}
				throw new AmbiguousMatchException();
			}

			// Token: 0x06000EC5 RID: 3781 RVA: 0x0003A134 File Offset: 0x00038334
			private int CompareCloserType(Type t1, Type t2)
			{
				if (t1 == t2)
				{
					return 0;
				}
				if (t1.IsGenericParameter && !t2.IsGenericParameter)
				{
					return 1;
				}
				if (!t1.IsGenericParameter && t2.IsGenericParameter)
				{
					return -1;
				}
				if (t1.HasElementType && t2.HasElementType)
				{
					return this.CompareCloserType(t1.GetElementType(), t2.GetElementType());
				}
				if (t1.IsSubclassOf(t2))
				{
					return -1;
				}
				if (t2.IsSubclassOf(t1))
				{
					return 1;
				}
				if (t1.IsInterface && Array.IndexOf<Type>(t2.GetInterfaces(), t1) >= 0)
				{
					return 1;
				}
				if (t2.IsInterface && Array.IndexOf<Type>(t1.GetInterfaces(), t2) >= 0)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x06000EC6 RID: 3782 RVA: 0x0003A200 File Offset: 0x00038400
			public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers)
			{
				if (match == null || match.Length == 0)
				{
					throw new ArgumentException("No properties provided", "match");
				}
				bool flag = returnType != null;
				int num = (indexes == null) ? -1 : indexes.Length;
				PropertyInfo propertyInfo = null;
				int num2 = 2147483646;
				int num3 = int.MaxValue;
				int num4 = 0;
				for (int i = match.Length - 1; i >= 0; i--)
				{
					PropertyInfo propertyInfo2 = match[i];
					ParameterInfo[] indexParameters = propertyInfo2.GetIndexParameters();
					if (num < 0 || num == indexParameters.Length)
					{
						if (!flag || propertyInfo2.PropertyType == returnType)
						{
							int num5 = 2147483646;
							if (num > 0)
							{
								num5 = Binder.Default.check_arguments_with_score(indexes, indexParameters);
								if (num5 == -1)
								{
									goto IL_10E;
								}
							}
							int derivedLevel = Binder.GetDerivedLevel(propertyInfo2.DeclaringType);
							if (propertyInfo != null)
							{
								if (num2 < num5)
								{
									goto IL_10E;
								}
								if (num2 == num5)
								{
									if (num4 == derivedLevel)
									{
										num3 = num5;
										goto IL_10E;
									}
									if (num4 > derivedLevel)
									{
										goto IL_10E;
									}
								}
							}
							propertyInfo = propertyInfo2;
							num2 = num5;
							num4 = derivedLevel;
						}
					}
					IL_10E:;
				}
				if (num3 <= num2)
				{
					throw new AmbiguousMatchException();
				}
				return propertyInfo;
			}

			// Token: 0x06000EC7 RID: 3783 RVA: 0x0003A338 File Offset: 0x00038538
			private static int check_arguments_with_score(Type[] types, ParameterInfo[] args)
			{
				int num = -1;
				for (int i = 0; i < types.Length; i++)
				{
					int num2 = Binder.Default.check_type_with_score(types[i], args[i].ParameterType);
					if (num2 == -1)
					{
						return -1;
					}
					if (num < num2)
					{
						num = num2;
					}
				}
				return num;
			}

			// Token: 0x06000EC8 RID: 3784 RVA: 0x0003A380 File Offset: 0x00038580
			private static int check_type_with_score(Type from, Type to)
			{
				if (from == null)
				{
					return (!to.IsValueType) ? 0 : -1;
				}
				if (from == to)
				{
					return 0;
				}
				if (to == typeof(object))
				{
					return 4;
				}
				TypeCode typeCode = Type.GetTypeCode(from);
				TypeCode typeCode2 = Type.GetTypeCode(to);
				switch (typeCode)
				{
				case TypeCode.Char:
					switch (typeCode2)
					{
					case TypeCode.UInt16:
						return 0;
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
					case TypeCode.Single:
					case TypeCode.Double:
						return 2;
					default:
						return -1;
					}
					break;
				case TypeCode.SByte:
					switch (typeCode2)
					{
					case TypeCode.Int16:
					case TypeCode.Int32:
					case TypeCode.Int64:
					case TypeCode.Single:
					case TypeCode.Double:
						return 2;
					}
					return (!from.IsEnum || to != typeof(Enum)) ? -1 : 1;
				case TypeCode.Byte:
					switch (typeCode2)
					{
					case TypeCode.Char:
					case TypeCode.Int16:
					case TypeCode.UInt16:
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
					case TypeCode.Single:
					case TypeCode.Double:
						return 2;
					}
					return (!from.IsEnum || to != typeof(Enum)) ? -1 : 1;
				case TypeCode.Int16:
					switch (typeCode2)
					{
					case TypeCode.Int32:
					case TypeCode.Int64:
					case TypeCode.Single:
					case TypeCode.Double:
						return 2;
					}
					return (!from.IsEnum || to != typeof(Enum)) ? -1 : 1;
				case TypeCode.UInt16:
					switch (typeCode2)
					{
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
					case TypeCode.Single:
					case TypeCode.Double:
						return 2;
					default:
						return (!from.IsEnum || to != typeof(Enum)) ? -1 : 1;
					}
					break;
				case TypeCode.Int32:
					switch (typeCode2)
					{
					case TypeCode.Int64:
					case TypeCode.Single:
					case TypeCode.Double:
						return 2;
					}
					return (!from.IsEnum || to != typeof(Enum)) ? -1 : 1;
				case TypeCode.UInt32:
					switch (typeCode2)
					{
					case TypeCode.Int64:
					case TypeCode.UInt64:
					case TypeCode.Single:
					case TypeCode.Double:
						return 2;
					default:
						return (!from.IsEnum || to != typeof(Enum)) ? -1 : 1;
					}
					break;
				case TypeCode.Int64:
				case TypeCode.UInt64:
				{
					TypeCode typeCode3 = typeCode2;
					if (typeCode3 != TypeCode.Single && typeCode3 != TypeCode.Double)
					{
						return (!from.IsEnum || to != typeof(Enum)) ? -1 : 1;
					}
					return 2;
				}
				case TypeCode.Single:
					return (typeCode2 != TypeCode.Double) ? -1 : 2;
				default:
					return (!to.IsAssignableFrom(from)) ? -1 : 3;
				}
			}
		}
	}
}
