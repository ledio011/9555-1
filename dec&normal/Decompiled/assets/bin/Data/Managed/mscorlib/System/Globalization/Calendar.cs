using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020000ED RID: 237
	[ComVisible(true)]
	[Serializable]
	public abstract class Calendar : ICloneable
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x00024204 File Offset: 0x00022404
		protected Calendar()
		{
			this.twoDigitYearMax = 99;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600094B RID: 2379
		public abstract int[] Eras { get; }

		// Token: 0x0600094C RID: 2380 RVA: 0x00024214 File Offset: 0x00022414
		[ComVisible(false)]
		public virtual object Clone()
		{
			Calendar calendar = (Calendar)base.MemberwiseClone();
			calendar.m_isReadOnly = false;
			return calendar;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00024238 File Offset: 0x00022438
		internal void CheckReadOnly()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException("This Calendar is read-only.");
			}
		}

		// Token: 0x0600094E RID: 2382
		public abstract int GetDayOfMonth(DateTime time);

		// Token: 0x0600094F RID: 2383
		public abstract DayOfWeek GetDayOfWeek(DateTime time);

		// Token: 0x06000950 RID: 2384
		public abstract int GetEra(DateTime time);

		// Token: 0x06000951 RID: 2385
		public abstract int GetMonth(DateTime time);

		// Token: 0x06000952 RID: 2386
		public abstract int GetYear(DateTime time);

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00024250 File Offset: 0x00022450
		internal string[] EraNames
		{
			get
			{
				if (this.M_EraNames == null || this.M_EraNames.Length != this.Eras.Length)
				{
					throw new Exception("Internal: M_EraNames not initialized!");
				}
				return (string[])this.M_EraNames.Clone();
			}
		}

		// Token: 0x04000328 RID: 808
		public const int CurrentEra = 0;

		// Token: 0x04000329 RID: 809
		[NonSerialized]
		private bool m_isReadOnly;

		// Token: 0x0400032A RID: 810
		[NonSerialized]
		internal int twoDigitYearMax;

		// Token: 0x0400032B RID: 811
		[NonSerialized]
		private int M_MaxYearValue;

		// Token: 0x0400032C RID: 812
		[NonSerialized]
		internal string[] M_AbbrEraNames;

		// Token: 0x0400032D RID: 813
		[NonSerialized]
		internal string[] M_EraNames;

		// Token: 0x0400032E RID: 814
		internal int m_currentEraValue;
	}
}
