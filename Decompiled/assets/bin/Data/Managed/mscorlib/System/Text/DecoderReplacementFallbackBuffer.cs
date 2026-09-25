using System;

namespace System.Text
{
	// Token: 0x02000398 RID: 920
	public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x06001B8F RID: 7055 RVA: 0x00067758 File Offset: 0x00065958
		public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
		{
			if (fallback == null)
			{
				throw new ArgumentNullException("fallback");
			}
			this.replacement = fallback.DefaultString;
			this.current = 0;
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x00067784 File Offset: 0x00065984
		public override int Remaining
		{
			get
			{
				return (!this.fallback_assigned) ? 0 : (this.replacement.Length - this.current);
			}
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000677AC File Offset: 0x000659AC
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			if (bytesUnknown == null)
			{
				throw new ArgumentNullException("bytesUnknown");
			}
			if (this.fallback_assigned && this.Remaining != 0)
			{
				throw new ArgumentException("Reentrant Fallback method invocation occured. It might be because either this FallbackBuffer is incorrectly shared by multiple threads, invoked inside Encoding recursively, or Reset invocation is forgotten.");
			}
			if (index < 0 || bytesUnknown.Length < index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.fallback_assigned = true;
			this.current = 0;
			return this.replacement.Length > 0;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00067824 File Offset: 0x00065A24
		public override char GetNextChar()
		{
			if (!this.fallback_assigned)
			{
				return '\0';
			}
			if (this.current >= this.replacement.Length)
			{
				return '\0';
			}
			return this.replacement[this.current++];
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00067874 File Offset: 0x00065A74
		public override void Reset()
		{
			this.fallback_assigned = false;
			this.current = 0;
		}

		// Token: 0x04000EBE RID: 3774
		private bool fallback_assigned;

		// Token: 0x04000EBF RID: 3775
		private int current;

		// Token: 0x04000EC0 RID: 3776
		private string replacement;
	}
}
