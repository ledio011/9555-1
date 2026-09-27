using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200097F RID: 2431
public class RankPVPRewardRootLogic : SingletonUnity<RankPVPRewardRootLogic>
{
	// Token: 0x060044AD RID: 17581 RVA: 0x001572AC File Offset: 0x001554AC
	protected override void Awake()
	{
		base.Awake();
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x060044AE RID: 17582 RVA: 0x001572F4 File Offset: 0x001554F4
	public void Reset()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		LadderRewardData ladderRewardDataByRank = DataManager.GetLadderRewardDataByRank(playerData.RankPVPData.RankPVPRankNum);
		this.AllRewardList = DataManager.GetLadderRewardDataList();
		this.AllRewardList.Sort(delegate(LadderRewardData x, LadderRewardData y)
		{
			if (x.ID.Length == y.ID.Length)
			{
				return x.ID.CompareTo(y.ID);
			}
			return x.ID.Length - y.ID.Length;
		});
		int num = Mathf.Min(this.AllRewardList.Count, this.lineMinCount) - this.rewardList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.rewardList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.uiWrapContent.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("{0:d2}", this.rewardList.Count);
				this.rewardList.Add(gameObject.GetComponent<RankPVPLineReward>());
			}
		}
		for (int j = 0; j < this.rewardList.Count; j++)
		{
			if (j < this.AllRewardList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.rewardList[j].gameObject, true);
				this.rewardList[j].ShowRewardInfo(this.AllRewardList[j]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.rewardList[j].gameObject, false);
			}
		}
		this.uiWrapContent.minIndex = 1 - this.AllRewardList.Count;
		this.WrapContentBottomWidget.height = this.AllRewardList.Count * this.uiWrapContent.itemSize;
		if (this.AllRewardList.Count == 1)
		{
			this.uiWrapContent.maxIndex = 1;
		}
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
		if (ladderRewardDataByRank != null)
		{
			UnityVersionUtil.SetActiveRecursive(this.MyReward.gameObject, true);
			this.MyReward.ShowRewardInfo(ladderRewardDataByRank);
			this.OutRankLabel.enabled = false;
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.MyReward.gameObject, false);
			this.OutRankLabel.enabled = true;
			this.OutRankLabel.text = StrDictionary.GetDictionaryString("#{101021}", new object[0]);
		}
	}

	// Token: 0x060044AF RID: 17583 RVA: 0x00157588 File Offset: 0x00155788
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RankPvpShowRewardRoot);
	}

	// Token: 0x060044B0 RID: 17584 RVA: 0x0015759C File Offset: 0x0015579C
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		RankPVPLineReward itemLogic = this.rewardList[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x060044B1 RID: 17585 RVA: 0x001575C4 File Offset: 0x001557C4
	private void ResetItemLine(RankPVPLineReward itemLogic, int idx)
	{
		if (idx < this.AllRewardList.Count)
		{
			itemLogic.ShowRewardInfo(this.AllRewardList[idx]);
		}
	}

	// Token: 0x04003164 RID: 12644
	public List<RankPVPLineReward> rewardList;

	// Token: 0x04003165 RID: 12645
	public RankPVPLineReward MyReward;

	// Token: 0x04003166 RID: 12646
	public UILabel OutRankLabel;

	// Token: 0x04003167 RID: 12647
	public UIWrapContentNew uiWrapContent;

	// Token: 0x04003168 RID: 12648
	private int lineMinCount = 6;

	// Token: 0x04003169 RID: 12649
	public UIWidget WrapContentBottomWidget;

	// Token: 0x0400316A RID: 12650
	private List<LadderRewardData> AllRewardList = new List<LadderRewardData>();

	// Token: 0x0400316B RID: 12651
	public UIScrollView uiScrollView;
}
