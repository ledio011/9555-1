using System;

namespace UnityEngine
{
	// Token: 0x020000CF RID: 207
	public struct Plane
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x000134CC File Offset: 0x000116CC
		public Plane(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(inNormal, inPoint);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000134E8 File Offset: 0x000116E8
		public Plane(Vector3 inNormal, float d)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = d;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00013500 File Offset: 0x00011700
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00013534 File Offset: 0x00011734
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0001353C File Offset: 0x0001173C
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00013544 File Offset: 0x00011744
		public bool Raycast(Ray ray, out float enter)
		{
			float num = Vector3.Dot(ray.direction, this.normal);
			float num2 = -Vector3.Dot(ray.origin, this.normal) - this.distance;
			if (Mathf.Approximately(num, 0f))
			{
				enter = 0f;
				return false;
			}
			enter = num2 / num;
			return enter > 0f;
		}

		// Token: 0x0400032D RID: 813
		private Vector3 m_Normal;

		// Token: 0x0400032E RID: 814
		private float m_Distance;
	}
}
