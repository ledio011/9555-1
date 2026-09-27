using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000169 RID: 361
	[ComVisible(true)]
	[Serializable]
	public class NullReferenceException : SystemException
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x00034124 File Offset: 0x00032324
		public NullReferenceException() : base(Locale.GetText("A null value was found where an object instance was required."))
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00034144 File Offset: 0x00032344
		public NullReferenceException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00034158 File Offset: 0x00032358
		public NullReferenceException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00034170 File Offset: 0x00032370
		protected NullReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000585 RID: 1413
		private const int Result = -2147467261;
	}
}
