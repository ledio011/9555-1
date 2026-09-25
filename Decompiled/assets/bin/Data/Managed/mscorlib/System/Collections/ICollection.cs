using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000A7 RID: 167
	[ComVisible(true)]
	public interface ICollection : IEnumerable
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060005B2 RID: 1458
		int Count { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005B3 RID: 1459
		bool IsSynchronized { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005B4 RID: 1460
		object SyncRoot { get; }

		// Token: 0x060005B5 RID: 1461
		void CopyTo(Array array, int index);
	}
}
