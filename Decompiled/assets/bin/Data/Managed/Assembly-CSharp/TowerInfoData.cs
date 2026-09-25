using System;
using SprotoType;

// Token: 0x02000140 RID: 320
public class TowerInfoData
{
	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0005FB84 File Offset: 0x0005DD84
	public tower_info PlayerTowerInfo
	{
		get
		{
			return this.mPlayerTowerInfo;
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0005FB8C File Offset: 0x0005DD8C
	public bool IsHaveSpecialReward
	{
		get
		{
			return this.mIsHaveSpecialReward;
		}
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x0005FB94 File Offset: 0x0005DD94
	public void Reset()
	{
		if (this.mPlayerTowerInfo != null)
		{
			this.mPlayerTowerInfo = null;
		}
		this.mIsHaveSpecialReward = false;
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x0005FBB0 File Offset: 0x0005DDB0
	public void ResetTowerData()
	{
		if (this.mPlayerTowerInfo != null)
		{
			this.mPlayerTowerInfo.times = 0L;
			this.mPlayerTowerInfo.cur_floor = 0L;
			this.UpdateTips();
		}
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x0005FBE0 File Offset: 0x0005DDE0
	public void UpdateTowerData(tower_info info)
	{
		this.mPlayerTowerInfo = info;
		this.UpdateTips();
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0005FBF0 File Offset: 0x0005DDF0
	public void UpdateTowerbest(long bestfloor)
	{
		if (this.mPlayerTowerInfo != null)
		{
			this.mPlayerTowerInfo.floor = bestfloor;
		}
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x0005FC0C File Offset: 0x0005DE0C
	public void UpdateTowerData(ret_grant_tower_reward.request request)
	{
		this.mPlayerTowerInfo = request.tower_info;
		this.mIsHaveSpecialReward = false;
		if (request.HasTower_special_reward)
		{
			for (int i = 0; i < request.tower_special_reward.Count; i++)
			{
				if (request.tower_special_reward[i].state == 0L && request.tower_special_reward[i].floor <= this.mPlayerTowerInfo.floor)
				{
					this.mIsHaveSpecialReward = true;
					break;
				}
			}
		}
		this.UpdateTips();
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x0005FC9C File Offset: 0x0005DE9C
	public void UpdateTowerData(ret_request_tower_copy_info.request request)
	{
		this.mPlayerTowerInfo = request.tower_info;
		this.mIsHaveSpecialReward = false;
		if (request.HasTower_special_reward)
		{
			for (int i = 0; i < request.tower_special_reward.Count; i++)
			{
				if (request.tower_special_reward[i].state == 0L && request.tower_special_reward[i].floor <= this.mPlayerTowerInfo.floor)
				{
					this.mIsHaveSpecialReward = true;
					break;
				}
			}
		}
		this.UpdateTips();
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x0005FD2C File Offset: 0x0005DF2C
	public bool IsHaveTowerTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_CHALLENGE))
		{
			return false;
		}
		if (this.mPlayerTowerInfo != null)
		{
			if (this.IsHaveSpecialReward)
			{
				return true;
			}
			if (this.mPlayerTowerInfo.cur_floor == 0L && this.mPlayerTowerInfo.wipe_out_state != 1L)
			{
				return true;
			}
			if (this.mPlayerTowerInfo.cur_floor != 0L && this.mPlayerTowerInfo.times > 0L && this.mPlayerTowerInfo.wipe_out_state != 1L)
			{
				return true;
			}
			if (this.mPlayerTowerInfo.wipe_out_state == 2L)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x0005FDDC File Offset: 0x0005DFDC
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

	// Token: 0x04000BE9 RID: 3049
	private tower_info mPlayerTowerInfo;

	// Token: 0x04000BEA RID: 3050
	private bool mIsHaveSpecialReward;
}
