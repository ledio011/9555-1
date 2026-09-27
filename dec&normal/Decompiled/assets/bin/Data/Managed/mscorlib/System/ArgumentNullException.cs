using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200005F RID: 95
	[ComVisible(true)]
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public ArgumentNullException() : base(Locale.GetText("Argument cannot be null."))
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000EB34 File Offset: 0x0000CD34
		public ArgumentNullException(string paramName) : base(Locale.GetText("Argument cannot be null."), paramName)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public ArgumentNullException(string paramName, string message) : base(message, paramName)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		protected ArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000189 RID: 393
		private const int Result = -2147467261;
	}
}
