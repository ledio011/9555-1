using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200021A RID: 538
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(EventInfo))]
	[Guid("9DE59C64-D889-35A1-B897-587D74469E5B")]
	[ComVisible(true)]
	[CLSCompliant(false)]
	public interface _EventInfo
	{
		// Token: 0x060012BF RID: 4799
		MethodInfo GetAddMethod(bool nonPublic);

		// Token: 0x060012C0 RID: 4800
		MethodInfo GetRemoveMethod(bool nonPublic);

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060012C1 RID: 4801
		EventAttributes Attributes { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060012C2 RID: 4802
		Type EventHandlerType { get; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060012C3 RID: 4803
		MemberTypes MemberType { get; }
	}
}
