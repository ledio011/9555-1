using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200027D RID: 637
	[ComVisible(true)]
	public interface IContributeClientContextSink
	{
		// Token: 0x060014BE RID: 5310
		IMessageSink GetClientContextSink(IMessageSink nextSink);
	}
}
