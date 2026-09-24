using System;
using System.Collections;
using System.Collections.Generic;

namespace Boo.Lang
{
	// Token: 0x02000003 RID: 3
	public abstract class GenericGenerator<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000004 RID: 4
		public abstract IEnumerator<T> GetEnumerator();

		// Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
		public override string ToString()
		{
			return string.Format("generator({0})", typeof(T));
		}
	}
}
