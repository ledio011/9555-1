using System;
using System.IO;
using SprotoType;
using UnityEngine;

// Token: 0x02000A05 RID: 2565
public class MissionTeamTipLogic : SingletonUnity<MissionTeamTipLogic>
{
	// Token: 0x17000FBF RID: 4031
	// (get) Token: 0x0600495E RID: 18782 RVA: 0x0017B470 File Offset: 0x00179670
	// (set) Token: 0x0600495F RID: 18783 RVA: 0x0017B478 File Offset: 0x00179678
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn
	{
		get
		{
			return this.OnClickTutorialBtn;
		}
		set
		{
			this.OnClickTutorialBtn = value;
		}
	}

	// Token: 0x06004960 RID: 18784 RVA: 0x0017B484 File Offset: 0x00179684
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004961 RID: 18785 RVA: 0x0017B490 File Offset: 0x00179690
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
		}
	}

	// Token: 0x06004962 RID: 18786 RVA: 0x0017B4AC File Offset: 0x001796AC
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x17000FC0 RID: 4032
	// (get) Token: 0x06004963 RID: 18787 RVA: 0x0017B4B8 File Offset: 0x001796B8
	// (set) Token: 0x06004964 RID: 18788 RVA: 0x0017B4C0 File Offset: 0x001796C0
	public bool IsEnterDanceArea
	{
		get
		{
			return this.mIsEnterDanceArea;
		}
		set
		{
			this.mIsEnterDanceArea = value;
		}
	}

	// Token: 0x17000FC1 RID: 4033
	// (get) Token: 0x06004965 RID: 18789 RVA: 0x0017B4CC File Offset: 0x001796CC
	public int CurPage
	{
		get
		{
			return MissionTeamTipLogic.mCurPage;
		}
	}

	// Token: 0x06004966 RID: 18790 RVA: 0x0017B4D4 File Offset: 0x001796D4
	public void EnableReset()
	{
		NGUITools.SetActive(this.MissionTipRoot.gameObject, false);
		NGUITools.SetActive(this.TeamTipRoot.gameObject, false);
		NGUITools.SetActive(this.MenuObj.gameObject, false);
		NGUITools.SetActive(this.DanceTipRoot.gameObject, false);
		this.startTime = 300f;
		this.startTime2 = 300f;
		this.startTime3 = 10f;
		this.IsHideFlag = false;
		this.TwHideBtnPic.ResetToBeginning();
		if (MissionTeamTipLogic.mCurPage == 1)
		{
			MissionTeamTipLogic.mCurPage = -1;
			this.OnClickMissionBtn();
		}
		else
		{
			MissionTeamTipLogic.mCurPage = -1;
			this.OnClickMenuBtn();
		}
	}

	// Token: 0x06004967 RID: 18791 RVA: 0x0017B580 File Offset: 0x00179780
	public void CheckTeamTips()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsTeamLeader())
		{
			if (playerData.TeamInfo.ApplyMemberDic.Count > 0)
			{
				this.TipsFlag.enabled = true;
			}
			else
			{
				this.TipsFlag.enabled = false;
			}
		}
		else
		{
			this.TipsFlag.enabled = false;
		}
	}

	// Token: 0x06004968 RID: 18792 RVA: 0x0017B5E8 File Offset: 0x001797E8
	public void UpdateMatchBtn()
	{
		if (MissionTeamTipLogic.mCurPage == 2)
		{
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.IsVertify)
				{
					NGUITools.SetActive(this.MatchBtn.gameObject, true);
				}
				else
				{
					NGUITools.SetActive(this.MatchBtn.gameObject, false);
				}
			}
		}
		else
		{
			NGUITools.SetActive(this.MatchBtn.gameObject, false);
		}
	}

	// Token: 0x06004969 RID: 18793 RVA: 0x0017B66C File Offset: 0x0017986C
	public void OnClickMatchBtn()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveTeam() && !playerData.IsTeamLeader())
		{
			NoticeLogic.AddNotifyData("#{102008}", true, false);
			return;
		}
		if (playerData.TeamInfo.IsVertify)
		{
			MessageBoxLogic.OpenOKCancelBox("#{103205}", "#{100127}", delegate
			{
				stop_random_select_team.request request = new stop_random_select_team.request();
				request.id = playerData.TeamInfo.TeamGoalData.ID;
				request.type1 = (long)playerData.TeamInfo.TeamGoalData.GoalType;
				NetLogic.GetInstance().Send<Protocol.stop_random_select_team>(request, null);
				NGUITools.SetActive(this.MatchBtn.gameObject, false);
			}, null, null, null);
		}
	}

	// Token: 0x0600496A RID: 18794 RVA: 0x0017B6F8 File Offset: 0x001798F8
	public void UpdateUnlockTips()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData.MenuTabBtnTipIdList.Contains(3018.ToString()))
		{
			FunctionTipsRootLogic.AddFunctionTips(this.TeamBtn.gameObject, Vector3.zero, -1f);
		}
		else
		{
			FunctionTipsRootLogic.RemoveFunctionTips(this.TeamBtn.gameObject);
		}
	}

	// Token: 0x0600496B RID: 18795 RVA: 0x0017B75C File Offset: 0x0017995C
	public void SetTeamApplyTips(bool ishave)
	{
		this.TipsFlag.enabled = ishave;
	}

	// Token: 0x0600496C RID: 18796 RVA: 0x0017B76C File Offset: 0x0017996C
	public void OnClickHideBtn()
	{
		if (this.IsHideFlag)
		{
			this.IsHideFlag = false;
			if (TutorialManager.CurStep == TUTORIAL_STEP.MAIN_MISSION_PHONE_START)
			{
				this.TwHideBtnPic.ResetToBeginning();
				this.TwControllerRoot.ResetToBeginning();
				this.CheckTutorialEvent();
			}
			else
			{
				this.TwHideBtnPic.PlayReverse();
				this.TwControllerRoot.PlayReverse();
			}
		}
		else
		{
			this.IsHideFlag = true;
			this.TwHideBtnPic.PlayForward();
			this.TwControllerRoot.PlayForward();
		}
	}

	// Token: 0x0600496D RID: 18797 RVA: 0x0017B7F0 File Offset: 0x001799F0
	public void SetToCloseState()
	{
		this.IsHideFlag = true;
		this.TwHideBtnPic.PlayForward();
		this.TwControllerRoot.PlayForward();
		this.TwHideBtnPic.enabled = false;
		this.TwControllerRoot.enabled = false;
		this.TwHideBtnPic.value = this.TwHideBtnPic.to;
		this.TwControllerRoot.value = this.TwControllerRoot.to;
	}

	// Token: 0x0600496E RID: 18798 RVA: 0x0017B860 File Offset: 0x00179A60
	public void OnClickTeamBtn()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.TEAM))
		{
			if (MissionTeamTipLogic.mCurPage == 2)
			{
				if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveTeam())
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, null, null);
				}
			}
			else
			{
				this.ShowTeamTip();
			}
		}
		else
		{
			int condition = DataManager.GetFunctionDataById(3018.ToString()).Condition;
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
			{
				condition
			}), true, false);
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.MenuTabBtnTipIdList.Contains(3018.ToString()))
		{
			FunctionTipsRootLogic.RemoveFunctionTips(this.TeamBtn.gameObject);
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.MenuTabBtnTipIdList.Remove(3018.ToString());
			this.UpdateUnlockTips();
		}
	}

	// Token: 0x0600496F RID: 18799 RVA: 0x0017B95C File Offset: 0x00179B5C
	public void OnClickMenuBtn()
	{
		if (MissionTeamTipLogic.mCurPage != 0)
		{
			this.mPrePage = MissionTeamTipLogic.mCurPage;
			MissionTeamTipLogic.mCurPage = 0;
			this.ShowMenuInfo();
		}
	}

	// Token: 0x06004970 RID: 18800 RVA: 0x0017B980 File Offset: 0x00179B80
	public void ShowMenuInfo()
	{
		NGUITools.SetActive(this.MissionTipRoot.gameObject, false);
		NGUITools.SetActive(this.TeamTipRoot.gameObject, false);
		NGUITools.SetActive(this.MenuObj.gameObject, true);
		NGUITools.SetActive(this.DanceTipRoot.gameObject, false);
		if (this.IsEnterDanceArea)
		{
			NGUITools.SetActive(this.DanceBtn.gameObject, true);
		}
		else
		{
			NGUITools.SetActive(this.DanceBtn.gameObject, false);
		}
		this.UpdateUnlockTips();
		this.CheckTeamTips();
		this.MissionTips.enabled = false;
	}

	// Token: 0x06004971 RID: 18801 RVA: 0x0017BA1C File Offset: 0x00179C1C
	public void OnClickBackBtn()
	{
		if (this.CurPage != 0)
		{
			this.OnClickMenuBtn();
		}
	}

	// Token: 0x06004972 RID: 18802 RVA: 0x0017BA30 File Offset: 0x00179C30
	public void OnClickCloseBtn()
	{
		this.OnClickHideBtn();
	}

	// Token: 0x06004973 RID: 18803 RVA: 0x0017BA38 File Offset: 0x00179C38
	public void OnClickMissionBtn()
	{
		if (MissionTeamTipLogic.mCurPage == 1)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload)
			{
				SingletonUnity<UIManager>.Instance.CloseAllPOPUI();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickMissionBtn();
				}, null);
			}
		}
		else
		{
			this.ShowMissionTip();
			if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_SWITCH_MISSION)
			{
				this.CheckTutorialEvent();
			}
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.MAIN_MISSION_CLICK_MISSION_TAB)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004974 RID: 18804 RVA: 0x0017BACC File Offset: 0x00179CCC
	public void OnClickDanceBtn()
	{
		if (this.CurPage != 3)
		{
			this.ShowDanceTip();
		}
	}

	// Token: 0x06004975 RID: 18805 RVA: 0x0017BAE0 File Offset: 0x00179CE0
	public void ChangeToDanceMission(bool IsEnterDance)
	{
		if (this.IsEnterDanceArea == IsEnterDance)
		{
			return;
		}
		this.IsEnterDanceArea = IsEnterDance;
		if (this.IsEnterDanceArea)
		{
			NGUITools.SetActive(this.MissionTipRoot.gameObject, false);
			NGUITools.SetActive(this.TeamTipRoot.gameObject, false);
			NGUITools.SetActive(this.MenuObj.gameObject, false);
			NGUITools.SetActive(this.DanceTipRoot.gameObject, true);
			this.ShowDanceTip();
		}
		else if (MissionTeamTipLogic.mCurPage == 3)
		{
			if (this.mPrePage == 1)
			{
				this.OnClickMissionBtn();
			}
			else if (this.mPrePage == 2)
			{
				this.OnClickTeamBtn();
			}
			else
			{
				this.OnClickMenuBtn();
			}
		}
		else if (MissionTeamTipLogic.mCurPage == 0)
		{
			NGUITools.SetActive(this.DanceBtn.gameObject, false);
		}
	}

	// Token: 0x06004976 RID: 18806 RVA: 0x0017BBBC File Offset: 0x00179DBC
	public void ShowNewMissionFlag()
	{
		if (MissionTeamTipLogic.mCurPage != 1)
		{
			this.MissionTips.enabled = true;
		}
	}

	// Token: 0x06004977 RID: 18807 RVA: 0x0017BBD8 File Offset: 0x00179DD8
	public void ShowMissionTip()
	{
		if (MissionTeamTipLogic.mCurPage != 1)
		{
			this.mPrePage = MissionTeamTipLogic.mCurPage;
			MissionTeamTipLogic.mCurPage = 1;
			NGUITools.SetActive(this.MissionTipRoot.gameObject, true);
			NGUITools.SetActive(this.TeamTipRoot.gameObject, false);
			NGUITools.SetActive(this.MenuObj.gameObject, false);
			NGUITools.SetActive(this.DanceTipRoot.gameObject, false);
			this.MissionTipRoot.Reset();
			this.TipScrollView.ResetPosition();
		}
	}

	// Token: 0x06004978 RID: 18808 RVA: 0x0017BC5C File Offset: 0x00179E5C
	public void ShowTeamTip()
	{
		if (MissionTeamTipLogic.mCurPage != 2)
		{
			this.mPrePage = MissionTeamTipLogic.mCurPage;
			MissionTeamTipLogic.mCurPage = 2;
			NGUITools.SetActive(this.MissionTipRoot.gameObject, false);
			NGUITools.SetActive(this.TeamTipRoot.gameObject, true);
			NGUITools.SetActive(this.MenuObj.gameObject, false);
			NGUITools.SetActive(this.DanceTipRoot.gameObject, false);
			this.TeamTipRoot.Reset();
			this.TipScrollView.ResetPosition();
			this.UpdateMatchBtn();
		}
	}

	// Token: 0x06004979 RID: 18809 RVA: 0x0017BCE8 File Offset: 0x00179EE8
	public void ShowDanceTip()
	{
		request_dance_state_info.request rpcReq = new request_dance_state_info.request();
		NetLogic.GetInstance().Send<Protocol.request_dance_state_info>(rpcReq, null);
		if (MissionTeamTipLogic.mCurPage != 3)
		{
			this.mPrePage = MissionTeamTipLogic.mCurPage;
			MissionTeamTipLogic.mCurPage = 3;
			NGUITools.SetActive(this.MissionTipRoot.gameObject, false);
			NGUITools.SetActive(this.TeamTipRoot.gameObject, false);
			NGUITools.SetActive(this.MenuObj.gameObject, false);
			NGUITools.SetActive(this.DanceTipRoot.gameObject, true);
			this.DanceTipRoot.Reset();
			this.TipScrollView.ResetPosition();
		}
	}

	// Token: 0x0600497A RID: 18810 RVA: 0x0017BD80 File Offset: 0x00179F80
	public void ResetMissionTip()
	{
		this.MissionTipRoot.Reset();
	}

	// Token: 0x0600497B RID: 18811 RVA: 0x0017BD90 File Offset: 0x00179F90
	public void AddMission(string missionId)
	{
		if (MissionTeamTipLogic.mCurPage == 1)
		{
			this.MissionTipRoot.Reset();
		}
	}

	// Token: 0x0600497C RID: 18812 RVA: 0x0017BDA8 File Offset: 0x00179FA8
	public void RemoveMission(string missionId)
	{
		if (MissionTeamTipLogic.mCurPage == 1)
		{
			this.MissionTipRoot.Reset();
		}
	}

	// Token: 0x0600497D RID: 18813 RVA: 0x0017BDC0 File Offset: 0x00179FC0
	public void UpdateMission(string missionId)
	{
		if (MissionTeamTipLogic.mCurPage == 1)
		{
			this.MissionTipRoot.UpdateMission(missionId);
		}
	}

	// Token: 0x0600497E RID: 18814 RVA: 0x0017BDDC File Offset: 0x00179FDC
	public void UpdateDanceInfo()
	{
		if (this.CurPage == 3)
		{
			this.DanceTipRoot.Reset();
		}
	}

	// Token: 0x0600497F RID: 18815 RVA: 0x0017BDF8 File Offset: 0x00179FF8
	private void Update()
	{
		this.startTime += Time.deltaTime;
		this.startTime2 += Time.deltaTime;
		this.startTime3 += Time.deltaTime;
		if (this.startTime >= 180f)
		{
			this.UpdataBattery();
			this.startTime = 0f;
		}
		if (this.startTime2 >= 60f)
		{
			this.UpdateMinute();
			this.startTime2 = 0f;
		}
		if (this.startTime3 >= 0.1f)
		{
			this.UpdateDelayTime();
			this.startTime3 = 0f;
		}
	}

	// Token: 0x06004980 RID: 18816 RVA: 0x0017BEA0 File Offset: 0x0017A0A0
	private string GetNetStateSpriteName(int time, int netState)
	{
		if (time > 0)
		{
			int num;
			if (time < 50)
			{
				num = 0;
			}
			else if (time < 200)
			{
				num = 1;
			}
			else if (time < 500)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
			if (netState == 0)
			{
				if (num > 2)
				{
					num = 2;
				}
				return this.wifiName[num];
			}
			if (netState == 1)
			{
				return this.mobileName[num];
			}
		}
		else
		{
			if (netState == 0)
			{
				return this.wifiName[3];
			}
			if (netState == 1)
			{
				return this.mobileName[4];
			}
		}
		return this.wifiName[3];
	}

	// Token: 0x06004981 RID: 18817 RVA: 0x0017BF40 File Offset: 0x0017A140
	private void UpdateDelayTime()
	{
		int netState = -1;
		int netDelayTime = SingletonDontDestoryUnity<NetManager>.Instance.NetDelayTime;
		if (Application.internetReachability == 2)
		{
			netState = 0;
		}
		else if (Application.internetReachability == 1)
		{
			netState = 1;
		}
		this.NetStateSprite.spriteName = this.GetNetStateSpriteName(netDelayTime, netState);
		this.NetStateSprite.MakePixelPerfect();
	}

	// Token: 0x06004982 RID: 18818 RVA: 0x0017BF98 File Offset: 0x0017A198
	private void UpdataBattery()
	{
		int batteryLevel = this.GetBatteryLevel();
		this.BatterySlider.value = Mathf.Clamp01((float)batteryLevel / 100f);
	}

	// Token: 0x06004983 RID: 18819 RVA: 0x0017BFC4 File Offset: 0x0017A1C4
	private int GetBatteryLevel()
	{
		int result = 50;
		try
		{
			string text = File.ReadAllText("/sys/class/power_supply/battery/capacity");
			result = int.Parse(text);
		}
		catch (Exception ex)
		{
			result = SingletonDontDestoryUnity<GameManager>.Instance.GetBatteryState();
		}
		return result;
	}

	// Token: 0x06004984 RID: 18820 RVA: 0x0017C01C File Offset: 0x0017A21C
	private void UpdateMinute()
	{
		DateTime now = DateTime.Now;
		this.TimeLabel.text = string.Format("{0:D2}:{1:D2}", now.Hour, now.Minute);
	}

	// Token: 0x040036AD RID: 13997
	private TutorialManager.OnClickTutorialBtn OnClickTutorialBtn;

	// Token: 0x040036AE RID: 13998
	public MissionTipLogic MissionTipRoot;

	// Token: 0x040036AF RID: 13999
	public TeamTipLogic TeamTipRoot;

	// Token: 0x040036B0 RID: 14000
	public DanceTipLogic DanceTipRoot;

	// Token: 0x040036B1 RID: 14001
	public GameObject MenuObj;

	// Token: 0x040036B2 RID: 14002
	private bool mIsEnterDanceArea;

	// Token: 0x040036B3 RID: 14003
	public TweenAlpha TwHideBtnPic;

	// Token: 0x040036B4 RID: 14004
	public TweenPosition TwControllerRoot;

	// Token: 0x040036B5 RID: 14005
	public UISprite TeamBtn;

	// Token: 0x040036B6 RID: 14006
	public UISprite MissionBtn;

	// Token: 0x040036B7 RID: 14007
	public static int mCurPage = -1;

	// Token: 0x040036B8 RID: 14008
	private int mPrePage = -1;

	// Token: 0x040036B9 RID: 14009
	private bool IsHideFlag;

	// Token: 0x040036BA RID: 14010
	public GameObject DanceBtn;

	// Token: 0x040036BB RID: 14011
	public UIScrollView TipScrollView;

	// Token: 0x040036BC RID: 14012
	public UISprite TipsFlag;

	// Token: 0x040036BD RID: 14013
	public UISprite MissionTips;

	// Token: 0x040036BE RID: 14014
	public GameObject MatchBtn;

	// Token: 0x040036BF RID: 14015
	public UILabel TimeLabel;

	// Token: 0x040036C0 RID: 14016
	public UISprite NetStateSprite;

	// Token: 0x040036C1 RID: 14017
	public UISlider BatterySlider;

	// Token: 0x040036C2 RID: 14018
	private float startTime;

	// Token: 0x040036C3 RID: 14019
	private float startTime2;

	// Token: 0x040036C4 RID: 14020
	private float startTime3;

	// Token: 0x040036C5 RID: 14021
	private string[] wifiName = new string[]
	{
		"CZ_WIFI_3",
		"CZ_WIFI_2",
		"CZ_WIFI_1",
		"CZ_WIFI_NOWIFI"
	};

	// Token: 0x040036C6 RID: 14022
	private string[] mobileName = new string[]
	{
		"CZ_4G_4",
		"CZ_4G_3",
		"CZ_4G_2",
		"CZ_4G_1",
		"CZ_4G_NO4G"
	};
}
