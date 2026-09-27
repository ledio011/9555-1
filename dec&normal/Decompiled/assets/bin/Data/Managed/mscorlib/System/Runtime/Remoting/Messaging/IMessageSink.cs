using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002AF RID: 687
	[ComVisible(true)]
	public interface IMessageSink
	{
		// Token: 0x06001582 RID: 5506
		IMessage SyncProcessMessage(IMessage msg);

		// Token: 0x06001583 RID: 5507
		IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink);
	}
}
