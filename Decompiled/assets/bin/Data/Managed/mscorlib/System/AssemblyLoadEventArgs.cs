using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000069 RID: 105
	[ComVisible(true)]
	public class AssemblyLoadEventArgs : EventArgs
	{
		// Token: 0x0600033A RID: 826 RVA: 0x00011A9C File Offset: 0x0000FC9C
		public AssemblyLoadEventArgs(Assembly loadedAssembly)
		{
			this.m_loadedAssembly = loadedAssembly;
		}

		// Token: 0x0400019A RID: 410
		private Assembly m_loadedAssembly;
	}
}
