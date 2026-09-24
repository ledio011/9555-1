using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x02000387 RID: 903
	[ComVisible(true)]
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x000622B4 File Offset: 0x000604B4
		public XmlSyntaxException()
		{
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000622BC File Offset: 0x000604BC
		public XmlSyntaxException(string message) : base(message)
		{
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000622C8 File Offset: 0x000604C8
		public XmlSyntaxException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000622D4 File Offset: 0x000604D4
		internal XmlSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
