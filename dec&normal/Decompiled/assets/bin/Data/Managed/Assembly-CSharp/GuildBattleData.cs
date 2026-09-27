using System;

// Token: 0x02000174 RID: 372
public class GuildBattleData
{
	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00062EF4 File Offset: 0x000610F4
	public string MName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name, new object[0]);
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00062F08 File Offset: 0x00061108
	public string MDesc
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Desc, new object[0]);
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06000F59 RID: 3929 RVA: 0x00062F1C File Offset: 0x0006111C
	public string MRule
	{
		get
		{
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			return StrDictionary.GetDictionaryString(this.Rule, new object[]
			{
				TimeTools.GetLocalShowTime_HM((long)this.StartTime1, playerCommonData.TimeOffset),
				TimeTools.GetLocalShowTime_HM((long)this.StartTime, playerCommonData.TimeOffset)
			});
		}
	}

	// Token: 0x04000F01 RID: 3841
	public string ID = string.Empty;

	// Token: 0x04000F02 RID: 3842
	public int LevelMin;

	// Token: 0x04000F03 RID: 3843
	public int LevelMax = int.MaxValue;

	// Token: 0x04000F04 RID: 3844
	public string ShowRewardID = string.Empty;

	// Token: 0x04000F05 RID: 3845
	public string Name = string.Empty;

	// Token: 0x04000F06 RID: 3846
	public string Desc = string.Empty;

	// Token: 0x04000F07 RID: 3847
	public string Rule = string.Empty;

	// Token: 0x04000F08 RID: 3848
	public string Background = string.Empty;

	// Token: 0x04000F09 RID: 3849
	public string Icon = string.Empty;

	// Token: 0x04000F0A RID: 3850
	public int Week = -1;

	// Token: 0x04000F0B RID: 3851
	public int StartTime = -1;

	// Token: 0x04000F0C RID: 3852
	public int Week1 = -1;

	// Token: 0x04000F0D RID: 3853
	public int StartTime1 = -1;

	// Token: 0x04000F0E RID: 3854
	public int MaxPlayNum;

	// Token: 0x04000F0F RID: 3855
	public int Week2 = -1;

	// Token: 0x04000F10 RID: 3856
	public int StartTime2 = -1;

	// Token: 0x04000F11 RID: 3857
	public int Week3 = -1;

	// Token: 0x04000F12 RID: 3858
	public int StartTime3 = -1;

	// Token: 0x04000F13 RID: 3859
	public int DurationTime = 1800;

	// Token: 0x04000F14 RID: 3860
	public int WaitTime = 10;

	// Token: 0x04000F15 RID: 3861
	public string FirstDropID = string.Empty;

	// Token: 0x04000F16 RID: 3862
	public int FirstRank;

	// Token: 0x04000F17 RID: 3863
	public string SecondDropID = string.Empty;

	// Token: 0x04000F18 RID: 3864
	public int SecondRank;

	// Token: 0x04000F19 RID: 3865
	public string ThirdDropID = string.Empty;

	// Token: 0x04000F1A RID: 3866
	public int ThirdRank;

	// Token: 0x04000F1B RID: 3867
	public string ParticipateDropID = string.Empty;

	// Token: 0x04000F1C RID: 3868
	public string RankDropID1 = string.Empty;

	// Token: 0x04000F1D RID: 3869
	public string RankDropID2 = string.Empty;

	// Token: 0x04000F1E RID: 3870
	public string RankDropID3 = string.Empty;

	// Token: 0x04000F1F RID: 3871
	public string RankDropID4 = string.Empty;

	// Token: 0x04000F20 RID: 3872
	public string RankDropID5 = string.Empty;

	// Token: 0x04000F21 RID: 3873
	public string RankDropID6 = string.Empty;

	// Token: 0x04000F22 RID: 3874
	public string RankDropID7 = string.Empty;

	// Token: 0x04000F23 RID: 3875
	public string RankDropID8 = string.Empty;

	// Token: 0x04000F24 RID: 3876
	public string WinDropID = string.Empty;

	// Token: 0x04000F25 RID: 3877
	public string FailureDropID = string.Empty;

	// Token: 0x04000F26 RID: 3878
	public string GuessID = string.Empty;

	// Token: 0x04000F27 RID: 3879
	public int GuessCost;

	// Token: 0x04000F28 RID: 3880
	public int GuessSucess = 100;
}
