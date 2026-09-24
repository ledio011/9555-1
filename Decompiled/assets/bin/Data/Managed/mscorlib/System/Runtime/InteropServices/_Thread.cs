using System;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200022A RID: 554
	[CLSCompliant(false)]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibImportClass(typeof(Thread))]
	[Guid("C281C7F1-4AA9-3517-961A-463CFED57E75")]
	public interface _Thread
	{
		// Token: 0x060012F4 RID: 4852
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060012F5 RID: 4853
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060012F6 RID: 4854
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060012F7 RID: 4855
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
