using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000E8 RID: 232
	[ComVisible(true)]
	[Serializable]
	public sealed class ExecutionEngineException : SystemException
	{
		// Token: 0x0600093C RID: 2364 RVA: 0x00024144 File Offset: 0x00022344
		public ExecutionEngineException() : base(Locale.GetText("Internal error occurred."))
		{
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00024158 File Offset: 0x00022358
		public ExecutionEngineException(string message) : base(message)
		{
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00024164 File Offset: 0x00022364
		internal ExecutionEngineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
