using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	internal class AttributeHelperEngine
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00006F5C File Offset: 0x0000515C
		private static Type GetParentTypeDisallowingMultipleInclusion(Type type)
		{
			Stack<Type> stack = new Stack<Type>();
			while (type != null && type != typeof(MonoBehaviour))
			{
				stack.Push(type);
				type = type.BaseType;
			}
			while (stack.Count > 0)
			{
				Type type2 = stack.Pop();
				object[] customAttributes = type2.GetCustomAttributes(typeof(DisallowMultipleComponent), false);
				if (customAttributes.Length != 0)
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006FD0 File Offset: 0x000051D0
		private static Type[] GetRequiredComponents(Type klass)
		{
			List<Type> list = null;
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				object[] customAttributes = klass.GetCustomAttributes(typeof(RequireComponent), false);
				foreach (RequireComponent requireComponent in customAttributes)
				{
					if (list == null && customAttributes.Length == 1 && klass.BaseType == typeof(MonoBehaviour))
					{
						return new Type[]
						{
							requireComponent.m_Type0,
							requireComponent.m_Type1,
							requireComponent.m_Type2
						};
					}
					if (list == null)
					{
						list = new List<Type>();
					}
					if (requireComponent.m_Type0 != null)
					{
						list.Add(requireComponent.m_Type0);
					}
					if (requireComponent.m_Type1 != null)
					{
						list.Add(requireComponent.m_Type1);
					}
					if (requireComponent.m_Type2 != null)
					{
						list.Add(requireComponent.m_Type2);
					}
				}
				klass = klass.BaseType;
			}
			if (list == null)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000070DC File Offset: 0x000052DC
		private static bool CheckIsEditorScript(Type klass)
		{
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				object[] customAttributes = klass.GetCustomAttributes(typeof(ExecuteInEditMode), false);
				if (customAttributes.Length != 0)
				{
					return true;
				}
				klass = klass.BaseType;
			}
			return false;
		}
	}
}
