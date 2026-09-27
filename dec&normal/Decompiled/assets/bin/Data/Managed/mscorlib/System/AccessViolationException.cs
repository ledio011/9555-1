using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000050 RID: 80
	[ComVisible(true)]
	[Serializable]
	public class AccessViolationException : SystemException
	{
		// Token: 0x0600017F RID: 383 RVA: 0x0000CA14 File Offset: 0x0000AC14
		public AccessViolationException() : base(Locale.GetText("Attempted to read or write protected memory. This is often an indication that other memory has been corrupted."))
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000CA34 File Offset: 0x0000AC34
		protected AccessViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000146 RID: 326
		private const int Result = -2147467261;
	}
}
