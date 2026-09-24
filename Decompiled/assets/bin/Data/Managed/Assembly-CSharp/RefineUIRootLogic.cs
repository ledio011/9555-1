using System;
using Sproto;
using SprotoType;
using UnityEngine;

// Token: 0x0200090F RID: 2319
public class RefineUIRootLogic : SingletonUnity<RefineUIRootLogic>
{
	// Token: 0x06003FB1 RID: 16305 RVA: 0x00129590 File Offset: 0x00127790
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06003FB2 RID: 16306 RVA: 0x0012959C File Offset: 0x0012779C
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x17000F92 RID: 3986
	// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x001295BC File Offset: 0x001277BC
	// (set) Token: 0x06003FB4 RID: 16308 RVA: 0x001295C4 File Offset: 0x001277C4
	public REFINE_PART CurPagePart
	{
		get
		{
			return this.mCurPagePart;
		}
		set
		{
			this.mCurPagePart = value;
		}
	}

	// Token: 0x06003FB5 RID: 16309 RVA: 0x001295D0 File Offset: 0x001277D0
	public void ResetEnable()
	{
		this.leftEffect.resetOnPlay = true;
		this.leftEffect.Play(true);
	}

	// Token: 0x06003FB6 RID: 16310 RVA: 0x001295EC File Offset: 0x001277EC
	private void OnEnable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
	}

	// Token: 0x06003FB7 RID: 16311 RVA: 0x0012961C File Offset: 0x0012781C
	private void OnDisable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
		this.isPlayEffect = false;
		if (TutorialManager.CurStep == TUTORIAL_STEP.STRENGTH_STAR_CLICK_BTN)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06003FB8 RID: 16312 RVA: 0x00129668 File Offset: 0x00127868
	public void UpdateMoney()
	{
		if (this.mPlayerData == null)
		{
			this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		if (this.curRefineData == null)
		{
			return;
		}
		int part = (int)this.mCurPagePart;
		int targetRefinePartLevel = this.mPlayerData.MainPlayerAttrData.GetTargetRefinePartLevel(this.mCurPagePart);
		RefineData refineData = null;
		if (targetRefinePartLevel < GameDefine.MAX_REFINE_LEVEL)
		{
			refineData = DataManager.GetRefineDataByPartLevelPRO(part, targetRefinePartLevel + 1, (int)this.mPlayerData.Profession);
		}
		if (refineData != null)
		{
			this.CostMoneyLabel.text = string.Format("{0}", this.curRefineData.MoneyCost);
			if ((long)this.curRefineData.MoneyCost <= GameMoneyHelper.GetMoneyNum(0))
			{
				this.CostMoneyLabel.color = Color.white;
			}
			else
			{
				this.CostMoneyLabel.color = Color.red;
			}
		}
	}

	// Token: 0x06003FB9 RID: 16313 RVA: 0x00129744 File Offset: 0x00127944
	public void Show()
	{
		this.ResetEnable();
		if (this.mPlayerData == null)
		{
			this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		if (this.mCurRefineData == null)
		{
			this.mCurRefineData = new RefineData[5];
		}
		for (int i = 1; i < 5; i++)
		{
			this.UpdateCurRefineData((REFINE_PART)i);
		}
		this.ResetPate();
	}

	// Token: 0x06003FBA RID: 16314 RVA: 0x001297A8 File Offset: 0x001279A8
	private void UpdateCurRefineData(REFINE_PART targetPart)
	{
		this.mCurRefineData[(int)targetPart] = DataManager.GetRefineDataByPartLevelPRO((int)targetPart, this.mPlayerData.MainPlayerAttrData.GetTargetRefinePartLevel(targetPart), (int)this.mPlayerData.Profession);
	}

	// Token: 0x06003FBB RID: 16315 RVA: 0x001297E4 File Offset: 0x001279E4
	public void OnClickTips()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
		{
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{100907}", "#{100906}", null, new object[0]);
		}, null);
	}

	// Token: 0x06003FBC RID: 16316 RVA: 0x00129814 File Offset: 0x00127A14
	public void Hide()
	{
	}

	// Token: 0x06003FBD RID: 16317 RVA: 0x00129818 File Offset: 0x00127A18
	public void ResetPate()
	{
		this.mUpgradeBtnUseFlag = false;
		this.UpdateLeftPage();
		this.UpdateRightPage(this.mCurPagePart);
	}

	// Token: 0x06003FBE RID: 16318 RVA: 0x00129834 File Offset: 0x00127A34
	public void UpdateLeftPage()
	{
		this.UpdateLeftStar();
		for (int i = 1; i < this.RefinePartIconList.Length; i++)
		{
			this.RefinePartIconList[i].spriteName = this.mCurRefineData[i].ICON;
		}
	}

	// Token: 0x06003FBF RID: 16319 RVA: 0x0012987C File Offset: 0x00127A7C
	private void UpdateLeftStar()
	{
		int num = 0;
		int targetRefinePartLevel = this.mPlayerData.MainPlayerAttrData.GetTargetRefinePartLevel(REFINE_PART.NECK);
		if (targetRefinePartLevel < 1)
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftStarObj[0], false);
		}
		else
		{
			this.curRefineData = this.mCurRefineData[1];
			num += this.curRefineData.GetCombatValue();
			UnityVersionUtil.SetActiveRecursive(this.LeftStarObj[0], true);
			this.LeftStarlabel[0].text = targetRefinePartLevel.ToString();
		}
		targetRefinePartLevel = this.mPlayerData.MainPlayerAttrData.GetTargetRefinePartLevel(REFINE_PART.RING1);
		if (targetRefinePartLevel < 1)
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftStarObj[1], false);
		}
		else
		{
			this.curRefineData = this.mCurRefineData[2];
			num += this.curRefineData.GetCombatValue();
			UnityVersionUtil.SetActiveRecursive(this.LeftStarObj[1], true);
			this.LeftStarlabel[1].text = targetRefinePartLevel.ToString();
		}
		targetRefinePartLevel = this.mPlayerData.MainPlayerAttrData.GetTargetRefinePartLevel(REFINE_PART.RING2);
		if (targetRefinePartLevel < 1)
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftStarObj[2], false);
		}
		else
		{
			this.curRefineData = this.mCurRefineData[3];
			num += this.curRefineData.GetCombatValue();
			UnityVersionUtil.SetActiveRecursive(this.LeftStarObj[2], true);
			this.LeftStarlabel[2].text = targetRefinePartLevel.ToString();
		}
		targetRefinePartLevel = this.mPlayerData.MainPlayerAttrData.GetTargetRefinePartLevel(REFINE_PART.BELT);
		if (targetRefinePartLevel < 1)
		{
			UnityVersionUtil.SetActiveRecursive(this.LeftStarObj[3], false);
		}
		else
		{
			this.curRefineData = this.mCurRefineData[4];
			num += this.curRefineData.GetCombatValue();
			UnityVersionUtil.SetActiveRecursive(this.LeftStarObj[3], true);
			this.LeftStarlabel[3].text = targetRefinePartLevel.ToString();
		}
		this.CombatValueLabel.text = num.ToString();
	}

	// Token: 0x06003FC0 RID: 16320 RVA: 0x00129A48 File Offset: 0x00127C48
	public void UpdateRightPage(REFINE_PART updatePart)
	{
		UnityVersionUtil.SetActiveRecursive(this.pageRightRoot, true);
		this.mCurPagePart = updatePart;
		int num = (int)this.mCurPagePart;
		int targetRefinePartLevel = this.mPlayerData.MainPlayerAttrData.GetTargetRefinePartLevel(this.mCurPagePart);
		this.CurPartNameLabel.text = StrDictionary.GetDictionaryString(this.RefinePartName[num], new object[0]);
		this.curRefineData = this.mCurRefineData[num];
		RefineData refineData = null;
		this.SelctIcon.transform.position = this.RefinePartIconList[num].transform.position;
		if (targetRefinePartLevel < GameDefine.MAX_REFINE_LEVEL)
		{
			refineData = DataManager.GetRefineDataByPartLevelPRO(num, targetRefinePartLevel + 1, (int)this.mPlayerData.Profession);
		}
		this.CurPartIcon.spriteName = this.curRefineData.ICON;
		this.CurParColorIcon.color = this.parColor[(int)updatePart];
		this.CurAttrTypeLabelList[0].text = GameDefine.GetAttributeName_S(this.curRefineData.Stat1);
		this.AttrIconPicList[0].spriteName = GameDefine.GetAttributeIcon(this.curRefineData.Stat1);
		this.CurAttrValLabelList[0].text = GameDefine.GetAttributeValueStr2(this.curRefineData.Stat1, this.curRefineData.Value1);
		if (refineData != null)
		{
			this.NextAttrValLabelList[0].text = GameDefine.GetAttributeValueStr2(refineData.Stat1, refineData.Value1);
			UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[0], true);
			UnityVersionUtil.SetActiveRecursive(this.NextAttrValLabelList[0].gameObject, true);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[0], false);
			UnityVersionUtil.SetActiveRecursive(this.NextAttrValLabelList[0].gameObject, false);
		}
		if (this.curRefineData.Stat2 == 0)
		{
			if (refineData == null || refineData.Stat2 == 0)
			{
				UnityVersionUtil.SetActiveRecursive(this.CurAttrLineList[1], false);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.CurAttrLineList[1], true);
				this.CurAttrTypeLabelList[1].text = GameDefine.GetAttributeName_S(this.curRefineData.Stat2);
				this.AttrIconPicList[1].spriteName = GameDefine.GetAttributeIcon(this.curRefineData.Stat2);
				this.CurAttrValLabelList[1].text = GameDefine.GetAttributeValueStr2(refineData.Stat2, 0);
				this.NextAttrValLabelList[1].text = GameDefine.GetAttributeValueStr2(refineData.Stat2, refineData.Value2);
				UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[1], true);
			}
		}
		else
		{
			this.CurAttrTypeLabelList[1].text = GameDefine.GetAttributeName_S(this.curRefineData.Stat2);
			this.AttrIconPicList[1].spriteName = GameDefine.GetAttributeIcon(this.curRefineData.Stat2);
			this.CurAttrValLabelList[1].text = GameDefine.GetAttributeValueStr2(this.curRefineData.Stat2, this.curRefineData.Value2);
			if (refineData != null)
			{
				this.NextAttrValLabelList[1].text = GameDefine.GetAttributeValueStr2(refineData.Stat2, refineData.Value2);
				UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[1], true);
				UnityVersionUtil.SetActiveRecursive(this.NextAttrValLabelList[1].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[1], false);
				UnityVersionUtil.SetActiveRecursive(this.NextAttrValLabelList[1].gameObject, false);
			}
		}
		if (this.curRefineData.Stat3 == 0)
		{
			if (refineData == null || refineData.Stat3 == 0)
			{
				UnityVersionUtil.SetActiveRecursive(this.CurAttrLineList[2], false);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.CurAttrLineList[2], true);
				this.CurAttrTypeLabelList[2].text = GameDefine.GetAttributeName_S(this.curRefineData.Stat3);
				this.AttrIconPicList[2].spriteName = GameDefine.GetAttributeIcon(this.curRefineData.Stat3);
				this.CurAttrValLabelList[2].text = GameDefine.GetAttributeValueStr2(refineData.Stat3, 0);
				this.NextAttrValLabelList[2].text = GameDefine.GetAttributeValueStr2(refineData.Stat3, refineData.Value3);
				UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[2], true);
			}
		}
		else
		{
			this.CurAttrTypeLabelList[2].text = GameDefine.GetAttributeName_S(this.curRefineData.Stat3);
			this.AttrIconPicList[2].spriteName = GameDefine.GetAttributeIcon(this.curRefineData.Stat3);
			this.CurAttrValLabelList[2].text = GameDefine.GetAttributeValueStr2(this.curRefineData.Stat3, this.curRefineData.Value3);
			if (refineData != null)
			{
				this.NextAttrValLabelList[2].text = GameDefine.GetAttributeValueStr2(refineData.Stat3, refineData.Value3);
				UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[2], true);
				UnityVersionUtil.SetActiveRecursive(this.NextAttrValLabelList[2].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[2], false);
				UnityVersionUtil.SetActiveRecursive(this.NextAttrValLabelList[2].gameObject, false);
			}
		}
		if (this.curRefineData.Stat4 == 0)
		{
			if (refineData == null || refineData.Stat4 == 0)
			{
				UnityVersionUtil.SetActiveRecursive(this.CurAttrLineList[3], false);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.CurAttrLineList[3], true);
				this.CurAttrTypeLabelList[3].text = GameDefine.GetAttributeName_S(this.curRefineData.Stat4);
				this.AttrIconPicList[3].spriteName = GameDefine.GetAttributeIcon(this.curRefineData.Stat4);
				this.CurAttrValLabelList[3].text = GameDefine.GetAttributeValueStr2(refineData.Stat4, 0);
				this.NextAttrValLabelList[3].text = GameDefine.GetAttributeValueStr2(refineData.Stat4, refineData.Value4);
				UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[3], true);
			}
		}
		else
		{
			this.CurAttrTypeLabelList[3].text = GameDefine.GetAttributeName_S(this.curRefineData.Stat4);
			this.AttrIconPicList[3].spriteName = GameDefine.GetAttributeIcon(this.curRefineData.Stat4);
			this.CurAttrValLabelList[3].text = GameDefine.GetAttributeValueStr2(this.curRefineData.Stat4, this.curRefineData.Value4);
			if (refineData != null)
			{
				this.NextAttrValLabelList[3].text = GameDefine.GetAttributeValueStr2(refineData.Stat4, refineData.Value4);
				UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[3], true);
				UnityVersionUtil.SetActiveRecursive(this.NextAttrValLabelList[3].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.NextLevelArrowPic[3], false);
				UnityVersionUtil.SetActiveRecursive(this.NextAttrValLabelList[3].gameObject, false);
			}
		}
		for (int i = 0; i < this.StarList.Length; i++)
		{
			if (i < targetRefinePartLevel)
			{
				UnityVersionUtil.SetActiveRecursive(this.StarList[i].gameObject, true);
				this.StarList[i].color = Color.white;
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.StarList[i].gameObject, false);
			}
		}
		if (this.isPlayEffect && targetRefinePartLevel < GameDefine.MAX_REFINE_LEVEL && targetRefinePartLevel >= 1)
		{
			this.starEffect[targetRefinePartLevel - 1].resetOnPlay = true;
			this.starEffect[targetRefinePartLevel - 1].Play(true);
			this.isPlayEffect = false;
		}
		if (refineData == null)
		{
			UnityVersionUtil.SetActiveRecursive(this.Cost1Icon.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.Cost1NeedNumLabel.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.Cost2Icon.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.Cost2NeedNumLabel.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.CostMoneyIcon.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.CostMoneyLabel.gameObject, false);
			this.costnamelabel.enabled = false;
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.Cost1Icon.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.Cost1NeedNumLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.Cost2Icon.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.Cost2NeedNumLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.CostMoneyIcon.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.CostMoneyLabel.gameObject, true);
			this.costnamelabel.enabled = true;
			this.RefershItemUI();
			this.CostMoneyLabel.text = string.Format("{0}", this.curRefineData.MoneyCost);
			if ((long)this.curRefineData.MoneyCost <= GameMoneyHelper.GetMoneyNum(0))
			{
				this.CostMoneyLabel.color = Color.white;
			}
			else
			{
				this.CostMoneyLabel.color = Color.red;
			}
		}
		if (targetRefinePartLevel < 3)
		{
			UnityVersionUtil.SetActiveRecursive(this.SaftyItemRoot, false);
			this.UseSaftyFlag = false;
		}
		else if (!string.IsNullOrEmpty(this.curRefineData.SaftyId) && this.curRefineData.Chance < 100)
		{
			UnityVersionUtil.SetActiveRecursive(this.SaftyItemRoot, true);
			ItemData itemDataByID = DataManager.GetItemDataByID(this.curRefineData.SaftyId);
			this.SaftyItemIcon.spriteName = itemDataByID.BackPackIcon;
			int itemStackNumById = this.mPlayerData.ItemBackPack.GetItemStackNumById(this.curRefineData.SaftyId);
			this.SaftyItemNeedNumLabel.text = string.Format("{0}/{1}", this.curRefineData.SaftyCost, itemStackNumById);
			if (itemStackNumById >= this.curRefineData.SaftyCost)
			{
				this.SaftyItemNeedNumLabel.color = Color.white;
				this.UseSaftyItem();
			}
			else
			{
				this.SaftyItemNeedNumLabel.color = Color.red;
				this.NotUseSaftyItem();
			}
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.SaftyItemRoot, false);
			this.UseSaftyFlag = false;
		}
	}

	// Token: 0x06003FC1 RID: 16321 RVA: 0x0012A3C0 File Offset: 0x001285C0
	public void RefershItemUI()
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(this.curRefineData.CostId1);
		if (itemDataByID == null)
		{
			return;
		}
		this.Cost1Icon.spriteName = itemDataByID.BackPackIcon;
		this.Cost1QualityIcon.spriteName = itemDataByID.QualityType.ToString();
		int itemStackNumById = this.mPlayerData.ItemBackPack.GetItemStackNumById(this.curRefineData.CostId1);
		this.Cost1NeedNumLabel.text = string.Format("{0}/{1}", this.curRefineData.Cost1, itemStackNumById);
		if (this.curRefineData.Cost1 > itemStackNumById)
		{
			this.Cost1NeedNumLabel.color = Color.red;
		}
		else
		{
			this.Cost1NeedNumLabel.color = Color.white;
		}
		if (string.IsNullOrEmpty(this.curRefineData.CostId2))
		{
			UnityVersionUtil.SetActiveRecursive(this.Cost2Icon.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.Cost2NeedNumLabel.gameObject, false);
		}
		else
		{
			ItemData itemDataByID2 = DataManager.GetItemDataByID(this.curRefineData.CostId2);
			this.Cost2Icon.spriteName = itemDataByID2.BackPackIcon;
			int itemStackNumById2 = this.mPlayerData.ItemBackPack.GetItemStackNumById(this.curRefineData.CostId2);
			this.Cost2NeedNumLabel.text = string.Format("{0}/{1}", this.curRefineData.Cost2, itemStackNumById2);
			this.Cost2QualityIcon.spriteName = itemDataByID2.QualityType.ToString();
			if (this.curRefineData.Cost2 > itemStackNumById2)
			{
				this.Cost2NeedNumLabel.color = Color.red;
			}
			else
			{
				this.Cost2NeedNumLabel.color = Color.white;
			}
		}
		if (!string.IsNullOrEmpty(this.curRefineData.SaftyId) && this.curRefineData.Chance < 100)
		{
			ItemData itemDataByID3 = DataManager.GetItemDataByID(this.curRefineData.SaftyId);
			this.SaftyItemIcon.spriteName = itemDataByID3.BackPackIcon;
			int itemStackNumById3 = this.mPlayerData.ItemBackPack.GetItemStackNumById(this.curRefineData.SaftyId);
			this.SaftyItemNeedNumLabel.text = string.Format("{0}/{1}", this.curRefineData.SaftyCost, itemStackNumById3);
			if (itemStackNumById3 >= this.curRefineData.SaftyCost)
			{
				this.SaftyItemNeedNumLabel.color = Color.white;
				this.UseSaftyItem();
			}
			else
			{
				this.SaftyItemNeedNumLabel.color = Color.red;
				this.NotUseSaftyItem();
			}
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.SaftyItemRoot, false);
			this.UseSaftyFlag = false;
		}
	}

	// Token: 0x06003FC2 RID: 16322 RVA: 0x0012A674 File Offset: 0x00128874
	private void UseSaftyItem()
	{
		this.UseSaftyFlag = true;
		this.SaftyUsePic.enabled = true;
	}

	// Token: 0x06003FC3 RID: 16323 RVA: 0x0012A68C File Offset: 0x0012888C
	private void NotUseSaftyItem()
	{
		this.UseSaftyFlag = false;
		this.SaftyUsePic.enabled = false;
	}

	// Token: 0x06003FC4 RID: 16324 RVA: 0x0012A6A4 File Offset: 0x001288A4
	public void OnClickUseSaftyItemBtn()
	{
		if (this.UseSaftyFlag)
		{
			this.NotUseSaftyItem();
		}
		else
		{
			RefineData refineData = this.mCurRefineData[(int)this.mCurPagePart];
			int itemStackNumById = this.mPlayerData.ItemBackPack.GetItemStackNumById(refineData.SaftyId);
			if (!string.IsNullOrEmpty(refineData.SaftyId) && refineData.SaftyCost <= itemStackNumById)
			{
				this.UseSaftyItem();
			}
			else
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(refineData.SaftyId);
				GameMoneyHelper.ShowItemProduct(refineData.SaftyId, GameDefine.SHOP_TYPE.TOOL_SHOP);
			}
		}
	}

	// Token: 0x06003FC5 RID: 16325 RVA: 0x0012A72C File Offset: 0x0012892C
	public void OnClickRefineCost1()
	{
		RefineData refineData = this.mCurRefineData[(int)this.mCurPagePart];
		if (refineData != null)
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(refineData.CostId1);
			if (itemDataByID != null)
			{
				ItemInfoRootLogicNew.ShowItemTips(itemDataByID, true, UI_PAGE_TYPE.REFINE);
			}
		}
	}

	// Token: 0x06003FC6 RID: 16326 RVA: 0x0012A768 File Offset: 0x00128968
	public void OnClickRefineCost2()
	{
		RefineData refineData = this.mCurRefineData[(int)this.mCurPagePart];
		if (refineData != null && !string.IsNullOrEmpty(refineData.CostId2))
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(refineData.CostId2);
			if (itemDataByID != null)
			{
				ItemInfoRootLogicNew.ShowItemTips(itemDataByID, true, UI_PAGE_TYPE.REFINE);
			}
		}
	}

	// Token: 0x06003FC7 RID: 16327 RVA: 0x0012A7B4 File Offset: 0x001289B4
	public void OnClickRefineMoeny()
	{
		ItemData moneyItemData = GameMoneyHelper.GetMoneyItemData(GameDefine.MONEY_TYPE.CASH);
		if (moneyItemData != null)
		{
			ItemInfoRootLogicNew.ShowItemTips(moneyItemData, true, UI_PAGE_TYPE.REFINE);
		}
	}

	// Token: 0x06003FC8 RID: 16328 RVA: 0x0012A7D8 File Offset: 0x001289D8
	public void OnClickSafeInfo()
	{
		RefineData refineData = this.mCurRefineData[(int)this.mCurPagePart];
		if (refineData != null)
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(refineData.SaftyId);
			if (itemDataByID != null)
			{
				ItemInfoRootLogicNew.ShowItemTips(itemDataByID, true, UI_PAGE_TYPE.REFINE);
			}
		}
	}

	// Token: 0x06003FC9 RID: 16329 RVA: 0x0012A814 File Offset: 0x00128A14
	public void OnClickRefineUpgradeBtn()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.STRENGTH_STAR_CLICK_BTN)
		{
			this.CheckTutorialEvent();
		}
		if (this.mUpgradeBtnUseFlag)
		{
			return;
		}
		RefineData refineData = this.mCurRefineData[(int)this.mCurPagePart];
		RefineData refineDataByPartLevelPRO = DataManager.GetRefineDataByPartLevelPRO(refineData.Part, refineData.Lv + 1, (int)this.mPlayerData.Profession);
		if (refineData.Lv < GameDefine.MAX_REFINE_LEVEL)
		{
			if (!GameMoneyHelper.BeforeCheckBuy(refineData.MoneyType, refineData.MoneyCost))
			{
				return;
			}
			int itemStackNumById = this.mPlayerData.ItemBackPack.GetItemStackNumById(refineData.CostId1);
			if (itemStackNumById < refineData.Cost1)
			{
				GameMoneyHelper.ShowItemProduct(refineData.CostId1, GameDefine.SHOP_TYPE.TOOL_SHOP);
				return;
			}
			if (!string.IsNullOrEmpty(refineData.CostId2))
			{
				int itemStackNumById2 = this.mPlayerData.ItemBackPack.GetItemStackNumById(refineData.CostId2);
				if (itemStackNumById2 < refineData.Cost2)
				{
					GameMoneyHelper.ShowItemProduct(refineData.CostId2, GameDefine.SHOP_TYPE.TOOL_SHOP);
					return;
				}
			}
			RefineData refineData2 = null;
			if (refineData.Lv > 0)
			{
				refineData2 = DataManager.GetRefineDataByPartLevelPRO(refineData.Part, refineData.Lv - 1, (int)this.mPlayerData.Profession);
			}
			equip_refine.request request = new equip_refine.request();
			request.Id = refineDataByPartLevelPRO.ID;
			request.curId = refineData.ID;
			request.partId = (long)refineDataByPartLevelPRO.Part;
			request.level = (long)refineDataByPartLevelPRO.Lv;
			request.safe = this.UseSaftyFlag;
			if (refineData2 != null)
			{
				request.preId = refineData2.ID;
			}
			NetLogic.GetInstance().Send<Protocol.equip_refine>(request, new RpcRspHandler(this.OnRefineUpgradeResponse));
			this.mUpgradeBtnUseFlag = true;
		}
		else
		{
			Debug.Log("Max Level");
		}
	}

	// Token: 0x06003FCA RID: 16330 RVA: 0x0012A9C4 File Offset: 0x00128BC4
	public void PlayEffect()
	{
		SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(18, 1f, null);
		this.upgradeEffect.resetOnPlay = true;
		this.upgradeEffect.Play(true);
		for (int i = 0; i < this.LineEffect.Length; i++)
		{
			this.LineEffect[i].resetOnPlay = true;
			this.LineEffect[i].Play(true);
		}
		this.isPlayEffect = true;
	}

	// Token: 0x06003FCB RID: 16331 RVA: 0x0012AA38 File Offset: 0x00128C38
	private void OnRefineUpgradeResponse(SprotoTypeBase rpcRsp)
	{
		this.mUpgradeBtnUseFlag = false;
		equip_refine.response response = rpcRsp as equip_refine.response;
		if (response != null)
		{
			if (response.state == 0L)
			{
				NoticeLogic.AddNotifyData("#{100919}", true, false);
				if (this != null && UnityVersionUtil.IsActive(base.gameObject))
				{
					this.PlayEffect();
				}
			}
			else if (response.state == 2L)
			{
				NoticeLogic.AddNotifyData("#{100920}", true, false);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100920}", true, false);
			}
			this.mPlayerData.MainPlayerAttrData.SetTargetRefinePartLevel((REFINE_PART)response.partId, (int)response.level);
			if (response.HasAllstar)
			{
				this.mPlayerData.MainPlayerAttrData.RefineLevel = (int)response.allstar;
			}
			if (this != null && UnityVersionUtil.IsActive(base.gameObject))
			{
				this.UpdateCurRefineData((REFINE_PART)response.partId);
				this.UpdateLeftPage();
				if ((long)this.mCurPagePart == response.partId)
				{
					this.UpdateRightPage(this.mCurPagePart);
				}
			}
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.UpdateEnhanceTips();
			if (response.state == 0L)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Refine", string.Format("part_{0}", response.partId), "success");
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Refine", string.Format("part_{0}", response.partId), string.Format("level_{0}", response.level));
			}
			else
			{
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Refine", string.Format("part_{0}", response.partId), "failure");
			}
		}
	}

	// Token: 0x06003FCC RID: 16332 RVA: 0x0012ABF8 File Offset: 0x00128DF8
	public void OnClickRefineLevelInfoBtn()
	{
	}

	// Token: 0x06003FCD RID: 16333 RVA: 0x0012ABFC File Offset: 0x00128DFC
	public void OnClickRefineNackBtn()
	{
		this.UpdateRightPage(REFINE_PART.NECK);
	}

	// Token: 0x06003FCE RID: 16334 RVA: 0x0012AC08 File Offset: 0x00128E08
	public void OnClickRefineRing1Btn()
	{
		this.UpdateRightPage(REFINE_PART.RING1);
	}

	// Token: 0x06003FCF RID: 16335 RVA: 0x0012AC14 File Offset: 0x00128E14
	public void OnClickRefineRing2Btn()
	{
		this.UpdateRightPage(REFINE_PART.RING2);
	}

	// Token: 0x06003FD0 RID: 16336 RVA: 0x0012AC20 File Offset: 0x00128E20
	public void OnClickRefineBeltBtn()
	{
		this.UpdateRightPage(REFINE_PART.BELT);
	}

	// Token: 0x06003FD1 RID: 16337 RVA: 0x0012AC2C File Offset: 0x00128E2C
	public void OnClickTipBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate(bool bSuccess, object param)
		{
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{100906}", "#{100907}", null, new object[0]);
		}, null);
	}

	// Token: 0x04002B8C RID: 11148
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04002B8D RID: 11149
	public UISprite[] RefinePartIconList;

	// Token: 0x04002B8E RID: 11150
	public UISprite SelctIcon;

	// Token: 0x04002B8F RID: 11151
	public UISprite CurPartIcon;

	// Token: 0x04002B90 RID: 11152
	public UISprite CurParColorIcon;

	// Token: 0x04002B91 RID: 11153
	private Color[] parColor = new Color[]
	{
		Color.black,
		new Color(1f, 0.78431374f, 0.29803923f, 1f),
		new Color(0.81960785f, 0.23921569f, 1f, 1f),
		new Color(1f, 0.3137255f, 0.3137255f, 1f),
		new Color(0.3137255f, 1f, 0.8f, 1f)
	};

	// Token: 0x04002B92 RID: 11154
	public UILabel CurPartNameLabel;

	// Token: 0x04002B93 RID: 11155
	public GameObject[] CurAttrLineList;

	// Token: 0x04002B94 RID: 11156
	public UILabel[] CurAttrTypeLabelList;

	// Token: 0x04002B95 RID: 11157
	public UILabel[] CurAttrValLabelList;

	// Token: 0x04002B96 RID: 11158
	public UISprite[] AttrIconPicList;

	// Token: 0x04002B97 RID: 11159
	public UILabel[] NextAttrValLabelList;

	// Token: 0x04002B98 RID: 11160
	public UISprite[] StarList;

	// Token: 0x04002B99 RID: 11161
	public GameObject[] NextLevelArrowPic;

	// Token: 0x04002B9A RID: 11162
	public UILabel costnamelabel;

	// Token: 0x04002B9B RID: 11163
	public UISprite Cost1Icon;

	// Token: 0x04002B9C RID: 11164
	public UISprite Cost1QualityIcon;

	// Token: 0x04002B9D RID: 11165
	public UISprite Cost2Icon;

	// Token: 0x04002B9E RID: 11166
	public UISprite Cost2QualityIcon;

	// Token: 0x04002B9F RID: 11167
	public UILabel Cost1NeedNumLabel;

	// Token: 0x04002BA0 RID: 11168
	public UILabel Cost2NeedNumLabel;

	// Token: 0x04002BA1 RID: 11169
	public UISprite CostMoneyIcon;

	// Token: 0x04002BA2 RID: 11170
	public UILabel CostMoneyLabel;

	// Token: 0x04002BA3 RID: 11171
	public UISprite SaftyItemIcon;

	// Token: 0x04002BA4 RID: 11172
	public UILabel SaftyItemNeedNumLabel;

	// Token: 0x04002BA5 RID: 11173
	public UISprite SaftyUsePic;

	// Token: 0x04002BA6 RID: 11174
	public GameObject SaftyItemRoot;

	// Token: 0x04002BA7 RID: 11175
	public GameObject pageRightRoot;

	// Token: 0x04002BA8 RID: 11176
	public bool UseSaftyFlag;

	// Token: 0x04002BA9 RID: 11177
	public UILabel CombatValueLabel;

	// Token: 0x04002BAA RID: 11178
	public GameObject[] LeftStarObj;

	// Token: 0x04002BAB RID: 11179
	public UILabel[] LeftStarlabel;

	// Token: 0x04002BAC RID: 11180
	public UIPlayTween upgradeEffect;

	// Token: 0x04002BAD RID: 11181
	public UIPlayTween[] LineEffect;

	// Token: 0x04002BAE RID: 11182
	public UIPlayTween[] starEffect;

	// Token: 0x04002BAF RID: 11183
	public UIPlayTween leftEffect;

	// Token: 0x04002BB0 RID: 11184
	public GameObject UpgradeBtn;

	// Token: 0x04002BB1 RID: 11185
	private RefineData[] mCurRefineData;

	// Token: 0x04002BB2 RID: 11186
	private RefineData curRefineData;

	// Token: 0x04002BB3 RID: 11187
	private string[] RefinePartName = new string[]
	{
		string.Empty,
		"#{100908}",
		"#{100909}",
		"#{100910}",
		"#{100911}"
	};

	// Token: 0x04002BB4 RID: 11188
	private PlayerData mPlayerData;

	// Token: 0x04002BB5 RID: 11189
	private REFINE_PART mCurPagePart = REFINE_PART.NECK;

	// Token: 0x04002BB6 RID: 11190
	private bool mUpgradeBtnUseFlag;

	// Token: 0x04002BB7 RID: 11191
	private bool isPlayEffect;
}
