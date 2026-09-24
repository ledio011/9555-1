using System;

namespace System.Text
{
	// Token: 0x020003A2 RID: 930
	[Serializable]
	public sealed class EncodingInfo
	{
		// Token: 0x06001BE9 RID: 7145 RVA: 0x00068CCC File Offset: 0x00066ECC
		public override bool Equals(object value)
		{
			EncodingInfo encodingInfo = value as EncodingInfo;
			return encodingInfo != null && encodingInfo.codepage == this.codepage;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00068CF8 File Offset: 0x00066EF8
		public override int GetHashCode()
		{
			return this.codepage;
		}

		// Token: 0x04000EEB RID: 3819
		private readonly int codepage;

		// Token: 0x04000EEC RID: 3820
		private Encoding encoding;
	}
}
