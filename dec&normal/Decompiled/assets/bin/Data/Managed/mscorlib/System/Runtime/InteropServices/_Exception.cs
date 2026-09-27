using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200021B RID: 539
	[CLSCompliant(false)]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("b36b5c63-42ef-38bc-a07e-0b34c98f164a")]
	public interface _Exception
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060012C4 RID: 4804
		Exception InnerException { get; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060012C5 RID: 4805
		string Message { get; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060012C6 RID: 4806
		string Source { get; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060012C7 RID: 4807
		string StackTrace { get; }

		// Token: 0x060012C8 RID: 4808
		Type GetType();

		// Token: 0x060012C9 RID: 4809
		string ToString();
	}
}
