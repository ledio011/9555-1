using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000BE RID: 190
	public sealed class NavMeshAgent : Behaviour
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x00012F38 File Offset: 0x00011138
		public bool SetDestination(Vector3 target)
		{
			return NavMeshAgent.INTERNAL_CALL_SetDestination(this, ref target);
		}

		// Token: 0x060007EF RID: 2031
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_SetDestination(NavMeshAgent self, ref Vector3 target);

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007F0 RID: 2032
		// (set) Token: 0x060007F1 RID: 2033
		public extern float stoppingDistance { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001AD RID: 429
		// (set) Token: 0x060007F2 RID: 2034
		public extern bool autoBraking { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060007F3 RID: 2035
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Stop([DefaultValue("false")] bool stopUpdates);

		// Token: 0x060007F4 RID: 2036 RVA: 0x00012F44 File Offset: 0x00011144
		[ExcludeFromDocs]
		public void Stop()
		{
			bool stopUpdates = false;
			this.Stop(stopUpdates);
		}

		// Token: 0x060007F5 RID: 2037
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void ResetPath();

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007F6 RID: 2038
		// (set) Token: 0x060007F7 RID: 2039
		public extern int walkableMask { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007F8 RID: 2040
		// (set) Token: 0x060007F9 RID: 2041
		public extern float speed { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001B0 RID: 432
		// (set) Token: 0x060007FA RID: 2042
		public extern float angularSpeed { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001B1 RID: 433
		// (set) Token: 0x060007FB RID: 2043
		public extern float acceleration { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001B2 RID: 434
		// (set) Token: 0x060007FC RID: 2044
		public extern float radius { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001B3 RID: 435
		// (set) Token: 0x060007FD RID: 2045
		public extern ObstacleAvoidanceType obstacleAvoidanceType { [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}
