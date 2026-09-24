using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000075 RID: 117
	internal struct Mark
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000AA98 File Offset: 0x00008C98
		public bool IsDefined
		{
			get
			{
				return this.Start >= 0 && this.End >= 0;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		public int Index
		{
			get
			{
				return (this.Start >= this.End) ? this.End : this.Start;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000AADC File Offset: 0x00008CDC
		public int Length
		{
			get
			{
				return (this.Start >= this.End) ? (this.Start - this.End) : (this.End - this.Start);
			}
		}

		// Token: 0x040009AF RID: 2479
		public int Start;

		// Token: 0x040009B0 RID: 2480
		public int End;

		// Token: 0x040009B1 RID: 2481
		public int Previous;
	}
}
