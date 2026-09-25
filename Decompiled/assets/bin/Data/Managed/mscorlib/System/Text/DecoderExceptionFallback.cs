using System;

namespace System.Text
{
	// Token: 0x02000392 RID: 914
	[Serializable]
	public sealed class DecoderExceptionFallback : DecoderFallback
	{
		// Token: 0x06001B74 RID: 7028 RVA: 0x00067618 File Offset: 0x00065818
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderExceptionFallbackBuffer();
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00067620 File Offset: 0x00065820
		public override bool Equals(object value)
		{
			return value is DecoderExceptionFallback;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0006762C File Offset: 0x0006582C
		public override int GetHashCode()
		{
			return 0;
		}
	}
}
