using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000167 RID: 359
	[ComVisible(true)]
	[Serializable]
	public class NotSupportedException : SystemException
	{
		// Token: 0x06000D77 RID: 3447 RVA: 0x00033F5C File Offset: 0x0003215C
		public NotSupportedException() : base(Locale.GetText("Operation is not supported."))
		{
			base.HResult = -2146233067;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00033F7C File Offset: 0x0003217C
		public NotSupportedException(string message) : base(message)
		{
			base.HResult = -2146233067;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00033F90 File Offset: 0x00032190
		protected NotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000582 RID: 1410
		private const int Result = -2146233067;
	}
}
