using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200023E RID: 574
	[ComVisible(true)]
	[Serializable]
	public class ExternalException : SystemException
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x000454AC File Offset: 0x000436AC
		public ExternalException() : base(Locale.GetText("External exception"))
		{
			base.HResult = -2147467259;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000454CC File Offset: 0x000436CC
		public ExternalException(string message) : base(message)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000454E0 File Offset: 0x000436E0
		protected ExternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000454EC File Offset: 0x000436EC
		public ExternalException(string message, int errorCode) : base(message)
		{
			base.HResult = errorCode;
		}
	}
}
