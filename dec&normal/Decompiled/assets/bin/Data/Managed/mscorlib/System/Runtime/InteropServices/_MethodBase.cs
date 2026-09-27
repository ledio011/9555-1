using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000221 RID: 545
	[Guid("6240837A-707F-3181-8E98-A36AE086766B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(MethodBase))]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface _MethodBase
	{
		// Token: 0x060012D9 RID: 4825
		MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x060012DA RID: 4826
		ParameterInfo[] GetParameters();

		// Token: 0x060012DB RID: 4827
		object Invoke(object obj, object[] parameters);

		// Token: 0x060012DC RID: 4828
		object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060012DD RID: 4829
		MethodAttributes Attributes { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060012DE RID: 4830
		CallingConventions CallingConvention { get; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060012DF RID: 4831
		bool IsAbstract { get; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060012E0 RID: 4832
		bool IsPublic { get; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060012E1 RID: 4833
		bool IsStatic { get; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060012E2 RID: 4834
		bool IsVirtual { get; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060012E3 RID: 4835
		RuntimeMethodHandle MethodHandle { get; }
	}
}
