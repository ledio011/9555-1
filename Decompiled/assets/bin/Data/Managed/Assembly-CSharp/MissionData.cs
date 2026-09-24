using System;

// Token: 0x02000187 RID: 391
public class MissionData
{
	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0006422C File Offset: 0x0006242C
	public MISSION_TRIGGER_TUTORIAL_TYPE TrigerType
	{
		get
		{
			return (MISSION_TRIGGER_TUTORIAL_TYPE)this.TriggerTutorialType;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00064234 File Offset: 0x00062434
	public MISSION_LOGICTYPE MissionLogicType
	{
		get
		{
			return (MISSION_LOGICTYPE)this.LogicType;
		}
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0006423C File Offset: 0x0006243C
	public string MName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name, new object[0]);
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00064250 File Offset: 0x00062450
	public string MDescribeID
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.DescribeID, new object[0]);
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00064264 File Offset: 0x00062464
	public string MTipDescribeID
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.TipDescribeID, new object[0]);
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00064278 File Offset: 0x00062478
	public string[] NextSideIDList
	{
		get
		{
			if (this.mNextSideIDList == null)
			{
				if (this.NextSideID.Contains(";"))
				{
					this.mNextSideIDList = this.NextSideID.Split(new char[]
					{
						';'
					});
				}
				else
				{
					this.mNextSideIDList = new string[1];
					this.mNextSideIDList[0] = this.NextSideID;
				}
			}
			return this.mNextSideIDList;
		}
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x000642E8 File Offset: 0x000624E8
	public int GetTimeLimitMissionStar()
	{
		if (this.Class != 8)
		{
			return 0;
		}
		TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(this.TimeLimitId);
		long missionRestTime = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetMissionRestTime(this.ID);
		if (missionRestTime >= timeLimitMissionDataByID.Reward3Time)
		{
			return 3;
		}
		if (missionRestTime >= timeLimitMissionDataByID.Reward2Time)
		{
			return 2;
		}
		if (missionRestTime >= timeLimitMissionDataByID.Reward1Time)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x04001057 RID: 4183
	public string ID = string.Empty;

	// Token: 0x04001058 RID: 4184
	[ServerExclude("ServerNoUse")]
	public string Name = string.Empty;

	// Token: 0x04001059 RID: 4185
	public string DescribeID = string.Empty;

	// Token: 0x0400105A RID: 4186
	[ServerExclude("ServerNoUse")]
	public string TipDescribeID = string.Empty;

	// Token: 0x0400105B RID: 4187
	[ServerExclude("ServerNoUse")]
	public string TagDescribeID = string.Empty;

	// Token: 0x0400105C RID: 4188
	public int Class = -1;

	// Token: 0x0400105D RID: 4189
	public int LogicType = -1;

	// Token: 0x0400105E RID: 4190
	public string LogicID = string.Empty;

	// Token: 0x0400105F RID: 4191
	public string PreID = string.Empty;

	// Token: 0x04001060 RID: 4192
	public string NextID = string.Empty;

	// Token: 0x04001061 RID: 4193
	public string AcceptDialog = string.Empty;

	// Token: 0x04001062 RID: 4194
	public string TargetDialog = string.Empty;

	// Token: 0x04001063 RID: 4195
	public string CompleteDialog = string.Empty;

	// Token: 0x04001064 RID: 4196
	public string Target = string.Empty;

	// Token: 0x04001065 RID: 4197
	public string TargetMapId = string.Empty;

	// Token: 0x04001066 RID: 4198
	public string Accept = string.Empty;

	// Token: 0x04001067 RID: 4199
	public string AcceptMapId = string.Empty;

	// Token: 0x04001068 RID: 4200
	public string Submit = string.Empty;

	// Token: 0x04001069 RID: 4201
	public string SubmitMapId = string.Empty;

	// Token: 0x0400106A RID: 4202
	public int MinLv;

	// Token: 0x0400106B RID: 4203
	public int DisplayLv;

	// Token: 0x0400106C RID: 4204
	public int AutoAcceptLv;

	// Token: 0x0400106D RID: 4205
	public int Repeat;

	// Token: 0x0400106E RID: 4206
	public string XDShowID = string.Empty;

	// Token: 0x0400106F RID: 4207
	public string XDDropID = string.Empty;

	// Token: 0x04001070 RID: 4208
	public string QJShowID = string.Empty;

	// Token: 0x04001071 RID: 4209
	public string QJDropID = string.Empty;

	// Token: 0x04001072 RID: 4210
	public string NQSShowID = string.Empty;

	// Token: 0x04001073 RID: 4211
	public string NQSDropID = string.Empty;

	// Token: 0x04001074 RID: 4212
	public int ShowStoryState = -1;

	// Token: 0x04001075 RID: 4213
	public string StoryID = string.Empty;

	// Token: 0x04001076 RID: 4214
	public int IsMultiMission;

	// Token: 0x04001077 RID: 4215
	public string TimeLimitId = string.Empty;

	// Token: 0x04001078 RID: 4216
	public int TriggerTutorialType = -1;

	// Token: 0x04001079 RID: 4217
	public string NextSideID = string.Empty;

	// Token: 0x0400107A RID: 4218
	private string[] mNextSideIDList;

	// Token: 0x0400107B RID: 4219
	public int ShowRank = -1;

	// Token: 0x0400107C RID: 4220
	public string TipStr = string.Empty;
}
