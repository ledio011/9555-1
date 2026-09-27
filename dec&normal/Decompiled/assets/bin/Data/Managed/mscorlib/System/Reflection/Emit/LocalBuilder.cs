using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001A9 RID: 425
	[ComDefaultInterface(typeof(_LocalBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
		internal LocalBuilder(Type t, ILGenerator ilgen)
		{
			this.type = t;
			this.ilgen = ilgen;
		}

		// Token: 0x040006D2 RID: 1746
		private string name;

		// Token: 0x040006D3 RID: 1747
		internal ILGenerator ilgen;

		// Token: 0x040006D4 RID: 1748
		private int startOffset;

		// Token: 0x040006D5 RID: 1749
		private int endOffset;
	}
}
