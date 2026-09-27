using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020000FB RID: 251
	[ComVisible(true)]
	[Serializable]
	public class DaylightTime
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x00026900 File Offset: 0x00024B00
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this.m_start = start;
			this.m_end = end;
			this.m_delta = delta;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00026920 File Offset: 0x00024B20
		public DateTime Start
		{
			get
			{
				return this.m_start;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00026928 File Offset: 0x00024B28
		public DateTime End
		{
			get
			{
				return this.m_end;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00026930 File Offset: 0x00024B30
		public TimeSpan Delta
		{
			get
			{
				return this.m_delta;
			}
		}

		// Token: 0x040003C7 RID: 967
		private DateTime m_start;

		// Token: 0x040003C8 RID: 968
		private DateTime m_end;

		// Token: 0x040003C9 RID: 969
		private TimeSpan m_delta;
	}
}
