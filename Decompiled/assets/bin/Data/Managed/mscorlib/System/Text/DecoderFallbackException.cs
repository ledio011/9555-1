using System;

namespace System.Text
{
	// Token: 0x02000396 RID: 918
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		// Token: 0x06001B86 RID: 7046 RVA: 0x000676A0 File Offset: 0x000658A0
		public DecoderFallbackException() : this(null)
		{
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000676AC File Offset: 0x000658AC
		public DecoderFallbackException(string message)
		{
			this.index = -1;
			base..ctor(message);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x000676BC File Offset: 0x000658BC
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
		{
			this.index = -1;
			base..ctor(message);
			this.bytes_unknown = bytesUnknown;
			this.index = index;
		}

		// Token: 0x04000EBA RID: 3770
		private const string defaultMessage = "Failed to decode the input byte sequence to Unicode characters.";

		// Token: 0x04000EBB RID: 3771
		private byte[] bytes_unknown;

		// Token: 0x04000EBC RID: 3772
		private int index;
	}
}
