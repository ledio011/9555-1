using System;

namespace System.Text
{
	// Token: 0x02000393 RID: 915
	public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x00067638 File Offset: 0x00065838
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0006763C File Offset: 0x0006583C
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			throw new DecoderFallbackException(null, bytesUnknown, index);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00067648 File Offset: 0x00065848
		public override char GetNextChar()
		{
			return '\0';
		}
	}
}
