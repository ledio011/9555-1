using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020002FF RID: 767
	[ComVisible(true)]
	public interface IDeserializationCallback
	{
		// Token: 0x060017BE RID: 6078
		void OnDeserialization(object sender);
	}
}
