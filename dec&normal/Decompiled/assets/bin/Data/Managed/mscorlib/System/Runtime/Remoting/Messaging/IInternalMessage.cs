using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002AB RID: 683
	internal interface IInternalMessage
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600157D RID: 5501
		// (set) Token: 0x0600157E RID: 5502
		Identity TargetIdentity { get; set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600157F RID: 5503
		// (set) Token: 0x06001580 RID: 5504
		string Uri { get; set; }
	}
}
