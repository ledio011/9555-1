using System;
using SprotoType;

// Token: 0x02000141 RID: 321
public class PlayerRankPVPData
{
	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0005FE68 File Offset: 0x0005E068
	// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0005FE70 File Offset: 0x0005E070
	public int RankPVPRankNum
	{
		get
		{
			return this.mRankPVPRankNum;
		}
		set
		{
			this.mRankPVPRankNum = value;
		}
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0005FE7C File Offset: 0x0005E07C
	public string GetRankPosStr()
	{
		if (this.mRankPVPRankNum > 0 && this.mRankPVPRankNum < 1001)
		{
			return this.mRankPVPRankNum.ToString();
		}
		return "--";
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x0005FEAC File Offset: 0x0005E0AC
	public string GetBestRankPosStr()
	{
		if (this.mBestRankPVPRankNum > 0 && this.mBestRankPVPRankNum < 1001)
		{
			return this.mBestRankPVPRankNum.ToString();
		}
		return "--";
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0005FEDC File Offset: 0x0005E0DC
	// (set) Token: 0x06000E8E RID: 3726 RVA: 0x0005FEE4 File Offset: 0x0005E0E4
	public int RankTimes
	{
		get
		{
			return this.mRankTimes;
		}
		set
		{
			this.mRankTimes = value;
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0005FEF0 File Offset: 0x0005E0F0
	// (set) Token: 0x06000E90 RID: 3728 RVA: 0x0005FEF8 File Offset: 0x0005E0F8
	public int BestRankPVPRankNum
	{
		get
		{
			return this.mBestRankPVPRankNum;
		}
		set
		{
			this.mBestRankPVPRankNum = value;
		}
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x0005FF04 File Offset: 0x0005E104
	public void Reset()
	{
		this.mRankTimes = 0;
		this.mBestRankPVPRankNum = -1;
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0005FF14 File Offset: 0x0005E114
	public void UpdateRankPvPData(syn_rank_pvp_data.request request)
	{
		if (request.HasRankPos)
		{
			this.mRankPVPRankNum = (int)request.rankPos;
		}
		else
		{
			this.mRankPVPRankNum = -1;
		}
		if (request.HasBestRankPos)
		{
			this.mBestRankPVPRankNum = (int)request.bestRankPos;
		}
		else
		{
			this.mBestRankPVPRankNum = -1;
		}
		this.mRankTimes = (int)request.times;
		this.UpdateTips();
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x0005FF7C File Offset: 0x0005E17C
	public void UpdateRankPvpInfo(tiantti_result.request request)
	{
		if (request.HasRankPos2)
		{
			this.mRankPVPRankNum = (int)request.rankPos2;
		}
		if (request.HasBestRankPos)
		{
			this.mBestRankPVPRankNum = (int)request.bestRankPos;
		}
		else
		{
			this.mBestRankPVPRankNum = -1;
		}
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x0005FFC8 File Offset: 0x0005E1C8
	public void SyncTimesPVPlocal()
	{
		if (this.mRankTimes > 0)
		{
			this.mRankTimes--;
		}
		this.UpdateTips();
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0005FFF8 File Offset: 0x0005E1F8
	public bool IsHavePVPTips()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.RANK_PVP) && this.mRankTimes > 0;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0006002C File Offset: 0x0005E22C
	public void UpdateTips()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMessageTips();
		}
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.RefershTips();
		}
		if (SingletonUnity<ActivityTipsRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<ActivityTipsRootLogic>.Instance.gameObject))
		{
			SingletonUnity<ActivityTipsRootLogic>.Instance.UpdateInfo();
		}
	}

	// Token: 0x04000BEB RID: 3051
	public const int MaxRankPos = 1000;

	// Token: 0x04000BEC RID: 3052
	public const int MaxRankTimes = 10;

	// Token: 0x04000BED RID: 3053
	private int mRankPVPRankNum = -1;

	// Token: 0x04000BEE RID: 3054
	private int mRankTimes;

	// Token: 0x04000BEF RID: 3055
	private int mBestRankPVPRankNum = -1;
}
