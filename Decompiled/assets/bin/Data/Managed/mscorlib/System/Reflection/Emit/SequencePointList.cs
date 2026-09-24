using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x020001BE RID: 446
	internal class SequencePointList
	{
		// Token: 0x04000848 RID: 2120
		private const int arrayGrow = 10;

		// Token: 0x04000849 RID: 2121
		private ISymbolDocumentWriter doc;

		// Token: 0x0400084A RID: 2122
		private SequencePoint[] points;

		// Token: 0x0400084B RID: 2123
		private int count;
	}
}
