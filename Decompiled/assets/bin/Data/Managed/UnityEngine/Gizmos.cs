using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200006A RID: 106
	public sealed class Gizmos
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x0000A380 File Offset: 0x00008580
		public static void DrawLine(Vector3 from, Vector3 to)
		{
			Gizmos.INTERNAL_CALL_DrawLine(ref from, ref to);
		}

		// Token: 0x060004B3 RID: 1203
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DrawLine(ref Vector3 from, ref Vector3 to);

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000A38C File Offset: 0x0000858C
		public static void DrawWireSphere(Vector3 center, float radius)
		{
			Gizmos.INTERNAL_CALL_DrawWireSphere(ref center, radius);
		}

		// Token: 0x060004B5 RID: 1205
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DrawWireSphere(ref Vector3 center, float radius);

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000A398 File Offset: 0x00008598
		public static void DrawSphere(Vector3 center, float radius)
		{
			Gizmos.INTERNAL_CALL_DrawSphere(ref center, radius);
		}

		// Token: 0x060004B7 RID: 1207
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DrawSphere(ref Vector3 center, float radius);

		// Token: 0x17000112 RID: 274
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public static Color color
		{
			set
			{
				Gizmos.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x060004B9 RID: 1209
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_color(ref Color value);
	}
}
