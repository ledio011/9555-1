using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000245 RID: 581
	[ComVisible(true)]
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x00045600 File Offset: 0x00043800
		public InvalidComObjectException() : base(Locale.GetText("Invalid COM object is used"))
		{
			base.HResult = -2146233049;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00045620 File Offset: 0x00043820
		public InvalidComObjectException(string message) : base(message)
		{
			base.HResult = -2146233049;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00045634 File Offset: 0x00043834
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000A05 RID: 2565
		private const int ErrorCode = -2146233049;
	}
}
