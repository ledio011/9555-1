using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000154 RID: 340
	internal class MonoCustomAttrs
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x00032344 File Offset: 0x00030544
		private static bool IsUserCattrProvider(object obj)
		{
			Type type = obj as Type;
			if (type is MonoType || type is TypeBuilder)
			{
				return false;
			}
			if (obj is Type)
			{
				return true;
			}
			if (MonoCustomAttrs.corlib == null)
			{
				MonoCustomAttrs.corlib = typeof(int).Assembly;
			}
			return obj.GetType().Assembly != MonoCustomAttrs.corlib;
		}

		// Token: 0x06000D04 RID: 3332
		[MethodImpl(4096)]
		internal static extern object[] GetCustomAttributesInternal(ICustomAttributeProvider obj, Type attributeType, bool pseudoAttrs);

		// Token: 0x06000D05 RID: 3333 RVA: 0x000323B0 File Offset: 0x000305B0
		internal static object[] GetPseudoCustomAttributes(ICustomAttributeProvider obj, Type attributeType)
		{
			object[] array = null;
			if (obj is MonoMethod)
			{
				array = ((MonoMethod)obj).GetPseudoCustomAttributes();
			}
			else if (obj is FieldInfo)
			{
				array = ((FieldInfo)obj).GetPseudoCustomAttributes();
			}
			else if (obj is ParameterInfo)
			{
				array = ((ParameterInfo)obj).GetPseudoCustomAttributes();
			}
			else if (obj is Type)
			{
				array = ((Type)obj).GetPseudoCustomAttributes();
			}
			if (attributeType != null && array != null)
			{
				int i = 0;
				while (i < array.Length)
				{
					if (attributeType.IsAssignableFrom(array[i].GetType()))
					{
						if (array.Length == 1)
						{
							return array;
						}
						return new object[]
						{
							array[i]
						};
					}
					else
					{
						i++;
					}
				}
				return new object[0];
			}
			return array;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00032480 File Offset: 0x00030680
		internal static object[] GetCustomAttributesBase(ICustomAttributeProvider obj, Type attributeType)
		{
			object[] array;
			if (MonoCustomAttrs.IsUserCattrProvider(obj))
			{
				array = obj.GetCustomAttributes(attributeType, true);
			}
			else
			{
				array = MonoCustomAttrs.GetCustomAttributesInternal(obj, attributeType, false);
			}
			object[] pseudoCustomAttributes = MonoCustomAttrs.GetPseudoCustomAttributes(obj, attributeType);
			if (pseudoCustomAttributes != null)
			{
				object[] array2 = new object[array.Length + pseudoCustomAttributes.Length];
				Array.Copy(array, array2, array.Length);
				Array.Copy(pseudoCustomAttributes, 0, array2, array.Length, pseudoCustomAttributes.Length);
				return array2;
			}
			return array;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000324E8 File Offset: 0x000306E8
		internal static Attribute GetCustomAttribute(ICustomAttributeProvider obj, Type attributeType, bool inherit)
		{
			object[] customAttributes = MonoCustomAttrs.GetCustomAttributes(obj, attributeType, inherit);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length > 1)
			{
				string text = "'{0}' has more than one attribute of type '{1}";
				text = string.Format(text, obj, attributeType);
				throw new AmbiguousMatchException(text);
			}
			return (Attribute)customAttributes[0];
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00032530 File Offset: 0x00030730
		internal static object[] GetCustomAttributes(ICustomAttributeProvider obj, Type attributeType, bool inherit)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (attributeType == typeof(MonoCustomAttrs))
			{
				attributeType = null;
			}
			object[] customAttributesBase = MonoCustomAttrs.GetCustomAttributesBase(obj, attributeType);
			if (!inherit && customAttributesBase.Length == 1)
			{
				object[] array;
				if (attributeType != null)
				{
					if (attributeType.IsAssignableFrom(customAttributesBase[0].GetType()))
					{
						array = (object[])Array.CreateInstance(attributeType, 1);
						array[0] = customAttributesBase[0];
					}
					else
					{
						array = (object[])Array.CreateInstance(attributeType, 0);
					}
				}
				else
				{
					array = (object[])Array.CreateInstance(customAttributesBase[0].GetType(), 1);
					array[0] = customAttributesBase[0];
				}
				return array;
			}
			if (attributeType != null && attributeType.IsSealed && inherit)
			{
				AttributeUsageAttribute attributeUsageAttribute = MonoCustomAttrs.RetrieveAttributeUsage(attributeType);
				if (!attributeUsageAttribute.Inherited)
				{
					inherit = false;
				}
			}
			int capacity = (customAttributesBase.Length >= 16) ? 16 : customAttributesBase.Length;
			Hashtable hashtable = new Hashtable(capacity);
			ArrayList arrayList = new ArrayList(capacity);
			ICustomAttributeProvider customAttributeProvider = obj;
			int num = 0;
			do
			{
				foreach (object obj2 in customAttributesBase)
				{
					Type type = obj2.GetType();
					if (attributeType == null || attributeType.IsAssignableFrom(type))
					{
						MonoCustomAttrs.AttributeInfo attributeInfo = (MonoCustomAttrs.AttributeInfo)hashtable[type];
						AttributeUsageAttribute attributeUsageAttribute2;
						if (attributeInfo != null)
						{
							attributeUsageAttribute2 = attributeInfo.Usage;
						}
						else
						{
							attributeUsageAttribute2 = MonoCustomAttrs.RetrieveAttributeUsage(type);
						}
						if ((num == 0 || attributeUsageAttribute2.Inherited) && (attributeUsageAttribute2.AllowMultiple || attributeInfo == null || (attributeInfo != null && attributeInfo.InheritanceLevel == num)))
						{
							arrayList.Add(obj2);
						}
						if (attributeInfo == null)
						{
							hashtable.Add(type, new MonoCustomAttrs.AttributeInfo(attributeUsageAttribute2, num));
						}
					}
				}
				if ((customAttributeProvider = MonoCustomAttrs.GetBase(customAttributeProvider)) != null)
				{
					num++;
					customAttributesBase = MonoCustomAttrs.GetCustomAttributesBase(customAttributeProvider, attributeType);
				}
			}
			while (inherit && customAttributeProvider != null);
			object[] array3;
			if (attributeType == null || attributeType.IsValueType)
			{
				array3 = (object[])Array.CreateInstance(typeof(Attribute), arrayList.Count);
			}
			else
			{
				array3 = (Array.CreateInstance(attributeType, arrayList.Count) as object[]);
			}
			arrayList.CopyTo(array3, 0);
			return array3;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00032790 File Offset: 0x00030990
		internal static object[] GetCustomAttributes(ICustomAttributeProvider obj, bool inherit)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (!inherit)
			{
				return (object[])MonoCustomAttrs.GetCustomAttributesBase(obj, null).Clone();
			}
			return MonoCustomAttrs.GetCustomAttributes(obj, typeof(MonoCustomAttrs), inherit);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000327CC File Offset: 0x000309CC
		internal static bool IsDefined(ICustomAttributeProvider obj, Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (MonoCustomAttrs.IsUserCattrProvider(obj))
			{
				return obj.IsDefined(attributeType, inherit);
			}
			if (MonoCustomAttrs.IsDefinedInternal(obj, attributeType))
			{
				return true;
			}
			object[] pseudoCustomAttributes = MonoCustomAttrs.GetPseudoCustomAttributes(obj, attributeType);
			if (pseudoCustomAttributes != null)
			{
				for (int i = 0; i < pseudoCustomAttributes.Length; i++)
				{
					if (attributeType.IsAssignableFrom(pseudoCustomAttributes[i].GetType()))
					{
						return true;
					}
				}
			}
			ICustomAttributeProvider @base;
			return inherit && (@base = MonoCustomAttrs.GetBase(obj)) != null && MonoCustomAttrs.IsDefined(@base, attributeType, inherit);
		}

		// Token: 0x06000D0B RID: 3339
		[MethodImpl(4096)]
		internal static extern bool IsDefinedInternal(ICustomAttributeProvider obj, Type AttributeType);

		// Token: 0x06000D0C RID: 3340 RVA: 0x00032860 File Offset: 0x00030A60
		private static PropertyInfo GetBasePropertyDefinition(PropertyInfo property)
		{
			MethodInfo methodInfo = property.GetGetMethod(true);
			if (methodInfo == null || !methodInfo.IsVirtual)
			{
				methodInfo = property.GetSetMethod(true);
			}
			if (methodInfo == null || !methodInfo.IsVirtual)
			{
				return null;
			}
			MethodInfo baseDefinition = methodInfo.GetBaseDefinition();
			if (baseDefinition == null || baseDefinition == methodInfo)
			{
				return null;
			}
			ParameterInfo[] indexParameters = property.GetIndexParameters();
			if (indexParameters != null && indexParameters.Length > 0)
			{
				Type[] array = new Type[indexParameters.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = indexParameters[i].ParameterType;
				}
				return baseDefinition.DeclaringType.GetProperty(property.Name, property.PropertyType, array);
			}
			return baseDefinition.DeclaringType.GetProperty(property.Name, property.PropertyType);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003292C File Offset: 0x00030B2C
		private static ICustomAttributeProvider GetBase(ICustomAttributeProvider obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is Type)
			{
				return ((Type)obj).BaseType;
			}
			MethodInfo methodInfo = null;
			if (obj is MonoProperty)
			{
				return MonoCustomAttrs.GetBasePropertyDefinition((MonoProperty)obj);
			}
			if (obj is MonoMethod)
			{
				methodInfo = (MethodInfo)obj;
			}
			if (methodInfo == null || !methodInfo.IsVirtual)
			{
				return null;
			}
			MethodInfo baseDefinition = methodInfo.GetBaseDefinition();
			if (baseDefinition == methodInfo)
			{
				return null;
			}
			return baseDefinition;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000329A8 File Offset: 0x00030BA8
		private static AttributeUsageAttribute RetrieveAttributeUsage(Type attributeType)
		{
			if (attributeType == typeof(AttributeUsageAttribute))
			{
				return new AttributeUsageAttribute(AttributeTargets.Class);
			}
			AttributeUsageAttribute attributeUsageAttribute = null;
			object[] customAttributes = MonoCustomAttrs.GetCustomAttributes(attributeType, MonoCustomAttrs.AttributeUsageType, false);
			if (customAttributes.Length == 0)
			{
				if (attributeType.BaseType != null)
				{
					attributeUsageAttribute = MonoCustomAttrs.RetrieveAttributeUsage(attributeType.BaseType);
				}
				if (attributeUsageAttribute != null)
				{
					return attributeUsageAttribute;
				}
				return MonoCustomAttrs.DefaultAttributeUsage;
			}
			else
			{
				if (customAttributes.Length > 1)
				{
					throw new FormatException("Duplicate AttributeUsageAttribute cannot be specified on an attribute type.");
				}
				return (AttributeUsageAttribute)customAttributes[0];
			}
		}

		// Token: 0x04000568 RID: 1384
		private static Assembly corlib;

		// Token: 0x04000569 RID: 1385
		private static readonly Type AttributeUsageType = typeof(AttributeUsageAttribute);

		// Token: 0x0400056A RID: 1386
		private static readonly AttributeUsageAttribute DefaultAttributeUsage = new AttributeUsageAttribute(AttributeTargets.All);

		// Token: 0x02000155 RID: 341
		private class AttributeInfo
		{
			// Token: 0x06000D0F RID: 3343 RVA: 0x00032A24 File Offset: 0x00030C24
			public AttributeInfo(AttributeUsageAttribute usage, int inheritanceLevel)
			{
				this._usage = usage;
				this._inheritanceLevel = inheritanceLevel;
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00032A3C File Offset: 0x00030C3C
			public AttributeUsageAttribute Usage
			{
				get
				{
					return this._usage;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00032A44 File Offset: 0x00030C44
			public int InheritanceLevel
			{
				get
				{
					return this._inheritanceLevel;
				}
			}

			// Token: 0x0400056B RID: 1387
			private AttributeUsageAttribute _usage;

			// Token: 0x0400056C RID: 1388
			private int _inheritanceLevel;
		}
	}
}
