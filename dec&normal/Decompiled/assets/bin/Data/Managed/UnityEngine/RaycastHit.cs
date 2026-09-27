using System;

namespace UnityEngine
{
	// Token: 0x020000DC RID: 220
	public struct RaycastHit
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00013B7C File Offset: 0x00011D7C
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x00013B84 File Offset: 0x00011D84
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00013B90 File Offset: 0x00011D90
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00013B98 File Offset: 0x00011D98
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00013BA0 File Offset: 0x00011DA0
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00013BA8 File Offset: 0x00011DA8
		public Rigidbody rigidbody
		{
			get
			{
				return (!(this.collider != null)) ? null : this.collider.attachedRigidbody;
			}
		}

		// Token: 0x04000346 RID: 838
		private Vector3 m_Point;

		// Token: 0x04000347 RID: 839
		private Vector3 m_Normal;

		// Token: 0x04000348 RID: 840
		private int m_FaceID;

		// Token: 0x04000349 RID: 841
		private float m_Distance;

		// Token: 0x0400034A RID: 842
		private Vector2 m_UV;

		// Token: 0x0400034B RID: 843
		private Collider m_Collider;
	}
}
