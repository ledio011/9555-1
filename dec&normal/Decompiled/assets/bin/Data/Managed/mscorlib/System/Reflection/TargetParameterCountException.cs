using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020001F2 RID: 498
	[ComVisible(true)]
	[Serializable]
	public sealed class TargetParameterCountException : Exception
	{
		// Token: 0x06001254 RID: 4692 RVA: 0x00044ECC File Offset: 0x000430CC
		public TargetParameterCountException() : base(Locale.GetText("Number of parameter does not match expected count."))
		{
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00044EE0 File Offset: 0x000430E0
		public TargetParameterCountException(string message) : base(message)
		{
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00044EEC File Offset: 0x000430EC
		internal TargetParameterCountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
