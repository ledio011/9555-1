using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000946 RID: 2374
public class ItemInfoRootLogicNew : SingletonUnity<ItemInfoRootLogicNew>
{
	// Token: 0x06004237 RID: 16951 RVA: 0x0013F744 File Offset: 0x0013D944
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004238 RID: 16952 RVA: 0x0013F750 File Offset: 0x0013D950
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x17000FA1 RID: 4001
	// (get) Token: 0x06004239 RID: 16953 RVA: 0x0013F770 File Offset: 0x0013D970
	public GameItem CurItem
	{
		get
		{
			return this.mCurItem;
		}
	}

	// Token: 0x0600423A RID: 16954 RVA: 0x0013F778 File Offset: 0x0013D978
	public void OnClickBtn1()
	{
		if (this.mOnClickBtnList[0] != null)
		{
			this.mOnClickBtnList[0](this.mCurItem);
		}
	}

	// Token: 0x0600423B RID: 16955 RVA: 0x0013F7A8 File Offset: 0x0013D9A8
	public void OnClickBtn2()
	{
		if (this.mOnClickBtnList[1] != null)
		{
			this.mOnClickBtnList[1](this.mCurItem);
		}
	}

	// Token: 0x0600423C RID: 16956 RVA: 0x0013F7D8 File Offset: 0x0013D9D8
	public void OnClickBtn3()
	{
		if (this.mOnClickBtnList[2] != null)
		{
			this.mOnClickBtnList[2](this.mCurItem);
		}
	}

	// Token: 0x0600423D RID: 16957 RVA: 0x0013F808 File Offset: 0x0013DA08
	public void OnClickBtn4()
	{
		if (this.mOnClickBtnList[3] != null)
		{
			this.mOnClickBtnList[3](this.mCurItem);
		}
	}

	// Token: 0x0600423E RID: 16958 RVA: 0x0013F838 File Offset: 0x0013DA38
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRootNew);
		if (this.onClose != null)
		{
			this.onClose();
		}
	}

	// Token: 0x0600423F RID: 16959 RVA: 0x0013F860 File Offset: 0x0013DA60
	private void UpdateRootPos(ITEM_SHOW_TYPE showType)
	{
		if (showType == ITEM_SHOW_TYPE.BACKPACK)
		{
			Vector3 localPosition = this.ItemInfoRoot.transform.localPosition;
			localPosition.x = this.LeftPos;
			this.ItemInfoRoot.transform.localPosition = localPosition;
			localPosition = this.ConsumeItemInfoRoot.transform.localPosition;
			localPosition.x = this.LeftPos;
			this.ConsumeItemInfoRoot.transform.localPosition = localPosition;
		}
		else if (showType == ITEM_SHOW_TYPE.EQUIPPACK)
		{
			Vector3 localPosition2 = this.ItemInfoRoot.transform.localPosition;
			localPosition2.x = this.RightPos;
			this.ItemInfoRoot.transform.localPosition = localPosition2;
			localPosition2 = this.ConsumeItemInfoRoot.transform.localPosition;
			localPosition2.x = this.RightPos;
			this.ConsumeItemInfoRoot.transform.localPosition = localPosition2;
		}
		else
		{
			Vector3 localPosition3 = this.ItemInfoRoot.transform.localPosition;
			localPosition3.x = this.CenterPos;
			this.ItemInfoRoot.transform.localPosition = localPosition3;
			localPosition3 = this.ConsumeItemInfoRoot.transform.localPosition;
			localPosition3.x = this.CenterPos;
			this.ConsumeItemInfoRoot.transform.localPosition = localPosition3;
		}
	}

	// Token: 0x06004240 RID: 16960 RVA: 0x0013F9A0 File Offset: 0x0013DBA0
	public static void ShowItemTips(shop_item item)
	{
		GameItem item1 = new GameItem(item.ItemID, (EQUIP_QUALITY)item.Quality, (int)item.curNum);
		if (item1.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item1.ItemLevel = 80;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
		{
			SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item1, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, true, false);
		}, null);
	}

	// Token: 0x06004241 RID: 16961 RVA: 0x0013FA14 File Offset: 0x0013DC14
	public static void ShowItemTips(consign_item item)
	{
		GameItem item1 = new GameItem(item.itemId, (EQUIP_QUALITY)item.quality, (int)item.stack);
		if (item1.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item1.ItemLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item1.ItemData.SubType);
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
		{
			SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item1, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, false, false);
		}, null);
	}

	// Token: 0x06004242 RID: 16962 RVA: 0x0013FAA8 File Offset: 0x0013DCA8
	public static void ShowItemTips(ItemData itemData, bool isNeedShowJumpPath = false, UI_PAGE_TYPE prePage = UI_PAGE_TYPE.INVALID)
	{
		GameItem item1 = new GameItem(itemData.ID, itemData.QualityType, 1);
		if (item1.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item1.ItemLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item1.ItemData.SubType);
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
		{
			SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item1, ITEM_SHOW_TYPE.REWARD_TIPS, isNeedShowJumpPath, prePage, false, false);
		}, null);
	}

	// Token: 0x06004243 RID: 16963 RVA: 0x0013FB44 File Offset: 0x0013DD44
	public static void ShowItemTips(gameitem item)
	{
		GameItem item1 = new GameItem(item.itemId, (EQUIP_QUALITY)item.quality, (int)item.stack);
		if (item1.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item1.ItemLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item1.ItemData.SubType);
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
		{
			SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item1, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, false, false);
		}, null);
	}

	// Token: 0x06004244 RID: 16964 RVA: 0x0013FBD8 File Offset: 0x0013DDD8
	public static void ShowItemTips(GameItem item, int level, bool isNeedShowJumpPath = false, UI_PAGE_TYPE prePage = UI_PAGE_TYPE.INVALID)
	{
		if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item.ItemLevel = level;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
		{
			SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, ITEM_SHOW_TYPE.REWARD_TIPS, isNeedShowJumpPath, prePage, false, false);
		}, null);
	}

	// Token: 0x06004245 RID: 16965 RVA: 0x0013FC40 File Offset: 0x0013DE40
	public static void ShowEquipTips(GameItem item, int level)
	{
		if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item.ItemLevel = level;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
			{
				SingletonUnity<ItemInfoRootLogicNew>.Instance.ResetAppraise(item, ITEM_SHOW_TYPE.REWARD_TIPS);
			}, null);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
			{
				SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, false, false);
			}, null);
		}
	}

	// Token: 0x06004246 RID: 16966 RVA: 0x0013FCBC File Offset: 0x0013DEBC
	public static void ShowEquipFullTips(GameItem item, int level)
	{
		if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item.ItemLevel = level;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
			{
				SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, false, true);
			}, null);
		}
		else
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
			{
				SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, false, false);
			}, null);
		}
	}

	// Token: 0x06004247 RID: 16967 RVA: 0x0013FD38 File Offset: 0x0013DF38
	public void ResetCompareEquip(GameItem gameItem, GameItem equipedItem)
	{
		NGUITools.SetActive(this.ConsumeItemInfoRoot.gameObject, false);
		NGUITools.SetActive(this.AppraiseRoot.gameObject, false);
		this.mCurItem = gameItem;
		ItemData itemData = this.mCurItem.ItemData;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.ItemInfoRoot.ItemNameLabel.text = itemData.MName;
		this.ItemInfoRoot.ItemIcon.spriteName = itemData.BackPackIcon;
		this.ItemInfoRoot.ClearItemLevel();
		if (itemData.CanSell())
		{
			this.ItemInfoRoot.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(itemData.GetSellPrice(this.mCurItem.GetItemQuality()), itemData.PriceType);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PriceLabel.gameObject, true);
		}
		else
		{
			this.ItemInfoRoot.PriceLabel.text = string.Empty;
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PriceLabel.gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemQualityIcon.gameObject, true);
		this.ItemInfoRoot.ItemQualityIcon.spriteName = gameItem.GetItemQuality().ToString();
		UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemEnhanceLabel.gameObject, true);
		int itemCombatVal = this.mCurItem.GetItemCombatVal();
		this.ItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100664}", new object[0]), itemCombatVal);
		this.ItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(gameItem.GetItemQuality());
		EquipData equipDataById = DataManager.GetEquipDataById(this.mCurItem.ItemId);
		if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			this.ItemInfoRoot.LevelNameLabel.text = StrDictionary.GetDictionaryString("#{100682}", new object[0]);
			this.ItemInfoRoot.LevelLabel.text = equipDataById.Class.ToString();
			this.SetLabelWarning(this.ItemInfoRoot.LevelLabel, false);
		}
		else
		{
			this.ItemInfoRoot.LevelNameLabel.text = StrDictionary.GetDictionaryString("#{100606}", new object[0]);
			this.ItemInfoRoot.LevelLabel.text = itemData.Level.ToString();
			this.SetLabelWarning(this.ItemInfoRoot.LevelLabel, playerData.Level < itemData.Level);
		}
		if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP && this.mCurItem.ItemData.SubType == 0)
		{
			this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.WeaponName[equipDataById.WeaponType], new object[0]);
			this.SetLabelWarning(this.ItemInfoRoot.ProfessionLabel, false);
			this.ItemInfoRoot.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{101219}", new object[0]);
		}
		else
		{
			this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.ProfessionName[equipDataById.Job], new object[0]);
			this.SetLabelWarning(this.ItemInfoRoot.ProfessionLabel, playerData.Profession != equipDataById.profession);
			this.ItemInfoRoot.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{100607}", new object[0]);
		}
		this.ItemInfoRoot.IsEquipedSprite.alpha = 0f;
		this.ItemInfoRoot.ShowBaseAtt(this.mCurItem, false);
		this.ItemInfoRoot.ShowOtherEquipInfo(this.mCurItem);
		if (equipedItem != null && !equipedItem.IsEmpty())
		{
			ItemData itemData2 = equipedItem.ItemData;
			this.EquipInfoRoot.ItemNameLabel.text = itemData2.MName;
			this.EquipInfoRoot.ItemIcon.spriteName = itemData2.BackPackIcon;
			this.EquipInfoRoot.ClearItemLevel();
			if (itemData.CanSell())
			{
				this.EquipInfoRoot.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(itemData2.GetSellPrice(equipedItem.GetItemQuality()), itemData2.PriceType);
				UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.PriceLabel.gameObject, true);
			}
			else
			{
				this.EquipInfoRoot.PriceLabel.text = string.Empty;
				UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.PriceLabel.gameObject, false);
			}
			UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.ItemQualityIcon.gameObject, true);
			this.EquipInfoRoot.ItemQualityIcon.spriteName = equipedItem.GetItemQuality().ToString();
			this.EquipInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(equipedItem.GetItemQuality());
			UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.ItemEnhanceLabel.gameObject, true);
			int itemCombatVal2 = equipedItem.GetItemCombatVal();
			this.EquipInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100664}", new object[0]), itemCombatVal2);
			EquipData equipDataById2 = DataManager.GetEquipDataById(equipedItem.ItemId);
			if (equipedItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				this.EquipInfoRoot.LevelNameLabel.text = StrDictionary.GetDictionaryString("#{100682}", new object[0]);
				this.EquipInfoRoot.LevelLabel.text = equipDataById2.Class.ToString();
				this.SetLabelWarning(this.EquipInfoRoot.LevelLabel, false);
			}
			else
			{
				this.EquipInfoRoot.LevelNameLabel.text = StrDictionary.GetDictionaryString("#{100606}", new object[0]);
				this.EquipInfoRoot.LevelLabel.text = itemData2.Level.ToString();
				this.SetLabelWarning(this.EquipInfoRoot.LevelLabel, false);
			}
			if (equipedItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP && equipedItem.ItemData.SubType == 0)
			{
				this.EquipInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.WeaponName[equipDataById2.WeaponType], new object[0]);
				this.SetLabelWarning(this.EquipInfoRoot.ProfessionLabel, false);
				this.EquipInfoRoot.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{101219}", new object[0]);
			}
			else
			{
				this.EquipInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.ProfessionName[equipDataById2.Job], new object[0]);
				this.SetLabelWarning(this.EquipInfoRoot.ProfessionLabel, playerData.Profession != equipDataById2.profession);
				this.EquipInfoRoot.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{100607}", new object[0]);
			}
			this.EquipInfoRoot.IsEquipedSprite.alpha = 1f;
			this.EquipInfoRoot.ShowBaseAtt(equipedItem, true);
			this.EquipInfoRoot.ShowOtherEquipInfo(equipedItem);
			this.EquipInfoRoot.BaseAttributeGrid.Reposition();
			this.EquipInfoRoot.RandomAttGrid.Reposition();
			if (itemCombatVal > itemCombatVal2)
			{
				NGUITools.SetActive(this.ItemInfoRoot.PowerUpArrow, true);
				NGUITools.SetActive(this.ItemInfoRoot.PowerDownArrow, false);
			}
			else if (itemCombatVal < itemCombatVal2)
			{
				NGUITools.SetActive(this.ItemInfoRoot.PowerUpArrow, false);
				NGUITools.SetActive(this.ItemInfoRoot.PowerDownArrow, true);
			}
			else
			{
				NGUITools.SetActive(this.ItemInfoRoot.PowerUpArrow, false);
				NGUITools.SetActive(this.ItemInfoRoot.PowerDownArrow, false);
			}
			NGUITools.SetActive(this.EquipInfoRoot.PowerUpArrow, false);
			NGUITools.SetActive(this.EquipInfoRoot.PowerDownArrow, false);
			this.UpdateRootPos(ITEM_SHOW_TYPE.EQUIPPACK);
		}
		else
		{
			NGUITools.SetActive(this.EquipInfoRoot.gameObject, false);
			NGUITools.SetActive(this.ItemInfoRoot.PowerUpArrow, true);
			NGUITools.SetActive(this.ItemInfoRoot.PowerDownArrow, false);
			NGUITools.SetActive(this.EquipInfoRoot.PowerUpArrow, false);
			NGUITools.SetActive(this.EquipInfoRoot.PowerDownArrow, false);
			this.UpdateRootPos(ITEM_SHOW_TYPE.BACKPACK);
		}
		this.ItemInfoRoot.BaseAttributeGrid.Reposition();
		this.ItemInfoRoot.RandomAttGrid.Reposition();
		if (equipedItem != null && !equipedItem.IsEmpty() && equipedItem.ItemData.Level >= this.mCurItem.ItemData.Level)
		{
			EquipData equipDataById3 = DataManager.GetEquipDataById(equipedItem.ItemId);
			if (this.mCurItem.ItemData.SubType == 0)
			{
				if (this.mCurItem.IsHaveRandomAtt)
				{
					if (equipDataById.WeaponType == equipDataById3.WeaponType)
					{
						this.SetBtn(3, "#{100614}", "#{100620}", "#{100621}", null);
						this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
						this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickInhertBtn);
						this.mOnClickBtnList[2] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
					}
					else
					{
						this.SetBtn(2, "#{100614}", "#{100621}", null, null);
						this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
						this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
					}
					if (this.CheckCanEquip(false))
					{
						this.SetBtnSp(this.BtnSpList[0], true);
					}
					else
					{
						this.SetBtnSp(this.BtnSpList[0], false);
					}
				}
				else
				{
					this.SetBtn(1, "#{100621}", null, null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
				}
			}
			else if (equipDataById.Job == equipDataById3.Job)
			{
				if (this.mCurItem.IsHaveRandomAtt)
				{
					this.SetBtn(3, "#{100614}", "#{100620}", "#{100621}", null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickInhertBtn);
					this.mOnClickBtnList[2] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
					if (this.CheckCanEquip(false))
					{
						this.SetBtnSp(this.BtnSpList[0], true);
					}
					else
					{
						this.SetBtnSp(this.BtnSpList[0], false);
					}
				}
				else
				{
					this.SetBtn(1, "#{100621}", null, null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
				}
			}
			else
			{
				this.SetBtn(1, "#{100621}", null, null, null);
				this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
			}
		}
		else if (this.mCurItem.ItemData.SubType == 0)
		{
			if (this.mCurItem.IsHaveRandomAtt)
			{
				this.SetBtn(2, "#{100614}", "#{100621}", null, null);
				this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
				this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
				if (this.CheckCanEquip(false))
				{
					this.SetBtnSp(this.BtnSpList[0], true);
				}
				else
				{
					this.SetBtnSp(this.BtnSpList[0], false);
				}
			}
			else
			{
				this.SetBtn(1, "#{100621}", null, null, null);
				this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
			}
		}
		else if (this.mCurItem.IsHaveRandomAtt)
		{
			this.SetBtn(2, "#{100614}", "#{100621}", null, null);
			this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
			this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
			if (this.CheckCanEquip(false))
			{
				this.SetBtnSp(this.BtnSpList[0], true);
			}
			else
			{
				this.SetBtnSp(this.BtnSpList[0], false);
			}
		}
		else
		{
			this.SetBtn(1, "#{100621}", null, null, null);
			this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
		}
	}

	// Token: 0x06004248 RID: 16968 RVA: 0x00140924 File Offset: 0x0013EB24
	public void ResetAppraise(GameItem gameItem, ITEM_SHOW_TYPE showType = ITEM_SHOW_TYPE.BACKPACK)
	{
		NGUITools.SetActive(this.ConsumeItemInfoRoot.gameObject, false);
		NGUITools.SetActive(this.EquipInfoRoot.gameObject, false);
		NGUITools.SetActive(this.ItemInfoObjRoot.gameObject, false);
		NGUITools.SetActive(this.AppraiseRoot.gameObject, true);
		this.SetBtn(0, null, null, null, null);
		this.AppraiseRoot.UpdateInfo(gameItem, showType);
	}

	// Token: 0x06004249 RID: 16969 RVA: 0x00140990 File Offset: 0x0013EB90
	public void Reset(GameItem gameItem, ITEM_SHOW_TYPE showType, bool isNeedShowJumpPath = false, UI_PAGE_TYPE prePage = UI_PAGE_TYPE.INVALID, bool isShopItem = false, bool isFullTips = false)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		this.mCurItem = gameItem;
		ItemData itemData = this.mCurItem.ItemData;
		this.mPrePage = prePage;
		if (isNeedShowJumpPath && !string.IsNullOrEmpty(itemData.JumpPath) && GameManager.IsSupportCurDataVersion47())
		{
			NGUITools.SetActive(this.ConsumeItemInfoRoot.gameObject, true);
			NGUITools.SetActive(this.EquipInfoRoot.gameObject, false);
			NGUITools.SetActive(this.ItemInfoObjRoot.gameObject, false);
			NGUITools.SetActive(this.AppraiseRoot.gameObject, false);
			this.UpdateRootPos(showType);
			this.ConsumeItemInfoRoot.ItemNameLabel.text = itemData.MName;
			this.ConsumeItemInfoRoot.ItemIcon.spriteName = itemData.BackPackIcon;
			this.ConsumeItemInfoRoot.ClearItemLevel();
			if (itemData.CanSell())
			{
				this.ConsumeItemInfoRoot.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(itemData.GetSellPrice(gameItem.GetItemQuality()), itemData.PriceType);
				UnityVersionUtil.SetActiveRecursive(this.ConsumeItemInfoRoot.PriceLabel.gameObject, true);
			}
			else
			{
				this.ConsumeItemInfoRoot.PriceLabel.text = string.Empty;
				UnityVersionUtil.SetActiveRecursive(this.ConsumeItemInfoRoot.PriceLabel.gameObject, false);
			}
			this.ConsumeItemInfoRoot.LevelLabel.text = itemData.Level.ToString();
			this.SetLabelWarning(this.ConsumeItemInfoRoot.LevelLabel, playerData.Level < itemData.Level);
			this.ConsumeItemInfoRoot.ItemQualityIcon.spriteName = itemData.QualityType.ToString();
			this.ConsumeItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(itemData.QualityType);
			this.ConsumeItemInfoRoot.TitleLabel.text = StrDictionary.GetDictionaryString("#{100611}", new object[0]);
			this.ConsumeItemInfoRoot.DescLabel.text = itemData.MDescription;
			if (itemData.Type == GameDefine.ITEM_TYPE.DANCE_TOOL || itemData.Type == GameDefine.ITEM_TYPE.WORLDSPEAK || itemData.Type == GameDefine.ITEM_TYPE.ENEMYWARP_TOOL)
			{
				if (gameItem.Parm[4] > 0)
				{
					this.ConsumeItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100643}", new object[0]), TimeTools.GetFormateTime((long)gameItem.Parm[4] - playerCommonData.GetCurServerTime()));
				}
				else if (gameItem.ItemData.UseHour > 0)
				{
					this.ConsumeItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100643}", new object[0]), TimeTools.GetFormateTime((long)(gameItem.ItemData.UseHour * 3600)));
				}
				else
				{
					this.ConsumeItemInfoRoot.ItemEnhanceLabel.text = string.Empty;
				}
			}
			else
			{
				this.ConsumeItemInfoRoot.ItemEnhanceLabel.text = string.Empty;
			}
			string[] array = itemData.JumpPath.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < this.ConsumeItemInfoRoot.JumpBtnRoot.Length; i++)
			{
				if (i < array.Length)
				{
					NGUITools.SetActive(this.ConsumeItemInfoRoot.JumpBtnRoot[i].gameObject, true);
					this.mJumpPath[i] = (JUMP_PATH)int.Parse(array[i]);
					switch (this.mJumpPath[i])
					{
					case JUMP_PATH.SHOP:
						this.ConsumeItemInfoRoot.JumpBtnLabel[i].text = StrDictionary.GetDictionaryString("#{100110}", new object[0]);
						break;
					case JUMP_PATH.SLOT:
						this.ConsumeItemInfoRoot.JumpBtnLabel[i].text = StrDictionary.GetDictionaryString("#{100109}", new object[0]);
						break;
					case JUMP_PATH.RACE:
						this.ConsumeItemInfoRoot.JumpBtnLabel[i].text = StrDictionary.GetDictionaryString("#{101569}", new object[0]);
						break;
					case JUMP_PATH.SCUFFLE:
						this.ConsumeItemInfoRoot.JumpBtnLabel[i].text = StrDictionary.GetDictionaryString("#{102017}", new object[0]);
						break;
					case JUMP_PATH.EQUIP_COPY:
						this.ConsumeItemInfoRoot.JumpBtnLabel[i].text = StrDictionary.GetDictionaryString("#{101510}", new object[0]);
						break;
					default:
						this.ConsumeItemInfoRoot.JumpBtnLabel[i].text = StrDictionary.GetDictionaryString("#{100110}", new object[0]);
						break;
					}
				}
				else
				{
					NGUITools.SetActive(this.ConsumeItemInfoRoot.JumpBtnRoot[i].gameObject, false);
				}
			}
			if (showType != ITEM_SHOW_TYPE.REWARD_TIPS)
			{
				GameDefine.ITEM_TYPE type = itemData.Type;
				if (type != GameDefine.ITEM_TYPE.POTION)
				{
					if (type == GameDefine.ITEM_TYPE.ENHANCE_ITEM)
					{
						if (this.mCurItem.ItemData.CanConsign())
						{
							this.SetBtn(1, "#{100623}", null, null, null);
							this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseEnhanceBtn);
						}
						else
						{
							this.SetBtn(1, "#{100623}", null, null, null);
							this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseEnhanceBtn);
						}
						if (this.CheckLevel(false))
						{
							this.SetBtnSp(this.BtnSpList[0], true);
						}
						else
						{
							this.SetBtnSp(this.BtnSpList[0], false);
						}
						goto IL_656;
					}
					if (type != GameDefine.ITEM_TYPE.POTION_2)
					{
						if (this.mCurItem.ItemData.CanConsign())
						{
							this.SetBtn(0, null, null, null, null);
						}
						else
						{
							this.SetBtn(0, null, null, null, null);
						}
						goto IL_656;
					}
				}
				if (this.mCurItem.ItemData.CanConsign())
				{
					this.SetBtn(2, "#{100623}", "#{100621}", null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseDragBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
				}
				else
				{
					this.SetBtn(2, "#{100623}", "#{100621}", null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseDragBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
				}
				if (this.CheckLevel(false))
				{
					this.SetBtnSp(this.BtnSpList[0], true);
				}
				else
				{
					this.SetBtnSp(this.BtnSpList[0], false);
				}
				IL_656:;
			}
			else
			{
				this.SetBtn(0, null, null, null, null);
			}
			return;
		}
		NGUITools.SetActive(this.ConsumeItemInfoRoot.gameObject, false);
		NGUITools.SetActive(this.EquipInfoRoot.gameObject, false);
		NGUITools.SetActive(this.AppraiseRoot.gameObject, false);
		this.UpdateRootPos(showType);
		this.ItemInfoRoot.ItemNameLabel.text = itemData.MName;
		this.ItemInfoRoot.ItemIcon.spriteName = itemData.BackPackIcon;
		this.ItemInfoRoot.ClearItemLevel();
		if (itemData.CanSell())
		{
			this.ItemInfoRoot.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(itemData.GetSellPrice(gameItem.GetItemQuality()), itemData.PriceType);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PriceLabel.gameObject, true);
		}
		else
		{
			this.ItemInfoRoot.PriceLabel.text = string.Empty;
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PriceLabel.gameObject, false);
		}
		if (itemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			this.ItemInfoRoot.LevelNameLabel.text = StrDictionary.GetDictionaryString("#{100682}", new object[0]);
			EquipData equipDataById = DataManager.GetEquipDataById(this.mCurItem.ItemId);
			this.ItemInfoRoot.LevelLabel.text = equipDataById.Class.ToString();
			this.SetLabelWarning(this.ItemInfoRoot.LevelLabel, false);
		}
		else
		{
			this.ItemInfoRoot.LevelNameLabel.text = StrDictionary.GetDictionaryString("#{100606}", new object[0]);
			this.ItemInfoRoot.LevelLabel.text = itemData.Level.ToString();
			this.SetLabelWarning(this.ItemInfoRoot.LevelLabel, playerData.Level < itemData.Level);
		}
		if (itemData.Type == GameDefine.ITEM_TYPE.EQUIP || itemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemQualityIcon.gameObject, true);
			this.ItemInfoRoot.ItemQualityIcon.spriteName = gameItem.GetItemQuality().ToString();
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemEnhanceLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PowerUpArrow, false);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PowerDownArrow, false);
			if (itemData.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				if (isShopItem)
				{
					this.ItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}[FFFF00] (Max)[-]", StrDictionary.GetDictionaryString("#{100664}", new object[0]), this.mCurItem.GetItemCombatVal());
				}
				else
				{
					this.ItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100664}", new object[0]), this.mCurItem.GetItemCombatVal());
				}
			}
			else if (gameItem.Parm[4] > 0)
			{
				this.ItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100643}", new object[0]), TimeTools.GetFormateTime((long)gameItem.Parm[4] - playerCommonData.GetCurServerTime()));
			}
			else if (gameItem.ItemData.UseHour > 0)
			{
				this.ItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100643}", new object[0]), TimeTools.GetFormateTime((long)(gameItem.ItemData.UseHour * 3600)));
			}
			else
			{
				this.ItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100643}", new object[0]), StrDictionary.GetDictionaryString("#{100646}", new object[0]));
			}
			this.ItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(gameItem.GetItemQuality());
			EquipData equipDataById2 = DataManager.GetEquipDataById(this.mCurItem.ItemId);
			if ((this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP || this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP) && this.mCurItem.ItemData.SubType == 0)
			{
				this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.WeaponName[equipDataById2.WeaponType], new object[0]);
				this.SetLabelWarning(this.ItemInfoRoot.ProfessionLabel, false);
				this.ItemInfoRoot.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{101219}", new object[0]);
			}
			else
			{
				this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.ProfessionName[equipDataById2.Job], new object[0]);
				this.SetLabelWarning(this.ItemInfoRoot.ProfessionLabel, playerData.Profession != equipDataById2.profession);
				this.ItemInfoRoot.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{100607}", new object[0]);
			}
			if (showType == ITEM_SHOW_TYPE.BACKPACK || showType == ITEM_SHOW_TYPE.REWARD_TIPS)
			{
				this.ItemInfoRoot.IsEquipedSprite.alpha = 0f;
			}
			else if (showType == ITEM_SHOW_TYPE.EQUIPPACK)
			{
				this.ItemInfoRoot.IsEquipedSprite.alpha = 1f;
			}
			if (showType == ITEM_SHOW_TYPE.EQUIPPACK || isFullTips)
			{
				this.ItemInfoRoot.ShowBaseAtt(this.mCurItem, true);
			}
			else
			{
				this.ItemInfoRoot.ShowBaseAtt(this.mCurItem, false);
			}
			this.ItemInfoRoot.ShowOtherEquipInfo(this.mCurItem);
		}
		else if (itemData.Type == GameDefine.ITEM_TYPE.BADGE)
		{
			BadgeData badgeDataById = DataManager.GetBadgeDataById(this.mCurItem.ItemId);
			this.ItemInfoRoot.ItemQualityIcon.spriteName = itemData.QualityType.ToString();
			this.ItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(itemData.QualityType);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemEnhanceLabel.gameObject, false);
			this.ItemInfoRoot.ItemEnhanceLabel.text = badgeDataById.Lv.ToString();
			this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString("#{100830}", new object[0]);
			this.ItemInfoRoot.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{100607}", new object[0]);
			if (showType == ITEM_SHOW_TYPE.BACKPACK || showType == ITEM_SHOW_TYPE.REWARD_TIPS)
			{
				this.ItemInfoRoot.IsEquipedSprite.alpha = 0f;
			}
			else if (showType == ITEM_SHOW_TYPE.EQUIPPACK)
			{
				this.ItemInfoRoot.IsEquipedSprite.alpha = 1f;
			}
			this.ItemInfoRoot.ShowBadgeInfo(this.mCurItem);
		}
		else if (itemData.Type == GameDefine.ITEM_TYPE.DANCE_TOOL || itemData.Type == GameDefine.ITEM_TYPE.WORLDSPEAK || itemData.Type == GameDefine.ITEM_TYPE.ENEMYWARP_TOOL)
		{
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemEnhanceLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PowerUpArrow, false);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PowerDownArrow, false);
			if (gameItem.Parm[4] > 0)
			{
				this.ItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100643}", new object[0]), TimeTools.GetFormateTime((long)gameItem.Parm[4] - playerCommonData.GetCurServerTime()));
			}
			else if (gameItem.ItemData.UseHour > 0)
			{
				this.ItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100643}", new object[0]), TimeTools.GetFormateTime((long)(gameItem.ItemData.UseHour * 3600)));
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemEnhanceLabel.gameObject, false);
			}
			this.ItemInfoRoot.ItemQualityIcon.spriteName = itemData.QualityType.ToString();
			this.ItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(itemData.QualityType);
			this.ItemInfoRoot.IsEquipedSprite.alpha = 0f;
			this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString("#{100830}", new object[0]);
			this.ItemInfoRoot.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{100607}", new object[0]);
			this.ItemInfoRoot.ShowDesInfo(this.mCurItem);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemEnhanceLabel.gameObject, false);
			this.ItemInfoRoot.ItemQualityIcon.spriteName = itemData.QualityType.ToString();
			this.ItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(itemData.QualityType);
			this.ItemInfoRoot.IsEquipedSprite.alpha = 0f;
			this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString("#{100830}", new object[0]);
			this.ItemInfoRoot.ProfessionNameLabel.text = StrDictionary.GetDictionaryString("#{100607}", new object[0]);
			this.ItemInfoRoot.ShowDesInfo(this.mCurItem);
		}
		this.ItemInfoRoot.BaseAttributeGrid.Reposition();
		this.ItemInfoRoot.RandomAttGrid.Reposition();
		if (showType != ITEM_SHOW_TYPE.REWARD_TIPS)
		{
			GameDefine.ITEM_TYPE type = itemData.Type;
			switch (type)
			{
			case GameDefine.ITEM_TYPE.BOX:
				if (this.mCurItem.ItemData.CanConsign())
				{
					this.SetBtn(2, "#{300405}", "#{300406}", null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenBoxBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenAllBoxBtn);
				}
				else
				{
					this.SetBtn(2, "#{300405}", "#{300406}", null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenBoxBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenAllBoxBtn);
				}
				if (this.CheckLevel(false))
				{
					this.SetBtnSp(this.BtnSpList[0], true);
					this.SetBtnSp(this.BtnSpList[1], true);
				}
				else
				{
					this.SetBtnSp(this.BtnSpList[0], false);
					this.SetBtnSp(this.BtnSpList[1], false);
				}
				goto IL_19F0;
			case GameDefine.ITEM_TYPE.FASHION_EQUIP:
				if (showType == ITEM_SHOW_TYPE.BACKPACK)
				{
					this.SetBtn(2, "#{100614}", "#{100621}", null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
				}
				else if (showType == ITEM_SHOW_TYPE.EQUIPPACK)
				{
					this.SetBtn(1, "#{100615}", null, null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickTakeOffBtn);
				}
				if (this.CheckCanEquip(false))
				{
					this.SetBtnSp(this.BtnSpList[0], true);
				}
				else
				{
					this.SetBtnSp(this.BtnSpList[0], false);
				}
				goto IL_19F0;
			default:
				switch (type)
				{
				case GameDefine.ITEM_TYPE.EQUIP:
					if (showType == ITEM_SHOW_TYPE.BACKPACK)
					{
						if (this.mCurItem.ItemData.CanConsign() && !this.mCurItem.BindFlag)
						{
							this.SetBtn(2, "#{100614}", "#{100621}", null, null);
							this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
							this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
						}
						else
						{
							this.SetBtn(2, "#{100614}", "#{100621}", null, null);
							this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
							this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
						}
						if (this.CheckCanEquip(false))
						{
							this.SetBtnSp(this.BtnSpList[0], true);
						}
						else
						{
							this.SetBtnSp(this.BtnSpList[0], false);
						}
					}
					else if (showType == ITEM_SHOW_TYPE.EQUIPPACK)
					{
						EquipData equipDataById3 = DataManager.GetEquipDataById(this.mCurItem.ItemId);
						if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP && equipDataById3.EquipType == EQUIP_BACKPACK_TYPE.WEAPON)
						{
							this.SetBtn(2, "#{100618}", "#{100118}", null, null);
							this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEnhanceBtn);
							this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSkillBtn);
							if (this.CheckFunctionUnlock(FUNCTION_TYPE.ENHANCE_EQUIP))
							{
								this.SetBtnSp(this.BtnSpList[0], true);
							}
							else
							{
								this.SetBtnSp(this.BtnSpList[0], false);
							}
							if (this.CheckFunctionUnlock(FUNCTION_TYPE.SKILL))
							{
								this.SetBtnSp(this.BtnSpList[1], true);
							}
							else
							{
								this.SetBtnSp(this.BtnSpList[1], false);
							}
						}
						else
						{
							this.SetBtn(2, "#{100615}", "#{100618}", null, null);
							this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickTakeOffBtn);
							this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEnhanceBtn);
							if (this.CheckFunctionUnlock(FUNCTION_TYPE.ENHANCE_EQUIP))
							{
								this.SetBtnSp(this.BtnSpList[1], true);
							}
							else
							{
								this.SetBtnSp(this.BtnSpList[1], false);
							}
						}
					}
					goto IL_19F0;
				case GameDefine.ITEM_TYPE.POTION:
					goto IL_123A;
				case GameDefine.ITEM_TYPE.ENHANCE_ITEM:
					if (this.mCurItem.ItemData.CanConsign())
					{
						this.SetBtn(1, "#{100623}", null, null, null);
						this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseEnhanceBtn);
					}
					else
					{
						this.SetBtn(1, "#{100623}", null, null, null);
						this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseEnhanceBtn);
					}
					if (this.CheckLevel(false))
					{
						this.SetBtnSp(this.BtnSpList[0], true);
					}
					else
					{
						this.SetBtnSp(this.BtnSpList[0], false);
					}
					goto IL_19F0;
				case GameDefine.ITEM_TYPE.BADGE:
					if (showType == ITEM_SHOW_TYPE.BACKPACK)
					{
						BadgeData badgeDataById2 = DataManager.GetBadgeDataById(this.mCurItem.ItemId);
						if (badgeDataById2.Lv < GameDefine.MAX_BADGE_LEVEL)
						{
							if (this.mCurItem.ItemData.CanConsign())
							{
								this.SetBtn(2, "#{100614}", "#{100616}", null, null);
								this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
								this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickMergeBtn);
							}
							else
							{
								this.SetBtn(2, "#{100614}", "#{100616}", null, null);
								this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
								this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickMergeBtn);
							}
							if (this.CheckFunctionUnlock(FUNCTION_TYPE.ENHANCE_BADGE))
							{
								this.SetBtnSp(this.BtnSpList[1], true);
							}
							else
							{
								this.SetBtnSp(this.BtnSpList[1], false);
							}
						}
						else if (this.mCurItem.ItemData.CanConsign())
						{
							this.SetBtn(1, "#{100614}", null, null, null);
							this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
						}
						else
						{
							this.SetBtn(1, "#{100614}", null, null, null);
							this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEquipBtn);
						}
					}
					else if (showType == ITEM_SHOW_TYPE.EQUIPPACK)
					{
						this.SetBtn(1, "#{100615}", null, null, null);
						this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickTakeOffBtn);
					}
					goto IL_19F0;
				}
				this.SetBtn(0, null, null, null, null);
				goto IL_19F0;
			case GameDefine.ITEM_TYPE.REMAIN:
				if (this.mCurItem.ItemData.CanConsign())
				{
					this.SetBtn(1, "#{100623}", null, null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUsItemBtn);
				}
				else
				{
					this.SetBtn(1, "#{100623}", null, null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUsItemBtn);
				}
				if (this.CheckLevel(false))
				{
					this.SetBtnSp(this.BtnSpList[0], true);
				}
				else
				{
					this.SetBtnSp(this.BtnSpList[0], false);
				}
				goto IL_19F0;
			case GameDefine.ITEM_TYPE.EXCHANGE:
				if (this.mCurItem.ItemData.CanConsign())
				{
					this.SetBtn(1, "#{100623}", null, null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseExChanegBtn);
				}
				else
				{
					this.SetBtn(1, "#{100623}", null, null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseExChanegBtn);
				}
				if (this.CheckLevel(false))
				{
					this.SetBtnSp(this.BtnSpList[0], true);
				}
				else
				{
					this.SetBtnSp(this.BtnSpList[0], false);
				}
				goto IL_19F0;
			case GameDefine.ITEM_TYPE.LOCK1:
				if (this.mCurItem.ItemData.CanConsign())
				{
					this.SetBtn(2, "#{300405}", "#{300406}", null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenBoxBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenAllBoxBtn);
				}
				else
				{
					this.SetBtn(2, "#{300405}", "#{300406}", null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenBoxBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenAllBoxBtn);
				}
				if (this.CheckLevel(false))
				{
					this.SetBtnSp(this.BtnSpList[0], true);
					this.SetBtnSp(this.BtnSpList[1], true);
				}
				else
				{
					this.SetBtnSp(this.BtnSpList[0], false);
					this.SetBtnSp(this.BtnSpList[1], false);
				}
				goto IL_19F0;
			case GameDefine.ITEM_TYPE.LOCK2:
				if (this.mCurItem.ItemData.CanConsign())
				{
					this.SetBtn(2, "#{100623}", "#{300407}", null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenBoxBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenAllBoxBtn);
				}
				else
				{
					this.SetBtn(2, "#{100623}", "#{300407}", null, null);
					this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenBoxBtn);
					this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickOpenAllBoxBtn);
				}
				if (this.CheckLevel(false))
				{
					this.SetBtnSp(this.BtnSpList[0], true);
					this.SetBtnSp(this.BtnSpList[1], true);
				}
				else
				{
					this.SetBtnSp(this.BtnSpList[0], false);
					this.SetBtnSp(this.BtnSpList[1], false);
				}
				goto IL_19F0;
			case GameDefine.ITEM_TYPE.RENAME:
				this.SetBtn(1, "#{100623}", null, null, null);
				this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUsItemBtn);
				if (this.CheckLevel(false))
				{
					this.SetBtnSp(this.BtnSpList[0], true);
				}
				else
				{
					this.SetBtnSp(this.BtnSpList[0], false);
				}
				goto IL_19F0;
			case GameDefine.ITEM_TYPE.POTION_2:
				break;
			}
			IL_123A:
			if (this.mCurItem.ItemData.CanConsign())
			{
				this.SetBtn(2, "#{100623}", "#{100621}", null, null);
				this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseDragBtn);
				this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
			}
			else
			{
				this.SetBtn(2, "#{100623}", "#{100621}", null, null);
				this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickUseDragBtn);
				this.mOnClickBtnList[1] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickSellBtn);
			}
			if (this.CheckLevel(false))
			{
				this.SetBtnSp(this.BtnSpList[0], true);
			}
			else
			{
				this.SetBtnSp(this.BtnSpList[0], false);
			}
			IL_19F0:;
		}
		else
		{
			this.SetBtn(0, null, null, null, null);
		}
	}

	// Token: 0x0600424A RID: 16970 RVA: 0x001423A0 File Offset: 0x001405A0
	private void SetBtn(int count, string btn0Name = null, string btn1Name = null, string btn2Name = null, string btn3Name = null)
	{
		for (int i = 0; i < this.BtnList.Length; i++)
		{
			NGUITools.SetActive(this.BtnList[i].gameObject, i < count);
		}
		this.BtnLabelList[0].text = ((!string.IsNullOrEmpty(btn0Name)) ? StrDictionary.GetDictionaryString(btn0Name, new object[0]) : string.Empty);
		this.BtnLabelList[1].text = ((!string.IsNullOrEmpty(btn1Name)) ? StrDictionary.GetDictionaryString(btn1Name, new object[0]) : string.Empty);
		this.BtnLabelList[2].text = ((!string.IsNullOrEmpty(btn2Name)) ? StrDictionary.GetDictionaryString(btn2Name, new object[0]) : string.Empty);
		this.BtnLabelList[3].text = ((!string.IsNullOrEmpty(btn3Name)) ? StrDictionary.GetDictionaryString(btn3Name, new object[0]) : string.Empty);
		this.SetBtnSp(this.BtnSpList[0], true);
		this.BtnGride.Reposition();
	}

	// Token: 0x0600424B RID: 16971 RVA: 0x001424B4 File Offset: 0x001406B4
	public void SetBtnSp(UISprite curBtn, bool isenable)
	{
		if (isenable)
		{
			curBtn.spriteName = GameDefine.BtnIcon[1];
		}
		else
		{
			curBtn.spriteName = GameDefine.BtnIcon[2];
		}
	}

	// Token: 0x0600424C RID: 16972 RVA: 0x001424DC File Offset: 0x001406DC
	private bool CheckCanEquip(bool isShow = false)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (!playerData.CheckLevel(this.mCurItem.ItemData.Level))
		{
			if (isShow)
			{
				TutorialManager.LevelLimitAction();
			}
			return false;
		}
		EquipData equipDataById = DataManager.GetEquipDataById(this.mCurItem.ItemId);
		if (this.mCurItem.ItemData.SubType == 0)
		{
			return true;
		}
		if (equipDataById != null && playerData.Profession != equipDataById.profession)
		{
			if (isShow)
			{
				NoticeLogic.AddNotifyData("#{100641}", true, false);
			}
			return false;
		}
		return true;
	}

	// Token: 0x0600424D RID: 16973 RVA: 0x00142570 File Offset: 0x00140770
	private bool CheckProfession(bool isShow = false)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		EquipData equipDataById = DataManager.GetEquipDataById(this.mCurItem.ItemId);
		if (equipDataById != null && playerData.Profession != equipDataById.profession)
		{
			if (isShow)
			{
				NoticeLogic.AddNotifyData("#{100641}", true, false);
			}
			return false;
		}
		return true;
	}

	// Token: 0x0600424E RID: 16974 RVA: 0x001425C8 File Offset: 0x001407C8
	private bool CheckLevel(bool isShow = false)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (!playerData.CheckLevel(this.mCurItem.ItemData.Level))
		{
			if (isShow)
			{
				NoticeLogic.AddNotifyData("#{100642}", true, false);
			}
			return false;
		}
		return true;
	}

	// Token: 0x0600424F RID: 16975 RVA: 0x00142610 File Offset: 0x00140810
	private bool CanTakeOff()
	{
		if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			ItemContainer equipBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipBackPack;
			if (equipBackPack.GetContainerEmptyNum() < 1)
			{
				return false;
			}
		}
		else if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.BADGE)
		{
			ItemContainer badgeBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BadgeBackPack;
			if (badgeBackPack.GetContainerEmptyNum() < 1)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06004250 RID: 16976 RVA: 0x0014268C File Offset: 0x0014088C
	private void OnClickUseBuff(GameItem item)
	{
		this.OnClickCloseBtn();
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.UseBuffDrag(item);
		}
	}

	// Token: 0x06004251 RID: 16977 RVA: 0x001426C0 File Offset: 0x001408C0
	private void OnClickUseDragBtn(GameItem item)
	{
		if (!this.CheckLevel(true))
		{
			return;
		}
		this.OnClickCloseBtn();
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.UseDrag(item);
		}
	}

	// Token: 0x06004252 RID: 16978 RVA: 0x00142700 File Offset: 0x00140900
	private void OnClickUseEnhanceBtn(GameItem item)
	{
		if (!this.CheckLevel(true))
		{
			return;
		}
		if (item.ItemId.Equals("3001"))
		{
			if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ENHANCE_EQUIP))
			{
				return;
			}
		}
		else if ((item.ItemId.Equals("4001") || item.ItemId.Equals("4002") || item.ItemId.Equals("4003")) && !SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ENHANCE_STAR))
		{
			return;
		}
		this.OnClickCloseBtn();
		SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
		{
			if (item.ItemId.Equals("3001"))
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
			}
			else if (item.ItemId.Equals("4001") || item.ItemId.Equals("4002") || item.ItemId.Equals("4003"))
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipRefine();
			}
		}, null);
	}

	// Token: 0x06004253 RID: 16979 RVA: 0x001427F4 File Offset: 0x001409F4
	private void OnClickConsignBtn(GameItem item)
	{
	}

	// Token: 0x06004254 RID: 16980 RVA: 0x001427F8 File Offset: 0x001409F8
	private void OnClickSellBtn(GameItem item)
	{
		int num = (int)((float)(item.ItemData.GetSellPrice(item.GetItemQuality()) * item.StackNum) * 1f);
		MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{100636}", new object[]
		{
			num
		}), StrDictionary.GetDictionaryString("#{100127}", new object[0]), delegate
		{
			if (this.mCurItem.ItemData.Type != GameDefine.ITEM_TYPE.BADGE)
			{
				sell_item.request request = new sell_item.request();
				request.indexId = this.mCurItem.IndexId;
				request.itemCount = (long)this.mCurItem.StackNum;
				request.type = (long)item.ContainerType;
				NetLogic.GetInstance().Send<Protocol.sell_item>(request, null);
			}
			this.OnClickCloseBtn();
		}, null, null, null);
	}

	// Token: 0x06004255 RID: 16981 RVA: 0x00142888 File Offset: 0x00140A88
	private void OnClickTakeOffBtn(GameItem item)
	{
		if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(117, 0.5f))
			{
				if (this.CanTakeOff())
				{
					Singleton<ObjManager>.Instance.MainPlayer.UnEquipItem(this.mCurItem);
					this.OnClickCloseBtn();
				}
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100272}", true, false);
			}
		}
		else if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.BADGE)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(198, 0.5f))
			{
				if (this.CanTakeOff())
				{
					Singleton<ObjManager>.Instance.MainPlayer.UnEquipBadge(this.mCurItem);
					this.OnClickCloseBtn();
				}
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100272}", true, false);
			}
		}
		else if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(221, 0.5f))
			{
				ItemContainer fashionEquipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FashionEquipPack;
				Singleton<ObjManager>.Instance.MainPlayer.UnEquipFashionItem(this.mCurItem);
				this.OnClickCloseBtn();
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100272}", true, false);
			}
		}
	}

	// Token: 0x06004256 RID: 16982 RVA: 0x001429D4 File Offset: 0x00140BD4
	private void OnClickEquipBtn(GameItem item)
	{
		if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			if (!this.CheckCanEquip(true))
			{
				return;
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(116, 0.5f))
			{
				Singleton<ObjManager>.Instance.MainPlayer.EquipItem(this.mCurItem, false);
				this.OnClickCloseBtn();
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100272}", true, false);
			}
		}
		else if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.BADGE)
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.BADGE_CLICK_EQUIP)
			{
				this.CheckTutorialEvent();
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(197, 0.5f))
			{
				ItemContainer badgeEquipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BadgeEquipPack;
				int firstEmptyItemIndex = badgeEquipPack.GetFirstEmptyItemIndex();
				if (firstEmptyItemIndex > -1)
				{
					Singleton<ObjManager>.Instance.MainPlayer.EquipBadge(this.mCurItem, firstEmptyItemIndex);
				}
				else
				{
					NoticeLogic.AddNotifyData("#{100638}", true, false);
				}
				this.OnClickCloseBtn();
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100272}", true, false);
			}
		}
		else if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			if (!this.CheckCanEquip(true))
			{
				return;
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(221, 0.5f))
			{
				ItemContainer fashionEquipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FashionEquipPack;
				Singleton<ObjManager>.Instance.MainPlayer.EquipFashionItem(this.mCurItem);
				this.OnClickCloseBtn();
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100272}", true, false);
			}
		}
	}

	// Token: 0x06004257 RID: 16983 RVA: 0x00142B68 File Offset: 0x00140D68
	private void OnClickMergeBtn(GameItem item)
	{
		if (this.CheckFunctionUnlock(FUNCTION_TYPE.ENHANCE_BADGE))
		{
			this.OnClickCloseBtn();
			SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickCloseBtn();
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowBadgeMerge(item, EquipStrengthenUIRootLogic.OPENTYPE.BADGE);
			}, null);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{100642}", true, false);
		}
	}

	// Token: 0x06004258 RID: 16984 RVA: 0x00142BD0 File Offset: 0x00140DD0
	private void OnClickEnhanceBtn(GameItem item)
	{
		if (!this.CheckFunctionUnlock(FUNCTION_TYPE.ENHANCE_EQUIP))
		{
			NoticeLogic.AddNotifyData("#{100642}", true, false);
			return;
		}
		this.OnClickCloseBtn();
		SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
		{
			SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowEquipEnhance(item, EquipStrengthenUIRootLogic.OPENTYPE.EQUIP);
		}, null);
	}

	// Token: 0x06004259 RID: 16985 RVA: 0x00142C34 File Offset: 0x00140E34
	private void OnClickInhertBtn(GameItem item)
	{
		int equipEnhanceLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item.ItemData.SubType);
		if (item != null)
		{
			item.ItemLevel = equipEnhanceLevel;
		}
		GameItem playerEquipItem = SingletonUnity<PlayerModelPageRootLogic>.Instance.GetTargetTypeEquip((EQUIP_BACKPACK_TYPE)item.ItemData.SubType);
		if (playerEquipItem != null && !playerEquipItem.IsEmpty())
		{
			playerEquipItem.ItemLevel = equipEnhanceLevel;
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipInhertRoot, delegate
			{
				SingletonUnity<EquipInhertRootLogic>.Instance.ShowInfo(item, playerEquipItem);
			}, null);
		}
		this.OnClickCloseBtn();
	}

	// Token: 0x0600425A RID: 16986 RVA: 0x00142CF4 File Offset: 0x00140EF4
	private void OnClickSkillBtn(GameItem item)
	{
		if (!this.CheckFunctionUnlock(FUNCTION_TYPE.SKILL))
		{
			NoticeLogic.AddNotifyData("#{100642}", true, false);
			return;
		}
		this.OnClickCloseBtn();
		SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickCloseBtn();
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EquipStrengthenUIRootLogic, delegate
		{
			SingletonUnity<EquipStrengthenUIRootLogic>.Instance.ShowSkillInfo(EquipStrengthenUIRootLogic.OPENTYPE.EQUIP);
		}, null);
	}

	// Token: 0x0600425B RID: 16987 RVA: 0x00142D5C File Offset: 0x00140F5C
	private void OnClickOpenBoxBtn(GameItem item)
	{
		if (!this.CheckLevel(true))
		{
			return;
		}
		this.OnClickCloseBtn();
		ItemData itemData = item.ItemData;
		open_item_package.request request = new open_item_package.request();
		if (itemData.Type == GameDefine.ITEM_TYPE.BOX)
		{
			request.indexId = item.IndexId;
			request.count = 1L;
			NetLogic.GetInstance().Send<Protocol.open_item_package>(request, null);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OpenBoxRoot, delegate
			{
				SingletonUnity<OpenBoxRootLogic>.Instance.Reset();
			}, null);
		}
		else if (itemData.Type == GameDefine.ITEM_TYPE.LOCK1)
		{
			string funLock = itemData.FunLock;
			ItemContainer itemBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
			List<GameItem> itemByItemId = itemBackPack.GetItemByItemId(funLock);
			if (itemByItemId != null && itemByItemId.Count > 0)
			{
				request.indexId = item.IndexId;
				request.indexId2 = itemByItemId[0].IndexId;
				request.count = 1L;
				NetLogic.GetInstance().Send<Protocol.open_item_package>(request, null);
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OpenBoxRoot, delegate
				{
					SingletonUnity<OpenBoxRootLogic>.Instance.Reset();
				}, null);
			}
			else
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(funLock);
				string dictionaryString = StrDictionary.GetDictionaryString("#{100647}", new object[]
				{
					itemData.MName,
					itemDataByID.MName
				});
				ShowItemsRootLogic.ShowYesBtn(itemDataByID, "#{100127}", dictionaryString, null, new object[0]);
			}
		}
		else if (itemData.Type == GameDefine.ITEM_TYPE.LOCK2)
		{
			string funLock2 = itemData.FunLock;
			ItemContainer itemBackPack2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
			List<GameItem> itemByItemId2 = itemBackPack2.GetItemByItemId(funLock2);
			if (itemByItemId2 != null && itemByItemId2.Count > 0)
			{
				request.indexId = itemByItemId2[0].IndexId;
				request.indexId2 = item.IndexId;
				request.count = 1L;
				NetLogic.GetInstance().Send<Protocol.open_item_package>(request, null);
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OpenBoxRoot, delegate
				{
					SingletonUnity<OpenBoxRootLogic>.Instance.Reset();
				}, null);
			}
			else
			{
				ItemData itemDataByID2 = DataManager.GetItemDataByID(funLock2);
				string dictionaryString2 = StrDictionary.GetDictionaryString("#{100648}", new object[]
				{
					itemData.MName,
					itemDataByID2.MName
				});
				ShowItemsRootLogic.ShowYesBtn(itemDataByID2, "#{100127}", dictionaryString2, null, new object[0]);
			}
		}
	}

	// Token: 0x0600425C RID: 16988 RVA: 0x00142FC4 File Offset: 0x001411C4
	private void OnClickOpenAllBoxBtn(GameItem item)
	{
		if (!this.CheckLevel(true))
		{
			return;
		}
		this.OnClickCloseBtn();
		ItemData itemData = item.ItemData;
		open_item_package.request request = new open_item_package.request();
		if (itemData.Type == GameDefine.ITEM_TYPE.BOX)
		{
			request.indexId = item.IndexId;
			request.count = (long)item.StackNum;
			if (request.count > 99L)
			{
				request.count = 99L;
			}
			NetLogic.GetInstance().Send<Protocol.open_item_package>(request, null);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OpenBoxRoot, delegate
			{
				SingletonUnity<OpenBoxRootLogic>.Instance.Reset();
			}, null);
		}
		else if (itemData.Type == GameDefine.ITEM_TYPE.LOCK1)
		{
			string funLock = itemData.FunLock;
			ItemContainer itemBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
			List<GameItem> itemByItemId = itemBackPack.GetItemByItemId(funLock);
			if (itemByItemId != null && itemByItemId.Count > 0)
			{
				request.indexId = item.IndexId;
				request.indexId2 = itemByItemId[0].IndexId;
				request.count = (long)Mathf.Min(item.StackNum, itemBackPack.GetItemStackNumById(funLock));
				if (request.count > 99L)
				{
					request.count = 99L;
				}
				NetLogic.GetInstance().Send<Protocol.open_item_package>(request, null);
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OpenBoxRoot, delegate
				{
					SingletonUnity<OpenBoxRootLogic>.Instance.Reset();
				}, null);
			}
			else
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(funLock);
				string dictionaryString = StrDictionary.GetDictionaryString("#{100647}", new object[]
				{
					itemData.MName,
					itemDataByID.MName
				});
				ShowItemsRootLogic.ShowYesBtn(itemDataByID, "#{100127}", dictionaryString, null, new object[0]);
			}
		}
		else if (itemData.Type == GameDefine.ITEM_TYPE.LOCK2)
		{
			string funLock2 = itemData.FunLock;
			ItemContainer itemBackPack2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
			List<GameItem> itemByItemId2 = itemBackPack2.GetItemByItemId(funLock2);
			if (itemByItemId2 != null && itemByItemId2.Count > 0)
			{
				request.indexId = itemByItemId2[0].IndexId;
				request.indexId2 = item.IndexId;
				request.count = (long)Mathf.Min(item.StackNum, itemBackPack2.GetItemStackNumById(funLock2));
				if (request.count > 99L)
				{
					request.count = 99L;
				}
				NetLogic.GetInstance().Send<Protocol.open_item_package>(request, null);
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OpenBoxRoot, delegate
				{
					SingletonUnity<OpenBoxRootLogic>.Instance.Reset();
				}, null);
			}
			else
			{
				ItemData itemDataByID2 = DataManager.GetItemDataByID(funLock2);
				string dictionaryString2 = StrDictionary.GetDictionaryString("#{100648}", new object[]
				{
					itemData.MName,
					itemDataByID2.MName
				});
				ShowItemsRootLogic.ShowYesBtn(itemDataByID2, "#{100127}", dictionaryString2, null, new object[0]);
			}
		}
	}

	// Token: 0x0600425D RID: 16989 RVA: 0x00143298 File Offset: 0x00141498
	private void LockYesBtnFun()
	{
		GameMoneyHelper.ShowItemProduct(this.CurItem.ItemData.FunLock, GameDefine.SHOP_TYPE.TOOL_SHOP);
	}

	// Token: 0x0600425E RID: 16990 RVA: 0x001432B0 File Offset: 0x001414B0
	private void OnClickUseExChanegBtn(GameItem item)
	{
		if (!this.CheckLevel(true))
		{
			return;
		}
		this.OnClickCloseBtn();
		use_item.request request = new use_item.request();
		request.indexId = item.IndexId;
		NetLogic.GetInstance().Send<Protocol.use_item>(request, null);
	}

	// Token: 0x0600425F RID: 16991 RVA: 0x001432F0 File Offset: 0x001414F0
	private void OnClickUsItemBtn(GameItem item)
	{
		if (!this.CheckLevel(true))
		{
			return;
		}
		this.OnClickCloseBtn();
		if (item.ItemData.Type == GameDefine.ITEM_TYPE.RENAME)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RenameRoot, null, null);
		}
		else
		{
			use_item.request request = new use_item.request();
			request.indexId = item.IndexId;
			NetLogic.GetInstance().Send<Protocol.use_item>(request, null);
		}
	}

	// Token: 0x06004260 RID: 16992 RVA: 0x00143358 File Offset: 0x00141558
	public void SetLabelWarning(UILabel curlabel, bool istrue)
	{
		if (istrue)
		{
			curlabel.color = Color.red;
		}
	}

	// Token: 0x06004261 RID: 16993 RVA: 0x00143370 File Offset: 0x00141570
	public bool CheckFunctionUnlock(FUNCTION_TYPE type)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		return playerCommonData.IsFunctionUnlock(type);
	}

	// Token: 0x06004262 RID: 16994 RVA: 0x00143390 File Offset: 0x00141590
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.BADGE_CLICK_EQUIP)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004263 RID: 16995 RVA: 0x001433A4 File Offset: 0x001415A4
	public void OnClickJumpBtn1()
	{
		this.JumpPage(this.mJumpPath[0]);
	}

	// Token: 0x06004264 RID: 16996 RVA: 0x001433B4 File Offset: 0x001415B4
	public void OnClickJumpBtn2()
	{
		this.JumpPage(this.mJumpPath[1]);
	}

	// Token: 0x06004265 RID: 16997 RVA: 0x001433C4 File Offset: 0x001415C4
	public void OnClickJumpBtn3()
	{
		this.JumpPage(this.mJumpPath[2]);
	}

	// Token: 0x06004266 RID: 16998 RVA: 0x001433D4 File Offset: 0x001415D4
	public void OnClickJumpBtn4()
	{
		this.JumpPage(this.mJumpPath[3]);
	}

	// Token: 0x06004267 RID: 16999 RVA: 0x001433E4 File Offset: 0x001415E4
	private void JumpPage(JUMP_PATH jumpPath)
	{
		switch (jumpPath)
		{
		case JUMP_PATH.SHOP:
			this.OnClickCloseBtn();
			GameMoneyHelper.ShowItemProduct(this.mCurItem.ItemId, GameDefine.SHOP_TYPE.TOOL_SHOP);
			break;
		case JUMP_PATH.SLOT:
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.LOTTO))
			{
				this.ClosePrePage();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotUIRoot, delegate
				{
					SingletonUnity<SlotUIRootLogic>.Instance.EnableReset();
					WaitResponseUIRootLogic.OpenWaitBox(242, 10f, 0f, null);
					NetLogic.GetInstance().Send<Protocol.request_slot_info>(null, null);
					SingletonUnity<SlotUIRootLogic>.Instance.SetPrePage(this.mPrePage);
				}, null);
				this.OnClickCloseBtn();
			}
			else
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100834}", new object[0]), true, false);
			}
			break;
		case JUMP_PATH.RACE:
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				this.OnClickCloseBtn();
				this.ClosePrePage();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.CAR_CHASE_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
					SingletonUnity<NewActivityUIRootLogic>.Instance.SetPrePage(this.mPrePage);
				}, null);
			}
			else
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
			}
			break;
		case JUMP_PATH.SCUFFLE:
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				this.OnClickCloseBtn();
				this.ClosePrePage();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.SCUFFLE_AREA_1, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
					SingletonUnity<NewActivityUIRootLogic>.Instance.SetPrePage(this.mPrePage);
				}, null);
			}
			else
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
			}
			break;
		case JUMP_PATH.EQUIP_COPY:
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ACTIVITY_DAILY))
			{
				this.OnClickCloseBtn();
				this.ClosePrePage();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
				{
					SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.EQUIP_COPY, null, null, GameDefine.ACTIVITY_TYPE.INVALID);
					SingletonUnity<NewActivityUIRootLogic>.Instance.SetPrePage(this.mPrePage);
				}, null);
			}
			else
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{101539}", new object[0]), true, false);
			}
			break;
		}
	}

	// Token: 0x06004268 RID: 17000 RVA: 0x001435BC File Offset: 0x001417BC
	private void ClosePrePage()
	{
		switch (this.mPrePage)
		{
		case UI_PAGE_TYPE.BACK_PACK_ITEM:
			if (SingletonUnity<PlayerInfoMenuRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PlayerInfoMenuRootLogic>.Instance.gameObject))
			{
				SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickCloseBtn();
			}
			break;
		case UI_PAGE_TYPE.ENHANCE_EQUIP:
			if (SingletonUnity<EquipStrengthenUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<EquipStrengthenUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.OnClickCloseBtn();
			}
			break;
		case UI_PAGE_TYPE.REFINE:
			if (SingletonUnity<EquipStrengthenUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<EquipStrengthenUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<EquipStrengthenUIRootLogic>.Instance.OnClickCloseBtn();
			}
			break;
		}
	}

	// Token: 0x04002E20 RID: 11808
	public const string equip_btn_str = "#{100614}";

	// Token: 0x04002E21 RID: 11809
	public const string un_equip_btn_str = "#{100615}";

	// Token: 0x04002E22 RID: 11810
	public const string use_str = "#{100623}";

	// Token: 0x04002E23 RID: 11811
	public const string useall_str = "#{300407}";

	// Token: 0x04002E24 RID: 11812
	public const string open_str = "#{300405}";

	// Token: 0x04002E25 RID: 11813
	public const string openall_str = "#{300406}";

	// Token: 0x04002E26 RID: 11814
	public const string recyle_str = "#{100621}";

	// Token: 0x04002E27 RID: 11815
	public const string trade_str = "#{100622}";

	// Token: 0x04002E28 RID: 11816
	public const string enhance_str = "#{100618}";

	// Token: 0x04002E29 RID: 11817
	public const string merge_str = "#{100616}";

	// Token: 0x04002E2A RID: 11818
	public const string split_str = "#{100617}";

	// Token: 0x04002E2B RID: 11819
	public const string skill_str = "#{100118}";

	// Token: 0x04002E2C RID: 11820
	public const string extra_str = "#{100623}";

	// Token: 0x04002E2D RID: 11821
	public const string Inhert_str = "#{100620}";

	// Token: 0x04002E2E RID: 11822
	public DelegateDefine.NoParamDelegate onClose;

	// Token: 0x04002E2F RID: 11823
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002E30 RID: 11824
	public float LeftPos;

	// Token: 0x04002E31 RID: 11825
	public float RightPos;

	// Token: 0x04002E32 RID: 11826
	public float CenterPos;

	// Token: 0x04002E33 RID: 11827
	public GameObject[] BtnList;

	// Token: 0x04002E34 RID: 11828
	public UILabel[] BtnLabelList;

	// Token: 0x04002E35 RID: 11829
	public UISprite[] BtnSpList;

	// Token: 0x04002E36 RID: 11830
	public UIGrid BtnGride;

	// Token: 0x04002E37 RID: 11831
	public AppraiseRootLogic AppraiseRoot;

	// Token: 0x04002E38 RID: 11832
	public ItemInfoSubRootLogicNew ItemInfoRoot;

	// Token: 0x04002E39 RID: 11833
	public GameObject ItemInfoObjRoot;

	// Token: 0x04002E3A RID: 11834
	public ItemInfoSubRootLogicNew EquipInfoRoot;

	// Token: 0x04002E3B RID: 11835
	public ItemInfoSubRootLogicNew ConsumeItemInfoRoot;

	// Token: 0x04002E3C RID: 11836
	private GameItem mCurItem;

	// Token: 0x04002E3D RID: 11837
	private DelegateDefine.OneGameItemParamDelegate[] mOnClickBtnList = new DelegateDefine.OneGameItemParamDelegate[4];

	// Token: 0x04002E3E RID: 11838
	private UI_PAGE_TYPE mPrePage = UI_PAGE_TYPE.INVALID;

	// Token: 0x04002E3F RID: 11839
	private JUMP_PATH[] mJumpPath = new JUMP_PATH[4];
}
