using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000177 RID: 375
	[ComVisible(true)]
	[Serializable]
	public class RankException : SystemException
	{
		// Token: 0x06000E1C RID: 3612 RVA: 0x000381D0 File Offset: 0x000363D0
		public RankException() : base(Locale.GetText("Two arrays must have the same number of dimensions."))
		{
			base.HResult = -2146233065;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000381F0 File Offset: 0x000363F0
		public RankException(string message) : base(message)
		{
			base.HResult = -2146233065;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00038204 File Offset: 0x00036404
		protected RankException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040005DD RID: 1501
		private const int Result = -2146233065;
	}
}
