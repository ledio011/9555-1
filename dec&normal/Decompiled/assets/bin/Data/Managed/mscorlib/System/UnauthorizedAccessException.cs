using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020003D6 RID: 982
	[ComVisible(true)]
	[Serializable]
	public class UnauthorizedAccessException : SystemException
	{
		// Token: 0x06001E84 RID: 7812 RVA: 0x00071E0C File Offset: 0x0007000C
		public UnauthorizedAccessException() : base(Locale.GetText("Access to the requested resource is not authorized."))
		{
			base.HResult = -2146233088;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00071E2C File Offset: 0x0007002C
		public UnauthorizedAccessException(string message) : base(message)
		{
			base.HResult = -2146233088;
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00071E40 File Offset: 0x00070040
		protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000FB0 RID: 4016
		private const int Result = -2146233088;
	}
}
