using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x020000FF RID: 255
	internal class SetupCoroutine
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x000153DC File Offset: 0x000135DC
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] args = null;
			if (variable != null)
			{
				args = new object[]
				{
					variable
				};
			}
			return behaviour.GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, behaviour, args, null, null, null);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00015414 File Offset: 0x00013614
		public static object InvokeStatic(Type klass, string name, object variable)
		{
			object[] args = null;
			if (variable != null)
			{
				args = new object[]
				{
					variable
				};
			}
			return klass.InvokeMember(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, args, null, null, null);
		}
	}
}
