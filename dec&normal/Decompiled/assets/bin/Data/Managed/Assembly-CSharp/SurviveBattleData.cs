using System;

// Token: 0x020001BD RID: 445
public class SurviveBattleData
{
	// Token: 0x17000381 RID: 897
	// (get) Token: 0x06001033 RID: 4147 RVA: 0x0006635C File Offset: 0x0006455C
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

	// Token: 0x04001324 RID: 4900
	public string ID;

	// Token: 0x04001325 RID: 4901
	public string MapID;

	// Token: 0x04001326 RID: 4902
	public string MapID2;

	// Token: 0x04001327 RID: 4903
	public int Type;

	// Token: 0x04001328 RID: 4904
	public string StartTime;

	// Token: 0x04001329 RID: 4905
	public int DurationTime;

	// Token: 0x0400132A RID: 4906
	public int WaitTime;

	// Token: 0x0400132B RID: 4907
	public int ExistTime;

	// Token: 0x0400132C RID: 4908
	public int MaxPlayer;

	// Token: 0x0400132D RID: 4909
	public int UnlockLevel;

	// Token: 0x0400132E RID: 4910
	public int FirstMaxScore;

	// Token: 0x0400132F RID: 4911
	public int SecondMinScore;

	// Token: 0x04001330 RID: 4912
	public string FirstDropID;

	// Token: 0x04001331 RID: 4913
	public int FirstRank;

	// Token: 0x04001332 RID: 4914
	public string SecondDropID;

	// Token: 0x04001333 RID: 4915
	public int SecondRank;

	// Token: 0x04001334 RID: 4916
	public string ThirdDropID;

	// Token: 0x04001335 RID: 4917
	public int ThirdRank;

	// Token: 0x04001336 RID: 4918
	public string FourthDropID;

	// Token: 0x04001337 RID: 4919
	public int FourthRank;

	// Token: 0x04001338 RID: 4920
	public string FifthDropID;

	// Token: 0x04001339 RID: 4921
	public string ShowRewardID;

	// Token: 0x0400133A RID: 4922
	public string Name;

	// Token: 0x0400133B RID: 4923
	public string Desc;

	// Token: 0x0400133C RID: 4924
	public string Rule;

	// Token: 0x0400133D RID: 4925
	public string Icon;

	// Token: 0x0400133E RID: 4926
	public string Background;

	// Token: 0x0400133F RID: 4927
	private long[] mStartTimes;
}
