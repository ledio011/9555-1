using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002AD RID: 685
	[ComVisible(true)]
	public interface IMessage
	{
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001581 RID: 5505
		IDictionary Properties { get; }
	}
}
