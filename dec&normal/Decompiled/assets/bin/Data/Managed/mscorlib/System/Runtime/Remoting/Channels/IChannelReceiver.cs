using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200026B RID: 619
	[ComVisible(true)]
	public interface IChannelReceiver : IChannel
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001469 RID: 5225
		object ChannelData { get; }

		// Token: 0x0600146A RID: 5226
		void StartListening(object data);
	}
}
