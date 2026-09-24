using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200027B RID: 635
	[ComVisible(true)]
	public interface IContextAttribute
	{
		// Token: 0x060014B9 RID: 5305
		void GetPropertiesForNewContext(IConstructionCallMessage msg);

		// Token: 0x060014BA RID: 5306
		bool IsContextOK(Context ctx, IConstructionCallMessage msg);
	}
}
