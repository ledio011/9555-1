using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000069 RID: 105
	internal interface IMachineFactory
	{
		// Token: 0x060001D7 RID: 471
		IMachine NewInstance();

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D8 RID: 472
		// (set) Token: 0x060001D9 RID: 473
		IDictionary Mapping { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001DA RID: 474
		int GroupCount { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001DB RID: 475
		// (set) Token: 0x060001DC RID: 476
		int Gap { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001DD RID: 477
		// (set) Token: 0x060001DE RID: 478
		string[] NamesMapping { get; set; }
	}
}
