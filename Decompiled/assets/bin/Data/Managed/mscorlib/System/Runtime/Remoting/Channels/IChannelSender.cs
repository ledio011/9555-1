using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200026C RID: 620
	[ComVisible(true)]
	public interface IChannelSender : IChannel
	{
		// Token: 0x0600146B RID: 5227
		IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI);
	}
}
