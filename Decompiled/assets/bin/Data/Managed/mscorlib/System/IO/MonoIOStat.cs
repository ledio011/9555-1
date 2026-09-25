using System;

namespace System.IO
{
	// Token: 0x02000131 RID: 305
	internal struct MonoIOStat
	{
		// Token: 0x040004F1 RID: 1265
		public string Name;

		// Token: 0x040004F2 RID: 1266
		public FileAttributes Attributes;

		// Token: 0x040004F3 RID: 1267
		public long Length;

		// Token: 0x040004F4 RID: 1268
		public long CreationTime;

		// Token: 0x040004F5 RID: 1269
		public long LastAccessTime;

		// Token: 0x040004F6 RID: 1270
		public long LastWriteTime;
	}
}
