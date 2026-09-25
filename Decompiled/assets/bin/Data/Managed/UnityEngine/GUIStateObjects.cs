using System;
using System.Collections.Generic;
using System.Security;

namespace UnityEngine
{
	// Token: 0x02000082 RID: 130
	internal class GUIStateObjects
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00010580 File Offset: 0x0000E780
		[SecuritySafeCritical]
		internal static object GetStateObject(Type t, int controlID)
		{
			object obj;
			if (!GUIStateObjects.s_StateCache.TryGetValue(controlID, out obj) || obj.GetType() != t)
			{
				obj = Activator.CreateInstance(t);
				GUIStateObjects.s_StateCache[controlID] = obj;
			}
			return obj;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000105C0 File Offset: 0x0000E7C0
		internal static object QueryStateObject(Type t, int controlID)
		{
			object obj = GUIStateObjects.s_StateCache[controlID];
			if (t.IsInstanceOfType(obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x0400017C RID: 380
		private static Dictionary<int, object> s_StateCache = new Dictionary<int, object>();
	}
}
