using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200027F RID: 639
	[ComVisible(true)]
	public interface IContributeObjectSink
	{
		// Token: 0x060014C0 RID: 5312
		IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
