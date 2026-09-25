using System;
using UnityEngine;

// Token: 0x020001AF RID: 431
public class SingleMapLockData
{
	// Token: 0x1700036B RID: 875
	// (get) Token: 0x06001006 RID: 4102 RVA: 0x00065A08 File Offset: 0x00063C08
	public Vector3 UIPos
	{
		get
		{
			return new Vector3((float)this.UIPosX / 100f, 0f, (float)this.UIPosZ / 100f);
		}
	}

	// Token: 0x0400128D RID: 4749
	public int ID = -1;

	// Token: 0x0400128E RID: 4750
	public int UnlockLevel;

	// Token: 0x0400128F RID: 4751
	public string AreaName = string.Empty;

	// Token: 0x04001290 RID: 4752
	public float UnlockAlph = 1.1f;

	// Token: 0x04001291 RID: 4753
	public int UIPosX;

	// Token: 0x04001292 RID: 4754
	public int UIPosZ;
}
