using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008DB RID: 2267
public class DailyBuyItem : MonoBehaviour
{
	// Token: 0x06003D45 RID: 15685 RVA: 0x00111264 File Offset: 0x0010F464
	public void UpdateInfo(daily_buy curinfo)
	{
		this.curBuyData = DataManager.GetDailyBuyDataBuyId(curinfo.ID);
		this.curInfo = curinfo;
		if (this.curInfo.state == 0L)
		{
			this.UIbtnSp.spriteName = GameDefine.BtnIconNew[0];
		}
		else
		{
			this.UIbtnSp.spriteName = GameDefine.BtnIconNew[1];
		}
		this.BtnLabel.text = string.Format("$ {0}", this.curBuyData.Dollor);
		this.infoLabel.text = StrDictionary.GetDictionaryString("#{300701}", new object[]
		{
			this.curBuyData.Value
		});
		this.ItemList.Clear();
		if (!string.IsNullOrEmpty(this.curBuyData.ItemID1))
		{
			this.ItemList.Add(new GameItem(this.curBuyData.ItemID1, (EQUIP_QUALITY)this.curBuyData.Quality1, this.curBuyData.ItemCount1));
		}
		if (!string.IsNullOrEmpty(this.curBuyData.ItemID2))
		{
			this.ItemList.Add(new GameItem(this.curBuyData.ItemID2, (EQUIP_QUALITY)this.curBuyData.Quality2, this.curBuyData.ItemCount2));
		}
		if (!string.IsNullOrEmpty(this.curBuyData.ItemID3))
		{
			this.ItemList.Add(new GameItem(this.curBuyData.ItemID3, (EQUIP_QUALITY)this.curBuyData.Quality3, this.curBuyData.ItemCount3));
		}
		if (!string.IsNullOrEmpty(this.curBuyData.ItemID4))
		{
			this.ItemList.Add(new GameItem(this.curBuyData.ItemID4, (EQUIP_QUALITY)this.curBuyData.Quality4, this.curBuyData.ItemCount4));
		}
		if (!string.IsNullOrEmpty(this.curBuyData.ItemID5))
		{
			this.ItemList.Add(new GameItem(this.curBuyData.ItemID5, (EQUIP_QUALITY)this.curBuyData.Quality5, this.curBuyData.ItemCount5));
		}
		if (this.ItemList.Count <= 4)
		{
			this.ParentGrid.cellWidth = 72f;
			this.ParentGrid.maxPerLine = 2;
		}
		else
		{
			this.ParentGrid.cellWidth = 62f;
			this.ParentGrid.maxPerLine = 3;
		}
		int num = this.ItemList.Count - this.RewardItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.RewardItems[0].gameObject) as GameObject;
				RewardItem component = gameObject.GetComponent<RewardItem>();
				gameObject.name = string.Format("reward{0:D2}", this.RewardItems.Count);
				gameObject.transform.parent = this.ParentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.RewardItems.Add(component);
			}
		}
		this.ParentGrid.Reposition();
		for (int j = 0; j < this.RewardItems.Count; j++)
		{
			if (j < this.ItemList.Count)
			{
				int level = 0;
				if (this.ItemList[j].ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.ItemList[j].ItemData.SubType);
				}
				this.RewardItems[j].UpdateItem(this.ItemList[j], level, false);
				UnityVersionUtil.SetActiveRecursive(this.RewardItems[j].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.RewardItems[j].gameObject, false);
			}
		}
	}

	// Token: 0x06003D46 RID: 15686 RVA: 0x00111650 File Offset: 0x0010F850
	public void OnClickBuyBtn()
	{
		if (this.curInfo.state == 0L)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.Billing(this.curBuyData.ProductId);
			if (GameSettingData.IsTestBilling)
			{
				WaitResponseUIRootLogic.OpenWaitBox(266, 10f, 0f, null);
				check_purchase.request request = new check_purchase.request();
				request.productId = this.curBuyData.ProductId;
				NetLogic.GetInstance().Send<Protocol.check_purchase>(request, null);
			}
		}
	}

	// Token: 0x040028B1 RID: 10417
	public UIGrid ParentGrid;

	// Token: 0x040028B2 RID: 10418
	public List<RewardItem> RewardItems;

	// Token: 0x040028B3 RID: 10419
	public UISprite UIbtnSp;

	// Token: 0x040028B4 RID: 10420
	public UILabel BtnLabel;

	// Token: 0x040028B5 RID: 10421
	private daily_buy curInfo;

	// Token: 0x040028B6 RID: 10422
	private DailyBuyData curBuyData;

	// Token: 0x040028B7 RID: 10423
	public UILabel infoLabel;

	// Token: 0x040028B8 RID: 10424
	private List<GameItem> ItemList = new List<GameItem>();
}
