using System;

namespace System.Text
{
	// Token: 0x02000397 RID: 919
	[Serializable]
	public sealed class DecoderReplacementFallback : DecoderFallback
	{
		// Token: 0x06001B89 RID: 7049 RVA: 0x000676DC File Offset: 0x000658DC
		public DecoderReplacementFallback() : this("?")
		{
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000676EC File Offset: 0x000658EC
		[MonoTODO]
		public DecoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException();
			}
			this.replacement = replacement;
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00067708 File Offset: 0x00065908
		public string DefaultString
		{
			get
			{
				return this.replacement;
			}
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00067710 File Offset: 0x00065910
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderReplacementFallbackBuffer(this);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00067718 File Offset: 0x00065918
		public override bool Equals(object value)
		{
			DecoderReplacementFallback decoderReplacementFallback = value as DecoderReplacementFallback;
			return decoderReplacementFallback != null && this.replacement == decoderReplacementFallback.replacement;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00067748 File Offset: 0x00065948
		public override int GetHashCode()
		{
			return this.replacement.GetHashCode();
		}

		// Token: 0x04000EBD RID: 3773
		private string replacement;
	}
}
