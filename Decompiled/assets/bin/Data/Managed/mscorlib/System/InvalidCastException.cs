using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000117 RID: 279
	[ComVisible(true)]
	[Serializable]
	public class InvalidCastException : SystemException
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x0002A5E0 File Offset: 0x000287E0
		public InvalidCastException() : base(Locale.GetText("Cannot cast from source type to destination type."))
		{
			base.HResult = -2147467262;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002A600 File Offset: 0x00028800
		public InvalidCastException(string message) : base(message)
		{
			base.HResult = -2147467262;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002A614 File Offset: 0x00028814
		protected InvalidCastException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000466 RID: 1126
		private const int Result = -2147467262;
	}
}
