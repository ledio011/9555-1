using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C5 RID: 197
	[Serializable]
	internal class CurrentSystemTimeZone : TimeZone, IDeserializationCallback
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0001B66C File Offset: 0x0001986C
		internal CurrentSystemTimeZone()
		{
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001B680 File Offset: 0x00019880
		internal CurrentSystemTimeZone(long lnow)
		{
			DateTime dateTime = new DateTime(lnow);
			long[] array;
			string[] array2;
			if (!CurrentSystemTimeZone.GetTimeZoneData(dateTime.Year, out array, out array2))
			{
				throw new NotSupportedException(Locale.GetText("Can't get timezone name."));
			}
			this.m_standardName = Locale.GetText(array2[0]);
			this.m_daylightName = Locale.GetText(array2[1]);
			this.m_ticksOffset = array[2];
			DaylightTime daylightTimeFromData = this.GetDaylightTimeFromData(array);
			this.m_CachedDaylightChanges.Add(dateTime.Year, daylightTimeFromData);
			this.OnDeserialization(daylightTimeFromData);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001B718 File Offset: 0x00019918
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(null);
		}

		// Token: 0x06000779 RID: 1913
		[MethodImpl(4096)]
		private static extern bool GetTimeZoneData(int year, out long[] data, out string[] names);

		// Token: 0x0600077A RID: 1914 RVA: 0x0001B724 File Offset: 0x00019924
		public override DaylightTime GetDaylightChanges(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year + Locale.GetText(" is not in a range between 1 and 9999."));
			}
			if (year == CurrentSystemTimeZone.this_year)
			{
				return CurrentSystemTimeZone.this_year_dlt;
			}
			Hashtable cachedDaylightChanges = this.m_CachedDaylightChanges;
			DaylightTime result;
			lock (cachedDaylightChanges)
			{
				DaylightTime daylightTime = (DaylightTime)this.m_CachedDaylightChanges[year];
				if (daylightTime == null)
				{
					long[] data;
					string[] array;
					if (!CurrentSystemTimeZone.GetTimeZoneData(year, out data, out array))
					{
						throw new ArgumentException(Locale.GetText("Can't get timezone data for " + year));
					}
					daylightTime = this.GetDaylightTimeFromData(data);
					this.m_CachedDaylightChanges.Add(year, daylightTime);
				}
				result = daylightTime;
			}
			return result;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001B808 File Offset: 0x00019A08
		public override TimeSpan GetUtcOffset(DateTime time)
		{
			if (this.IsDaylightSavingTime(time))
			{
				return this.utcOffsetWithDLS;
			}
			return this.utcOffsetWithOutDLS;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001B824 File Offset: 0x00019A24
		private void OnDeserialization(DaylightTime dlt)
		{
			if (dlt == null)
			{
				CurrentSystemTimeZone.this_year = DateTime.Now.Year;
				long[] data;
				string[] array;
				if (!CurrentSystemTimeZone.GetTimeZoneData(CurrentSystemTimeZone.this_year, out data, out array))
				{
					throw new ArgumentException(Locale.GetText("Can't get timezone data for " + CurrentSystemTimeZone.this_year));
				}
				dlt = this.GetDaylightTimeFromData(data);
			}
			else
			{
				CurrentSystemTimeZone.this_year = dlt.Start.Year;
			}
			this.utcOffsetWithOutDLS = new TimeSpan(this.m_ticksOffset);
			this.utcOffsetWithDLS = new TimeSpan(this.m_ticksOffset + dlt.Delta.Ticks);
			CurrentSystemTimeZone.this_year_dlt = dlt;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001B8D4 File Offset: 0x00019AD4
		private DaylightTime GetDaylightTimeFromData(long[] data)
		{
			return new DaylightTime(new DateTime(data[0]), new DateTime(data[1]), new TimeSpan(data[3]));
		}

		// Token: 0x04000272 RID: 626
		private string m_standardName;

		// Token: 0x04000273 RID: 627
		private string m_daylightName;

		// Token: 0x04000274 RID: 628
		private Hashtable m_CachedDaylightChanges = new Hashtable(1);

		// Token: 0x04000275 RID: 629
		private long m_ticksOffset;

		// Token: 0x04000276 RID: 630
		[NonSerialized]
		private TimeSpan utcOffsetWithOutDLS;

		// Token: 0x04000277 RID: 631
		[NonSerialized]
		private TimeSpan utcOffsetWithDLS;

		// Token: 0x04000278 RID: 632
		private static int this_year;

		// Token: 0x04000279 RID: 633
		private static DaylightTime this_year_dlt;
	}
}
