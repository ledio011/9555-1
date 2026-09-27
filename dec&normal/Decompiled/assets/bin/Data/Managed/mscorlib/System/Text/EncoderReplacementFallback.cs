using System;

namespace System.Text
{
	// Token: 0x0200039E RID: 926
	[Serializable]
	public sealed class EncoderReplacementFallback : EncoderFallback
	{
		// Token: 0x06001BAC RID: 7084 RVA: 0x00067980 File Offset: 0x00065B80
		public EncoderReplacementFallback() : this("?")
		{
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00067990 File Offset: 0x00065B90
		[MonoTODO]
		public EncoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException();
			}
			this.replacement = replacement;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x000679AC File Offset: 0x00065BAC
		public string DefaultString
		{
			get
			{
				return this.replacement;
			}
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000679B4 File Offset: 0x00065BB4
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderReplacementFallbackBuffer(this);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x000679BC File Offset: 0x00065BBC
		public override bool Equals(object value)
		{
			EncoderReplacementFallback encoderReplacementFallback = value as EncoderReplacementFallback;
			return encoderReplacementFallback != null && this.replacement == encoderReplacementFallback.replacement;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000679EC File Offset: 0x00065BEC
		public override int GetHashCode()
		{
			return this.replacement.GetHashCode();
		}

		// Token: 0x04000EC9 RID: 3785
		private string replacement;
	}
}
