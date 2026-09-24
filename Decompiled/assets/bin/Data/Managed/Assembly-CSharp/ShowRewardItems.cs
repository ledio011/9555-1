using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A46 RID: 2630
[ExecuteInEditMode]
public class ShowRewardItems : MonoBehaviour
{
	// Token: 0x06004CB2 RID: 19634 RVA: 0x001A016C File Offset: 0x0019E36C
	private void Awake()
	{
	}

	// Token: 0x06004CB3 RID: 19635 RVA: 0x001A0170 File Offset: 0x0019E370
	public void ShowRewards(Dictionary<string, item> items)
	{
		if (items == null || items.Count == 0)
		{
			for (int i = 0; i < this.rewardItmes.Count; i++)
			{
				NGUITools.SetActive(this.rewardItmes[i].gameObject, false);
			}
			return;
		}
		this.ShowRewards(new List<item>(items.Values));
	}

	// Token: 0x06004CB4 RID: 19636 RVA: 0x001A01D4 File Offset: 0x0019E3D4
	public void ShowRewards(List<item> items)
	{
		if (items == null || items.Count == 0)
		{
			for (int i = 0; i < this.rewardItmes.Count; i++)
			{
				NGUITools.SetActive(this.rewardItmes[i].gameObject, false);
			}
			return;
		}
		items.Sort(delegate(item x, item y)
		{
			if (x.itemId.Length != y.itemId.Length)
			{
				return -x.itemId.Length + y.itemId.Length;
			}
			return y.itemId.CompareTo(x.itemId);
		});
		int num = items.Count - this.rewardItmes.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.rewardItmes[0].gameObject) as GameObject;
				this.grid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("zhuangBei{0:d2}", this.rewardItmes.Count);
				this.rewardItmes.Add(gameObject.GetComponent<RewardItem>());
			}
		}
		for (int k = 0; k < this.rewardItmes.Count; k++)
		{
			NGUITools.SetActive(this.rewardItmes[k].gameObject, k < items.Count);
		}
		int num2 = 0;
		for (int l = 0; l < items.Count; l++)
		{
			this.rewardItmes[num2++].UpdateItem(items[l].itemId, (int)items[l].quality, (int)items[l].itemCount, (!items[l].HasCount2) ? 0 : ((int)items[l].count2));
		}
		this.grid.Reposition();
	}

	// Token: 0x06004CB5 RID: 19637 RVA: 0x001A03C8 File Offset: 0x0019E5C8
	public void ShowRewards(List<string> itemIds, List<int> qualitys, List<int> counts)
	{
		if (itemIds == null)
		{
			for (int i = 0; i < this.rewardItmes.Count; i++)
			{
				NGUITools.SetActive(this.rewardItmes[i].gameObject, false);
			}
			return;
		}
		int num = itemIds.Count - this.rewardItmes.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.rewardItmes[0].gameObject) as GameObject;
				this.grid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("zhuangBei{0:d2}", this.rewardItmes.Count);
				this.rewardItmes.Add(gameObject.GetComponent<RewardItem>());
			}
		}
		for (int k = 0; k < this.rewardItmes.Count; k++)
		{
			NGUITools.SetActive(this.rewardItmes[k].gameObject, k < itemIds.Count);
		}
		for (int l = 0; l < itemIds.Count; l++)
		{
			this.rewardItmes[l].UpdateItem(itemIds[l], qualitys[l], counts[l], 0);
		}
		this.grid.Reposition();
	}

	// Token: 0x06004CB6 RID: 19638 RVA: 0x001A0550 File Offset: 0x0019E750
	public void ShowRewards(ItemData itemData, int itemCount = 1)
	{
		for (int i = 0; i < this.rewardItmes.Count; i++)
		{
			NGUITools.SetActive(this.rewardItmes[i].gameObject, i < 1);
		}
		this.rewardItmes[0].UpdateItem(itemData.ID, itemData.QualityType, itemCount, 0);
		this.grid.Reposition();
	}

	// Token: 0x06004CB7 RID: 19639 RVA: 0x001A05C0 File Offset: 0x0019E7C0
	public void ShowRewards(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		if (itemIds == null)
		{
			for (int i = 0; i < this.rewardItmes.Count; i++)
			{
				NGUITools.SetActive(this.rewardItmes[i].gameObject, false);
			}
			return;
		}
		int num = itemIds.Count - this.rewardItmes.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.rewardItmes[0].gameObject) as GameObject;
				this.grid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("zhuangBei{0:d2}", this.rewardItmes.Count);
				this.rewardItmes.Add(gameObject.GetComponent<RewardItem>());
			}
		}
		for (int k = 0; k < this.rewardItmes.Count; k++)
		{
			NGUITools.SetActive(this.rewardItmes[k].gameObject, k < itemIds.Count);
		}
		for (int l = 0; l < itemIds.Count; l++)
		{
			this.rewardItmes[l].UpdateItem(itemIds[l], qualitys[l], counts[l], 0);
		}
		this.grid.Reposition();
	}

	// Token: 0x04003A53 RID: 14931
	public UIGrid grid;

	// Token: 0x04003A54 RID: 14932
	public List<RewardItem> rewardItmes = new List<RewardItem>();
}
