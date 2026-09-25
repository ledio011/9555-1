using System;
using UnityEngine;

// Token: 0x02000A8E RID: 2702
public static class vp_TimeUtility
{
	// Token: 0x06004E96 RID: 20118 RVA: 0x001AEBC8 File Offset: 0x001ACDC8
	public static vp_TimeUtility.Units TimeToUnits(float timeInSeconds)
	{
		vp_TimeUtility.Units result = default(vp_TimeUtility.Units);
		result.hours = (int)timeInSeconds / 3600;
		result.minutes = ((int)timeInSeconds - result.hours * 3600) / 60;
		result.seconds = (int)timeInSeconds % 60;
		result.deciSeconds = (int)((timeInSeconds - (float)result.seconds) * 10f) % 60;
		result.centiSeconds = (int)((timeInSeconds - (float)result.seconds) * 100f % 600f);
		result.milliSeconds = (int)((timeInSeconds - (float)result.seconds) * 1000f % 6000f);
		return result;
	}

	// Token: 0x06004E97 RID: 20119 RVA: 0x001AEC6C File Offset: 0x001ACE6C
	public static float UnitsToSeconds(vp_TimeUtility.Units units)
	{
		float num = 0f;
		num += (float)(units.hours * 3600);
		num += (float)(units.minutes * 60);
		num += (float)units.seconds;
		num += (float)units.deciSeconds * 0.1f;
		num += (float)(units.centiSeconds / 100);
		return num + (float)(units.milliSeconds / 1000);
	}

	// Token: 0x06004E98 RID: 20120 RVA: 0x001AECDC File Offset: 0x001ACEDC
	public static string TimeToString(float timeInSeconds, bool showHours, bool showMinutes, bool showSeconds, bool showTenths, bool showHundredths, bool showMilliSeconds, char delimiter = ':')
	{
		vp_TimeUtility.Units units = vp_TimeUtility.TimeToUnits(timeInSeconds);
		string text = (units.hours >= 10) ? units.hours.ToString() : ("0" + units.hours.ToString());
		string text2 = (units.minutes >= 10) ? units.minutes.ToString() : ("0" + units.minutes.ToString());
		string text3 = (units.seconds >= 10) ? units.seconds.ToString() : ("0" + units.seconds.ToString());
		string text4 = units.deciSeconds.ToString();
		string text5 = (units.centiSeconds >= 10) ? units.centiSeconds.ToString() : ("0" + units.centiSeconds.ToString());
		string text6 = (units.milliSeconds >= 100) ? units.milliSeconds.ToString() : ("0" + units.milliSeconds.ToString());
		text6 = ((units.milliSeconds >= 10) ? text6 : ("0" + text6));
		return string.Concat(new string[]
		{
			(!showHours) ? string.Empty : text,
			(!showMinutes) ? string.Empty : (delimiter + text2),
			(!showSeconds) ? string.Empty : (delimiter + text3),
			(!showTenths) ? string.Empty : (delimiter + text4),
			(!showHundredths) ? string.Empty : (delimiter + text5),
			(!showMilliSeconds) ? string.Empty : (delimiter + text6)
		}).TrimStart(new char[]
		{
			delimiter
		});
	}

	// Token: 0x06004E99 RID: 20121 RVA: 0x001AEF08 File Offset: 0x001AD108
	public static string SystemTimeToString(DateTime systemTime, bool showHours, bool showMinutes, bool showSeconds, bool showTenths, bool showHundredths, bool showMilliSeconds, char delimiter = ':')
	{
		return vp_TimeUtility.TimeToString(vp_TimeUtility.SystemTimeToSeconds(systemTime), showHours, showMinutes, showSeconds, showTenths, showHundredths, showMilliSeconds, delimiter);
	}

	// Token: 0x06004E9A RID: 20122 RVA: 0x001AEF2C File Offset: 0x001AD12C
	public static string SystemTimeToString(bool showHours, bool showMinutes, bool showSeconds, bool showTenths, bool showHundredths, bool showMilliSeconds, char delimiter = ':')
	{
		return vp_TimeUtility.SystemTimeToString(DateTime.Now, showHours, showMinutes, showSeconds, showTenths, showHundredths, showMilliSeconds, delimiter);
	}

	// Token: 0x06004E9B RID: 20123 RVA: 0x001AEF50 File Offset: 0x001AD150
	public static vp_TimeUtility.Units SystemTimeToUnits(DateTime systemTime)
	{
		return new vp_TimeUtility.Units
		{
			hours = systemTime.Hour,
			minutes = systemTime.Minute,
			seconds = systemTime.Second,
			deciSeconds = (int)((float)systemTime.Millisecond / 100f),
			centiSeconds = systemTime.Millisecond / 10,
			milliSeconds = systemTime.Millisecond
		};
	}

	// Token: 0x06004E9C RID: 20124 RVA: 0x001AEFC8 File Offset: 0x001AD1C8
	public static vp_TimeUtility.Units SystemTimeToUnits()
	{
		return vp_TimeUtility.SystemTimeToUnits(DateTime.Now);
	}

	// Token: 0x06004E9D RID: 20125 RVA: 0x001AEFD4 File Offset: 0x001AD1D4
	public static float SystemTimeToSeconds(DateTime systemTime)
	{
		return vp_TimeUtility.UnitsToSeconds(vp_TimeUtility.SystemTimeToUnits(systemTime));
	}

	// Token: 0x06004E9E RID: 20126 RVA: 0x001AEFE4 File Offset: 0x001AD1E4
	public static float SystemTimeToSeconds()
	{
		return vp_TimeUtility.SystemTimeToSeconds(DateTime.Now);
	}

	// Token: 0x06004E9F RID: 20127 RVA: 0x001AEFF0 File Offset: 0x001AD1F0
	public static float TimeToDegrees(float seconds, bool includeHours = false, bool includeMinutes = false, bool includeSeconds = true, bool includeMilliSeconds = true)
	{
		vp_TimeUtility.Units units = vp_TimeUtility.TimeToUnits(seconds);
		if (includeHours && includeMinutes && includeSeconds)
		{
			return vp_TimeUtility.HoursToDegreesInternal((float)units.hours, (float)units.minutes, (float)units.seconds);
		}
		if (includeHours && includeMinutes)
		{
			return vp_TimeUtility.HoursToDegreesInternal((float)units.hours, (float)units.minutes, 0f);
		}
		if (includeMinutes && includeSeconds && includeMilliSeconds)
		{
			return vp_TimeUtility.MinutesToDegreesInternal((float)units.minutes, (float)units.seconds, (float)units.milliSeconds);
		}
		if (includeMinutes && includeSeconds)
		{
			return vp_TimeUtility.MinutesToDegreesInternal((float)units.minutes, (float)units.seconds, 0f);
		}
		if (includeSeconds && includeMilliSeconds)
		{
			return vp_TimeUtility.SecondsToDegreesInternal((float)units.seconds, (float)units.milliSeconds);
		}
		if (includeHours)
		{
			return vp_TimeUtility.HoursToDegreesInternal((float)units.hours, 0f, 0f);
		}
		if (includeMinutes)
		{
			return vp_TimeUtility.MinutesToDegreesInternal((float)units.minutes, 0f, 0f);
		}
		if (includeSeconds)
		{
			return vp_TimeUtility.TimeToDegrees((float)units.seconds, false, false, true, true);
		}
		if (includeMilliSeconds)
		{
			return vp_TimeUtility.MilliSecondsToDegreesInternal((float)units.milliSeconds);
		}
		Debug.LogError("Error: (vp_TimeUtility.TimeToDegrees) This combination of time units is not supported.");
		return 0f;
	}

	// Token: 0x06004EA0 RID: 20128 RVA: 0x001AF150 File Offset: 0x001AD350
	public static Vector3 SystemTimeToDegrees(DateTime time, bool smooth = true)
	{
		return new Vector3(vp_TimeUtility.HoursToDegreesInternal((float)time.Hour, (!smooth) ? 0f : ((float)time.Minute), (!smooth) ? 0f : ((float)time.Second)), vp_TimeUtility.MinutesToDegreesInternal((float)time.Minute, (!smooth) ? 0f : ((float)time.Second), (!smooth) ? 0f : ((float)time.Millisecond)), vp_TimeUtility.SecondsToDegreesInternal((float)time.Second, (!smooth) ? 0f : ((float)time.Millisecond)));
	}

	// Token: 0x06004EA1 RID: 20129 RVA: 0x001AF204 File Offset: 0x001AD404
	public static Vector3 SystemTimeToDegrees(bool smooth = true)
	{
		return vp_TimeUtility.SystemTimeToDegrees(DateTime.Now, smooth);
	}

	// Token: 0x06004EA2 RID: 20130 RVA: 0x001AF214 File Offset: 0x001AD414
	private static float HoursToDegreesInternal(float hours, float minutes = 0f, float seconds = 0f)
	{
		return hours * 30f + minutes * 0.5f + seconds * 0.008333333f;
	}

	// Token: 0x06004EA3 RID: 20131 RVA: 0x001AF230 File Offset: 0x001AD430
	private static float MinutesToDegreesInternal(float minutes, float seconds = 0f, float milliSeconds = 0f)
	{
		return minutes * 6f + seconds * 0.1f + milliSeconds * 0.0001f;
	}

	// Token: 0x06004EA4 RID: 20132 RVA: 0x001AF24C File Offset: 0x001AD44C
	private static float SecondsToDegreesInternal(float seconds, float milliSeconds = 0f)
	{
		return seconds * 6f + milliSeconds * 0.006f;
	}

	// Token: 0x06004EA5 RID: 20133 RVA: 0x001AF260 File Offset: 0x001AD460
	private static float MilliSecondsToDegreesInternal(float milliSeconds)
	{
		return milliSeconds * 0.36f;
	}

	// Token: 0x02000A8F RID: 2703
	public struct Units
	{
		// Token: 0x04003D2F RID: 15663
		public int hours;

		// Token: 0x04003D30 RID: 15664
		public int minutes;

		// Token: 0x04003D31 RID: 15665
		public int seconds;

		// Token: 0x04003D32 RID: 15666
		public int deciSeconds;

		// Token: 0x04003D33 RID: 15667
		public int centiSeconds;

		// Token: 0x04003D34 RID: 15668
		public int milliSeconds;
	}
}
