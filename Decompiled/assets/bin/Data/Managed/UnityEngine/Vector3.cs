using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000135 RID: 309
	public struct Vector3
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0001A4AC File Offset: 0x000186AC
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0001A4C4 File Offset: 0x000186C4
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0001A4E0 File Offset: 0x000186E0
		public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0001A548 File Offset: 0x00018748
		public static Vector3 Slerp(Vector3 from, Vector3 to, float t)
		{
			return Vector3.INTERNAL_CALL_Slerp(ref from, ref to, t);
		}

		// Token: 0x06000B3A RID: 2874
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_Slerp(ref Vector3 from, ref Vector3 to, float t);

		// Token: 0x06000B3B RID: 2875 RVA: 0x0001A554 File Offset: 0x00018754
		public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			return Vector3.INTERNAL_CALL_RotateTowards(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta);
		}

		// Token: 0x06000B3C RID: 2876
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_RotateTowards(ref Vector3 current, ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta);

		// Token: 0x1700027C RID: 636
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
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0001A604 File Offset: 0x00018804
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001A638 File Offset: 0x00018838
		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001A678 File Offset: 0x00018878
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0001A6E8 File Offset: 0x000188E8
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001A714 File Offset: 0x00018914
		public override bool Equals(object other)
		{
			if (!(other is Vector3))
			{
				return false;
			}
			Vector3 vector = (Vector3)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001A778 File Offset: 0x00018978
		public static Vector3 Normalize(Vector3 value)
		{
			float num = Vector3.Magnitude(value);
			if (num > 1E-05f)
			{
				return value / num;
			}
			return Vector3.zero;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001A7A4 File Offset: 0x000189A4
		public void Normalize()
		{
			float num = Vector3.Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0001A7EC File Offset: 0x000189EC
		public Vector3 normalized
		{
			get
			{
				return Vector3.Normalize(this);
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001A7FC File Offset: 0x000189FC
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1})", new object[]
			{
				this.x,
				this.y,
				this.z
			});
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001A838 File Offset: 0x00018A38
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001A86C File Offset: 0x00018A6C
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Vector3.Dot(onNormal, onNormal);
			if (num < 1E-45f)
			{
				return Vector3.zero;
			}
			return onNormal * Vector3.Dot(vector, onNormal) / num;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001A8A8 File Offset: 0x00018AA8
		public static float Angle(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001A8D8 File Offset: 0x00018AD8
		public static float Distance(Vector3 a, Vector3 b)
		{
			Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
			return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001A950 File Offset: 0x00018B50
		public static float Magnitude(Vector3 a)
		{
			return Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0001A988 File Offset: 0x00018B88
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0001A9B8 File Offset: 0x00018BB8
		public static float SqrMagnitude(Vector3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0001A9EC File Offset: 0x00018BEC
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0001AA18 File Offset: 0x00018C18
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0001AA58 File Offset: 0x00018C58
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0001AA98 File Offset: 0x00018C98
		public static Vector3 zero
		{
			get
			{
				return new Vector3(0f, 0f, 0f);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		public static Vector3 one
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0001AAC8 File Offset: 0x00018CC8
		public static Vector3 forward
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		public static Vector3 back
		{
			get
			{
				return new Vector3(0f, 0f, -1f);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0001AAF8 File Offset: 0x00018CF8
		public static Vector3 up
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001AB10 File Offset: 0x00018D10
		public static Vector3 down
		{
			get
			{
				return new Vector3(0f, -1f, 0f);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0001AB28 File Offset: 0x00018D28
		public static Vector3 left
		{
			get
			{
				return new Vector3(-1f, 0f, 0f);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0001AB40 File Offset: 0x00018D40
		public static Vector3 right
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0001AB58 File Offset: 0x00018D58
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001AB8C File Offset: 0x00018D8C
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001AC04 File Offset: 0x00018E04
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001AC28 File Offset: 0x00018E28
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0001AC4C File Offset: 0x00018E4C
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001AC64 File Offset: 0x00018E64
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x040004EA RID: 1258
		public const float kEpsilon = 1E-05f;

		// Token: 0x040004EB RID: 1259
		public float x;

		// Token: 0x040004EC RID: 1260
		public float y;

		// Token: 0x040004ED RID: 1261
		public float z;
	}
}
