using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000215 RID: 533
public class CurMissionDictionary
{
	// Token: 0x06001290 RID: 4752 RVA: 0x0007940C File Offset: 0x0007760C
	public CurMissionDictionary()
	{
		this.Reset();
	}

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x06001291 RID: 4753 RVA: 0x0007947C File Offset: 0x0007767C
	public List<string> CurTargetNpcIdList
	{
		get
		{
			return this.mCurTargetNpcIdList;
		}
	}

	// Token: 0x170003CF RID: 975
	// (get) Token: 0x06001292 RID: 4754 RVA: 0x00079484 File Offset: 0x00077684
	public List<string> CurCompleteNpcIdList
	{
		get
		{
			return this.mCurCompleteNpcIdList;
		}
	}

	// Token: 0x170003D0 RID: 976
	// (get) Token: 0x06001293 RID: 4755 RVA: 0x0007948C File Offset: 0x0007768C
	public Dictionary<string, CurMission> CurMissionDic
	{
		get
		{
			return this.mCurMissionDic;
		}
	}

	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x06001294 RID: 4756 RVA: 0x00079494 File Offset: 0x00077694
	// (set) Token: 0x06001295 RID: 4757 RVA: 0x0007949C File Offset: 0x0007769C
	public List<long> MissionCompleteFlag
	{
		get
		{
			return this.mMissionCompleteFlag;
		}
		set
		{
			this.mMissionCompleteFlag = value;
		}
	}

	// Token: 0x170003D2 RID: 978
	// (get) Token: 0x06001296 RID: 4758 RVA: 0x000794A8 File Offset: 0x000776A8
	// (set) Token: 0x06001297 RID: 4759 RVA: 0x000794B0 File Offset: 0x000776B0
	public long LastMainMissionId
	{
		get
		{
			return this.mLastMainMissionId;
		}
		set
		{
			this.mLastMainMissionId = value;
		}
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x000794BC File Offset: 0x000776BC
	public void Reset()
	{
		this.mCurMissionDic.Clear();
		this.mROBBERYMission = null;
		this.mEscortMission = null;
		this.mMainMission = null;
		this.mTimeLimitMission = null;
		this.mMissionCompleteFlag.Clear();
		this.mTimeLimitMissionCompleteDic.Clear();
		this.mCurTargetNpcIdList.Clear();
		this.mCurCompleteNpcIdList.Clear();
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x0007951C File Offset: 0x0007771C
	public bool IsMissionAccepted(string missionId)
	{
		return this.mCurMissionDic.ContainsKey(missionId);
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x0007952C File Offset: 0x0007772C
	public bool IsMissionFull()
	{
		return this.mCurMissionDic.Count >= 50;
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x00079544 File Offset: 0x00077744
	public bool SetMissionComplete(string missionId)
	{
		if (string.IsNullOrEmpty(missionId))
		{
			return false;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID.Class == 1)
		{
			this.mLastMainMissionId = (long)int.Parse(missionId);
			return true;
		}
		if (missionDataByID.Class == 2)
		{
			int num = int.Parse(missionId) - this.SideMissionPreIndex;
			int num2 = num / 64;
			int num4;
			long num5;
			if (num2 < this.mMissionCompleteFlag.Count)
			{
				List<long> list2;
				List<long> list = list2 = this.mMissionCompleteFlag;
				int num3 = num4 = num2;
				num5 = list2[num4];
				list[num3] = (num5 | 1L << num % 64);
				return true;
			}
			int num6 = num2 + 1 - this.mMissionCompleteFlag.Count;
			for (int i = 0; i < num6; i++)
			{
				this.mMissionCompleteFlag.Add(0L);
			}
			List<long> list4;
			List<long> list3 = list4 = this.mMissionCompleteFlag;
			int num7 = num4 = num2;
			num5 = list4[num4];
			list3[num7] = (num5 | 1L << num % 64);
			return true;
		}
		else
		{
			if (missionDataByID.Class == 8)
			{
				this.mTimeLimitMissionCompleteDic.Add(missionId, 1);
				TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
				if (timeLimitMissionDataByID != null && missionId.Equals(timeLimitMissionDataByID.MissionList[timeLimitMissionDataByID.MissionList.Count - 1]))
				{
					SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTimeMissionFinish(timeLimitMissionDataByID.MissionList[0]);
					for (int j = 0; j < timeLimitMissionDataByID.MissionList.Count; j++)
					{
						if (this.mTimeLimitMissionCompleteDic.ContainsKey(timeLimitMissionDataByID.MissionList[j]))
						{
							this.mTimeLimitMissionCompleteDic.Remove(timeLimitMissionDataByID.MissionList[j]);
						}
					}
				}
				return true;
			}
			return false;
		}
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x0007970C File Offset: 0x0007790C
	public bool IsMissionCompleted(string missionId)
	{
		if (string.IsNullOrEmpty(missionId))
		{
			return false;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID == null)
		{
			return false;
		}
		if (missionDataByID.Class == 8)
		{
			return this.mTimeLimitMissionCompleteDic.ContainsKey(missionId);
		}
		int num = int.Parse(missionId);
		if (num < this.SideMissionPreIndex)
		{
			if (num >= this.MainMissionMaxIndex)
			{
				return false;
			}
			if (this.LastMainMissionId == -1L)
			{
				return false;
			}
			if (this.LastMainMissionId > 1000L)
			{
				return num >= 1000 && (long)num <= this.mLastMainMissionId;
			}
			return num > 1000 || (long)num <= this.mLastMainMissionId;
		}
		else
		{
			num -= this.SideMissionPreIndex;
			int num2 = num / 64;
			if (num2 < this.mMissionCompleteFlag.Count)
			{
				long num3 = 1L << num % 64 & this.mMissionCompleteFlag[num2];
				return num3 != 0L;
			}
			return false;
		}
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x00079814 File Offset: 0x00077A14
	public bool AddMission(string missionId, long serverTime)
	{
		if (string.IsNullOrEmpty(missionId))
		{
			return false;
		}
		if (this.mCurMissionDic.ContainsKey(missionId))
		{
			return false;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID == null)
		{
			return false;
		}
		CurMission curMission = new CurMission();
		curMission.Reset();
		curMission.MissionId = missionId;
		this.mCurMissionDic.Add(missionId, curMission);
		this.mCurMissionDic[missionId].LastChangeTime = serverTime;
		if (missionDataByID.Class == 4)
		{
			this.mEscortMission = curMission;
		}
		else if (missionDataByID.Class == 5)
		{
			this.mROBBERYMission = curMission;
		}
		else if (missionDataByID.Class == 1)
		{
			this.mMainMission = curMission;
		}
		else if (missionDataByID.Class == 8)
		{
			this.mTimeLimitMission = curMission;
		}
		if (curMission.MissionState == MISSION_STATE.ACCEPTED)
		{
			this.mCurTargetNpcIdList.Add(missionDataByID.Target);
		}
		else if (curMission.MissionState == MISSION_STATE.COMPLETE)
		{
			this.mCurCompleteNpcIdList.Add(missionDataByID.Submit);
		}
		return true;
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x00079920 File Offset: 0x00077B20
	public bool RemoveMission(string missionId)
	{
		if (string.IsNullOrEmpty(missionId))
		{
			return false;
		}
		if (!this.mCurMissionDic.ContainsKey(missionId))
		{
			Debug.Log("not have this mission id :" + missionId);
			return false;
		}
		MISSION_STATE missionState = this.mCurMissionDic[missionId].MissionState;
		bool flag = this.mCurMissionDic.Remove(missionId);
		if (flag)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
			if (missionDataByID.Class == 4)
			{
				this.mEscortMission = null;
			}
			else if (missionDataByID.Class == 5)
			{
				this.mROBBERYMission = null;
			}
			else if (missionDataByID.Class == 1)
			{
				this.mMainMission = null;
			}
			else if (missionDataByID.Class == 8)
			{
				TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
				if (timeLimitMissionDataByID != null)
				{
					for (int i = 0; i < timeLimitMissionDataByID.MissionList.Count; i++)
					{
						if (this.mTimeLimitMissionCompleteDic.ContainsKey(timeLimitMissionDataByID.MissionList[i]))
						{
							this.mTimeLimitMissionCompleteDic.Remove(timeLimitMissionDataByID.MissionList[i]);
						}
					}
				}
				this.mTimeLimitMission = null;
			}
			if (missionState == MISSION_STATE.ACCEPTED)
			{
				this.mCurTargetNpcIdList.Remove(missionDataByID.Target);
			}
			else if (missionState == MISSION_STATE.COMPLETE)
			{
				this.mCurCompleteNpcIdList.Remove(missionDataByID.Submit);
			}
		}
		return flag;
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x00079A88 File Offset: 0x00077C88
	public void SetMissionParam(string missionId, int paramIndex, long val)
	{
		if (!this.mCurMissionDic.ContainsKey(missionId))
		{
			return;
		}
		this.mCurMissionDic[missionId].SetParam(paramIndex, val);
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x00079AB0 File Offset: 0x00077CB0
	public long GetMissionParam(string missionId, int paramIndex)
	{
		if (!this.mCurMissionDic.ContainsKey(missionId))
		{
			return -1L;
		}
		return this.mCurMissionDic[missionId].GetParam(paramIndex);
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x00079AE4 File Offset: 0x00077CE4
	public bool SetMissionState(string missionId, MISSION_STATE state, long changeTime)
	{
		if (!this.mCurMissionDic.ContainsKey(missionId))
		{
			return false;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (this.mCurMissionDic[missionId].MissionState == MISSION_STATE.ACCEPTED)
		{
			this.mCurTargetNpcIdList.Remove(missionDataByID.Target);
		}
		else if (this.mCurMissionDic[missionId].MissionState == MISSION_STATE.COMPLETE)
		{
			this.mCurCompleteNpcIdList.Remove(missionDataByID.Submit);
		}
		if (state == MISSION_STATE.ACCEPTED)
		{
			this.mCurTargetNpcIdList.Add(missionDataByID.Target);
		}
		else if (state == MISSION_STATE.COMPLETE)
		{
			this.mCurCompleteNpcIdList.Add(missionDataByID.Submit);
		}
		if (changeTime > -1L)
		{
			this.mCurMissionDic[missionId].LastChangeTime = changeTime;
		}
		return this.mCurMissionDic[missionId].SetMissionState(state);
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x00079BC4 File Offset: 0x00077DC4
	public long GetMissionChangeTime(string missionId)
	{
		if (!this.mCurMissionDic.ContainsKey(missionId))
		{
			return -1L;
		}
		return this.mCurMissionDic[missionId].LastChangeTime;
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x00079BEC File Offset: 0x00077DEC
	public MISSION_STATE GetMissionState(string missionId)
	{
		if (!this.mCurMissionDic.ContainsKey(missionId))
		{
			return MISSION_STATE.INVALID;
		}
		return this.mCurMissionDic[missionId].MissionState;
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x00079C20 File Offset: 0x00077E20
	public CurMission GetCurMissionByClassType(MISSION_CLASS_TYPE classType)
	{
		if (classType == MISSION_CLASS_TYPE.ESCORT)
		{
			return this.mEscortMission;
		}
		if (classType == MISSION_CLASS_TYPE.ROBBERY)
		{
			return this.mROBBERYMission;
		}
		if (classType == MISSION_CLASS_TYPE.MAIN)
		{
			return this.mMainMission;
		}
		if (classType == MISSION_CLASS_TYPE.TIME_LIMIT)
		{
			return this.mTimeLimitMission;
		}
		return null;
	}

	// Token: 0x040017CD RID: 6093
	public const int MAX_MISSION_NUM = 50;

	// Token: 0x040017CE RID: 6094
	private const int DATA_SIZE = 64;

	// Token: 0x040017CF RID: 6095
	private List<string> mCurTargetNpcIdList = new List<string>();

	// Token: 0x040017D0 RID: 6096
	private List<string> mCurCompleteNpcIdList = new List<string>();

	// Token: 0x040017D1 RID: 6097
	private Dictionary<string, CurMission> mCurMissionDic = new Dictionary<string, CurMission>();

	// Token: 0x040017D2 RID: 6098
	private CurMission mEscortMission;

	// Token: 0x040017D3 RID: 6099
	private CurMission mROBBERYMission;

	// Token: 0x040017D4 RID: 6100
	private CurMission mMainMission;

	// Token: 0x040017D5 RID: 6101
	private CurMission mTimeLimitMission;

	// Token: 0x040017D6 RID: 6102
	private List<long> mMissionCompleteFlag = new List<long>();

	// Token: 0x040017D7 RID: 6103
	private Dictionary<string, int> mTimeLimitMissionCompleteDic = new Dictionary<string, int>();

	// Token: 0x040017D8 RID: 6104
	private int SideMissionPreIndex = 40000;

	// Token: 0x040017D9 RID: 6105
	private int MainMissionMaxIndex = 2000;

	// Token: 0x040017DA RID: 6106
	private long mLastMainMissionId = -1L;
}
