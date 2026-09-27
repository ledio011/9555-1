using System;

namespace System.IO
{
	// Token: 0x02000135 RID: 309
	internal class SearchPattern
	{
		// Token: 0x040004FF RID: 1279
		private SearchPattern.Op ops;

		// Token: 0x04000500 RID: 1280
		private bool ignore;

		// Token: 0x04000501 RID: 1281
		internal static readonly char[] WildcardChars = new char[]
		{
			'*',
			'?'
		};

		// Token: 0x04000502 RID: 1282
		internal static readonly char[] InvalidChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x02000136 RID: 310
		private class Op
		{
			// Token: 0x04000503 RID: 1283
			public SearchPattern.OpCode Code;

			// Token: 0x04000504 RID: 1284
			public string Argument;

			// Token: 0x04000505 RID: 1285
			public SearchPattern.Op Next;
		}

		// Token: 0x02000137 RID: 311
		private enum OpCode
		{
			// Token: 0x04000507 RID: 1287
			ExactString,
			// Token: 0x04000508 RID: 1288
			AnyChar,
			// Token: 0x04000509 RID: 1289
			AnyString,
			// Token: 0x0400050A RID: 1290
			End,
			// Token: 0x0400050B RID: 1291
			True
		}
	}
}
