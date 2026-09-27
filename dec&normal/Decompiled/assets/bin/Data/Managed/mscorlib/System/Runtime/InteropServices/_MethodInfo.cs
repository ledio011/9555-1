using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000223 RID: 547
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[Guid("FFCC1B5D-ECB8-38DD-9B01-3DC8ABC2AA5F")]
	[ComVisible(true)]
	[TypeLibImportClass(typeof(MethodInfo))]
	public interface _MethodInfo
	{
		// Token: 0x060012E4 RID: 4836
		MethodInfo GetBaseDefinition();

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060012E5 RID: 4837
		MemberTypes MemberType { get; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060012E6 RID: 4838
		Type ReturnType { get; }
	}
}
