using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200005A RID: 90
	[ComVisible(true)]
	[Serializable]
	public class AppDomainUnloadedException : SystemException
	{
		// Token: 0x06000251 RID: 593 RVA: 0x0000E860 File Offset: 0x0000CA60
		public AppDomainUnloadedException() : base(Locale.GetText("Can't access an unloaded application domain."))
		{
			base.HResult = -2146234348;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000E880 File Offset: 0x0000CA80
		public AppDomainUnloadedException(string message) : base(message)
		{
			base.HResult = -2146234348;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000E894 File Offset: 0x0000CA94
		public AppDomainUnloadedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234348;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000E8AC File Offset: 0x0000CAAC
		protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400017F RID: 383
		private const int Result = -2146234348;
	}
}
