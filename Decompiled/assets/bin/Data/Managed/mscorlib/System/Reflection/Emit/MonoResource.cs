using System;
using System.IO;

namespace System.Reflection.Emit
{
	// Token: 0x020001AF RID: 431
	internal struct MonoResource
	{
		// Token: 0x0400070E RID: 1806
		public byte[] data;

		// Token: 0x0400070F RID: 1807
		public string name;

		// Token: 0x04000710 RID: 1808
		public string filename;

		// Token: 0x04000711 RID: 1809
		public ResourceAttributes attrs;

		// Token: 0x04000712 RID: 1810
		public int offset;

		// Token: 0x04000713 RID: 1811
		public Stream stream;
	}
}
