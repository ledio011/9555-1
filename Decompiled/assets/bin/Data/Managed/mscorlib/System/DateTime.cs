using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	[StructLayout(3)]
	public struct DateTime : IComparable<DateTime>, IEquatable<DateTime>, IComparable, IConvertible, IFormattable
	{
		// Token: 0x0600077E RID: 1918 RVA: 0x0001B8F4 File Offset: 0x00019AF4
		public DateTime(long ticks)
		{
			this.ticks = new TimeSpan(ticks);
			if (ticks < DateTime.MinValue.Ticks || ticks > DateTime.MaxValue.Ticks)
			{
				string text = Locale.GetText("Value {0} is outside the valid range [{1},{2}].", new object[]
				{
					ticks,
					DateTime.MinValue.Ticks,
					DateTime.MaxValue.Ticks
				});
				throw new ArgumentOutOfRangeException("ticks", text);
			}
			this.kind = DateTimeKind.Unspecified;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001B98C File Offset: 0x00019B8C
		public DateTime(int year, int month, int day)
		{
			this = new DateTime(year, month, day, 0, 0, 0, 0);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001B9A8 File Offset: 0x00019BA8
		public DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this = new DateTime(year, month, day, hour, minute, second, 0);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001B9C8 File Offset: 0x00019BC8
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			if (year < 1 || year > 9999 || month < 1 || month > 12 || day < 1 || day > DateTime.DaysInMonth(year, month) || hour < 0 || hour > 23 || minute < 0 || minute > 59 || second < 0 || second > 59 || millisecond < 0 || millisecond > 999)
			{
				throw new ArgumentOutOfRangeException("Parameters describe an unrepresentable DateTime.");
			}
			this.ticks = new TimeSpan(DateTime.AbsoluteDays(year, month, day), hour, minute, second, millisecond);
			this.kind = DateTimeKind.Unspecified;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001BA80 File Offset: 0x00019C80
		internal DateTime(bool check, TimeSpan value)
		{
			if (check && (value.Ticks < DateTime.MinValue.Ticks || value.Ticks > DateTime.MaxValue.Ticks))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.ticks = value;
			this.kind = DateTimeKind.Unspecified;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001BADC File Offset: 0x00019CDC
		public DateTime(long ticks, DateTimeKind kind)
		{
			this = new DateTime(ticks);
			this.CheckDateTimeKind(kind);
			this.kind = kind;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001BAF4 File Offset: 0x00019CF4
		static DateTime()
		{
			if (MonoTouchAOTHelper.FalseFlag)
			{
				GenericComparer<DateTime> genericComparer = new GenericComparer<DateTime>();
				GenericEqualityComparer<DateTime> genericEqualityComparer = new GenericEqualityComparer<DateTime>();
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001BDEC File Offset: 0x00019FEC
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001BDFC File Offset: 0x00019FFC
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001BE04 File Offset: 0x0001A004
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001BE0C File Offset: 0x0001A00C
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001BE14 File Offset: 0x0001A014
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001BE1C File Offset: 0x0001A01C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001BE24 File Offset: 0x0001A024
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001BE2C File Offset: 0x0001A02C
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001BE34 File Offset: 0x0001A034
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001BE3C File Offset: 0x0001A03C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001BE44 File Offset: 0x0001A044
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (targetType == typeof(DateTime))
			{
				return this;
			}
			if (targetType == typeof(string))
			{
				return this.ToString(provider);
			}
			if (targetType == typeof(object))
			{
				return this;
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001BEB8 File Offset: 0x0001A0B8
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		private static int AbsoluteDays(int year, int month, int day)
		{
			int num = 0;
			int i = 1;
			int[] array = (!DateTime.IsLeapYear(year)) ? DateTime.daysmonth : DateTime.daysmonthleap;
			while (i < month)
			{
				num += array[i++];
			}
			return day - 1 + num + 365 * (year - 1) + (year - 1) / 4 - (year - 1) / 100 + (year - 1) / 400;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001BF38 File Offset: 0x0001A138
		private int FromTicks(DateTime.Which what)
		{
			int num = 1;
			int[] array = DateTime.daysmonth;
			int i = this.ticks.Days;
			int num2 = i / 146097;
			i -= num2 * 146097;
			int num3 = i / 36524;
			if (num3 == 4)
			{
				num3 = 3;
			}
			i -= num3 * 36524;
			int num4 = i / 1461;
			i -= num4 * 1461;
			int num5 = i / 365;
			if (num5 == 4)
			{
				num5 = 3;
			}
			if (what == DateTime.Which.Year)
			{
				return num2 * 400 + num3 * 100 + num4 * 4 + num5 + 1;
			}
			i -= num5 * 365;
			if (what == DateTime.Which.DayYear)
			{
				return i + 1;
			}
			if (num5 == 3 && (num3 == 3 || num4 != 24))
			{
				array = DateTime.daysmonthleap;
			}
			while (i >= array[num])
			{
				i -= array[num++];
			}
			if (what == DateTime.Which.Month)
			{
				return num;
			}
			return i + 1;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001C038 File Offset: 0x0001A238
		public int Month
		{
			get
			{
				return this.FromTicks(DateTime.Which.Month);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001C044 File Offset: 0x0001A244
		public int Day
		{
			get
			{
				return this.FromTicks(DateTime.Which.Day);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001C050 File Offset: 0x0001A250
		public DayOfWeek DayOfWeek
		{
			get
			{
				return (this.ticks.Days + DayOfWeek.Monday) % (DayOfWeek)7;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001C064 File Offset: 0x0001A264
		public TimeSpan TimeOfDay
		{
			get
			{
				return new TimeSpan(this.ticks.Ticks % 864000000000L);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001C080 File Offset: 0x0001A280
		public int Hour
		{
			get
			{
				return this.ticks.Hours;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001C090 File Offset: 0x0001A290
		public int Minute
		{
			get
			{
				return this.ticks.Minutes;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001C0A0 File Offset: 0x0001A2A0
		public int Second
		{
			get
			{
				return this.ticks.Seconds;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001C0B0 File Offset: 0x0001A2B0
		public int Millisecond
		{
			get
			{
				return this.ticks.Milliseconds;
			}
		}

		// Token: 0x0600079E RID: 1950
		[MethodImpl(4096)]
		internal static extern long GetTimeMonotonic();

		// Token: 0x0600079F RID: 1951
		[MethodImpl(4096)]
		internal static extern long GetNow();

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		public static DateTime Now
		{
			get
			{
				long now = DateTime.GetNow();
				DateTime dateTime = new DateTime(now);
				if (now - DateTime.last_now > 600000000L)
				{
					DateTime.to_local_time_span_object = TimeZone.CurrentTimeZone.GetLocalTimeDiff(dateTime);
					DateTime.last_now = now;
				}
				DateTime result = dateTime + (TimeSpan)DateTime.to_local_time_span_object;
				result.kind = DateTimeKind.Local;
				return result;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001C124 File Offset: 0x0001A324
		public long Ticks
		{
			get
			{
				return this.ticks.Ticks;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001C134 File Offset: 0x0001A334
		public static DateTime Today
		{
			get
			{
				DateTime now = DateTime.Now;
				return new DateTime(now.Year, now.Month, now.Day)
				{
					kind = now.kind
				};
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001C174 File Offset: 0x0001A374
		public static DateTime UtcNow
		{
			get
			{
				return new DateTime(DateTime.GetNow(), DateTimeKind.Utc);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001C184 File Offset: 0x0001A384
		public int Year
		{
			get
			{
				return this.FromTicks(DateTime.Which.Year);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0001C190 File Offset: 0x0001A390
		public DateTimeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001C198 File Offset: 0x0001A398
		public DateTime Add(TimeSpan value)
		{
			DateTime result = this.AddTicks(value.Ticks);
			result.kind = this.kind;
			return result;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001C1C4 File Offset: 0x0001A3C4
		public DateTime AddTicks(long value)
		{
			if (value + this.ticks.Ticks > 3155378975999999999L || value + this.ticks.Ticks < 0L)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new DateTime(value + this.ticks.Ticks)
			{
				kind = this.kind
			};
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001C228 File Offset: 0x0001A428
		public DateTime AddMilliseconds(double value)
		{
			if (value * 10000.0 > 9.223372036854776E+18 || value * 10000.0 < -9.223372036854776E+18)
			{
				throw new ArgumentOutOfRangeException();
			}
			long value2 = (long)Math.Round(value * 10000.0);
			return this.AddTicks(value2);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001C288 File Offset: 0x0001A488
		public DateTime AddSeconds(double value)
		{
			return this.AddMilliseconds(value * 1000.0);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001C29C File Offset: 0x0001A49C
		public static int Compare(DateTime t1, DateTime t2)
		{
			if (t1.ticks < t2.ticks)
			{
				return -1;
			}
			if (t1.ticks > t2.ticks)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is DateTime))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.DateTime"));
			}
			return DateTime.Compare(this, (DateTime)value);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001C30C File Offset: 0x0001A50C
		public int CompareTo(DateTime value)
		{
			return DateTime.Compare(this, value);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001C31C File Offset: 0x0001A51C
		public bool Equals(DateTime value)
		{
			return value.ticks == this.ticks;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001C330 File Offset: 0x0001A530
		public long ToBinary()
		{
			DateTimeKind dateTimeKind = this.kind;
			if (dateTimeKind == DateTimeKind.Utc)
			{
				return this.Ticks | 4611686018427387904L;
			}
			if (dateTimeKind != DateTimeKind.Local)
			{
				return this.Ticks;
			}
			return this.ToUniversalTime().Ticks | long.MinValue;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001C388 File Offset: 0x0001A588
		public static DateTime FromBinary(long dateData)
		{
			ulong num = (ulong)dateData >> 62;
			if (num == (ulong)0)
			{
				return new DateTime(dateData, DateTimeKind.Unspecified);
			}
			if (num != (ulong)1)
			{
				DateTime dateTime = new DateTime(dateData & 4611686018427387903L, DateTimeKind.Utc);
				return dateTime.ToLocalTime();
			}
			return new DateTime(dateData ^ 4611686018427387904L, DateTimeKind.Utc);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
		{
			return new DateTime(value.Ticks, kind);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
		public static int DaysInMonth(int year, int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException();
			}
			int[] array = (!DateTime.IsLeapYear(year)) ? DateTime.daysmonth : DateTime.daysmonthleap;
			return array[month];
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001C44C File Offset: 0x0001A64C
		public override bool Equals(object value)
		{
			return value is DateTime && ((DateTime)value).ticks == this.ticks;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001C480 File Offset: 0x0001A680
		private void CheckDateTimeKind(DateTimeKind kind)
		{
			if (kind != DateTimeKind.Unspecified && kind != DateTimeKind.Utc && kind != DateTimeKind.Local)
			{
				throw new ArgumentException("Invalid DateTimeKind value.", "kind");
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001C4A8 File Offset: 0x0001A6A8
		public override int GetHashCode()
		{
			return (int)this.ticks.Ticks;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001C4B8 File Offset: 0x0001A6B8
		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public static bool IsLeapYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException();
			}
			return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001C4F4 File Offset: 0x0001A6F4
		public static DateTime Parse(string s, IFormatProvider provider)
		{
			return DateTime.Parse(s, provider, DateTimeStyles.AllowWhiteSpaces);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001C500 File Offset: 0x0001A700
		public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			Exception ex = null;
			DateTime result;
			DateTimeOffset dateTimeOffset;
			if (!DateTime.CoreParse(s, provider, styles, out result, out dateTimeOffset, true, ref ex))
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001C538 File Offset: 0x0001A738
		internal static bool CoreParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result, out DateTimeOffset dto, bool setExceptionOnError, ref Exception exception)
		{
			dto = new DateTimeOffset(0L, TimeSpan.Zero);
			if (s == null || s.Length == 0)
			{
				if (setExceptionOnError)
				{
					exception = new FormatException("String was not recognized as a valid DateTime.");
				}
				result = DateTime.MinValue;
				return false;
			}
			if (provider == null)
			{
				provider = CultureInfo.CurrentCulture;
			}
			DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(provider);
			string[] array = DateTime.YearMonthDayFormats(instance, setExceptionOnError, ref exception);
			if (array == null)
			{
				result = DateTime.MinValue;
				return false;
			}
			bool flag = false;
			foreach (string firstPart in array)
			{
				bool flag2 = false;
				if (DateTime._DoParse(s, firstPart, string.Empty, false, out result, out dto, instance, styles, true, ref flag2, ref flag))
				{
					return true;
				}
				if (flag2)
				{
					for (int j = 0; j < DateTime.ParseTimeFormats.Length; j++)
					{
						if (DateTime._DoParse(s, firstPart, DateTime.ParseTimeFormats[j], false, out result, out dto, instance, styles, true, ref flag2, ref flag))
						{
							return true;
						}
					}
				}
			}
			int num = instance.MonthDayPattern.IndexOf('d');
			int num2 = instance.MonthDayPattern.IndexOf('M');
			if (num == -1 || num2 == -1)
			{
				result = DateTime.MinValue;
				if (setExceptionOnError)
				{
					exception = new FormatException(Locale.GetText("Order of month and date is not defined by {0}", new object[]
					{
						instance.MonthDayPattern
					}));
				}
				return false;
			}
			bool flag3 = num < num2;
			string[] array2 = (!flag3) ? DateTime.MonthDayShortFormats : DateTime.DayMonthShortFormats;
			for (int k = 0; k < array2.Length; k++)
			{
				bool flag4 = false;
				if (DateTime._DoParse(s, array2[k], string.Empty, false, out result, out dto, instance, styles, true, ref flag4, ref flag))
				{
					return true;
				}
			}
			for (int l = 0; l < DateTime.ParseTimeFormats.Length; l++)
			{
				string firstPart2 = DateTime.ParseTimeFormats[l];
				bool flag5 = false;
				if (DateTime._DoParse(s, firstPart2, string.Empty, false, out result, out dto, instance, styles, false, ref flag5, ref flag))
				{
					return true;
				}
				if (flag5)
				{
					for (int m = 0; m < array2.Length; m++)
					{
						if (DateTime._DoParse(s, firstPart2, array2[m], false, out result, out dto, instance, styles, false, ref flag5, ref flag))
						{
							return true;
						}
					}
					foreach (string text in array)
					{
						if (text[text.Length - 1] != 'T')
						{
							if (DateTime._DoParse(s, firstPart2, text, false, out result, out dto, instance, styles, false, ref flag5, ref flag))
							{
								return true;
							}
						}
					}
				}
			}
			if (DateTime.ParseExact(s, instance.GetAllDateTimePatternsInternal(), instance, styles, out result, false, ref flag, setExceptionOnError, ref exception))
			{
				return true;
			}
			if (!setExceptionOnError)
			{
				return false;
			}
			exception = new FormatException("String was not recognized as a valid DateTime.");
			return false;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001C818 File Offset: 0x0001AA18
		private static string[] YearMonthDayFormats(DateTimeFormatInfo dfi, bool setExceptionOnError, ref Exception exc)
		{
			int num = dfi.ShortDatePattern.IndexOf('d');
			int num2 = dfi.ShortDatePattern.IndexOf('M');
			int num3 = dfi.ShortDatePattern.IndexOf('y');
			if (num == -1 || num2 == -1 || num3 == -1)
			{
				if (setExceptionOnError)
				{
					exc = new FormatException(Locale.GetText("Order of year, month and date is not defined by {0}", new object[]
					{
						dfi.ShortDatePattern
					}));
				}
				return null;
			}
			if (num3 < num2)
			{
				if (num2 < num)
				{
					return DateTime.ParseYearMonthDayFormats;
				}
				if (num3 < num)
				{
					return DateTime.ParseYearDayMonthFormats;
				}
				if (setExceptionOnError)
				{
					exc = new FormatException(Locale.GetText("Order of date, year and month defined by {0} is not supported", new object[]
					{
						dfi.ShortDatePattern
					}));
				}
				return null;
			}
			else
			{
				if (num < num2)
				{
					return DateTime.ParseDayMonthYearFormats;
				}
				if (num < num3)
				{
					return DateTime.ParseMonthDayYearFormats;
				}
				if (setExceptionOnError)
				{
					exc = new FormatException(Locale.GetText("Order of month, year and date defined by {0} is not supported", new object[]
					{
						dfi.ShortDatePattern
					}));
				}
				return null;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001C918 File Offset: 0x0001AB18
		private static int _ParseNumber(string s, int valuePos, int min_digits, int digits, bool leadingzero, bool sloppy_parsing, out int num_parsed)
		{
			int num = 0;
			if (sloppy_parsing)
			{
				leadingzero = false;
			}
			if (!leadingzero)
			{
				int num2 = 0;
				int i = valuePos;
				while (i < s.Length && i < digits + valuePos)
				{
					if (!char.IsDigit(s[i]))
					{
						break;
					}
					num2++;
					i++;
				}
				digits = num2;
			}
			if (digits < min_digits)
			{
				num_parsed = -1;
				return 0;
			}
			if (s.Length - valuePos < digits)
			{
				num_parsed = -1;
				return 0;
			}
			for (int i = valuePos; i < digits + valuePos; i++)
			{
				char c = s[i];
				if (!char.IsDigit(c))
				{
					num_parsed = -1;
					return 0;
				}
				num = num * 10 + (int)((byte)(c - '0'));
			}
			num_parsed = digits;
			return num;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001C9D8 File Offset: 0x0001ABD8
		private static int _ParseEnum(string s, int sPos, string[] values, string[] invValues, bool exact, out int num_parsed)
		{
			for (int i = values.Length - 1; i >= 0; i--)
			{
				if (!exact && invValues[i].Length > values[i].Length)
				{
					if (invValues[i].Length > 0 && DateTime._ParseString(s, sPos, 0, invValues[i], out num_parsed))
					{
						return i;
					}
					if (values[i].Length > 0 && DateTime._ParseString(s, sPos, 0, values[i], out num_parsed))
					{
						return i;
					}
				}
				else
				{
					if (values[i].Length > 0 && DateTime._ParseString(s, sPos, 0, values[i], out num_parsed))
					{
						return i;
					}
					if (!exact && invValues[i].Length > 0 && DateTime._ParseString(s, sPos, 0, invValues[i], out num_parsed))
					{
						return i;
					}
				}
			}
			num_parsed = -1;
			return -1;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
		private static bool _ParseString(string s, int sPos, int maxlength, string value, out int num_parsed)
		{
			if (maxlength <= 0)
			{
				maxlength = value.Length;
			}
			if (sPos + maxlength <= s.Length && string.Compare(s, sPos, value, 0, maxlength, true, CultureInfo.InvariantCulture) == 0)
			{
				num_parsed = maxlength;
				return true;
			}
			num_parsed = -1;
			return false;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		private static bool _ParseAmPm(string s, int valuePos, int num, DateTimeFormatInfo dfi, bool exact, out int num_parsed, ref int ampm)
		{
			num_parsed = -1;
			if (ampm != -1)
			{
				return false;
			}
			if (DateTime.IsLetter(s, valuePos))
			{
				DateTimeFormatInfo invariantInfo = DateTimeFormatInfo.InvariantInfo;
				if ((!exact && DateTime._ParseString(s, valuePos, num, invariantInfo.PMDesignator, out num_parsed)) || (dfi.PMDesignator != string.Empty && DateTime._ParseString(s, valuePos, num, dfi.PMDesignator, out num_parsed)))
				{
					ampm = 1;
				}
				else
				{
					if ((exact || !DateTime._ParseString(s, valuePos, num, invariantInfo.AMDesignator, out num_parsed)) && !DateTime._ParseString(s, valuePos, num, dfi.AMDesignator, out num_parsed))
					{
						return false;
					}
					if (exact || num_parsed != 0)
					{
						ampm = 0;
					}
				}
				return true;
			}
			if (dfi.AMDesignator != string.Empty)
			{
				return false;
			}
			if (exact)
			{
				ampm = 0;
			}
			num_parsed = 0;
			return true;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		private static bool _ParseTimeSeparator(string s, int sPos, DateTimeFormatInfo dfi, bool exact, out int num_parsed)
		{
			return DateTime._ParseString(s, sPos, 0, dfi.TimeSeparator, out num_parsed) || (!exact && DateTime._ParseString(s, sPos, 0, ":", out num_parsed));
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001CC18 File Offset: 0x0001AE18
		private static bool _ParseDateSeparator(string s, int sPos, DateTimeFormatInfo dfi, bool exact, out int num_parsed)
		{
			num_parsed = -1;
			if (exact && s[sPos] != '/')
			{
				return false;
			}
			if (DateTime._ParseTimeSeparator(s, sPos, dfi, exact, out num_parsed) || char.IsDigit(s[sPos]) || char.IsLetter(s[sPos]))
			{
				return false;
			}
			num_parsed = 1;
			return true;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001CC78 File Offset: 0x0001AE78
		private static bool IsLetter(string s, int pos)
		{
			return pos < s.Length && char.IsLetter(s[pos]);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001CC98 File Offset: 0x0001AE98
		private static bool _DoParse(string s, string firstPart, string secondPart, bool exact, out DateTime result, out DateTimeOffset dto, DateTimeFormatInfo dfi, DateTimeStyles style, bool firstPartIsDate, ref bool incompleteFormat, ref bool longYear)
		{
			bool flag = false;
			bool flag2 = false;
			bool sloppy_parsing = false;
			dto = new DateTimeOffset(0L, TimeSpan.Zero);
			bool flag3 = !exact && secondPart != null;
			incompleteFormat = false;
			int num = 0;
			string text = firstPart;
			bool flag4 = false;
			DateTimeFormatInfo invariantInfo = DateTimeFormatInfo.InvariantInfo;
			if (text.Length == 1)
			{
				text = DateTimeUtils.GetStandardPattern(text[0], dfi, out flag, out flag2);
			}
			result = new DateTime(0L);
			if (text == null)
			{
				return false;
			}
			if (s == null)
			{
				return false;
			}
			if ((style & DateTimeStyles.AllowLeadingWhite) != DateTimeStyles.None)
			{
				text = text.TrimStart(null);
				s = s.TrimStart(null);
			}
			if ((style & DateTimeStyles.AllowTrailingWhite) != DateTimeStyles.None)
			{
				text = text.TrimEnd(null);
				s = s.TrimEnd(null);
			}
			if (flag2)
			{
				dfi = invariantInfo;
			}
			if ((style & DateTimeStyles.AllowInnerWhite) != DateTimeStyles.None)
			{
				sloppy_parsing = true;
			}
			string text2 = text;
			int length = text.Length;
			int num2 = 0;
			int num3 = 0;
			if (length == 0)
			{
				return false;
			}
			int num4 = -1;
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			int num8 = -1;
			int num9 = -1;
			int num10 = -1;
			double num11 = -1.0;
			int num12 = -1;
			int num13 = -1;
			int num14 = -1;
			int num15 = -1;
			bool flag5 = true;
			while (num != s.Length)
			{
				int num16 = 0;
				if (flag3 && num2 + num3 == 0)
				{
					bool flag6 = DateTime.IsLetter(s, num);
					if (flag6)
					{
						if (s[num] == 'Z')
						{
							num16 = 1;
						}
						else
						{
							DateTime._ParseString(s, num, 0, "GMT", out num16);
						}
						if (num16 > 0 && !DateTime.IsLetter(s, num + num16))
						{
							num += num16;
							flag = true;
							continue;
						}
					}
					if (!flag4 && DateTime._ParseAmPm(s, num, 0, dfi, exact, out num16, ref num12))
					{
						if (DateTime.IsLetter(s, num + num16))
						{
							num12 = -1;
						}
						else if (num16 > 0)
						{
							num += num16;
							continue;
						}
					}
					if (!flag4 && num5 == -1 && flag6)
					{
						num5 = DateTime._ParseEnum(s, num, dfi.RawDayNames, invariantInfo.RawDayNames, exact, out num16);
						if (num5 == -1)
						{
							num5 = DateTime._ParseEnum(s, num, dfi.RawAbbreviatedDayNames, invariantInfo.RawAbbreviatedDayNames, exact, out num16);
						}
						if (num5 != -1 && !DateTime.IsLetter(s, num + num16))
						{
							num += num16;
							continue;
						}
						num5 = -1;
					}
					if (char.IsWhiteSpace(s[num]) || s[num] == ',')
					{
						num++;
						continue;
					}
					num16 = 0;
				}
				if (num2 + num3 >= length)
				{
					if (flag3 && num3 == 0)
					{
						flag4 = (flag5 && firstPart[firstPart.Length - 1] == 'T');
						if (flag5 || !(text == string.Empty))
						{
							num2 = 0;
							if (flag5)
							{
								text = secondPart;
							}
							else
							{
								text = string.Empty;
							}
							text2 = text;
							length = text2.Length;
							flag5 = false;
							continue;
						}
					}
					IL_EA9:
					if (num2 + 1 < length && text2[num2] == '.' && text2[num2 + 1] == 'F')
					{
						num2++;
						while (num2 < length && text2[num2] == 'F')
						{
							num2++;
						}
					}
					while (num2 < length && text2[num2] == 'K')
					{
						num2++;
					}
					if (num2 < length)
					{
						return false;
					}
					if (s.Length > num)
					{
						if (num == 0)
						{
							return false;
						}
						if (char.IsDigit(s[num]) && char.IsDigit(s[num - 1]))
						{
							return false;
						}
						if (char.IsLetter(s[num]) && char.IsLetter(s[num - 1]))
						{
							return false;
						}
						incompleteFormat = true;
						return false;
					}
					else
					{
						if (num8 == -1)
						{
							num8 = 0;
						}
						if (num9 == -1)
						{
							num9 = 0;
						}
						if (num10 == -1)
						{
							num10 = 0;
						}
						if (num11 == -1.0)
						{
							num11 = 0.0;
						}
						if (num4 == -1 && num6 == -1 && num7 == -1)
						{
							if ((style & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
							{
								num4 = 1;
								num6 = 1;
								num7 = 1;
							}
							else
							{
								num4 = DateTime.Today.Day;
								num6 = DateTime.Today.Month;
								num7 = DateTime.Today.Year;
							}
						}
						if (num4 == -1)
						{
							num4 = 1;
						}
						if (num6 == -1)
						{
							num6 = 1;
						}
						if (num7 == -1)
						{
							if ((style & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
							{
								num7 = 1;
							}
							else
							{
								num7 = DateTime.Today.Year;
							}
						}
						if (num12 == 0 && num8 == 12)
						{
							num8 = 0;
						}
						if (num12 == 1 && (!flag3 || num8 < 12))
						{
							num8 += 12;
						}
						if (num7 < 1 || num7 > 9999 || num6 < 1 || num6 > 12 || num4 < 1 || num4 > DateTime.DaysInMonth(num7, num6) || num8 < 0 || num8 > 23 || num9 < 0 || num9 > 59 || num10 < 0 || num10 > 59)
						{
							return false;
						}
						result = new DateTime(num7, num6, num4, num8, num9, num10, 0);
						result = result.AddSeconds(num11);
						if (num5 != -1 && num5 != (int)result.DayOfWeek)
						{
							return false;
						}
						if (num13 == -1)
						{
							if (result != DateTime.MinValue)
							{
								try
								{
									dto = new DateTimeOffset(result);
								}
								catch
								{
								}
							}
						}
						else
						{
							if (num15 == -1)
							{
								num15 = 0;
							}
							if (num14 == -1)
							{
								num14 = 0;
							}
							if (num13 == 1)
							{
								num14 = -num14;
								num15 = -num15;
							}
							try
							{
								dto = new DateTimeOffset(result, new TimeSpan(num14, num15, 0));
							}
							catch
							{
							}
						}
						bool flag7 = (style & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None;
						if (num13 != -1)
						{
							long num17 = (result.ticks - dto.Offset).Ticks;
							if (num17 < 0L)
							{
								num17 += 864000000000L;
							}
							result = new DateTime(false, new TimeSpan(num17));
							result.kind = DateTimeKind.Utc;
							if ((style & DateTimeStyles.RoundtripKind) != DateTimeStyles.None)
							{
								result = result.ToLocalTime();
							}
						}
						else if (flag || (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
						{
							result.kind = DateTimeKind.Utc;
						}
						else if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None)
						{
							result.kind = DateTimeKind.Local;
						}
						bool flag8 = !flag7 && (style & DateTimeStyles.RoundtripKind) == DateTimeStyles.None;
						if (result.kind != DateTimeKind.Unspecified)
						{
							if (flag7)
							{
								result = result.ToUniversalTime();
							}
							else if (flag8)
							{
								result = result.ToLocalTime();
							}
						}
						return true;
					}
				}
				else
				{
					bool leadingzero = true;
					if (text2[num2] == '\'')
					{
						num3 = 1;
						while (num2 + num3 < length)
						{
							if (text2[num2 + num3] == '\'')
							{
								break;
							}
							if (num == s.Length || s[num] != text2[num2 + num3])
							{
								return false;
							}
							num++;
							num3++;
						}
						num2 += num3 + 1;
						num3 = 0;
					}
					else if (text2[num2] == '"')
					{
						num3 = 1;
						while (num2 + num3 < length)
						{
							if (text2[num2 + num3] == '"')
							{
								break;
							}
							if (num == s.Length || s[num] != text2[num2 + num3])
							{
								return false;
							}
							num++;
							num3++;
						}
						num2 += num3 + 1;
						num3 = 0;
					}
					else if (text2[num2] == '\\')
					{
						num2 += num3 + 1;
						num3 = 0;
						if (num2 >= length)
						{
							return false;
						}
						if (s[num] != text2[num2])
						{
							return false;
						}
						num++;
						num2++;
					}
					else if (text2[num2] == '%')
					{
						num2++;
					}
					else if (char.IsWhiteSpace(s[num]) || (s[num] == ',' && ((!exact && text2[num2] == '/') || char.IsWhiteSpace(text2[num2]))))
					{
						num++;
						num3 = 0;
						if (exact && (style & DateTimeStyles.AllowInnerWhite) == DateTimeStyles.None)
						{
							if (!char.IsWhiteSpace(text2[num2]))
							{
								return false;
							}
							num2++;
						}
						else
						{
							int i;
							for (i = num; i < s.Length; i++)
							{
								if (!char.IsWhiteSpace(s[i]) && s[i] != ',')
								{
									break;
								}
							}
							num = i;
							for (i = num2; i < text2.Length; i++)
							{
								if (!char.IsWhiteSpace(text2[i]) && text2[i] != ',')
								{
									break;
								}
							}
							num2 = i;
							if (!exact && num2 < text2.Length && text2[num2] == '/' && !DateTime._ParseDateSeparator(s, num, dfi, exact, out num16))
							{
								num2++;
							}
						}
					}
					else if (num2 + num3 + 1 < length && text2[num2 + num3 + 1] == text2[num2 + num3])
					{
						num3++;
					}
					else
					{
						char c = text2[num2];
						switch (c)
						{
						case 'F':
							leadingzero = false;
							goto IL_A82;
						case 'G':
							if (s[num] != 'G')
							{
								return false;
							}
							if (num2 + 2 < length && num + 2 < s.Length && text2[num2 + 1] == 'M' && s[num + 1] == 'M' && text2[num2 + 2] == 'T' && s[num + 2] == 'T')
							{
								flag = true;
								num3 = 2;
								num16 = 3;
							}
							else
							{
								num3 = 0;
								num16 = 1;
							}
							break;
						case 'H':
							if (num8 != -1 || (!flag3 && num12 >= 0))
							{
								return false;
							}
							if (num3 == 0)
							{
								num8 = DateTime._ParseNumber(s, num, 1, 2, false, sloppy_parsing, out num16);
							}
							else
							{
								num8 = DateTime._ParseNumber(s, num, 1, 2, true, sloppy_parsing, out num16);
							}
							if (num8 >= 24)
							{
								return false;
							}
							break;
						default:
							switch (c)
							{
							case 's':
								if (num10 != -1)
								{
									return false;
								}
								if (num3 == 0)
								{
									num10 = DateTime._ParseNumber(s, num, 1, 2, false, sloppy_parsing, out num16);
								}
								else
								{
									num10 = DateTime._ParseNumber(s, num, 1, 2, true, sloppy_parsing, out num16);
								}
								if (num10 >= 60)
								{
									return false;
								}
								break;
							case 't':
								if (!DateTime._ParseAmPm(s, num, (num3 <= 0) ? 1 : 0, dfi, exact, out num16, ref num12))
								{
									return false;
								}
								break;
							default:
								switch (c)
								{
								case 'd':
									if ((num3 < 2 && num4 != -1) || (num3 >= 2 && num5 != -1))
									{
										return false;
									}
									if (num3 == 0)
									{
										num4 = DateTime._ParseNumber(s, num, 1, 2, false, sloppy_parsing, out num16);
									}
									else if (num3 == 1)
									{
										num4 = DateTime._ParseNumber(s, num, 1, 2, true, sloppy_parsing, out num16);
									}
									else if (num3 == 2)
									{
										num5 = DateTime._ParseEnum(s, num, dfi.RawAbbreviatedDayNames, invariantInfo.RawAbbreviatedDayNames, exact, out num16);
									}
									else
									{
										num5 = DateTime._ParseEnum(s, num, dfi.RawDayNames, invariantInfo.RawDayNames, exact, out num16);
									}
									break;
								default:
									if (c != '/')
									{
										if (c != ':')
										{
											if (c != 'Z')
											{
												if (c != 'm')
												{
													if (s[num] != text2[num2])
													{
														return false;
													}
													num3 = 0;
													num16 = 1;
												}
												else
												{
													if (num9 != -1)
													{
														return false;
													}
													if (num3 == 0)
													{
														num9 = DateTime._ParseNumber(s, num, 1, 2, false, sloppy_parsing, out num16);
													}
													else
													{
														num9 = DateTime._ParseNumber(s, num, 1, 2, true, sloppy_parsing, out num16);
													}
													if (num9 >= 60)
													{
														return false;
													}
												}
											}
											else
											{
												if (s[num] != 'Z')
												{
													return false;
												}
												num3 = 0;
												num16 = 1;
												flag = true;
											}
										}
										else if (!DateTime._ParseTimeSeparator(s, num, dfi, exact, out num16))
										{
											return false;
										}
									}
									else
									{
										if (!DateTime._ParseDateSeparator(s, num, dfi, exact, out num16))
										{
											return false;
										}
										num3 = 0;
									}
									break;
								case 'f':
									goto IL_A82;
								case 'h':
									if (num8 != -1)
									{
										return false;
									}
									if (num3 == 0)
									{
										num8 = DateTime._ParseNumber(s, num, 1, 2, false, sloppy_parsing, out num16);
									}
									else
									{
										num8 = DateTime._ParseNumber(s, num, 1, 2, true, sloppy_parsing, out num16);
									}
									if (num8 > 12)
									{
										return false;
									}
									if (num8 == 12)
									{
										num8 = 0;
									}
									break;
								}
								break;
							case 'y':
								if (num7 != -1)
								{
									return false;
								}
								if (num3 == 0)
								{
									num7 = DateTime._ParseNumber(s, num, 1, 2, false, sloppy_parsing, out num16);
								}
								else if (num3 < 3)
								{
									num7 = DateTime._ParseNumber(s, num, 1, 2, true, sloppy_parsing, out num16);
								}
								else
								{
									num7 = DateTime._ParseNumber(s, num, (!exact) ? 3 : 4, 4, false, sloppy_parsing, out num16);
									if (num7 >= 1000 && num16 == 4 && !longYear && s.Length > 4 + num)
									{
										int num18 = 0;
										int num19 = DateTime._ParseNumber(s, num, 5, 5, false, sloppy_parsing, out num18);
										longYear = (num19 > 9999);
									}
									num3 = 3;
								}
								if (num16 <= 2)
								{
									num7 += ((num7 >= 30) ? 1900 : 2000);
								}
								break;
							case 'z':
								if (num13 != -1)
								{
									return false;
								}
								if (s[num] == '+')
								{
									num13 = 0;
								}
								else
								{
									if (s[num] != '-')
									{
										return false;
									}
									num13 = 1;
								}
								num++;
								if (num3 == 0)
								{
									num14 = DateTime._ParseNumber(s, num, 1, 2, false, sloppy_parsing, out num16);
								}
								else if (num3 == 1)
								{
									num14 = DateTime._ParseNumber(s, num, 1, 2, true, sloppy_parsing, out num16);
								}
								else
								{
									num14 = DateTime._ParseNumber(s, num, 1, 2, true, true, out num16);
									num += num16;
									if (num16 < 0)
									{
										return false;
									}
									num16 = 0;
									if ((num < s.Length && char.IsDigit(s[num])) || DateTime._ParseTimeSeparator(s, num, dfi, exact, out num16))
									{
										num += num16;
										num15 = DateTime._ParseNumber(s, num, 1, 2, true, sloppy_parsing, out num16);
										if (num16 < 0)
										{
											return false;
										}
									}
									else
									{
										if (!flag3)
										{
											return false;
										}
										num16 = 0;
									}
								}
								break;
							}
							break;
						case 'K':
							if (s[num] == 'Z')
							{
								num++;
								flag = true;
							}
							else if (s[num] == '+' || s[num] == '-')
							{
								if (num13 != -1)
								{
									return false;
								}
								if (s[num] == '+')
								{
									num13 = 0;
								}
								else if (s[num] == '-')
								{
									num13 = 1;
								}
								num++;
								num14 = DateTime._ParseNumber(s, num, 0, 2, true, sloppy_parsing, out num16);
								num += num16;
								if (num16 < 0)
								{
									return false;
								}
								if (char.IsDigit(s[num]))
								{
									num16 = 0;
								}
								else if (!DateTime._ParseString(s, num, 0, dfi.TimeSeparator, out num16))
								{
									return false;
								}
								num += num16;
								num15 = DateTime._ParseNumber(s, num, 0, 2, true, sloppy_parsing, out num16);
								num3 = 2;
								if (num16 < 0)
								{
									return false;
								}
							}
							break;
						case 'M':
							if (num6 != -1)
							{
								return false;
							}
							if (flag3)
							{
								num16 = -1;
								if (num3 == 0 || num3 == 3)
								{
									num6 = DateTime._ParseNumber(s, num, 1, 2, false, sloppy_parsing, out num16);
								}
								if (num3 > 1 && num16 == -1)
								{
									num6 = DateTime._ParseEnum(s, num, dfi.RawMonthNames, invariantInfo.RawMonthNames, exact, out num16) + 1;
								}
								if (num3 > 1 && num16 == -1)
								{
									num6 = DateTime._ParseEnum(s, num, dfi.RawAbbreviatedMonthNames, invariantInfo.RawAbbreviatedMonthNames, exact, out num16) + 1;
								}
							}
							else if (num3 == 0)
							{
								num6 = DateTime._ParseNumber(s, num, 1, 2, false, sloppy_parsing, out num16);
							}
							else if (num3 == 1)
							{
								num6 = DateTime._ParseNumber(s, num, 1, 2, true, sloppy_parsing, out num16);
							}
							else if (num3 == 2)
							{
								num6 = DateTime._ParseEnum(s, num, dfi.RawAbbreviatedMonthNames, invariantInfo.RawAbbreviatedMonthNames, exact, out num16) + 1;
							}
							else
							{
								num6 = DateTime._ParseEnum(s, num, dfi.RawMonthNames, invariantInfo.RawMonthNames, exact, out num16) + 1;
							}
							break;
						}
						IL_DF3:
						if (num16 < 0)
						{
							return false;
						}
						num += num16;
						if (!exact && !flag3)
						{
							c = text2[num2];
							if (c == 'F' || c == 'f' || c == 'm' || c == 's' || c == 'z')
							{
								if (s.Length > num && s[num] == 'Z' && (num2 + 1 == text2.Length || text2[num2 + 1] != 'Z'))
								{
									flag = true;
									num++;
								}
							}
						}
						num2 = num2 + num3 + 1;
						num3 = 0;
						continue;
						IL_A82:
						if (num3 > 6 || num11 != -1.0)
						{
							return false;
						}
						double num20 = (double)DateTime._ParseNumber(s, num, 0, num3 + 1, leadingzero, sloppy_parsing, out num16);
						if (num16 == -1)
						{
							return false;
						}
						num11 = num20 / Math.Pow(10.0, (double)num16);
						goto IL_DF3;
					}
				}
			}
			goto IL_EA9;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
		private static bool ParseExact(string s, string[] formats, DateTimeFormatInfo dfi, DateTimeStyles style, out DateTime ret, bool exact, ref bool longYear, bool setExceptionOnError, ref Exception exception)
		{
			bool flag = false;
			for (int i = 0; i < formats.Length; i++)
			{
				string text = formats[i];
				if (text == null || text == string.Empty)
				{
					break;
				}
				DateTime dateTime;
				DateTimeOffset dateTimeOffset;
				if (DateTime._DoParse(s, formats[i], null, exact, out dateTime, out dateTimeOffset, dfi, style, false, ref flag, ref longYear))
				{
					ret = dateTime;
					return true;
				}
			}
			if (setExceptionOnError)
			{
				exception = new FormatException("Invalid format string");
			}
			ret = DateTime.MinValue;
			return false;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001E02C File Offset: 0x0001C22C
		public TimeSpan Subtract(DateTime value)
		{
			return new TimeSpan(this.ticks.Ticks) - value.ticks;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001E04C File Offset: 0x0001C24C
		public DateTime Subtract(TimeSpan value)
		{
			TimeSpan value2 = new TimeSpan(this.ticks.Ticks) - value;
			return new DateTime(true, value2)
			{
				kind = this.kind
			};
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001E088 File Offset: 0x0001C288
		public override string ToString()
		{
			return this.ToString("G", null);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001E098 File Offset: 0x0001C298
		public string ToString(IFormatProvider provider)
		{
			return this.ToString(null, provider);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
		public string ToString(string format, IFormatProvider provider)
		{
			DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(provider);
			if (format == null || format == string.Empty)
			{
				format = "G";
			}
			bool flag = false;
			bool flag2 = false;
			if (format.Length == 1)
			{
				char c = format[0];
				format = DateTimeUtils.GetStandardPattern(c, instance, out flag, out flag2);
				if (c == 'U')
				{
					return DateTimeUtils.ToString(this.ToUniversalTime(), format, instance);
				}
				if (format == null)
				{
					throw new FormatException("format is not one of the format specifier characters defined for DateTimeFormatInfo");
				}
			}
			return DateTimeUtils.ToString(this, format, instance);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001E130 File Offset: 0x0001C330
		public DateTime ToLocalTime()
		{
			return TimeZone.CurrentTimeZone.ToLocalTime(this);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001E144 File Offset: 0x0001C344
		public DateTime ToUniversalTime()
		{
			return TimeZone.CurrentTimeZone.ToUniversalTime(this);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001E158 File Offset: 0x0001C358
		public static DateTime operator +(DateTime d, TimeSpan t)
		{
			return new DateTime(true, d.ticks + t)
			{
				kind = d.kind
			};
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001E18C File Offset: 0x0001C38C
		public static bool operator ==(DateTime d1, DateTime d2)
		{
			return d1.ticks == d2.ticks;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
		public static bool operator >(DateTime t1, DateTime t2)
		{
			return t1.ticks > t2.ticks;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001E1BC File Offset: 0x0001C3BC
		public static bool operator >=(DateTime t1, DateTime t2)
		{
			return t1.ticks >= t2.ticks;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001E1D4 File Offset: 0x0001C3D4
		public static bool operator !=(DateTime d1, DateTime d2)
		{
			return d1.ticks != d2.ticks;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001E1EC File Offset: 0x0001C3EC
		public static bool operator <(DateTime t1, DateTime t2)
		{
			return t1.ticks < t2.ticks;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001E204 File Offset: 0x0001C404
		public static bool operator <=(DateTime t1, DateTime t2)
		{
			return t1.ticks <= t2.ticks;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001E21C File Offset: 0x0001C41C
		public static TimeSpan operator -(DateTime d1, DateTime d2)
		{
			return new TimeSpan((d1.ticks - d2.ticks).Ticks);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001E24C File Offset: 0x0001C44C
		public static DateTime operator -(DateTime d, TimeSpan t)
		{
			return new DateTime(true, d.ticks - t)
			{
				kind = d.kind
			};
		}

		// Token: 0x0400027A RID: 634
		private const int dp400 = 146097;

		// Token: 0x0400027B RID: 635
		private const int dp100 = 36524;

		// Token: 0x0400027C RID: 636
		private const int dp4 = 1461;

		// Token: 0x0400027D RID: 637
		private const long w32file_epoch = 504911232000000000L;

		// Token: 0x0400027E RID: 638
		private const long MAX_VALUE_TICKS = 3155378975999999999L;

		// Token: 0x0400027F RID: 639
		internal const long UnixEpoch = 621355968000000000L;

		// Token: 0x04000280 RID: 640
		private const long ticks18991230 = 599264352000000000L;

		// Token: 0x04000281 RID: 641
		private const double OAMinValue = -657435.0;

		// Token: 0x04000282 RID: 642
		private const double OAMaxValue = 2958466.0;

		// Token: 0x04000283 RID: 643
		private const string formatExceptionMessage = "String was not recognized as a valid DateTime.";

		// Token: 0x04000284 RID: 644
		private TimeSpan ticks;

		// Token: 0x04000285 RID: 645
		private DateTimeKind kind;

		// Token: 0x04000286 RID: 646
		public static readonly DateTime MaxValue = new DateTime(false, new TimeSpan(3155378975999999999L));

		// Token: 0x04000287 RID: 647
		public static readonly DateTime MinValue = new DateTime(false, new TimeSpan(0L));

		// Token: 0x04000288 RID: 648
		private static readonly string[] ParseTimeFormats = new string[]
		{
			"H:m:s.fffffffzzz",
			"H:m:s.fffffff",
			"H:m:s tt zzz",
			"H:m:szzz",
			"H:m:s",
			"H:mzzz",
			"H:m",
			"H tt",
			"H'時'm'分's'秒'"
		};

		// Token: 0x04000289 RID: 649
		private static readonly string[] ParseYearDayMonthFormats = new string[]
		{
			"yyyy/M/dT",
			"M/yyyy/dT",
			"yyyy'年'M'月'd'日",
			"yyyy/d/MMMM",
			"yyyy/MMM/d",
			"d/MMMM/yyyy",
			"MMM/d/yyyy",
			"d/yyyy/MMMM",
			"MMM/yyyy/d",
			"yy/d/M"
		};

		// Token: 0x0400028A RID: 650
		private static readonly string[] ParseYearMonthDayFormats = new string[]
		{
			"yyyy/M/dT",
			"M/yyyy/dT",
			"yyyy'年'M'月'd'日",
			"yyyy/MMMM/d",
			"yyyy/d/MMM",
			"MMMM/d/yyyy",
			"d/MMM/yyyy",
			"MMMM/yyyy/d",
			"d/yyyy/MMM",
			"yy/MMMM/d",
			"yy/d/MMM",
			"MMM/yy/d"
		};

		// Token: 0x0400028B RID: 651
		private static readonly string[] ParseDayMonthYearFormats = new string[]
		{
			"yyyy/M/dT",
			"M/yyyy/dT",
			"yyyy'年'M'月'd'日",
			"yyyy/MMMM/d",
			"yyyy/d/MMM",
			"d/MMMM/yyyy",
			"MMM/d/yyyy",
			"MMMM/yyyy/d",
			"d/yyyy/MMM",
			"d/MMMM/yy",
			"yy/MMM/d",
			"d/yy/MMM",
			"yy/d/MMM",
			"MMM/d/yy",
			"MMM/yy/d"
		};

		// Token: 0x0400028C RID: 652
		private static readonly string[] ParseMonthDayYearFormats = new string[]
		{
			"yyyy/M/dT",
			"M/yyyy/dT",
			"yyyy'年'M'月'd'日",
			"yyyy/MMMM/d",
			"yyyy/d/MMM",
			"MMMM/d/yyyy",
			"d/MMM/yyyy",
			"MMMM/yyyy/d",
			"d/yyyy/MMM",
			"MMMM/d/yy",
			"MMM/yy/d",
			"d/MMM/yy",
			"yy/MMM/d",
			"d/yy/MMM",
			"yy/d/MMM"
		};

		// Token: 0x0400028D RID: 653
		private static readonly string[] MonthDayShortFormats = new string[]
		{
			"MMMM/d",
			"d/MMM",
			"yyyy/MMMM"
		};

		// Token: 0x0400028E RID: 654
		private static readonly string[] DayMonthShortFormats = new string[]
		{
			"d/MMMM",
			"MMM/yy",
			"yyyy/MMMM"
		};

		// Token: 0x0400028F RID: 655
		private static readonly int[] daysmonth = new int[]
		{
			0,
			31,
			28,
			31,
			30,
			31,
			30,
			31,
			31,
			30,
			31,
			30,
			31
		};

		// Token: 0x04000290 RID: 656
		private static readonly int[] daysmonthleap = new int[]
		{
			0,
			31,
			29,
			31,
			30,
			31,
			30,
			31,
			31,
			30,
			31,
			30,
			31
		};

		// Token: 0x04000291 RID: 657
		private static object to_local_time_span_object;

		// Token: 0x04000292 RID: 658
		private static long last_now;

		// Token: 0x020000C7 RID: 199
		private enum Which
		{
			// Token: 0x04000294 RID: 660
			Day,
			// Token: 0x04000295 RID: 661
			DayYear,
			// Token: 0x04000296 RID: 662
			Month,
			// Token: 0x04000297 RID: 663
			Year
		}
	}
}
