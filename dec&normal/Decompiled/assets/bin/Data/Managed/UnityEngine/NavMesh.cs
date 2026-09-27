using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000BD RID: 189
	public sealed class NavMesh : Object
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x00012EF8 File Offset: 0x000110F8
		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int passableMask)
		{
			return NavMesh.INTERNAL_CALL_Raycast(ref sourcePosition, ref targetPosition, out hit, passableMask);
		}

		// Token: 0x060007E8 RID: 2024
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_Raycast(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int passableMask);

		// Token: 0x060007E9 RID: 2025 RVA: 0x00012F08 File Offset: 0x00011108
		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, int passableMask, NavMeshPath path)
		{
			path.ClearCorners();
			return NavMesh.CalculatePathInternal(sourcePosition, targetPosition, passableMask, path);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00012F1C File Offset: 0x0001111C
		internal static bool CalculatePathInternal(Vector3 sourcePosition, Vector3 targetPosition, int passableMask, NavMeshPath path)
		{
			return NavMesh.INTERNAL_CALL_CalculatePathInternal(ref sourcePosition, ref targetPosition, passableMask, path);
		}

		// Token: 0x060007EB RID: 2027
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_CalculatePathInternal(ref Vector3 sourcePosition, ref Vector3 targetPosition, int passableMask, NavMeshPath path);

		// Token: 0x060007EC RID: 2028 RVA: 0x00012F2C File Offset: 0x0001112C
		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int allowedMask)
		{
			return NavMesh.INTERNAL_CALL_SamplePosition(ref sourcePosition, out hit, maxDistance, allowedMask);
		}

		// Token: 0x060007ED RID: 2029
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_SamplePosition(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int allowedMask);
	}
}
