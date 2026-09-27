using System;

namespace System.Text
{
	// Token: 0x0200039B RID: 923
	[Serializable]
	public abstract class EncoderFallback
	{
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x000678FC File Offset: 0x00065AFC
		public static EncoderFallback ExceptionFallback
		{
			get
			{
				return EncoderFallback.exception_fallback;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00067904 File Offset: 0x00065B04
		public static EncoderFallback ReplacementFallback
		{
			get
			{
				return EncoderFallback.replacement_fallback;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0006790C File Offset: 0x00065B0C
		internal static EncoderFallback StandardSafeFallback
		{
			get
			{
				return EncoderFallback.standard_safe_fallback;
			}
		}

		// Token: 0x06001BA2 RID: 7074
		public abstract EncoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x04000EC1 RID: 3777
		private static EncoderFallback exception_fallback = new EncoderExceptionFallback();

		// Token: 0x04000EC2 RID: 3778
		private static EncoderFallback replacement_fallback = new EncoderReplacementFallback();

		// Token: 0x04000EC3 RID: 3779
		private static EncoderFallback standard_safe_fallback = new EncoderReplacementFallback("�");
	}
}
