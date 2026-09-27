using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000172 RID: 370
	[ComVisible(true)]
	[Serializable]
	public class OverflowException : ArithmeticException
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x00037F60 File Offset: 0x00036160
		public OverflowException() : base(Locale.GetText("Number overflow."))
		{
			base.HResult = -2146233066;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00037F80 File Offset: 0x00036180
		public OverflowException(string message) : base(message)
		{
			base.HResult = -2146233066;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00037F94 File Offset: 0x00036194
		protected OverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040005CE RID: 1486
		private const int Result = -2146233066;
	}
}
