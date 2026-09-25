using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000280 RID: 640
	[ComVisible(true)]
	public interface IContributeServerContextSink
	{
		// Token: 0x060014C1 RID: 5313
		IMessageSink GetServerContextSink(IMessageSink nextSink);
	}
}
