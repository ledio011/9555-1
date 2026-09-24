using System;

namespace UnityEngine
{
	// Token: 0x020000DD RID: 221
	public struct RaycastHit2D
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00013BCC File Offset: 0x00011DCC
		public Vector2 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00013BD4 File Offset: 0x00011DD4
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00013BDC File Offset: 0x00011DDC
		public float fraction
		{
			get
			{
				return this.m_Fraction;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00013BE4 File Offset: 0x00011DE4
		public Collider2D collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00013BEC File Offset: 0x00011DEC
		public Rigidbody2D rigidbody
		{
			get
			{
				return (!(this.collider != null)) ? null : this.collider.attachedRigidbody;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00013C10 File Offset: 0x00011E10
		public Transform transform
		{
			get
			{
				Rigidbody2D rigidbody = this.rigidbody;
				if (rigidbody != null)
				{
					return rigidbody.transform;
				}
				if (this.collider != null)
				{
					return this.collider.transform;
				}
				return null;
			}
		}

		// Token: 0x0400034C RID: 844
		private Vector2 m_Centroid;

		// Token: 0x0400034D RID: 845
		private Vector2 m_Point;

		// Token: 0x0400034E RID: 846
		private Vector2 m_Normal;

		// Token: 0x0400034F RID: 847
		private float m_Distance;

		// Token: 0x04000350 RID: 848
		private float m_Fraction;

		// Token: 0x04000351 RID: 849
		private Collider2D m_Collider;
	}
}
