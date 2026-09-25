using System;

namespace Sproto
{
	// Token: 0x02000816 RID: 2070
	public class SprotoTypeSize
	{
		// Token: 0x060031E6 RID: 12774 RVA: 0x000C340C File Offset: 0x000C160C
		public static void error(string info)
		{
			throw new Exception(info);
		}

		// Token: 0x04002153 RID: 8531
		public static readonly int sizeof_header = 2;

		// Token: 0x04002154 RID: 8532
		public static readonly int sizeof_length = 4;

		// Token: 0x04002155 RID: 8533
		public static readonly int sizeof_field = 2;

		// Token: 0x04002156 RID: 8534
		public static readonly int encode_max_size = 16777216;
	}
}
