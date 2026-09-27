using System;
using System.Collections;

namespace System.Globalization
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	internal class CCGregorianEraHandler
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x000244E4 File Offset: 0x000226E4
		public CCGregorianEraHandler()
		{
			this._Eras = new SortedList();
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x000244F8 File Offset: 0x000226F8
		public int[] Eras
		{
			get
			{
				int[] array = new int[this._Eras.Count];
				for (int i = 0; i < this._Eras.Count; i++)
				{
					array[i] = ((CCGregorianEraHandler.Era)this._Eras.GetByIndex(i)).Nr;
				}
				return array;
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00024550 File Offset: 0x00022750
		public void appendEra(int nr, int rd_start, int rd_end)
		{
			CCGregorianEraHandler.Era era = new CCGregorianEraHandler.Era(nr, rd_start, rd_end);
			this._Eras[nr] = era;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00024580 File Offset: 0x00022780
		public void appendEra(int nr, int rd_start)
		{
			this.appendEra(nr, rd_start, CCFixed.FromDateTime(DateTime.MaxValue));
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00024594 File Offset: 0x00022794
		public int EraYear(out int era, int date)
		{
			IList valueList = this._Eras.GetValueList();
			foreach (object obj in valueList)
			{
				CCGregorianEraHandler.Era era2 = (CCGregorianEraHandler.Era)obj;
				if (era2.Covers(date))
				{
					return era2.EraYear(out era, date);
				}
			}
			throw new ArgumentOutOfRangeException("date", "Time value was out of era range.");
		}

		// Token: 0x04000330 RID: 816
		private SortedList _Eras;

		// Token: 0x020000F1 RID: 241
		[Serializable]
		private struct Era
		{
			// Token: 0x06000965 RID: 2405 RVA: 0x00024628 File Offset: 0x00022828
			public Era(int nr, int start, int end)
			{
				if (nr == 0)
				{
					throw new ArgumentException("Era number shouldn't be zero.");
				}
				this._nr = nr;
				if (start > end)
				{
					throw new ArgumentException("Era should start before end.");
				}
				this._start = start;
				this._end = end;
				this._gregorianYearStart = CCGregorianCalendar.year_from_fixed(this._start);
				int num = CCGregorianCalendar.year_from_fixed(this._end);
				this._maxYear = num - this._gregorianYearStart + 1;
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000966 RID: 2406 RVA: 0x0002469C File Offset: 0x0002289C
			public int Nr
			{
				get
				{
					return this._nr;
				}
			}

			// Token: 0x06000967 RID: 2407 RVA: 0x000246A4 File Offset: 0x000228A4
			public bool Covers(int date)
			{
				return this._start <= date && date <= this._end;
			}

			// Token: 0x06000968 RID: 2408 RVA: 0x000246C4 File Offset: 0x000228C4
			public int EraYear(out int era, int date)
			{
				if (!this.Covers(date))
				{
					throw new ArgumentOutOfRangeException("date", "Time was out of Era range.");
				}
				int num = CCGregorianCalendar.year_from_fixed(date);
				era = this._nr;
				return num - this._gregorianYearStart + 1;
			}

			// Token: 0x04000331 RID: 817
			private int _nr;

			// Token: 0x04000332 RID: 818
			private int _start;

			// Token: 0x04000333 RID: 819
			private int _gregorianYearStart;

			// Token: 0x04000334 RID: 820
			private int _end;

			// Token: 0x04000335 RID: 821
			private int _maxYear;
		}
	}
}
