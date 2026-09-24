using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x020008BF RID: 2239
public class TimeTools
{
	// Token: 0x06003C4C RID: 15436 RVA: 0x00107D34 File Offset: 0x00105F34
	public static TimeSpan GetLocalShowTime(long startTime, long offsetTime)
	{
		TimeSpan timeSpan;
		timeSpan..ctor(startTime * TimeTools.SECONDS_TO_TICKS);
		TimeSpan timeSpan2;
		timeSpan2..ctor(offsetTime * TimeTools.SECONDS_TO_TICKS * 3600L);
		timeSpan += timeSpan2;
		timeSpan += TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
		timeSpan..ctor((864000000000L + timeSpan.Ticks) % 864000000000L);
		return timeSpan;
	}

	// Token: 0x06003C4D RID: 15437 RVA: 0x00107DA8 File Offset: 0x00105FA8
	public static TimeSpan GetLocalShowTime(long[] startTime, long offsetTime)
	{
		TimeSpan timeSpan;
		timeSpan..ctor(startTime[0] * TimeTools.SECONDS_TO_TICKS);
		TimeSpan timeSpan2;
		timeSpan2..ctor(offsetTime * TimeTools.SECONDS_TO_TICKS * 3600L);
		timeSpan += timeSpan2;
		timeSpan += TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
		timeSpan..ctor((864000000000L + timeSpan.Ticks) % 864000000000L);
		return timeSpan;
	}

	// Token: 0x06003C4E RID: 15438 RVA: 0x00107E1C File Offset: 0x0010601C
	public static int GetOffsetDay(long startTime, long offsetTime)
	{
		TimeSpan timeSpan;
		timeSpan..ctor(startTime * TimeTools.SECONDS_TO_TICKS);
		TimeSpan timeSpan2;
		timeSpan2..ctor(offsetTime * TimeTools.SECONDS_TO_TICKS * 3600L);
		timeSpan += timeSpan2;
		timeSpan += TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
		if (timeSpan.Ticks > 0L)
		{
			return (int)(timeSpan.Ticks / 864000000000L);
		}
		return -1 + (int)(timeSpan.Ticks / 864000000000L);
	}

	// Token: 0x06003C4F RID: 15439 RVA: 0x00107EA0 File Offset: 0x001060A0
	public static List<TimeSpan> GetLocalShowTime(long[] startTime, long offsetTime, long durationtime, int next, bool isshownext = false)
	{
		if (startTime == null || startTime.Length == 0)
		{
			return null;
		}
		List<TimeSpan> list = new List<TimeSpan>();
		TimeSpan timeSpan;
		timeSpan..ctor(offsetTime * TimeTools.SECONDS_TO_TICKS * 3600L);
		int num = 0;
		if (next > 0)
		{
			num = next - 1;
		}
		if (isshownext)
		{
			num = (num + 1) % startTime.Length;
		}
		TimeSpan timeSpan2;
		timeSpan2..ctor(startTime[num] * TimeTools.SECONDS_TO_TICKS);
		timeSpan2 += timeSpan;
		timeSpan2 += TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
		timeSpan2..ctor((864000000000L + timeSpan2.Ticks) % 864000000000L);
		list.Add(timeSpan2);
		timeSpan2..ctor((startTime[num] + durationtime) * TimeTools.SECONDS_TO_TICKS);
		timeSpan2 += timeSpan;
		timeSpan2 += TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
		timeSpan2..ctor((864000000000L + timeSpan2.Ticks) % 864000000000L);
		list.Add(timeSpan2);
		return list;
	}

	// Token: 0x06003C50 RID: 15440 RVA: 0x00107FA8 File Offset: 0x001061A8
	public static TimeSpan GetShopItemTime(int[] endtimes)
	{
		if (endtimes == null || endtimes.Length != 6)
		{
			return new TimeSpan(0, 0, 0);
		}
		long num = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.TimeOffset * 3600L;
		long curServerTime = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime();
		DateTime dateTime;
		dateTime..ctor(endtimes[0], endtimes[1], endtimes[2], endtimes[3], endtimes[4], endtimes[5]);
		TimeSpan result;
		result..ctor(dateTime.Subtract(new DateTime(1970, 1, 1)).Ticks - curServerTime * TimeTools.SECONDS_TO_TICKS + num * TimeTools.SECONDS_TO_TICKS);
		return result;
	}

	// Token: 0x06003C51 RID: 15441 RVA: 0x00108044 File Offset: 0x00106244
	public static bool IsTimeRange(int[] starttimes, int[] endtimes)
	{
		if (endtimes == null || endtimes.Length != 6 || starttimes == null || starttimes.Length != 6)
		{
			return false;
		}
		DateTime dateTime;
		dateTime..ctor(starttimes[0], starttimes[1], starttimes[2], starttimes[3], starttimes[4], starttimes[5]);
		DateTime dateTime2;
		dateTime2..ctor(endtimes[0], endtimes[1], endtimes[2], endtimes[3], endtimes[4], endtimes[5]);
		TimeSpan timeSpan = dateTime.Subtract(new DateTime(1970, 1, 1));
		TimeSpan timeSpan2 = dateTime2.Subtract(new DateTime(1970, 1, 1));
		long num = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.TimeOffset * 3600L;
		long num2 = (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime() - num) * TimeTools.SECONDS_TO_TICKS;
		return timeSpan.Ticks <= num2 && num2 < timeSpan2.Ticks;
	}

	// Token: 0x06003C52 RID: 15442 RVA: 0x0010811C File Offset: 0x0010631C
	public static bool IsTimeRange(int[] starttimes)
	{
		if (starttimes == null || starttimes.Length != 6)
		{
			return false;
		}
		DateTime dateTime;
		dateTime..ctor(starttimes[0], starttimes[1], starttimes[2], starttimes[3], starttimes[4], starttimes[5]);
		TimeSpan timeSpan = dateTime.Subtract(new DateTime(1970, 1, 1));
		long num = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.TimeOffset * 3600L;
		long num2 = (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime() - num) * TimeTools.SECONDS_TO_TICKS;
		return timeSpan.Ticks <= num2;
	}

	// Token: 0x06003C53 RID: 15443 RVA: 0x001081A8 File Offset: 0x001063A8
	public static bool IsTimeRange(long starttime, long endtime)
	{
		TimeSpan timeSpan;
		timeSpan..ctor(starttime * TimeTools.SECONDS_TO_TICKS);
		timeSpan..ctor((864000000000L + timeSpan.Ticks) % 864000000000L);
		TimeSpan timeSpan2;
		timeSpan2..ctor(endtime * TimeTools.SECONDS_TO_TICKS);
		timeSpan2..ctor((864000000000L + timeSpan2.Ticks) % 864000000000L);
		long num = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.TimeOffset * 3600L;
		long num2 = (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime() - num) * TimeTools.SECONDS_TO_TICKS;
		TimeSpan timeSpan3;
		timeSpan3..ctor((864000000000L + num2) % 864000000000L);
		return timeSpan.Ticks <= timeSpan3.Ticks && timeSpan3.Ticks <= timeSpan2.Ticks;
	}

	// Token: 0x06003C54 RID: 15444 RVA: 0x0010828C File Offset: 0x0010648C
	public static string GetLocalShowTime_HM(long startTime, long offsetTime)
	{
		TimeSpan timeSpan;
		timeSpan..ctor(startTime * TimeTools.SECONDS_TO_TICKS);
		TimeSpan timeSpan2;
		timeSpan2..ctor(offsetTime * TimeTools.SECONDS_TO_TICKS * 3600L);
		timeSpan += timeSpan2;
		timeSpan += TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
		timeSpan..ctor((864000000000L + timeSpan.Ticks) % 864000000000L);
		return string.Format("{0:D2}:{1:D2}", timeSpan.Hours, timeSpan.Minutes);
	}

	// Token: 0x06003C55 RID: 15445 RVA: 0x00108320 File Offset: 0x00106520
	public static int GetPassDays(long GetTime, long GetCurServerTime)
	{
		long num = GetCurServerTime - GetTime;
		TimeSpan timeSpan;
		timeSpan..ctor(0, 0, (int)num);
		return (int)timeSpan.TotalDays;
	}

	// Token: 0x06003C56 RID: 15446 RVA: 0x00108344 File Offset: 0x00106544
	public static string GetLocalTime(long GetTime, long GetCurServerTime)
	{
		long num = GetTime - GetCurServerTime;
		TimeSpan timeSpan;
		timeSpan..ctor(0, 0, (int)num);
		DateTime dateTime = DateTime.Now.AddSeconds((double)num);
		return string.Format("{0:D2}:{1:D2}", dateTime.Hour, dateTime.Minute);
	}

	// Token: 0x06003C57 RID: 15447 RVA: 0x00108394 File Offset: 0x00106594
	public static string GetFormateTime(long time)
	{
		TimeSpan timeSpan;
		timeSpan..ctor(time * TimeTools.SECONDS_TO_TICKS);
		if (timeSpan.Days > 0 && timeSpan.Hours > 0)
		{
			return string.Format("{0} {1} {2} {3}", new object[]
			{
				timeSpan.Days,
				StrDictionary.GetDictionaryString("#{100644}", new object[0]),
				timeSpan.Hours,
				StrDictionary.GetDictionaryString("#{100645}", new object[0])
			});
		}
		if (timeSpan.Days > 0)
		{
			return string.Format("{0} {1}", timeSpan.Days, StrDictionary.GetDictionaryString("#{100644}", new object[0]));
		}
		if (timeSpan.Hours > 0)
		{
			return string.Format("{0} {1}", timeSpan.Hours, StrDictionary.GetDictionaryString("#{100645}", new object[0]));
		}
		return string.Format("{0} {1}", 1, StrDictionary.GetDictionaryString("#{100645}", new object[0]));
	}

	// Token: 0x06003C58 RID: 15448 RVA: 0x001084A8 File Offset: 0x001066A8
	public static string GetMinuteSecondStr(int second)
	{
		int num = second % 60;
		int num2 = second / 60;
		TimeTools.sb.Length = 0;
		TimeTools.sb.AppendFormat("{0:D2}:{1:D2}", num2, num);
		return TimeTools.sb.ToString();
	}

	// Token: 0x06003C59 RID: 15449 RVA: 0x001084F0 File Offset: 0x001066F0
	public static string GetMinuteSecondStr(long second)
	{
		return TimeTools.GetMinuteSecondStr((int)second);
	}

	// Token: 0x06003C5A RID: 15450 RVA: 0x001084FC File Offset: 0x001066FC
	public static string GetCentiSecondStr(int centisecond)
	{
		int num = centisecond % 100;
		int num2 = centisecond / 100 % 60;
		int num3 = centisecond / 100 / 60;
		TimeTools.sb.Length = 0;
		TimeTools.sb.AppendFormat("{0:D2}:{1:D2}:{2:D2}", num3, num2, num);
		return TimeTools.sb.ToString();
	}

	// Token: 0x06003C5B RID: 15451 RVA: 0x00108558 File Offset: 0x00106758
	public static string GetHourMinSecStr(long times)
	{
		long num = times / 3600L;
		times %= 3600L;
		long num2 = times / 60L;
		long num3 = times % 60L;
		TimeTools.sb.Length = 0;
		TimeTools.sb.AppendFormat("{0:D2}:{1:D2}:{2:D2}", num, num2, num3);
		return TimeTools.sb.ToString();
	}

	// Token: 0x06003C5C RID: 15452 RVA: 0x001085BC File Offset: 0x001067BC
	public static string GetDaySecondStr(long times)
	{
		long num = times / TimeTools.DAY_SECOND;
		if (num > 0L)
		{
			TimeTools.sb.Length = 0;
			TimeTools.sb.AppendFormat("{0} {1}", num, StrDictionary.GetDictionaryString("#{100644}", new object[0]));
			return TimeTools.sb.ToString();
		}
		times %= TimeTools.DAY_SECOND;
		long num2 = times / 3600L;
		times %= 3600L;
		long num3 = times / 60L;
		long num4 = times % 60L;
		TimeTools.sb.Length = 0;
		TimeTools.sb.AppendFormat("{0:D2}:{1:D2}:{2:D2}", num2, num3, num4);
		return TimeTools.sb.ToString();
	}

	// Token: 0x06003C5D RID: 15453 RVA: 0x00108674 File Offset: 0x00106874
	public static string GetFullTime(long times)
	{
		TimeSpan timeSpan;
		timeSpan..ctor(0, 0, (int)times);
		if (timeSpan.Days > 0)
		{
			return string.Format("{0}{1} {2:d2}:{3:d2}:{4:d2}", new object[]
			{
				timeSpan.Days,
				StrDictionary.GetDictionaryString("#{100644}", new object[0]),
				timeSpan.Hours,
				timeSpan.Minutes,
				timeSpan.Seconds
			});
		}
		return string.Format("{0:d2}:{1:d2}:{2:d2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
	}

	// Token: 0x04002786 RID: 10118
	private const string strMinFormate = "{0:D2}:{1:D2}";

	// Token: 0x04002787 RID: 10119
	private const string strformate = "{0:D2}:{1:D2}:{2:D2}";

	// Token: 0x04002788 RID: 10120
	private static StringBuilder sb = new StringBuilder(512);

	// Token: 0x04002789 RID: 10121
	public static long SECONDS_TO_TICKS = 10000000L;

	// Token: 0x0400278A RID: 10122
	public static long DAY_SECOND = 86400L;
}
