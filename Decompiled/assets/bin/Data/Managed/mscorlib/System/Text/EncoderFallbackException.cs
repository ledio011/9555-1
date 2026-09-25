using System;

namespace System.Text
{
	// Token: 0x0200039D RID: 925
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		// Token: 0x06001BA8 RID: 7080 RVA: 0x0006791C File Offset: 0x00065B1C
		public EncoderFallbackException() : this(null)
		{
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00067928 File Offset: 0x00065B28
		public EncoderFallbackException(string message)
		{
			this.index = -1;
			base..ctor(message);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00067938 File Offset: 0x00065B38
		internal EncoderFallbackException(char charUnknown, int index)
		{
			this.index = -1;
			base..ctor(null);
			this.char_unknown = charUnknown;
			this.index = index;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00067958 File Offset: 0x00065B58
		internal EncoderFallbackException(char charUnknownHigh, char charUnknownLow, int index)
		{
			this.index = -1;
			base..ctor(null);
			this.char_unknown_high = charUnknownHigh;
			this.char_unknown_low = charUnknownLow;
			this.index = index;
		}

		// Token: 0x04000EC4 RID: 3780
		private const string defaultMessage = "Failed to decode the input byte sequence to Unicode characters.";

		// Token: 0x04000EC5 RID: 3781
		private char char_unknown;

		// Token: 0x04000EC6 RID: 3782
		private char char_unknown_high;

		// Token: 0x04000EC7 RID: 3783
		private char char_unknown_low;

		// Token: 0x04000EC8 RID: 3784
		private int index;
	}
}
