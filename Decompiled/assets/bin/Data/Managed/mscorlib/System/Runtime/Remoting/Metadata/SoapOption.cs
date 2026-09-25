using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020002CA RID: 714
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum SoapOption
	{
		// Token: 0x04000B7F RID: 2943
		None = 0,
		// Token: 0x04000B80 RID: 2944
		AlwaysIncludeTypes = 1,
		// Token: 0x04000B81 RID: 2945
		XsdString = 2,
		// Token: 0x04000B82 RID: 2946
		EmbedAll = 4,
		// Token: 0x04000B83 RID: 2947
		Option1 = 8,
		// Token: 0x04000B84 RID: 2948
		Option2 = 16
	}
}
