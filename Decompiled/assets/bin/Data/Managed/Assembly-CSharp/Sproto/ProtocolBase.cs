using System;

namespace Sproto
{
	// Token: 0x0200080C RID: 2060
	public abstract class ProtocolBase
	{
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x000C1544 File Offset: 0x000BF744
		public ProtocolFunctionDictionary Protocol
		{
			get
			{
				return this._Protocol;
			}
		}

		// Token: 0x04002132 RID: 8498
		private ProtocolFunctionDictionary _Protocol = new ProtocolFunctionDictionary();
	}
}
