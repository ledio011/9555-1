using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020001F1 RID: 497
	[ComVisible(true)]
	[Serializable]
	public sealed class TargetInvocationException : Exception
	{
		// Token: 0x06001251 RID: 4689 RVA: 0x00044EA4 File Offset: 0x000430A4
		public TargetInvocationException(Exception inner) : base("Exception has been thrown by the target of an invocation.", inner)
		{
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00044EB4 File Offset: 0x000430B4
		public TargetInvocationException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00044EC0 File Offset: 0x000430C0
		internal TargetInvocationException(SerializationInfo info, StreamingContext sc) : base(info, sc)
		{
		}
	}
}
