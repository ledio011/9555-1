using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x020002DA RID: 730
	[ComVisible(true)]
	public interface ITrackingHandler
	{
		// Token: 0x060016FF RID: 5887
		void DisconnectedObject(object obj);

		// Token: 0x06001700 RID: 5888
		void MarshaledObject(object obj, ObjRef or);

		// Token: 0x06001701 RID: 5889
		void UnmarshaledObject(object obj, ObjRef or);
	}
}
