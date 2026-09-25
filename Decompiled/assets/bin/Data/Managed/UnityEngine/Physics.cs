using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000CD RID: 205
	public class Physics
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00013308 File Offset: 0x00011508
		public static Vector3 gravity
		{
			get
			{
				Vector3 result;
				Physics.INTERNAL_get_gravity(out result);
				return result;
			}
		}

		// Token: 0x06000840 RID: 2112
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_gravity(out Vector3 value);

		// Token: 0x06000841 RID: 2113 RVA: 0x00013320 File Offset: 0x00011520
		private static bool Internal_Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance, int layermask)
		{
			return Physics.INTERNAL_CALL_Internal_Raycast(ref origin, ref direction, out hitInfo, distance, layermask);
		}

		// Token: 0x06000842 RID: 2114
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_Internal_Raycast(ref Vector3 origin, ref Vector3 direction, out RaycastHit hitInfo, float distance, int layermask);

		// Token: 0x06000843 RID: 2115 RVA: 0x00013330 File Offset: 0x00011530
		private static bool Internal_CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float distance, int layermask)
		{
			return Physics.INTERNAL_CALL_Internal_CapsuleCast(ref point1, ref point2, radius, ref direction, out hitInfo, distance, layermask);
		}

		// Token: 0x06000844 RID: 2116
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_Internal_CapsuleCast(ref Vector3 point1, ref Vector3 point2, float radius, ref Vector3 direction, out RaycastHit hitInfo, float distance, int layermask);

		// Token: 0x06000845 RID: 2117 RVA: 0x00013344 File Offset: 0x00011544
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Internal_Raycast(origin, direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00013354 File Offset: 0x00011554
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float distance)
		{
			int layerMask = -5;
			return Physics.Raycast(ray, out hitInfo, distance, layerMask);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00013370 File Offset: 0x00011570
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo)
		{
			int layerMask = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(ray, out hitInfo, positiveInfinity, layerMask);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00013390 File Offset: 0x00011590
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Raycast(ray.origin, ray.direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000133A8 File Offset: 0x000115A8
		public static RaycastHit[] RaycastAll(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, distance, layerMask);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000133C0 File Offset: 0x000115C0
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layermask)
		{
			return Physics.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layermask);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000133D0 File Offset: 0x000115D0
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float distance)
		{
			int layermask = -5;
			return Physics.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layermask);
		}

		// Token: 0x0600084C RID: 2124
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern RaycastHit[] INTERNAL_CALL_RaycastAll(ref Vector3 origin, ref Vector3 direction, float distance, int layermask);

		// Token: 0x0600084D RID: 2125 RVA: 0x000133EC File Offset: 0x000115EC
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Internal_CapsuleCast(ray.origin, ray.origin, radius, ray.direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x04000323 RID: 803
		public const int kIgnoreRaycastLayer = 4;

		// Token: 0x04000324 RID: 804
		public const int kDefaultRaycastLayers = -5;

		// Token: 0x04000325 RID: 805
		public const int kAllLayers = -1;

		// Token: 0x04000326 RID: 806
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x04000327 RID: 807
		public const int DefaultRaycastLayers = -5;

		// Token: 0x04000328 RID: 808
		public const int AllLayers = -1;
	}
}
