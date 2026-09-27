using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200014E RID: 334
	[ComVisible(true)]
	[Serializable]
	public class MemberAccessException : SystemException
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x0003207C File Offset: 0x0003027C
		public MemberAccessException() : base(Locale.GetText("Cannot access a class member."))
		{
			base.HResult = -2146233062;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0003209C File Offset: 0x0003029C
		public MemberAccessException(string message) : base(message)
		{
			base.HResult = -2146233062;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000320B0 File Offset: 0x000302B0
		protected MemberAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000559 RID: 1369
		private const int Result = -2146233062;
	}
}
