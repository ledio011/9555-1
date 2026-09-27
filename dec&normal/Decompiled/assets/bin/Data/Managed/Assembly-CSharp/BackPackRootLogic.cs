using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200096D RID: 2413
public class BackPackRootLogic : MonoBehaviour
{
	// Token: 0x17000FA7 RID: 4007
	// (get) Token: 0x060043F5 RID: 17397 RVA: 0x0015139C File Offset: 0x0014F59C
	public bool IsInSellMode
	{
		get
		{
			return this.mIsInSellMode;
		}
	}

	// Token: 0x17000FA8 RID: 4008
	// (get) Token: 0x060043F6 RID: 17398 RVA: 0x001513A4 File Offset: 0x0014F5A4
	public bool CanSellItem
	{
		get
		{
			return this.mCanSellItem;
		}
	}

	// Token: 0x17000FA9 RID: 4009
	// (get) Token: 0x060043F7 RID: 17399 RVA: 0x001513AC File Offset: 0x0014F5AC
	public ItemContainer CurContainner
	{
		get
		{
			return this.mCurContainner;
		}
	}

	// Token: 0x060043F8 RID: 17400 RVA: 0x001513B4 File Offset: 0x0014F5B4
	private void Awake()
	{
		if (!this.mInitFlag)
		{
			this.mInitFlag = true;
			this.Init();
		}
	}

	// Token: 0x060043F9 RID: 17401 RVA: 0x001513D0 File Offset: 0x0014F5D0
	private void Init()
	{
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
		for (int i = 0; i < this.ItemLineList.Count; i++)
		{
			ItemLineLogic itemLineLogic = this.ItemLineList[i];
			itemLineLogic.onClickItem = (ItemUILogic.OnClickItemDelegate)Delegate.Combine(itemLineLogic.onClickItem, new ItemUILogic.OnClickItemDelegate(this.OnClickItem));
		}
	}

	// Token: 0x060043FA RID: 17402 RVA: 0x00151450 File Offset: 0x0014F650
	public void InitCanSell(bool canSell)
	{
		this.mCanSellItem = canSell;
	}

	// Token: 0x060043FB RID: 17403 RVA: 0x0015145C File Offset: 0x0014F65C
	public void Hide()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
	}

	// Token: 0x060043FC RID: 17404 RVA: 0x0015146C File Offset: 0x0014F66C
	public void ShowList(List<GameItem> list, bool needResetPos)
	{
		this.mCurTapPage = BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_Part_PAGE;
		this.ShowItem(list, needResetPos);
		if (needResetPos)
		{
			this.scrollView.ResetPosition();
		}
	}

	// Token: 0x060043FD RID: 17405 RVA: 0x0015149C File Offset: 0x0014F69C
	public void Show(bool needResetPackPos, ITEM_CONTAINER_TYPE containerType)
	{
		this.mCurContainner = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GetItemContainer(containerType);
		UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		NGUITools.SetActive(this.SellBtnRoot, false);
		if (containerType != ITEM_CONTAINER_TYPE.EQUIP_BACKPACK || !this.mCanSellItem)
		{
			NGUITools.SetActive(this.RecycleBtnRoot, false);
		}
		else
		{
			NGUITools.SetActive(this.RecycleBtnRoot, true);
		}
		this.mIsInSellMode = false;
		if (needResetPackPos)
		{
			this.Reset();
		}
		else
		{
			this.UpdateBackPack();
		}
	}

	// Token: 0x060043FE RID: 17406 RVA: 0x00151524 File Offset: 0x0014F724
	public ItemUILogic GetItemObjByItemIndex(long indexId)
	{
		for (int i = 0; i < this.ItemLineList.Count; i++)
		{
			for (int j = 0; j < this.ItemLineList[i].ItemObjList.Count; j++)
			{
				if (this.ItemLineList[i].ItemObjList[j].curItem != null)
				{
					if (this.ItemLineList[i].ItemObjList[j].curItem.IndexId == indexId)
					{
						return this.ItemLineList[i].ItemObjList[j];
					}
				}
			}
		}
		return null;
	}

	// Token: 0x060043FF RID: 17407 RVA: 0x001515DC File Offset: 0x0014F7DC
	private void OnEnable()
	{
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateBackPack));
	}

	// Token: 0x06004400 RID: 17408 RVA: 0x0015160C File Offset: 0x0014F80C
	private void OnDisable()
	{
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateBackPack));
	}

	// Token: 0x06004401 RID: 17409 RVA: 0x0015163C File Offset: 0x0014F83C
	public void UpdateBackPack()
	{
		switch (this.mCurTapPage)
		{
		case BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_ALL_PAGE:
			this.ShowAllItem(false);
			break;
		case BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_EQUIP_PAGE:
			this.ShowEquipItem(false);
			break;
		case BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_ITEM_PAGE:
			this.ShowItemItem(false);
			break;
		}
		this.ResetTopTapBtn();
	}

	// Token: 0x06004402 RID: 17410 RVA: 0x00151694 File Offset: 0x0014F894
	private void Reset()
	{
		this.mCurTapPage = BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_ALL_PAGE;
		this.OnClickShowAllItem();
	}

	// Token: 0x06004403 RID: 17411 RVA: 0x001516A4 File Offset: 0x0014F8A4
	public void ResetTopTapBtn()
	{
		for (int i = 0; i < this.TopTabBtnPic.Length; i++)
		{
			if (i == (int)this.mCurTapPage)
			{
				this.TopTabBtnPic[i].color = Color.yellow;
			}
			else
			{
				this.TopTabBtnPic[i].color = Color.white;
			}
		}
	}

	// Token: 0x06004404 RID: 17412 RVA: 0x00151700 File Offset: 0x0014F900
	public void OnClickShowAllItem()
	{
		this.mCurTapPage = BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_ALL_PAGE;
		this.ShowAllItem(true);
		this.ResetTopTapBtn();
		this.scrollView.ResetPosition();
	}

	// Token: 0x06004405 RID: 17413 RVA: 0x00151724 File Offset: 0x0014F924
	public void OnClickShowEquipItem()
	{
		this.mCurTapPage = BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_EQUIP_PAGE;
		this.ShowEquipItem(true);
		this.ResetTopTapBtn();
		this.scrollView.ResetPosition();
	}

	// Token: 0x06004406 RID: 17414 RVA: 0x00151748 File Offset: 0x0014F948
	public void OnClickShowItemBtn()
	{
		this.mCurTapPage = BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_ITEM_PAGE;
		this.ShowItemItem(true);
		this.ResetTopTapBtn();
		this.scrollView.ResetPosition();
	}

	// Token: 0x06004407 RID: 17415 RVA: 0x0015176C File Offset: 0x0014F96C
	private void ShowAllItem(bool needResetPos)
	{
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(this.mCurContainner, true, GameDefine.ITEM_TYPE.INVALID, false, PROFESSION_TYPE.INVALID);
		if (GameManager.IsSupportCurDataVersion77() && (this.mCurContainner.ContainType == ITEM_CONTAINER_TYPE.BADGE_BACKPACK || this.mCurContainner.ContainType == ITEM_CONTAINER_TYPE.EQUIP_BACKPACK || this.mCurContainner.ContainType == ITEM_CONTAINER_TYPE.FASHION_BACKPACK || this.mCurContainner.ContainType == ITEM_CONTAINER_TYPE.ITEM_BACKPACK) && !this.mCurContainner.IsFull())
		{
			GameItem gameItem = new GameItem();
			gameItem.SetAddItem();
			targetTypeItem.Add(gameItem);
		}
		if (targetTypeItem != null)
		{
			this.ShowItem(targetTypeItem, needResetPos);
		}
	}

	// Token: 0x06004408 RID: 17416 RVA: 0x00151808 File Offset: 0x0014FA08
	private void ShowEquipItem(bool needResetPos)
	{
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(this.mCurContainner, false, GameDefine.ITEM_TYPE.EQUIP, false, PROFESSION_TYPE.INVALID);
		if (targetTypeItem != null)
		{
			this.ShowItem(targetTypeItem, needResetPos);
		}
	}

	// Token: 0x06004409 RID: 17417 RVA: 0x00151834 File Offset: 0x0014FA34
	private void ShowItemItem(bool needResetPos)
	{
		List<GameItem> otherTypeItem = ItemContainerTool.GetOtherTypeItem(this.mCurContainner, GameDefine.ITEM_TYPE.EQUIP, false);
		if (otherTypeItem != null)
		{
			this.ShowItem(otherTypeItem, needResetPos);
		}
	}

	// Token: 0x0600440A RID: 17418 RVA: 0x00151860 File Offset: 0x0014FA60
	public void ShowItemPart(List<GameItem> itemList, BackPackRootLogic.BACKPACK_TAP_PAGE type = BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_Part_PAGE, bool needResetPos = true)
	{
		this.mCurTapPage = type;
		this.ShowItem(itemList, needResetPos);
	}

	// Token: 0x0600440B RID: 17419 RVA: 0x00151874 File Offset: 0x0014FA74
	public void ShowItem(List<GameItem> itemList, bool needResetPos)
	{
		this.mCurItemList = itemList;
		if (this.mCurTapPage == BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_ALL_PAGE)
		{
			this.uiWrapContent.minIndex = -19;
			this.BottomWidget.height = this.uiWrapContent.itemSize * ItemContainer.BACKPACK_MAXSIZE / this.ItemLineList[0].ItemObjList.Count;
		}
		else
		{
			if (this.mCurItemList.Count > 0)
			{
				this.uiWrapContent.minIndex = -((this.mCurItemList.Count + this.ItemLineList[0].ItemObjList.Count - 1) / this.ItemLineList[0].ItemObjList.Count - 1);
			}
			else
			{
				this.uiWrapContent.minIndex = 0;
			}
			this.BottomWidget.height = this.uiWrapContent.itemSize * ((this.mCurItemList.Count + this.ItemLineList[0].ItemObjList.Count - 1) / this.ItemLineList[0].ItemObjList.Count);
		}
		if (needResetPos)
		{
			this.uiWrapContent.SortBasedOnScrollMovement();
		}
		else
		{
			this.ReShowItemList();
		}
		this.mIsInSellMode = false;
		this.UpdateSellMode();
	}

	// Token: 0x0600440C RID: 17420 RVA: 0x001519C0 File Offset: 0x0014FBC0
	private void UpdateSellMode()
	{
		if (this.mChoosedSellItemList.Count > 0)
		{
			for (int i = 0; i < this.ItemLineList.Count; i++)
			{
				for (int j = 0; j < this.ItemLineList[i].ItemObjList.Count; j++)
				{
					this.ItemLineList[i].ItemObjList[j].SetSellChoose(false);
				}
			}
		}
		this.mChoosedSellItemList.Clear();
		if (this.CanSellItem)
		{
			if (!this.mIsInSellMode)
			{
				NGUITools.SetActive(this.SellBtnRoot, false);
				this.RecycleLabel.text = StrDictionary.GetDictionaryString("#{100621}", new object[0]);
			}
			else
			{
				NGUITools.SetActive(this.SellBtnRoot, true);
				if (GameManager.IsSupportCurDataVersion())
				{
					this.RecycleLabel.text = StrDictionary.GetDictionaryString("#{100289}", new object[0]);
				}
				else
				{
					this.RecycleLabel.text = "Back";
				}
				this.SellBtnPic.spriteName = this.disableBtnPic;
			}
		}
	}

	// Token: 0x0600440D RID: 17421 RVA: 0x00151AE4 File Offset: 0x0014FCE4
	public void OnClickRecycleBtn()
	{
		this.mIsInSellMode = !this.mIsInSellMode;
		this.UpdateSellMode();
	}

	// Token: 0x0600440E RID: 17422 RVA: 0x00151AFC File Offset: 0x0014FCFC
	public void OnClickSellBtn()
	{
		if (this.mChoosedSellItemList.Count > 0)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SellItemsRoot, delegate
			{
				SingletonUnity<SellItemsRootLogic>.Instance.ShowRewards(this.mChoosedSellItemList);
			}, null);
		}
	}

	// Token: 0x0600440F RID: 17423 RVA: 0x00151B2C File Offset: 0x0014FD2C
	public void ReShowItemList()
	{
		for (int i = 0; i < this.ItemLineList.Count; i++)
		{
			if (this.mCurContainner != null)
			{
				this.ResetItemLine(this.ItemLineList[i], this.ItemLineList[i].CurIndex, this.mCurContainner.ContainerSize);
			}
			else
			{
				this.ResetItemLine(this.ItemLineList[i], this.ItemLineList[i].CurIndex, this.mCurItemList.Count);
			}
		}
	}

	// Token: 0x06004410 RID: 17424 RVA: 0x00151BC4 File Offset: 0x0014FDC4
	public void OnClickClearUpBtn()
	{
		switch (this.mCurTapPage)
		{
		case BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_ALL_PAGE:
			this.ShowAllItem(true);
			break;
		case BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_EQUIP_PAGE:
			this.ShowEquipItem(true);
			break;
		case BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_ITEM_PAGE:
			this.ShowItemItem(true);
			break;
		}
		this.scrollView.ResetPosition();
	}

	// Token: 0x06004411 RID: 17425 RVA: 0x00151C20 File Offset: 0x0014FE20
	public ItemUILogic GetFirstEquipItem()
	{
		for (int i = 0; i < this.ItemLineList.Count; i++)
		{
			for (int j = 0; j < this.ItemLineList[i].ItemObjList.Count; j++)
			{
				if (this.ItemLineList[i].ItemObjList[j].curItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					return this.ItemLineList[i].ItemObjList[j];
				}
			}
		}
		return null;
	}

	// Token: 0x06004412 RID: 17426 RVA: 0x00151CB0 File Offset: 0x0014FEB0
	public void OnClickItem(GameItem item, ItemUILogic curUIItem)
	{
		if (item != null)
		{
			if (this.CanSellItem && this.mIsInSellMode)
			{
				if (item.ItemId.Equals(GameDefine.EmptyAddItemID))
				{
					return;
				}
				if (this.mChoosedSellItemList.Contains(item))
				{
					this.mChoosedSellItemList.Remove(item);
					curUIItem.SetSellChoose(false);
				}
				else if (this.mChoosedSellItemList.Count < this.MAX_SELL_NUM)
				{
					this.mChoosedSellItemList.Add(item);
					curUIItem.SetSellChoose(true);
				}
				else if (GameManager.IsSupportCurDataVersion47())
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100654}", new object[]
					{
						this.MAX_SELL_NUM
					}), true, false);
				}
				if (this.mChoosedSellItemList.Count > 0)
				{
					if (!this.SellBtnPic.spriteName.Equals(this.enableBtnPic))
					{
						this.SellBtnPic.spriteName = this.enableBtnPic;
					}
				}
				else if (!this.SellBtnPic.spriteName.Equals(this.disableBtnPic))
				{
					this.SellBtnPic.spriteName = this.disableBtnPic;
				}
			}
			else if (this.onClickItem != null)
			{
				this.onClickItem(item);
			}
		}
	}

	// Token: 0x06004413 RID: 17427 RVA: 0x00151E00 File Offset: 0x00150000
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		ItemLineLogic curLine = this.ItemLineList[index];
		if (this.mCurContainner == null)
		{
			this.ResetItemLine(curLine, Mathf.Abs(realIndex), this.mCurItemList.Count);
		}
		else
		{
			this.ResetItemLine(curLine, Mathf.Abs(realIndex), this.mCurContainner.ContainerSize);
		}
	}

	// Token: 0x06004414 RID: 17428 RVA: 0x00151E5C File Offset: 0x0015005C
	private void ResetItemLine(ItemLineLogic curLine, int realIndex, int maxIndex)
	{
		int num = realIndex * this.ItemLineList[0].ItemObjList.Count;
		List<GameItem> list = new List<GameItem>();
		list.Clear();
		for (int i = 0; i < curLine.ItemObjList.Count; i++)
		{
			if (i + num < this.mCurItemList.Count)
			{
				list.Add(this.mCurItemList[i + num]);
			}
			else
			{
				list.Add(null);
			}
		}
		if (this.mCurTapPage == BackPackRootLogic.BACKPACK_TAP_PAGE.BACKPACK_ALL_PAGE)
		{
			curLine.Reset(list, true, realIndex, num, maxIndex);
		}
		else
		{
			curLine.Reset(list, false, realIndex, num, maxIndex);
		}
		if (this.onItemLineReset != null)
		{
			this.onItemLineReset(curLine);
		}
		if (this.mIsInSellMode)
		{
			for (int j = 0; j < curLine.ItemObjList.Count; j++)
			{
				if (this.mChoosedSellItemList.Contains(curLine.ItemObjList[j].curItem))
				{
					curLine.ItemObjList[j].SetSellChoose(true);
				}
				else
				{
					curLine.ItemObjList[j].SetSellChoose(false);
				}
			}
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.BADGE_CLICK_ITEM)
		{
			FunctionTipsRootLogic.ClearHandTip();
		}
	}

	// Token: 0x06004415 RID: 17429 RVA: 0x00151F9C File Offset: 0x0015019C
	private int GetMinCurLine()
	{
		int num = int.MaxValue;
		for (int i = 0; i < this.ItemLineList.Count; i++)
		{
			if (num > this.ItemLineList[i].CurIndex)
			{
				num = this.ItemLineList[i].CurIndex;
			}
		}
		return num;
	}

	// Token: 0x04003096 RID: 12438
	public BackPackRootLogic.OnClickItemDelegate onClickItem;

	// Token: 0x04003097 RID: 12439
	public BackPackRootLogic.OnItemLineResetDelegate onItemLineReset;

	// Token: 0x04003098 RID: 12440
	public UISprite[] TopTabBtnPic;

	// Token: 0x04003099 RID: 12441
	public List<ItemLineLogic> ItemLineList = new List<ItemLineLogic>();

	// Token: 0x0400309A RID: 12442
	public Transform OffsetRoot;

	// Token: 0x0400309B RID: 12443
	private BackPackRootLogic.BACKPACK_TAP_PAGE mCurTapPage;

	// Token: 0x0400309C RID: 12444
	private List<GameItem> mCurItemList;

	// Token: 0x0400309D RID: 12445
	private bool mInitFlag;

	// Token: 0x0400309E RID: 12446
	public UIWrapContentNew uiWrapContent;

	// Token: 0x0400309F RID: 12447
	public UIScrollView scrollView;

	// Token: 0x040030A0 RID: 12448
	public UIWidget BottomWidget;

	// Token: 0x040030A1 RID: 12449
	public GameObject RecycleBtnRoot;

	// Token: 0x040030A2 RID: 12450
	public UILabel RecycleLabel;

	// Token: 0x040030A3 RID: 12451
	public GameObject SellBtnRoot;

	// Token: 0x040030A4 RID: 12452
	public UISprite SellBtnPic;

	// Token: 0x040030A5 RID: 12453
	private bool mIsInSellMode;

	// Token: 0x040030A6 RID: 12454
	private bool mCanSellItem;

	// Token: 0x040030A7 RID: 12455
	private int MAX_SELL_NUM = 20;

	// Token: 0x040030A8 RID: 12456
	private List<GameItem> mChoosedSellItemList = new List<GameItem>();

	// Token: 0x040030A9 RID: 12457
	private ItemContainer mCurContainner;

	// Token: 0x040030AA RID: 12458
	private string disableBtnPic = "CZ_anNiu_2+";

	// Token: 0x040030AB RID: 12459
	private string enableBtnPic = "CZ_anNiu_2";

	// Token: 0x0200096E RID: 2414
	public enum BACKPACK_TAP_PAGE
	{
		// Token: 0x040030AD RID: 12461
		BACKPACK_ALL_PAGE,
		// Token: 0x040030AE RID: 12462
		BACKPACK_EQUIP_PAGE,
		// Token: 0x040030AF RID: 12463
		BACKPACK_ITEM_PAGE,
		// Token: 0x040030B0 RID: 12464
		BACKPACK_Part_PAGE
	}

	// Token: 0x02000AF4 RID: 2804
	// (Invoke) Token: 0x06005059 RID: 20569
	public delegate void OnClickItemDelegate(GameItem item);

	// Token: 0x02000AF5 RID: 2805
	// (Invoke) Token: 0x0600505D RID: 20573
	public delegate void OnItemLineResetDelegate(ItemLineLogic itemObj);
}
