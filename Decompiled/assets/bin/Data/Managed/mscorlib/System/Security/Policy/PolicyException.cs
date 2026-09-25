using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
	// Token: 0x02000367 RID: 871
	[ComVisible(true)]
	[Serializable]
	public class PolicyException : SystemException, _Exception
	{
		// Token: 0x060019C8 RID: 6600 RVA: 0x0005F428 File Offset: 0x0005D628
		public PolicyException() : base(Locale.GetText("Cannot run because of policy."))
		{
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0005F43C File Offset: 0x0005D63C
		public PolicyException(string message) : base(message)
		{
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0005F448 File Offset: 0x0005D648
		protected PolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
