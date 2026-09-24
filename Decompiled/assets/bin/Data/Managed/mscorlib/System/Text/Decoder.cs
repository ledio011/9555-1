using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000391 RID: 913
	[ComVisible(true)]
	[Serializable]
	public abstract class Decoder
	{
		// Token: 0x170004C8 RID: 1224
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x000675D0 File Offset: 0x000657D0
		[ComVisible(false)]
		public DecoderFallback Fallback
		{
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.fallback = value;
				this.fallback_buffer = null;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x000675EC File Offset: 0x000657EC
		[ComVisible(false)]
		public DecoderFallbackBuffer FallbackBuffer
		{
			get
			{
				if (this.fallback_buffer == null)
				{
					this.fallback_buffer = this.fallback.CreateFallbackBuffer();
				}
				return this.fallback_buffer;
			}
		}

		// Token: 0x06001B72 RID: 7026
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		// Token: 0x04000EB5 RID: 3765
		private DecoderFallback fallback = new DecoderReplacementFallback();

		// Token: 0x04000EB6 RID: 3766
		private DecoderFallbackBuffer fallback_buffer;
	}
}
