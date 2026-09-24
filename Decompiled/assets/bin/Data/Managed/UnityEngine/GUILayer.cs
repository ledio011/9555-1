using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000076 RID: 118
	public sealed class GUILayer : Behaviour
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x0000D134 File Offset: 0x0000B334
		public GUIElement HitTest(Vector3 screenPosition)
		{
			return GUILayer.INTERNAL_CALL_HitTest(this, ref screenPosition);
		}

		// Token: 0x060005B8 RID: 1464
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern GUIElement INTERNAL_CALL_HitTest(GUILayer self, ref Vector3 screenPosition);
	}
}
