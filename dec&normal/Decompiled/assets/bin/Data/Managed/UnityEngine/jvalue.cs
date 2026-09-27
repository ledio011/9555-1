using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200009F RID: 159
	[StructLayout(2)]
	public struct jvalue
	{
		// Token: 0x040001D3 RID: 467
		[FieldOffset(0)]
		public bool z;

		// Token: 0x040001D4 RID: 468
		[FieldOffset(0)]
		public byte b;

		// Token: 0x040001D5 RID: 469
		[FieldOffset(0)]
		public char c;

		// Token: 0x040001D6 RID: 470
		[FieldOffset(0)]
		public short s;

		// Token: 0x040001D7 RID: 471
		[FieldOffset(0)]
		public int i;

		// Token: 0x040001D8 RID: 472
		[FieldOffset(0)]
		public long j;

		// Token: 0x040001D9 RID: 473
		[FieldOffset(0)]
		public float f;

		// Token: 0x040001DA RID: 474
		[FieldOffset(0)]
		public double d;

		// Token: 0x040001DB RID: 475
		[FieldOffset(0)]
		public IntPtr l;
	}
}
