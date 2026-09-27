using System;

namespace System.Globalization
{
	// Token: 0x020000F3 RID: 243
	internal class CCMath
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x0002480C File Offset: 0x00022A0C
		public static int div(int x, int y)
		{
			return (int)Math.Floor((double)x / (double)y);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002481C File Offset: 0x00022A1C
		public static int mod(int x, int y)
		{
			return x - y * CCMath.div(x, y);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0002482C File Offset: 0x00022A2C
		public static int div_mod(out int remainder, int x, int y)
		{
			int num = CCMath.div(x, y);
			remainder = x - y * num;
			return num;
		}
	}
}
