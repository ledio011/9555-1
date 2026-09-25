using System;

namespace System.Text
{
	// Token: 0x0200039F RID: 927
	public sealed class EncoderReplacementFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x06001BB2 RID: 7090 RVA: 0x000679FC File Offset: 0x00065BFC
		public EncoderReplacementFallbackBuffer(EncoderReplacementFallback fallback)
		{
			if (fallback == null)
			{
				throw new ArgumentNullException("fallback");
			}
			this.replacement = fallback.DefaultString;
			this.current = 0;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x00067A28 File Offset: 0x00065C28
		public override int Remaining
		{
			get
			{
				return this.replacement.Length - this.current;
			}
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00067A3C File Offset: 0x00065C3C
		public override bool Fallback(char charUnknown, int index)
		{
			return this.Fallback(index);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00067A48 File Offset: 0x00065C48
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			return this.Fallback(index);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00067A54 File Offset: 0x00065C54
		private bool Fallback(int index)
		{
			if (this.fallback_assigned && this.Remaining != 0)
			{
				throw new ArgumentException("Reentrant Fallback method invocation occured. It might be because either this FallbackBuffer is incorrectly shared by multiple threads, invoked inside Encoding recursively, or Reset invocation is forgotten.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.fallback_assigned = true;
			this.current = 0;
			return this.replacement.Length > 0;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00067AB0 File Offset: 0x00065CB0
		public override char GetNextChar()
		{
			if (this.current >= this.replacement.Length)
			{
				return '\0';
			}
			return this.replacement[this.current++];
		}

		// Token: 0x04000ECA RID: 3786
		private string replacement;

		// Token: 0x04000ECB RID: 3787
		private int current;

		// Token: 0x04000ECC RID: 3788
		private bool fallback_assigned;
	}
}
