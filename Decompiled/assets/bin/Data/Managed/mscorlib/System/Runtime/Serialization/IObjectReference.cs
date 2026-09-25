using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000302 RID: 770
	[ComVisible(true)]
	public interface IObjectReference
	{
		// Token: 0x060017CB RID: 6091
		object GetRealObject(StreamingContext context);
	}
}
