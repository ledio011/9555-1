using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000A9 RID: 169
	[ComVisible(true)]
	public interface IDictionary : ICollection, IEnumerable
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005B7 RID: 1463
		bool IsFixedSize { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005B8 RID: 1464
		bool IsReadOnly { get; }

		// Token: 0x170000DD RID: 221
		object this[object key]
		{
			get;
			set;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005BB RID: 1467
		ICollection Keys { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005BC RID: 1468
		ICollection Values { get; }

		// Token: 0x060005BD RID: 1469
		void Add(object key, object value);

		// Token: 0x060005BE RID: 1470
		void Clear();

		// Token: 0x060005BF RID: 1471
		bool Contains(object key);

		// Token: 0x060005C0 RID: 1472
		IDictionaryEnumerator GetEnumerator();

		// Token: 0x060005C1 RID: 1473
		void Remove(object key);
	}
}
