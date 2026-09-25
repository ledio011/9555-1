using System;

namespace UnityEngine
{
	// Token: 0x02000136 RID: 310
	public struct Vector4
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x0001AC7C File Offset: 0x00018E7C
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001AC9C File Offset: 0x00018E9C
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x17000288 RID: 648
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				case 3:
					return this.w;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001AD7C File Offset: 0x00018F7C
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		public override bool Equals(object other)
		{
			if (!(other is Vector4))
			{
				return false;
			}
			Vector4 vector = (Vector4)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z) && this.w.Equals(vector.w);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001AE30 File Offset: 0x00019030
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001AE88 File Offset: 0x00019088
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0001AED4 File Offset: 0x000190D4
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(Vector4.Dot(this, this));
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001AEEC File Offset: 0x000190EC
		public static float SqrMagnitude(Vector4 a)
		{
			return Vector4.Dot(a, a);
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0001AEF8 File Offset: 0x000190F8
		public float sqrMagnitude
		{
			get
			{
				return Vector4.Dot(this, this);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0001AF0C File Offset: 0x0001910C
		public static Vector4 zero
		{
			get
			{
				return new Vector4(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0001AF28 File Offset: 0x00019128
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001AF78 File Offset: 0x00019178
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0001AFC8 File Offset: 0x000191C8
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0001AFF4 File Offset: 0x000191F4
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0001B020 File Offset: 0x00019220
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return Vector4.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0001B038 File Offset: 0x00019238
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0001B05C File Offset: 0x0001925C
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0001B07C File Offset: 0x0001927C
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x040004EE RID: 1262
		public const float kEpsilon = 1E-05f;

		// Token: 0x040004EF RID: 1263
		public float x;

		// Token: 0x040004F0 RID: 1264
		public float y;

		// Token: 0x040004F1 RID: 1265
		public float z;

		// Token: 0x040004F2 RID: 1266
		public float w;
	}
}
