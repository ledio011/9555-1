using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200099D RID: 2461
public class SlotLogRootLogic : SingletonUnity<SlotLogRootLogic>
{
	// Token: 0x060045BD RID: 17853 RVA: 0x0015F7A8 File Offset: 0x0015D9A8
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
		int num = items.Count - this.rewardItmes.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.rewardItmes[0].gameObject) as GameObject;
				gameObject.transform.parent = this.ParentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.rewardItmes.Add(gameObject.GetComponent<RewardItem>());
			}
		}
		for (int k = 0; k < this.rewardItmes.Count; k++)
		{
			NGUITools.SetActive(this.rewardItmes[k].gameObject, k < items.Count);
		}
		for (int l = 0; l < items.Count; l++)
		{
			this.rewardItmes[l].UpdateItem(items[l].itemId, (int)items[l].quality, (int)items[l].itemCount, 0);
		}
		this.ParentGrid.Reposition();
		this.parentView.ResetPosition();
	}

	// Token: 0x060045BE RID: 17854 RVA: 0x0015F928 File Offset: 0x0015DB28
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SlotLogRoot);
	}

	// Token: 0x040032BA RID: 12986
	public List<RewardItem> rewardItmes = new List<RewardItem>();

	// Token: 0x040032BB RID: 12987
	public UIScrollView parentView;

	// Token: 0x040032BC RID: 12988
	public UIGrid ParentGrid;
}
