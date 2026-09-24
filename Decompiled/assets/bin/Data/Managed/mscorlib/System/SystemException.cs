using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200038F RID: 911
	[ComVisible(true)]
	[Serializable]
	public class SystemException : Exception
	{
		// Token: 0x06001B5B RID: 7003 RVA: 0x00066D10 File Offset: 0x00064F10
		public SystemException() : base(Locale.GetText("A system exception has occurred."))
		{
			base.HResult = -2146233087;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00066D30 File Offset: 0x00064F30
		public SystemException(string message) : base(message)
		{
			base.HResult = -2146233087;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00066D44 File Offset: 0x00064F44
		protected SystemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00066D50 File Offset: 0x00064F50
		public SystemException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233087;
		}

		// Token: 0x04000EB3 RID: 3763
		private const int Result = -2146233087;
	}
}
