using System;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class KillTargetMissionData
{
	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06000F72 RID: 3954 RVA: 0x000634C4 File Offset: 0x000616C4
	public Vector3 Pos
	{
		get
		{
			float num = (float)this.PosX / 100f;
			float num2 = (float)this.PosZ / 100f;
			return new Vector3(num, SceneManager.GetHitHeight(num, num2), num2);
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000634FC File Offset: 0x000616FC
	public float PosRange
	{
		get
		{
			return (float)this.Range / 100f;
		}
	}

	// Token: 0x04000F97 RID: 3991
	public string ID = string.Empty;

	// Token: 0x04000F98 RID: 3992
	public string SceneID = string.Empty;

	// Token: 0x04000F99 RID: 3993
	public int PosX;

	// Token: 0x04000F9A RID: 3994
	public int PosZ;

	// Token: 0x04000F9B RID: 3995
	public int Range;

	// Token: 0x04000F9C RID: 3996
	public string NpcID = string.Empty;

	// Token: 0x04000F9D RID: 3997
	public int FlashNum;

	// Token: 0x04000F9E RID: 3998
	public int RequireNum;
}
