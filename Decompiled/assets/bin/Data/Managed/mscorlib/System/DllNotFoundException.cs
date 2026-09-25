using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000DE RID: 222
	[ComVisible(true)]
	[Serializable]
	public class DllNotFoundException : TypeLoadException
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x0002149C File Offset: 0x0001F69C
		public DllNotFoundException() : base(Locale.GetText("DLL not found."))
		{
			base.HResult = -2146233052;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000214BC File Offset: 0x0001F6BC
		public DllNotFoundException(string message) : base(message)
		{
			base.HResult = -2146233052;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000214D0 File Offset: 0x0001F6D0
		protected DllNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000214DC File Offset: 0x0001F6DC
		public DllNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233052;
		}

		// Token: 0x040002EB RID: 747
		private const int Result = -2146233052;
	}
}
