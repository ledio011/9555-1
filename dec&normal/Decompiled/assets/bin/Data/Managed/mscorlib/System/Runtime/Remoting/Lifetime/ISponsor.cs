using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000291 RID: 657
	[ComVisible(true)]
	public interface ISponsor
	{
		// Token: 0x060014FC RID: 5372
		TimeSpan Renewal(ILease lease);
	}
}
