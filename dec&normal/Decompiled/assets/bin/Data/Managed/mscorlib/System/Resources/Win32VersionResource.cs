using System;
using System.Collections;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x020001FE RID: 510
	[DefaultMember("Item")]
	internal class Win32VersionResource : Win32Resource
	{
		// Token: 0x040009A9 RID: 2473
		public string[] WellKnownProperties;

		// Token: 0x040009AA RID: 2474
		private long signature;

		// Token: 0x040009AB RID: 2475
		private int struct_version;

		// Token: 0x040009AC RID: 2476
		private long file_version;

		// Token: 0x040009AD RID: 2477
		private long product_version;

		// Token: 0x040009AE RID: 2478
		private int file_flags_mask;

		// Token: 0x040009AF RID: 2479
		private int file_flags;

		// Token: 0x040009B0 RID: 2480
		private int file_os;

		// Token: 0x040009B1 RID: 2481
		private int file_type;

		// Token: 0x040009B2 RID: 2482
		private int file_subtype;

		// Token: 0x040009B3 RID: 2483
		private long file_date;

		// Token: 0x040009B4 RID: 2484
		private int file_lang;

		// Token: 0x040009B5 RID: 2485
		private int file_codepage;

		// Token: 0x040009B6 RID: 2486
		private Hashtable properties;
	}
}
