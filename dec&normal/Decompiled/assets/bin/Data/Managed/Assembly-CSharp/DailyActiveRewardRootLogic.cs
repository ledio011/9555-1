using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008DA RID: 2266
public class DailyActiveRewardRootLogic : SingletonUnity<DailyActiveRewardRootLogic>
{
	// Token: 0x06003D38 RID: 15672 RVA: 0x001104FC File Offset: 0x0010E6FC
	protected override void Awake()
	{
		base.Awake();
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06003D39 RID: 15673 RVA: 0x00110544 File Offset: 0x0010E744
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.DailyActiveRewardRoot);
	}

	// Token: 0x06003D3A RID: 15674 RVA: 0x00110564 File Offset: 0x0010E764
	public void EnableReset()
	{
		for (int i = 0; i < this.LineItemsList.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.LineItemsList[i].gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.rewardObj, false);
	}

	// Token: 0x06003D3B RID: 15675 RVA: 0x001105B0 File Offset: 0x0010E7B0
	public void Reset(ret_request_daily_active.request request)
	{
		if (request.HasDaily_actives)
		{
			this.activeList = new List<daily_active>(request.daily_actives.Values);
		}
		this.activeList.Sort(delegate(daily_active x, daily_active y)
		{
			if (x.ID.Length == y.ID.Length)
			{
				return x.ID.CompareTo(y.ID);
			}
			return x.ID.Length - y.ID.Length;
		});
		this.activeList = this.CheckActivityList(this.activeList);
		if (request.HasDaily_rewards)
		{
			this.rewardList = new List<daily_reward>(request.daily_rewards.Values);
		}
		this.rewardList.Sort(delegate(daily_reward x, daily_reward y)
		{
			if (x.ID.Length == y.ID.Length)
			{
				return x.ID.CompareTo(y.ID);
			}
			return x.ID.Length - y.ID.Length;
		});
		this.RewardDataList.Clear();
		for (int i = 0; i < this.rewardList.Count; i++)
		{
			DailyActiveRewardData dailyActiveRewardDataById = DataManager.GetDailyActiveRewardDataById(this.rewardList[i].ID);
			if (dailyActiveRewardDataById != null)
			{
				this.RewardDataList.Add(dailyActiveRewardDataById);
			}
		}
		if (this.RewardDataList.Count > 0)
		{
			this.MaxActive = this.RewardDataList[this.RewardDataList.Count - 1].Score;
		}
		this.curScore = (int)request.score;
		int num = Mathf.Min(this.activeList.Count, this.lineMinCount) - this.LineItemsList.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.LineItemsList[0].gameObject) as GameObject;
				DailyActiveLineLogic component = gameObject.GetComponent<DailyActiveLineLogic>();
				gameObject.name = string.Format("line{0:D2}", this.LineItemsList.Count);
				gameObject.transform.parent = this.uiWrapContent.transform;
				gameObject.transform.localScale = Vector3.one;
				this.LineItemsList.Add(component);
			}
		}
		for (int k = 0; k < this.LineItemsList.Count; k++)
		{
			if (k < this.activeList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItemsList[k].gameObject, true);
				this.LineItemsList[k].UpdateInfo(this.activeList[k]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItemsList[k].gameObject, false);
			}
		}
		this.uiWrapContent.minIndex = 1 - this.activeList.Count;
		this.WrapContentBottomWidget.height = this.activeList.Count * this.uiWrapContent.itemSize;
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
		UnityVersionUtil.SetActiveRecursive(this.rewardObj, true);
		num = this.rewardList.Count - this.rewardItemsList.Count;
		if (num > 0)
		{
			for (int l = 0; l < num; l++)
			{
				GameObject gameObject2 = Object.Instantiate(this.rewardItemsList[0].gameObject) as GameObject;
				DailyActiveRewardItem component2 = gameObject2.GetComponent<DailyActiveRewardItem>();
				gameObject2.name = string.Format("reward{0:D2}", this.rewardItemsList.Count);
				gameObject2.transform.parent = this.RewardGrid.transform;
				gameObject2.transform.localScale = Vector3.one;
				this.rewardItemsList.Add(component2);
			}
		}
		for (int m = 0; m < this.rewardItemsList.Count; m++)
		{
			if (m < this.rewardList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.rewardItemsList[m].gameObject, true);
				this.rewardItemsList[m].UpdateInfo(this.rewardList[m]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.rewardItemsList[m].gameObject, false);
			}
		}
		this.RewardGrid.Reposition();
		this.SetActiveSlider((int)request.score);
		this.ActiveLabel.text = string.Format("{0}/{1}", (int)request.score, this.MaxActive);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "DailyActive", "open");
	}

	// Token: 0x06003D3C RID: 15676 RVA: 0x00110A2C File Offset: 0x0010EC2C
	private void SetActiveSlider(int score)
	{
		int num = -1;
		for (int i = 0; i < this.RewardDataList.Count; i++)
		{
			if (score <= this.RewardDataList[i].Score)
			{
				num = i;
				break;
			}
		}
		if (score > this.MaxActive)
		{
			this.ActiveSlider.value = 1f;
		}
		else if (num > 0)
		{
			this.ActiveSlider.value = this.sliderValue[num - 1] + (this.sliderValue[num] - this.sliderValue[num - 1]) * ((float)(score - this.RewardDataList[num - 1].Score) / (float)(this.RewardDataList[num].Score - this.RewardDataList[num - 1].Score));
		}
		else if (this.RewardDataList.Count > 0)
		{
			this.ActiveSlider.value = this.sliderValue[0] * ((float)score / (float)this.RewardDataList[0].Score);
		}
		else
		{
			this.ActiveSlider.value = 0f;
		}
	}

	// Token: 0x06003D3D RID: 15677 RVA: 0x00110B58 File Offset: 0x0010ED58
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		DailyActiveLineLogic itemLogic = this.LineItemsList[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x06003D3E RID: 15678 RVA: 0x00110B80 File Offset: 0x0010ED80
	private void ResetItemLine(DailyActiveLineLogic itemLogic, int idx)
	{
		if (idx < this.activeList.Count)
		{
			itemLogic.UpdateInfo(this.activeList[idx]);
		}
	}

	// Token: 0x06003D3F RID: 15679 RVA: 0x00110BA8 File Offset: 0x0010EDA8
	public void UpdateInfo(string id)
	{
		for (int i = 0; i < this.rewardList.Count; i++)
		{
			if (this.rewardList[i].ID.Equals(id))
			{
				this.rewardList[i].state = 2L;
			}
		}
		for (int j = 0; j < this.rewardItemsList.Count; j++)
		{
			this.rewardItemsList[j].UpdateInfo(this.rewardList[j]);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "DailyActive", string.Format("require_{0}", id));
	}

	// Token: 0x06003D40 RID: 15680 RVA: 0x00110C58 File Offset: 0x0010EE58
	public List<daily_active> CheckActivityList(List<daily_active> list)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		for (int i = list.Count - 1; i >= 0; i--)
		{
			DailyActiveData dailyActiveDataById = DataManager.GetDailyActiveDataById(list[i].ID);
			if (dailyActiveDataById.Type == 9)
			{
				RefineData[] array = new RefineData[5];
				int num = 0;
				for (int j = 1; j < 5; j++)
				{
					array[j] = DataManager.GetRefineDataByPartLevelPRO(j, playerData.MainPlayerAttrData.GetTargetRefinePartLevel((REFINE_PART)j), (int)playerData.Profession);
					num += array[j].Lv;
				}
				if (num >= 40)
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 10)
			{
				ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
				int num2 = 0;
				for (int k = 0; k < mainPlayer.CharacterSkillData.Count; k++)
				{
					CharacterSkillData characterSkillData = mainPlayer.CharacterSkillData[k];
					if (characterSkillData != null && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(characterSkillData.UnlockLevel))
					{
						int index = characterSkillData.Index;
						if (index > 3)
						{
							SkillData skillDataById = DataManager.GetSkillDataById(characterSkillData.ID);
							if (skillDataById != null && skillDataById.IsUpgrade == 1)
							{
								num2 += characterSkillData.Level + 1;
							}
						}
					}
				}
				if (num2 >= 400)
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 0)
			{
				CopySceneData copySceneDataById = DataManager.GetCopySceneDataById("901");
				if (!playerData.CheckLevel(copySceneDataById.MinLevel))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 1)
			{
				CopySceneData copySceneDataById2 = DataManager.GetCopySceneDataById("223");
				if (copySceneDataById2 == null)
				{
					list.RemoveAt(i);
				}
				if (!playerData.CheckLevel(copySceneDataById2.MinLevel))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 2)
			{
				CopySceneData copySceneDataById3 = DataManager.GetCopySceneDataById("201");
				if (!playerData.CheckLevel(copySceneDataById3.MinLevel))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 3)
			{
				CopySceneData copySceneDataById4 = DataManager.GetCopySceneDataById("211");
				if (!playerData.CheckLevel(copySceneDataById4.MinLevel))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 4)
			{
				EscortData escortDataById = DataManager.GetEscortDataById("30002");
				if (!playerData.CheckLevel(escortDataById.UnlockLevel))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 5)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_CHALLENGE))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 6)
			{
				WildBossData wildBossDataByID = DataManager.GetWildBossDataByID("801");
				if (!playerData.CheckLevel(wildBossDataByID.LevelMin))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 7)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.RANK_PVP))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 8)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.ENHANCE_EQUIP))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 9)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.ENHANCE_STAR))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 10)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.SKILL))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 11)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_EQUIP))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 12)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_TOOL))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 13)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.GIFT_DAILY))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 14)
			{
				EscortData escortDataById2 = DataManager.GetEscortDataById("30001");
				if (!playerData.CheckLevel(escortDataById2.UnlockLevel))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 15)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.DOMIN))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 16)
			{
				CopySceneData copySceneDataById5 = DataManager.GetCopySceneDataById("1201");
				if (!playerData.CheckLevel(copySceneDataById5.MinLevel))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 18)
			{
				SurviveBattleData surviveBattleDataById = DataManager.GetSurviveBattleDataById("1101");
				if (!playerData.CheckLevel(surviveBattleDataById.UnlockLevel))
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 19)
			{
				if (!playerData.IsHaveGuild())
				{
					list.RemoveAt(i);
				}
			}
			else if (dailyActiveDataById.Type == 20 && !playerData.IsHaveGuild())
			{
				list.RemoveAt(i);
			}
		}
		return list;
	}

	// Token: 0x06003D41 RID: 15681 RVA: 0x00111180 File Offset: 0x0010F380
	public bool CheckUnlockFunction(FUNCTION_TYPE curFunction)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		return playerCommonData.IsFunctionUnlock(curFunction);
	}

	// Token: 0x0400289F RID: 10399
	public UIWrapContentNew uiWrapContent;

	// Token: 0x040028A0 RID: 10400
	private int lineMinCount = 6;

	// Token: 0x040028A1 RID: 10401
	public UIWidget WrapContentBottomWidget;

	// Token: 0x040028A2 RID: 10402
	public UIScrollView uiScrollView;

	// Token: 0x040028A3 RID: 10403
	public List<DailyActiveLineLogic> LineItemsList;

	// Token: 0x040028A4 RID: 10404
	public List<DailyActiveRewardItem> rewardItemsList;

	// Token: 0x040028A5 RID: 10405
	public GameObject rewardObj;

	// Token: 0x040028A6 RID: 10406
	public UIGrid RewardGrid;

	// Token: 0x040028A7 RID: 10407
	public UISlider ActiveSlider;

	// Token: 0x040028A8 RID: 10408
	public UILabel ActiveLabel;

	// Token: 0x040028A9 RID: 10409
	private int MaxActive = 100;

	// Token: 0x040028AA RID: 10410
	private List<daily_active> activeList;

	// Token: 0x040028AB RID: 10411
	private List<daily_reward> rewardList;

	// Token: 0x040028AC RID: 10412
	private List<DailyActiveRewardData> RewardDataList = new List<DailyActiveRewardData>();

	// Token: 0x040028AD RID: 10413
	private int curScore;

	// Token: 0x040028AE RID: 10414
	private float[] sliderValue = new float[]
	{
		0.175f,
		0.45f,
		0.7f,
		1f
	};
}
