using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200027E RID: 638
	[ComVisible(true)]
	public interface IContributeEnvoySink
	{
		// Token: 0x060014BF RID: 5311
		IMessageSink GetEnvoySink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
