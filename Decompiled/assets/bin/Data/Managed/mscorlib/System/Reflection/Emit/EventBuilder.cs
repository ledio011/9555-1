using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200019D RID: 413
	[ComDefaultInterface(typeof(_EventBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public sealed class EventBuilder : _EventBuilder
	{
		// Token: 0x04000683 RID: 1667
		internal string name;

		// Token: 0x04000684 RID: 1668
		private Type type;

		// Token: 0x04000685 RID: 1669
		private TypeBuilder typeb;

		// Token: 0x04000686 RID: 1670
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000687 RID: 1671
		internal MethodBuilder add_method;

		// Token: 0x04000688 RID: 1672
		internal MethodBuilder remove_method;

		// Token: 0x04000689 RID: 1673
		internal MethodBuilder raise_method;

		// Token: 0x0400068A RID: 1674
		internal MethodBuilder[] other_methods;

		// Token: 0x0400068B RID: 1675
		internal EventAttributes attrs;

		// Token: 0x0400068C RID: 1676
		private int table_idx;
	}
}
