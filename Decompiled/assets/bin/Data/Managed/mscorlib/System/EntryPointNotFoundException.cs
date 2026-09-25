using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000E0 RID: 224
	[ComVisible(true)]
	[Serializable]
	public class EntryPointNotFoundException : TypeLoadException
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x00021F34 File Offset: 0x00020134
		public EntryPointNotFoundException() : base(Locale.GetText("Cannot load class because of missing entry method."))
		{
			base.HResult = -2146233053;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00021F54 File Offset: 0x00020154
		public EntryPointNotFoundException(string message) : base(message)
		{
			base.HResult = -2146233053;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00021F68 File Offset: 0x00020168
		protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00021F74 File Offset: 0x00020174
		public EntryPointNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233053;
		}

		// Token: 0x040002FA RID: 762
		private const int Result = -2146233053;
	}
}
