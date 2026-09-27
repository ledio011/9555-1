using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x020003CB RID: 971
	[ComVisible(true)]
	[Serializable]
	public struct TimeSpan : IComparable<TimeSpan>, IEquatable<TimeSpan>, IComparable
	{
		// Token: 0x06001D60 RID: 7520 RVA: 0x0006F1BC File Offset: 0x0006D3BC
		public TimeSpan(long ticks)
		{
			this._ticks = ticks;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0006F1C8 File Offset: 0x0006D3C8
		public TimeSpan(int hours, int minutes, int seconds)
		{
			this._ticks = TimeSpan.CalculateTicks(0, hours, minutes, seconds, 0);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0006F1DC File Offset: 0x0006D3DC
		public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			this._ticks = TimeSpan.CalculateTicks(days, hours, minutes, seconds, milliseconds);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0006F1F0 File Offset: 0x0006D3F0
		static TimeSpan()
		{
			if (MonoTouchAOTHelper.FalseFlag)
			{
				GenericComparer<TimeSpan> genericComparer = new GenericComparer<TimeSpan>();
				GenericEqualityComparer<TimeSpan> genericEqualityComparer = new GenericEqualityComparer<TimeSpan>();
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0006F248 File Offset: 0x0006D448
		internal static long CalculateTicks(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			int num = hours * 3600;
			int num2 = minutes * 60;
			long num3 = (long)(num + num2 + seconds) * 1000L + (long)milliseconds;
			num3 *= 10000L;
			bool flag = false;
			if (days > 0)
			{
				long num4 = 864000000000L * (long)days;
				if (num3 < 0L)
				{
					long num5 = num3;
					num3 += num4;
					flag = (num5 > num3);
				}
				else
				{
					num3 += num4;
					flag = (num3 < 0L);
				}
			}
			else if (days < 0)
			{
				long num6 = 864000000000L * (long)days;
				if (num3 <= 0L)
				{
					num3 += num6;
					flag = (num3 > 0L);
				}
				else
				{
					long num7 = num3;
					num3 += num6;
					flag = (num3 > num7);
				}
			}
			if (flag)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("The timespan is too big or too small."));
			}
			return num3;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x0006F314 File Offset: 0x0006D514
		public int Days
		{
			get
			{
				return (int)(this._ticks / 864000000000L);
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0006F328 File Offset: 0x0006D528
		public int Hours
		{
			get
			{
				return (int)(this._ticks % 864000000000L / 36000000000L);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0006F348 File Offset: 0x0006D548
		public int Milliseconds
		{
			get
			{
				return (int)(this._ticks % 10000000L / 10000L);
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0006F360 File Offset: 0x0006D560
		public int Minutes
		{
			get
			{
				return (int)(this._ticks % 36000000000L / 600000000L);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0006F37C File Offset: 0x0006D57C
		public int Seconds
		{
			get
			{
				return (int)(this._ticks % 600000000L / 10000000L);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0006F394 File Offset: 0x0006D594
		public long Ticks
		{
			get
			{
				return this._ticks;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0006F39C File Offset: 0x0006D59C
		public double TotalDays
		{
			get
			{
				return (double)this._ticks / 864000000000.0;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0006F3B0 File Offset: 0x0006D5B0
		public double TotalHours
		{
			get
			{
				return (double)this._ticks / 36000000000.0;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0006F3C4 File Offset: 0x0006D5C4
		public double TotalMilliseconds
		{
			get
			{
				return (double)this._ticks / 10000.0;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0006F3D8 File Offset: 0x0006D5D8
		public double TotalMinutes
		{
			get
			{
				return (double)this._ticks / 600000000.0;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0006F3EC File Offset: 0x0006D5EC
		public double TotalSeconds
		{
			get
			{
				return (double)this._ticks / 10000000.0;
			}
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0006F400 File Offset: 0x0006D600
		public TimeSpan Add(TimeSpan ts)
		{
			TimeSpan result;
			try
			{
				result = new TimeSpan(checked(this._ticks + ts.Ticks));
			}
			catch (OverflowException)
			{
				throw new OverflowException(Locale.GetText("Resulting timespan is too big."));
			}
			return result;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0006F454 File Offset: 0x0006D654
		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			if (t1._ticks < t2._ticks)
			{
				return -1;
			}
			if (t1._ticks > t2._ticks)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0006F484 File Offset: 0x0006D684
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is TimeSpan))
			{
				throw new ArgumentException(Locale.GetText("Argument has to be a TimeSpan."), "value");
			}
			return TimeSpan.Compare(this, (TimeSpan)value);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0006F4C0 File Offset: 0x0006D6C0
		public int CompareTo(TimeSpan value)
		{
			return TimeSpan.Compare(this, value);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0006F4D0 File Offset: 0x0006D6D0
		public bool Equals(TimeSpan obj)
		{
			return obj._ticks == this._ticks;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0006F4E4 File Offset: 0x0006D6E4
		public override bool Equals(object value)
		{
			return value is TimeSpan && this._ticks == ((TimeSpan)value)._ticks;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0006F514 File Offset: 0x0006D714
		public static TimeSpan FromDays(double value)
		{
			return TimeSpan.From(value, 864000000000L);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0006F528 File Offset: 0x0006D728
		public static TimeSpan FromHours(double value)
		{
			return TimeSpan.From(value, 36000000000L);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0006F53C File Offset: 0x0006D73C
		public static TimeSpan FromMinutes(double value)
		{
			return TimeSpan.From(value, 600000000L);
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0006F54C File Offset: 0x0006D74C
		public static TimeSpan FromSeconds(double value)
		{
			return TimeSpan.From(value, 10000000L);
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0006F55C File Offset: 0x0006D75C
		public static TimeSpan FromMilliseconds(double value)
		{
			return TimeSpan.From(value, 10000L);
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0006F56C File Offset: 0x0006D76C
		private static TimeSpan From(double value, long tickMultiplicator)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException(Locale.GetText("Value cannot be NaN."), "value");
			}
			if (double.IsNegativeInfinity(value) || double.IsPositiveInfinity(value) || value < (double)TimeSpan.MinValue.Ticks || value > (double)TimeSpan.MaxValue.Ticks)
			{
				throw new OverflowException(Locale.GetText("Outside range [MinValue,MaxValue]"));
			}
			TimeSpan result;
			try
			{
				value *= (double)(tickMultiplicator / 10000L);
				checked
				{
					long num = (long)Math.Round(value);
					result = new TimeSpan(num * 10000L);
				}
			}
			catch (OverflowException)
			{
				throw new OverflowException(Locale.GetText("Resulting timespan is too big."));
			}
			return result;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0006F63C File Offset: 0x0006D83C
		public override int GetHashCode()
		{
			return this._ticks.GetHashCode();
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0006F64C File Offset: 0x0006D84C
		public TimeSpan Subtract(TimeSpan ts)
		{
			TimeSpan result;
			try
			{
				result = new TimeSpan(checked(this._ticks - ts.Ticks));
			}
			catch (OverflowException)
			{
				throw new OverflowException(Locale.GetText("Resulting timespan is too big."));
			}
			return result;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0006F6A0 File Offset: 0x0006D8A0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(14);
			if (this._ticks < 0L)
			{
				stringBuilder.Append('-');
			}
			if (this.Days != 0)
			{
				stringBuilder.Append(Math.Abs(this.Days));
				stringBuilder.Append('.');
			}
			stringBuilder.Append(Math.Abs(this.Hours).ToString("D2"));
			stringBuilder.Append(':');
			stringBuilder.Append(Math.Abs(this.Minutes).ToString("D2"));
			stringBuilder.Append(':');
			stringBuilder.Append(Math.Abs(this.Seconds).ToString("D2"));
			int num = (int)Math.Abs(this._ticks % 10000000L);
			if (num != 0)
			{
				stringBuilder.Append('.');
				stringBuilder.Append(num.ToString("D7"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0006F7A0 File Offset: 0x0006D9A0
		public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0006F7AC File Offset: 0x0006D9AC
		public static bool operator ==(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0006F7C0 File Offset: 0x0006D9C0
		public static bool operator >(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks > t2._ticks;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0006F7D4 File Offset: 0x0006D9D4
		public static bool operator >=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks >= t2._ticks;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0006F7EC File Offset: 0x0006D9EC
		public static bool operator !=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks != t2._ticks;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0006F804 File Offset: 0x0006DA04
		public static bool operator <(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks < t2._ticks;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0006F818 File Offset: 0x0006DA18
		public static bool operator <=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks <= t2._ticks;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0006F830 File Offset: 0x0006DA30
		public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		// Token: 0x04000F79 RID: 3961
		public const long TicksPerDay = 864000000000L;

		// Token: 0x04000F7A RID: 3962
		public const long TicksPerHour = 36000000000L;

		// Token: 0x04000F7B RID: 3963
		public const long TicksPerMillisecond = 10000L;

		// Token: 0x04000F7C RID: 3964
		public const long TicksPerMinute = 600000000L;

		// Token: 0x04000F7D RID: 3965
		public const long TicksPerSecond = 10000000L;

		// Token: 0x04000F7E RID: 3966
		public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);

		// Token: 0x04000F7F RID: 3967
		public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);

		// Token: 0x04000F80 RID: 3968
		public static readonly TimeSpan Zero = new TimeSpan(0L);

		// Token: 0x04000F81 RID: 3969
		private long _ticks;
	}
}
