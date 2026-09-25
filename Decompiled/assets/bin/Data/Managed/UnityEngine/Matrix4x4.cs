using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B1 RID: 177
	public struct Matrix4x4
	{
		// Token: 0x17000195 RID: 405
		public float this[int row, int column]
		{
			get
			{
				return this[row + column * 4];
			}
			set
			{
				this[row + column * 4] = value;
			}
		}

		// Token: 0x17000196 RID: 406
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.m00;
				case 1:
					return this.m10;
				case 2:
					return this.m20;
				case 3:
					return this.m30;
				case 4:
					return this.m01;
				case 5:
					return this.m11;
				case 6:
					return this.m21;
				case 7:
					return this.m31;
				case 8:
					return this.m02;
				case 9:
					return this.m12;
				case 10:
					return this.m22;
				case 11:
					return this.m32;
				case 12:
					return this.m03;
				case 13:
					return this.m13;
				case 14:
					return this.m23;
				case 15:
					return this.m33;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.m00 = value;
					break;
				case 1:
					this.m10 = value;
					break;
				case 2:
					this.m20 = value;
					break;
				case 3:
					this.m30 = value;
					break;
				case 4:
					this.m01 = value;
					break;
				case 5:
					this.m11 = value;
					break;
				case 6:
					this.m21 = value;
					break;
				case 7:
					this.m31 = value;
					break;
				case 8:
					this.m02 = value;
					break;
				case 9:
					this.m12 = value;
					break;
				case 10:
					this.m22 = value;
					break;
				case 11:
					this.m32 = value;
					break;
				case 12:
					this.m03 = value;
					break;
				case 13:
					this.m13 = value;
					break;
				case 14:
					this.m23 = value;
					break;
				case 15:
					this.m33 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000122D0 File Offset: 0x000104D0
		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ this.GetColumn(1).GetHashCode() << 2 ^ this.GetColumn(2).GetHashCode() >> 2 ^ this.GetColumn(3).GetHashCode() >> 1;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00012324 File Offset: 0x00010524
		public override bool Equals(object other)
		{
			if (!(other is Matrix4x4))
			{
				return false;
			}
			Matrix4x4 matrix4x = (Matrix4x4)other;
			return this.GetColumn(0).Equals(matrix4x.GetColumn(0)) && this.GetColumn(1).Equals(matrix4x.GetColumn(1)) && this.GetColumn(2).Equals(matrix4x.GetColumn(2)) && this.GetColumn(3).Equals(matrix4x.GetColumn(3));
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000123C8 File Offset: 0x000105C8
		public Vector4 GetColumn(int i)
		{
			return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000123F0 File Offset: 0x000105F0
		public void SetRow(int i, Vector4 v)
		{
			this[i, 0] = v.x;
			this[i, 1] = v.y;
			this[i, 2] = v.z;
			this[i, 3] = v.w;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00012430 File Offset: 0x00010630
		public Vector3 MultiplyPoint(Vector3 v)
		{
			Vector3 result;
			result.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z + this.m03;
			result.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z + this.m13;
			result.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z + this.m23;
			float num = this.m30 * v.x + this.m31 * v.y + this.m32 * v.z + this.m33;
			num = 1f / num;
			result.x *= num;
			result.y *= num;
			result.z *= num;
			return result;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00012558 File Offset: 0x00010758
		public Vector3 MultiplyPoint3x4(Vector3 v)
		{
			Vector3 result;
			result.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z + this.m03;
			result.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z + this.m13;
			result.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z + this.m23;
			return result;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00012614 File Offset: 0x00010814
		public Vector3 MultiplyVector(Vector3 v)
		{
			Vector3 result;
			result.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z;
			result.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z;
			result.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z;
			return result;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000126BC File Offset: 0x000108BC
		public static Matrix4x4 Scale(Vector3 v)
		{
			return new Matrix4x4
			{
				m00 = v.x,
				m01 = 0f,
				m02 = 0f,
				m03 = 0f,
				m10 = 0f,
				m11 = v.y,
				m12 = 0f,
				m13 = 0f,
				m20 = 0f,
				m21 = 0f,
				m22 = v.z,
				m23 = 0f,
				m30 = 0f,
				m31 = 0f,
				m32 = 0f,
				m33 = 1f
			};
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00012798 File Offset: 0x00010998
		public static Matrix4x4 identity
		{
			get
			{
				return new Matrix4x4
				{
					m00 = 1f,
					m01 = 0f,
					m02 = 0f,
					m03 = 0f,
					m10 = 0f,
					m11 = 1f,
					m12 = 0f,
					m13 = 0f,
					m20 = 0f,
					m21 = 0f,
					m22 = 1f,
					m23 = 0f,
					m30 = 0f,
					m31 = 0f,
					m32 = 0f,
					m33 = 1f
				};
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00012870 File Offset: 0x00010A70
		public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			return Matrix4x4.INTERNAL_CALL_TRS(ref pos, ref q, ref s);
		}

		// Token: 0x06000799 RID: 1945
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Matrix4x4 INTERNAL_CALL_TRS(ref Vector3 pos, ref Quaternion q, ref Vector3 s);

		// Token: 0x0600079A RID: 1946 RVA: 0x00012880 File Offset: 0x00010A80
		public override string ToString()
		{
			return UnityString.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", new object[]
			{
				this.m00,
				this.m01,
				this.m02,
				this.m03,
				this.m10,
				this.m11,
				this.m12,
				this.m13,
				this.m20,
				this.m21,
				this.m22,
				this.m23,
				this.m30,
				this.m31,
				this.m32,
				this.m33
			});
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00012988 File Offset: 0x00010B88
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return new Matrix4x4
			{
				m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30,
				m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31,
				m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32,
				m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33,
				m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30,
				m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31,
				m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32,
				m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33,
				m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30,
				m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31,
				m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32,
				m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33,
				m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30,
				m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31,
				m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32,
				m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33
			};
		}

		// Token: 0x040002EA RID: 746
		public float m00;

		// Token: 0x040002EB RID: 747
		public float m10;

		// Token: 0x040002EC RID: 748
		public float m20;

		// Token: 0x040002ED RID: 749
		public float m30;

		// Token: 0x040002EE RID: 750
		public float m01;

		// Token: 0x040002EF RID: 751
		public float m11;

		// Token: 0x040002F0 RID: 752
		public float m21;

		// Token: 0x040002F1 RID: 753
		public float m31;

		// Token: 0x040002F2 RID: 754
		public float m02;

		// Token: 0x040002F3 RID: 755
		public float m12;

		// Token: 0x040002F4 RID: 756
		public float m22;

		// Token: 0x040002F5 RID: 757
		public float m32;

		// Token: 0x040002F6 RID: 758
		public float m03;

		// Token: 0x040002F7 RID: 759
		public float m13;

		// Token: 0x040002F8 RID: 760
		public float m23;

		// Token: 0x040002F9 RID: 761
		public float m33;
	}
}
