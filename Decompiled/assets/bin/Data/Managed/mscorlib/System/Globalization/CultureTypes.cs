using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020000F7 RID: 247
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CultureTypes
	{
		// Token: 0x04000374 RID: 884
		NeutralCultures = 1,
		// Token: 0x04000375 RID: 885
		SpecificCultures = 2,
		// Token: 0x04000376 RID: 886
		InstalledWin32Cultures = 4,
		// Token: 0x04000377 RID: 887
		AllCultures = 7,
		// Token: 0x04000378 RID: 888
		UserCustomCulture = 8,
		// Token: 0x04000379 RID: 889
		ReplacementCultures = 16,
		// Token: 0x0400037A RID: 890
		WindowsOnlyCultures = 32,
		// Token: 0x0400037B RID: 891
		FrameworkCultures = 64
	}
}
