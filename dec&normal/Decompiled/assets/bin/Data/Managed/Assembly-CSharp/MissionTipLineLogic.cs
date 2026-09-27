using System;
using UnityEngine;

// Token: 0x02000A06 RID: 2566
public class MissionTipLineLogic : MonoBehaviour
{
	// Token: 0x17000FC2 RID: 4034
	// (get) Token: 0x06004987 RID: 18823 RVA: 0x0017C07C File Offset: 0x0017A27C
	// (set) Token: 0x06004988 RID: 18824 RVA: 0x0017C084 File Offset: 0x0017A284
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

	// Token: 0x06004989 RID: 18825 RVA: 0x0017C090 File Offset: 0x0017A290
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x0600498A RID: 18826 RVA: 0x0017C09C File Offset: 0x0017A29C
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
			SingletonUnity<MissionTeamTipLogic>.Instance.TipScrollView.ResetPosition();
		}
	}

	// Token: 0x17000FC3 RID: 4035
	// (get) Token: 0x0600498B RID: 18827 RVA: 0x0017C0D8 File Offset: 0x0017A2D8
	// (set) Token: 0x0600498C RID: 18828 RVA: 0x0017C0E0 File Offset: 0x0017A2E0
	public string MissionId
	{
		get
		{
			return this.mMissionId;
		}
		set
		{
			this.mMissionId = value;
		}
	}

	// Token: 0x0600498D RID: 18829 RVA: 0x0017C0EC File Offset: 0x0017A2EC
	public void Reset(string missionId)
	{
		this.curLevel = 0;
		if (!this.mMissionId.Equals(missionId))
		{
			this.mOnClickTutorialBtn = null;
		}
		this.mMissionId = missionId;
		this.mCurMissionData = DataManager.GetMissionDataByID(this.mMissionId);
		this.missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		MISSION_STATE missionState = this.missionManager.GetMissionState(this.mMissionId);
		string text = string.Empty;
		switch (this.mCurMissionData.Class)
		{
		case 0:
			text = StrDictionary.GetDictionaryString("#{100168}", new object[]
			{
				this.missionManager.GetMissionParam(missionId, 3) + 1L
			});
			break;
		case 1:
			text = StrDictionary.GetDictionaryString("#{100169}", new object[0]);
			break;
		case 2:
			text = StrDictionary.GetDictionaryString("#{100170}", new object[0]);
			break;
		case 3:
		case 6:
			text = StrDictionary.GetDictionaryString("#{100171}", new object[0]);
			break;
		case 4:
			text = StrDictionary.GetDictionaryString("#{100172}", new object[0]);
			break;
		case 5:
			text = StrDictionary.GetDictionaryString("#{100173}", new object[0]);
			break;
		case 7:
			text = StrDictionary.GetDictionaryString("#{100200}", new object[0]);
			break;
		case 8:
			text = StrDictionary.GetDictionaryString("#{100001}", new object[0]);
			break;
		default:
			text = string.Format("[{0}]", this.mCurMissionData.Class);
			break;
		}
		if (this.mCurMissionData.Class == 0)
		{
			this.MissionInfoLabel.text = string.Concat(new string[]
			{
				StrDictionary.GetDictionaryString("#{100171}", new object[0]),
				"[FDAE33]",
				StrDictionary.GetDictionaryString(this.mCurMissionData.TipDescribeID, new object[0]),
				text,
				"[-]"
			});
		}
		else
		{
			this.MissionInfoLabel.text = text + "[FDAE33]" + StrDictionary.GetDictionaryString(this.mCurMissionData.TipDescribeID, new object[0]) + "[-]";
		}
		MissionManager.GetMissionStateLabel(this.mCurMissionData, missionState, this.MissionStateLabel);
		this.ShowCompleteEffect(missionState);
		if (this.mCurMissionData.Class == 8)
		{
			TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(this.mCurMissionData.TimeLimitId);
			this.MissionInfoLabel.text = text + "[FDAE33]" + timeLimitMissionDataByID.MName + "[-]";
			this.mTotalTime = timeLimitMissionDataByID.LimitTime;
			this.restTime = this.missionManager.GetMissionRestTime(this.mCurMissionData.ID);
			if (this.restTime <= 0L)
			{
				this.TimeCountBottomPic.enabled = false;
				this.mTimeCount = 0.01f;
			}
			else
			{
				this.TimeCountBottomPic.enabled = true;
			}
			this.TimeClockPic.enabled = true;
		}
		else
		{
			this.TimeCountBottomPic.enabled = false;
			this.TimeClockPic.enabled = false;
		}
	}

	// Token: 0x0600498E RID: 18830 RVA: 0x0017C3FC File Offset: 0x0017A5FC
	private void Update()
	{
		if (this.mCurMissionData != null && this.mCurMissionData.Class == 8)
		{
			this.mTimeCount -= Time.deltaTime;
			if (this.mTimeCount < 0f)
			{
				this.mTimeCount = 1f;
				this.restTime = this.missionManager.GetMissionRestTime(this.mCurMissionData.ID);
				if (this.restTime <= 0L)
				{
					this.missionManager.AbandonMission(this.mCurMissionData.ID, true);
					this.mCurMissionData = null;
					this.TimeCountBottomPic.enabled = false;
				}
				else
				{
					if (!this.TimeCountBottomPic.enabled)
					{
						this.TimeCountBottomPic.enabled = true;
					}
					float num = (float)this.restTime / (float)this.mTotalTime;
					this.TimeCountBottomPic.width = (int)((float)(this.BottomPic.width - 6) * num);
				}
			}
		}
	}

	// Token: 0x0600498F RID: 18831 RVA: 0x0017C4F4 File Offset: 0x0017A6F4
	private void OnEnable()
	{
		this.curLevel = 0;
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.RefreshMission));
	}

	// Token: 0x06004990 RID: 18832 RVA: 0x0017C520 File Offset: 0x0017A720
	private void OnDisable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.RefreshMission));
	}

	// Token: 0x06004991 RID: 18833 RVA: 0x0017C550 File Offset: 0x0017A750
	public void RefreshMission()
	{
		if (this.mCurMissionData == null)
		{
			return;
		}
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		if (this.curLevel != level)
		{
			MissionManager missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
			MISSION_STATE missionState = missionManager.GetMissionState(this.mMissionId);
			MissionManager.GetMissionStateLabel(this.mCurMissionData, missionState, this.MissionStateLabel);
			this.ShowCompleteEffect(missionState);
		}
		this.curLevel = level;
	}

	// Token: 0x06004992 RID: 18834 RVA: 0x0017C5C0 File Offset: 0x0017A7C0
	public void ShowCompleteEffect(MISSION_STATE curMissionState)
	{
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		if (this.mCurMissionData.Class == 1)
		{
			int num = 15;
			FunctionData functionDataById = DataManager.GetFunctionDataById(4087.ToString());
			if (functionDataById != null)
			{
				num = functionDataById.Condition;
			}
			if (level < num)
			{
				if (UnityVersionUtil.IsActive(base.gameObject))
				{
					UnityVersionUtil.SetActiveRecursive(this.CompleteTipObj.gameObject, true);
				}
				return;
			}
		}
		if (this.mCurMissionData.MinLv > level)
		{
			UnityVersionUtil.SetActiveRecursive(this.CompleteTipObj.gameObject, false);
		}
		else if (curMissionState == MISSION_STATE.COMPLETE)
		{
			if (UnityVersionUtil.IsActive(base.gameObject))
			{
				UnityVersionUtil.SetActiveRecursive(this.CompleteTipObj.gameObject, true);
			}
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.CompleteTipObj.gameObject, false);
		}
	}

	// Token: 0x06004993 RID: 18835 RVA: 0x0017C6A0 File Offset: 0x0017A8A0
	public void OnClickMissionLine()
	{
		if (this.mCurMissionData == null)
		{
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.MAIN_MISSION_START || TutorialManager.CurStep == TUTORIAL_STEP.MAIN_MISSION_TIP_START || TutorialManager.CurStep == TUTORIAL_STEP.DAILY_MISSION_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.SIDE_MISSION_CLICK_START || TutorialManager.CurStep == TUTORIAL_STEP.SIDE_MISSION_CAR_TIP_START || TutorialManager.CurStep == TUTORIAL_STEP.MAIN_MISSION_CLICK_MISSION)
		{
			this.CheckTutorialEvent();
		}
		MissionManager.ClickMissionAction(this.mCurMissionData.ID);
		SingletonUnity<MissionTeamTipLogic>.Instance.MissionTipRoot.CloseHandTip();
	}

	// Token: 0x040036C8 RID: 14024
	private TutorialManager.OnClickTutorialBtn OnClickTutorialBtn;

	// Token: 0x040036C9 RID: 14025
	public UILabel MissionInfoLabel;

	// Token: 0x040036CA RID: 14026
	public UILabel MissionStateLabel;

	// Token: 0x040036CB RID: 14027
	private string mMissionId = string.Empty;

	// Token: 0x040036CC RID: 14028
	public GameObject CompleteTipObj;

	// Token: 0x040036CD RID: 14029
	private MissionData mCurMissionData;

	// Token: 0x040036CE RID: 14030
	private int curLevel;

	// Token: 0x040036CF RID: 14031
	public UISprite BottomPic;

	// Token: 0x040036D0 RID: 14032
	public UISprite TimeCountBottomPic;

	// Token: 0x040036D1 RID: 14033
	public UISprite TimeClockPic;

	// Token: 0x040036D2 RID: 14034
	private long mTotalTime;

	// Token: 0x040036D3 RID: 14035
	private float mTimeCount;

	// Token: 0x040036D4 RID: 14036
	private MissionManager missionManager;

	// Token: 0x040036D5 RID: 14037
	private long restTime;
}
