using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200023C RID: 572
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class DllImportAttribute : Attribute
	{
		// Token: 0x040009F2 RID: 2546
		public CallingConvention CallingConvention;

		// Token: 0x040009F3 RID: 2547
		public CharSet CharSet;

		// Token: 0x040009F4 RID: 2548
		private string Dll;

		// Token: 0x040009F5 RID: 2549
		public string EntryPoint;

		// Token: 0x040009F6 RID: 2550
		public bool ExactSpelling;

		// Token: 0x040009F7 RID: 2551
		public bool PreserveSig;

		// Token: 0x040009F8 RID: 2552
		public bool SetLastError;

		// Token: 0x040009F9 RID: 2553
		public bool BestFitMapping;

		// Token: 0x040009FA RID: 2554
		public bool ThrowOnUnmappableChar;
	}
}
