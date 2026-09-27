using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class ActivityBossData
{
	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0006026C File Offset: 0x0005E46C
	public List<Vector3> NpcPosList
	{
		get
		{
			if (this.mNpcPosList.Count == 0)
			{
				string[] array = this.NpcPos.Split(new char[]
				{
					'#'
				});
				int[] array2 = new int[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = int.Parse(array[i]);
				}
				for (int j = 0; j < array2.Length / 2; j++)
				{
					this.mNpcPosList.Add(new Vector3((float)array2[j * 2] / 100f, 0f, (float)array2[j * 2 + 1] / 100f));
				}
			}
			return this.mNpcPosList;
		}
	}

	// Token: 0x04000C16 RID: 3094
	public string Key;

	// Token: 0x04000C17 RID: 3095
	public string ID;

	// Token: 0x04000C18 RID: 3096
	public string MapID;

	// Token: 0x04000C19 RID: 3097
	public string NpcID;

	// Token: 0x04000C1A RID: 3098
	public string NpcPos;

	// Token: 0x04000C1B RID: 3099
	public int ReBirthTime;

	// Token: 0x04000C1C RID: 3100
	public string DropID1;

	// Token: 0x04000C1D RID: 3101
	public int Rank1;

	// Token: 0x04000C1E RID: 3102
	public string DropID2;

	// Token: 0x04000C1F RID: 3103
	public int Rank2;

	// Token: 0x04000C20 RID: 3104
	public string DropID3;

	// Token: 0x04000C21 RID: 3105
	public int Rank3;

	// Token: 0x04000C22 RID: 3106
	public string DropID4;

	// Token: 0x04000C23 RID: 3107
	public int Rank4;

	// Token: 0x04000C24 RID: 3108
	public string DropID5;

	// Token: 0x04000C25 RID: 3109
	public int Rank5;

	// Token: 0x04000C26 RID: 3110
	public string DropID6;

	// Token: 0x04000C27 RID: 3111
	public int Rank6;

	// Token: 0x04000C28 RID: 3112
	private List<Vector3> mNpcPosList = new List<Vector3>();
}
