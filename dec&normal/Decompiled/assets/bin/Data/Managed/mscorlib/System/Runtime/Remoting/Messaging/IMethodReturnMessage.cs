using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002B2 RID: 690
	[ComVisible(true)]
	public interface IMethodReturnMessage : IMessage, IMethodMessage
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001593 RID: 5523
		Exception Exception { get; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001594 RID: 5524
		int OutArgCount { get; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001595 RID: 5525
		object[] OutArgs { get; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001596 RID: 5526
		object ReturnValue { get; }

		// Token: 0x06001597 RID: 5527
		object GetOutArg(int argNum);

		// Token: 0x06001598 RID: 5528
		string GetOutArgName(int index);
	}
}
