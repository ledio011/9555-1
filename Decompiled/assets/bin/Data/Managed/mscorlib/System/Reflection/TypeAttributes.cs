using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001F3 RID: 499
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeAttributes
	{
		// Token: 0x04000968 RID: 2408
		VisibilityMask = 7,
		// Token: 0x04000969 RID: 2409
		NotPublic = 0,
		// Token: 0x0400096A RID: 2410
		Public = 1,
		// Token: 0x0400096B RID: 2411
		NestedPublic = 2,
		// Token: 0x0400096C RID: 2412
		NestedPrivate = 3,
		// Token: 0x0400096D RID: 2413
		NestedFamily = 4,
		// Token: 0x0400096E RID: 2414
		NestedAssembly = 5,
		// Token: 0x0400096F RID: 2415
		NestedFamANDAssem = 6,
		// Token: 0x04000970 RID: 2416
		NestedFamORAssem = 7,
		// Token: 0x04000971 RID: 2417
		LayoutMask = 24,
		// Token: 0x04000972 RID: 2418
		AutoLayout = 0,
		// Token: 0x04000973 RID: 2419
		SequentialLayout = 8,
		// Token: 0x04000974 RID: 2420
		ExplicitLayout = 16,
		// Token: 0x04000975 RID: 2421
		ClassSemanticsMask = 32,
		// Token: 0x04000976 RID: 2422
		Class = 0,
		// Token: 0x04000977 RID: 2423
		Interface = 32,
		// Token: 0x04000978 RID: 2424
		Abstract = 128,
		// Token: 0x04000979 RID: 2425
		Sealed = 256,
		// Token: 0x0400097A RID: 2426
		SpecialName = 1024,
		// Token: 0x0400097B RID: 2427
		Import = 4096,
		// Token: 0x0400097C RID: 2428
		Serializable = 8192,
		// Token: 0x0400097D RID: 2429
		StringFormatMask = 196608,
		// Token: 0x0400097E RID: 2430
		AnsiClass = 0,
		// Token: 0x0400097F RID: 2431
		UnicodeClass = 65536,
		// Token: 0x04000980 RID: 2432
		AutoClass = 131072,
		// Token: 0x04000981 RID: 2433
		BeforeFieldInit = 1048576,
		// Token: 0x04000982 RID: 2434
		ReservedMask = 264192,
		// Token: 0x04000983 RID: 2435
		RTSpecialName = 2048,
		// Token: 0x04000984 RID: 2436
		HasSecurity = 262144,
		// Token: 0x04000985 RID: 2437
		CustomFormatClass = 196608,
		// Token: 0x04000986 RID: 2438
		CustomFormatMask = 12582912
	}
}
