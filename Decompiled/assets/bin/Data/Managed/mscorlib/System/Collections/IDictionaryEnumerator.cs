using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000AA RID: 170
	[ComVisible(true)]
	public interface IDictionaryEnumerator : IEnumerator
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005C2 RID: 1474
		DictionaryEntry Entry { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005C3 RID: 1475
		object Key { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005C4 RID: 1476
		object Value { get; }
	}
}
