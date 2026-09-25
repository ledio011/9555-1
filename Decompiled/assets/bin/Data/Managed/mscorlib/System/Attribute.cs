using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200006C RID: 108
	[ComDefaultInterface(typeof(_Attribute))]
	[ClassInterface(ClassInterfaceType.None)]
	[AttributeUsage(AttributeTargets.All)]
	[ComVisible(true)]
	[Serializable]
	public abstract class Attribute : _Attribute
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		private static void CheckParameters(object element, Type attributeType)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!typeof(Attribute).IsAssignableFrom(attributeType))
			{
				throw new ArgumentException(Locale.GetText("Type is not derived from System.Attribute."), "attributeType");
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00011B10 File Offset: 0x0000FD10
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00011B1C File Offset: 0x0000FD1C
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType, bool inherit)
		{
			Attribute.CheckParameters(element, attributeType);
			return MonoCustomAttrs.GetCustomAttribute(element, attributeType, inherit);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00011B30 File Offset: 0x0000FD30
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00011B38 File Offset: 0x0000FD38
		public static bool IsDefined(ParameterInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00011B44 File Offset: 0x0000FD44
		public static bool IsDefined(MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00011B50 File Offset: 0x0000FD50
		public static bool IsDefined(MemberInfo element, Type attributeType, bool inherit)
		{
			Attribute.CheckParameters(element, attributeType);
			MemberTypes memberType = element.MemberType;
			if (memberType != MemberTypes.Constructor && memberType != MemberTypes.Event && memberType != MemberTypes.Field && memberType != MemberTypes.Method && memberType != MemberTypes.Property && memberType != MemberTypes.TypeInfo && memberType != MemberTypes.NestedType)
			{
				throw new NotSupportedException(Locale.GetText("Element is not a constructor, method, property, event, type or field."));
			}
			if (memberType == MemberTypes.Property)
			{
				return MonoCustomAttrs.IsDefined(element, attributeType, inherit);
			}
			return element.IsDefined(attributeType, inherit);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public static bool IsDefined(ParameterInfo element, Type attributeType, bool inherit)
		{
			Attribute.CheckParameters(element, attributeType);
			return element.IsDefined(attributeType, inherit) || Attribute.IsDefined(element.Member, attributeType, inherit);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		public override bool Equals(object obj)
		{
			return obj != null && obj is Attribute && ValueType.DefaultEquals(this, obj);
		}
	}
}
