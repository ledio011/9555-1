using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001F6 RID: 502
	[ComVisible(true)]
	public class ResolveEventArgs : EventArgs
	{
		// Token: 0x0600127D RID: 4733 RVA: 0x0004513C File Offset: 0x0004333C
		public ResolveEventArgs(string name)
		{
			this.m_Name = name;
		}

		// Token: 0x04000988 RID: 2440
		private string m_Name;
	}
}
