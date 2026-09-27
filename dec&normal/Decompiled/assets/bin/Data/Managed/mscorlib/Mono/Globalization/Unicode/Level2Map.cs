using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000032 RID: 50
	internal class Level2Map
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003C94 File Offset: 0x00001E94
		public Level2Map(byte source, byte replace)
		{
			this.Source = source;
			this.Replace = replace;
		}

		// Token: 0x04000081 RID: 129
		public byte Source;

		// Token: 0x04000082 RID: 130
		public byte Replace;
	}
}
