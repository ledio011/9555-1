using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200095A RID: 2394
public class MissionPageRootLogic : SingletonUnity<MissionPageRootLogic>
{
	// Token: 0x060042FB RID: 17147 RVA: 0x00148CD4 File Offset: 0x00146ED4
	public void Reset(bool isRefersh = false)
	{
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			return;
		}
		this.isRefershFlag = isRefersh;
		UnityVersionUtil.SetActiveRecursive(this.missionLinePrefab, false);
		UnityVersionUtil.SetActiveRecursive(this.MissionLineChoosePic.gameObject, false);
		this.MissionLineChoosePic.transform.parent = this.MissionTabArray[0];
		for (int i = 0; i < this.lineisOpenFlag.Length; i++)
		{
			this.lineisOpenFlag[i] = false;
		}
		List<string> allMissionId = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetAllMissionId();
		this.curMainMissionList.Clear();
		this.curSideMissionList.Clear();
		this.curDailyMissionList.Clear();
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		for (int j = 0; j < allMissionId.Count; j++)
		{
			MissionData missionDataByID = DataManager.GetMissionDataByID(allMissionId[j]);
			if (level >= missionDataByID.DisplayLv)
			{
				if (missionDataByID != null)
				{
					switch (missionDataByID.Class)
					{
					case 0:
					case 3:
					case 6:
						this.curDailyMissionList.Add(missionDataByID);
						break;
					case 1:
						this.curMainMissionList.Add(missionDataByID);
						break;
					case 2:
					case 4:
					case 5:
						this.curSideMissionList.Add(missionDataByID);
						break;
					}
				}
			}
		}
		this.ResetMissionTab(this.MissionLineRootArray[0], this.curMainMissionList, this.mMainMissionLineList);
		this.ResetMissionTab(this.MissionLineRootArray[1], this.curDailyMissionList, this.mDailyMissionLineList);
		this.ResetMissionTab(this.MissionLineRootArray[2], this.curSideMissionList, this.mSideMissionLineList);
		if (!this.isRefershFlag)
		{
			this.AutoChoiceMission();
		}
		else
		{
			this.SelectLine();
		}
		this.Table.Reposition();
	}

	// Token: 0x060042FC RID: 17148 RVA: 0x00148EAC File Offset: 0x001470AC
	public void AutoChoiceMission()
	{
		this.isRefershFlag = false;
		if (this.curMainMissionList.Count != 0)
		{
			UnityVersionUtil.SetActiveRecursive(this.MissionLineChoosePic.gameObject, true);
			this.MissionLineChoosePic.transform.parent = this.mMainMissionLineList[0].transform;
			this.MissionLineChoosePic.transform.localPosition = Vector3.zero;
			this.OnClickTabRootBtn(this.MissionTabArray[0].gameObject);
		}
		else if (this.mDailyMissionLineList.Count != 0)
		{
			UnityVersionUtil.SetActiveRecursive(this.MissionLineChoosePic.gameObject, true);
			this.MissionLineChoosePic.transform.parent = this.mDailyMissionLineList[0].transform;
			this.MissionLineChoosePic.transform.localPosition = Vector3.zero;
			this.OnClickTabRootBtn(this.MissionTabArray[1].gameObject);
		}
		else if (this.curSideMissionList.Count != 0)
		{
			UnityVersionUtil.SetActiveRecursive(this.MissionLineChoosePic.gameObject, true);
			this.MissionLineChoosePic.transform.parent = this.mSideMissionLineList[0].transform;
			this.MissionLineChoosePic.transform.localPosition = Vector3.zero;
			this.OnClickTabRootBtn(this.MissionTabArray[2].gameObject);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.MissionLineChoosePic.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.MissionPageRoot, false);
		}
	}

	// Token: 0x060042FD RID: 17149 RVA: 0x0014902C File Offset: 0x0014722C
	public void SelectLine()
	{
		this.lineisOpenFlag[this.curSelectLines] = this.isOpening;
		switch (this.curSelectLines)
		{
		case 0:
			if (this.curMainMissionList.Count != 0)
			{
				UnityVersionUtil.SetActiveRecursive(this.MissionLineChoosePic.gameObject, true);
				this.MissionLineChoosePic.transform.parent = this.mMainMissionLineList[0].transform;
				this.MissionLineChoosePic.transform.localPosition = Vector3.zero;
				this.OnClickTabRootBtn(this.MissionTabArray[0].gameObject);
			}
			else
			{
				this.AutoChoiceMission();
			}
			break;
		case 1:
			if (this.mDailyMissionLineList.Count != 0)
			{
				UnityVersionUtil.SetActiveRecursive(this.MissionLineChoosePic.gameObject, true);
				this.MissionLineChoosePic.transform.parent = this.mDailyMissionLineList[0].transform;
				this.MissionLineChoosePic.transform.localPosition = Vector3.zero;
				this.OnClickTabRootBtn(this.MissionTabArray[1].gameObject);
			}
			else
			{
				this.AutoChoiceMission();
			}
			break;
		case 2:
			if (this.curSideMissionList.Count != 0)
			{
				UnityVersionUtil.SetActiveRecursive(this.MissionLineChoosePic.gameObject, true);
				this.MissionLineChoosePic.transform.parent = this.mSideMissionLineList[0].transform;
				this.MissionLineChoosePic.transform.localPosition = Vector3.zero;
				this.OnClickTabRootBtn(this.MissionTabArray[2].gameObject);
			}
			else
			{
				this.AutoChoiceMission();
			}
			break;
		}
	}

	// Token: 0x060042FE RID: 17150 RVA: 0x001491D8 File Offset: 0x001473D8
	private void ResetMissionTab(Transform missionLineRoot, List<MissionData> dataList, List<MissionLineLogic> curMisLine)
	{
		int num = dataList.Count - curMisLine.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.missionLinePrefab) as GameObject;
				gameObject.transform.parent = missionLineRoot;
				gameObject.transform.localScale = Vector3.one;
				MissionLineLogic component = gameObject.GetComponent<MissionLineLogic>();
				curMisLine.Add(component);
			}
		}
		else if (num < 0)
		{
			for (int j = 0; j > num; j--)
			{
				int num2 = curMisLine.Count - 1 + j;
				MissionLineLogic missionLineLogic = curMisLine[num2];
				curMisLine.RemoveAt(num2);
				Object.Destroy(missionLineLogic.gameObject);
			}
		}
		for (int k = 0; k < curMisLine.Count; k++)
		{
			if (k < dataList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(curMisLine[k].gameObject, true);
				curMisLine[k].Reset(dataList[k]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(curMisLine[k].gameObject, false);
			}
		}
	}

	// Token: 0x060042FF RID: 17151 RVA: 0x001492FC File Offset: 0x001474FC
	public void ResetMissionDataPage(MissionLineLogic missionLine)
	{
		if (!UnityVersionUtil.IsActive(this.MissionPageRoot))
		{
			UnityVersionUtil.SetActiveRecursive(this.MissionPageRoot, true);
		}
		MissionData curMissionData = missionLine.CurMissionData;
		this.MissionTitleLabel.text = curMissionData.MTipDescribeID;
		this.MissionDiscribeLabel.text = curMissionData.MDescribeID;
		MISSION_STATE missionState = SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetMissionState(curMissionData.ID);
		MissionManager.GetMissionStateLabel(curMissionData, missionState, this.MissionStateLabel);
		PROFESSION_TYPE profession = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession;
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		if (curMissionData.Class == 0)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, curMissionData.ID, REWARD_TYPE.DAILY_MISSION, level, profession, 0);
			NGUITools.SetActive(this.AbandonBtn, true);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100311}", new object[0]);
			NGUITools.SetActive(this.MissionDiscribeLabel.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, true);
			int num = (int)SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetMissionParam(curMissionData.ID, 1);
			if (num < 0)
			{
				num = level;
			}
			ShowRewardManager.ShowRewardItem(this.DailyFullShowReward, curMissionData.ID, REWARD_TYPE.DAILY_MISSION, num, profession, 1);
		}
		else if (curMissionData.Class == 1)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, curMissionData.ID, REWARD_TYPE.MISSION, level, profession, 0);
			NGUITools.SetActive(this.AbandonBtn, false);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDiscribeLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (curMissionData.Class == 4 || curMissionData.Class == 5)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, curMissionData.ID, REWARD_TYPE.ESCORT, level, profession, 0);
			NGUITools.SetActive(this.AbandonBtn, true);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDiscribeLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (curMissionData.Class == 3)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, curMissionData.ID, REWARD_TYPE.DAILY_MISSION, level, profession, 1);
			NGUITools.SetActive(this.AbandonBtn, false);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDiscribeLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (curMissionData.Class == 2)
		{
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, curMissionData.ID, REWARD_TYPE.MISSION, level, profession, 0);
			NGUITools.SetActive(this.AbandonBtn, false);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDiscribeLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		else if (curMissionData.Class == 6)
		{
			int num2 = (int)SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.GetMissionParam(curMissionData.ID, 1);
			if (num2 < 0)
			{
				num2 = level;
			}
			ShowRewardManager.ShowRewardItem(this.ShowRewardItem, curMissionData.ID, REWARD_TYPE.DAILY_MISSION, num2, profession, 1);
			NGUITools.SetActive(this.AbandonBtn, false);
			this.StaticMissionTitle2Label.text = StrDictionary.GetDictionaryString("#{100307}", new object[0]);
			NGUITools.SetActive(this.MissionDiscribeLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.DailyFullShowReward.gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.MissionLineChoosePic.gameObject, true);
		this.MissionLineChoosePic.transform.parent = missionLine.transform;
		this.MissionLineChoosePic.transform.localPosition = Vector3.zero;
		this.MissionLineChoosePic.transform.localScale = Vector3.one;
		this.curMissionId = missionLine.CurMissionData.ID;
	}

	// Token: 0x06004300 RID: 17152 RVA: 0x001496F4 File Offset: 0x001478F4
	public void OnClickAbandonBtn()
	{
		SingletonDontDestoryUnity<GameManager>.Instance.MissionManager.AbandonMission(this.curMissionId, false);
	}

	// Token: 0x06004301 RID: 17153 RVA: 0x00149710 File Offset: 0x00147910
	public void OnClickGoBtn()
	{
		this.OnClickCloseBtn();
		MissionManager.ClickMissionAction(this.curMissionId);
	}

	// Token: 0x06004302 RID: 17154 RVA: 0x00149724 File Offset: 0x00147924
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MissionPageRoot);
	}

	// Token: 0x06004303 RID: 17155 RVA: 0x00149738 File Offset: 0x00147938
	public void CheckTabState(int tabIndex)
	{
		for (int i = 0; i < this.lineSps.Length; i++)
		{
			if (i == this.curSelectLines)
			{
				this.lineSps[i].spriteName = "CZ_huaDongBG_1";
			}
			else
			{
				this.lineSps[i].spriteName = "CZ_huaDongBG";
			}
		}
	}

	// Token: 0x06004304 RID: 17156 RVA: 0x00149794 File Offset: 0x00147994
	public void OnClickTabRootBtn(GameObject obj)
	{
		if (obj.name.Equals("0"))
		{
			if (this.mMainMissionLineList.Count != 0)
			{
				this.OpenSubline(0);
				this.ResetMissionDataPage(this.mMainMissionLineList[0]);
			}
		}
		else if (obj.name.Equals("1"))
		{
			if (this.mDailyMissionLineList.Count != 0)
			{
				this.OpenSubline(1);
				this.ResetMissionDataPage(this.mDailyMissionLineList[0]);
			}
		}
		else if (obj.name.Equals("2"))
		{
			if (this.mSideMissionLineList.Count != 0)
			{
				this.OpenSubline(2);
				this.ResetMissionDataPage(this.mSideMissionLineList[0]);
			}
		}
	}

	// Token: 0x06004305 RID: 17157 RVA: 0x00149874 File Offset: 0x00147A74
	public void OpenSubline(int tabindex)
	{
		if (this.isRefershFlag)
		{
			this.curSelectLines = tabindex;
			if (!this.lineisOpenFlag[tabindex])
			{
				this.subLineFlagAnim[tabindex].PlayForward();
				this.subLineScaleAnim[tabindex].PlayForward();
				this.lineisOpenFlag[tabindex] = true;
			}
		}
		else
		{
			this.curSelectLines = tabindex;
			if (this.lineisOpenFlag[tabindex])
			{
				this.subLineFlagAnim[tabindex].PlayReverse();
				this.subLineScaleAnim[tabindex].PlayReverse();
				this.lineisOpenFlag[tabindex] = false;
				this.isOpening = false;
			}
			else
			{
				this.subLineFlagAnim[tabindex].PlayForward();
				this.subLineScaleAnim[tabindex].PlayForward();
				this.lineisOpenFlag[tabindex] = true;
				this.isOpening = true;
			}
		}
		this.isRefershFlag = false;
		for (int i = 0; i < this.lineisOpenFlag.Length; i++)
		{
			if (i != tabindex && this.lineisOpenFlag[i])
			{
				this.subLineFlagAnim[i].PlayReverse();
				this.subLineScaleAnim[i].PlayReverse();
				this.lineisOpenFlag[i] = false;
			}
		}
		this.CheckTabState(tabindex);
	}

	// Token: 0x04002F5D RID: 12125
	public UILabel MissionTitleLabel;

	// Token: 0x04002F5E RID: 12126
	public UILabel MissionDiscribeLabel;

	// Token: 0x04002F5F RID: 12127
	public UILabel MissionStateLabel;

	// Token: 0x04002F60 RID: 12128
	public GameObject MissionLineChoosePic;

	// Token: 0x04002F61 RID: 12129
	public Transform[] MissionTabArray;

	// Token: 0x04002F62 RID: 12130
	public Transform[] MissionLineRootArray;

	// Token: 0x04002F63 RID: 12131
	public bool[] lineisOpenFlag;

	// Token: 0x04002F64 RID: 12132
	public ShowRewardItems ShowRewardItem;

	// Token: 0x04002F65 RID: 12133
	private List<MissionLineLogic> mMainMissionLineList = new List<MissionLineLogic>();

	// Token: 0x04002F66 RID: 12134
	private List<MissionLineLogic> mSideMissionLineList = new List<MissionLineLogic>();

	// Token: 0x04002F67 RID: 12135
	private List<MissionLineLogic> mDailyMissionLineList = new List<MissionLineLogic>();

	// Token: 0x04002F68 RID: 12136
	public GameObject missionLinePrefab;

	// Token: 0x04002F69 RID: 12137
	public GameObject MissionPageRoot;

	// Token: 0x04002F6A RID: 12138
	public GameObject AbandonBtn;

	// Token: 0x04002F6B RID: 12139
	public UITable Table;

	// Token: 0x04002F6C RID: 12140
	public UILabel StaticMissionTitle2Label;

	// Token: 0x04002F6D RID: 12141
	public ShowRewardItems DailyFullShowReward;

	// Token: 0x04002F6E RID: 12142
	private string curMissionId;

	// Token: 0x04002F6F RID: 12143
	public TweenRotation[] subLineFlagAnim;

	// Token: 0x04002F70 RID: 12144
	public UISprite[] lineSps;

	// Token: 0x04002F71 RID: 12145
	public TweenScale[] subLineScaleAnim;

	// Token: 0x04002F72 RID: 12146
	private int curSelectLines = -1;

	// Token: 0x04002F73 RID: 12147
	private bool isRefershFlag;

	// Token: 0x04002F74 RID: 12148
	private bool isOpening;

	// Token: 0x04002F75 RID: 12149
	private List<MissionData> curMainMissionList = new List<MissionData>();

	// Token: 0x04002F76 RID: 12150
	private List<MissionData> curSideMissionList = new List<MissionData>();

	// Token: 0x04002F77 RID: 12151
	private List<MissionData> curDailyMissionList = new List<MissionData>();
}
