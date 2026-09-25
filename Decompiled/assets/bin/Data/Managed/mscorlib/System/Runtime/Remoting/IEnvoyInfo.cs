using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200028C RID: 652
	[ComVisible(true)]
	public interface IEnvoyInfo
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060014ED RID: 5357
		IMessageSink EnvoySinks { get; }
	}
}
