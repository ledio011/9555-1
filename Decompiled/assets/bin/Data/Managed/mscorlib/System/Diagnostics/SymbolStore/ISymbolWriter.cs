using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020000DC RID: 220
	[ComVisible(true)]
	public interface ISymbolWriter
	{
		// Token: 0x06000896 RID: 2198
		void Initialize(IntPtr emitter, string filename, bool fFullBuild);
	}
}
