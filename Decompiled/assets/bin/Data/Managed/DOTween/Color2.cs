using System;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000008 RID: 8
	public struct Color2
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Color2(Color ca, Color cb)
		{
			this.ca = ca;
			this.cb = cb;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		public static Color2 operator +(Color2 c1, Color2 c2)
		{
			return new Color2(c1.ca + c2.ca, c1.cb + c2.cb);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
		public static Color2 operator -(Color2 c1, Color2 c2)
		{
			return new Color2(c1.ca - c2.ca, c1.cb - c2.cb);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B8 File Offset: 0x000002B8
		public static Color2 operator *(Color2 c1, float f)
		{
			return new Color2(c1.ca * f, c1.cb * f);
		}

		// Token: 0x04000010 RID: 16
		public Color ca;

		// Token: 0x04000011 RID: 17
		public Color cb;
	}
}
