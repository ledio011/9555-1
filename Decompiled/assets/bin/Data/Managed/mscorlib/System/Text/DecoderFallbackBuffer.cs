using System;

namespace System.Text
{
	// Token: 0x02000395 RID: 917
	public abstract class DecoderFallbackBuffer
	{
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001B82 RID: 7042
		public abstract int Remaining { get; }

		// Token: 0x06001B83 RID: 7043
		public abstract bool Fallback(byte[] bytesUnknown, int index);

		// Token: 0x06001B84 RID: 7044
		public abstract char GetNextChar();

		// Token: 0x06001B85 RID: 7045 RVA: 0x0006769C File Offset: 0x0006589C
		public virtual void Reset()
		{
		}
	}
}
