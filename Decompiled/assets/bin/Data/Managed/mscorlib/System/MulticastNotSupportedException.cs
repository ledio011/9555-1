using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000164 RID: 356
	[ComVisible(true)]
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x00033ED0 File Offset: 0x000320D0
		public MulticastNotSupportedException() : base(Locale.GetText("This operation cannot be performed with the specified delagates."))
		{
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00033EE4 File Offset: 0x000320E4
		public MulticastNotSupportedException(string message) : base(message)
		{
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00033EF0 File Offset: 0x000320F0
		internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
