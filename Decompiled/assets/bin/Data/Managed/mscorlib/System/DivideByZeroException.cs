using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000DD RID: 221
	[ComVisible(true)]
	[Serializable]
	public class DivideByZeroException : ArithmeticException
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x00021444 File Offset: 0x0001F644
		public DivideByZeroException() : base(Locale.GetText("Division by zero"))
		{
			base.HResult = -2147352558;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00021464 File Offset: 0x0001F664
		public DivideByZeroException(string message) : base(message)
		{
			base.HResult = -2147352558;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00021478 File Offset: 0x0001F678
		public DivideByZeroException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147352558;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00021490 File Offset: 0x0001F690
		protected DivideByZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040002EA RID: 746
		private const int Result = -2147352558;
	}
}
