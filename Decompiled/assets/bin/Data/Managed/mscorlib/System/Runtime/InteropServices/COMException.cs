using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000233 RID: 563
	[ComVisible(true)]
	[Serializable]
	public class COMException : ExternalException
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x000453A8 File Offset: 0x000435A8
		public COMException()
		{
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x000453B0 File Offset: 0x000435B0
		public COMException(string message) : base(message)
		{
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000453BC File Offset: 0x000435BC
		public COMException(string message, int errorCode) : base(message, errorCode)
		{
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000453C8 File Offset: 0x000435C8
		protected COMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000453D4 File Offset: 0x000435D4
		public override string ToString()
		{
			return string.Format("{0} (0x{1:x}): {2} {3}{4}{5}", new object[]
			{
				this.GetType(),
				base.HResult,
				this.Message,
				(this.InnerException != null) ? this.InnerException.ToString() : string.Empty,
				Environment.NewLine,
				(this.StackTrace == null) ? string.Empty : this.StackTrace
			});
		}
	}
}
