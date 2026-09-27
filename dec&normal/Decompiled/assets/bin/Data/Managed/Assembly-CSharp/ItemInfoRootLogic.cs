using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000943 RID: 2371
public class ItemInfoRootLogic : SingletonUnity<ItemInfoRootLogic>
{
	// Token: 0x060041FF RID: 16895 RVA: 0x0013B6B8 File Offset: 0x001398B8
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004200 RID: 16896 RVA: 0x0013B6C4 File Offset: 0x001398C4
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x17000FA0 RID: 4000
	// (get) Token: 0x06004201 RID: 16897 RVA: 0x0013B6E4 File Offset: 0x001398E4
	public GameItem CurItem
	{
		get
		{
			return this.mCurItem;
		}
	}

	// Token: 0x06004202 RID: 16898 RVA: 0x0013B6EC File Offset: 0x001398EC
	public void OnClickBtn1()
	{
		if (this.mOnClickBtnList[0] != null)
		{
			this.mOnClickBtnList[0](this.mCurItem);
		}
	}

	// Token: 0x06004203 RID: 16899 RVA: 0x0013B71C File Offset: 0x0013991C
	public void OnClickBtn2()
	{
		if (this.mOnClickBtnList[1] != null)
		{
			this.mOnClickBtnList[1](this.mCurItem);
		}
	}

	// Token: 0x06004204 RID: 16900 RVA: 0x0013B74C File Offset: 0x0013994C
	public void OnClickBtn3()
	{
		if (this.mOnClickBtnList[2] != null)
		{
			this.mOnClickBtnList[2](this.mCurItem);
		}
	}

	// Token: 0x06004205 RID: 16901 RVA: 0x0013B77C File Offset: 0x0013997C
	public void OnClickBtn4()
	{
		if (this.mOnClickBtnList[3] != null)
		{
			this.mOnClickBtnList[3](this.mCurItem);
		}
	}

	// Token: 0x06004206 RID: 16902 RVA: 0x0013B7AC File Offset: 0x001399AC
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ItemInfoRoot);
		if (this.onClose != null)
		{
			this.onClose();
		}
	}

	// Token: 0x06004207 RID: 16903 RVA: 0x0013B7D4 File Offset: 0x001399D4
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

	// Token: 0x06004208 RID: 16904 RVA: 0x0013B914 File Offset: 0x00139B14
	public static void ShowItemTips(shop_item item)
	{
		GameItem item1 = new GameItem(item.ItemID, (EQUIP_QUALITY)item.Quality, (int)item.curNum);
		if (item1.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item1.ItemLevel = 80;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRoot, delegate
		{
			SingletonUnity<ItemInfoRootLogic>.Instance.Reset(item1, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, true);
		}, null);
	}

	// Token: 0x06004209 RID: 16905 RVA: 0x0013B988 File Offset: 0x00139B88
	public static void ShowItemTips(consign_item item)
	{
		GameItem item1 = new GameItem(item.itemId, (EQUIP_QUALITY)item.quality, (int)item.stack);
		if (item1.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item1.ItemLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item1.ItemData.SubType);
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRoot, delegate
		{
			SingletonUnity<ItemInfoRootLogic>.Instance.Reset(item1, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, false);
		}, null);
	}

	// Token: 0x0600420A RID: 16906 RVA: 0x0013BA1C File Offset: 0x00139C1C
	public static void ShowItemTips(ItemData itemData, bool isNeedShowJumpPath = false, UI_PAGE_TYPE prePage = UI_PAGE_TYPE.INVALID)
	{
		GameItem item1 = new GameItem(itemData.ID, itemData.QualityType, 1);
		if (item1.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item1.ItemLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item1.ItemData.SubType);
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRoot, delegate
		{
			SingletonUnity<ItemInfoRootLogic>.Instance.Reset(item1, ITEM_SHOW_TYPE.REWARD_TIPS, isNeedShowJumpPath, prePage, false);
		}, null);
	}

	// Token: 0x0600420B RID: 16907 RVA: 0x0013BAB8 File Offset: 0x00139CB8
	public static void ShowItemTips(gameitem item)
	{
		GameItem item1 = new GameItem(item.itemId, (EQUIP_QUALITY)item.quality, (int)item.stack);
		if (item1.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item1.ItemLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item1.ItemData.SubType);
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRoot, delegate
		{
			SingletonUnity<ItemInfoRootLogic>.Instance.Reset(item1, ITEM_SHOW_TYPE.REWARD_TIPS, false, UI_PAGE_TYPE.INVALID, false);
		}, null);
	}

	// Token: 0x0600420C RID: 16908 RVA: 0x0013BB4C File Offset: 0x00139D4C
	public static void ShowItemTips(GameItem item, int level, bool isNeedShowJumpPath = false, UI_PAGE_TYPE prePage = UI_PAGE_TYPE.INVALID)
	{
		if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			item.ItemLevel = level;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRoot, delegate
		{
			SingletonUnity<ItemInfoRootLogic>.Instance.Reset(item, ITEM_SHOW_TYPE.REWARD_TIPS, isNeedShowJumpPath, prePage, false);
		}, null);
	}

	// Token: 0x0600420D RID: 16909 RVA: 0x0013BBB4 File Offset: 0x00139DB4
	public void ResetCompareEquip(GameItem gameItem, GameItem equipedItem)
	{
		NGUITools.SetActive(this.ConsumeItemInfoRoot.gameObject, false);
		this.mCurItem = gameItem;
		ItemData itemData = this.mCurItem.ItemData;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.ItemInfoRoot.ItemNameLabel.text = itemData.MName;
		this.ItemInfoRoot.ItemIcon.spriteName = itemData.BackPackIcon;
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
		this.ItemInfoRoot.LevelLabel.text = itemData.Level.ToString();
		this.SetLabelWarning(this.ItemInfoRoot.LevelLabel, playerData.Level < itemData.Level);
		this.ItemInfoRoot.BindingLabel.text = ((!this.mCurItem.BindFlag) ? StrDictionary.GetDictionaryString("#{100630}", new object[0]) : StrDictionary.GetDictionaryString("#{100629}", new object[0]));
		this.ItemInfoRoot.LegendSprite.enabled = false;
		UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemQualityIcon.gameObject, true);
		this.ItemInfoRoot.ItemQualityIcon.spriteName = gameItem.GetItemQuality().ToString();
		UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemEnhanceLabel.gameObject, true);
		int itemCombatVal = this.mCurItem.GetItemCombatVal();
		this.ItemInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100664}", new object[0]), itemCombatVal);
		this.ItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(gameItem.GetItemQuality());
		this.ItemInfoRoot.QualityLabel.text = GameDefine.GetStrByQuality(gameItem.GetItemQuality());
		UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ScoreLabel.gameObject, true);
		EquipData equipDataById = DataManager.GetEquipDataById(this.mCurItem.ItemId);
		this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.ProfessionName[equipDataById.Job], new object[0]);
		this.SetLabelWarning(this.ItemInfoRoot.ProfessionLabel, playerData.Profession != equipDataById.profession);
		this.ItemInfoRoot.DescLabel.text = string.Empty;
		this.ItemInfoRoot.TitleLabel.text = StrDictionary.GetDictionaryString("#{100612}", new object[0]);
		this.ItemInfoRoot.IsEquipedSprite.enabled = false;
		for (int i = 0; i < this.ItemInfoRoot.LeftAttrLabelList.Length; i++)
		{
			if (i <= (int)this.mCurItem.Quality)
			{
				UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.LeftAttrLabelList[i].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.LeftAttrLabelList[i].gameObject, false);
			}
		}
		int[] array = new int[]
		{
			-1,
			-1,
			-1,
			-1
		};
		if (this.mCurItem.Quality >= EQUIP_QUALITY.KUANG_WHITE)
		{
			this.ItemInfoRoot.LeftAttrLabelList[0].text = GameDefine.GetAttributeName_S((int)equipDataById.BaseStatusType);
			array[0] = equipDataById.GetAttrValByQualityAndLevel(0, (int)this.mCurItem.Quality, this.mCurItem.ItemLevel);
			this.ItemInfoRoot.RightAttrLabelList[0].text = GameDefine.GetAttributeValueStr((int)equipDataById.BaseStatusType, array[0]);
			this.ItemInfoRoot.LefeAttrIconList[0].spriteName = GameDefine.GetAttributeIcon((int)equipDataById.BaseStatusType);
		}
		if (this.mCurItem.Quality >= EQUIP_QUALITY.KUANG_GREEN)
		{
			this.ItemInfoRoot.LeftAttrLabelList[1].text = GameDefine.GetAttributeName_S(equipDataById.Status1);
			array[1] = equipDataById.GetAttrValByQualityAndLevel(1, (int)this.mCurItem.Quality, this.mCurItem.ItemLevel);
			this.ItemInfoRoot.RightAttrLabelList[1].text = GameDefine.GetAttributeValueStr(equipDataById.Status1, array[1]);
			this.ItemInfoRoot.LefeAttrIconList[1].spriteName = GameDefine.GetAttributeIcon(equipDataById.Status1);
		}
		if (this.mCurItem.Quality >= EQUIP_QUALITY.KUANG_BLUE)
		{
			this.ItemInfoRoot.LeftAttrLabelList[2].text = GameDefine.GetAttributeName_S(equipDataById.Status2);
			array[2] = equipDataById.GetAttrValByQualityAndLevel(2, (int)this.mCurItem.Quality, this.mCurItem.ItemLevel);
			this.ItemInfoRoot.RightAttrLabelList[2].text = GameDefine.GetAttributeValueStr(equipDataById.Status2, array[2]);
			this.ItemInfoRoot.LefeAttrIconList[2].spriteName = GameDefine.GetAttributeIcon(equipDataById.Status2);
		}
		if (this.mCurItem.Quality >= EQUIP_QUALITY.KUANG_PURPLE)
		{
			this.ItemInfoRoot.LegendSprite.enabled = true;
			this.ItemInfoRoot.LeftAttrLabelList[3].text = GameDefine.GetAttributeName_S(equipDataById.ExStatus);
			array[3] = equipDataById.GetAttrValByQualityAndLevel(3, (int)this.mCurItem.Quality, this.mCurItem.ItemLevel);
			this.ItemInfoRoot.RightAttrLabelList[3].text = GameDefine.GetAttributeValueStr(equipDataById.ExStatus, array[3]);
			this.ItemInfoRoot.LefeAttrIconList[3].spriteName = GameDefine.GetAttributeIcon(equipDataById.ExStatus);
		}
		if (equipedItem != null && !equipedItem.IsEmpty())
		{
			ItemData itemData2 = equipedItem.ItemData;
			this.EquipInfoRoot.ItemNameLabel.text = itemData2.MName;
			this.EquipInfoRoot.ItemIcon.spriteName = itemData2.BackPackIcon;
			if (itemData.CanSell())
			{
				this.EquipInfoRoot.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(itemData2.GetSellPrice(equipedItem.Quality), itemData2.PriceType);
				UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.PriceLabel.gameObject, true);
			}
			else
			{
				this.EquipInfoRoot.PriceLabel.text = string.Empty;
				UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.PriceLabel.gameObject, false);
			}
			this.EquipInfoRoot.LevelLabel.text = itemData2.Level.ToString();
			this.SetLabelWarning(this.EquipInfoRoot.LevelLabel, false);
			this.EquipInfoRoot.BindingLabel.text = ((!equipedItem.BindFlag) ? StrDictionary.GetDictionaryString("#{100630}", new object[0]) : StrDictionary.GetDictionaryString("#{100629}", new object[0]));
			this.EquipInfoRoot.LegendSprite.enabled = false;
			UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.ItemQualityIcon.gameObject, true);
			this.EquipInfoRoot.ItemQualityIcon.spriteName = equipedItem.Quality.ToString();
			UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.ItemEnhanceLabel.gameObject, true);
			int itemCombatVal2 = equipedItem.GetItemCombatVal();
			this.EquipInfoRoot.ItemEnhanceLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100664}", new object[0]), itemCombatVal2);
			this.EquipInfoRoot.QualityLabel.text = GameDefine.GetStrByQuality(equipedItem.Quality);
			UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.ScoreLabel.gameObject, true);
			EquipData equipDataById2 = DataManager.GetEquipDataById(equipedItem.ItemId);
			this.EquipInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.ProfessionName[equipDataById2.Job], new object[0]);
			this.SetLabelWarning(this.EquipInfoRoot.ProfessionLabel, playerData.Profession != equipDataById2.profession);
			this.EquipInfoRoot.DescLabel.text = string.Empty;
			this.EquipInfoRoot.TitleLabel.text = StrDictionary.GetDictionaryString("#{100612}", new object[0]);
			this.EquipInfoRoot.IsEquipedSprite.enabled = true;
			for (int j = 0; j < this.EquipInfoRoot.LeftAttrLabelList.Length; j++)
			{
				if (j <= (int)equipedItem.Quality)
				{
					UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.LeftAttrLabelList[j].gameObject, true);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.EquipInfoRoot.LeftAttrLabelList[j].gameObject, false);
				}
			}
			int[] array2 = new int[]
			{
				-1,
				-1,
				-1,
				-1
			};
			if (equipedItem.Quality >= EQUIP_QUALITY.KUANG_WHITE)
			{
				this.EquipInfoRoot.LeftAttrLabelList[0].text = GameDefine.GetAttributeName_S((int)equipDataById2.BaseStatusType);
				array2[0] = equipDataById2.GetAttrValByQualityAndLevel(0, (int)equipedItem.Quality, equipedItem.ItemLevel);
				this.EquipInfoRoot.RightAttrLabelList[0].text = GameDefine.GetAttributeValueStr((int)equipDataById2.BaseStatusType, array2[0]);
				this.EquipInfoRoot.LefeAttrIconList[0].spriteName = GameDefine.GetAttributeIcon((int)equipDataById2.BaseStatusType);
			}
			if (equipedItem.Quality >= EQUIP_QUALITY.KUANG_GREEN)
			{
				this.EquipInfoRoot.LeftAttrLabelList[1].text = GameDefine.GetAttributeName_S(equipDataById2.Status1);
				array2[1] = equipDataById2.GetAttrValByQualityAndLevel(1, (int)equipedItem.Quality, equipedItem.ItemLevel);
				this.EquipInfoRoot.RightAttrLabelList[1].text = GameDefine.GetAttributeValueStr((int)equipDataById2.BaseStatusType, array2[1]);
				this.EquipInfoRoot.LefeAttrIconList[1].spriteName = GameDefine.GetAttributeIcon(equipDataById2.Status1);
			}
			if (equipedItem.Quality >= EQUIP_QUALITY.KUANG_BLUE)
			{
				this.EquipInfoRoot.LeftAttrLabelList[2].text = GameDefine.GetAttributeName_S(equipDataById2.Status2);
				array2[2] = equipDataById2.GetAttrValByQualityAndLevel(2, (int)equipedItem.Quality, equipedItem.ItemLevel);
				this.EquipInfoRoot.RightAttrLabelList[2].text = GameDefine.GetAttributeValueStr((int)equipDataById2.BaseStatusType, array2[2]);
				this.EquipInfoRoot.LefeAttrIconList[2].spriteName = GameDefine.GetAttributeIcon(equipDataById2.Status2);
			}
			if (equipedItem.Quality >= EQUIP_QUALITY.KUANG_PURPLE)
			{
				this.EquipInfoRoot.LegendSprite.enabled = true;
				this.EquipInfoRoot.LeftAttrLabelList[3].text = GameDefine.GetAttributeName_S(equipDataById2.ExStatus);
				array2[3] = equipDataById2.GetAttrValByQualityAndLevel(3, (int)equipedItem.Quality, equipedItem.ItemLevel);
				this.EquipInfoRoot.RightAttrLabelList[3].text = GameDefine.GetAttributeValueStr((int)equipDataById2.BaseStatusType, array2[3]);
				this.EquipInfoRoot.LefeAttrIconList[3].spriteName = GameDefine.GetAttributeIcon(equipDataById2.ExStatus);
			}
			this.EquipInfoRoot.AttributeGrid.Reposition();
			this.EquipInfoRoot.InfoGrid.Reposition();
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k] > array2[k])
				{
					NGUITools.SetActive(this.ItemInfoRoot.UpArrowList[k], true);
					NGUITools.SetActive(this.ItemInfoRoot.DownArrowList[k], false);
				}
				else if (array[k] < array2[k] && array[k] > 0)
				{
					NGUITools.SetActive(this.ItemInfoRoot.UpArrowList[k], false);
					NGUITools.SetActive(this.ItemInfoRoot.DownArrowList[k], true);
				}
				else
				{
					NGUITools.SetActive(this.ItemInfoRoot.UpArrowList[k], false);
					NGUITools.SetActive(this.ItemInfoRoot.DownArrowList[k], false);
				}
				NGUITools.SetActive(this.EquipInfoRoot.UpArrowList[k], false);
				NGUITools.SetActive(this.EquipInfoRoot.DownArrowList[k], false);
			}
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
			for (int l = 0; l < array.Length; l++)
			{
				if (array[l] > 0)
				{
					NGUITools.SetActive(this.ItemInfoRoot.UpArrowList[l], true);
					NGUITools.SetActive(this.ItemInfoRoot.DownArrowList[l], false);
				}
				else
				{
					NGUITools.SetActive(this.ItemInfoRoot.UpArrowList[l], false);
					NGUITools.SetActive(this.ItemInfoRoot.DownArrowList[l], false);
				}
			}
			NGUITools.SetActive(this.ItemInfoRoot.PowerUpArrow, true);
			NGUITools.SetActive(this.ItemInfoRoot.PowerDownArrow, false);
			NGUITools.SetActive(this.EquipInfoRoot.PowerUpArrow, false);
			NGUITools.SetActive(this.EquipInfoRoot.PowerDownArrow, false);
			this.UpdateRootPos(ITEM_SHOW_TYPE.BACKPACK);
		}
		this.ItemInfoRoot.AttributeGrid.Reposition();
		this.ItemInfoRoot.InfoGrid.Reposition();
		if (!this.mCurItem.BindFlag && this.mCurItem.ItemData.CanConsign())
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

	// Token: 0x0600420E RID: 16910 RVA: 0x0013CA04 File Offset: 0x0013AC04
	public void Reset(GameItem gameItem, ITEM_SHOW_TYPE showType, bool isNeedShowJumpPath = false, UI_PAGE_TYPE prePage = UI_PAGE_TYPE.INVALID, bool isShopItem = false)
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
			this.UpdateRootPos(showType);
			this.ConsumeItemInfoRoot.ItemNameLabel.text = itemData.MName;
			this.ConsumeItemInfoRoot.ItemIcon.spriteName = itemData.BackPackIcon;
			if (itemData.CanSell())
			{
				this.ConsumeItemInfoRoot.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(itemData.GetSellPrice(gameItem.Quality), itemData.PriceType);
				UnityVersionUtil.SetActiveRecursive(this.ConsumeItemInfoRoot.PriceLabel.gameObject, true);
			}
			else
			{
				this.ConsumeItemInfoRoot.PriceLabel.text = string.Empty;
				UnityVersionUtil.SetActiveRecursive(this.ConsumeItemInfoRoot.PriceLabel.gameObject, false);
			}
			this.ConsumeItemInfoRoot.LevelLabel.text = itemData.Level.ToString();
			this.SetLabelWarning(this.ConsumeItemInfoRoot.LevelLabel, playerData.Level < itemData.Level);
			this.ConsumeItemInfoRoot.TitleLabel.text = StrDictionary.GetDictionaryString("#{100611}", new object[0]);
			this.ConsumeItemInfoRoot.ItemQualityIcon.spriteName = itemData.QualityType.ToString();
			this.ConsumeItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(itemData.QualityType);
			this.ConsumeItemInfoRoot.DescLabel.text = itemData.MDescription;
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
						goto IL_53B;
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
						goto IL_53B;
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
				IL_53B:;
			}
			else
			{
				this.SetBtn(0, null, null, null, null);
			}
			return;
		}
		NGUITools.SetActive(this.ConsumeItemInfoRoot.gameObject, false);
		NGUITools.SetActive(this.EquipInfoRoot.gameObject, false);
		this.UpdateRootPos(showType);
		this.ItemInfoRoot.ItemNameLabel.text = itemData.MName;
		this.ItemInfoRoot.ItemIcon.spriteName = itemData.BackPackIcon;
		if (itemData.CanSell())
		{
			this.ItemInfoRoot.PriceLabel.text = GameMoneyHelper.GetMoneyValStr(itemData.GetSellPrice(gameItem.Quality), itemData.PriceType);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PriceLabel.gameObject, true);
		}
		else
		{
			this.ItemInfoRoot.PriceLabel.text = string.Empty;
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.PriceLabel.gameObject, false);
		}
		this.ItemInfoRoot.LevelLabel.text = itemData.Level.ToString();
		this.SetLabelWarning(this.ItemInfoRoot.LevelLabel, playerData.Level < itemData.Level);
		this.ItemInfoRoot.BindingLabel.text = ((!this.mCurItem.BindFlag) ? StrDictionary.GetDictionaryString("#{100630}", new object[0]) : StrDictionary.GetDictionaryString("#{100629}", new object[0]));
		this.ItemInfoRoot.LegendSprite.enabled = false;
		if (itemData.Type == GameDefine.ITEM_TYPE.EQUIP || itemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemQualityIcon.gameObject, true);
			this.ItemInfoRoot.ItemQualityIcon.spriteName = gameItem.Quality.ToString();
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
			this.ItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(gameItem.Quality);
			this.ItemInfoRoot.QualityLabel.text = GameDefine.GetStrByQuality(gameItem.Quality);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ScoreLabel.gameObject, true);
			EquipData equipDataById = DataManager.GetEquipDataById(this.mCurItem.ItemId);
			this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString(GameDefine.ProfessionName[equipDataById.Job], new object[0]);
			this.SetLabelWarning(this.ItemInfoRoot.ProfessionLabel, playerData.Profession != equipDataById.profession);
			this.ItemInfoRoot.DescLabel.text = string.Empty;
			this.ItemInfoRoot.TitleLabel.text = StrDictionary.GetDictionaryString("#{100612}", new object[0]);
			if (showType == ITEM_SHOW_TYPE.BACKPACK || showType == ITEM_SHOW_TYPE.REWARD_TIPS)
			{
				this.ItemInfoRoot.IsEquipedSprite.enabled = false;
			}
			else if (showType == ITEM_SHOW_TYPE.EQUIPPACK)
			{
				this.ItemInfoRoot.IsEquipedSprite.enabled = true;
			}
			for (int j = 0; j < this.ItemInfoRoot.LeftAttrLabelList.Length; j++)
			{
				if (j <= (int)this.mCurItem.Quality)
				{
					UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.LeftAttrLabelList[j].gameObject, true);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.LeftAttrLabelList[j].gameObject, false);
				}
			}
			if (this.mCurItem.Quality >= EQUIP_QUALITY.KUANG_WHITE)
			{
				if (isShopItem)
				{
					this.ItemInfoRoot.LeftAttrLabelList[0].text = string.Format("{0}[FFFF00] (Max)[-]", GameDefine.GetAttributeName_S((int)equipDataById.BaseStatusType));
				}
				else
				{
					this.ItemInfoRoot.LeftAttrLabelList[0].text = GameDefine.GetAttributeName_S((int)equipDataById.BaseStatusType);
				}
				this.ItemInfoRoot.RightAttrLabelList[0].text = GameDefine.GetAttributeValueStr((int)equipDataById.BaseStatusType, equipDataById.GetAttrValByQualityAndLevel(0, (int)this.mCurItem.Quality, this.mCurItem.ItemLevel));
				this.ItemInfoRoot.LefeAttrIconList[0].spriteName = GameDefine.GetAttributeIcon((int)equipDataById.BaseStatusType);
			}
			if (this.mCurItem.Quality >= EQUIP_QUALITY.KUANG_GREEN)
			{
				if (isShopItem)
				{
					this.ItemInfoRoot.LeftAttrLabelList[1].text = string.Format("{0}[FFFF00] (Max)[-]", GameDefine.GetAttributeName_S(equipDataById.Status1));
				}
				else
				{
					this.ItemInfoRoot.LeftAttrLabelList[1].text = GameDefine.GetAttributeName_S(equipDataById.Status1);
				}
				this.ItemInfoRoot.RightAttrLabelList[1].text = GameDefine.GetAttributeValueStr(equipDataById.Status1, equipDataById.GetAttrValByQualityAndLevel(1, (int)this.mCurItem.Quality, this.mCurItem.ItemLevel));
				this.ItemInfoRoot.LefeAttrIconList[1].spriteName = GameDefine.GetAttributeIcon(equipDataById.Status1);
			}
			if (this.mCurItem.Quality >= EQUIP_QUALITY.KUANG_BLUE)
			{
				if (isShopItem)
				{
					this.ItemInfoRoot.LeftAttrLabelList[2].text = string.Format("{0}[FFFF00] (Max)[-]", GameDefine.GetAttributeName_S(equipDataById.Status2));
				}
				else
				{
					this.ItemInfoRoot.LeftAttrLabelList[2].text = GameDefine.GetAttributeName_S(equipDataById.Status2);
				}
				this.ItemInfoRoot.RightAttrLabelList[2].text = GameDefine.GetAttributeValueStr(equipDataById.Status2, equipDataById.GetAttrValByQualityAndLevel(2, (int)this.mCurItem.Quality, this.mCurItem.ItemLevel));
				this.ItemInfoRoot.LefeAttrIconList[2].spriteName = GameDefine.GetAttributeIcon(equipDataById.Status2);
			}
			if (this.mCurItem.Quality >= EQUIP_QUALITY.KUANG_PURPLE)
			{
				if (isShopItem)
				{
					this.ItemInfoRoot.LeftAttrLabelList[3].text = string.Format("{0}[FFFF00] (Max)[-]", GameDefine.GetAttributeName_S(equipDataById.ExStatus));
				}
				else
				{
					this.ItemInfoRoot.LeftAttrLabelList[3].text = GameDefine.GetAttributeName_S(equipDataById.ExStatus);
				}
				this.ItemInfoRoot.LegendSprite.enabled = true;
				this.ItemInfoRoot.RightAttrLabelList[3].text = GameDefine.GetAttributeValueStr(equipDataById.ExStatus, equipDataById.GetAttrValByQualityAndLevel(3, (int)this.mCurItem.Quality, this.mCurItem.ItemLevel));
				this.ItemInfoRoot.LefeAttrIconList[3].spriteName = GameDefine.GetAttributeIcon(equipDataById.ExStatus);
			}
		}
		else if (itemData.Type == GameDefine.ITEM_TYPE.BADGE)
		{
			BadgeData badgeDataById = DataManager.GetBadgeDataById(this.mCurItem.ItemId);
			this.ItemInfoRoot.ItemQualityIcon.spriteName = itemData.QualityType.ToString();
			this.ItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(itemData.QualityType);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemEnhanceLabel.gameObject, false);
			this.ItemInfoRoot.ItemEnhanceLabel.text = badgeDataById.Lv.ToString();
			this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString("#{100830}", new object[0]);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ScoreLabel.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.QualityLabel.gameObject, false);
			this.ItemInfoRoot.DescLabel.text = string.Empty;
			this.ItemInfoRoot.TitleLabel.text = StrDictionary.GetDictionaryString("#{100612}", new object[0]);
			if (showType == ITEM_SHOW_TYPE.BACKPACK || showType == ITEM_SHOW_TYPE.REWARD_TIPS)
			{
				this.ItemInfoRoot.IsEquipedSprite.enabled = false;
			}
			else if (showType == ITEM_SHOW_TYPE.EQUIPPACK)
			{
				this.ItemInfoRoot.IsEquipedSprite.enabled = true;
			}
			for (int k = 0; k < this.ItemInfoRoot.LeftAttrLabelList.Length; k++)
			{
				NGUITools.SetActive(this.ItemInfoRoot.LeftAttrLabelList[k].gameObject, false);
			}
			if (badgeDataById.Status1 != -1)
			{
				NGUITools.SetActive(this.ItemInfoRoot.LeftAttrLabelList[0].gameObject, true);
				this.ItemInfoRoot.LeftAttrLabelList[0].text = GameDefine.GetAttributeName_S(badgeDataById.Status1);
				this.ItemInfoRoot.RightAttrLabelList[0].text = GameDefine.GetAttributeValueStr(badgeDataById.Status1, badgeDataById.Value1);
				this.ItemInfoRoot.LefeAttrIconList[0].spriteName = GameDefine.GetAttributeIcon(badgeDataById.Status1);
			}
			if (badgeDataById.Status2 != -1)
			{
				NGUITools.SetActive(this.ItemInfoRoot.LeftAttrLabelList[1].gameObject, true);
				this.ItemInfoRoot.LeftAttrLabelList[1].text = GameDefine.GetAttributeName_S(badgeDataById.Status2);
				this.ItemInfoRoot.RightAttrLabelList[1].text = GameDefine.GetAttributeValueStr(badgeDataById.Status2, badgeDataById.Value2);
				this.ItemInfoRoot.LefeAttrIconList[1].spriteName = GameDefine.GetAttributeIcon(badgeDataById.Status2);
			}
		}
		else
		{
			this.ItemInfoRoot.TitleLabel.text = StrDictionary.GetDictionaryString("#{100611}", new object[0]);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ItemEnhanceLabel.gameObject, false);
			this.ItemInfoRoot.ItemQualityIcon.spriteName = itemData.QualityType.ToString();
			this.ItemInfoRoot.ItemNameLabel.color = GameDefine.GetColorByQuality(itemData.QualityType);
			this.ItemInfoRoot.IsEquipedSprite.enabled = false;
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.ScoreLabel.gameObject, false);
			this.ItemInfoRoot.ProfessionLabel.text = StrDictionary.GetDictionaryString("#{100830}", new object[0]);
			UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.QualityLabel.gameObject, false);
			this.ItemInfoRoot.DescLabel.text = itemData.MDescription;
			for (int l = 0; l < this.ItemInfoRoot.LeftAttrLabelList.Length; l++)
			{
				UnityVersionUtil.SetActiveRecursive(this.ItemInfoRoot.LeftAttrLabelList[l].gameObject, false);
			}
		}
		this.ItemInfoRoot.AttributeGrid.Reposition();
		this.ItemInfoRoot.InfoGrid.Reposition();
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
				goto IL_1B31;
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
				goto IL_1B31;
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
						EquipData equipDataById2 = DataManager.GetEquipDataById(this.mCurItem.ItemId);
						if (this.mCurItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP && equipDataById2.EquipType == EQUIP_BACKPACK_TYPE.WEAPON)
						{
							this.SetBtn(1, "#{100618}", null, null, null);
							this.mOnClickBtnList[0] = new DelegateDefine.OneGameItemParamDelegate(this.OnClickEnhanceBtn);
							if (this.CheckFunctionUnlock(FUNCTION_TYPE.ENHANCE_EQUIP))
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
					goto IL_1B31;
				case GameDefine.ITEM_TYPE.POTION:
					goto IL_1356;
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
					goto IL_1B31;
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
					goto IL_1B31;
				}
				if (this.mCurItem.ItemData.CanConsign())
				{
					this.SetBtn(0, null, null, null, null);
				}
				else
				{
					this.SetBtn(0, null, null, null, null);
				}
				goto IL_1B31;
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
				goto IL_1B31;
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
				goto IL_1B31;
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
				goto IL_1B31;
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
				goto IL_1B31;
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
				goto IL_1B31;
			case GameDefine.ITEM_TYPE.POTION_2:
				break;
			}
			IL_1356:
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
			IL_1B31:;
		}
		else
		{
			this.SetBtn(0, null, null, null, null);
		}
		for (int m = 0; m < this.ItemInfoRoot.UpArrowList.Length; m++)
		{
			NGUITools.SetActive(this.ItemInfoRoot.UpArrowList[m], false);
			NGUITools.SetActive(this.ItemInfoRoot.DownArrowList[m], false);
		}
	}

	// Token: 0x0600420F RID: 16911 RVA: 0x0013E59C File Offset: 0x0013C79C
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

	// Token: 0x06004210 RID: 16912 RVA: 0x0013E6B0 File Offset: 0x0013C8B0
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

	// Token: 0x06004211 RID: 16913 RVA: 0x0013E6D8 File Offset: 0x0013C8D8
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

	// Token: 0x06004212 RID: 16914 RVA: 0x0013E758 File Offset: 0x0013C958
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

	// Token: 0x06004213 RID: 16915 RVA: 0x0013E7B0 File Offset: 0x0013C9B0
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

	// Token: 0x06004214 RID: 16916 RVA: 0x0013E7F8 File Offset: 0x0013C9F8
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

	// Token: 0x06004215 RID: 16917 RVA: 0x0013E874 File Offset: 0x0013CA74
	private void OnClickUseBuff(GameItem item)
	{
		this.OnClickCloseBtn();
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.UseBuffDrag(item);
		}
	}

	// Token: 0x06004216 RID: 16918 RVA: 0x0013E8A8 File Offset: 0x0013CAA8
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

	// Token: 0x06004217 RID: 16919 RVA: 0x0013E8E8 File Offset: 0x0013CAE8
	private void OnClickUseEnhanceBtn(GameItem item)
	{
		if (!this.CheckLevel(true))
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

	// Token: 0x06004218 RID: 16920 RVA: 0x0013E93C File Offset: 0x0013CB3C
	private void OnClickConsignBtn(GameItem item)
	{
	}

	// Token: 0x06004219 RID: 16921 RVA: 0x0013E940 File Offset: 0x0013CB40
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

	// Token: 0x0600421A RID: 16922 RVA: 0x0013E9D0 File Offset: 0x0013CBD0
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

	// Token: 0x0600421B RID: 16923 RVA: 0x0013EB1C File Offset: 0x0013CD1C
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

	// Token: 0x0600421C RID: 16924 RVA: 0x0013ECB0 File Offset: 0x0013CEB0
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

	// Token: 0x0600421D RID: 16925 RVA: 0x0013ED18 File Offset: 0x0013CF18
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

	// Token: 0x0600421E RID: 16926 RVA: 0x0013ED7C File Offset: 0x0013CF7C
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
				ShowItemsRootLogic.ShowYestOrNoBtn(itemDataByID, 1, "#{100127}", "#{100647}", new DelegateDefine.NoParamDelegate(this.LockYesBtnFun), null, new object[0]);
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
				ShowItemsRootLogic.ShowYesBtn(itemDataByID2, "#{100127}", "#{100648}", null, new object[0]);
			}
		}
	}

	// Token: 0x0600421F RID: 16927 RVA: 0x0013EFAC File Offset: 0x0013D1AC
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
				ShowItemsRootLogic.ShowYestOrNoBtn(itemDataByID, 1, "#{100127}", "#{100647}", new DelegateDefine.NoParamDelegate(this.LockYesBtnFun), null, new object[0]);
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
				ShowItemsRootLogic.ShowYesBtn(itemDataByID2, "#{100127}", "#{100648}", null, new object[0]);
			}
		}
	}

	// Token: 0x06004220 RID: 16928 RVA: 0x0013F24C File Offset: 0x0013D44C
	private void LockYesBtnFun()
	{
		GameMoneyHelper.ShowItemProduct(this.CurItem.ItemData.FunLock, GameDefine.SHOP_TYPE.TOOL_SHOP);
	}

	// Token: 0x06004221 RID: 16929 RVA: 0x0013F264 File Offset: 0x0013D464
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

	// Token: 0x06004222 RID: 16930 RVA: 0x0013F2A4 File Offset: 0x0013D4A4
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

	// Token: 0x06004223 RID: 16931 RVA: 0x0013F30C File Offset: 0x0013D50C
	public void SetLabelWarning(UILabel curlabel, bool istrue)
	{
		if (istrue)
		{
			curlabel.color = Color.red;
		}
	}

	// Token: 0x06004224 RID: 16932 RVA: 0x0013F324 File Offset: 0x0013D524
	public bool CheckFunctionUnlock(FUNCTION_TYPE type)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		return playerCommonData.IsFunctionUnlock(type);
	}

	// Token: 0x06004225 RID: 16933 RVA: 0x0013F344 File Offset: 0x0013D544
	private void OnDisable()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.BADGE_CLICK_EQUIP)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004226 RID: 16934 RVA: 0x0013F358 File Offset: 0x0013D558
	public void OnClickJumpBtn1()
	{
		this.JumpPage(this.mJumpPath[0]);
	}

	// Token: 0x06004227 RID: 16935 RVA: 0x0013F368 File Offset: 0x0013D568
	public void OnClickJumpBtn2()
	{
		this.JumpPage(this.mJumpPath[1]);
	}

	// Token: 0x06004228 RID: 16936 RVA: 0x0013F378 File Offset: 0x0013D578
	public void OnClickJumpBtn3()
	{
		this.JumpPage(this.mJumpPath[2]);
	}

	// Token: 0x06004229 RID: 16937 RVA: 0x0013F388 File Offset: 0x0013D588
	public void OnClickJumpBtn4()
	{
		this.JumpPage(this.mJumpPath[3]);
	}

	// Token: 0x0600422A RID: 16938 RVA: 0x0013F398 File Offset: 0x0013D598
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

	// Token: 0x0600422B RID: 16939 RVA: 0x0013F570 File Offset: 0x0013D770
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

	// Token: 0x04002DF3 RID: 11763
	public const string equip_btn_str = "#{100614}";

	// Token: 0x04002DF4 RID: 11764
	public const string un_equip_btn_str = "#{100615}";

	// Token: 0x04002DF5 RID: 11765
	public const string use_str = "#{100623}";

	// Token: 0x04002DF6 RID: 11766
	public const string useall_str = "#{300407}";

	// Token: 0x04002DF7 RID: 11767
	public const string open_str = "#{300405}";

	// Token: 0x04002DF8 RID: 11768
	public const string openall_str = "#{300406}";

	// Token: 0x04002DF9 RID: 11769
	public const string recyle_str = "#{100621}";

	// Token: 0x04002DFA RID: 11770
	public const string trade_str = "#{100622}";

	// Token: 0x04002DFB RID: 11771
	public const string enhance_str = "#{100618}";

	// Token: 0x04002DFC RID: 11772
	public const string merge_str = "#{100616}";

	// Token: 0x04002DFD RID: 11773
	public const string split_str = "#{100617}";

	// Token: 0x04002DFE RID: 11774
	public const string extra_str = "#{100623}";

	// Token: 0x04002DFF RID: 11775
	public DelegateDefine.NoParamDelegate onClose;

	// Token: 0x04002E00 RID: 11776
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002E01 RID: 11777
	public float LeftPos;

	// Token: 0x04002E02 RID: 11778
	public float RightPos;

	// Token: 0x04002E03 RID: 11779
	public float CenterPos;

	// Token: 0x04002E04 RID: 11780
	public GameObject[] BtnList;

	// Token: 0x04002E05 RID: 11781
	public UILabel[] BtnLabelList;

	// Token: 0x04002E06 RID: 11782
	public UISprite[] BtnSpList;

	// Token: 0x04002E07 RID: 11783
	public UIGrid BtnGride;

	// Token: 0x04002E08 RID: 11784
	public ItemInfoSubRootLogic ItemInfoRoot;

	// Token: 0x04002E09 RID: 11785
	public GameObject ItemInfoObjRoot;

	// Token: 0x04002E0A RID: 11786
	public ItemInfoSubRootLogic EquipInfoRoot;

	// Token: 0x04002E0B RID: 11787
	public ItemInfoSubRootLogic ConsumeItemInfoRoot;

	// Token: 0x04002E0C RID: 11788
	private GameItem mCurItem;

	// Token: 0x04002E0D RID: 11789
	private DelegateDefine.OneGameItemParamDelegate[] mOnClickBtnList = new DelegateDefine.OneGameItemParamDelegate[4];

	// Token: 0x04002E0E RID: 11790
	private UI_PAGE_TYPE mPrePage = UI_PAGE_TYPE.INVALID;

	// Token: 0x04002E0F RID: 11791
	private JUMP_PATH[] mJumpPath = new JUMP_PATH[4];
}
