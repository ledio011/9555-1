using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200014F RID: 335
	[ComVisible(true)]
	[Serializable]
	public class MethodAccessException : MemberAccessException
	{
		// Token: 0x06000CF1 RID: 3313 RVA: 0x000320BC File Offset: 0x000302BC
		public MethodAccessException() : base(Locale.GetText("Attempt to access a private/protected method failed."))
		{
			base.HResult = -2146233072;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000320DC File Offset: 0x000302DC
		protected MethodAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400055A RID: 1370
		private const int Result = -2146233072;
	}
}
