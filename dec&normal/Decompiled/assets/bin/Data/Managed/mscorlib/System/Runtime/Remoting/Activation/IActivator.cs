using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200025A RID: 602
	[ComVisible(true)]
	public interface IActivator
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001428 RID: 5160
		IActivator NextActivator { get; }

		// Token: 0x06001429 RID: 5161
		IConstructionReturnMessage Activate(IConstructionCallMessage msg);
	}
}
