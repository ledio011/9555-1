using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020001E4 RID: 484
	internal struct MonoPropertyInfo
	{
		// Token: 0x06001229 RID: 4649
		[MethodImpl(4096)]
		internal static extern void get_property_info(MonoProperty prop, ref MonoPropertyInfo info, PInfo req_info);

		// Token: 0x04000926 RID: 2342
		public Type parent;

		// Token: 0x04000927 RID: 2343
		public string name;

		// Token: 0x04000928 RID: 2344
		public MethodInfo get_method;

		// Token: 0x04000929 RID: 2345
		public MethodInfo set_method;

		// Token: 0x0400092A RID: 2346
		public PropertyAttributes attrs;
	}
}
