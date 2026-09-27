using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020002B3 RID: 691
	[ComVisible(true)]
	public interface IRemotingFormatter : IFormatter
	{
		// Token: 0x06001599 RID: 5529
		object Deserialize(Stream serializationStream, HeaderHandler handler);

		// Token: 0x0600159A RID: 5530
		void Serialize(Stream serializationStream, object graph, Header[] headers);
	}
}
