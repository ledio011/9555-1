using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D7 RID: 215
	[DefaultMember("Item")]
	public struct Quaternion
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x00013638 File Offset: 0x00011838
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00013658 File Offset: 0x00011858
		public static Quaternion identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00013674 File Offset: 0x00011874
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000136C0 File Offset: 0x000118C0
		public static Quaternion AngleAxis(float angle, Vector3 axis)
		{
			return Quaternion.INTERNAL_CALL_AngleAxis(angle, ref axis);
		}

		// Token: 0x0600087C RID: 2172
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_AngleAxis(float angle, ref Vector3 axis);

		// Token: 0x0600087D RID: 2173 RVA: 0x000136CC File Offset: 0x000118CC
		public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			return Quaternion.INTERNAL_CALL_FromToRotation(ref fromDirection, ref toDirection);
		}

		// Token: 0x0600087E RID: 2174
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_FromToRotation(ref Vector3 fromDirection, ref Vector3 toDirection);

		// Token: 0x0600087F RID: 2175 RVA: 0x000136D8 File Offset: 0x000118D8
		public static Quaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
		{
			return Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref upwards);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000136E4 File Offset: 0x000118E4
		[ExcludeFromDocs]
		public static Quaternion LookRotation(Vector3 forward)
		{
			Vector3 up = Vector3.up;
			return Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref up);
		}

		// Token: 0x06000881 RID: 2177
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_LookRotation(ref Vector3 forward, ref Vector3 upwards);

		// Token: 0x06000882 RID: 2178 RVA: 0x00013700 File Offset: 0x00011900
		public static Quaternion Slerp(Quaternion from, Quaternion to, float t)
		{
			return Quaternion.INTERNAL_CALL_Slerp(ref from, ref to, t);
		}

		// Token: 0x06000883 RID: 2179
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_Slerp(ref Quaternion from, ref Quaternion to, float t);

		// Token: 0x06000884 RID: 2180 RVA: 0x0001370C File Offset: 0x0001190C
		public static Quaternion Inverse(Quaternion rotation)
		{
			return Quaternion.INTERNAL_CALL_Inverse(ref rotation);
		}

		// Token: 0x06000885 RID: 2181
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_Inverse(ref Quaternion rotation);

		// Token: 0x06000886 RID: 2182 RVA: 0x00013718 File Offset: 0x00011918
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

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00013770 File Offset: 0x00011970
		public Vector3 eulerAngles
		{
			get
			{
				return Quaternion.Internal_ToEulerRad(this) * 57.29578f;
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00013788 File Offset: 0x00011988
		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * 0.017453292f);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000137A4 File Offset: 0x000119A4
		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler * 0.017453292f);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000137B8 File Offset: 0x000119B8
		private static Vector3 Internal_ToEulerRad(Quaternion rotation)
		{
			return Quaternion.INTERNAL_CALL_Internal_ToEulerRad(ref rotation);
		}

		// Token: 0x0600088B RID: 2187
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_Internal_ToEulerRad(ref Quaternion rotation);

		// Token: 0x0600088C RID: 2188 RVA: 0x000137C4 File Offset: 0x000119C4
		private static Quaternion Internal_FromEulerRad(Vector3 euler)
		{
			return Quaternion.INTERNAL_CALL_Internal_FromEulerRad(ref euler);
		}

		// Token: 0x0600088D RID: 2189
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_Internal_FromEulerRad(ref Vector3 euler);

		// Token: 0x0600088E RID: 2190 RVA: 0x000137D0 File Offset: 0x000119D0
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00013808 File Offset: 0x00011A08
		public override bool Equals(object other)
		{
			if (!(other is Quaternion))
			{
				return false;
			}
			Quaternion quaternion = (Quaternion)other;
			return this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z) && this.w.Equals(quaternion.w);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00013884 File Offset: 0x00011A84
		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00013994 File Offset: 0x00011B94
		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 result;
			result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return result;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00013AD0 File Offset: 0x00011CD0
		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.Dot(lhs, rhs) <= 0.999999f;
		}

		// Token: 0x0400033A RID: 826
		public const float kEpsilon = 1E-06f;

		// Token: 0x0400033B RID: 827
		public float x;

		// Token: 0x0400033C RID: 828
		public float y;

		// Token: 0x0400033D RID: 829
		public float z;

		// Token: 0x0400033E RID: 830
		public float w;
	}
}
