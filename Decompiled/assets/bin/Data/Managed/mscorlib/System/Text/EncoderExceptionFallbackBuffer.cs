using System;

namespace System.Text
{
	// Token: 0x0200039A RID: 922
	public sealed class EncoderExceptionFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000678AC File Offset: 0x00065AAC
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000678B0 File Offset: 0x00065AB0
		public override bool Fallback(char charUnknown, int index)
		{
			throw new EncoderFallbackException(charUnknown, index);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000678BC File Offset: 0x00065ABC
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			throw new EncoderFallbackException(charUnknownHigh, charUnknownLow, index);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000678C8 File Offset: 0x00065AC8
		public override char GetNextChar()
		{
			return '\0';
		}
	}
}
