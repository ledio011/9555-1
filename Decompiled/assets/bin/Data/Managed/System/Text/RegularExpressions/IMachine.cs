using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000068 RID: 104
	internal interface IMachine
	{
		// Token: 0x060001D5 RID: 469
		Match Scan(Regex regex, string text, int start, int end);

		// Token: 0x060001D6 RID: 470
		string Replace(Regex regex, string input, string replacement, int count, int startat);
	}
}
