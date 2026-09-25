using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	[DefaultMember("Item")]
	public struct Color
	{
		// Token: 0x06000345 RID: 837 RVA: 0x00007A24 File Offset: 0x00005C24
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00007A44 File Offset: 0x00005C44
		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = 1f;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00007A68 File Offset: 0x00005C68
		public override string ToString()
		{
			return UnityString.Format("RGBA({0:F3}, {1:F3}, {2:F3}, {3:F3})", new object[]
			{
				this.r,
				this.g,
				this.b,
				this.a
			});
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00007AC0 File Offset: 0x00005CC0
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00007AE0 File Offset: 0x00005CE0
		public override bool Equals(object other)
		{
			if (!(other is Color))
			{
				return false;
			}
			Color color = (Color)other;
			return this.r.Equals(color.r) && this.g.Equals(color.g) && this.b.Equals(color.b) && this.a.Equals(color.a);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00007B5C File Offset: 0x00005D5C
		public static Color Lerp(Color a, Color b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00007BDC File Offset: 0x00005DDC
		public static Color red
		{
			get
			{
				return new Color(1f, 0f, 0f, 1f);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public static Color green
		{
			get
			{
				return new Color(0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00007C14 File Offset: 0x00005E14
		public static Color blue
		{
			get
			{
				return new Color(0f, 0f, 1f, 1f);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00007C30 File Offset: 0x00005E30
		public static Color white
		{
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00007C4C File Offset: 0x00005E4C
		public static Color black
		{
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00007C68 File Offset: 0x00005E68
		public static Color yellow
		{
			get
			{
				return new Color(1f, 0.92156863f, 0.015686275f, 1f);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00007C84 File Offset: 0x00005E84
		public static Color cyan
		{
			get
			{
				return new Color(0f, 1f, 1f, 1f);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public static Color magenta
		{
			get
			{
				return new Color(1f, 0f, 1f, 1f);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00007CBC File Offset: 0x00005EBC
		public static Color gray
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public static Color grey
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public static Color operator +(Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00007D44 File Offset: 0x00005F44
		public static Color operator -(Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00007D94 File Offset: 0x00005F94
		public static Color operator *(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00007DE4 File Offset: 0x00005FE4
		public static Color operator *(Color a, float b)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00007E10 File Offset: 0x00006010
		public static bool operator !=(Color lhs, Color rhs)
		{
			return lhs != rhs;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00007E24 File Offset: 0x00006024
		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.r, c.g, c.b, c.a);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00007E48 File Offset: 0x00006048
		public static implicit operator Color(Vector4 v)
		{
			return new Color(v.x, v.y, v.z, v.w);
		}

		// Token: 0x04000055 RID: 85
		public float r;

		// Token: 0x04000056 RID: 86
		public float g;

		// Token: 0x04000057 RID: 87
		public float b;

		// Token: 0x04000058 RID: 88
		public float a;
	}
}
