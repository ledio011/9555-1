using System;

// Token: 0x020001C5 RID: 453
public class WildBossData
{
	// Token: 0x1700038D RID: 909
	// (get) Token: 0x06001048 RID: 4168 RVA: 0x0006688C File Offset: 0x00064A8C
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

	// Token: 0x0400139B RID: 5019
	public string ID;

	// Token: 0x0400139C RID: 5020
	public string MapID;

	// Token: 0x0400139D RID: 5021
	public int Type;

	// Token: 0x0400139E RID: 5022
	public string Desc;

	// Token: 0x0400139F RID: 5023
	public string Rule;

	// Token: 0x040013A0 RID: 5024
	public int SubType;

	// Token: 0x040013A1 RID: 5025
	public string StartTime;

	// Token: 0x040013A2 RID: 5026
	public int DurationTime;

	// Token: 0x040013A3 RID: 5027
	public int WaitTime;

	// Token: 0x040013A4 RID: 5028
	public int ExistTime;

	// Token: 0x040013A5 RID: 5029
	public int MaxPlayer;

	// Token: 0x040013A6 RID: 5030
	public int LevelMin;

	// Token: 0x040013A7 RID: 5031
	public int LevelMax;

	// Token: 0x040013A8 RID: 5032
	public string LastKillDropID;

	// Token: 0x040013A9 RID: 5033
	public string FirstDropID;

	// Token: 0x040013AA RID: 5034
	public int FirstRank;

	// Token: 0x040013AB RID: 5035
	public string SecondDropID;

	// Token: 0x040013AC RID: 5036
	public int SecondRank;

	// Token: 0x040013AD RID: 5037
	public string ThirdDropID;

	// Token: 0x040013AE RID: 5038
	public int ThirdRank;

	// Token: 0x040013AF RID: 5039
	public string FourthDropID;

	// Token: 0x040013B0 RID: 5040
	public int FourthRank;

	// Token: 0x040013B1 RID: 5041
	public string FifthDropID;

	// Token: 0x040013B2 RID: 5042
	public int FifthRank;

	// Token: 0x040013B3 RID: 5043
	public string ParticipateDropID;

	// Token: 0x040013B4 RID: 5044
	public string ShowRewardID = "1";

	// Token: 0x040013B5 RID: 5045
	public string BossID;

	// Token: 0x040013B6 RID: 5046
	public string Icon;

	// Token: 0x040013B7 RID: 5047
	public string Background;

	// Token: 0x040013B8 RID: 5048
	public long MinHp = 100L;

	// Token: 0x040013B9 RID: 5049
	public long MaxHp = 2147483647L;

	// Token: 0x040013BA RID: 5050
	public long SingleHp = 100L;

	// Token: 0x040013BB RID: 5051
	public int PVP;

	// Token: 0x040013BC RID: 5052
	public int Time;

	// Token: 0x040013BD RID: 5053
	public int Limit;

	// Token: 0x040013BE RID: 5054
	private long[] mStartTimes;
}
