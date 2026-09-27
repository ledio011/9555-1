using System;
using UnityEngine;

// Token: 0x02000A30 RID: 2608
public class NewMissionInfoRootLogic : MonoBehaviour
{
	// Token: 0x06004C09 RID: 19465 RVA: 0x0019B0DC File Offset: 0x001992DC
	public void Reset(string missionId)
	{
		if (this.curMissionData != null && this.curMissionData.ID.Equals(missionId))
		{
			return;
		}
		NGUITools.SetActive(this.StarObj, false);
		this.curMissionData = DataManager.GetMissionDataByID(missionId);
		this.MissionDescLabel.text = this.curMissionData.MDescribeID;
		this.missionManager = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager;
		MISSION_STATE missionState = this.missionManager.GetMissionState(this.curMissionData.ID);
		PROFESSION_TYPE profession = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession;
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		this.TimeLabel.enabled = false;
		if (this.curMissionData.Class == 0)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardRoot, this.curMissionData.ID, REWARD_TYPE.DAILY_MISSION, level, profession, 0);
			NGUITools.SetActive(this.AbandonBtnSp.gameObject, false);
			this.StartBtnSp.transform.localPosition = new Vector3(0f, this.StartBtnSp.transform.localPosition.y, 0f);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100311}", new object[0]);
			NGUITools.SetActive(this.MissionDescLabel.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, true);
			int num = (int)this.missionManager.GetMissionParam(this.curMissionData.ID, 1);
			if (num < 0)
			{
				num = level;
			}
			ShowRewardManager.ShowRewardItem(this.DailyFullShowReward, this.curMissionData.ID, REWARD_TYPE.DAILY_MISSION, num, profession, 1);
		}
		else if (this.curMissionData.Class == 1)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardRoot, this.curMissionData.ID, REWARD_TYPE.MISSION, level, profession, 0);
			NGUITools.SetActive(this.AbandonBtnSp.gameObject, false);
			this.StartBtnSp.transform.localPosition = new Vector3(0f, this.StartBtnSp.transform.localPosition.y, 0f);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDescLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (this.curMissionData.Class == 4 || this.curMissionData.Class == 5)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardRoot, this.curMissionData.ID, REWARD_TYPE.ESCORT, level, profession, 0);
			NGUITools.SetActive(this.AbandonBtnSp.gameObject, true);
			this.StartBtnSp.transform.localPosition = new Vector3(60f, this.StartBtnSp.transform.localPosition.y, 0f);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDescLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (this.curMissionData.Class == 3)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardRoot, this.curMissionData.ID, REWARD_TYPE.DAILY_MISSION, level, profession, 1);
			NGUITools.SetActive(this.AbandonBtnSp.gameObject, false);
			this.StartBtnSp.transform.localPosition = new Vector3(0f, this.StartBtnSp.transform.localPosition.y, 0f);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDescLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (this.curMissionData.Class == 2)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardRoot, this.curMissionData.ID, REWARD_TYPE.MISSION, level, profession, 0);
			NGUITools.SetActive(this.AbandonBtnSp.gameObject, false);
			this.StartBtnSp.transform.localPosition = new Vector3(0f, this.StartBtnSp.transform.localPosition.y, 0f);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDescLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (this.curMissionData.Class == 6)
		{
			int num2 = (int)this.missionManager.GetMissionParam(this.curMissionData.ID, 1);
			if (num2 < 0)
			{
				num2 = level;
			}
			ShowRewardManager.ShowRewardItem(this.ShowRewardRoot, this.curMissionData.ID, REWARD_TYPE.DAILY_MISSION, num2, profession, 1);
			NGUITools.SetActive(this.AbandonBtnSp.gameObject, false);
			this.StartBtnSp.transform.localPosition = new Vector3(0f, this.StartBtnSp.transform.localPosition.y, 0f);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDescLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (this.curMissionData.Class == 7)
		{
			OnlineMissionData onlineMissionDataByID = DataManager.GetOnlineMissionDataByID(this.curMissionData.ID);
			string id = string.Empty;
			id = this.missionManager.GetMissionParam(this.curMissionData.ID, 1).ToString();
			onlineMissionDataByID = DataManager.GetOnlineMissionDataByID(id);
			if (onlineMissionDataByID != null)
			{
				ShowRewardManager.ShowRewardItem(this.ShowRewardRoot, id, REWARD_TYPE.ONLINEMISSION, level, profession, 0);
				NGUITools.SetActive(this.StarObj, true);
				for (int i = 0; i < this.Star.Length; i++)
				{
					if (i < onlineMissionDataByID.MissionStar)
					{
						this.Star[i].color = Color.white;
						this.Star[i].alpha = 1f;
					}
					else
					{
						this.Star[i].color = Color.black;
						this.Star[i].alpha = 0.5f;
					}
				}
			}
			NGUITools.SetActive(this.AbandonBtnSp.gameObject, true);
			this.StartBtnSp.transform.localPosition = new Vector3(60f, this.StartBtnSp.transform.localPosition.y, 0f);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDescLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (this.curMissionData.Class == 8)
		{
			this.TimeLabel.enabled = true;
			long times;
			if (missionState == MISSION_STATE.INVALID)
			{
				TimeLimitMissionData timeLimitMissionDataByID = DataManager.GetTimeLimitMissionDataByID(this.curMissionData.TimeLimitId);
				this.MissionDescLabel.text = timeLimitMissionDataByID.MDesc;
				times = timeLimitMissionDataByID.LimitTime;
			}
			else
			{
				times = this.missionManager.GetMissionRestTime(this.curMissionData.ID);
			}
			this.TimeLabel.text = StrDictionary.GetDictionaryString("#{100002}", new object[]
			{
				TimeTools.GetHourMinSecStr(times)
			});
			ShowRewardManager.ShowRewardItem(this.ShowRewardRoot, this.curMissionData.ID, REWARD_TYPE.MISSION, level, profession, 0);
			NGUITools.SetActive(this.AbandonBtnSp.gameObject, false);
			this.StartBtnSp.transform.localPosition = new Vector3(0f, this.StartBtnSp.transform.localPosition.y, 0f);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDescLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
	}

	// Token: 0x06004C0A RID: 19466 RVA: 0x0019B8FC File Offset: 0x00199AFC
	public void OnClickStartBtn()
	{
		MissionData missionData = this.curMissionData;
		SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickCloseBtn();
		MissionManager.ClickMissionAction(missionData.ID);
	}

	// Token: 0x06004C0B RID: 19467 RVA: 0x0019B928 File Offset: 0x00199B28
	public void OnClickAbandonBtn()
	{
		this.missionManager.AbandonMission(this.curMissionData.ID, false);
	}

	// Token: 0x06004C0C RID: 19468 RVA: 0x0019B944 File Offset: 0x00199B44
	private void OnDisable()
	{
		this.curMissionData = null;
	}

	// Token: 0x06004C0D RID: 19469 RVA: 0x0019B950 File Offset: 0x00199B50
	private void Update()
	{
		if (this.curMissionData != null && this.curMissionData.Class == 8)
		{
			this.timeCount += Time.deltaTime;
			if (this.timeCount >= 1f)
			{
				this.timeCount -= 1f;
				if (this.missionManager.IsMissionAccepted(this.curMissionData.ID))
				{
					this.TimeLabel.text = StrDictionary.GetDictionaryString("#{100002}", new object[]
					{
						TimeTools.GetHourMinSecStr(this.missionManager.GetMissionRestTime(this.curMissionData.ID))
					});
				}
			}
		}
	}

	// Token: 0x040039BB RID: 14779
	public UILabel MissionDescLabel;

	// Token: 0x040039BC RID: 14780
	public UILabel StaticMissionTitle2Label;

	// Token: 0x040039BD RID: 14781
	public ShowRewardItems ShowRewardRoot;

	// Token: 0x040039BE RID: 14782
	public ShowRewardItems DailyFullShowReward;

	// Token: 0x040039BF RID: 14783
	public UISprite StartBtnSp;

	// Token: 0x040039C0 RID: 14784
	public UISprite AbandonBtnSp;

	// Token: 0x040039C1 RID: 14785
	private MissionData curMissionData;

	// Token: 0x040039C2 RID: 14786
	public GameObject StarObj;

	// Token: 0x040039C3 RID: 14787
	public UISprite[] Star;

	// Token: 0x040039C4 RID: 14788
	public UILabel TimeLabel;

	// Token: 0x040039C5 RID: 14789
	private MissionManager missionManager;

	// Token: 0x040039C6 RID: 14790
	private float timeCount;
}
