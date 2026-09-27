using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x0200001D RID: 29
	[ComVisible(true)]
	public interface IContainer : IDisposable
	{
		// Token: 0x06000060 RID: 96
		void Remove(IComponent component);
	}
}
