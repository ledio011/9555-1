using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001D1 RID: 465
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum MemberTypes
	{
		// Token: 0x040008C3 RID: 2243
		Constructor = 1,
		// Token: 0x040008C4 RID: 2244
		Event = 2,
		// Token: 0x040008C5 RID: 2245
		Field = 4,
		// Token: 0x040008C6 RID: 2246
		Method = 8,
		// Token: 0x040008C7 RID: 2247
		Property = 16,
		// Token: 0x040008C8 RID: 2248
		TypeInfo = 32,
		// Token: 0x040008C9 RID: 2249
		Custom = 64,
		// Token: 0x040008CA RID: 2250
		NestedType = 128,
		// Token: 0x040008CB RID: 2251
		All = 191
	}
}
