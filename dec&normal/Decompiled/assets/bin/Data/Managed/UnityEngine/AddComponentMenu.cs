using System;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public sealed class AddComponentMenu : Attribute
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00003444 File Offset: 0x00001644
		public AddComponentMenu(string menuName)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = 0;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000345C File Offset: 0x0000165C
		public AddComponentMenu(string menuName, int order)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = order;
		}

		// Token: 0x04000001 RID: 1
		private string m_AddComponentMenu;

		// Token: 0x04000002 RID: 2
		private int m_Ordering;
	}
}
