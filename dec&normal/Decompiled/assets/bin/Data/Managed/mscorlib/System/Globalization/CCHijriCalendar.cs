using System;

namespace System.Globalization
{
	// Token: 0x020000F2 RID: 242
	internal class CCHijriCalendar
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x00024708 File Offset: 0x00022908
		public static int fixed_from_dmy(int day, int month, int year)
		{
			int num = 227013;
			num += 354 * (year - 1);
			num += CCMath.div(3 + 11 * year, 30);
			num += (int)Math.Ceiling(29.5 * (double)(month - 1));
			return num + day;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00024754 File Offset: 0x00022954
		public static int year_from_fixed(int date)
		{
			return CCMath.div(30 * (date - 227014) + 10646, 10631);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00024770 File Offset: 0x00022970
		public static void my_from_fixed(out int month, out int year, int date)
		{
			year = CCHijriCalendar.year_from_fixed(date);
			int num = 1 + (int)Math.Ceiling((double)(date - 29 - CCHijriCalendar.fixed_from_dmy(1, 1, year)) / 29.5);
			month = ((num >= 12) ? 12 : num);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x000247BC File Offset: 0x000229BC
		public static void dmy_from_fixed(out int day, out int month, out int year, int date)
		{
			CCHijriCalendar.my_from_fixed(out month, out year, date);
			day = date - CCHijriCalendar.fixed_from_dmy(1, month, year) + 1;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000247D8 File Offset: 0x000229D8
		public static int month_from_fixed(int date)
		{
			int result;
			int num;
			CCHijriCalendar.my_from_fixed(out result, out num, date);
			return result;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000247F0 File Offset: 0x000229F0
		public static int day_from_fixed(int date)
		{
			int result;
			int num;
			int num2;
			CCHijriCalendar.dmy_from_fixed(out result, out num, out num2, date);
			return result;
		}

		// Token: 0x04000336 RID: 822
		private const int epoch = 227014;
	}
}
