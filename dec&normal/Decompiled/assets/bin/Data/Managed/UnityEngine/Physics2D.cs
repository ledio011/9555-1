using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000CE RID: 206
	public class Physics2D
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x0001341C File Offset: 0x0001161C
		private static void Internal_Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_Raycast(ref origin, ref direction, distance, layerMask, minDepth, maxDepth, out raycastHit);
		}

		// Token: 0x06000850 RID: 2128
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_Raycast(ref Vector2 origin, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit);

		// Token: 0x06000851 RID: 2129 RVA: 0x00013430 File Offset: 0x00011630
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.Raycast(origin, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00013454 File Offset: 0x00011654
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			RaycastHit2D result;
			Physics2D.Internal_Raycast(origin, direction, distance, layerMask, minDepth, maxDepth, out result);
			return result;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00013474 File Offset: 0x00011674
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06000854 RID: 2132
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern RaycastHit2D[] INTERNAL_CALL_RaycastAll(ref Vector2 origin, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06000855 RID: 2133 RVA: 0x0001349C File Offset: 0x0001169C
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.INTERNAL_CALL_GetRayIntersectionNonAlloc(ref ray, results, distance, layerMask);
		}

		// Token: 0x06000856 RID: 2134
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_GetRayIntersectionNonAlloc(ref Ray ray, RaycastHit2D[] results, float distance, int layerMask);

		// Token: 0x06000857 RID: 2135 RVA: 0x000134A8 File Offset: 0x000116A8
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06000858 RID: 2136
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Collider2D INTERNAL_CALL_OverlapPoint(ref Vector2 point, int layerMask, float minDepth, float maxDepth);

		// Token: 0x04000329 RID: 809
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x0400032A RID: 810
		public const int DefaultRaycastLayers = -5;

		// Token: 0x0400032B RID: 811
		public const int AllLayers = -1;

		// Token: 0x0400032C RID: 812
		private static List<Rigidbody2D> m_LastDisabledRigidbody2D = new List<Rigidbody2D>();
	}
}
