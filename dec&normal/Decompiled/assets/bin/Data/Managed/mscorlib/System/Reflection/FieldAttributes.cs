using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001C6 RID: 454
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum FieldAttributes
	{
		// Token: 0x04000896 RID: 2198
		FieldAccessMask = 7,
		// Token: 0x04000897 RID: 2199
		PrivateScope = 0,
		// Token: 0x04000898 RID: 2200
		Private = 1,
		// Token: 0x04000899 RID: 2201
		FamANDAssem = 2,
		// Token: 0x0400089A RID: 2202
		Assembly = 3,
		// Token: 0x0400089B RID: 2203
		Family = 4,
		// Token: 0x0400089C RID: 2204
		FamORAssem = 5,
		// Token: 0x0400089D RID: 2205
		Public = 6,
		// Token: 0x0400089E RID: 2206
		Static = 16,
		// Token: 0x0400089F RID: 2207
		InitOnly = 32,
		// Token: 0x040008A0 RID: 2208
		Literal = 64,
		// Token: 0x040008A1 RID: 2209
		NotSerialized = 128,
		// Token: 0x040008A2 RID: 2210
		HasFieldRVA = 256,
		// Token: 0x040008A3 RID: 2211
		SpecialName = 512,
		// Token: 0x040008A4 RID: 2212
		RTSpecialName = 1024,
		// Token: 0x040008A5 RID: 2213
		HasFieldMarshal = 4096,
		// Token: 0x040008A6 RID: 2214
		PinvokeImpl = 8192,
		// Token: 0x040008A7 RID: 2215
		HasDefault = 32768,
		// Token: 0x040008A8 RID: 2216
		ReservedMask = 38144
	}
}
