using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	[StructLayout(3)]
	public struct DateTimeOffset : IComparable<DateTimeOffset>, IEquatable<DateTimeOffset>, IComparable, IFormattable, IDeserializationCallback, ISerializable
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x0001E280 File Offset: 0x0001C480
		public DateTimeOffset(DateTime dateTime)
		{
			this.dt = dateTime;
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				this.utc_offset = TimeSpan.Zero;
			}
			else
			{
				this.utc_offset = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
			}
			if (this.UtcDateTime < DateTime.MinValue || this.UtcDateTime > DateTime.MaxValue)
			{
				throw new ArgumentOutOfRangeException("The UTC date and time that results from applying the offset is earlier than MinValue or later than MaxValue.");
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001E2F8 File Offset: 0x0001C4F8
		public DateTimeOffset(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == DateTimeKind.Utc && offset != TimeSpan.Zero)
			{
				throw new ArgumentException("dateTime.Kind equals Utc and offset does not equal zero.");
			}
			if (dateTime.Kind == DateTimeKind.Local && offset != TimeZone.CurrentTimeZone.GetUtcOffset(dateTime))
			{
				throw new ArgumentException("dateTime.Kind equals Local and offset does not equal the offset of the system's local time zone.");
			}
			if (offset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException("offset is not specified in whole minutes.");
			}
			if (offset < new TimeSpan(-14, 0, 0) || offset > new TimeSpan(14, 0, 0))
			{
				throw new ArgumentOutOfRangeException("offset is less than -14 hours or greater than 14 hours.");
			}
			this.dt = dateTime;
			this.utc_offset = offset;
			if (this.UtcDateTime < DateTime.MinValue || this.UtcDateTime > DateTime.MaxValue)
			{
				throw new ArgumentOutOfRangeException("The UtcDateTime property is earlier than MinValue or later than MaxValue.");
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001E3F0 File Offset: 0x0001C5F0
		public DateTimeOffset(long ticks, TimeSpan offset)
		{
			this = new DateTimeOffset(new DateTime(ticks), offset);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001E400 File Offset: 0x0001C600
		private DateTimeOffset(SerializationInfo info, StreamingContext context)
		{
			DateTime dateTime = (DateTime)info.GetValue("DateTime", typeof(DateTime));
			short @int = info.GetInt16("OffsetMinutes");
			this.utc_offset = TimeSpan.FromMinutes((double)@int);
			this.dt = dateTime.Add(this.utc_offset);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001E454 File Offset: 0x0001C654
		static DateTimeOffset()
		{
			if (MonoTouchAOTHelper.FalseFlag)
			{
				GenericComparer<DateTimeOffset> genericComparer = new GenericComparer<DateTimeOffset>();
				GenericEqualityComparer<DateTimeOffset> genericEqualityComparer = new GenericEqualityComparer<DateTimeOffset>();
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
		int IComparable.CompareTo(object obj)
		{
			return this.CompareTo((DateTimeOffset)obj);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			DateTime dateTime = new DateTime(this.dt.Ticks);
			DateTime value = dateTime.Subtract(this.utc_offset);
			info.AddValue("DateTime", value);
			info.AddValue("OffsetMinutes", (short)this.utc_offset.TotalMinutes);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001E514 File Offset: 0x0001C714
		[MonoTODO]
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001E518 File Offset: 0x0001C718
		public int CompareTo(DateTimeOffset other)
		{
			return this.UtcDateTime.CompareTo(other.UtcDateTime);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001E53C File Offset: 0x0001C73C
		public bool Equals(DateTimeOffset other)
		{
			return this.UtcDateTime == other.UtcDateTime;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001E550 File Offset: 0x0001C750
		public override bool Equals(object obj)
		{
			return obj is DateTimeOffset && this.UtcDateTime == ((DateTimeOffset)obj).UtcDateTime;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001E584 File Offset: 0x0001C784
		public override int GetHashCode()
		{
			return this.dt.GetHashCode() ^ this.utc_offset.GetHashCode();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001E5A0 File Offset: 0x0001C7A0
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001E5AC File Offset: 0x0001C7AC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(formatProvider);
			if (format == null || format == string.Empty)
			{
				format = instance.ShortDatePattern + " " + instance.LongTimePattern + " zzz";
			}
			bool flag = false;
			bool flag2 = false;
			if (format.Length == 1)
			{
				char format2 = format[0];
				try
				{
					format = DateTimeUtils.GetStandardPattern(format2, instance, out flag, out flag2, true);
				}
				catch
				{
					format = null;
				}
				if (format == null)
				{
					throw new FormatException("format is not one of the format specifier characters defined for DateTimeFormatInfo");
				}
			}
			return (!flag) ? DateTimeUtils.ToString(this.DateTime, new TimeSpan?(this.Offset), format, instance) : DateTimeUtils.ToString(this.UtcDateTime, new TimeSpan?(TimeSpan.Zero), format, instance);
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001E684 File Offset: 0x0001C884
		public DateTime DateTime
		{
			get
			{
				return DateTime.SpecifyKind(this.dt, DateTimeKind.Unspecified);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001E694 File Offset: 0x0001C894
		public TimeSpan Offset
		{
			get
			{
				return this.utc_offset;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001E69C File Offset: 0x0001C89C
		public DateTime UtcDateTime
		{
			get
			{
				return DateTime.SpecifyKind(this.dt - this.utc_offset, DateTimeKind.Utc);
			}
		}

		// Token: 0x0400029C RID: 668
		public static readonly DateTimeOffset MaxValue = new DateTimeOffset(DateTime.MaxValue, TimeSpan.Zero);

		// Token: 0x0400029D RID: 669
		public static readonly DateTimeOffset MinValue = new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero);

		// Token: 0x0400029E RID: 670
		private DateTime dt;

		// Token: 0x0400029F RID: 671
		private TimeSpan utc_offset;
	}
}
