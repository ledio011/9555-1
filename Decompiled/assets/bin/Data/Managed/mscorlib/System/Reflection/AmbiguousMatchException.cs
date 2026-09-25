using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000178 RID: 376
	[ComVisible(true)]
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x00038210 File Offset: 0x00036410
		public AmbiguousMatchException() : base("Ambiguous matching in method resolution")
		{
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00038220 File Offset: 0x00036420
		public AmbiguousMatchException(string message) : base(message)
		{
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003822C File Offset: 0x0003642C
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
