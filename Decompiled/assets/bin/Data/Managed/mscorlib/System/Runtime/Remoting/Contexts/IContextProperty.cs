using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200027C RID: 636
	[ComVisible(true)]
	public interface IContextProperty
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060014BB RID: 5307
		string Name { get; }

		// Token: 0x060014BC RID: 5308
		void Freeze(Context newContext);

		// Token: 0x060014BD RID: 5309
		bool IsNewContextOK(Context newCtx);
	}
}
