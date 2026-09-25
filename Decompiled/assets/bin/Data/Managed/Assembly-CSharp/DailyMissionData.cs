using System;

// Token: 0x02000165 RID: 357
public class DailyMissionData
{
	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00061D64 File Offset: 0x0005FF64
	public string[] ShowRewardIDList
	{
		get
		{
			if (this.mShowRewardIDList == null)
			{
				this.mShowRewardIDList = this.ShowRewardID.Split(new char[]
				{
					'#'
				});
			}
			return this.mShowRewardIDList;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06000F0B RID: 3851 RVA: 0x00061D94 File Offset: 0x0005FF94
	public string[] DropIDList
	{
		get
		{
			if (this.mDropIDList == null)
			{
				this.mDropIDList = this.DropID.Split(new char[]
				{
					'#'
				});
			}
			return this.mDropIDList;
		}
	}

	// Token: 0x04000DDC RID: 3548
	public string ID = string.Empty;

	// Token: 0x04000DDD RID: 3549
	public int LevelMax;

	// Token: 0x04000DDE RID: 3550
	public int LevelMin;

	// Token: 0x04000DDF RID: 3551
	public string ShowRewardID = string.Empty;

	// Token: 0x04000DE0 RID: 3552
	public string DropID = string.Empty;

	// Token: 0x04000DE1 RID: 3553
	[ServerExclude("ServerNoUse")]
	public string[] mShowRewardIDList;

	// Token: 0x04000DE2 RID: 3554
	[ServerExclude("ServerNoUse")]
	public string[] mDropIDList;
}
