using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000304 RID: 772
	[ComVisible(true)]
	public interface ISerializationSurrogate
	{
		// Token: 0x060017CD RID: 6093
		void GetObjectData(object obj, SerializationInfo info, StreamingContext context);

		// Token: 0x060017CE RID: 6094
		object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
	}
}
