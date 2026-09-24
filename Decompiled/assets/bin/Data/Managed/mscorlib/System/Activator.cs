using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Security.Policy;
using System.Text;

namespace System
{
	// Token: 0x02000054 RID: 84
	[ComDefaultInterface(typeof(_Activator))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class Activator : _Activator
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, null);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes, null);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, securityInfo);
			if (assembly == null)
			{
				return null;
			}
			Type type = assembly.GetType(typeName, true, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			return (obj == null) ? null : new ObjectHandle(obj);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000CB38 File Offset: 0x0000AD38
		public static ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			if (assemblyName == null)
			{
				assemblyName = Assembly.GetCallingAssembly().GetName().Name;
			}
			return Activator.CreateInstance(assemblyName, typeName, null);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				assemblyName = Assembly.GetCallingAssembly().GetName().Name;
			}
			return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes, null);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
		{
			Assembly assembly;
			if (assemblyName == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			else
			{
				assembly = Assembly.Load(assemblyName, securityInfo);
			}
			Type type = assembly.GetType(typeName, true, ignoreCase);
			object obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			return (obj == null) ? null : new ObjectHandle(obj);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		public static T CreateInstance<T>()
		{
			return (T)((object)Activator.CreateInstance(typeof(T)));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000CC04 File Offset: 0x0000AE04
		public static object CreateInstance(Type type)
		{
			return Activator.CreateInstance(type, false);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000CC10 File Offset: 0x0000AE10
		public static object CreateInstance(Type type, params object[] args)
		{
			return Activator.CreateInstance(type, args, new object[0]);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000CC20 File Offset: 0x0000AE20
		public static object CreateInstance(Type type, object[] args, object[] activationAttributes)
		{
			return Activator.CreateInstance(type, BindingFlags.Default, Binder.DefaultBinder, args, null, activationAttributes);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000CC34 File Offset: 0x0000AE34
		public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Activator.CheckType(type);
			if (type.ContainsGenericParameters)
			{
				throw new ArgumentException(type + " is an open generic type", "type");
			}
			if ((bindingAttr & (BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)) == BindingFlags.Default)
			{
				bindingAttr |= (BindingFlags.Instance | BindingFlags.Public);
			}
			int num = 0;
			if (args != null)
			{
				num = args.Length;
			}
			Type[] array = (num != 0) ? new Type[num] : Type.EmptyTypes;
			for (int i = 0; i < num; i++)
			{
				if (args[i] != null)
				{
					array[i] = args[i].GetType();
				}
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			ConstructorInfo constructorInfo = (ConstructorInfo)binder.SelectMethod(bindingAttr, type.GetConstructors(bindingAttr), array, null);
			if (constructorInfo != null)
			{
				Activator.CheckAbstractType(type);
				if (activationAttributes != null && activationAttributes.Length > 0)
				{
					if (!type.IsMarshalByRef)
					{
						string text = Locale.GetText("Type '{0}' doesn't derive from MarshalByRefObject.", new object[]
						{
							type.FullName
						});
						throw new NotSupportedException(text);
					}
					object obj = ActivationServices.CreateProxyFromAttributes(type, activationAttributes);
					if (obj != null)
					{
						constructorInfo.Invoke(obj, bindingAttr, binder, args, culture);
						return obj;
					}
				}
				return constructorInfo.Invoke(bindingAttr, binder, args, culture);
			}
			if (type.IsValueType && array.Length == 0)
			{
				return Activator.CreateInstanceInternal(type);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Type type2 in array)
			{
				stringBuilder.Append((type2 == null) ? "(unknown)" : type2.ToString());
				stringBuilder.Append(", ");
			}
			if (stringBuilder.Length > 2)
			{
				stringBuilder.Length -= 2;
			}
			throw new MissingMethodException(string.Format(Locale.GetText("No constructor found for {0}::.ctor({1})"), type.FullName, stringBuilder));
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000CE04 File Offset: 0x0000B004
		public static object CreateInstance(Type type, bool nonPublic)
		{
			Activator.CheckType(type);
			if (type.ContainsGenericParameters)
			{
				throw new ArgumentException(type + " is an open generic type", "type");
			}
			Activator.CheckAbstractType(type);
			MonoType monoType = type as MonoType;
			ConstructorInfo constructorInfo;
			if (monoType != null)
			{
				constructorInfo = monoType.GetDefaultConstructor();
				if (!nonPublic && constructorInfo != null && !constructorInfo.IsPublic)
				{
					constructorInfo = null;
				}
			}
			else
			{
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
				if (nonPublic)
				{
					bindingFlags |= BindingFlags.NonPublic;
				}
				constructorInfo = type.GetConstructor(bindingFlags, null, CallingConventions.Any, Type.EmptyTypes, null);
			}
			if (constructorInfo != null)
			{
				return constructorInfo.Invoke(null);
			}
			if (type.IsValueType)
			{
				return Activator.CreateInstanceInternal(type);
			}
			throw new MissingMethodException(Locale.GetText("Default constructor not found."), ".ctor() of " + type.FullName);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000CED0 File Offset: 0x0000B0D0
		private static void CheckType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type == typeof(TypedReference) || type == typeof(ArgIterator) || type == typeof(void) || type == typeof(RuntimeArgumentHandle))
			{
				string text = Locale.GetText("CreateInstance cannot be used to create this type ({0}).", new object[]
				{
					type.FullName
				});
				throw new NotSupportedException(text);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000CF50 File Offset: 0x0000B150
		private static void CheckAbstractType(Type type)
		{
			if (type.IsAbstract)
			{
				string text = Locale.GetText("Cannot create an abstract class '{0}'.", new object[]
				{
					type.FullName
				});
				throw new MissingMethodException(text);
			}
		}

		// Token: 0x06000198 RID: 408
		[MethodImpl(4096)]
		internal static extern object CreateInstanceInternal(Type type);

		// Token: 0x0400014D RID: 333
		private const BindingFlags _flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;

		// Token: 0x0400014E RID: 334
		private const BindingFlags _accessFlags = BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
	}
}
