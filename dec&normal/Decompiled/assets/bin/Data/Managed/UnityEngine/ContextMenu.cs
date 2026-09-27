using System;

namespace UnityEngine
{
	// Token: 0x02000043 RID: 67
	public sealed class ContextMenu : Attribute
	{
		// Token: 0x06000384 RID: 900 RVA: 0x000080A4 File Offset: 0x000062A4
		public ContextMenu(string name)
		{
			this.m_ItemName = name;
		}

		// Token: 0x04000068 RID: 104
		private string m_ItemName;
	}
}
