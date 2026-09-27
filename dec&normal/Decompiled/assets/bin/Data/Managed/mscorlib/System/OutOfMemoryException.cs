using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000171 RID: 369
	[ComVisible(true)]
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		// Token: 0x06000E0B RID: 3595 RVA: 0x00037F08 File Offset: 0x00036108
		public OutOfMemoryException() : base(Locale.GetText("Out of memory."))
		{
			base.HResult = -2147024882;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00037F28 File Offset: 0x00036128
		public OutOfMemoryException(string message) : base(message)
		{
			base.HResult = -2147024882;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00037F3C File Offset: 0x0003613C
		public OutOfMemoryException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024882;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00037F54 File Offset: 0x00036154
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040005CD RID: 1485
		private const int Result = -2147024882;
	}
}
