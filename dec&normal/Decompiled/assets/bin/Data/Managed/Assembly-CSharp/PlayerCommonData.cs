using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class PlayerCommonData
{
	// Token: 0x06000D55 RID: 3413 RVA: 0x0005BF40 File Offset: 0x0005A140
	public PlayerCommonData()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.CheckLevelUpUnlockFunction));
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0005C000 File Offset: 0x0005A200
	// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0005C008 File Offset: 0x0005A208
	public Dictionary<string, function_info> TutorialFunctionStateDic
	{
		get
		{
			return this.mTutorialFunctionStateDic;
		}
		set
		{
			this.mTutorialFunctionStateDic = value;
		}
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0005C014 File Offset: 0x0005A214
	// (set) Token: 0x06000D5A RID: 3418 RVA: 0x0005C01C File Offset: 0x0005A21C
	public List<string> NeedShowUnlockIdList
	{
		get
		{
			return this.mNeedShowUnlockIdList;
		}
		set
		{
			this.mNeedShowUnlockIdList = value;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0005C028 File Offset: 0x0005A228
	// (set) Token: 0x06000D5C RID: 3420 RVA: 0x0005C030 File Offset: 0x0005A230
	public List<string> MenuTabBtnTipIdList
	{
		get
		{
			return this.mMenuTabBtnTipIdList;
		}
		set
		{
			this.mMenuTabBtnTipIdList = value;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0005C03C File Offset: 0x0005A23C
	public Dictionary<string, bool> FunctionUnlockState
	{
		get
		{
			return this.mFunctionUnlockState;
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0005C044 File Offset: 0x0005A244
	// (set) Token: 0x06000D5F RID: 3423 RVA: 0x0005C04C File Offset: 0x0005A24C
	public long ServerTime
	{
		get
		{
			return this.mServerTime;
		}
		set
		{
			this.mServerTime = value;
			this.mLocalTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0005C060 File Offset: 0x0005A260
	// (set) Token: 0x06000D61 RID: 3425 RVA: 0x0005C068 File Offset: 0x0005A268
	public long CommondSeed
	{
		get
		{
			return this.commondSeed;
		}
		set
		{
			this.commondSeed = value;
		}
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0005C074 File Offset: 0x0005A274
	private static uint MyRandom()
	{
		PlayerCommonData.randomSeed = PlayerCommonData.randomSeed * 1103515245U + 12345U;
		return PlayerCommonData.randomSeed << 16 | (PlayerCommonData.randomSeed >> 16 & 65535U);
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x0005C0B0 File Offset: 0x0005A2B0
	public static void InitRandom(long seed)
	{
		PlayerCommonData.sendIndex = 0L;
		PlayerCommonData.randomSeed = (uint)seed;
		PlayerCommonData.randomArray.Clear();
		for (int i = 0; i < 1000; i++)
		{
			uint num = PlayerCommonData.MyRandom();
			PlayerCommonData.randomArray.Add((int)(num % 100U));
		}
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0005C100 File Offset: 0x0005A300
	public static int GetRandom(GameDefine.OBJ_TYPE type)
	{
		if (type == GameDefine.OBJ_TYPE.OBJ_MAIN_PLAYER && !GameSettingData.IsLowPhone)
		{
			int num = (int)(PlayerCommonData.sendIndex % (long)PlayerCommonData.randomArray.Count);
			PlayerCommonData.sendIndex += 1L;
			return PlayerCommonData.randomArray[num];
		}
		return Random.Range(0, 100);
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0005C154 File Offset: 0x0005A354
	// (set) Token: 0x06000D66 RID: 3430 RVA: 0x0005C15C File Offset: 0x0005A35C
	public int ServerLevel
	{
		get
		{
			return this.mServerLevel;
		}
		set
		{
			this.mServerLevel = value;
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0005C168 File Offset: 0x0005A368
	// (set) Token: 0x06000D68 RID: 3432 RVA: 0x0005C170 File Offset: 0x0005A370
	public long ServerLevelSealTime
	{
		get
		{
			return this.mServerLevelSealTime;
		}
		set
		{
			this.mServerLevelSealTime = value;
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0005C17C File Offset: 0x0005A37C
	// (set) Token: 0x06000D6A RID: 3434 RVA: 0x0005C184 File Offset: 0x0005A384
	public int ResetTime
	{
		get
		{
			return this.mResetTime;
		}
		set
		{
			this.mResetTime = value;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0005C190 File Offset: 0x0005A390
	// (set) Token: 0x06000D6C RID: 3436 RVA: 0x0005C198 File Offset: 0x0005A398
	public int RankPvpResetTime
	{
		get
		{
			return this.mRankPvpResetTime;
		}
		set
		{
			this.mRankPvpResetTime = value;
		}
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0005C1A4 File Offset: 0x0005A3A4
	public long GetCurServerTime()
	{
		return this.mServerTime + (long)(Time.realtimeSinceStartup - this.mLocalTime);
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0005C1BC File Offset: 0x0005A3BC
	public static long GetServerTime()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime();
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0005C1D0 File Offset: 0x0005A3D0
	// (set) Token: 0x06000D70 RID: 3440 RVA: 0x0005C1D8 File Offset: 0x0005A3D8
	public long TimeOffset
	{
		get
		{
			return this.mTimeOffset;
		}
		set
		{
			this.mTimeOffset = value;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0005C1E4 File Offset: 0x0005A3E4
	// (set) Token: 0x06000D72 RID: 3442 RVA: 0x0005C1EC File Offset: 0x0005A3EC
	public long DailyMissionRefreshTime
	{
		get
		{
			return this.mDailyMissionRefreshTime;
		}
		set
		{
			this.mDailyMissionRefreshTime = value;
			this.ResetTime = (int)value * 3600;
		}
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0005C204 File Offset: 0x0005A404
	// (set) Token: 0x06000D74 RID: 3444 RVA: 0x0005C20C File Offset: 0x0005A40C
	public long CreateTime
	{
		get
		{
			return this.mCreateTime;
		}
		set
		{
			this.mCreateTime = value;
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0005C218 File Offset: 0x0005A418
	public long CreateDay
	{
		get
		{
			if (this.mCreateDay == -1L)
			{
				this.mCreateDay = (this.ServerTime - this.mCreateTime) / 86400L;
			}
			return this.mCreateDay;
		}
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0005C248 File Offset: 0x0005A448
	// (set) Token: 0x06000D77 RID: 3447 RVA: 0x0005C25C File Offset: 0x0005A45C
	public bool Big_PackFlag
	{
		get
		{
			return this.mBig_PackFlag == 1L;
		}
		set
		{
			this.mBig_PackFlag = 0L;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0005C268 File Offset: 0x0005A468
	// (set) Token: 0x06000D79 RID: 3449 RVA: 0x0005C27C File Offset: 0x0005A47C
	public bool First_PackFlag
	{
		get
		{
			return this.mFirst_PackFlag == 1L;
		}
		set
		{
			this.mFirst_PackFlag = 0L;
		}
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0005C288 File Offset: 0x0005A488
	// (set) Token: 0x06000D7B RID: 3451 RVA: 0x0005C290 File Offset: 0x0005A490
	public long Push
	{
		get
		{
			return this.mPush;
		}
		set
		{
			this.mPush = value;
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0005C29C File Offset: 0x0005A49C
	// (set) Token: 0x06000D7D RID: 3453 RVA: 0x0005C2A4 File Offset: 0x0005A4A4
	public bool PushShowFlag
	{
		get
		{
			return this.mPushShowFlag;
		}
		set
		{
			this.mPushShowFlag = value;
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0005C2B0 File Offset: 0x0005A4B0
	// (set) Token: 0x06000D7F RID: 3455 RVA: 0x0005C2B8 File Offset: 0x0005A4B8
	public long ChampionGuildId
	{
		get
		{
			return this.mChampionGuildId;
		}
		set
		{
			this.mChampionGuildId = value;
		}
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0005C2C4 File Offset: 0x0005A4C4
	public bool IsChampionGuild(long guildId)
	{
		return guildId == this.mChampionGuildId;
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0005C2D0 File Offset: 0x0005A4D0
	public void ClearData()
	{
		this.mFunctionUnlockState.Clear();
		this.mNeedShowUnlockIdList.Clear();
		this.mMenuTabBtnTipIdList.Clear();
		this.mTutorialFunctionStateDic.Clear();
		this.ServerTime = -1L;
		this.TimeOffset = -1L;
		this.DailyMissionRefreshTime = -1L;
		this.mPushShowFlag = false;
		PlayerCommonData.sendIndex = 0L;
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0005C330 File Offset: 0x0005A530
	public void ResetFunctionUnlockData()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		List<FunctionData> functionDataList = DataManager.GetFunctionDataList();
		functionDataList.Sort(delegate(FunctionData x, FunctionData y)
		{
			if (x.Condition != y.Condition)
			{
				return x.Condition - y.Condition;
			}
			return x.Index - y.Index;
		});
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		for (int i = 0; i < functionDataList.Count; i++)
		{
			if (!this.IsTutorialCanShow(functionDataList[i].ID))
			{
				this.SetFunctionUnlockState(functionDataList[i].ID, true);
			}
			else if (functionDataList[i].Class == 0)
			{
				this.SetFunctionUnlockState(functionDataList[i].ID, true);
			}
			else if (functionDataList[i].Class == 1)
			{
				if (functionDataList[i].Condition <= playerData.Level)
				{
					this.SetFunctionUnlockState(functionDataList[i].ID, true);
				}
				else
				{
					this.SetFunctionUnlockState(functionDataList[i].ID, false);
				}
			}
			else if (functionDataList[i].Class == 2)
			{
				if ((long)functionDataList[i].Condition <= playerCommonData.CreateDay)
				{
					this.SetFunctionUnlockState(functionDataList[i].ID, true);
				}
				else
				{
					this.SetFunctionUnlockState(functionDataList[i].ID, false);
				}
			}
			else
			{
				this.SetFunctionUnlockState(functionDataList[i].ID, false);
			}
			if (functionDataList[i].FirstOpen == 1 && this.mFunctionUnlockState.ContainsKey(functionDataList[i].ID) && this.mFunctionUnlockState[functionDataList[i].ID])
			{
				if (this.mTutorialFunctionStateDic == null || !this.mTutorialFunctionStateDic.ContainsKey(functionDataList[i].ID) || this.mTutorialFunctionStateDic[functionDataList[i].ID].state == 0L)
				{
					if (functionDataList[i].UnlockType == 2)
					{
						this.mMenuTabBtnTipIdList.Add(functionDataList[i].ID);
					}
					else
					{
						this.mNeedShowUnlockIdList.Add(functionDataList[i].ID);
					}
				}
			}
		}
		List<string> list = new List<string>();
		for (int j = 0; j < functionDataList.Count; j++)
		{
			if (!this.mFunctionUnlockState[functionDataList[j].ID] && functionDataList[j].SideMissionIdList != null && functionDataList[j].SideMissionIdList.Length > 0)
			{
				for (int k = 0; k < functionDataList[j].SideMissionIdList.Length; k++)
				{
					list.Add(functionDataList[j].SideMissionIdList[k]);
				}
			}
		}
		missionManager.SetFunctionMissionIdList(list);
		if (playerData.IsHaveGuild())
		{
			this.SetFunctionUnlockState(3017.ToString(), true);
		}
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0005C680 File Offset: 0x0005A880
	public void CheckTitleLevelFunction()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.CurTitleLevel > 0)
		{
			this.SetFunctionUnlockState(3011.ToString(), true);
			this.SetFunctionUnlockState(4031.ToString(), true);
		}
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0005C6D0 File Offset: 0x0005A8D0
	public void CheckUnlockSideMission()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		List<FunctionData> functionDataList = DataManager.GetFunctionDataList();
		functionDataList.Sort(delegate(FunctionData x, FunctionData y)
		{
			if (x.Condition != y.Condition)
			{
				return x.Condition - y.Condition;
			}
			return x.Index - y.Index;
		});
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		for (int i = 0; i < functionDataList.Count; i++)
		{
			if (functionDataList[i].Class == 1 && functionDataList[i].Condition <= playerData.Level && functionDataList[i].SideMissionIdList != null)
			{
				for (int j = 0; j < functionDataList[i].SideMissionIdList.Length; j++)
				{
					if (missionManager.IsMissionAcceptable(functionDataList[i].SideMissionIdList[j]))
					{
						missionManager.AcceptMission(functionDataList[i].SideMissionIdList[j]);
					}
				}
			}
		}
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0005C7DC File Offset: 0x0005A9DC
	public bool IsFunctionUnlock(FUNCTION_TYPE type)
	{
		if (type == FUNCTION_TYPE.COUNT)
		{
			return true;
		}
		int num = (int)type;
		string text = num.ToString();
		FunctionData functionDataById = DataManager.GetFunctionDataById(text);
		return this.CanCheckFunction(functionDataById) && this.mFunctionUnlockState.ContainsKey(text) && this.mFunctionUnlockState[text];
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0005C834 File Offset: 0x0005AA34
	public bool CanCheckFunction(FunctionData curfunctiondata)
	{
		return curfunctiondata != null && (curfunctiondata.IsDownload != 1 || SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload);
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0005C85C File Offset: 0x0005AA5C
	public bool IsTutorialCanShow(FUNCTION_TYPE type)
	{
		int num = (int)type;
		string funciontId = num.ToString();
		return this.IsTutorialCanShow(funciontId);
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x0005C87C File Offset: 0x0005AA7C
	public bool IsTutorialCanShow(string funciontId)
	{
		FunctionData functionDataById = DataManager.GetFunctionDataById(funciontId);
		if (functionDataById == null)
		{
			return false;
		}
		if (this.mTutorialFunctionStateDic == null || !this.mTutorialFunctionStateDic.ContainsKey(funciontId))
		{
			return true;
		}
		if (functionDataById.Class == 3)
		{
			return this.mTutorialFunctionStateDic[funciontId].state < (long)functionDataById.Condition;
		}
		return this.mTutorialFunctionStateDic[funciontId].state == 0L;
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0005C8FC File Offset: 0x0005AAFC
	public bool CheckFirstClickState(string key)
	{
		if (this.mTutorialFunctionStateDic == null)
		{
			this.mTutorialFunctionStateDic = new Dictionary<string, function_info>();
		}
		return !this.mTutorialFunctionStateDic.ContainsKey(key) || this.mTutorialFunctionStateDic[key].state != 1L;
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0005C94C File Offset: 0x0005AB4C
	public void SetFirstClickState(string key)
	{
		if (this.mTutorialFunctionStateDic == null)
		{
			this.mTutorialFunctionStateDic = new Dictionary<string, function_info>();
		}
		if (this.mTutorialFunctionStateDic.ContainsKey(key) && this.mTutorialFunctionStateDic[key].state == 1L)
		{
			return;
		}
		if (!this.mTutorialFunctionStateDic.ContainsKey(key))
		{
			function_info function_info = new function_info();
			function_info.ID = key;
			function_info.state = 1L;
			this.mTutorialFunctionStateDic.Add(key, function_info);
		}
		unlock_function_complete.request request = new unlock_function_complete.request();
		request.ID = key;
		request.state = 1L;
		NetLogic.GetInstance().Send<Protocol.unlock_function_complete>(request, null);
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x0005C9F0 File Offset: 0x0005ABF0
	public void SetTutorialShowFinish(FUNCTION_TYPE type)
	{
		int num = (int)type;
		string tutorialShowFinish = num.ToString();
		this.SetTutorialShowFinish(tutorialShowFinish);
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0005CA10 File Offset: 0x0005AC10
	public void SetTutorialShowFinish(string functionId)
	{
		if (!this.IsTutorialCanShow(functionId))
		{
			return;
		}
		FunctionData functionDataById = DataManager.GetFunctionDataById(functionId);
		if (functionDataById == null)
		{
			return;
		}
		int num = 1;
		if (this.mTutorialFunctionStateDic == null)
		{
			this.mTutorialFunctionStateDic = new Dictionary<string, function_info>();
		}
		if (functionDataById.Class == 3)
		{
			if (this.mTutorialFunctionStateDic.ContainsKey(functionId))
			{
				this.mTutorialFunctionStateDic[functionId].state += 1L;
				num = (int)this.mTutorialFunctionStateDic[functionId].state;
			}
			else
			{
				function_info function_info = new function_info();
				function_info.ID = functionId;
				function_info.state = (long)num;
				this.mTutorialFunctionStateDic.Add(functionId, function_info);
			}
		}
		else if (this.mTutorialFunctionStateDic.ContainsKey(functionId))
		{
			this.mTutorialFunctionStateDic[functionId].state = 1L;
		}
		else
		{
			function_info function_info2 = new function_info();
			function_info2.ID = functionId;
			function_info2.state = 1L;
			this.mTutorialFunctionStateDic.Add(functionId, function_info2);
		}
		unlock_function_complete.request request = new unlock_function_complete.request();
		request.ID = functionId;
		request.state = (long)num;
		NetLogic.GetInstance().Send<Protocol.unlock_function_complete>(request, null);
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0005CB38 File Offset: 0x0005AD38
	public void SetTimeMissionFinish(string missionId)
	{
		Debug.Log("SetTimeMissionFinish :: " + missionId);
		int num = 1;
		if (this.mTutorialFunctionStateDic == null)
		{
			this.mTutorialFunctionStateDic = new Dictionary<string, function_info>();
		}
		if (this.mTutorialFunctionStateDic.ContainsKey(missionId))
		{
			this.mTutorialFunctionStateDic[missionId].state += 1L;
			num = (int)this.mTutorialFunctionStateDic[missionId].state;
		}
		else
		{
			function_info function_info = new function_info();
			function_info.ID = missionId;
			function_info.state = (long)num;
			this.mTutorialFunctionStateDic.Add(missionId, function_info);
		}
		unlock_function_complete.request request = new unlock_function_complete.request();
		request.ID = missionId;
		request.state = (long)num;
		NetLogic.GetInstance().Send<Protocol.unlock_function_complete>(request, null);
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x0005CBF4 File Offset: 0x0005ADF4
	public bool IsTimeMissionCanAccept(string missionId)
	{
		MissionData missionDataByID = DataManager.GetMissionDataByID(missionId);
		if (missionDataByID == null || missionDataByID.Class != 8)
		{
			return false;
		}
		TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(missionDataByID.TimeLimitId);
		return timeLimitMissionDataByID != null && (this.mTutorialFunctionStateDic == null || !this.mTutorialFunctionStateDic.ContainsKey(missionId) || this.mTutorialFunctionStateDic[missionId].state < (long)timeLimitMissionDataByID.LimitNum);
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x0005CC70 File Offset: 0x0005AE70
	public void SyncCommonData(sync_common_data.request request)
	{
		if (this.ServerTime == -1L)
		{
			this.ServerTime = request.serverTime;
		}
		this.TimeOffset = request.time_offset;
		LocalDataSaveManager.SetTimeOffset(this.TimeOffset);
		this.DailyMissionRefreshTime = request.daily_mission_refresh_time;
		this.mBig_PackFlag = request.big_pack;
		this.mFirst_PackFlag = request.first_buy;
		if (request.HasFunc_info)
		{
			this.mTutorialFunctionStateDic = request.func_info;
		}
		if (request.HasAdfree && request.adfree == 1L)
		{
			LocalDataSaveManager.SetADFreeFlag(1);
		}
		this.ResetFunctionUnlockData();
		PlayerCommonData.PvpScale = (float)request.pvp_scale / 10000f;
		if (request.HasPush)
		{
			this.Push = request.push;
			LocalDataSaveManager.SetRewardFlag((int)this.Push);
		}
		else
		{
			this.Push = -1L;
			LocalDataSaveManager.SetRewardFlag(0);
		}
		if (request.HasGuildId)
		{
			this.mChampionGuildId = request.guildId;
		}
		PlayerCommonData.InitRandom(request.seed);
		if (request.HasServer_level)
		{
			this.mServerLevel = (int)request.server_level;
		}
		if (request.HasStart_time)
		{
			this.mServerLevelSealTime = request.start_time;
		}
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x0005CDAC File Offset: 0x0005AFAC
	public void UpdateAddFree(string key)
	{
		PurchaseData purchaseDataBuyId = DataManager.GetPurchaseDataBuyId(key);
		if (purchaseDataBuyId != null && purchaseDataBuyId.AdFree == 1)
		{
			LocalDataSaveManager.SetADFreeFlag(1);
		}
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0005CDD8 File Offset: 0x0005AFD8
	public bool CheckDollorBuy(string key)
	{
		PurchaseData purchaseDataBuyId = DataManager.GetPurchaseDataBuyId(key);
		return purchaseDataBuyId != null;
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0005CDF4 File Offset: 0x0005AFF4
	public void CheckPopTipsUI()
	{
		if (SingletonUnity<UIManager>.Instance.ShowSpecialUI() || SingletonUnity<UIManager>.Instance.ShowSpecialRebirthUI())
		{
			return;
		}
		if (SingletonUnity<UIManager>.Instance.CheckReShowUI(UIInfo.LoadingUIRoot))
		{
			this.mPushShowFlag = true;
			return;
		}
		if (this.mPushShowFlag)
		{
			this.CheckShowUnlockFunction();
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.AutoPopUIRoot, delegate
			{
				SingletonUnity<AutoPopUIRoot>.Instance.Reset();
			}, null);
		}
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x0005CE80 File Offset: 0x0005B080
	public void CheckShowUnlockFunction()
	{
		if (!this.mPushShowFlag && SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
			return;
		}
		if (Singleton<ObjManager>.Instance.MainPlayer == null || Singleton<ObjManager>.Instance.MainPlayer.IsLocalDrivingCar)
		{
			return;
		}
		if (TutorialManager.IsTutorialCanShow())
		{
			if (this.IsTutorialCanShow(FUNCTION_TYPE.TIPBTN_TUTORIAL_TIP) && SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.MessageObj))
			{
				TutorialManager.ShowTutorial(TUTORIAL_STEP.FUNCTION_TIP_START);
			}
			else if (this.mNeedShowUnlockIdList.Count > 0)
			{
				string mCurShowId = this.mNeedShowUnlockIdList[0];
				this.mNeedShowUnlockIdList.RemoveAt(0);
				this.SetFunctionUnlockState(mCurShowId, true);
				FunctionData functionDataById = DataManager.GetFunctionDataById(mCurShowId);
				FUNCTION_TYPE function_TYPE = (FUNCTION_TYPE)int.Parse(functionDataById.ID);
				if (functionDataById.UnlockType == 1)
				{
					if (functionDataById.FirstOpen == 1)
					{
						if (SingletonUnity<JoyStickLogic>.Exists)
						{
							SingletonUnity<JoyStickLogic>.Instance.MoveOutScreen();
						}
						Singleton<ObjManager>.Instance.MainPlayer.DisactiveTargetArriveFinish();
						Singleton<ObjManager>.Instance.MainPlayer.StopMove();
						SingletonDontDestoryUnity<GameManager>.Instance.AutoSearchPath.IsAutoMovingFlag = false;
					}
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.UnlockFunctionRoot, delegate
					{
						SingletonUnity<UnlockFunctionRootLogic>.Instance.ResetUnlockFunction(mCurShowId);
					}, null);
				}
				else if (functionDataById.UnlockType == 0)
				{
					if (functionDataById.SideMissionIdList != null)
					{
						MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
						for (int i = 0; i < functionDataById.SideMissionIdList.Length; i++)
						{
							if (missionManager.IsMissionAcceptable(functionDataById.SideMissionIdList[i]))
							{
								missionManager.AcceptMission(functionDataById.SideMissionIdList[i]);
							}
						}
					}
					if (SingletonUnity<FunctionBtnRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FunctionBtnRootLogic>.Instance.gameObject))
					{
						SingletonUnity<FunctionBtnRootLogic>.Instance.refershBtn();
					}
					FUNCTION_TYPE function_TYPE2 = function_TYPE;
					if (function_TYPE2 != FUNCTION_TYPE.MAIN_MISSION)
					{
						if (function_TYPE2 != FUNCTION_TYPE.MAP_TIP)
						{
							this.CheckShowUnlockFunction();
						}
						else
						{
							TutorialManager.ShowTutorial(TUTORIAL_STEP.NEW_MAP_TIP_START);
							this.CheckShowUnlockFunction();
						}
					}
					else
					{
						TutorialManager.ShowTutorial(TUTORIAL_STEP.MAIN_MISSION_PHONE_START);
						if (SingletonUnity<MissionTeamTipLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MissionTeamTipLogic>.Instance.gameObject))
						{
							SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.CloseHandTip();
						}
					}
				}
				else if (functionDataById.UnlockType == 2)
				{
					if (functionDataById.SideMissionIdList != null)
					{
						MissionManager missionManager2 = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
						for (int j = 0; j < functionDataById.SideMissionIdList.Length; j++)
						{
							if (missionManager2.IsMissionAcceptable(functionDataById.SideMissionIdList[j]))
							{
								missionManager2.AcceptMission(functionDataById.SideMissionIdList[j]);
							}
						}
					}
					if (function_TYPE == FUNCTION_TYPE.ENHANCE_EQUIP)
					{
						if (this.IsTutorialCanShow(FUNCTION_TYPE.ENHANCE_EQUIP))
						{
							TutorialManager.ShowTutorial(TUTORIAL_STEP.ENHANCE_START);
						}
					}
					else
					{
						this.CheckShowUnlockFunction();
					}
				}
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowSpecialRebirthUI();
			}
		}
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x0005D1B0 File Offset: 0x0005B3B0
	private void AddFunctionTip(FUNCTION_TYPE functionType)
	{
		switch (functionType)
		{
		case FUNCTION_TYPE.GIFT_DAILY:
		case FUNCTION_TYPE.GIFT_INVEST:
		case FUNCTION_TYPE.GIFT_7DAY:
			if (SingletonUnity<FunctionBtnRootLogic>.Exists)
			{
				FunctionTipsRootLogic.AddFunctionTips(SingletonUnity<FunctionBtnRootLogic>.Instance.GiftFuncBtn.gameObject, Vector3.zero, -1f);
			}
			break;
		default:
			if (functionType != FUNCTION_TYPE.ACTIVITY_CHALLENGE)
			{
				if (functionType != FUNCTION_TYPE.ENHANCE_STAR)
				{
					if (functionType == FUNCTION_TYPE.CHARACTER_BADGE)
					{
						if (SingletonUnity<FunctionBtnRootLogic>.Exists)
						{
							FunctionTipsRootLogic.AddFunctionTips(SingletonUnity<FunctionBtnRootLogic>.Instance.CharacterBtnIcon.gameObject, Vector3.zero, -1f);
						}
					}
				}
				else if (SingletonUnity<FunctionBtnRootLogic>.Exists)
				{
					FunctionTipsRootLogic.AddFunctionTips(SingletonUnity<FunctionBtnRootLogic>.Instance.EnhanceFuncBtn.gameObject, Vector3.zero, -1f);
				}
			}
			else if (SingletonUnity<FunctionBtnRootLogic>.Exists)
			{
				FunctionTipsRootLogic.AddFunctionTips(SingletonUnity<FunctionBtnRootLogic>.Instance.ActivityBtnIcon.gameObject, Vector3.zero, -1f);
			}
			break;
		}
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x0005D2B4 File Offset: 0x0005B4B4
	public void CheckLevelUpUnlockFunction()
	{
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		List<FunctionData> functionDataList = DataManager.GetFunctionDataList();
		MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		functionDataList.Sort(delegate(FunctionData x, FunctionData y)
		{
			if (x.Condition != y.Condition)
			{
				return x.Condition - y.Condition;
			}
			return x.Index - y.Index;
		});
		for (int i = 0; i < functionDataList.Count; i++)
		{
			if (functionDataList[i].Class == 1 && functionDataList[i].Condition <= level && (!this.mFunctionUnlockState.ContainsKey(functionDataList[i].ID) || !this.mFunctionUnlockState[functionDataList[i].ID]))
			{
				FUNCTION_TYPE function_TYPE = (FUNCTION_TYPE)int.Parse(functionDataList[i].ID);
				if (this.CanCheckFunction(functionDataList[i]))
				{
					if (functionDataList[i].UnlockType == 2)
					{
						if (functionDataList[i].FirstOpen == 1 && !this.mMenuTabBtnTipIdList.Contains(functionDataList[i].ID))
						{
							this.mMenuTabBtnTipIdList.Add(functionDataList[i].ID);
						}
						if (function_TYPE == FUNCTION_TYPE.ENHANCE_EQUIP || function_TYPE == FUNCTION_TYPE.ACTIVITY_CHALLENGE || function_TYPE == FUNCTION_TYPE.TEAM)
						{
							this.mNeedShowUnlockIdList.Add(functionDataList[i].ID);
						}
					}
					else if (!this.mNeedShowUnlockIdList.Contains(functionDataList[i].ID))
					{
						this.mNeedShowUnlockIdList.Add(functionDataList[i].ID);
					}
					if (functionDataList[i].UnlockType == 2)
					{
						this.SetFunctionUnlockState(functionDataList[i].ID, true);
						if (function_TYPE != FUNCTION_TYPE.ENHANCE_EQUIP && function_TYPE != FUNCTION_TYPE.ACTIVITY_CHALLENGE && function_TYPE != FUNCTION_TYPE.TEAM)
						{
							if (functionDataList[i].SideMissionIdList != null)
							{
								for (int j = 0; j < functionDataList[i].SideMissionIdList.Length; j++)
								{
									if (missionManager.IsMissionAcceptable(functionDataList[i].SideMissionIdList[j]))
									{
										missionManager.AcceptMission(functionDataList[i].SideMissionIdList[j]);
									}
								}
							}
						}
					}
				}
				if (functionDataList[i].UnlockType == 0 || functionDataList[i].UnlockType == 2 || functionDataList[i].UnlockType == 1)
				{
					this.SetFunctionUnlockState(functionDataList[i].ID, true);
				}
			}
		}
		this.CheckShowUnlockFunction();
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateUnlockTips();
		}
		if (SingletonUnity<MissionTeamTipLogic>.Exists)
		{
			SingletonUnity<MissionTeamTipLogic>.Instance.UpdateUnlockTips();
		}
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0005D590 File Offset: 0x0005B790
	private void SetFunctionUnlockState(string id, bool isUnlock)
	{
		if (this.mFunctionUnlockState.ContainsKey(id))
		{
			this.mFunctionUnlockState[id] = isUnlock;
		}
		else
		{
			this.mFunctionUnlockState.Add(id, isUnlock);
		}
		if (isUnlock)
		{
			FunctionData functionDataById = DataManager.GetFunctionDataById(id);
			if (functionDataById.SideMissionIdList != null && functionDataById.SideMissionIdList.Length > 0)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.RemoveUnLockFunctionMission(functionDataById.SideMissionIdList);
			}
			if (functionDataById.FirstOpen != 1 && functionDataById.Class != 4)
			{
				this.SetTutorialShowFinish(id);
			}
		}
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x0005D628 File Offset: 0x0005B828
	public long GetResetDiffTime()
	{
		DateTime dateTime;
		dateTime..ctor(this.GetCurServerTime() * TimeTools.SECONDS_TO_TICKS);
		long num = 86400L;
		return ((long)this.ResetTime + this.TimeOffset * 3600L + 120L - (long)dateTime.TimeOfDay.TotalSeconds + num) % num;
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x0005D680 File Offset: 0x0005B880
	private bool IsCurMainMissionTriggerTutorial()
	{
		CurMission curMissionByClassType = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetCurMissionByClassType(MISSION_CLASS_TYPE.MAIN);
		if (curMissionByClassType == null)
		{
			return false;
		}
		MissionData missionDataByID = DataManager.GetMissionDataByID(curMissionByClassType.MissionId);
		if (missionDataByID.TrigerType == MISSION_TRIGGER_TUTORIAL_TYPE.INVALID || !this.IsTutorialCanShow((FUNCTION_TYPE)missionDataByID.TriggerTutorialType))
		{
			return false;
		}
		if (missionDataByID.TrigerType != MISSION_TRIGGER_TUTORIAL_TYPE.ENHANCE_WEAPON_TUTORIAL_1 && missionDataByID.TrigerType != MISSION_TRIGGER_TUTORIAL_TYPE.ENHANCE_WEAPON_TUTORIAL_2 && missionDataByID.TrigerType != MISSION_TRIGGER_TUTORIAL_TYPE.ENHANCE_WEAPON_TUTORIAL_3)
		{
			return true;
		}
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsCanUpgradeWeapon())
		{
			this.SetTutorialShowFinish((FUNCTION_TYPE)missionDataByID.TriggerTutorialType);
			return false;
		}
		return true;
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x0005D720 File Offset: 0x0005B920
	public bool CheckBuyBadgeTutorial()
	{
		if (GameManager.IsSupportCurDataVersion145())
		{
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			if (missionManager.IsMissionAccepted("40026") && this.IsTutorialCanShow(FUNCTION_TYPE.BUY_BADGE_TUTORIAL))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x0005D764 File Offset: 0x0005B964
	public float ServerLevelSealRatio()
	{
		long num = this.ServerLevelSealTime - this.GetCurServerTime();
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		if (num > 0L)
		{
			return 1f;
		}
		if (level > this.ServerLevel)
		{
			LevelSealData levelSealDataByID = DataManager.GetLevelSealDataByID((level - this.ServerLevel).ToString());
			if (levelSealDataByID != null)
			{
				return levelSealDataByID.InhibitRatio;
			}
		}
		else
		{
			if (level == this.ServerLevel)
			{
				return 1f;
			}
			LevelSealData levelSealDataByID2 = DataManager.GetLevelSealDataByID((this.ServerLevel - level).ToString());
			if (levelSealDataByID2 != null)
			{
				return levelSealDataByID2.EncourageRatio;
			}
		}
		return 1f;
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x0005D810 File Offset: 0x0005BA10
	~PlayerCommonData()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.CheckLevelUpUnlockFunction));
	}

	// Token: 0x04000B5A RID: 2906
	private Dictionary<string, function_info> mTutorialFunctionStateDic = new Dictionary<string, function_info>();

	// Token: 0x04000B5B RID: 2907
	private List<string> mNeedShowUnlockIdList = new List<string>();

	// Token: 0x04000B5C RID: 2908
	private List<string> mMenuTabBtnTipIdList = new List<string>();

	// Token: 0x04000B5D RID: 2909
	private Dictionary<string, bool> mFunctionUnlockState = new Dictionary<string, bool>();

	// Token: 0x04000B5E RID: 2910
	private long mServerTime;

	// Token: 0x04000B5F RID: 2911
	private long commondSeed;

	// Token: 0x04000B60 RID: 2912
	public static long sendIndex = 0L;

	// Token: 0x04000B61 RID: 2913
	public static uint randomSeed;

	// Token: 0x04000B62 RID: 2914
	private static List<int> randomArray = new List<int>();

	// Token: 0x04000B63 RID: 2915
	private int mServerLevel;

	// Token: 0x04000B64 RID: 2916
	private long mServerLevelSealTime;

	// Token: 0x04000B65 RID: 2917
	private int mResetTime = 10800;

	// Token: 0x04000B66 RID: 2918
	private int mRankPvpResetTime = 32400;

	// Token: 0x04000B67 RID: 2919
	private float mLocalTime;

	// Token: 0x04000B68 RID: 2920
	public static XorFloat PvpScale = new XorFloat();

	// Token: 0x04000B69 RID: 2921
	public static XorFloat PvpScaleAdd = new XorFloat(0.05f);

	// Token: 0x04000B6A RID: 2922
	private long mTimeOffset;

	// Token: 0x04000B6B RID: 2923
	private long mDailyMissionRefreshTime;

	// Token: 0x04000B6C RID: 2924
	private long mCreateTime;

	// Token: 0x04000B6D RID: 2925
	private long mCreateDay = -1L;

	// Token: 0x04000B6E RID: 2926
	private long mBig_PackFlag;

	// Token: 0x04000B6F RID: 2927
	private long mFirst_PackFlag;

	// Token: 0x04000B70 RID: 2928
	private long mPush = -1L;

	// Token: 0x04000B71 RID: 2929
	private bool mPushShowFlag;

	// Token: 0x04000B72 RID: 2930
	private long mChampionGuildId;
}
