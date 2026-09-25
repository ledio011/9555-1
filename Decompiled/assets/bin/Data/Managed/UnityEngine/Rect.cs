using System;

namespace UnityEngine
{
	// Token: 0x020000DE RID: 222
	public struct Rect
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x00013C58 File Offset: 0x00011E58
		public Rect(float left, float top, float width, float height)
		{
			this.m_XMin = left;
			this.m_YMin = top;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00013C78 File Offset: 0x00011E78
		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00013CB0 File Offset: 0x00011EB0
		public static Rect MinMaxRect(float left, float top, float right, float bottom)
		{
			return new Rect(left, top, right - left, bottom - top);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00013CC0 File Offset: 0x00011EC0
		public void Set(float left, float top, float width, float height)
		{
			this.m_XMin = left;
			this.m_YMin = top;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00013CE0 File Offset: 0x00011EE0
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public float x
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				this.m_XMin = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00013CF4 File Offset: 0x00011EF4
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00013CFC File Offset: 0x00011EFC
		public float y
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				this.m_YMin = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00013D08 File Offset: 0x00011F08
		public Vector2 position
		{
			get
			{
				return new Vector2(this.m_XMin, this.m_YMin);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00013D1C File Offset: 0x00011F1C
		public Vector2 center
		{
			get
			{
				return new Vector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00013D4C File Offset: 0x00011F4C
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00013D54 File Offset: 0x00011F54
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00013D60 File Offset: 0x00011F60
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00013D68 File Offset: 0x00011F68
		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00013D74 File Offset: 0x00011F74
		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_Width, this.m_Height);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00013D88 File Offset: 0x00011F88
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00013D90 File Offset: 0x00011F90
		public float xMin
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				float xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00013DBC File Offset: 0x00011FBC
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00013DC4 File Offset: 0x00011FC4
		public float yMin
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				float yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00013DF0 File Offset: 0x00011FF0
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00013E00 File Offset: 0x00012000
		public float xMax
		{
			get
			{
				return this.m_Width + this.m_XMin;
			}
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00013E10 File Offset: 0x00012010
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00013E20 File Offset: 0x00012020
		public float yMax
		{
			get
			{
				return this.m_Height + this.m_YMin;
			}
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00013E30 File Offset: 0x00012030
		public override string ToString()
		{
			return UnityString.Format("(x:{0:F2}, y:{1:F2}, width:{2:F2}, height:{3:F2})", new object[]
			{
				this.x,
				this.y,
				this.width,
				this.height
			});
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00013E88 File Offset: 0x00012088
		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00013EE0 File Offset: 0x000120E0
		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00013F38 File Offset: 0x00012138
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.width.GetHashCode() << 2 ^ this.y.GetHashCode() >> 2 ^ this.height.GetHashCode() >> 1;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00013F88 File Offset: 0x00012188
		public override bool Equals(object other)
		{
			if (!(other is Rect))
			{
				return false;
			}
			Rect rect = (Rect)other;
			return this.x.Equals(rect.x) && this.y.Equals(rect.y) && this.width.Equals(rect.width) && this.height.Equals(rect.height);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00014010 File Offset: 0x00012210
		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.width != rhs.width || lhs.height != rhs.height;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001406C File Offset: 0x0001226C
		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x04000352 RID: 850
		private float m_XMin;

		// Token: 0x04000353 RID: 851
		private float m_YMin;

		// Token: 0x04000354 RID: 852
		private float m_Width;

		// Token: 0x04000355 RID: 853
		private float m_Height;
	}
}
