using System;

namespace System.Globalization
{
	// Token: 0x020000EF RID: 239
	internal class CCGregorianCalendar
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x000242B4 File Offset: 0x000224B4
		public static bool is_leap_year(int year)
		{
			if (CCMath.mod(year, 4) != 0)
			{
				return false;
			}
			int num = CCMath.mod(year, 400);
			return num != 100 && num != 200 && num != 300;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00024308 File Offset: 0x00022508
		public static int fixed_from_dmy(int day, int month, int year)
		{
			int num = 0;
			num += 365 * (year - 1);
			num += CCMath.div(year - 1, 4);
			num -= CCMath.div(year - 1, 100);
			num += CCMath.div(year - 1, 400);
			num += CCMath.div(367 * month - 362, 12);
			if (month > 2)
			{
				num += ((!CCGregorianCalendar.is_leap_year(year)) ? -2 : -1);
			}
			return num + day;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00024388 File Offset: 0x00022588
		public static int year_from_fixed(int date)
		{
			int x = date - 1;
			int num = CCMath.div_mod(out x, x, 146097);
			int num2 = CCMath.div_mod(out x, x, 36524);
			int num3 = CCMath.div_mod(out x, x, 1461);
			int num4 = CCMath.div(x, 365);
			int num5 = 400 * num + 100 * num2 + 4 * num3 + num4;
			return (num2 != 4 && num4 != 4) ? (num5 + 1) : num5;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00024400 File Offset: 0x00022600
		public static void my_from_fixed(out int month, out int year, int date)
		{
			year = CCGregorianCalendar.year_from_fixed(date);
			int num = date - CCGregorianCalendar.fixed_from_dmy(1, 1, year);
			int num2;
			if (date < CCGregorianCalendar.fixed_from_dmy(1, 3, year))
			{
				num2 = 0;
			}
			else if (CCGregorianCalendar.is_leap_year(year))
			{
				num2 = 1;
			}
			else
			{
				num2 = 2;
			}
			month = CCMath.div(12 * (num + num2) + 373, 367);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00024464 File Offset: 0x00022664
		public static void dmy_from_fixed(out int day, out int month, out int year, int date)
		{
			CCGregorianCalendar.my_from_fixed(out month, out year, date);
			day = date - CCGregorianCalendar.fixed_from_dmy(1, month, year) + 1;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00024480 File Offset: 0x00022680
		public static int month_from_fixed(int date)
		{
			int result;
			int num;
			CCGregorianCalendar.my_from_fixed(out result, out num, date);
			return result;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00024498 File Offset: 0x00022698
		public static int day_from_fixed(int date)
		{
			int result;
			int num;
			int num2;
			CCGregorianCalendar.dmy_from_fixed(out result, out num, out num2, date);
			return result;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000244B4 File Offset: 0x000226B4
		public static int GetDayOfMonth(DateTime time)
		{
			return CCGregorianCalendar.day_from_fixed(CCFixed.FromDateTime(time));
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000244C4 File Offset: 0x000226C4
		public static int GetMonth(DateTime time)
		{
			return CCGregorianCalendar.month_from_fixed(CCFixed.FromDateTime(time));
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000244D4 File Offset: 0x000226D4
		public static int GetYear(DateTime time)
		{
			return CCGregorianCalendar.year_from_fixed(CCFixed.FromDateTime(time));
		}

		// Token: 0x0400032F RID: 815
		private const int epoch = 1;
	}
}
