using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000118 RID: 280
	[ComVisible(true)]
	[Serializable]
	public class InvalidOperationException : SystemException
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x0002A620 File Offset: 0x00028820
		public InvalidOperationException() : base(Locale.GetText("Operation is not valid due to the current state of the object"))
		{
			base.HResult = -2146233079;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002A640 File Offset: 0x00028840
		public InvalidOperationException(string message) : base(message)
		{
			base.HResult = -2146233079;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002A654 File Offset: 0x00028854
		public InvalidOperationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233079;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002A66C File Offset: 0x0002886C
		protected InvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000467 RID: 1127
		private const int Result = -2146233079;
	}
}
