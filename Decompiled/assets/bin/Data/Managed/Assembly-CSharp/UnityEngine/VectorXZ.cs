using System;

namespace UnityEngine
{
	// Token: 0x02000A7E RID: 2686
	public struct VectorXZ
	{
		// Token: 0x06004E16 RID: 19990 RVA: 0x001AADEC File Offset: 0x001A8FEC
		public VectorXZ(float x, float z)
		{
			this.x = x;
			this.z = z;
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x001AADFC File Offset: 0x001A8FFC
		public VectorXZ(VectorXZ a)
		{
			this.x = a.x;
			this.z = a.z;
		}

		// Token: 0x17000FDA RID: 4058
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
					Debug.LogError("Invalid VectorXZ index!");
				}
				return this.z;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06004E19 RID: 19993 RVA: 0x001AAE4C File Offset: 0x001A904C
		public VectorXZ normalized
		{
			get
			{
				VectorXZ result = new VectorXZ(this.x, this.z);
				result.Normalize();
				return result;
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x001AAE74 File Offset: 0x001A9074
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(this.x * this.x + this.z * this.z);
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06004E1B RID: 19995 RVA: 0x001AAEA4 File Offset: 0x001A90A4
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.z * this.z;
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06004E1C RID: 19996 RVA: 0x001AAEC4 File Offset: 0x001A90C4
		public static VectorXZ zero
		{
			get
			{
				return new VectorXZ(0f, 0f);
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06004E1D RID: 19997 RVA: 0x001AAED8 File Offset: 0x001A90D8
		public static VectorXZ one
		{
			get
			{
				return new VectorXZ(1f, 1f);
			}
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x001AAEEC File Offset: 0x001A90EC
		public void Set(float new_x, float new_z)
		{
			this.x = new_x;
			this.z = new_z;
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x001AAEFC File Offset: 0x001A90FC
		public static VectorXZ Lerp(VectorXZ from, VectorXZ to, float t)
		{
			t = Mathf.Clamp01(t);
			return new VectorXZ(from.x + (to.x - from.x) * t, from.z + (to.z - from.z) * t);
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x001AAF48 File Offset: 0x001A9148
		public void Scale(VectorXZ scale)
		{
			this.x *= scale.x;
			this.z *= scale.z;
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x001AAF80 File Offset: 0x001A9180
		public void Normalize()
		{
			float magnitude = this.magnitude;
			if (magnitude > 1E-05f)
			{
				this /= magnitude;
			}
			else
			{
				this = VectorXZ.zero;
			}
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x001AAFC4 File Offset: 0x001A91C4
		public override string ToString()
		{
			return string.Format("({0:F1}, {1:F1})", new object[]
			{
				this.x,
				this.z
			});
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x001AB000 File Offset: 0x001A9200
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.z.GetHashCode() << 2;
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x001AB01C File Offset: 0x001A921C
		public override bool Equals(object other)
		{
			if (!(other is VectorXZ))
			{
				return false;
			}
			VectorXZ vectorXZ = (VectorXZ)other;
			return this.x.Equals(vectorXZ.x) && this.z.Equals(vectorXZ.z);
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x001AB06C File Offset: 0x001A926C
		public static float Dot(VectorXZ lhs, VectorXZ rhs)
		{
			return lhs.x * rhs.x + lhs.z * rhs.z;
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x001AB090 File Offset: 0x001A9290
		public static float Distance(VectorXZ a, VectorXZ b)
		{
			return (a - b).magnitude;
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x001AB0AC File Offset: 0x001A92AC
		public static float SqrMagnitude(VectorXZ a)
		{
			return a.x * a.x + a.z * a.z;
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x001AB0D0 File Offset: 0x001A92D0
		public Vector3 toVector3(float y)
		{
			return new Vector3(this.x, y, this.z);
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x001AB0E4 File Offset: 0x001A92E4
		public static Vector3 operator +(VectorXZ a, Vector3 b)
		{
			return new Vector3(a.x + b.x, b.y, a.z + b.z);
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x001AB11C File Offset: 0x001A931C
		public static VectorXZ operator +(VectorXZ a, VectorXZ b)
		{
			return new VectorXZ(a.x + b.x, a.z + b.z);
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x001AB144 File Offset: 0x001A9344
		public static VectorXZ operator -(VectorXZ a, VectorXZ b)
		{
			return new VectorXZ(a.x - b.x, a.z - b.z);
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x001AB16C File Offset: 0x001A936C
		public static VectorXZ operator -(VectorXZ a)
		{
			return new VectorXZ(-a.x, -a.z);
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x001AB184 File Offset: 0x001A9384
		public static VectorXZ operator *(VectorXZ a, float d)
		{
			return new VectorXZ(a.x * d, a.z * d);
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x001AB1A0 File Offset: 0x001A93A0
		public static VectorXZ operator *(float d, VectorXZ a)
		{
			return new VectorXZ(a.x * d, a.z * d);
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x001AB1BC File Offset: 0x001A93BC
		public static VectorXZ operator /(VectorXZ a, float d)
		{
			return new VectorXZ(a.x / d, a.z / d);
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x001AB1D8 File Offset: 0x001A93D8
		public static bool operator ==(VectorXZ lhs, VectorXZ rhs)
		{
			return VectorXZ.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x001AB1F0 File Offset: 0x001A93F0
		public static bool operator !=(VectorXZ lhs, VectorXZ rhs)
		{
			return VectorXZ.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x001AB208 File Offset: 0x001A9408
		public static implicit operator VectorXZ(Vector3 v)
		{
			return new VectorXZ(v.x, v.z);
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x001AB220 File Offset: 0x001A9420
		public static implicit operator Vector3(VectorXZ v)
		{
			return new Vector3(v.x, 0f, v.z);
		}

		// Token: 0x04003CA3 RID: 15523
		public const float kEpsilon = 1E-05f;

		// Token: 0x04003CA4 RID: 15524
		public float x;

		// Token: 0x04003CA5 RID: 15525
		public float z;
	}
}
