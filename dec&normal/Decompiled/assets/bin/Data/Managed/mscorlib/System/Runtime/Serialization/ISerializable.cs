using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000303 RID: 771
	[ComVisible(true)]
	public interface ISerializable
	{
		// Token: 0x060017CC RID: 6092
		void GetObjectData(SerializationInfo info, StreamingContext context);
	}
}
