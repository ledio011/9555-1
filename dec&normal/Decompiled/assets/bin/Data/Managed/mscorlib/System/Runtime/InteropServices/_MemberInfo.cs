using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000220 RID: 544
	[TypeLibImportClass(typeof(MemberInfo))]
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("f7102fa9-cabb-3a74-a6da-b4567ef1b079")]
	public interface _MemberInfo
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060012D5 RID: 4821
		Type DeclaringType { get; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060012D6 RID: 4822
		MemberTypes MemberType { get; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060012D7 RID: 4823
		string Name { get; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060012D8 RID: 4824
		Type ReflectedType { get; }
	}
}
