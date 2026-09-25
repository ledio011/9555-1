using System;

namespace UnityEngine
{
	// Token: 0x0200008A RID: 138
	[Flags]
	public enum HideFlags
	{
		// Token: 0x04000199 RID: 409
		None = 0,
		// Token: 0x0400019A RID: 410
		HideInHierarchy = 1,
		// Token: 0x0400019B RID: 411
		HideInInspector = 2,
		// Token: 0x0400019C RID: 412
		DontSave = 4,
		// Token: 0x0400019D RID: 413
		NotEditable = 8,
		// Token: 0x0400019E RID: 414
		HideAndDontSave = 13
	}
}
