using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x0200001E RID: 30
	[ComVisible(true)]
	public interface ISite : IServiceProvider
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000061 RID: 97
		IContainer Container { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000062 RID: 98
		string Name { get; }
	}
}
