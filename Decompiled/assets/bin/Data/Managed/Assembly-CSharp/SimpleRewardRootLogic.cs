using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000993 RID: 2451
public class SimpleRewardRootLogic : SingletonUnity<SimpleRewardRootLogic>
{
	// Token: 0x06004568 RID: 17768 RVA: 0x0015C750 File Offset: 0x0015A950
	public static void ResetSimpleReward(List<item> rewardList)
	{
		if (SingletonUnity<SimpleRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SimpleRewardRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SimpleRewardRootLogic>.Instance.ResetReward(rewardList);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SimpleRewardRoot, delegate
			{
				SingletonUnity<SimpleRewardRootLogic>.Instance.ResetReward(rewardList);
			}, null);
		}
	}

	// Token: 0x06004569 RID: 17769 RVA: 0x0015C7BC File Offset: 0x0015A9BC
	public static void AddRewards(List<item> list)
	{
		if (list != null && list.Count > 0)
		{
			if (SingletonUnity<SimpleRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SimpleRewardRootLogic>.Instance.gameObject))
			{
				for (int i = 0; i < list.Count; i++)
				{
					SimpleRewardRootLogic.AddReward(list[i]);
				}
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SimpleRewardRoot, delegate
				{
					for (int j = 0; j < list.Count; j++)
					{
						SimpleRewardRootLogic.AddReward(list[j]);
					}
				}, null);
			}
		}
	}

	// Token: 0x0600456A RID: 17770 RVA: 0x0015C860 File Offset: 0x0015AA60
	public static void AddReward(item rewardItem)
	{
		if (rewardItem == null)
		{
			return;
		}
		if (DataManager.GetItemDataByID(rewardItem.itemId) == null)
		{
			return;
		}
		if (SingletonUnity<SimpleRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SimpleRewardRootLogic>.Instance.gameObject))
		{
			SingletonUnity<SimpleRewardRootLogic>.Instance.AddRewardItem(rewardItem);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SimpleRewardRoot, delegate
			{
				SingletonUnity<SimpleRewardRootLogic>.Instance.AddRewardItem(rewardItem);
			}, null);
		}
	}

	// Token: 0x0600456B RID: 17771 RVA: 0x0015C8F0 File Offset: 0x0015AAF0
	public static void AddRewards(List<GameItem> list)
	{
		if (list != null && list.Count > 0)
		{
			if (SingletonUnity<SimpleRewardRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<SimpleRewardRootLogic>.Instance.gameObject))
			{
				for (int i = 0; i < list.Count; i++)
				{
					SimpleRewardRootLogic.AddReward(list[i]);
				}
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SimpleRewardRoot, delegate
				{
					for (int j = 0; j < list.Count; j++)
					{
						SimpleRewardRootLogic.AddReward(list[j]);
					}
				}, null);
			}
		}
	}

	// Token: 0x0600456C RID: 17772 RVA: 0x0015C994 File Offset: 0x0015AB94
	public static void AddReward(GameItem gameitem)
	{
		SimpleRewardRootLogic.AddReward(new item
		{
			itemId = gameitem.ItemId,
			itemCount = (long)gameitem.StackNum,
			quality = (long)gameitem.Quality
		});
	}

	// Token: 0x0600456D RID: 17773 RVA: 0x0015C9D4 File Offset: 0x0015ABD4
	public static void AddReward(ItemData rewardItem, int count, int quality)
	{
		SimpleRewardRootLogic.AddReward(new item
		{
			itemId = rewardItem.ID,
			itemCount = (long)count,
			quality = (long)quality
		});
	}

	// Token: 0x0600456E RID: 17774 RVA: 0x0015CA0C File Offset: 0x0015AC0C
	public void AddRewardItem(item rewardItem)
	{
		this.mLineReward.Add(rewardItem);
		this.mLineRewardItemData.Add(DataManager.GetItemDataByID(rewardItem.itemId));
	}

	// Token: 0x0600456F RID: 17775 RVA: 0x0015CA3C File Offset: 0x0015AC3C
	public void ResetReward(List<item> rewardList)
	{
		this.mLineReward.Clear();
		this.mLineRewardItemData.Clear();
		this.mIconReward.Clear();
		this.mIconRewardItemData.Clear();
		for (int i = 0; i < rewardList.Count; i++)
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(rewardList[i].itemId);
			if (this.IsSimpleLineReward(itemDataByID))
			{
				this.mLineReward.Add(rewardList[i]);
				this.mLineRewardItemData.Add(itemDataByID);
			}
			else
			{
				this.mIconReward.Add(rewardList[i]);
				this.mIconRewardItemData.Add(itemDataByID);
			}
		}
	}

	// Token: 0x06004570 RID: 17776 RVA: 0x0015CAEC File Offset: 0x0015ACEC
	private bool IsSimpleLineReward(ItemData itemData)
	{
		return itemData.Type == GameDefine.ITEM_TYPE.ADD_COIN || itemData.Type == GameDefine.ITEM_TYPE.ADD_DIAMOND || itemData.Type == GameDefine.ITEM_TYPE.ADD_EXP || itemData.Type == GameDefine.ITEM_TYPE.ADD_GOLD || itemData.Type == GameDefine.ITEM_TYPE.ADD_HONOR;
	}

	// Token: 0x06004571 RID: 17777 RVA: 0x0015CB3C File Offset: 0x0015AD3C
	private void Update()
	{
		if (this.mLineReward.Count > 0 && Time.time - this.lastLineRewardShowTime > this.LINE_REWARD_INTERVAL)
		{
			this.lastLineRewardShowTime = Time.time;
			SimpleRewardLine simpleRewardLine;
			if (this.DisableList.Count > 0)
			{
				simpleRewardLine = this.DisableList[0];
				this.DisableList.RemoveAt(0);
			}
			else
			{
				GameObject gameObject = Object.Instantiate(this.PrefabLine.gameObject) as GameObject;
				gameObject.transform.parent = this.PrefabLine.transform.parent;
				gameObject.transform.localScale = Vector3.one;
				simpleRewardLine = gameObject.GetComponent<SimpleRewardLine>();
			}
			this.EnableList.Add(simpleRewardLine);
			simpleRewardLine.Reset(this.mLineRewardItemData[0], (int)this.mLineReward[0].itemCount, (EQUIP_QUALITY)this.mLineReward[0].quality, new SimpleRewardLine.OnFinished(this.OnRecycleRewardLine));
			this.mLineReward.RemoveAt(0);
			this.mLineRewardItemData.RemoveAt(0);
		}
	}

	// Token: 0x06004572 RID: 17778 RVA: 0x0015CC5C File Offset: 0x0015AE5C
	private void OnRecycleRewardLine(SimpleRewardLine line)
	{
		UnityVersionUtil.SetActiveRecursive(line.gameObject, false);
		this.EnableList.Remove(line);
		this.DisableList.Add(line);
		if (this.EnableList.Count == 0 && this.mLineReward.Count == 0)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SimpleRewardRoot);
		}
	}

	// Token: 0x0400324A RID: 12874
	public List<SimpleRewardLine> DisableList;

	// Token: 0x0400324B RID: 12875
	public List<SimpleRewardLine> EnableList;

	// Token: 0x0400324C RID: 12876
	public SimpleRewardLine PrefabLine;

	// Token: 0x0400324D RID: 12877
	private List<item> mLineReward = new List<item>();

	// Token: 0x0400324E RID: 12878
	private List<ItemData> mLineRewardItemData = new List<ItemData>();

	// Token: 0x0400324F RID: 12879
	private List<item> mIconReward = new List<item>();

	// Token: 0x04003250 RID: 12880
	private List<ItemData> mIconRewardItemData = new List<ItemData>();

	// Token: 0x04003251 RID: 12881
	private float lastLineRewardShowTime;

	// Token: 0x04003252 RID: 12882
	private float LINE_REWARD_INTERVAL = 0.5f;
}
