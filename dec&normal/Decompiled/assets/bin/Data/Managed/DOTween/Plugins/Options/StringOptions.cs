using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000037 RID: 55
	public struct StringOptions : IPlugOptions
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000580C File Offset: 0x00003A0C
		public void Reset()
		{
			this.richTextEnabled = false;
			this.scrambleMode = ScrambleMode.None;
			this.scrambledChars = null;
			this.startValueStrippedLength = (this.changeValueStrippedLength = 0);
		}

		// Token: 0x040000F3 RID: 243
		public bool richTextEnabled;

		// Token: 0x040000F4 RID: 244
		public ScrambleMode scrambleMode;

		// Token: 0x040000F5 RID: 245
		public char[] scrambledChars;

		// Token: 0x040000F6 RID: 246
		internal int startValueStrippedLength;

		// Token: 0x040000F7 RID: 247
		internal int changeValueStrippedLength;
	}
}
