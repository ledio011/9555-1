using System;
using System.Collections.Generic;

// Token: 0x0200015E RID: 350
public class CopySceneData
{
	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00061B5C File Offset: 0x0005FD5C
	public bool IsTeamCopy
	{
		get
		{
			return this.MaxMember > 1;
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00061B68 File Offset: 0x0005FD68
	public bool IsCarChasingCopy
	{
		get
		{
			return this.SubType == 7;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00061B74 File Offset: 0x0005FD74
	public bool IsScuffleCopy
	{
		get
		{
			return this.SubType == 20;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00061B80 File Offset: 0x0005FD80
	public bool IsSingleDance
	{
		get
		{
			return this.SubType == 26;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00061B8C File Offset: 0x0005FD8C
	public bool IsPVPMap
	{
		get
		{
			return this.SubType == 1;
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00061B98 File Offset: 0x0005FD98
	public bool IsSexGame
	{
		get
		{
			return this.SubType == 27;
		}
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x00061BA4 File Offset: 0x0005FDA4
	public bool IsPVPFlag()
	{
		return this.SubType == 1 || this.SubType == 20;
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00061BC4 File Offset: 0x0005FDC4
	public bool IsCashCopy
	{
		get
		{
			return this.SubType == 11;
		}
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00061BD0 File Offset: 0x0005FDD0
	public bool IsExpCopy
	{
		get
		{
			return this.SubType == 12 || this.SubType == 22;
		}
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00061BEC File Offset: 0x0005FDEC
	public bool IsEquipCopy
	{
		get
		{
			return this.SubType == 16;
		}
	}

	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00061BF8 File Offset: 0x0005FDF8
	public string MName
	{
		get
		{
			return StrDictionary.GetDictionaryString(this.Name, new object[0]);
		}
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x00061C0C File Offset: 0x0005FE0C
	public void GetStarDescriptionList(int gradeFlag, List<string> resultList)
	{
		resultList.Clear();
		if (gradeFlag % 2 == 1)
		{
			resultList.Add(this.StarDescription1);
		}
		gradeFlag /= 2;
		if (gradeFlag % 2 == 1)
		{
			resultList.Add(this.StarDescription2);
		}
		gradeFlag /= 2;
		if (gradeFlag % 2 == 1)
		{
			resultList.Add(this.StarDescription3);
		}
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00061C68 File Offset: 0x0005FE68
	public string BackgroundPath
	{
		get
		{
			return "UI/CopyMissionPic/" + this.Background;
		}
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x00061C7C File Offset: 0x0005FE7C
	public bool IsCopyDifficult()
	{
		return int.Parse(this.ID) % 2 == 0;
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00061C90 File Offset: 0x0005FE90
	public COPY_SCENE_TYPE CopyType
	{
		get
		{
			return (COPY_SCENE_TYPE)this.Type;
		}
	}

	// Token: 0x04000D8C RID: 3468
	public string ID = string.Empty;

	// Token: 0x04000D8D RID: 3469
	[ServerExclude("ServerNoUse")]
	public string Desc = string.Empty;

	// Token: 0x04000D8E RID: 3470
	[ServerExclude("ServerNoUse")]
	public string Rule = string.Empty;

	// Token: 0x04000D8F RID: 3471
	public string MapId = string.Empty;

	// Token: 0x04000D90 RID: 3472
	public string ShowRewardId = string.Empty;

	// Token: 0x04000D91 RID: 3473
	public string DropId = string.Empty;

	// Token: 0x04000D92 RID: 3474
	public int WaitTime;

	// Token: 0x04000D93 RID: 3475
	public int Type = -1;

	// Token: 0x04000D94 RID: 3476
	public int SubType = -1;

	// Token: 0x04000D95 RID: 3477
	public int ExistTime;

	// Token: 0x04000D96 RID: 3478
	public int EndTime = 5;

	// Token: 0x04000D97 RID: 3479
	public int Reload;

	// Token: 0x04000D98 RID: 3480
	public int Parm1;

	// Token: 0x04000D99 RID: 3481
	public int Parm2;

	// Token: 0x04000D9A RID: 3482
	public int Parm3 = -1;

	// Token: 0x04000D9B RID: 3483
	public int Parm4;

	// Token: 0x04000D9C RID: 3484
	[ServerExclude("ServerNoUse")]
	public string Name = string.Empty;

	// Token: 0x04000D9D RID: 3485
	public int MaxPlayNum;

	// Token: 0x04000D9E RID: 3486
	public int MinLevel;

	// Token: 0x04000D9F RID: 3487
	public int MaxLevel = 80;

	// Token: 0x04000DA0 RID: 3488
	public int MinMember;

	// Token: 0x04000DA1 RID: 3489
	public int MaxMember;

	// Token: 0x04000DA2 RID: 3490
	[ServerExclude("ServerNoUse")]
	public string Background = string.Empty;

	// Token: 0x04000DA3 RID: 3491
	public string Icon = string.Empty;

	// Token: 0x04000DA4 RID: 3492
	public string StarDescription1;

	// Token: 0x04000DA5 RID: 3493
	public string StarDescription2;

	// Token: 0x04000DA6 RID: 3494
	public string StarDescription3;

	// Token: 0x04000DA7 RID: 3495
	public string WipeItem;

	// Token: 0x04000DA8 RID: 3496
	public int CanWipeOut = 1;

	// Token: 0x04000DA9 RID: 3497
	public string FinishPoint;

	// Token: 0x04000DAA RID: 3498
	public string TimeInc = string.Empty;

	// Token: 0x04000DAB RID: 3499
	public string SingleMapID = string.Empty;
}
