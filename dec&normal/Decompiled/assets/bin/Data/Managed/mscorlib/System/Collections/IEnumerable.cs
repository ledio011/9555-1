using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020000AB RID: 171
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	[ComVisible(true)]
	public interface IEnumerable
	{
		// Token: 0x060005C5 RID: 1477
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
