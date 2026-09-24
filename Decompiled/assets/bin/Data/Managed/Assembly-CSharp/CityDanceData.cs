using System;

// Token: 0x02000159 RID: 345
public class CityDanceData
{
	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00061964 File Offset: 0x0005FB64
	public long[] StartTimes
	{
		get
		{
			if ((this.mStartTimes == null || this.mStartTimes.Length == 0) && !string.IsNullOrEmpty(this.StartTime))
			{
				string[] array = this.StartTime.Split(new char[]
				{
					'#'
				});
				this.mStartTimes = new long[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mStartTimes[i] = long.Parse(array[i]);
				}
			}
			return this.mStartTimes;
		}
	}

	// Token: 0x04000D64 RID: 3428
	public string ID;

	// Token: 0x04000D65 RID: 3429
	public string MapId;

	// Token: 0x04000D66 RID: 3430
	public int Type;

	// Token: 0x04000D67 RID: 3431
	public string ShowRewardId;

	// Token: 0x04000D68 RID: 3432
	public string DropId;

	// Token: 0x04000D69 RID: 3433
	public int DurationTime;

	// Token: 0x04000D6A RID: 3434
	public int RewardDuration;

	// Token: 0x04000D6B RID: 3435
	public int UnlockLevel;

	// Token: 0x04000D6C RID: 3436
	public string Name;

	// Token: 0x04000D6D RID: 3437
	public string Description;

	// Token: 0x04000D6E RID: 3438
	public string SceneObjName;

	// Token: 0x04000D6F RID: 3439
	public string Rule;

	// Token: 0x04000D70 RID: 3440
	public string Icon;

	// Token: 0x04000D71 RID: 3441
	public string Background;

	// Token: 0x04000D72 RID: 3442
	public string NpcID;

	// Token: 0x04000D73 RID: 3443
	public string MissionIcon;

	// Token: 0x04000D74 RID: 3444
	public string StartTime;

	// Token: 0x04000D75 RID: 3445
	public int CDTime;

	// Token: 0x04000D76 RID: 3446
	private long[] mStartTimes;
}
