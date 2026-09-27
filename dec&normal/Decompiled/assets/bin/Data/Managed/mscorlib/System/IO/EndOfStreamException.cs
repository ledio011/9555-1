using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200011E RID: 286
	[ComVisible(true)]
	[Serializable]
	public class EndOfStreamException : IOException
	{
		// Token: 0x06000B65 RID: 2917 RVA: 0x0002BCF4 File Offset: 0x00029EF4
		public EndOfStreamException() : base(Locale.GetText("Failed to read past end of stream."))
		{
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002BD08 File Offset: 0x00029F08
		protected EndOfStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
