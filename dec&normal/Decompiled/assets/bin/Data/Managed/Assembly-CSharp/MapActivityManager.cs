using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EE RID: 238
public class MapActivityManager : MonoBehaviour
{
	// Token: 0x0600079F RID: 1951 RVA: 0x00034DFC File Offset: 0x00032FFC
	public void Reset()
	{
		this.CurActivityMapDataList.Clear();
		this.EnableDic.Clear();
		this.CurShowList.Clear();
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00034E20 File Offset: 0x00033020
	public void InitMapInfo(List<ActivityMapData> curdatalist, Transform parenttra)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.CurActivityMapDataList.Clear();
		this.EnableDic.Clear();
		this.CurShowList.Clear();
		for (int i = 0; i < curdatalist.Count; i++)
		{
			if (curdatalist[i].IsVisible)
			{
				this.CurActivityMapDataList.Add(curdatalist[i]);
			}
		}
		this.ParentTra = parenttra;
		if (this.CurActivityMapDataList == null || this.CurActivityMapDataList.Count == 0)
		{
			return;
		}
		for (int j = 0; j < this.CurActivityMapDataList.Count; j++)
		{
			if (!this.CheckIsHaveAndEnbale(this.CurActivityMapDataList[j]))
			{
				this.InitObj(this.CurActivityMapDataList[j]);
			}
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x00034F00 File Offset: 0x00033100
	public void RefershMapInfo(List<ActivityMapData> curdatalist, Transform parenttra)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.CurActivityMapDataList.Clear();
		for (int i = 0; i < curdatalist.Count; i++)
		{
			if (curdatalist[i].IsVisible)
			{
				this.CurActivityMapDataList.Add(curdatalist[i]);
			}
		}
		this.ParentTra = parenttra;
		if (this.CurActivityMapDataList == null || this.CurActivityMapDataList.Count == 0)
		{
			return;
		}
		for (int j = 0; j < this.CurActivityMapDataList.Count; j++)
		{
			if (!this.CheckIsHaveAndEnbale(this.CurActivityMapDataList[j]))
			{
				this.InitObj(this.CurActivityMapDataList[j]);
			}
		}
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00034FCC File Offset: 0x000331CC
	public void AcceptMapActivity(string missionid)
	{
		List<ActivityPoint> list = new List<ActivityPoint>(this.EnableDic.Values);
		List<string> list2 = new List<string>();
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionid);
		bool flag = false;
		if (missionDataByID.Class == 8)
		{
			flag = true;
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].CurActData.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
			{
				if (flag)
				{
					MissionData missionDataByID2 = DataManager.GetMissionDataByID(list[i].CurActData.ActivityID);
					if (missionDataByID2.Class == 8)
					{
						list2.Add(list[i].ID);
					}
				}
				else if (list[i].CurActData.ActivityID.Equals(missionid))
				{
					list2.Add(list[i].ID);
				}
			}
		}
		for (int j = 0; j < list2.Count; j++)
		{
			string text = list2[j];
			if (!string.IsNullOrEmpty(text))
			{
				if (this.EnableDic.ContainsKey(text))
				{
					Object.Destroy(this.EnableDic[text].gameObject);
					this.EnableDic.Remove(text);
				}
				this.CurShowList.Remove(text);
			}
		}
		if (list2.Count > 0 && !UIManager.IsUnlockTutorialEnable())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.UnlockFunctionRoot, delegate
			{
				SingletonUnity<UnlockFunctionRootLogic>.Instance.ShowAcceptMissionEffect(missionid);
			}, null);
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00035174 File Offset: 0x00033374
	public void UpdateTimeActivityMapFlag()
	{
		this.UpdateActivityMapFlag(GameDefine.ACTIVITY_TYPE.ESCORT);
		this.UpdateActivityMapFlag(GameDefine.ACTIVITY_TYPE.ATTACK_ESCORT);
		this.UpdateActivityMapFlag(GameDefine.ACTIVITY_TYPE.CITY_DANCE);
		this.UpdateActivityMapFlag(GameDefine.ACTIVITY_TYPE.BAR_FIGHT);
		this.UpdateActivityMapFlag(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE);
		this.UpdateActivityMapFlag(GameDefine.ACTIVITY_TYPE.WILD_BOSS);
		this.UpdateActivityMapFlag(GameDefine.ACTIVITY_TYPE.GUILD_BOSS);
		this.UpdateActivityMapFlag(GameDefine.ACTIVITY_TYPE.GUILD_BATTLE);
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x000351BC File Offset: 0x000333BC
	public void UpdateActivityMapFlag(GameDefine.ACTIVITY_TYPE type)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.ResetActivityTips(playerData.ActivityData.IsTimeActivityCanPlay(type), type);
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x000351E8 File Offset: 0x000333E8
	public void ResetActivityTips(bool isOpen, GameDefine.ACTIVITY_TYPE type = GameDefine.ACTIVITY_TYPE.INVALID)
	{
		List<ActivityPoint> list = new List<ActivityPoint>(this.EnableDic.Values);
		List<ActivityPoint> list2 = new List<ActivityPoint>();
		string text = string.Empty;
		if (list != null && list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].CurActData.ActivityType == type)
				{
					list2.Add(list[i]);
				}
			}
		}
		for (int j = 0; j < list2.Count; j++)
		{
			ActivityPoint activityPoint = list2[j];
			text = activityPoint.CurActData.ID;
			if (activityPoint != null && activityPoint.IsEnable != isOpen)
			{
				ActivityMapData curActData = activityPoint.CurActData;
				if (this.EnableDic.ContainsKey(text))
				{
					Object.Destroy(this.EnableDic[text].gameObject);
					this.EnableDic.Remove(text);
				}
				this.CurShowList.Remove(text);
				this.InitObj(curActData);
			}
		}
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x00035300 File Offset: 0x00033500
	public bool CheckIsHaveAndEnbale(ActivityMapData needdata)
	{
		if (!this.EnableDic.ContainsKey(needdata.ID))
		{
			return false;
		}
		ActivityPoint activityPoint = this.EnableDic[needdata.ID];
		bool flag = true;
		if (needdata.IsUnlock)
		{
			if (needdata.IsTimeActivity())
			{
				PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
				if (!playerData.ActivityData.IsTimeActivityCanPlay(needdata.ActivityType))
				{
					flag = false;
				}
			}
		}
		else
		{
			flag = false;
		}
		if (activityPoint != null && activityPoint.IsEnable == flag)
		{
			return true;
		}
		Object.Destroy(this.EnableDic[needdata.ID].gameObject);
		this.EnableDic.Remove(needdata.ID);
		this.CurShowList.Remove(needdata.ID);
		return false;
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x000353D4 File Offset: 0x000335D4
	public void InitObj(ActivityMapData initdata)
	{
		GameObject gameObject = null;
		bool isenable = true;
		if (initdata.Color != 0 || initdata.IsShowDoorFlag())
		{
			if (initdata.IsShowDoorFlag())
			{
				gameObject = (ResourcesManager.LoadAndInstantiate("Items/City_jinRu") as GameObject);
			}
			else if (initdata.IsUnlock)
			{
				if (initdata.IsTimeActivity())
				{
					PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
					if (playerData.ActivityData.IsTimeActivityCanPlay(initdata.ActivityType))
					{
						gameObject = (ResourcesManager.LoadAndInstantiate("Items/" + GameDefine.ACTIVITYPOINTNAME[initdata.Color]) as GameObject);
					}
					else
					{
						isenable = false;
						gameObject = (ResourcesManager.LoadAndInstantiate("Items/Activity_Point_6") as GameObject);
					}
				}
				else
				{
					gameObject = (ResourcesManager.LoadAndInstantiate("Items/" + GameDefine.ACTIVITYPOINTNAME[initdata.Color]) as GameObject);
				}
			}
			else
			{
				isenable = false;
				gameObject = (ResourcesManager.LoadAndInstantiate("Items/Activity_Point_6") as GameObject);
			}
		}
		if (gameObject != null)
		{
			ActivityPoint component = gameObject.GetComponent<ActivityPoint>();
			if (component != null)
			{
				component.Reset(initdata, this.ParentTra, isenable);
				this.EnableDic.Add(initdata.ID, component);
				this.CurShowList.Add(initdata.ID);
			}
		}
	}

	// Token: 0x04000667 RID: 1639
	private List<ActivityMapData> CurActivityMapDataList = new List<ActivityMapData>();

	// Token: 0x04000668 RID: 1640
	public Dictionary<string, ActivityPoint> EnableDic = new Dictionary<string, ActivityPoint>();

	// Token: 0x04000669 RID: 1641
	private List<string> CurShowList = new List<string>();

	// Token: 0x0400066A RID: 1642
	private Transform ParentTra;
}
