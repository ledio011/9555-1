using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000AC RID: 172
	[Guid("496B0ABF-CDEE-11D3-88E8-00902754C43A")]
	[ComVisible(true)]
	public interface IEnumerator
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005C6 RID: 1478
		object Current { get; }

		// Token: 0x060005C7 RID: 1479
		bool MoveNext();

		// Token: 0x060005C8 RID: 1480
		void Reset();
	}
}
