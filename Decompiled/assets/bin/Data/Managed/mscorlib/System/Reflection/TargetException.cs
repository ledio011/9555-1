using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020001F0 RID: 496
	[ComVisible(true)]
	[Serializable]
	public class TargetException : Exception
	{
		// Token: 0x0600124E RID: 4686 RVA: 0x00044E78 File Offset: 0x00043078
		public TargetException() : base(Locale.GetText("Unable to invoke an invalid target."))
		{
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00044E8C File Offset: 0x0004308C
		public TargetException(string message) : base(message)
		{
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00044E98 File Offset: 0x00043098
		protected TargetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
