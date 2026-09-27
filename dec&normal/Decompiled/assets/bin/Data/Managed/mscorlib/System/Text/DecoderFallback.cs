using System;

namespace System.Text
{
	// Token: 0x02000394 RID: 916
	[Serializable]
	public abstract class DecoderFallback
	{
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0006767C File Offset: 0x0006587C
		public static DecoderFallback ExceptionFallback
		{
			get
			{
				return DecoderFallback.exception_fallback;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00067684 File Offset: 0x00065884
		public static DecoderFallback ReplacementFallback
		{
			get
			{
				return DecoderFallback.replacement_fallback;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x0006768C File Offset: 0x0006588C
		internal static DecoderFallback StandardSafeFallback
		{
			get
			{
				return DecoderFallback.standard_safe_fallback;
			}
		}

		// Token: 0x06001B80 RID: 7040
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x04000EB7 RID: 3767
		private static DecoderFallback exception_fallback = new DecoderExceptionFallback();

		// Token: 0x04000EB8 RID: 3768
		private static DecoderFallback replacement_fallback = new DecoderReplacementFallback();

		// Token: 0x04000EB9 RID: 3769
		private static DecoderFallback standard_safe_fallback = new DecoderReplacementFallback("�");
	}
}
