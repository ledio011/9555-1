using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000F7 RID: 247
	[StructLayout(0)]
	public class ScriptableObject : Object
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x00014D00 File Offset: 0x00012F00
		public ScriptableObject()
		{
			ScriptableObject.Internal_CreateScriptableObject(this);
		}

		// Token: 0x0600097B RID: 2427
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateScriptableObject([Writable] ScriptableObject self);
	}
}
