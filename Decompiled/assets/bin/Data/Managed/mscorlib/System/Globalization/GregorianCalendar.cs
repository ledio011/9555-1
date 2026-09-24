using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020000FC RID: 252
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class GregorianCalendar : Calendar
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x00026938 File Offset: 0x00024B38
		public GregorianCalendar(GregorianCalendarTypes type)
		{
			this.CalendarType = type;
			this.M_AbbrEraNames = new string[]
			{
				"AD"
			};
			this.M_EraNames = new string[]
			{
				"A.D."
			};
			if (this.twoDigitYearMax == 99)
			{
				this.twoDigitYearMax = 2029;
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00026994 File Offset: 0x00024B94
		public GregorianCalendar() : this(GregorianCalendarTypes.Localized)
		{
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x000269A0 File Offset: 0x00024BA0
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					1
				};
			}
		}

		// Token: 0x17000185 RID: 389
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x000269AC File Offset: 0x00024BAC
		public virtual GregorianCalendarTypes CalendarType
		{
			set
			{
				base.CheckReadOnly();
				this.m_type = value;
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000269BC File Offset: 0x00024BBC
		public override int GetDayOfMonth(DateTime time)
		{
			return CCGregorianCalendar.GetDayOfMonth(time);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000269C4 File Offset: 0x00024BC4
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000269E0 File Offset: 0x00024BE0
		public override int GetEra(DateTime time)
		{
			return 1;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000269E4 File Offset: 0x00024BE4
		public override int GetMonth(DateTime time)
		{
			return CCGregorianCalendar.GetMonth(time);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000269EC File Offset: 0x00024BEC
		public override int GetYear(DateTime time)
		{
			return CCGregorianCalendar.GetYear(time);
		}

		// Token: 0x040003CA RID: 970
		public const int ADEra = 1;

		// Token: 0x040003CB RID: 971
		[NonSerialized]
		internal GregorianCalendarTypes m_type;

		// Token: 0x040003CC RID: 972
		private static DateTime? Min;

		// Token: 0x040003CD RID: 973
		private static DateTime? Max;
	}
}
