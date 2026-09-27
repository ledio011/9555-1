using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000E9 RID: 233
	[ComVisible(true)]
	[Serializable]
	public class FieldAccessException : MemberAccessException
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x00024170 File Offset: 0x00022370
		public FieldAccessException() : base(Locale.GetText("Attempt to access a private/protected field failed."))
		{
			base.HResult = -2146233081;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00024190 File Offset: 0x00022390
		public FieldAccessException(string message) : base(message)
		{
			base.HResult = -2146233081;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000241A4 File Offset: 0x000223A4
		protected FieldAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000326 RID: 806
		private const int Result = -2146233081;
	}
}
