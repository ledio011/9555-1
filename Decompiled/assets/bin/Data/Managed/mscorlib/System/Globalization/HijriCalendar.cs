using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020000FE RID: 254
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class HijriCalendar : Calendar
	{
		// Token: 0x06000A0A RID: 2570 RVA: 0x000269F4 File Offset: 0x00024BF4
		public HijriCalendar()
		{
			this.M_AbbrEraNames = new string[]
			{
				"A.H."
			};
			this.M_EraNames = new string[]
			{
				"Anno Hegirae"
			};
			if (this.twoDigitYearMax == 99)
			{
				this.twoDigitYearMax = 1451;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00026AAC File Offset: 0x00024CAC
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					HijriCalendar.HijriEra
				};
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00026ABC File Offset: 0x00024CBC
		internal virtual int AddHijriDate
		{
			get
			{
				return this.M_AddHijriDate;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00026AC4 File Offset: 0x00024CC4
		internal void M_CheckFixedHijri(string param, int rdHijri)
		{
			if (rdHijri < HijriCalendar.M_MinFixed || rdHijri > HijriCalendar.M_MaxFixed - this.AddHijriDate)
			{
				StringWriter stringWriter = new StringWriter();
				int num;
				int num2;
				int num3;
				CCHijriCalendar.dmy_from_fixed(out num, out num2, out num3, HijriCalendar.M_MaxFixed - this.AddHijriDate);
				if (this.AddHijriDate != 0)
				{
					stringWriter.Write("This HijriCalendar (AddHijriDate {0}) allows dates from 1. 1. 1 to {1}. {2}. {3}.", new object[]
					{
						this.AddHijriDate,
						num,
						num2,
						num3
					});
				}
				else
				{
					stringWriter.Write("HijriCalendar allows dates from 1.1.1 to {0}.{1}.{2}.", num, num2, num3);
				}
				throw new ArgumentOutOfRangeException(param, stringWriter.ToString());
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00026B80 File Offset: 0x00024D80
		internal void M_CheckDateTime(DateTime time)
		{
			int rdHijri = CCFixed.FromDateTime(time) - this.AddHijriDate;
			this.M_CheckFixedHijri("time", rdHijri);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00026BA8 File Offset: 0x00024DA8
		internal int M_FromDateTime(DateTime time)
		{
			return CCFixed.FromDateTime(time) - this.AddHijriDate;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00026BB8 File Offset: 0x00024DB8
		public override int GetDayOfMonth(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCHijriCalendar.day_from_fixed(num);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00026BE0 File Offset: 0x00024DE0
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCFixed.day_of_week(num);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00026C08 File Offset: 0x00024E08
		public override int GetEra(DateTime time)
		{
			this.M_CheckDateTime(time);
			return HijriCalendar.HijriEra;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00026C18 File Offset: 0x00024E18
		public override int GetMonth(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCHijriCalendar.month_from_fixed(num);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00026C40 File Offset: 0x00024E40
		public override int GetYear(DateTime time)
		{
			int num = this.M_FromDateTime(time);
			this.M_CheckFixedHijri("time", num);
			return CCHijriCalendar.year_from_fixed(num);
		}

		// Token: 0x040003D5 RID: 981
		public static readonly int HijriEra = 1;

		// Token: 0x040003D6 RID: 982
		internal static readonly int M_MinFixed = CCHijriCalendar.fixed_from_dmy(1, 1, 1);

		// Token: 0x040003D7 RID: 983
		internal static readonly int M_MaxFixed = CCGregorianCalendar.fixed_from_dmy(31, 12, 9999);

		// Token: 0x040003D8 RID: 984
		internal int M_AddHijriDate;

		// Token: 0x040003D9 RID: 985
		private static DateTime Min = new DateTime(622, 7, 18, 0, 0, 0);

		// Token: 0x040003DA RID: 986
		private static DateTime Max = new DateTime(9999, 12, 31, 11, 59, 59);
	}
}
