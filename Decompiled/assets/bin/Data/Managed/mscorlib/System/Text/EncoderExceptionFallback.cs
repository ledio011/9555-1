using System;

namespace System.Text
{
	// Token: 0x02000399 RID: 921
	[Serializable]
	public sealed class EncoderExceptionFallback : EncoderFallback
	{
		// Token: 0x06001B95 RID: 7061 RVA: 0x0006788C File Offset: 0x00065A8C
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderExceptionFallbackBuffer();
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00067894 File Offset: 0x00065A94
		public override bool Equals(object value)
		{
			return value is EncoderExceptionFallback;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000678A0 File Offset: 0x00065AA0
		public override int GetHashCode()
		{
			return 0;
		}
	}
}
