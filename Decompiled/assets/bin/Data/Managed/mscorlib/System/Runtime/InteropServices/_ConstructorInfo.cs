using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000216 RID: 534
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ConstructorInfo))]
	[ComVisible(true)]
	[Guid("E9A19478-9646-3679-9B10-8411AE1FD57D")]
	public interface _ConstructorInfo
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060012BE RID: 4798
		MemberTypes MemberType { get; }
	}
}
