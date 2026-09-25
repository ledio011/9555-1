using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000134 RID: 308
	[ComVisible(true)]
	[Serializable]
	public class PathTooLongException : IOException
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x0002F304 File Offset: 0x0002D504
		public PathTooLongException() : base(Locale.GetText("Pathname is longer than the maximum length"))
		{
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002F318 File Offset: 0x0002D518
		public PathTooLongException(string message) : base(message)
		{
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002F324 File Offset: 0x0002D524
		protected PathTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
