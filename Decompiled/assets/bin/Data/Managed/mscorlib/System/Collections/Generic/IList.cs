using System;

namespace System.Collections.Generic
{
	// Token: 0x02000099 RID: 153
	public interface IList<T> : ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000517 RID: 1303
		int IndexOf(T item);

		// Token: 0x06000518 RID: 1304
		void Insert(int index, T item);

		// Token: 0x06000519 RID: 1305
		void RemoveAt(int index);

		// Token: 0x170000AE RID: 174
		T this[int index]
		{
			get;
			set;
		}
	}
}
