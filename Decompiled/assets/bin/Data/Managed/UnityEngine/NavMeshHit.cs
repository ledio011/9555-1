using System;

namespace UnityEngine
{
	// Token: 0x020000BF RID: 191
	public struct NavMeshHit
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00012F5C File Offset: 0x0001115C
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x04000309 RID: 777
		private Vector3 m_Position;

		// Token: 0x0400030A RID: 778
		private Vector3 m_Normal;

		// Token: 0x0400030B RID: 779
		private float m_Distance;

		// Token: 0x0400030C RID: 780
		private int m_Mask;

		// Token: 0x0400030D RID: 781
		private int m_Hit;
	}
}
