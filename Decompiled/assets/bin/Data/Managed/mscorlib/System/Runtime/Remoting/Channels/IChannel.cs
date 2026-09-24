using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000269 RID: 617
	[ComVisible(true)]
	public interface IChannel
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001467 RID: 5223
		string ChannelName { get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001468 RID: 5224
		int ChannelPriority { get; }
	}
}
