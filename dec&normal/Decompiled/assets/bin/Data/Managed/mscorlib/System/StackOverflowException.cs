using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200038A RID: 906
	[ComVisible(true)]
	[Serializable]
	public sealed class StackOverflowException : SystemException
	{
		// Token: 0x06001A90 RID: 6800 RVA: 0x00062704 File Offset: 0x00060904
		public StackOverflowException() : base(Locale.GetText("The requested operation caused a stack overflow."))
		{
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00062718 File Offset: 0x00060918
		public StackOverflowException(string message) : base(message)
		{
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00062724 File Offset: 0x00060924
		public StackOverflowException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00062730 File Offset: 0x00060930
		internal StackOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
