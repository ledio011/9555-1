using System;

namespace System
{
	// Token: 0x0200002A RID: 42
	internal class DefaultUriParser : System.UriParser
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public DefaultUriParser()
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002CDC File Offset: 0x00000EDC
		public DefaultUriParser(string scheme)
		{
			this.scheme_name = scheme;
		}
	}
}
