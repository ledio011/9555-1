using System;

namespace Mono.Xml
{
	// Token: 0x0200004E RID: 78
	internal class SmallXmlParserException : SystemException
	{
		// Token: 0x0600016B RID: 363 RVA: 0x0000C758 File Offset: 0x0000A958
		public SmallXmlParserException(string msg, int line, int column) : base(string.Format("{0}. At ({1},{2})", msg, line, column))
		{
			this.line = line;
			this.column = column;
		}

		// Token: 0x04000142 RID: 322
		private int line;

		// Token: 0x04000143 RID: 323
		private int column;
	}
}
