using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x02000104 RID: 260
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with.NET")]
	[Serializable]
	public class ThaiBuddhistCalendar : Calendar
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x00027854 File Offset: 0x00025A54
		public ThaiBuddhistCalendar()
		{
			this.M_AbbrEraNames = new string[]
			{
				"T.B.C.E."
			};
			this.M_EraNames = new string[]
			{
				"ThaiBuddhist current era"
			};
			if (this.twoDigitYearMax == 99)
			{
				this.twoDigitYearMax = 2572;
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000278A8 File Offset: 0x00025AA8
		static ThaiBuddhistCalendar()
		{
			ThaiBuddhistCalendar.M_EraHandler = new CCGregorianEraHandler();
			ThaiBuddhistCalendar.M_EraHandler.appendEra(1, CCGregorianCalendar.fixed_from_dmy(1, 1, -542));
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00027900 File Offset: 0x00025B00
		public override int[] Eras
		{
			get
			{
				return (int[])ThaiBuddhistCalendar.M_EraHandler.Eras.Clone();
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00027918 File Offset: 0x00025B18
		public override int GetDayOfMonth(DateTime time)
		{
			return CCGregorianCalendar.GetDayOfMonth(time);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00027920 File Offset: 0x00025B20
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			return CCFixed.day_of_week(date);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002793C File Offset: 0x00025B3C
		public override int GetEra(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int result;
			ThaiBuddhistCalendar.M_EraHandler.EraYear(out result, date);
			return result;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00027960 File Offset: 0x00025B60
		public override int GetMonth(DateTime time)
		{
			return CCGregorianCalendar.GetMonth(time);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00027968 File Offset: 0x00025B68
		public override int GetYear(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			int num;
			return ThaiBuddhistCalendar.M_EraHandler.EraYear(out num, date);
		}

		// Token: 0x04000426 RID: 1062
		public const int ThaiBuddhistEra = 1;

		// Token: 0x04000427 RID: 1063
		internal static readonly CCGregorianEraHandler M_EraHandler;

		// Token: 0x04000428 RID: 1064
		private static DateTime ThaiMin = new DateTime(1, 1, 1, 0, 0, 0);

		// Token: 0x04000429 RID: 1065
		private static DateTime ThaiMax = new DateTime(9999, 12, 31, 11, 59, 59);
	}
}
