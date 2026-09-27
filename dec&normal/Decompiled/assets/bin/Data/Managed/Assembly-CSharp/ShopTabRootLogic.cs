using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A22 RID: 2594
public class ShopTabRootLogic : SingletonUnity<ShopTabRootLogic>
{
	// Token: 0x06004AD5 RID: 19157 RVA: 0x0018B06C File Offset: 0x0018926C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004AD6 RID: 19158 RVA: 0x0018B078 File Offset: 0x00189278
	private void CheckTutorialEvent()
	{
		if (SingletonUnity<TutorialUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<TutorialUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<TutorialUIRootLogic>.Instance.CloseCheck();
		}
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06004AD7 RID: 19159 RVA: 0x0018B0D0 File Offset: 0x001892D0
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x17000FC6 RID: 4038
	// (get) Token: 0x06004AD8 RID: 19160 RVA: 0x0018B0DC File Offset: 0x001892DC
	public GameObject CurSelectObj
	{
		get
		{
			return this.curSelectObj;
		}
	}

	// Token: 0x17000FC7 RID: 4039
	// (get) Token: 0x06004AD9 RID: 19161 RVA: 0x0018B0E4 File Offset: 0x001892E4
	public shop_item CurSelectItem
	{
		get
		{
			return this.curSelectItem;
		}
	}

	// Token: 0x06004ADA RID: 19162 RVA: 0x0018B0EC File Offset: 0x001892EC
	public void EnableReset(GameDefine.SHOP_TYPE shoptype, string needItemid = null)
	{
		this.ShowItemInfoRoot.Reset();
		this.NeedItemid = needItemid;
		this.ClearSelectObj();
		this.curType = shoptype;
		this.ambientLight = RenderSettings.ambientLight;
		for (int i = 0; i < this.mShopItemBtns.Count; i++)
		{
			NGUITools.SetActive(this.mShopItemBtns[i].gameObject, false);
		}
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		this.buyCount = 1;
		this.curPage = -1;
		this.RefershTimeLabel.text = StrDictionary.GetDictionaryString("#{301102}", new object[]
		{
			TimeTools.GetLocalShowTime_HM((long)playerCommonData.ResetTime, playerCommonData.TimeOffset)
		});
		this.curSelectObj = null;
		this.curSelectItem = null;
		WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
		ask_shop_list.request request = new ask_shop_list.request();
		request.type = (long)this.curType;
		if (!string.IsNullOrEmpty(this.NeedItemid))
		{
			request.itemId = this.NeedItemid;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_START)
		{
			request.special = 1L;
		}
		NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
		if (this.specialMoneyObj != null)
		{
			if (this.curType == GameDefine.SHOP_TYPE.GUILD_SHOP)
			{
				this.specialMoneyFlag.spriteName = GameMoneyHelper.GetMoneyIcon(GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE);
				UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, true);
				this.specialMoneyFlag.MakePixelPerfect();
				this.MoneyLabel.text = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GuildContribute.ToString();
			}
			else if (this.curType == GameDefine.SHOP_TYPE.BATTLECOIN_SHOP)
			{
				this.specialMoneyFlag.spriteName = GameMoneyHelper.GetMoneyIcon(GameDefine.MONEY_TYPE.BATTLECOIN);
				UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, true);
				this.specialMoneyFlag.MakePixelPerfect();
				this.MoneyLabel.text = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BattleCoin.ToString();
			}
			else if (this.curType == GameDefine.SHOP_TYPE.ACTIVITY_SHOP)
			{
				this.specialMoneyFlag.spriteName = GameMoneyHelper.GetMoneyIcon(GameDefine.MONEY_TYPE.ACTIVITYCOIN);
				UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, true);
				this.specialMoneyFlag.MakePixelPerfect();
				this.MoneyLabel.text = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityCoin.ToString();
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, false);
			}
		}
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			if (this.curType == GameDefine.SHOP_TYPE.GUILD_SHOP)
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.ShowGangMoney();
			}
			else
			{
				SingletonUnity<MenuBaseRootLogic>.Instance.hideGangMoney();
			}
		}
		this.flurryShopOpen();
	}

	// Token: 0x06004ADB RID: 19163 RVA: 0x0018B390 File Offset: 0x00189590
	private void flurryShopOpen()
	{
		switch (this.curType)
		{
		case GameDefine.SHOP_TYPE.TOOL_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_tool", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.EQUIP_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_equip", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.BIGSALE_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_bigsale", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.GUILD_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_guild", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.BATTLECOIN_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_battle", "open", "times");
			break;
		case GameDefine.SHOP_TYPE.ACTIVITY_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_activity", "open", "times");
			break;
		}
	}

	// Token: 0x06004ADC RID: 19164 RVA: 0x0018B488 File Offset: 0x00189688
	public void UpdateShop(ret_ask_shop_list.request request)
	{
		if (this.curType != (GameDefine.SHOP_TYPE)request.type)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Shop Ttype error! + curtype:",
				this.curType,
				"***return Type:",
				(GameDefine.SHOP_TYPE)request.type
			}));
			return;
		}
		if (this.curType == GameDefine.SHOP_TYPE.GUILD_SHOP && !SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsHaveGuild())
		{
			NoticeLogic.AddNotifyData("#{102006}", true, false);
			return;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.curPage != (int)request.curPage)
		{
			this.curSelectObj = null;
		}
		this.curPage = (int)request.curPage;
		this.maxPage = (int)request.maxPage;
		List<shop_item> shop_list = request.shop_list;
		this.shopList = shop_list;
		int num = this.shopList.Count - this.mShopItemBtns.Count;
		int count = this.mShopItemBtns.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.mShopItemBtns[0].gameObject) as GameObject;
				gameObject.name = string.Format("shangPing_{0}", count + i);
				this.ItemGrid.AddChild(gameObject.transform);
				gameObject.transform.localScale = Vector3.one;
				this.mShopItemBtns.Add(gameObject.GetComponent<ShopItemBtnLogic>());
			}
		}
		for (int j = 0; j < this.mShopItemBtns.Count; j++)
		{
			NGUITools.SetActive(this.mShopItemBtns[j].gameObject, j < this.shopList.Count);
		}
		this.curPageLable.text = string.Format("{0}/{1}", this.curPage, this.maxPage);
		if (this.shopList.Count == 0)
		{
			return;
		}
		for (int k = 0; k < this.shopList.Count; k++)
		{
			if (this.curSelectObj == null)
			{
				this.curSelectObj = this.mShopItemBtns[k].gameObject;
				this.curSelectItem = this.shopList[k];
			}
			this.mShopItemBtns[k].Reset(this.shopList[k], this.curType, new ShopItemBtnLogic.OnClickShopItemDelegate(this.UpdateSelect));
		}
		this.ItemGrid.Reposition();
		if (!string.IsNullOrEmpty(this.NeedItemid))
		{
			for (int l = 0; l < this.shopList.Count; l++)
			{
				if (this.shopList[l].ItemID.Equals(this.NeedItemid))
				{
					this.curSelectObj = this.mShopItemBtns[l].gameObject;
					this.curSelectItem = this.shopList[l];
					this.NeedItemid = string.Empty;
					break;
				}
			}
		}
		this.NeedItemid = string.Empty;
		this.UpdateSelect(this.curSelectItem, this.curSelectObj);
		if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_WAIT)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004ADD RID: 19165 RVA: 0x0018B7E4 File Offset: 0x001899E4
	private void UpdateSelectItem()
	{
		this.ShowItemInfoRoot.RefershInfo(this.curSelectItem);
		if (this.curSelectItem.Limit > 0L)
		{
			this.remainCount = (int)(this.curSelectItem.Limit - this.curSelectItem.curNum);
		}
		else
		{
			this.remainCount = 999999;
		}
		if (this.buyCount > this.remainCount)
		{
			this.buyCount = this.remainCount;
		}
		if (this.remainCount < 1)
		{
			this.buyCount = 0;
		}
		this.UpdateBuyCost();
	}

	// Token: 0x06004ADE RID: 19166 RVA: 0x0018B878 File Offset: 0x00189A78
	private void UpdateBuyCost()
	{
		this.BuyCountLabel.text = string.Format("{0}", this.buyCount);
		ItemData itemDataByID = DataManager.GetItemDataByID(this.curSelectItem.ItemID);
		this.buyCost = (int)(this.curSelectItem.Price * (long)this.buyCount);
		this.BuyCostLabel.text = GameMoneyHelper.GetMoneyValStr(this.buyCost, (GameDefine.MONEY_TYPE)this.curSelectItem.PriceType);
	}

	// Token: 0x06004ADF RID: 19167 RVA: 0x0018B8F4 File Offset: 0x00189AF4
	public void OnClickBuyLeft()
	{
		if (this.buyCount > 1)
		{
			this.buyCount--;
			this.UpdateBuyCost();
		}
		else
		{
			NoticeLogic.AddNotifyData("#{101245}", true, false);
		}
	}

	// Token: 0x06004AE0 RID: 19168 RVA: 0x0018B928 File Offset: 0x00189B28
	public void OnClickBuyRight()
	{
		if (this.buyCount < this.remainCount)
		{
			this.buyCount++;
			ItemData itemDataByID = DataManager.GetItemDataByID(this.curSelectItem.ItemID);
			if (itemDataByID != null && itemDataByID.Type == GameDefine.ITEM_TYPE.BOX)
			{
				ShopData shopDataByID = DataManager.GetShopDataByID(this.curSelectItem.ID);
				if (this.buyCount > shopDataByID.SingleLimit)
				{
					this.buyCount = shopDataByID.SingleLimit;
					NoticeLogic.AddNotifyData("#{301124}", true, false);
				}
			}
			this.UpdateBuyCost();
		}
		else
		{
			NoticeLogic.AddNotifyData("#{101246}", true, false);
		}
	}

	// Token: 0x06004AE1 RID: 19169 RVA: 0x0018B9CC File Offset: 0x00189BCC
	public void OnClickBuyItem()
	{
		if (this.curSelectItem == null)
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_BUY)
			{
				TutorialManager.CloseTutorial();
			}
			return;
		}
		if (this.buyCount < 1)
		{
			if (this.remainCount == 0)
			{
				NoticeLogic.AddNotifyData("#{101244}", true, false);
			}
			if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_BUY)
			{
				TutorialManager.CloseTutorial();
			}
			return;
		}
		if (!GameMoneyHelper.BeforeCheckBuy((GameDefine.MONEY_TYPE)this.curSelectItem.PriceType, this.buyCost))
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_BUY)
			{
				TutorialManager.CloseTutorial();
			}
			return;
		}
		ShopData shopDataByID = DataManager.GetShopDataByID(this.curSelectItem.ID);
		if (shopDataByID != null && !string.IsNullOrEmpty(shopDataByID.EndTime) && TimeTools.GetShopItemTime(shopDataByID.EndTimes).TotalSeconds <= 0.0)
		{
			if (GameManager.IsSupportCurDataVersion56())
			{
				NoticeLogic.AddNotifyData("#{301119}", true, false);
			}
			else
			{
				NoticeLogic.AddNotifyData("Sales time is over. The item cannot be purchased.", true, false);
			}
			if (TutorialManager.CurStep == TUTORIAL_STEP.BUY_BADGE_CLICK_BUY)
			{
				TutorialManager.CloseTutorial();
			}
			return;
		}
		buy_shop_item.request request = new buy_shop_item.request();
		request.ID = this.curSelectItem.ID;
		request.itemCount = (long)this.buyCount;
		request.type = (long)this.curType;
		NetLogic.GetInstance().Send<Protocol.buy_shop_item>(request, null);
	}

	// Token: 0x06004AE2 RID: 19170 RVA: 0x0018BB24 File Offset: 0x00189D24
	public void UpdateShopItem(shop_item item, long type)
	{
		if (type != (long)this.curType || item == null)
		{
			return;
		}
		if (this.curSelectItem != null && item.ID == this.curSelectItem.ID)
		{
			this.curSelectItem.curNum = item.curNum;
			this.UpdateSelect(this.curSelectItem, this.curSelectObj);
		}
		if (this.shopList != null)
		{
			for (int i = 0; i < this.shopList.Count; i++)
			{
				if (this.shopList[i].ID == item.ID)
				{
					this.shopList[i].curNum = item.curNum;
				}
			}
		}
		for (int j = 0; j < this.mShopItemBtns.Count; j++)
		{
			this.mShopItemBtns[j].UpdateInfo(item);
		}
	}

	// Token: 0x06004AE3 RID: 19171 RVA: 0x0018BC1C File Offset: 0x00189E1C
	public void UpdateMoneyLabel()
	{
		if (this.specialMoneyObj != null)
		{
			if (this.curType == GameDefine.SHOP_TYPE.GUILD_SHOP)
			{
				this.specialMoneyFlag.spriteName = GameMoneyHelper.GetMoneyIcon(GameDefine.MONEY_TYPE.GUILD_CONTRIBUTE);
				UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, true);
				this.specialMoneyFlag.MakePixelPerfect();
				this.MoneyLabel.text = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.GuildContribute.ToString();
			}
			else if (this.curType == GameDefine.SHOP_TYPE.BATTLECOIN_SHOP)
			{
				this.specialMoneyFlag.spriteName = GameMoneyHelper.GetMoneyIcon(GameDefine.MONEY_TYPE.BATTLECOIN);
				UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, true);
				this.specialMoneyFlag.MakePixelPerfect();
				this.MoneyLabel.text = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BattleCoin.ToString();
			}
			else if (this.curType == GameDefine.SHOP_TYPE.ACTIVITY_SHOP)
			{
				this.specialMoneyFlag.spriteName = GameMoneyHelper.GetMoneyIcon(GameDefine.MONEY_TYPE.ACTIVITYCOIN);
				UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, true);
				this.specialMoneyFlag.MakePixelPerfect();
				this.MoneyLabel.text = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityCoin.ToString();
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.specialMoneyObj, false);
			}
		}
	}

	// Token: 0x06004AE4 RID: 19172 RVA: 0x0018BD58 File Offset: 0x00189F58
	public void OnClickLeft()
	{
		int num = this.curPage - 1;
		if (num > 0)
		{
			WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
			ask_shop_list.request request = new ask_shop_list.request();
			request.curPage = (long)num;
			request.type = (long)this.curType;
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
		}
	}

	// Token: 0x06004AE5 RID: 19173 RVA: 0x0018BDB4 File Offset: 0x00189FB4
	public void OnClikcRight()
	{
		int num = this.curPage + 1;
		if (num <= this.maxPage)
		{
			WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
			ask_shop_list.request request = new ask_shop_list.request();
			request.curPage = (long)num;
			request.type = (long)this.curType;
			NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request, null);
		}
	}

	// Token: 0x06004AE6 RID: 19174 RVA: 0x0018BE14 File Offset: 0x0018A014
	private void UpdateSelect(shop_item item)
	{
		for (int i = 0; i < this.mShopItemBtns.Count; i++)
		{
			this.mShopItemBtns[i].UpdateSelect(item);
		}
	}

	// Token: 0x06004AE7 RID: 19175 RVA: 0x0018BE50 File Offset: 0x0018A050
	public void UpdateSelect(shop_item item, GameObject obj)
	{
		this.curSelectObj = obj;
		if (this.curSelectItem == null || !this.curSelectItem.ID.Equals(item.ID))
		{
			this.buyCount = 1;
		}
		this.curSelectItem = item;
		this.UpdateSelect(item);
		this.UpdateSelectItem();
		this.flurryShopItemclick(item);
	}

	// Token: 0x06004AE8 RID: 19176 RVA: 0x0018BEAC File Offset: 0x0018A0AC
	private void flurryShopItemclick(shop_item clickitem)
	{
		switch (this.curType)
		{
		case GameDefine.SHOP_TYPE.TOOL_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_tool", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.EQUIP_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_equip", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.BIGSALE_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_bigsale", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.GUILD_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_guild", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.BATTLECOIN_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_battle", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		case GameDefine.SHOP_TYPE.ACTIVITY_SHOP:
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Shop_activity", string.Format("shopitem_{0}", clickitem.ID), "clicktimes");
			break;
		}
	}

	// Token: 0x06004AE9 RID: 19177 RVA: 0x0018BFE8 File Offset: 0x0018A1E8
	public void ClearSelectObj()
	{
		this.curSelectItem = null;
		this.curSelectObj = null;
	}

	// Token: 0x06004AEA RID: 19178 RVA: 0x0018BFF8 File Offset: 0x0018A1F8
	public void OnClickNumLabel()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NumRoot, delegate
		{
			SingletonUnity<NumRootLogic>.Instance.Reset(new DelegateDefine.OneIntParamDelegate(this.CalNumFun));
		}, null);
	}

	// Token: 0x06004AEB RID: 19179 RVA: 0x0018C018 File Offset: 0x0018A218
	private void CalNumFun(int num)
	{
		if (this.curSelectItem == null)
		{
			return;
		}
		int num2 = this.buyCount;
		int num3 = (int)(GameMoneyHelper.GetMoneyNum((int)this.curSelectItem.PriceType) / this.curSelectItem.Price);
		if (num == -1)
		{
			num2 /= 10;
		}
		else if (num == -2)
		{
			num2 = num3;
		}
		else
		{
			num2 = num2 * 10 + num;
		}
		ItemData itemDataByID = DataManager.GetItemDataByID(this.curSelectItem.ItemID);
		if (itemDataByID != null && itemDataByID.Type == GameDefine.ITEM_TYPE.BOX)
		{
			ShopData shopDataByID = DataManager.GetShopDataByID(this.curSelectItem.ID);
			if (num2 > shopDataByID.SingleLimit)
			{
				num2 = shopDataByID.SingleLimit;
			}
		}
		if (num2 > this.remainCount)
		{
			num2 = this.remainCount;
		}
		if (this.remainCount < 1)
		{
			num2 = 0;
		}
		this.buyCount = num2;
		this.UpdateBuyCost();
	}

	// Token: 0x06004AEC RID: 19180 RVA: 0x0018C0F8 File Offset: 0x0018A2F8
	public void OnClickMaxBtn()
	{
		this.CalNumFun(-2);
	}

	// Token: 0x06004AED RID: 19181 RVA: 0x0018C104 File Offset: 0x0018A304
	public void ResetNormalLight()
	{
		RenderSettings.ambientLight = this.ambientLight;
	}

	// Token: 0x06004AEE RID: 19182 RVA: 0x0018C114 File Offset: 0x0018A314
	private void OnDisable()
	{
		this.ResetNormalLight();
		this.ShowItemInfoRoot.ModelViewObj.UnLoadFakeObj();
		if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.hideGangMoney();
		}
	}

	// Token: 0x06004AEF RID: 19183 RVA: 0x0018C160 File Offset: 0x0018A360
	public void OnClickMoneyGetBtn()
	{
		if (this.curType == GameDefine.SHOP_TYPE.GUILD_SHOP)
		{
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.GUILD_ACTIVITY))
			{
				return;
			}
			if (GameManager.IsSupportCurDataVersion56())
			{
				MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100795}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
				{
					if (SingletonUnity<NewGuildUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewGuildUIRootLogic>.Instance.gameObject))
					{
						SingletonUnity<NewGuildUIRootLogic>.Instance.OnClickGuildInfoBtn();
					}
					else
					{
						SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewGuildUIRootLogic, delegate
						{
							SingletonUnity<NewGuildUIRootLogic>.Instance.targetTabType = 0;
						}, null);
					}
				}, null, null, null);
			}
			else
			{
				MessageBoxLogic.OpenOKCancelBox("Your contribution points is insufficient.#rYou can contribute points by challenging [ffff00]Guild BOSS[-] or [ffff00]Donation[-].#rDo you want to go to the guild for contribution points?", StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
				{
					if (SingletonUnity<NewGuildUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewGuildUIRootLogic>.Instance.gameObject))
					{
						SingletonUnity<NewGuildUIRootLogic>.Instance.OnClickGuildInfoBtn();
					}
					else
					{
						SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
						SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewGuildUIRootLogic, delegate
						{
							SingletonUnity<NewGuildUIRootLogic>.Instance.targetTabType = 0;
						}, null);
					}
				}, null, null, null);
			}
		}
		else if (this.curType == GameDefine.SHOP_TYPE.BATTLECOIN_SHOP)
		{
			if (!this.CheckUnlockFunction(FUNCTION_TYPE.ACTIVITY_TIME))
			{
				return;
			}
			if (GameManager.IsSupportCurDataVersion56())
			{
				MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100656}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
				{
					SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE, null, false);
					}, null);
				}, null, null, null);
			}
			else
			{
				MessageBoxLogic.OpenOKCancelBox("The battle coin is not enough#rYou can get more coin in [ffff00]survival field[-]", StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
				{
					SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
					{
						SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickTimeBtn(GameDefine.ACTIVITY_TYPE.SURVIVE_BATTLE, null, false);
					}, null);
				}, null, null, null);
			}
		}
		else if (this.curType == GameDefine.SHOP_TYPE.ACTIVITY_SHOP)
		{
			if (GameManager.IsSupportCurDataVersion56())
			{
				MessageBoxLogic.OpenOKBox(StrDictionary.GetDictionaryString("#{502004}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), null);
			}
			else
			{
				MessageBoxLogic.OpenOKBox("Special coin are obtained at the event and can be exchanged for items in the event shop.", StrDictionary.GetDictionaryString("#{100127}", new object[0]), null);
			}
		}
	}

	// Token: 0x06004AF0 RID: 19184 RVA: 0x0018C32C File Offset: 0x0018A52C
	public bool CheckUnlockFunction(FUNCTION_TYPE curFunction)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData.IsFunctionUnlock(curFunction))
		{
			return true;
		}
		NoticeLogic.AddNotifyData("#{101539}", true, false);
		return false;
	}

	// Token: 0x04003879 RID: 14457
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x0400387A RID: 14458
	public ShopItemInfoNew ShowItemInfoRoot;

	// Token: 0x0400387B RID: 14459
	public List<ShopItemBtnLogic> mShopItemBtns = new List<ShopItemBtnLogic>();

	// Token: 0x0400387C RID: 14460
	public UIGrid ItemGrid;

	// Token: 0x0400387D RID: 14461
	public UILabel curPageLable;

	// Token: 0x0400387E RID: 14462
	private int curPage = 1;

	// Token: 0x0400387F RID: 14463
	private int maxPage = 1;

	// Token: 0x04003880 RID: 14464
	public GameObject specialMoneyObj;

	// Token: 0x04003881 RID: 14465
	public UILabel MoneyLabel;

	// Token: 0x04003882 RID: 14466
	public UISprite specialMoneyFlag;

	// Token: 0x04003883 RID: 14467
	private GameDefine.SHOP_TYPE curType;

	// Token: 0x04003884 RID: 14468
	private GameObject curSelectObj;

	// Token: 0x04003885 RID: 14469
	private shop_item curSelectItem;

	// Token: 0x04003886 RID: 14470
	private int buyCount = 1;

	// Token: 0x04003887 RID: 14471
	public UILabel BuyCountLabel;

	// Token: 0x04003888 RID: 14472
	public UILabel BuyCostLabel;

	// Token: 0x04003889 RID: 14473
	private int buyCost;

	// Token: 0x0400388A RID: 14474
	private int remainCount;

	// Token: 0x0400388B RID: 14475
	private List<shop_item> shopList;

	// Token: 0x0400388C RID: 14476
	public UILabel RefershTimeLabel;

	// Token: 0x0400388D RID: 14477
	private Color ambientLight;

	// Token: 0x0400388E RID: 14478
	private string NeedItemid = string.Empty;

	// Token: 0x0400388F RID: 14479
	public UIWidget BuyBtnRoot;
}
