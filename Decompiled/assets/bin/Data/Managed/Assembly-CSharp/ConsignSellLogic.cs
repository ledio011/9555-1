using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000931 RID: 2353
public class ConsignSellLogic : MonoBehaviour
{
	// Token: 0x0600416B RID: 16747 RVA: 0x00137018 File Offset: 0x00135218
	private void Awake()
	{
		this.ShowBackPackLogicScript.onClickItem = new BackPackRootLogic.OnClickItemDelegate(this.OnClickItem);
		this.ShowBackPackLogicScript.onItemLineReset = new BackPackRootLogic.OnItemLineResetDelegate(this.OnItemLineReset);
	}

	// Token: 0x0600416C RID: 16748 RVA: 0x00137054 File Offset: 0x00135254
	public void OnItemLineReset(ItemLineLogic curLine)
	{
		if (this.currentSelectItem != null && !this.currentSelectItem.IsEmpty() && this.mCurChoosedObj == null)
		{
			for (int i = 0; i < curLine.ItemObjList.Count; i++)
			{
				if (curLine.ItemObjList[i].curItem != null && curLine.ItemObjList[i].curItem.IndexId == this.currentSelectItem.IndexId)
				{
					this.ChoosedPic.transform.position = curLine.ItemObjList[i].transform.position;
					NGUITools.SetActive(this.ChoosedPic.gameObject, true);
					this.mCurChoosedObj = curLine.ItemObjList[i];
					break;
				}
			}
		}
	}

	// Token: 0x0600416D RID: 16749 RVA: 0x00137134 File Offset: 0x00135334
	private void UpdateSlider(int count)
	{
		if (this.currentSelectItem != null && !this.currentSelectItem.IsEmpty() && this.currentSelectItem.StackNum >= 1)
		{
			float value = (float)count / (float)this.currentSelectItem.StackNum;
			this.sellCountSlider.value = value;
		}
		else
		{
			this.sellCountSlider.value = 0f;
		}
	}

	// Token: 0x0600416E RID: 16750 RVA: 0x001371A0 File Offset: 0x001353A0
	public void OnSelectItem(GameItem item)
	{
		if (this.currentSelectItem == null || item.IndexId != this.currentSelectItem.IndexId)
		{
			this.currentSelectItem = item;
			this.pricePerfent = 1f;
			this.UpdateSlider(1);
			this.UpdateSelectItem();
			this.UpdateSelectItemPic();
		}
	}

	// Token: 0x0600416F RID: 16751 RVA: 0x001371F4 File Offset: 0x001353F4
	public void UpdateSelectItemPic()
	{
		if (this.currentSelectItem != null)
		{
			ItemUILogic itemObjByItemIndex = this.ShowBackPackLogicScript.GetItemObjByItemIndex(this.currentSelectItem.IndexId);
			if (itemObjByItemIndex != null)
			{
				NGUITools.SetActive(this.ChoosedPic.gameObject, true);
				this.ChoosedPic.transform.position = itemObjByItemIndex.transform.position;
			}
			else
			{
				NGUITools.SetActive(this.ChoosedPic.gameObject, false);
			}
			this.mCurChoosedObj = itemObjByItemIndex;
		}
		else
		{
			NGUITools.SetActive(this.ChoosedPic.gameObject, false);
			this.mCurChoosedObj = null;
		}
	}

	// Token: 0x06004170 RID: 16752 RVA: 0x00137298 File Offset: 0x00135498
	public void OnClickItem(GameItem item)
	{
		if (this.currentSelectItem == null || item.IndexId != this.currentSelectItem.IndexId)
		{
			this.currentSelectItem = item;
			ItemData itemData = item.ItemData;
			this.pricePerfent = 1f;
			this.UpdateSlider(1);
			this.UpdateSelectItem();
			this.UpdateSelectItemPic();
		}
		else if (this.currentSelectItem != null)
		{
			int level = 0;
			if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item.ItemData.SubType);
			}
			ItemInfoRootLogicNew.ShowItemTips(this.currentSelectItem, level, false, UI_PAGE_TYPE.INVALID);
		}
	}

	// Token: 0x06004171 RID: 16753 RVA: 0x00137344 File Offset: 0x00135544
	private void ChangeTab()
	{
		if (this.currentPage == 0)
		{
			this.tabEdgeSprite1.spriteName = "CZ_wuPinYanSe_3";
			this.tabEdgeSprite2.spriteName = "CZ_tongYongDi_zhuYao_4";
			this.tabEdgeSprite3.spriteName = "CZ_tongYongDi_zhuYao_4";
			this.tabEdgeState1.active = true;
			this.tabEdgeState2.active = false;
			this.tabEdgeState3.active = false;
		}
		else if (this.currentPage == 1)
		{
			this.tabEdgeSprite2.spriteName = "CZ_wuPinYanSe_3";
			this.tabEdgeSprite1.spriteName = "CZ_tongYongDi_zhuYao_4";
			this.tabEdgeSprite3.spriteName = "CZ_tongYongDi_zhuYao_4";
			this.tabEdgeState1.active = false;
			this.tabEdgeState2.active = true;
			this.tabEdgeState3.active = false;
		}
		else if (this.currentPage == 2)
		{
			this.tabEdgeSprite3.spriteName = "CZ_wuPinYanSe_3";
			this.tabEdgeSprite1.spriteName = "CZ_tongYongDi_zhuYao_4";
			this.tabEdgeSprite2.spriteName = "CZ_tongYongDi_zhuYao_4";
			this.tabEdgeState1.active = false;
			this.tabEdgeState2.active = false;
			this.tabEdgeState3.active = true;
		}
	}

	// Token: 0x06004172 RID: 16754 RVA: 0x0013747C File Offset: 0x0013567C
	private void UpdateSelectTime()
	{
		if (this.selectTime == 0)
		{
			this.timeLabel1.active = true;
			this.timeLabel2.active = false;
			this.timeLabel3.active = false;
			this.timeSelectSprite.transform.position = this.timeLabel1.transform.position;
		}
		else if (this.selectTime == 1)
		{
			this.timeLabel1.active = false;
			this.timeLabel2.active = true;
			this.timeLabel3.active = false;
			this.timeSelectSprite.transform.position = this.timeLabel2.transform.position;
		}
		else if (this.selectTime == 2)
		{
			this.timeLabel1.active = false;
			this.timeLabel2.active = false;
			this.timeLabel3.active = true;
			this.timeSelectSprite.transform.position = this.timeLabel3.transform.position;
		}
	}

	// Token: 0x06004173 RID: 16755 RVA: 0x00137584 File Offset: 0x00135784
	public void SliderChange()
	{
		this.UpdateSelectItem();
	}

	// Token: 0x06004174 RID: 16756 RVA: 0x0013758C File Offset: 0x0013578C
	private void UpdateSelectItem()
	{
		if (this.currentSelectItem != null && !this.currentSelectItem.IsEmpty())
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(this.currentSelectItem.ItemId);
			this.selectSpriteNameLabel.text = itemDataByID.MName;
			this.selectSprite.spriteName = itemDataByID.BackPackIcon;
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				this.selectSpriteQuality.spriteName = this.currentSelectItem.GetItemQuality().ToString();
			}
			else
			{
				this.selectSpriteQuality.spriteName = itemDataByID.QualityType.ToString();
			}
			this.sellCount = (int)Mathf.Round(this.sellCountSlider.value * (float)this.currentSelectItem.StackNum);
			this.sellCountLabel.text = this.sellCount.ToString();
			this.singelPrice = (int)((float)itemDataByID.ConsignPrice * this.pricePerfent + 0.5f);
			this.singelPriceLabel.text = this.singelPrice.ToString();
			this.finalPrice = this.singelPrice * this.sellCount;
			this.finalPriceLabel.text = this.finalPrice.ToString();
			this.SellCommission = this.commisions[this.selectTime];
			this.SellCommissionLabel.text = this.SellCommission.ToString();
			if (this.sellCount > 0)
			{
				this.SellBtn.spriteName = GameDefine.BtnIcon[1];
			}
			else
			{
				this.SellBtn.spriteName = GameDefine.BtnIcon[2];
			}
			this.priceBtnAdd.isEnabled = true;
			this.priceBtnSub.isEnabled = true;
		}
		else
		{
			this.sellCount = 0;
			this.selectSprite.spriteName = "CZ_A_beiBao_ZBKD";
			this.selectSpriteQuality.spriteName = string.Empty;
			this.selectSpriteNameLabel.text = string.Empty;
			this.sellCountSlider.value = 0f;
			this.singelPrice = 0;
			this.singelPriceLabel.text = this.singelPrice.ToString();
			this.finalPrice = this.singelPrice * this.sellCount;
			this.finalPriceLabel.text = this.finalPrice.ToString();
			this.SellCommissionLabel.text = "0";
			this.SellBtn.spriteName = GameDefine.BtnIcon[2];
			this.priceBtnAdd.isEnabled = true;
			this.priceBtnSub.isEnabled = true;
			this.sellCountLabel.text = this.sellCount.ToString();
		}
	}

	// Token: 0x06004175 RID: 16757 RVA: 0x00137820 File Offset: 0x00135A20
	public void Reset(int subIndex = 0)
	{
		if (this.playerData == null)
		{
			this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		this.currentPage = subIndex;
		this.UpdateInfo(true);
		this.ChangeTab();
	}

	// Token: 0x06004176 RID: 16758 RVA: 0x00137854 File Offset: 0x00135A54
	public void UpdateInfo(bool isNeedResetPos = true)
	{
		if (this.currentPage == 0)
		{
			ItemContainer itemContainer = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.EQUIP_BACKPACK);
			List<GameItem> consignSellItem = ItemContainerTool.GetConsignSellItem(itemContainer);
			if (consignSellItem.Count > 0)
			{
				this.currentSelectItem = consignSellItem[0];
			}
			else
			{
				this.currentSelectItem = null;
			}
			this.ShowBackPackLogicScript.ShowList(consignSellItem, isNeedResetPos);
		}
		else if (this.currentPage == 1)
		{
			ItemContainer itemContainer2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.BADGE_BACKPACK);
			List<GameItem> consignSellItem2 = ItemContainerTool.GetConsignSellItem(itemContainer2);
			if (consignSellItem2.Count > 0)
			{
				this.currentSelectItem = consignSellItem2[0];
			}
			else
			{
				this.currentSelectItem = null;
			}
			this.ShowBackPackLogicScript.ShowList(consignSellItem2, isNeedResetPos);
		}
		else
		{
			ItemContainer itemContainer3 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(ITEM_CONTAINER_TYPE.ITEM_BACKPACK);
			List<GameItem> consignSellItem3 = ItemContainerTool.GetConsignSellItem(itemContainer3);
			if (consignSellItem3.Count > 0)
			{
				this.currentSelectItem = consignSellItem3[0];
			}
			else
			{
				this.currentSelectItem = null;
			}
			this.ShowBackPackLogicScript.ShowList(consignSellItem3, isNeedResetPos);
		}
		this.UpdateSlider(1);
		this.UpdateSelectItem();
		this.UpdateSelectTime();
		this.UpdateSelectItemPic();
	}

	// Token: 0x06004177 RID: 16759 RVA: 0x00137980 File Offset: 0x00135B80
	public void ClickTab1()
	{
		if (this.currentPage != 0)
		{
			this.currentSelectItem = null;
			this.currentPage = 0;
			this.pricePerfent = 1f;
			this.UpdateInfo(true);
		}
		this.ChangeTab();
	}

	// Token: 0x06004178 RID: 16760 RVA: 0x001379B4 File Offset: 0x00135BB4
	public void ClickTab2()
	{
		if (this.currentPage != 1)
		{
			this.currentSelectItem = null;
			this.pricePerfent = 1f;
			this.currentPage = 1;
			this.UpdateInfo(true);
		}
		this.ChangeTab();
	}

	// Token: 0x06004179 RID: 16761 RVA: 0x001379F4 File Offset: 0x00135BF4
	public void ClickTab3()
	{
		if (this.currentPage != 2)
		{
			this.currentSelectItem = null;
			this.pricePerfent = 1f;
			this.currentPage = 2;
			this.UpdateInfo(true);
		}
		this.ChangeTab();
	}

	// Token: 0x0600417A RID: 16762 RVA: 0x00137A34 File Offset: 0x00135C34
	public void ClickTime1()
	{
		if (this.selectTime != 0)
		{
			this.selectTime = 0;
			this.UpdateSelectTime();
			this.UpdateSelectItem();
		}
	}

	// Token: 0x0600417B RID: 16763 RVA: 0x00137A54 File Offset: 0x00135C54
	public void ClickTime2()
	{
		if (this.selectTime != 1)
		{
			this.selectTime = 1;
			this.UpdateSelectTime();
			this.UpdateSelectItem();
		}
	}

	// Token: 0x0600417C RID: 16764 RVA: 0x00137A78 File Offset: 0x00135C78
	public void ClickTime3()
	{
		if (this.selectTime != 2)
		{
			this.selectTime = 2;
			this.UpdateSelectTime();
			this.UpdateSelectItem();
		}
	}

	// Token: 0x0600417D RID: 16765 RVA: 0x00137A9C File Offset: 0x00135C9C
	public void ClickMax()
	{
		if (this.currentSelectItem != null)
		{
			this.sellCountSlider.value = 1f;
			this.UpdateSelectItem();
		}
	}

	// Token: 0x0600417E RID: 16766 RVA: 0x00137AC0 File Offset: 0x00135CC0
	public void ClickAddPrice()
	{
		if (this.currentSelectItem == null)
		{
			return;
		}
		ItemData itemDataByID = DataManager.GetItemDataByID(this.currentSelectItem.ItemId);
		this.pricePerfent += 0.02f;
		int num = (int)((float)itemDataByID.ConsignPrice * this.pricePerfent + 0.5f);
		if (num == this.singelPrice)
		{
			float num2 = (float)(num + 1) / (float)itemDataByID.ConsignPrice;
			if (num2 > 1.5f)
			{
				this.pricePerfent = 1.5f;
			}
			else
			{
				this.pricePerfent = num2;
			}
		}
		if (this.pricePerfent > 1.5f)
		{
			this.priceBtnAdd.isEnabled = false;
			this.pricePerfent = 1.5f;
			NoticeLogic.AddNotifyData("#{101247}", true, false);
		}
		this.UpdateSelectItem();
		if (!this.priceBtnSub.isEnabled)
		{
			this.priceBtnSub.isEnabled = true;
		}
	}

	// Token: 0x0600417F RID: 16767 RVA: 0x00137BA4 File Offset: 0x00135DA4
	public void ClickSubPrice()
	{
		if (this.currentSelectItem == null)
		{
			return;
		}
		ItemData itemDataByID = DataManager.GetItemDataByID(this.currentSelectItem.ItemId);
		this.pricePerfent -= 0.02f;
		int num = (int)((float)itemDataByID.ConsignPrice * this.pricePerfent + 0.5f);
		if (num == this.singelPrice)
		{
			float num2 = (float)(num - 1) / (float)itemDataByID.ConsignPrice;
			if (num2 < 0.5f)
			{
				this.pricePerfent = 0.5f;
			}
			else
			{
				this.pricePerfent = num2;
			}
		}
		if (this.pricePerfent < 0.5f)
		{
			this.priceBtnSub.isEnabled = false;
			this.pricePerfent = 0.5f;
			NoticeLogic.AddNotifyData("#{101248}", true, false);
		}
		this.UpdateSelectItem();
		if (!this.priceBtnAdd.isEnabled)
		{
			this.priceBtnAdd.isEnabled = true;
		}
	}

	// Token: 0x06004180 RID: 16768 RVA: 0x00137C88 File Offset: 0x00135E88
	public void ShowItemList(List<GameItem> list, bool isNeedResetPos = true)
	{
	}

	// Token: 0x06004181 RID: 16769 RVA: 0x00137C8C File Offset: 0x00135E8C
	public void ClickSell()
	{
		if (this.sellCount <= 0)
		{
			return;
		}
		if (GameMoneyHelper.BeforeCheckBuy(GameDefine.MONEY_TYPE.CASH, this.SellCommission))
		{
			consign_sale_item.request request = new consign_sale_item.request();
			request.indexId = this.currentSelectItem.IndexId;
			request.itemCount = (long)this.sellCount;
			request.price = (long)this.singelPrice;
			request.timeType = (long)this.selectTime;
			request.itemType = (long)this.currentSelectItem.ItemData.ItemType;
			NetLogic.GetInstance().Send<Protocol.consign_sale_item>(request, null);
		}
	}

	// Token: 0x04002D2C RID: 11564
	private PlayerData playerData;

	// Token: 0x04002D2D RID: 11565
	private List<GameItem> mCurItemList;

	// Token: 0x04002D2E RID: 11566
	private bool mInitFlag;

	// Token: 0x04002D2F RID: 11567
	private int currentPage;

	// Token: 0x04002D30 RID: 11568
	public SpriteState tabEdgeState1;

	// Token: 0x04002D31 RID: 11569
	public SpriteState tabEdgeState2;

	// Token: 0x04002D32 RID: 11570
	public SpriteState tabEdgeState3;

	// Token: 0x04002D33 RID: 11571
	public UISprite tabEdgeSprite1;

	// Token: 0x04002D34 RID: 11572
	public UISprite tabEdgeSprite2;

	// Token: 0x04002D35 RID: 11573
	public UISprite tabEdgeSprite3;

	// Token: 0x04002D36 RID: 11574
	private GameItem currentSelectItem;

	// Token: 0x04002D37 RID: 11575
	private int selectTime;

	// Token: 0x04002D38 RID: 11576
	public UISprite selectSpriteQuality;

	// Token: 0x04002D39 RID: 11577
	public UISprite selectSprite;

	// Token: 0x04002D3A RID: 11578
	public UILabel selectSpriteNameLabel;

	// Token: 0x04002D3B RID: 11579
	public LabelState timeLabel1;

	// Token: 0x04002D3C RID: 11580
	public LabelState timeLabel2;

	// Token: 0x04002D3D RID: 11581
	public LabelState timeLabel3;

	// Token: 0x04002D3E RID: 11582
	public UISprite timeSelectSprite;

	// Token: 0x04002D3F RID: 11583
	public BackPackRootLogic ShowBackPackLogicScript;

	// Token: 0x04002D40 RID: 11584
	public UISprite SellBtn;

	// Token: 0x04002D41 RID: 11585
	public UILabel singelPriceLabel;

	// Token: 0x04002D42 RID: 11586
	public UILabel finalPriceLabel;

	// Token: 0x04002D43 RID: 11587
	private int singelPrice;

	// Token: 0x04002D44 RID: 11588
	private int finalPrice;

	// Token: 0x04002D45 RID: 11589
	public UILabel sellCountLabel;

	// Token: 0x04002D46 RID: 11590
	private int sellCount;

	// Token: 0x04002D47 RID: 11591
	public UISlider sellCountSlider;

	// Token: 0x04002D48 RID: 11592
	public UILabel SellCommissionLabel;

	// Token: 0x04002D49 RID: 11593
	private int SellCommission = 1000;

	// Token: 0x04002D4A RID: 11594
	private int[] commisions = new int[]
	{
		1000,
		1500,
		2000
	};

	// Token: 0x04002D4B RID: 11595
	private float pricePerfent = 1f;

	// Token: 0x04002D4C RID: 11596
	private float pressTime;

	// Token: 0x04002D4D RID: 11597
	public UIButton priceBtnAdd;

	// Token: 0x04002D4E RID: 11598
	public UIButton priceBtnSub;

	// Token: 0x04002D4F RID: 11599
	public UISprite ChoosedPic;

	// Token: 0x04002D50 RID: 11600
	private ItemUILogic mCurChoosedObj;
}
