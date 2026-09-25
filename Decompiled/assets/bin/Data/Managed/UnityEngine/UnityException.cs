using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	public class UnityException : SystemException
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x00019FEC File Offset: 0x000181EC
		public UnityException() : base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001A004 File Offset: 0x00018204
		public UnityException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0001A018 File Offset: 0x00018218
		public UnityException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0001A030 File Offset: 0x00018230
		protected UnityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040004E2 RID: 1250
		private const int Result = -2147467261;

		// Token: 0x040004E3 RID: 1251
		private string unityStackTrace;
	}
}
