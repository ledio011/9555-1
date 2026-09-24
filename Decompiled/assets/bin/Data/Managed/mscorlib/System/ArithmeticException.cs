using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000061 RID: 97
	[ComVisible(true)]
	[Serializable]
	public class ArithmeticException : SystemException
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000EC58 File Offset: 0x0000CE58
		public ArithmeticException() : base(Locale.GetText("Overflow or underflow in the arithmetic operation."))
		{
			base.HResult = -2147024362;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000EC78 File Offset: 0x0000CE78
		public ArithmeticException(string message) : base(message)
		{
			base.HResult = -2147024362;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public ArithmeticException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024362;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000ECA4 File Offset: 0x0000CEA4
		protected ArithmeticException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400018C RID: 396
		private const int Result = -2147024362;
	}
}
