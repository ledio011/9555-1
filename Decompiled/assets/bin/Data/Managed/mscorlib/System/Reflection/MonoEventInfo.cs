using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020001DB RID: 475
	internal struct MonoEventInfo
	{
		// Token: 0x060011A2 RID: 4514
		[MethodImpl(4096)]
		private static extern void get_event_info(MonoEvent ev, out MonoEventInfo info);

		// Token: 0x060011A3 RID: 4515 RVA: 0x00042DD4 File Offset: 0x00040FD4
		internal static MonoEventInfo GetEventInfo(MonoEvent ev)
		{
			MonoEventInfo result;
			MonoEventInfo.get_event_info(ev, out result);
			return result;
		}

		// Token: 0x04000904 RID: 2308
		public Type declaring_type;

		// Token: 0x04000905 RID: 2309
		public Type reflected_type;

		// Token: 0x04000906 RID: 2310
		public string name;

		// Token: 0x04000907 RID: 2311
		public MethodInfo add_method;

		// Token: 0x04000908 RID: 2312
		public MethodInfo remove_method;

		// Token: 0x04000909 RID: 2313
		public MethodInfo raise_method;

		// Token: 0x0400090A RID: 2314
		public EventAttributes attrs;

		// Token: 0x0400090B RID: 2315
		public MethodInfo[] other_methods;
	}
}
