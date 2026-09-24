using System;

namespace System.Collections.Generic
{
	// Token: 0x02000095 RID: 149
	public interface IDictionary<TKey, TValue> : ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x0600050D RID: 1293
		void Add(TKey key, TValue value);

		// Token: 0x0600050E RID: 1294
		bool ContainsKey(TKey key);

		// Token: 0x0600050F RID: 1295
		bool Remove(TKey key);

		// Token: 0x06000510 RID: 1296
		bool TryGetValue(TKey key, out TValue value);

		// Token: 0x170000AC RID: 172
		TValue this[TKey key]
		{
			get;
			set;
		}
	}
}
