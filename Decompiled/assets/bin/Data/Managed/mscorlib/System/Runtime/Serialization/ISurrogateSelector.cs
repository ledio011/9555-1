using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000305 RID: 773
	[ComVisible(true)]
	public interface ISurrogateSelector
	{
		// Token: 0x060017CF RID: 6095
		ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector);
	}
}
