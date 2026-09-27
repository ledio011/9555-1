using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class GuildCaptureData
{
	// Token: 0x06000F5C RID: 3932 RVA: 0x00062F8C File Offset: 0x0006118C
	public Vector3 GetNpcPos()
	{
		if (this.mPos == null)
		{
			string[] array = this.NpcPos.Split(new char[]
			{
				'#'
			});
			this.mPos = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.mPos[i] = int.Parse(array[i]);
			}
		}
		float num = (float)this.mPos[0] / 100f;
		float num2 = (float)this.mPos[1] / 100f;
		return new Vector3(num, SceneManager.GetHitHeight(new Vector3(num, 0f, num2)), num2);
	}

	// Token: 0x04000F44 RID: 3908
	public string ID;

	// Token: 0x04000F45 RID: 3909
	public string MapID;

	// Token: 0x04000F46 RID: 3910
	public int DurationTime;

	// Token: 0x04000F47 RID: 3911
	public int Week;

	// Token: 0x04000F48 RID: 3912
	public int StartTime;

	// Token: 0x04000F49 RID: 3913
	public string ShowRewardID;

	// Token: 0x04000F4A RID: 3914
	public string DropID;

	// Token: 0x04000F4B RID: 3915
	public string NpcID;

	// Token: 0x04000F4C RID: 3916
	public string NpcPos;

	// Token: 0x04000F4D RID: 3917
	public int Basestatus;

	// Token: 0x04000F4E RID: 3918
	public int BSValue;

	// Token: 0x04000F4F RID: 3919
	public string Icon;

	// Token: 0x04000F50 RID: 3920
	public string Name;

	// Token: 0x04000F51 RID: 3921
	public string Rule;

	// Token: 0x04000F52 RID: 3922
	public string Description;

	// Token: 0x04000F53 RID: 3923
	public string Background;

	// Token: 0x04000F54 RID: 3924
	private int[] mPos;
}
