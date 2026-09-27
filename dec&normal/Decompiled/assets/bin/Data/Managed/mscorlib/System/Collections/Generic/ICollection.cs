using System;

namespace System.Collections.Generic
{
	// Token: 0x02000093 RID: 147
	public interface ICollection<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000505 RID: 1285
		int Count { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000506 RID: 1286
		bool IsReadOnly { get; }

		// Token: 0x06000507 RID: 1287
		void Add(T item);

		// Token: 0x06000508 RID: 1288
		void Clear();

		// Token: 0x06000509 RID: 1289
		bool Contains(T item);

		// Token: 0x0600050A RID: 1290
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x0600050B RID: 1291
		bool Remove(T item);
	}
}
