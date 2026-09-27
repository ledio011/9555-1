using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Serialization
{
	// Token: 0x020002FE RID: 766
	[ComVisible(true)]
	public sealed class FormatterServices
	{
		// Token: 0x060017B9 RID: 6073 RVA: 0x00056810 File Offset: 0x00054A10
		public static object[] GetObjectData(object obj, MemberInfo[] members)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			int num = members.Length;
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				MemberInfo memberInfo = members[i];
				if (memberInfo == null)
				{
					throw new ArgumentNullException(string.Format("members[{0}]", i));
				}
				if (memberInfo.MemberType != MemberTypes.Field)
				{
					throw new SerializationException(string.Format("members [{0}] is not a field.", i));
				}
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				array[i] = fieldInfo.GetValue(obj);
			}
			return array;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000568B4 File Offset: 0x00054AB4
		public static MemberInfo[] GetSerializableMembers(Type type, StreamingContext context)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ArrayList arrayList = new ArrayList();
			for (Type type2 = type; type2 != null; type2 = type2.BaseType)
			{
				if (!type2.IsSerializable)
				{
					string message = string.Format("Type {0} in assembly {1} is not marked as serializable.", type2, type2.Assembly.FullName);
					throw new SerializationException(message);
				}
				FormatterServices.GetFields(type, type2, arrayList);
			}
			MemberInfo[] array = new MemberInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00056934 File Offset: 0x00054B34
		private static void GetFields(Type reflectedType, Type type, ArrayList fields)
		{
			FieldInfo[] fields2 = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields2)
			{
				if (!fieldInfo.IsNotSerialized)
				{
					MonoField monoField = fieldInfo as MonoField;
					if (monoField != null && reflectedType != type && !monoField.IsPublic)
					{
						string newName = type.Name + "+" + monoField.Name;
						fields.Add(monoField.Clone(newName));
					}
					else
					{
						fields.Add(fieldInfo);
					}
				}
			}
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000569C8 File Offset: 0x00054BC8
		public static object GetUninitializedObject(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type == typeof(string))
			{
				throw new ArgumentException("Uninitialized Strings cannot be created.");
			}
			return ActivationServices.AllocateUninitializedClassInstance(type);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000569FC File Offset: 0x00054BFC
		public static object GetSafeUninitializedObject(Type type)
		{
			return FormatterServices.GetUninitializedObject(type);
		}

		// Token: 0x04000C5B RID: 3163
		private const BindingFlags fieldFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
