using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200005B RID: 91
	[ComVisible(true)]
	[Serializable]
	public class ApplicationException : Exception
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		public ApplicationException() : base(Locale.GetText("An application exception has occurred."))
		{
			base.HResult = -2146232832;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		public ApplicationException(string message) : base(message)
		{
			base.HResult = -2146232832;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000180 RID: 384
		private const int Result = -2146232832;
	}
}
