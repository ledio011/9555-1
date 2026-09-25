using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000314 RID: 788
	[ComVisible(true)]
	[Serializable]
	public class SerializationException : SystemException
	{
		// Token: 0x06001809 RID: 6153 RVA: 0x00057A74 File Offset: 0x00055C74
		public SerializationException() : base("An error occurred during (de)serialization")
		{
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00057A84 File Offset: 0x00055C84
		public SerializationException(string message) : base(message)
		{
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00057A90 File Offset: 0x00055C90
		protected SerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
