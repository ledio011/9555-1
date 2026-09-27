using System;

namespace UnityEngine
{
	// Token: 0x02000042 RID: 66
	public struct ContactPoint
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000809C File Offset: 0x0000629C
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x04000064 RID: 100
		internal Vector3 m_Point;

		// Token: 0x04000065 RID: 101
		internal Vector3 m_Normal;

		// Token: 0x04000066 RID: 102
		internal Collider m_ThisCollider;

		// Token: 0x04000067 RID: 103
		internal Collider m_OtherCollider;
	}
}
