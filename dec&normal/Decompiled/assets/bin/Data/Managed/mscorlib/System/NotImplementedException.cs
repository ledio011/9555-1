using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000166 RID: 358
	[ComVisible(true)]
	[Serializable]
	public class NotImplementedException : SystemException
	{
		// Token: 0x06000D73 RID: 3443 RVA: 0x00033F04 File Offset: 0x00032104
		public NotImplementedException() : base(Locale.GetText("The requested feature is not implemented."))
		{
			base.HResult = -2147467263;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00033F24 File Offset: 0x00032124
		public NotImplementedException(string message) : base(message)
		{
			base.HResult = -2147467263;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00033F38 File Offset: 0x00032138
		public NotImplementedException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467263;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00033F50 File Offset: 0x00032150
		protected NotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000581 RID: 1409
		private const int Result = -2147467263;
	}
}
