using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000112 RID: 274
	[ComVisible(true)]
	[Serializable]
	public sealed class IndexOutOfRangeException : SystemException
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x00028768 File Offset: 0x00026968
		public IndexOutOfRangeException() : base(Locale.GetText("Array index is out of range."))
		{
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002877C File Offset: 0x0002697C
		public IndexOutOfRangeException(string message) : base(message)
		{
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00028788 File Offset: 0x00026988
		internal IndexOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
