using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000148 RID: 328
public class ActivityObjData
{
	// Token: 0x06000EBA RID: 3770 RVA: 0x00060EBC File Offset: 0x0005F0BC
	public ActivityObjData(ActivityMapData actMapData)
	{
		this.ActMapData = actMapData;
		this.IsMissionObj = false;
		this.Position = this.ActMapData.Position;
		if (actMapData.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
		{
			this.MisData = DataManager.GetMissionDataByID(actMapData.ActivityID);
		}
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x00060F14 File Offset: 0x0005F114
	public ActivityObjData(string missionId, MISSION_STATE misState, Vector3 pos)
	{
		this.IsMissionObj = true;
		this.MissionId = missionId;
		this.MissionState = misState;
		this.Position = pos;
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID != null && missionDataByID.Class == 1 && missionDataByID.MissionLogicType == MISSION_LOGICTYPE.CAPTURE_SUCCESS)
		{
			DominData dominDataByID = DataManager.GetDominDataByID(missionDataByID.LogicID);
			if (dominDataByID != null)
			{
				this.Position = dominDataByID.GetPos();
			}
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00060F90 File Offset: 0x0005F190
	// (set) Token: 0x06000EBD RID: 3773 RVA: 0x00060FB0 File Offset: 0x0005F1B0
	public string MissionId
	{
		get
		{
			if (this.MisData == null)
			{
				return string.Empty;
			}
			return this.MisData.ID;
		}
		set
		{
			this.MisData = DataManager.GetMissionDataByID(value);
		}
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x00060FC0 File Offset: 0x0005F1C0
	public string GetIcon()
	{
		if (this.IsMissionObj)
		{
			return GameDefine.MAP_ACTIVITY_MISSION_ICON[this.MisData.Class];
		}
		if (this.ActMapData.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
		{
			return GameDefine.MAP_ACTIVITY_MISSION_ICON[this.MisData.Class];
		}
		if (this.ActMapData.ActivityType != GameDefine.ACTIVITY_TYPE.DOMIN)
		{
			return this.ActMapData.Icon;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (!playerData.Domin_InfoDic.ContainsKey(this.ActMapData.ActivityID))
		{
			return string.Empty;
		}
		domin_info domin_info = playerData.Domin_InfoDic[this.ActMapData.ActivityID];
		if (domin_info.state == 0L && domin_info.HasServerId && playerData.Domin_CharacterDic.ContainsKey(domin_info.serverId))
		{
			return GameDefine.Player_Icon_Small_Pic[(int)(checked((IntPtr)playerData.Domin_CharacterDic[domin_info.serverId].general.profession))];
		}
		return GameDefine.Player_Icon_Small_Pic[(int)playerData.Profession];
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x000610CC File Offset: 0x0005F2CC
	public string GetStateIcon()
	{
		if (this.IsMissionObj)
		{
			return GameDefine.GetMissionStateIcon(this.MissionState);
		}
		if (this.ActMapData.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION)
		{
			return GameDefine.GetMissionStateIcon(MISSION_STATE.INVALID);
		}
		if (this.ActMapData.IsUnlock)
		{
			return string.Empty;
		}
		return "CZ_effect_suo";
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x00061124 File Offset: 0x0005F324
	public bool IsMission()
	{
		return this.IsMissionObj || this.ActMapData.ActivityType == GameDefine.ACTIVITY_TYPE.MISSION;
	}

	// Token: 0x04000C38 RID: 3128
	public ActivityMapData ActMapData;

	// Token: 0x04000C39 RID: 3129
	public MissionData MisData;

	// Token: 0x04000C3A RID: 3130
	public Vector3 Position;

	// Token: 0x04000C3B RID: 3131
	public bool IsMissionObj;

	// Token: 0x04000C3C RID: 3132
	public MISSION_STATE MissionState = MISSION_STATE.INVALID;
}
