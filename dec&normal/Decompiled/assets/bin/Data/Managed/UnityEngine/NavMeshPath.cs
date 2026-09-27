using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000C0 RID: 192
	[StructLayout(0)]
	public sealed class NavMeshPath
	{
		// Token: 0x060007FF RID: 2047
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern NavMeshPath();

		// Token: 0x06000800 RID: 2048
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void DestroyNavMeshPath();

		// Token: 0x06000801 RID: 2049 RVA: 0x00012F64 File Offset: 0x00011164
		~NavMeshPath()
		{
			this.DestroyNavMeshPath();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000802 RID: 2050
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Vector3[] CalculateCornersInternal();

		// Token: 0x06000803 RID: 2051
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void ClearCornersInternal();

		// Token: 0x06000804 RID: 2052 RVA: 0x00012FA0 File Offset: 0x000111A0
		public void ClearCorners()
		{
			this.ClearCornersInternal();
			this.m_corners = null;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00012FB0 File Offset: 0x000111B0
		private void CalculateCorners()
		{
			if (this.m_corners == null)
			{
				this.m_corners = this.CalculateCornersInternal();
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00012FCC File Offset: 0x000111CC
		public Vector3[] corners
		{
			get
			{
				this.CalculateCorners();
				return this.m_corners;
			}
		}

		// Token: 0x0400030E RID: 782
		internal IntPtr m_Ptr;

		// Token: 0x0400030F RID: 783
		internal Vector3[] m_corners;
	}
}
