using System;

namespace System.Text
{
	// Token: 0x0200039C RID: 924
	public abstract class EncoderFallbackBuffer
	{
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001BA4 RID: 7076
		public abstract int Remaining { get; }

		// Token: 0x06001BA5 RID: 7077
		public abstract bool Fallback(char charUnknown, int index);

		// Token: 0x06001BA6 RID: 7078
		public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

		// Token: 0x06001BA7 RID: 7079
		public abstract char GetNextChar();
	}
}
