using System;
using System.Collections.Generic;

// Token: 0x020001C0 RID: 448
public class TimeLimitMissionData
{
	// Token: 0x17000385 RID: 901
	// (get) Token: 0x0600103B RID: 4155 RVA: 0x00066504 File Offset: 0x00064704
	public string MName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name, new object[0]);
		}
	}

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x0600103C RID: 4156 RVA: 0x00066518 File Offset: 0x00064718
	public string MDesc
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Desc, new object[0]);
		}
	}

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x0600103D RID: 4157 RVA: 0x0006652C File Offset: 0x0006472C
	public string MTip
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Tip, new object[0]);
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x0600103E RID: 4158 RVA: 0x00066540 File Offset: 0x00064740
	public List<string> MissionList
	{
		get
		{
			if (this.mMissionList == null)
			{
				this.mMissionList = new List<string>();
				string[] array = this.ContainsMission.Split(new char[]
				{
					'#'
				});
				for (int i = 0; i < array.Length; i++)
				{
					this.MissionList.Add(array[i]);
				}
			}
			return this.mMissionList;
		}
	}

	// Token: 0x0400134D RID: 4941
	public string ID;

	// Token: 0x0400134E RID: 4942
	public string Name;

	// Token: 0x0400134F RID: 4943
	public string Desc;

	// Token: 0x04001350 RID: 4944
	public string Tip;

	// Token: 0x04001351 RID: 4945
	public string ContainsMission;

	// Token: 0x04001352 RID: 4946
	public long LimitTime;

	// Token: 0x04001353 RID: 4947
	public int LimitNum;

	// Token: 0x04001354 RID: 4948
	public string Reward1;

	// Token: 0x04001355 RID: 4949
	public string ShowReward1;

	// Token: 0x04001356 RID: 4950
	public long Reward1Time;

	// Token: 0x04001357 RID: 4951
	public string Reward2;

	// Token: 0x04001358 RID: 4952
	public string ShowReward2;

	// Token: 0x04001359 RID: 4953
	public long Reward2Time;

	// Token: 0x0400135A RID: 4954
	public string Reward3;

	// Token: 0x0400135B RID: 4955
	public string ShowReward3;

	// Token: 0x0400135C RID: 4956
	public long Reward3Time;

	// Token: 0x0400135D RID: 4957
	public string ShowRewardId;

	// Token: 0x0400135E RID: 4958
	private List<string> mMissionList;
}
