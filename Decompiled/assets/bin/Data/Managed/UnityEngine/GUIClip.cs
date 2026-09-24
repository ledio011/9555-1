using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000073 RID: 115
	internal sealed class GUIClip
	{
		// Token: 0x0600059B RID: 1435 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.INTERNAL_CALL_Push(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
		}

		// Token: 0x0600059C RID: 1436
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Push(ref Rect screenRect, ref Vector2 scrollOffset, ref Vector2 renderOffset, bool resetOffset);

		// Token: 0x0600059D RID: 1437
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void Pop();

		// Token: 0x0600059E RID: 1438 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		public static Vector2 Unclip(Vector2 pos)
		{
			GUIClip.Unclip_Vector2(ref pos);
			return pos;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000CED0 File Offset: 0x0000B0D0
		private static void Unclip_Vector2(ref Vector2 pos)
		{
			GUIClip.INTERNAL_CALL_Unclip_Vector2(ref pos);
		}

		// Token: 0x060005A0 RID: 1440
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Unclip_Vector2(ref Vector2 pos);

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		public static Vector2 Clip(Vector2 absolutePos)
		{
			GUIClip.Clip_Vector2(ref absolutePos);
			return absolutePos;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		private static void Clip_Vector2(ref Vector2 absolutePos)
		{
			GUIClip.INTERNAL_CALL_Clip_Vector2(ref absolutePos);
		}

		// Token: 0x060005A3 RID: 1443
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Clip_Vector2(ref Vector2 absolutePos);

		// Token: 0x060005A4 RID: 1444
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern Matrix4x4 GetMatrix();

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		internal static void SetMatrix(Matrix4x4 m)
		{
			GUIClip.INTERNAL_CALL_SetMatrix(ref m);
		}

		// Token: 0x060005A6 RID: 1446
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetMatrix(ref Matrix4x4 m);
	}
}
