using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000AF RID: 175
	[ComVisible(true)]
	public interface IList : ICollection, IEnumerable
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005CC RID: 1484
		bool IsFixedSize { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005CD RID: 1485
		bool IsReadOnly { get; }

		// Token: 0x170000E6 RID: 230
		object this[int index]
		{
			get;
			set;
		}

		// Token: 0x060005D0 RID: 1488
		int Add(object value);

		// Token: 0x060005D1 RID: 1489
		void Clear();

		// Token: 0x060005D2 RID: 1490
		bool Contains(object value);

		// Token: 0x060005D3 RID: 1491
		int IndexOf(object value);

		// Token: 0x060005D4 RID: 1492
		void Insert(int index, object value);

		// Token: 0x060005D5 RID: 1493
		void Remove(object value);

		// Token: 0x060005D6 RID: 1494
		void RemoveAt(int index);
	}
}
