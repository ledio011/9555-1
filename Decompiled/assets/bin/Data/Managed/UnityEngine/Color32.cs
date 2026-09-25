using System;

namespace UnityEngine
{
	// Token: 0x0200003C RID: 60
	public struct Color32
	{
		// Token: 0x0600035C RID: 860 RVA: 0x00007E6C File Offset: 0x0000606C
		public Color32(byte r, byte g, byte b, byte a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00007E8C File Offset: 0x0000608C
		public override string ToString()
		{
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r,
				this.g,
				this.b,
				this.a
			});
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00007EE4 File Offset: 0x000060E4
		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)(Mathf.Clamp01(c.r) * 255f), (byte)(Mathf.Clamp01(c.g) * 255f), (byte)(Mathf.Clamp01(c.b) * 255f), (byte)(Mathf.Clamp01(c.a) * 255f));
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00007F44 File Offset: 0x00006144
		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		// Token: 0x04000059 RID: 89
		public byte r;

		// Token: 0x0400005A RID: 90
		public byte g;

		// Token: 0x0400005B RID: 91
		public byte b;

		// Token: 0x0400005C RID: 92
		public byte a;
	}
}
