using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200006D RID: 109
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum AttributeTargets
	{
		// Token: 0x0400019C RID: 412
		Assembly = 1,
		// Token: 0x0400019D RID: 413
		Module = 2,
		// Token: 0x0400019E RID: 414
		Class = 4,
		// Token: 0x0400019F RID: 415
		Struct = 8,
		// Token: 0x040001A0 RID: 416
		Enum = 16,
		// Token: 0x040001A1 RID: 417
		Constructor = 32,
		// Token: 0x040001A2 RID: 418
		Method = 64,
		// Token: 0x040001A3 RID: 419
		Property = 128,
		// Token: 0x040001A4 RID: 420
		Field = 256,
		// Token: 0x040001A5 RID: 421
		Event = 512,
		// Token: 0x040001A6 RID: 422
		Interface = 1024,
		// Token: 0x040001A7 RID: 423
		Parameter = 2048,
		// Token: 0x040001A8 RID: 424
		Delegate = 4096,
		// Token: 0x040001A9 RID: 425
		ReturnValue = 8192,
		// Token: 0x040001AA RID: 426
		GenericParameter = 16384,
		// Token: 0x040001AB RID: 427
		All = 32767
	}
}
