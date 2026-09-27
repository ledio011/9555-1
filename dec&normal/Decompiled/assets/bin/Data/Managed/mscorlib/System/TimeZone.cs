using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020003CC RID: 972
	[ComVisible(true)]
	[Serializable]
	public abstract class TimeZone
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0006F858 File Offset: 0x0006DA58
		public static TimeZone CurrentTimeZone
		{
			get
			{
				return TimeZone.currentTimeZone;
			}
		}

		// Token: 0x06001D8A RID: 7562
		public abstract DaylightTime GetDaylightChanges(int year);

		// Token: 0x06001D8B RID: 7563
		public abstract TimeSpan GetUtcOffset(DateTime time);

		// Token: 0x06001D8C RID: 7564 RVA: 0x0006F860 File Offset: 0x0006DA60
		public virtual bool IsDaylightSavingTime(DateTime time)
		{
			return TimeZone.IsDaylightSavingTime(time, this.GetDaylightChanges(time.Year));
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0006F878 File Offset: 0x0006DA78
		public static bool IsDaylightSavingTime(DateTime time, DaylightTime daylightTimes)
		{
			if (daylightTimes == null)
			{
				throw new ArgumentNullException("daylightTimes");
			}
			if (daylightTimes.Start.Ticks == daylightTimes.End.Ticks)
			{
				return false;
			}
			if (daylightTimes.Start.Ticks < daylightTimes.End.Ticks)
			{
				if (daylightTimes.Start.Ticks < time.Ticks && daylightTimes.End.Ticks > time.Ticks)
				{
					return true;
				}
			}
			else if (time.Year == daylightTimes.Start.Year && time.Year == daylightTimes.End.Year && (time.Ticks < daylightTimes.End.Ticks || time.Ticks > daylightTimes.Start.Ticks))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0006F988 File Offset: 0x0006DB88
		public virtual DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			TimeSpan utcOffset = this.GetUtcOffset(time);
			if (utcOffset.Ticks > 0L)
			{
				if (DateTime.MaxValue - utcOffset < time)
				{
					return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Local);
				}
			}
			else if (utcOffset.Ticks < 0L && time.Ticks + utcOffset.Ticks < DateTime.MinValue.Ticks)
			{
				return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Local);
			}
			DateTime dateTime = time.Add(utcOffset);
			DaylightTime daylightChanges = this.GetDaylightChanges(time.Year);
			if (daylightChanges.Delta.Ticks == 0L)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
			}
			if (dateTime < daylightChanges.End && daylightChanges.End.Subtract(daylightChanges.Delta) <= dateTime)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
			}
			TimeSpan utcOffset2 = this.GetUtcOffset(dateTime);
			return DateTime.SpecifyKind(time.Add(utcOffset2), DateTimeKind.Local);
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0006FAA0 File Offset: 0x0006DCA0
		public virtual DateTime ToUniversalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return time;
			}
			TimeSpan utcOffset = this.GetUtcOffset(time);
			if (utcOffset.Ticks < 0L)
			{
				if (DateTime.MaxValue + utcOffset < time)
				{
					return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
				}
			}
			else if (utcOffset.Ticks > 0L && DateTime.MinValue + utcOffset > time)
			{
				return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
			}
			return DateTime.SpecifyKind(new DateTime(time.Ticks - utcOffset.Ticks), DateTimeKind.Utc);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0006FB44 File Offset: 0x0006DD44
		internal TimeSpan GetLocalTimeDiff(DateTime time)
		{
			return this.GetLocalTimeDiff(time, this.GetUtcOffset(time));
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0006FB54 File Offset: 0x0006DD54
		internal TimeSpan GetLocalTimeDiff(DateTime time, TimeSpan utc_offset)
		{
			DaylightTime daylightChanges = this.GetDaylightChanges(time.Year);
			if (daylightChanges.Delta.Ticks == 0L)
			{
				return utc_offset;
			}
			DateTime dateTime = time.Add(utc_offset);
			if (dateTime < daylightChanges.End && daylightChanges.End.Subtract(daylightChanges.Delta) <= dateTime)
			{
				return utc_offset;
			}
			if (dateTime >= daylightChanges.Start && daylightChanges.Start.Add(daylightChanges.Delta) > dateTime)
			{
				return utc_offset - daylightChanges.Delta;
			}
			return this.GetUtcOffset(dateTime);
		}

		// Token: 0x04000F82 RID: 3970
		private static TimeZone currentTimeZone = new CurrentSystemTimeZone(DateTime.GetNow());
	}
}
