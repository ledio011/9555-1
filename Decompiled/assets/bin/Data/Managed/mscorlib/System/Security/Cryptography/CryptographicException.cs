using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	// Token: 0x02000324 RID: 804
	[ComVisible(true)]
	[Serializable]
	public class CryptographicException : SystemException, _Exception
	{
		// Token: 0x06001876 RID: 6262 RVA: 0x00059204 File Offset: 0x00057404
		public CryptographicException() : base(Locale.GetText("Error occured during a cryptographic operation."))
		{
			base.HResult = -2146233296;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00059224 File Offset: 0x00057424
		public CryptographicException(string message) : base(message)
		{
			base.HResult = -2146233296;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00059238 File Offset: 0x00057438
		public CryptographicException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233296;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00059250 File Offset: 0x00057450
		protected CryptographicException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
