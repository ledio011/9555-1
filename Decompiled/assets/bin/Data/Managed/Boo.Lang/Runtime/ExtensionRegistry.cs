using System;
using System.Collections.Generic;
using System.Reflection;

namespace Boo.Lang.Runtime
{
	// Token: 0x0200000F RID: 15
	public class ExtensionRegistry
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002B20 File Offset: 0x00000D20
		public IEnumerable<MemberInfo> Extensions
		{
			get
			{
				return this._extensions;
			}
		}

		// Token: 0x04000014 RID: 20
		private List<MemberInfo> _extensions = new List<MemberInfo>();

		// Token: 0x04000015 RID: 21
		private object _classLock = new object();
	}
}
