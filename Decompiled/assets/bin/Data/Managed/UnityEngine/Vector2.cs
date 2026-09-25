using System;

namespace UnityEngine
{
	// Token: 0x02000134 RID: 308
	public struct Vector2
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x0001A07C File Offset: 0x0001827C
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x17000275 RID: 629
		public float this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.x;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException("Invalid Vector2 index!");
				}
				return this.y;
			}
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001A10C File Offset: 0x0001830C
		public static Vector2 Lerp(Vector2 from, Vector2 to, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector2(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0001A158 File Offset: 0x00018358
		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001A180 File Offset: 0x00018380
		public void Normalize()
		{
			float magnitude = this.magnitude;
			if (magnitude > 1E-05f)
			{
				this /= magnitude;
			}
			else
			{
				this = Vector2.zero;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0001A1C4 File Offset: 0x000183C4
		public Vector2 normalized
		{
			get
			{
				Vector2 result = new Vector2(this.x, this.y);
				result.Normalize();
				return result;
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001A1EC File Offset: 0x000183EC
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1})", new object[]
			{
				this.x,
				this.y
			});
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001A21C File Offset: 0x0001841C
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001A238 File Offset: 0x00018438
		public override bool Equals(object other)
		{
			if (!(other is Vector2))
			{
				return false;
			}
			Vector2 vector = (Vector2)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001A288 File Offset: 0x00018488
		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001A2AC File Offset: 0x000184AC
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(this.x * this.x + this.y * this.y);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0001A2D0 File Offset: 0x000184D0
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001A2F0 File Offset: 0x000184F0
		public static float Distance(Vector2 a, Vector2 b)
		{
			return (a - b).magnitude;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001A30C File Offset: 0x0001850C
		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001A330 File Offset: 0x00018530
		public float SqrMagnitude()
		{
			return this.x * this.x + this.y * this.y;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0001A350 File Offset: 0x00018550
		public static Vector2 zero
		{
			get
			{
				return new Vector2(0f, 0f);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001A364 File Offset: 0x00018564
		public static Vector2 one
		{
			get
			{
				return new Vector2(1f, 1f);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0001A378 File Offset: 0x00018578
		public static Vector2 up
		{
			get
			{
				return new Vector2(0f, 1f);
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001A38C File Offset: 0x0001858C
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0001A3B4 File Offset: 0x000185B4
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0001A3DC File Offset: 0x000185DC
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001A410 File Offset: 0x00018610
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001A42C File Offset: 0x0001862C
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0001A448 File Offset: 0x00018648
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0001A460 File Offset: 0x00018660
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0001A478 File Offset: 0x00018678
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0001A490 File Offset: 0x00018690
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x040004E7 RID: 1255
		public const float kEpsilon = 1E-05f;

		// Token: 0x040004E8 RID: 1256
		public float x;

		// Token: 0x040004E9 RID: 1257
		public float y;
	}
}
