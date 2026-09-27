using System;

namespace UnityEngine
{
	// Token: 0x020000DB RID: 219
	public struct Ray
	{
		// Token: 0x06000898 RID: 2200 RVA: 0x00013B08 File Offset: 0x00011D08
		public Ray(Vector3 origin, Vector3 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00013B20 File Offset: 0x00011D20
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00013B28 File Offset: 0x00011D28
		public Vector3 direction
		{
			get
			{
				return this.m_Direction;
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00013B30 File Offset: 0x00011D30
		public Vector3 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00013B4C File Offset: 0x00011D4C
		public override string ToString()
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin,
				this.m_Direction
			});
		}

		// Token: 0x04000344 RID: 836
		private Vector3 m_Origin;

		// Token: 0x04000345 RID: 837
		private Vector3 m_Direction;
	}
}
