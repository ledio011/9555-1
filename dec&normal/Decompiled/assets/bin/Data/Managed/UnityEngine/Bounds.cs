using System;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	public struct Bounds
	{
		// Token: 0x06000289 RID: 649 RVA: 0x000074B0 File Offset: 0x000056B0
		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000074CC File Offset: 0x000056CC
		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ this.extents.GetHashCode() << 2;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000074F8 File Offset: 0x000056F8
		public override bool Equals(object other)
		{
			if (!(other is Bounds))
			{
				return false;
			}
			Bounds bounds = (Bounds)other;
			return this.center.Equals(bounds.center) && this.extents.Equals(bounds.extents);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00007558 File Offset: 0x00005758
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00007560 File Offset: 0x00005760
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000756C File Offset: 0x0000576C
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00007580 File Offset: 0x00005780
		public Vector3 size
		{
			get
			{
				return this.m_Extents * 2f;
			}
			set
			{
				this.m_Extents = value * 0.5f;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00007594 File Offset: 0x00005794
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000759C File Offset: 0x0000579C
		public Vector3 extents
		{
			get
			{
				return this.m_Extents;
			}
			set
			{
				this.m_Extents = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000075A8 File Offset: 0x000057A8
		// (set) Token: 0x06000293 RID: 659 RVA: 0x000075BC File Offset: 0x000057BC
		public Vector3 min
		{
			get
			{
				return this.center - this.extents;
			}
			set
			{
				this.SetMinMax(value, this.max);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000075CC File Offset: 0x000057CC
		// (set) Token: 0x06000295 RID: 661 RVA: 0x000075E0 File Offset: 0x000057E0
		public Vector3 max
		{
			get
			{
				return this.center + this.extents;
			}
			set
			{
				this.SetMinMax(this.min, value);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000075F0 File Offset: 0x000057F0
		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000761C File Offset: 0x0000581C
		public void Encapsulate(Vector3 point)
		{
			this.SetMinMax(Vector3.Min(this.min, point), Vector3.Max(this.max, point));
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000763C File Offset: 0x0000583C
		public void Encapsulate(Bounds bounds)
		{
			this.Encapsulate(bounds.center - bounds.extents);
			this.Encapsulate(bounds.center + bounds.extents);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007670 File Offset: 0x00005870
		public override string ToString()
		{
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center,
				this.m_Extents
			});
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000076A0 File Offset: 0x000058A0
		public static bool operator ==(Bounds lhs, Bounds rhs)
		{
			return lhs.center == rhs.center && lhs.extents == rhs.extents;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000076D0 File Offset: 0x000058D0
		public static bool operator !=(Bounds lhs, Bounds rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04000036 RID: 54
		private Vector3 m_Center;

		// Token: 0x04000037 RID: 55
		private Vector3 m_Extents;
	}
}
