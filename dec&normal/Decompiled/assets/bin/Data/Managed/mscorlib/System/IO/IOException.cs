using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200012B RID: 299
	[ComVisible(true)]
	[Serializable]
	public class IOException : SystemException
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002D868 File Offset: 0x0002BA68
		public IOException() : base("I/O Error")
		{
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002D878 File Offset: 0x0002BA78
		public IOException(string message) : base(message)
		{
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002D884 File Offset: 0x0002BA84
		protected IOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002D890 File Offset: 0x0002BA90
		public IOException(string message, int hresult) : base(message)
		{
			base.HResult = hresult;
		}
	}
}
