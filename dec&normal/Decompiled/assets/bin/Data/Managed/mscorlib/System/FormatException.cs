using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000EB RID: 235
	[ComVisible(true)]
	[Serializable]
	public class FormatException : SystemException
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x000241B8 File Offset: 0x000223B8
		public FormatException() : base(Locale.GetText("Invalid format."))
		{
			base.HResult = -2146233033;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000241D8 File Offset: 0x000223D8
		public FormatException(string message) : base(message)
		{
			base.HResult = -2146233033;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000241EC File Offset: 0x000223EC
		protected FormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000327 RID: 807
		private const int Result = -2146233033;
	}
}
