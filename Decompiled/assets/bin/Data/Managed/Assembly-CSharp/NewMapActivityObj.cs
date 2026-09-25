using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A2C RID: 2604
public class NewMapActivityObj : MonoBehaviour
{
	// Token: 0x17000FCA RID: 4042
	// (get) Token: 0x06004BD3 RID: 19411 RVA: 0x00197A2C File Offset: 0x00195C2C
	public bool IsMissionObj
	{
		get
		{
			return this.isMissionObj;
		}
	}

	// Token: 0x06004BD4 RID: 19412 RVA: 0x00197A34 File Offset: 0x00195C34
	public void Reset(ActivityMapData actMapData)
	{
		this.isMissionObj = false;
		this.mCurActivityMapData = actMapData;
		if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.DOMIN)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			if (!playerData.Domin_InfoDic.ContainsKey(actMapData.ActivityID))
			{
				NGUITools.SetActive(base.gameObject, false);
				return;
			}
			domin_info domin_info = playerData.Domin_InfoDic[actMapData.ActivityID];
			if (domin_info.state == 0L && domin_info.HasServerId && playerData.Domin_CharacterDic.ContainsKey(domin_info.serverId))
			{
				this.IconPic.spriteName = GameDefine.Player_Icon_Small_Pic[(int)(checked((IntPtr)playerData.Domin_CharacterDic[domin_info.serverId].general.profession))];
			}
			else
			{
				this.IconPic.spriteName = GameDefine.Player_Icon_Small_Pic[(int)playerData.Profession];
			}
		}
		else
		{
			this.IconPic.spriteName = actMapData.Icon;
		}
		this.mMissionIdList = null;
		this.isUnLock = this.mCurActivityMapData.IsUnlock;
		this.IconPic.MakePixelPerfect();
		if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
		{
			NGUITools.SetActive(this.LockPic.gameObject, true);
			this.LockPic.spriteName = GameDefine.GetMissionStateIcon(MISSION_STATE.INVALID);
			this.LockPic.pivot = UIWidget.Pivot.Left;
			this.LockPic.MakePixelPerfect();
			this.LockPic.width = (int)((float)this.LockPic.width * 0.6f);
			this.LockPic.height = (int)((float)this.LockPic.height * 0.6f);
			this.LockPic.transform.localPosition = new Vector3(10f, 0f, 0f);
			this.LockPic.color = Color.white;
		}
		else if (this.isUnLock)
		{
			NGUITools.SetActive(this.LockPic.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.LockPic.gameObject, true);
			this.LockPic.spriteName = "CZ_effect_suo";
			this.LockPic.pivot = UIWidget.Pivot.Center;
			this.LockPic.transform.localPosition = Vector3.zero;
			this.LockPic.color = Color.red;
			this.LockPic.width = 43;
			this.LockPic.height = 43;
		}
		this.SetNormalState();
		base.transform.localScale = Vector3.one * 0.5f;
	}

	// Token: 0x06004BD5 RID: 19413 RVA: 0x00197CC4 File Offset: 0x00195EC4
	public void Reset(MissionData missionData, List<string> missionIdList)
	{
		this.isMissionObj = true;
		this.mCurMissionData = missionData;
		this.mMissionIdList = missionIdList;
		this.IconPic.spriteName = GameDefine.MAP_ACTIVITY_MISSION_ICON[missionData.Class];
		this.IconPic.MakePixelPerfect();
		NGUITools.SetActive(this.LockPic.gameObject, true);
		MISSION_STATE missionState = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetMissionState(missionData.ID);
		this.LockPic.spriteName = GameDefine.GetMissionStateIcon(missionState);
		this.LockPic.pivot = UIWidget.Pivot.Left;
		this.LockPic.MakePixelPerfect();
		this.LockPic.width = (int)((float)this.LockPic.width * 0.6f);
		this.LockPic.height = (int)((float)this.LockPic.height * 0.6f);
		this.LockPic.transform.localPosition = new Vector3(10f, 0f, 0f);
		this.LockPic.color = Color.white;
		this.SetNormalState();
		base.transform.localScale = Vector3.one * 0.5f;
	}

	// Token: 0x06004BD6 RID: 19414 RVA: 0x00197DE8 File Offset: 0x00195FE8
	public void OnClickBtn()
	{
		if (UnityVersionUtil.IsActive(this.ActiveObj.gameObject))
		{
			return;
		}
		if (this.isUnLock)
		{
			NewMapUIRootLogic instance = SingletonUnity<NewMapUIRootLogic>.Instance;
			if (instance != null && UnityVersionUtil.IsActive(instance.gameObject) && instance.CurMapInfo.ID.Equals(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.ID))
			{
				instance.OnClickLocalMap();
			}
		}
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (this.mCurActivityMapData != null)
		{
			if (!this.mCurActivityMapData.CheckCanGoTo())
			{
				return;
			}
			if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
			{
				if (SingletonUnity<NewMissionUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMissionUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<NewMissionUIRootLogic>.Instance.ClickTargetMission(this.mCurActivityMapData.ActivityID);
				}
				else
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickMissionBtn(this.mCurActivityMapData.ActivityID, SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID);
				}
			}
			else if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.DAILY_COPY || this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.SEX_MINI)
			{
				if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.SEX_MINI)
				{
					this.mCurActivityMapData.SubType = 27;
				}
				if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
				{
					if (this.mCurActivityMapData.IsNeedDailyActid())
					{
						SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ClickTargetDailyCopy((MAPTYPE)this.mCurActivityMapData.SubType, this.mCurActivityMapData.ActivityID);
					}
					else
					{
						SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ClickTargetDailyCopy((MAPTYPE)this.mCurActivityMapData.SubType);
					}
				}
				else if (this.mCurActivityMapData.IsNeedDailyActid())
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn((MAPTYPE)this.mCurActivityMapData.SubType, this.mCurActivityMapData.ActivityID, SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID, GameDefine.ACTIVITY_TYPE.INVALID);
				}
				else
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn((MAPTYPE)this.mCurActivityMapData.SubType, null, SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID, GameDefine.ACTIVITY_TYPE.INVALID);
				}
			}
			else if (this.mCurActivityMapData.IsInTimeActivityUI())
			{
				if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
				{
					if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.WILD_BOSS)
					{
						SingletonUnity<NewDailyActivityUIRootLogic>.Instance.WildBossLineList[0].ClickTargetBtn();
					}
					else
					{
						if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
						{
							SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjDisable();
						}
						SingletonUnity<NewDailyActivityUIRootLogic>.Instance.ClickTargetType(this.mCurActivityMapData.ActivityType);
						this.SetActiveState();
					}
				}
				else
				{
					if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
					{
						SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjDisable();
					}
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(this.mCurActivityMapData.ActivityType, SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID, true);
					this.SetActiveState();
				}
			}
			else if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BOSS || this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.GUILD_BATTLE)
			{
				if (SingletonUnity<NewDailyActivityUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyActivityUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<NewDailyActivityUIRootLogic>.Instance.OnClickTargetType(this.mCurActivityMapData.ActivityType);
				}
				else
				{
					if (SingletonUnity<NewMapUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMapUIRootLogic>.Instance.gameObject))
					{
						SingletonUnity<NewMapUIRootLogic>.Instance.SetActivityObjDisable();
					}
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(this.mCurActivityMapData.ActivityType, SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID, true);
					this.SetActiveState();
				}
			}
			else if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.TOWER)
			{
				if (SingletonUnity<NewDailyCopyUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewDailyCopyUIRootLogic>.Instance.gameObject))
				{
					SingletonUnity<NewDailyCopyUIRootLogic>.Instance.ClickTargetType(GameDefine.ACTIVITY_TYPE.TOWER);
				}
				else
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.INVALID, null, SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID, GameDefine.ACTIVITY_TYPE.TOWER);
				}
			}
			else if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.RANKPVP)
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickRankBtn();
			}
			else if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.DOMIN)
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDominBtn(this.mCurActivityMapData.ActivityID);
			}
		}
		else if (this.mCurMissionData != null)
		{
			if (SingletonUnity<NewMissionUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMissionUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<NewMissionUIRootLogic>.Instance.ClickTargetMission(this.mCurMissionData.ID);
			}
			else
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickMissionBtn(this.mCurMissionData.ID, SingletonUnity<NewMapUIRootLogic>.Instance.CurMapInfo.ID);
			}
		}
	}

	// Token: 0x06004BD7 RID: 19415 RVA: 0x001982E4 File Offset: 0x001964E4
	public void SetActiveState()
	{
		this.IconPic.color = Color.white;
		NGUITools.SetActive(this.ActiveObj, true);
		this.IconPic.depth = 81;
		this.LockPic.depth = 82;
	}

	// Token: 0x06004BD8 RID: 19416 RVA: 0x00198328 File Offset: 0x00196528
	public void SetDisActiveState()
	{
		this.IconPic.color = Color.gray;
		this.IconPic.alpha = 0.7f;
		NGUITools.SetActive(this.ActiveObj, false);
		this.IconPic.depth = 71;
		this.LockPic.depth = 72;
	}

	// Token: 0x06004BD9 RID: 19417 RVA: 0x0019837C File Offset: 0x0019657C
	public void SetNormalState()
	{
		this.IconPic.color = Color.white;
		this.IconPic.alpha = 0.7f;
		NGUITools.SetActive(this.ActiveObj, false);
		this.IconPic.depth = 71;
		this.LockPic.depth = 72;
	}

	// Token: 0x06004BDA RID: 19418 RVA: 0x001983D0 File Offset: 0x001965D0
	public void UpdateSelection(int actType, int subType, string actId)
	{
		if (!this.isMissionObj)
		{
			if (this.mCurActivityMapData.Type == actType && this.mCurActivityMapData.SubType == subType)
			{
				if (string.IsNullOrEmpty(actId))
				{
					this.SetActiveState();
				}
				else if (this.mCurActivityMapData.ActivityID.Equals(actId))
				{
					this.SetActiveState();
				}
				else
				{
					this.SetDisActiveState();
				}
			}
			else
			{
				this.SetDisActiveState();
			}
		}
		else
		{
			this.SetDisActiveState();
		}
	}

	// Token: 0x06004BDB RID: 19419 RVA: 0x00198460 File Offset: 0x00196660
	public void UpdateSelection(string actId, bool isMission)
	{
		if (isMission)
		{
			if (this.isMissionObj)
			{
				if (this.mMissionIdList != null)
				{
					if (this.mMissionIdList.Contains(actId))
					{
						this.SetActiveState();
					}
					else
					{
						this.SetDisActiveState();
					}
				}
				else if (this.mCurMissionData.ID.Equals(actId))
				{
					this.SetActiveState();
				}
				else
				{
					this.SetDisActiveState();
				}
			}
			else if (this.mCurActivityMapData.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
			{
				if (this.mCurActivityMapData.ActivityID.Equals(actId))
				{
					this.SetActiveState();
				}
				else
				{
					this.SetDisActiveState();
				}
			}
			else
			{
				this.SetDisActiveState();
			}
		}
		else if (this.isMissionObj)
		{
			this.SetDisActiveState();
		}
		else if (this.mCurActivityMapData.ActivityID.Equals(actId))
		{
			this.SetActiveState();
		}
		else
		{
			this.SetDisActiveState();
		}
	}

	// Token: 0x0400397C RID: 14716
	public UISprite IconPic;

	// Token: 0x0400397D RID: 14717
	public UISprite LockPic;

	// Token: 0x0400397E RID: 14718
	public GameObject ActiveObj;

	// Token: 0x0400397F RID: 14719
	private ActivityMapData mCurActivityMapData;

	// Token: 0x04003980 RID: 14720
	private bool isUnLock;

	// Token: 0x04003981 RID: 14721
	private MissionData mCurMissionData;

	// Token: 0x04003982 RID: 14722
	private List<string> mMissionIdList;

	// Token: 0x04003983 RID: 14723
	private bool isMissionObj;
}
