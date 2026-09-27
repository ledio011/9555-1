using System;
using System.Collections.Generic;
using System.Reflection;
using Boo.Lang.Runtime.DynamicDispatching;
using Boo.Lang.Runtime.DynamicDispatching.Emitters;

namespace Boo.Lang.Runtime
{
	// Token: 0x02000011 RID: 17
	public class RuntimeServices
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002B64 File Offset: 0x00000D64
		private static Dispatcher GetDispatcher(object target, string cacheKeyName, Type[] cacheKeyTypes, DispatcherCache.DispatcherFactory factory)
		{
			Type type = (target as Type) ?? target.GetType();
			DispatcherKey key = new DispatcherKey(type, cacheKeyName, cacheKeyTypes);
			return RuntimeServices._cache.Get(key, factory);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B9C File Offset: 0x00000D9C
		public static object Coerce(object value, Type toType)
		{
			if (value == null)
			{
				return null;
			}
			object[] args = new object[]
			{
				toType
			};
			Dispatcher dispatcher = RuntimeServices.GetDispatcher(value, "$Coerce$", new Type[]
			{
				toType
			}, () => RuntimeServices.CreateCoerceDispatcher(value, toType));
			return dispatcher(value, args);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C14 File Offset: 0x00000E14
		private static Dispatcher CreateCoerceDispatcher(object value, Type toType)
		{
			if (toType.IsInstanceOfType(value))
			{
				return new Dispatcher(RuntimeServices.IdentityDispatcher);
			}
			if (value is ICoercible)
			{
				return new Dispatcher(RuntimeServices.CoercibleDispatcher);
			}
			Type type = value.GetType();
			if (RuntimeServices.IsPromotableNumeric(type) && RuntimeServices.IsPromotableNumeric(toType))
			{
				return RuntimeServices.EmitPromotionDispatcher(type, toType);
			}
			MethodInfo methodInfo = RuntimeServices.FindImplicitConversionOperator(type, toType);
			if (methodInfo == null)
			{
				return new Dispatcher(RuntimeServices.IdentityDispatcher);
			}
			return RuntimeServices.EmitImplicitConversionDispatcher(methodInfo);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002C98 File Offset: 0x00000E98
		private static Dispatcher EmitPromotionDispatcher(Type fromType, Type toType)
		{
			return (Dispatcher)Delegate.CreateDelegate(typeof(Dispatcher), typeof(NumericPromotions).GetMethod(string.Concat(new object[]
			{
				"From",
				Type.GetTypeCode(fromType),
				"To",
				Type.GetTypeCode(toType)
			})));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D00 File Offset: 0x00000F00
		private static bool IsPromotableNumeric(Type fromType)
		{
			return RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(fromType));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D10 File Offset: 0x00000F10
		private static Dispatcher EmitImplicitConversionDispatcher(MethodInfo method)
		{
			return new ImplicitConversionEmitter(method).Emit();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D20 File Offset: 0x00000F20
		private static object CoercibleDispatcher(object o, object[] args)
		{
			return ((ICoercible)o).Coerce((Type)args[0]);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D38 File Offset: 0x00000F38
		private static object IdentityDispatcher(object o, object[] args)
		{
			return o;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002D3C File Offset: 0x00000F3C
		private static bool IsNumeric(TypeCode code)
		{
			switch (code)
			{
			case TypeCode.SByte:
				return true;
			case TypeCode.Byte:
				return true;
			case TypeCode.Int16:
				return true;
			case TypeCode.UInt16:
				return true;
			case TypeCode.Int32:
				return true;
			case TypeCode.UInt32:
				return true;
			case TypeCode.Int64:
				return true;
			case TypeCode.UInt64:
				return true;
			case TypeCode.Single:
				return true;
			case TypeCode.Double:
				return true;
			case TypeCode.Decimal:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002D9C File Offset: 0x00000F9C
		public static string op_Addition(string lhs, string rhs)
		{
			return lhs + rhs;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public static bool EqualityOperator(object lhs, object rhs)
		{
			if (lhs == rhs)
			{
				return true;
			}
			if (lhs == null)
			{
				return rhs.Equals(lhs);
			}
			if (rhs == null)
			{
				return lhs.Equals(rhs);
			}
			TypeCode typeCode = Type.GetTypeCode(lhs.GetType());
			TypeCode typeCode2 = Type.GetTypeCode(rhs.GetType());
			if (RuntimeServices.IsNumeric(typeCode) && RuntimeServices.IsNumeric(typeCode2))
			{
				return RuntimeServices.EqualityOperator(lhs, typeCode, rhs, typeCode2);
			}
			Array array = lhs as Array;
			if (array != null)
			{
				Array array2 = rhs as Array;
				if (array2 != null)
				{
					return RuntimeServices.ArrayEqualityImpl(array, array2);
				}
			}
			return lhs.Equals(rhs) || rhs.Equals(lhs);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E4C File Offset: 0x0000104C
		private static bool ArrayEqualityImpl(Array lhs, Array rhs)
		{
			if (lhs.Rank != 1 || rhs.Rank != 1)
			{
				throw new ArgumentException("array rank must be 1");
			}
			if (lhs.Length != rhs.Length)
			{
				return false;
			}
			for (int i = 0; i < lhs.Length; i++)
			{
				if (!RuntimeServices.EqualityOperator(lhs.GetValue(i), rhs.GetValue(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002EC4 File Offset: 0x000010C4
		private static TypeCode GetConvertTypeCode(TypeCode lhsTypeCode, TypeCode rhsTypeCode)
		{
			if (lhsTypeCode == TypeCode.Decimal || rhsTypeCode == TypeCode.Decimal)
			{
				return TypeCode.Decimal;
			}
			if (lhsTypeCode == TypeCode.Double || rhsTypeCode == TypeCode.Double)
			{
				return TypeCode.Double;
			}
			if (lhsTypeCode == TypeCode.Single || rhsTypeCode == TypeCode.Single)
			{
				return TypeCode.Single;
			}
			if (lhsTypeCode == TypeCode.UInt64)
			{
				if (rhsTypeCode == TypeCode.SByte || rhsTypeCode == TypeCode.Int16 || rhsTypeCode == TypeCode.Int32 || rhsTypeCode == TypeCode.Int64)
				{
					return TypeCode.Int64;
				}
				return TypeCode.UInt64;
			}
			else if (rhsTypeCode == TypeCode.UInt64)
			{
				if (lhsTypeCode == TypeCode.SByte || lhsTypeCode == TypeCode.Int16 || lhsTypeCode == TypeCode.Int32 || lhsTypeCode == TypeCode.Int64)
				{
					return TypeCode.Int64;
				}
				return TypeCode.UInt64;
			}
			else
			{
				if (lhsTypeCode == TypeCode.Int64 || rhsTypeCode == TypeCode.Int64)
				{
					return TypeCode.Int64;
				}
				if (lhsTypeCode == TypeCode.UInt32)
				{
					if (rhsTypeCode == TypeCode.SByte || rhsTypeCode == TypeCode.Int16 || rhsTypeCode == TypeCode.Int32)
					{
						return TypeCode.Int64;
					}
					return TypeCode.UInt32;
				}
				else
				{
					if (rhsTypeCode != TypeCode.UInt32)
					{
						return TypeCode.Int32;
					}
					if (lhsTypeCode == TypeCode.SByte || lhsTypeCode == TypeCode.Int16 || lhsTypeCode == TypeCode.Int32)
					{
						return TypeCode.Int64;
					}
					return TypeCode.UInt32;
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002FC0 File Offset: 0x000011C0
		private static bool EqualityOperator(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case TypeCode.UInt32:
				return convertible.ToUInt32(null) == convertible2.ToUInt32(null);
			case TypeCode.Int64:
				return convertible.ToInt64(null) == convertible2.ToInt64(null);
			case TypeCode.UInt64:
				return convertible.ToUInt64(null) == convertible2.ToUInt64(null);
			case TypeCode.Single:
				return convertible.ToSingle(null) == convertible2.ToSingle(null);
			case TypeCode.Double:
				return convertible.ToDouble(null) == convertible2.ToDouble(null);
			case TypeCode.Decimal:
				return convertible.ToDecimal(null) == convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) == convertible2.ToInt32(null);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003084 File Offset: 0x00001284
		internal static bool IsPromotableNumeric(TypeCode code)
		{
			switch (code)
			{
			case TypeCode.Boolean:
				return true;
			case TypeCode.Char:
				return true;
			case TypeCode.SByte:
				return true;
			case TypeCode.Byte:
				return true;
			case TypeCode.Int16:
				return true;
			case TypeCode.UInt16:
				return true;
			case TypeCode.Int32:
				return true;
			case TypeCode.UInt32:
				return true;
			case TypeCode.Int64:
				return true;
			case TypeCode.UInt64:
				return true;
			case TypeCode.Single:
				return true;
			case TypeCode.Double:
				return true;
			case TypeCode.Decimal:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000030F0 File Offset: 0x000012F0
		internal static MethodInfo FindImplicitConversionOperator(Type from, Type to)
		{
			MethodInfo result;
			if ((result = RuntimeServices.FindImplicitConversionMethod(from.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy), from, to)) == null)
			{
				result = (RuntimeServices.FindImplicitConversionMethod(to.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy), from, to) ?? RuntimeServices.FindImplicitConversionMethod(RuntimeServices.GetExtensionMethods(), from, to));
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003138 File Offset: 0x00001338
		private static IEnumerable<MethodInfo> GetExtensionMethods()
		{
			foreach (MemberInfo member in RuntimeServices._extensions.Extensions)
			{
				if (member.MemberType == MemberTypes.Method)
				{
					yield return (MethodInfo)member;
				}
			}
			yield break;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003154 File Offset: 0x00001354
		private static MethodInfo FindImplicitConversionMethod(IEnumerable<MethodInfo> candidates, Type from, Type to)
		{
			foreach (MethodInfo methodInfo in candidates)
			{
				if (!(methodInfo.Name != "op_Implicit"))
				{
					if (methodInfo.ReturnType == to)
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (parameters.Length == 1)
						{
							if (parameters[0].ParameterType.IsAssignableFrom(from))
							{
								return methodInfo;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04000016 RID: 22
		internal const BindingFlags InstanceMemberFlags = default(BindingFlags);

		// Token: 0x04000017 RID: 23
		internal const BindingFlags DefaultBindingFlags = default(BindingFlags);

		// Token: 0x04000018 RID: 24
		private const BindingFlags InvokeBindingFlags = default(BindingFlags);

		// Token: 0x04000019 RID: 25
		private const BindingFlags SetPropertyBindingFlags = default(BindingFlags);

		// Token: 0x0400001A RID: 26
		private const BindingFlags GetPropertyBindingFlags = default(BindingFlags);

		// Token: 0x0400001B RID: 27
		private static readonly object[] NoArguments = new object[0];

		// Token: 0x0400001C RID: 28
		private static readonly Type RuntimeServicesType = typeof(RuntimeServices);

		// Token: 0x0400001D RID: 29
		private static readonly DispatcherCache _cache = new DispatcherCache();

		// Token: 0x0400001E RID: 30
		private static readonly ExtensionRegistry _extensions = new ExtensionRegistry();

		// Token: 0x0400001F RID: 31
		private static readonly object True = true;
	}
}
