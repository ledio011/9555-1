using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000281 RID: 641
	[ComVisible(true)]
	public interface IDynamicMessageSink
	{
		// Token: 0x060014C2 RID: 5314
		void ProcessMessageFinish(IMessage replyMsg, bool bCliSide, bool bAsync);

		// Token: 0x060014C3 RID: 5315
		void ProcessMessageStart(IMessage reqMsg, bool bCliSide, bool bAsync);
	}
}
