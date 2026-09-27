using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001D2 RID: 466
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum MethodAttributes
	{
		// Token: 0x040008CD RID: 2253
		MemberAccessMask = 7,
		// Token: 0x040008CE RID: 2254
		PrivateScope = 0,
		// Token: 0x040008CF RID: 2255
		Private = 1,
		// Token: 0x040008D0 RID: 2256
		FamANDAssem = 2,
		// Token: 0x040008D1 RID: 2257
		Assembly = 3,
		// Token: 0x040008D2 RID: 2258
		Family = 4,
		// Token: 0x040008D3 RID: 2259
		FamORAssem = 5,
		// Token: 0x040008D4 RID: 2260
		Public = 6,
		// Token: 0x040008D5 RID: 2261
		Static = 16,
		// Token: 0x040008D6 RID: 2262
		Final = 32,
		// Token: 0x040008D7 RID: 2263
		Virtual = 64,
		// Token: 0x040008D8 RID: 2264
		HideBySig = 128,
		// Token: 0x040008D9 RID: 2265
		VtableLayoutMask = 256,
		// Token: 0x040008DA RID: 2266
		CheckAccessOnOverride = 512,
		// Token: 0x040008DB RID: 2267
		ReuseSlot = 0,
		// Token: 0x040008DC RID: 2268
		NewSlot = 256,
		// Token: 0x040008DD RID: 2269
		Abstract = 1024,
		// Token: 0x040008DE RID: 2270
		SpecialName = 2048,
		// Token: 0x040008DF RID: 2271
		PinvokeImpl = 8192,
		// Token: 0x040008E0 RID: 2272
		UnmanagedExport = 8,
		// Token: 0x040008E1 RID: 2273
		RTSpecialName = 4096,
		// Token: 0x040008E2 RID: 2274
		ReservedMask = 53248,
		// Token: 0x040008E3 RID: 2275
		HasSecurity = 16384,
		// Token: 0x040008E4 RID: 2276
		RequireSecObject = 32768
	}
}
