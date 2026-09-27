using System;
using System.Collections.Generic;

// Token: 0x0200090A RID: 2314
public class EquipStrengthenUIRootLogic : SingletonUnity<EquipStrengthenUIRootLogic>
{
	// Token: 0x17000F91 RID: 3985
	// (get) Token: 0x06003F7B RID: 16251 RVA: 0x00127D9C File Offset: 0x00125F9C
	public EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE CurPageType
	{
		get
		{
			return this.mcurPageType;
		}
	}

	// Token: 0x06003F7C RID: 16252 RVA: 0x00127DA4 File Offset: 0x00125FA4
	public void ClearBackAction()
	{
		this.mCurOpenType = EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG;
	}

	// Token: 0x06003F7D RID: 16253 RVA: 0x00127DB0 File Offset: 0x00125FB0
	private GameItem GetPlayerWeapon()
	{
		ItemContainer equipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack;
		return equipPack.getWeapon();
	}

	// Token: 0x06003F7E RID: 16254 RVA: 0x00127DD4 File Offset: 0x00125FD4
	private GameItem GetFirstEquip()
	{
		ItemContainer equipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack;
		return equipPack.GetFirstNoEmptyItem();
	}

	// Token: 0x06003F7F RID: 16255 RVA: 0x00127DF8 File Offset: 0x00125FF8
	private List<GameItem> GetEquipListInEquipPack()
	{
		ItemContainer equipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack;
		return equipPack.ItemList;
	}

	// Token: 0x06003F80 RID: 16256 RVA: 0x00127E1C File Offset: 0x0012601C
	public void InitEquipStrengtheUI()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickSkillBtn), true, "CZ_left_Skill", StrDictionary.GetDictionaryString("#{100118}", new object[0]), FUNCTION_TYPE.SKILL, new DelegateDefine.NoParamReturnDelegate(this.CheckSkillUpdateTips));
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickEquipEnhanceBtn), true, "CZ_left_Enhance", StrDictionary.GetDictionaryString("#{100618}", new object[0]), FUNCTION_TYPE.ENHANCE_EQUIP, new DelegateDefine.NoParamReturnDelegate(playerData.IsHaveEnhanceTips));
			MenuTabBtnInfo menuTabBtnInfo3 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickEquipRefineBtn), true, "CZ_left_Upgrade", StrDictionary.GetDictionaryString("#{100619}", new object[0]), FUNCTION_TYPE.ENHANCE_STAR, new DelegateDefine.NoParamReturnDelegate(playerData.IsHaveRefineTips));
			MenuTabBtnInfo menuTabBtnInfo4 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickBadgeMergeBtn), true, "CZ_left_BadgeUp", StrDictionary.GetDictionaryString("#{100616}", new object[0]), FUNCTION_TYPE.ENHANCE_BADGE, null);
			MenuTabBtnInfo menuTabBtnInfo5 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickPlayerTitleBtn), true, "CZ_left_Character", StrDictionary.GetDictionaryString("#{101701}", new object[0]), FUNCTION_TYPE.TITLE_TITLE, new DelegateDefine.NoParamReturnDelegate(this.CheckTitleTips));
			list.Add(menuTabBtnInfo);
			list.Add(menuTabBtnInfo2);
			list.Add(menuTabBtnInfo3);
			list.Add(menuTabBtnInfo4);
			list.Add(menuTabBtnInfo5);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
			this.mcurPageType = EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.NOTHING;
		}, null);
	}

	// Token: 0x06003F81 RID: 16257 RVA: 0x00127E3C File Offset: 0x0012603C
	public void Reset()
	{
		if (!SingletonUnity<MenuBaseRootLogic>.Instance.AutoClickTipsTap())
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ENHANCE_EQUIP))
			{
				this.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
			}
			else
			{
				this.OnClickSkillBtn();
			}
		}
	}

	// Token: 0x06003F82 RID: 16258 RVA: 0x00127E84 File Offset: 0x00126084
	public void OnClickSkillBtn()
	{
		this.ShowSkillInfo(EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
	}

	// Token: 0x06003F83 RID: 16259 RVA: 0x00127E90 File Offset: 0x00126090
	public void ShowSkillInfo(EquipStrengthenUIRootLogic.OPENTYPE type = EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG)
	{
		if (this.mcurPageType == EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.SKILL)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RefineUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BadgeMergeRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnhanceUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuShengWangRootUI);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GameMenuSkillInfoRootUI, delegate
		{
			SingletonUnity<SkillInfoRootLogic>.Instance.Reset();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
		this.mcurPageType = EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.SKILL;
		this.mCurOpenType = type;
	}

	// Token: 0x06003F84 RID: 16260 RVA: 0x00127F2C File Offset: 0x0012612C
	public void ShowEquipEnhance(GameItem item = null, EquipStrengthenUIRootLogic.OPENTYPE type = EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG)
	{
		if (item == null)
		{
			item = this.GetPlayerWeapon();
		}
		if (item == null)
		{
			item = this.GetFirstEquip();
		}
		if (item != null)
		{
			if (!SingletonUnity<EnhanceUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<EnhanceUIRootLogic>.Instance.gameObject))
			{
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RefineUIRootLogic);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BadgeMergeRoot);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuSkillInfoRootUI);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuShengWangRootUI);
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.EnhanceUIRootLogic, null, null);
			}
			SingletonUnity<EnhanceUIRootLogic>.Instance.Show(item);
			SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(1);
		}
		this.mCurOpenType = type;
	}

	// Token: 0x06003F85 RID: 16261 RVA: 0x00127FE4 File Offset: 0x001261E4
	public void OnClickEquipEnhanceBtn()
	{
		if (this.mcurPageType == EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.ENHANCE)
		{
			return;
		}
		this.ShowEquipEnhance(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
		this.mcurPageType = EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.ENHANCE;
	}

	// Token: 0x06003F86 RID: 16262 RVA: 0x00128004 File Offset: 0x00126204
	public void ShowEquipRefine()
	{
		if (!SingletonUnity<RefineUIRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<RefineUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnhanceUIRootLogic);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BadgeMergeRoot);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuSkillInfoRootUI);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuShengWangRootUI);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.RefineUIRootLogic, null, null);
		}
		SingletonUnity<RefineUIRootLogic>.Instance.Show();
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(2);
	}

	// Token: 0x06003F87 RID: 16263 RVA: 0x00128094 File Offset: 0x00126294
	public void OnClickEquipRefineBtn()
	{
		if (this.mcurPageType == EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.REFINE)
		{
			return;
		}
		this.ShowEquipRefine();
		this.mcurPageType = EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.REFINE;
	}

	// Token: 0x06003F88 RID: 16264 RVA: 0x001280B0 File Offset: 0x001262B0
	public void ShowBadgeMerge(GameItem item = null, EquipStrengthenUIRootLogic.OPENTYPE type = EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG)
	{
		if (!SingletonUnity<BadgeMergeRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<BadgeMergeRootLogic>.Instance.gameObject))
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RefineUIRootLogic);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnhanceUIRootLogic);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuSkillInfoRootUI);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuShengWangRootUI);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BadgeMergeRoot, delegate
			{
				SingletonUnity<BadgeMergeRootLogic>.Instance.Show(item);
			}, null);
		}
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(3);
		this.mCurOpenType = type;
	}

	// Token: 0x06003F89 RID: 16265 RVA: 0x00128154 File Offset: 0x00126354
	public void OnClickBadgeMergeBtn()
	{
		if (this.mcurPageType == EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.BADGE)
		{
			return;
		}
		this.ShowBadgeMerge(null, EquipStrengthenUIRootLogic.OPENTYPE.NOTHINTG);
		this.mcurPageType = EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.BADGE;
	}

	// Token: 0x06003F8A RID: 16266 RVA: 0x00128174 File Offset: 0x00126374
	public void OnClickPlayerTitleBtn()
	{
		if (this.mcurPageType == EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.TITLE)
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RefineUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BadgeMergeRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnhanceUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuSkillInfoRootUI);
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.GameMenuShengWangRootUI, delegate
		{
			SingletonUnity<JSShengWangLogic>.Instance.Reset();
		}, null);
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(4);
		this.mcurPageType = EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.TITLE;
	}

	// Token: 0x06003F8B RID: 16267 RVA: 0x0012820C File Offset: 0x0012640C
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RefineUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EnhanceUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.EquipStrengthenUIRootLogic);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuSkillInfoRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BadgeMergeRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuShengWangRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		if (this.mCurOpenType == EquipStrengthenUIRootLogic.OPENTYPE.BADGE)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
			{
				SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickBadgeBtn();
			}, null);
		}
		else if (this.mCurOpenType == EquipStrengthenUIRootLogic.OPENTYPE.EQUIP)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
			{
				SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickEquipBackPackBtn();
			}, null);
		}
	}

	// Token: 0x06003F8C RID: 16268 RVA: 0x001282FC File Offset: 0x001264FC
	private void OnEnable()
	{
		this.InitEquipStrengtheUI();
	}

	// Token: 0x06003F8D RID: 16269 RVA: 0x00128304 File Offset: 0x00126504
	public bool CheckSkillUpdateTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SKILL))
		{
			return false;
		}
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		for (int i = 0; i < mainPlayer.CharacterSkillData.Count; i++)
		{
			CharacterSkillData characterSkillData = mainPlayer.CharacterSkillData[i];
			if (characterSkillData != null && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(characterSkillData.UnlockLevel))
			{
				int index = characterSkillData.Index;
				if (index >= 4 && index <= 6)
				{
					SkillData skillDataById = DataManager.GetSkillDataById(characterSkillData.ID);
					SkillupgradeData skillupgradeDataByLevel = DataManager.GetSkillupgradeDataByLevel(characterSkillData.Level + 1);
					if (skillDataById != null && skillDataById.IsUpgrade != 0 && skillupgradeDataByLevel != null)
					{
						if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level > characterSkillData.Level + 1 && GameMoneyHelper.GetMoneyNum(skillupgradeDataByLevel.PriceType) >= (long)skillupgradeDataByLevel.PriceValue)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06003F8E RID: 16270 RVA: 0x00128410 File Offset: 0x00126610
	public bool CheckTitleTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.TITLE_TITLE))
		{
			return false;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		int curTitleExp = playerData.MainPlayerAttrData.CurTitleExp;
		int curTitleLevel = playerData.MainPlayerAttrData.CurTitleLevel;
		if (curTitleLevel < 10 && curTitleLevel >= 0)
		{
			TitleData titleDateById = DataManager.GetTitleDateById(curTitleLevel.ToString());
			return curTitleExp >= titleDateById.EXP;
		}
		return false;
	}

	// Token: 0x04002B55 RID: 11093
	private EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE mcurPageType = EquipStrengthenUIRootLogic.EQUIP_STRENGTHEN_PAGE.NOTHING;

	// Token: 0x04002B56 RID: 11094
	private EquipStrengthenUIRootLogic.OPENTYPE mCurOpenType;

	// Token: 0x0200090B RID: 2315
	public enum EQUIP_STRENGTHEN_PAGE
	{
		// Token: 0x04002B5C RID: 11100
		NOTHING = -1,
		// Token: 0x04002B5D RID: 11101
		ENHANCE,
		// Token: 0x04002B5E RID: 11102
		REFINE,
		// Token: 0x04002B5F RID: 11103
		BADGE,
		// Token: 0x04002B60 RID: 11104
		INHERT,
		// Token: 0x04002B61 RID: 11105
		SKILL,
		// Token: 0x04002B62 RID: 11106
		TITLE
	}

	// Token: 0x0200090C RID: 2316
	public enum OPENTYPE
	{
		// Token: 0x04002B64 RID: 11108
		NOTHINTG,
		// Token: 0x04002B65 RID: 11109
		EQUIP,
		// Token: 0x04002B66 RID: 11110
		BADGE
	}
}
