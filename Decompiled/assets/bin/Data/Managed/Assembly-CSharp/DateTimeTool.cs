using System;

// Token: 0x020008AF RID: 2223
public class DateTimeTool
{
	// Token: 0x06003BE5 RID: 15333 RVA: 0x00105690 File Offset: 0x00103890
	public static int CalculateWorkingDays(DateTime dtStart, DateTime dtEnd)
	{
		return (int)(dtEnd - dtStart).TotalDays;
	}

	// Token: 0x06003BE6 RID: 15334 RVA: 0x001056B0 File Offset: 0x001038B0
	public static int CalculateHours(DateTime dtStart, DateTime dtEnd)
	{
		return (int)(dtEnd - dtStart).TotalHours;
	}

	// Token: 0x06003BE7 RID: 15335 RVA: 0x001056D0 File Offset: 0x001038D0
	public static DateTime LongToDateTimeLocal(long time)
	{
		DateTime dateTime;
		dateTime..ctor(1970, 1, 1);
		DateTime dateTime2;
		dateTime2..ctor(time * 10000000L, 1);
		dateTime2 = dateTime2.ToLocalTime();
		DateTime result;
		result..ctor(time * 10000000L + dateTime.Ticks);
		result = result.ToLocalTime();
		return result;
	}

	// Token: 0x06003BE8 RID: 15336 RVA: 0x00105724 File Offset: 0x00103924
	public static double CalculateSeconds(DateTime dtStart, DateTime dtEnd)
	{
		return (dtEnd - dtStart).TotalSeconds;
	}

	// Token: 0x06003BE9 RID: 15337 RVA: 0x00105740 File Offset: 0x00103940
	public static string GetTimeByLong(long time)
	{
		long num = time / 3600L;
		long num2 = time % 3600L / 60L;
		time %= 60L;
		return string.Format("{0:D2}:{1:D2}:{2:D2}", num, num2, time);
	}
}
