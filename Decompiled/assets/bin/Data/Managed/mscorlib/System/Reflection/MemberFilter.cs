using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001CE RID: 462
	// (Invoke) Token: 0x0600113C RID: 4412
	[ComVisible(true)]
	[Serializable]
	public delegate bool MemberFilter(MemberInfo m, object filterCriteria);
}
