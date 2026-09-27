using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000987 RID: 2439
public class SellItemsRootLogic : SingletonUnity<SellItemsRootLogic>
{
	// Token: 0x060044F8 RID: 17656 RVA: 0x0015931C File Offset: 0x0015751C
	public void ShowRewards(List<GameItem> items)
	{
		this.curSellItem.Clear();
		if (GameManager.IsSupportCurDataVersion())
		{
			this.infolabel.text = StrDictionary.GetDictionaryString("#{100290}", new object[0]);
		}
		else
		{
			this.infolabel.text = "To get money";
		}
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
		int num2 = 0;
		for (int l = 0; l < items.Count; l++)
		{
			this.rewardItmes[l].UpdateItem(items[l].ItemId, (int)items[l].GetItemQuality(), items[l].StackNum, 0);
			num2 += items[l].ItemData.GetSellPrice(items[l].GetItemQuality());
			this.curSellItem.Add(items[l]);
		}
		this.SellMoneyLabel.text = GameMoneyHelper.GetMoneyValStr(num2, GameDefine.MONEY_TYPE.CASH);
		this.ParentGrid.Reposition();
		this.parentView.ResetPosition();
	}

	// Token: 0x060044F9 RID: 17657 RVA: 0x0015952C File Offset: 0x0015772C
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SellItemsRoot);
	}

	// Token: 0x060044FA RID: 17658 RVA: 0x00159540 File Offset: 0x00157740
	public void OnClickYesBtn()
	{
		for (int i = 0; i < this.curSellItem.Count; i++)
		{
			sell_item.request request = new sell_item.request();
			request.indexId = this.curSellItem[i].IndexId;
			request.itemCount = (long)this.curSellItem[i].StackNum;
			request.type = (long)this.curSellItem[i].ContainerType;
			NetLogic.GetInstance().Send<Protocol.sell_item>(request, null);
		}
		this.OnClickCloseBtn();
	}

	// Token: 0x060044FB RID: 17659 RVA: 0x001595C8 File Offset: 0x001577C8
	public void OnClickCancelBtn()
	{
		this.OnClickCloseBtn();
	}

	// Token: 0x040031C5 RID: 12741
	public List<RewardItem> rewardItmes = new List<RewardItem>();

	// Token: 0x040031C6 RID: 12742
	public UIScrollView parentView;

	// Token: 0x040031C7 RID: 12743
	public UIGrid ParentGrid;

	// Token: 0x040031C8 RID: 12744
	public UILabel SellMoneyLabel;

	// Token: 0x040031C9 RID: 12745
	private List<GameItem> curSellItem = new List<GameItem>();

	// Token: 0x040031CA RID: 12746
	public UILabel infolabel;
}
