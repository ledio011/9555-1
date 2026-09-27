using System;

namespace System.Globalization
{
	// Token: 0x020000EE RID: 238
	internal class CCFixed
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x00024290 File Offset: 0x00022490
		public static int FromDateTime(DateTime time)
		{
			return 1 + (int)(time.Ticks / 864000000000L);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x000242A8 File Offset: 0x000224A8
		public static DayOfWeek day_of_week(int date)
		{
			return (DayOfWeek)CCMath.mod(date, 7);
		}
	}
}
