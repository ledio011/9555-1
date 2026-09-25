using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002B0 RID: 688
	[ComVisible(true)]
	public interface IMethodCallMessage : IMessage, IMethodMessage
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001584 RID: 5508
		int InArgCount { get; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001585 RID: 5509
		object[] InArgs { get; }

		// Token: 0x06001586 RID: 5510
		object GetInArg(int argNum);

		// Token: 0x06001587 RID: 5511
		string GetInArgName(int index);
	}
}
