using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200096F RID: 2415
public class PlayerInfoMenuRootLogic : SingletonUnity<PlayerInfoMenuRootLogic>
{
	// Token: 0x06004418 RID: 17432 RVA: 0x0015201C File Offset: 0x0015021C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x17000FAA RID: 4010
	// (get) Token: 0x06004419 RID: 17433 RVA: 0x00152028 File Offset: 0x00150228
	public GAME_MENU_TAP_TYPE CurTapType
	{
		get
		{
			return this.mCurTapType;
		}
	}

	// Token: 0x17000FAB RID: 4011
	// (get) Token: 0x0600441A RID: 17434 RVA: 0x00152030 File Offset: 0x00150230
	// (set) Token: 0x0600441B RID: 17435 RVA: 0x00152038 File Offset: 0x00150238
	public FakeObjLogic PlayerModelVisual
	{
		get
		{
			return this.mPlayerModelVisual;
		}
		set
		{
			this.mPlayerModelVisual = value;
		}
	}

	// Token: 0x0600441C RID: 17436 RVA: 0x00152044 File Offset: 0x00150244
	private new void Awake()
	{
		base.Awake();
		this.ResetFakeObjRoot();
	}

	// Token: 0x0600441D RID: 17437 RVA: 0x00152054 File Offset: 0x00150254
	private void ResetFakeObjRoot()
	{
		FakeObjRootLogic instance = SingletonUnity<FakeObjRootLogic>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeObjRoot");
			instance = SingletonUnity<FakeObjRootLogic>.Instance;
		}
	}

	// Token: 0x0600441E RID: 17438 RVA: 0x00152084 File Offset: 0x00150284
	private void OnDisable()
	{
		this.UnLoadFakeObj();
	}

	// Token: 0x0600441F RID: 17439 RVA: 0x0015208C File Offset: 0x0015028C
	public void ResetModelVisual()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer fashionEquipPack = playerData.FashionEquipPack;
		ItemContainer equipPack = playerData.EquipPack;
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		if (playerData.IsShowFashion)
		{
			text = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.HEAD, playerData.Profession, true);
			if (string.IsNullOrEmpty(text))
			{
				text = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.HEAD, playerData.Profession, false);
			}
			if (this.CheckWeaponIsSame())
			{
				text2 = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, playerData.Profession, true);
				if (string.IsNullOrEmpty(text2))
				{
					text2 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, playerData.Profession, false);
				}
			}
			else
			{
				text2 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, playerData.Profession, false);
			}
			text3 = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.BODY, playerData.Profession, true);
			if (string.IsNullOrEmpty(text3))
			{
				text3 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.BODY, playerData.Profession, false);
			}
			text4 = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.LEG, playerData.Profession, true);
			if (string.IsNullOrEmpty(text4))
			{
				text4 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.LEG, playerData.Profession, false);
			}
		}
		else
		{
			text = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.HEAD, playerData.Profession, false);
			text2 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, playerData.Profession, false);
			text3 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.BODY, playerData.Profession, false);
			text4 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.LEG, playerData.Profession, false);
		}
		if (this.mPlayerModelVisual == null)
		{
			this.mPlayerModelVisual = new FakeObjLogic();
			this.mPlayerModelVisual.InitFakeObject(text2, text, text3, text4, playerData.Profession, SingletonUnity<FakeObjRootLogic>.Instance.MeshRoot, null, "FakeObj");
		}
		else
		{
			this.mPlayerModelVisual.CheckFakeObject(text2, text, text3, text4, null);
			this.mPlayerModelVisual.PlayAnim("idle", playerData.CharacterModelData.ModelFirstType);
		}
	}

	// Token: 0x06004420 RID: 17440 RVA: 0x0015225C File Offset: 0x0015045C
	public bool CheckWeaponIsSame()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer fashionEquipPack = playerData.FashionEquipPack;
		ItemContainer equipPack = playerData.EquipPack;
		EquipData equipWeaponData = equipPack.GetEquipWeaponData(EQUIP_BACKPACK_TYPE.WEAPON);
		EquipData equipWeaponData2 = fashionEquipPack.GetEquipWeaponData(EQUIP_BACKPACK_TYPE.WEAPON);
		return equipWeaponData == null || equipWeaponData2 == null || equipWeaponData.WeaponType == equipWeaponData2.WeaponType;
	}

	// Token: 0x06004421 RID: 17441 RVA: 0x001522B4 File Offset: 0x001504B4
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuBackPackRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuPlayerInfoRootUI);
		if (SingletonUnity<PlayerModelPageRootLogic>.Exists)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuPlayerModelPageRoot);
		}
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		this.UnLoadFakeObj();
	}

	// Token: 0x06004422 RID: 17442 RVA: 0x0015231C File Offset: 0x0015051C
	public void UnLoadFakeObj()
	{
		if (this.mPlayerModelVisual != null)
		{
			this.mPlayerModelVisual.DestroyFakeObj();
			this.mPlayerModelVisual = null;
		}
		if (SingletonUnity<FakeObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeObjRootLogic>.Instance.DisableFakeObjRoot();
		}
	}

	// Token: 0x06004423 RID: 17443 RVA: 0x00152368 File Offset: 0x00150568
	public void Reset()
	{
		UnityVersionUtil.SetActiveRecursive(this.AutoEquipBtn, false);
		if (!SingletonUnity<MenuBaseRootLogic>.Instance.AutoClickTipsTap())
		{
			this.OnClickPlayerInfoBtn();
		}
	}

	// Token: 0x06004424 RID: 17444 RVA: 0x0015238C File Offset: 0x0015058C
	public void OnClickPlayerInfoBtn()
	{
		if (this.mCurTapType != GAME_MENU_TAP_TYPE.PLAYERINFO_TAP)
		{
			this.mCurTapType = GAME_MENU_TAP_TYPE.PLAYERINFO_TAP;
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuBackPackRootUI);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuPlayerModelPageRoot, new UIManager.OnOpenUIDelegate(this.OnOpenPlayerModelPageRoot), null);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuPlayerInfoRootUI, new UIManager.OnOpenUIDelegate(this.OnOpenPlayerInfoRoot), null);
		}
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(0);
		UnityVersionUtil.SetActiveRecursive(this.AutoEquipBtn, false);
	}

	// Token: 0x06004425 RID: 17445 RVA: 0x0015240C File Offset: 0x0015060C
	public void OnClickItemBackPackBtn()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
		if (this.mCurTapType != GAME_MENU_TAP_TYPE.ITEM_BACKPACK_TAP)
		{
			this.mCurTapType = GAME_MENU_TAP_TYPE.ITEM_BACKPACK_TAP;
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuPlayerInfoRootUI);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuPlayerModelPageRoot, new UIManager.OnOpenUIDelegate(this.OnOpenPlayerModelPageRoot), null);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuBackPackRootUI, new UIManager.OnOpenUIDelegate(this.OnOpenBackPackRoot), null);
		}
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(4);
		UnityVersionUtil.SetActiveRecursive(this.AutoEquipBtn, false);
	}

	// Token: 0x06004426 RID: 17446 RVA: 0x001524A8 File Offset: 0x001506A8
	public void OnClickEquipBackPackBtn()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
		if (this.mCurTapType != GAME_MENU_TAP_TYPE.EQUIP_BACKPACK_TAB)
		{
			this.mCurTapType = GAME_MENU_TAP_TYPE.EQUIP_BACKPACK_TAB;
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuPlayerInfoRootUI);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuPlayerModelPageRoot, new UIManager.OnOpenUIDelegate(this.OnOpenPlayerModelPageRoot), null);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuBackPackRootUI, new UIManager.OnOpenUIDelegate(this.OnOpenBackPackRoot), null);
		}
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(1);
		UnityVersionUtil.SetActiveRecursive(this.AutoEquipBtn, false);
	}

	// Token: 0x06004427 RID: 17447 RVA: 0x00152544 File Offset: 0x00150744
	public void OnClickFashionBackPackBtn()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
		if (this.mCurTapType != GAME_MENU_TAP_TYPE.FASHION_TAB)
		{
			this.mCurTapType = GAME_MENU_TAP_TYPE.FASHION_TAB;
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuPlayerInfoRootUI);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuPlayerModelPageRoot, new UIManager.OnOpenUIDelegate(this.OnOpenPlayerModelPageRoot), null);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuBackPackRootUI, new UIManager.OnOpenUIDelegate(this.OnOpenBackPackRoot), null);
		}
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(2);
		UnityVersionUtil.SetActiveRecursive(this.AutoEquipBtn, false);
	}

	// Token: 0x06004428 RID: 17448 RVA: 0x001525E0 File Offset: 0x001507E0
	public void OnClickBadgeBtn()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
		if (this.mCurTapType != GAME_MENU_TAP_TYPE.BADGE_BACKPACK_TAB)
		{
			this.mCurTapType = GAME_MENU_TAP_TYPE.BADGE_BACKPACK_TAB;
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuPlayerInfoRootUI);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuPlayerModelPageRoot, new UIManager.OnOpenUIDelegate(this.OnOpenPlayerModelPageRoot), null);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuBackPackRootUI, new UIManager.OnOpenUIDelegate(this.OnOpenBackPackRoot), null);
		}
		SingletonUnity<MenuBaseRootLogic>.Instance.SetTargetBtnToggleEnable(3);
		UnityVersionUtil.SetActiveRecursive(this.AutoEquipBtn, false);
	}

	// Token: 0x06004429 RID: 17449 RVA: 0x0015267C File Offset: 0x0015087C
	public void GoToZhuangBeiQiangHua(GameItem item)
	{
		this.curitem = item;
		this.isGoToEquipUp = true;
	}

	// Token: 0x0600442A RID: 17450 RVA: 0x0015268C File Offset: 0x0015088C
	private void OnOpenBackPackRoot(bool success, object param)
	{
		if (this.mCurTapType == GAME_MENU_TAP_TYPE.ITEM_BACKPACK_TAP)
		{
			SingletonUnity<BackPackPageRootLogic>.Instance.Reset(true, ITEM_CONTAINER_TYPE.ITEM_BACKPACK);
		}
		else if (this.mCurTapType == GAME_MENU_TAP_TYPE.EQUIP_BACKPACK_TAB)
		{
			SingletonUnity<BackPackPageRootLogic>.Instance.Reset(true, ITEM_CONTAINER_TYPE.EQUIP_BACKPACK);
		}
		else if (this.mCurTapType == GAME_MENU_TAP_TYPE.BADGE_BACKPACK_TAB)
		{
			SingletonUnity<BackPackPageRootLogic>.Instance.Reset(true, ITEM_CONTAINER_TYPE.BADGE_BACKPACK);
		}
		else if (this.mCurTapType == GAME_MENU_TAP_TYPE.FASHION_TAB)
		{
			SingletonUnity<BackPackPageRootLogic>.Instance.Reset(true, ITEM_CONTAINER_TYPE.FASHION_BACKPACK);
		}
	}

	// Token: 0x0600442B RID: 17451 RVA: 0x00152708 File Offset: 0x00150908
	private void OnOpenPlayerInfoRoot(bool success, object param)
	{
		if (success)
		{
			SingletonUnity<JSSXKuangRootLogic>.Instance.Reset(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData);
		}
	}

	// Token: 0x0600442C RID: 17452 RVA: 0x00152734 File Offset: 0x00150934
	private void OnOpenSkillInfoRoot(bool success, object param)
	{
		if (success)
		{
			SingletonUnity<SkillInfoRootLogic>.Instance.Reset();
		}
	}

	// Token: 0x0600442D RID: 17453 RVA: 0x00152748 File Offset: 0x00150948
	private void OnOpenShengWangRoot(bool success, object param)
	{
		if (success)
		{
			SingletonUnity<JSShengWangLogic>.Instance.Reset();
		}
	}

	// Token: 0x0600442E RID: 17454 RVA: 0x0015275C File Offset: 0x0015095C
	private void OnOpenPlayerModelPageRoot(bool success, object param)
	{
		if (success)
		{
			PlayerModelPageRootLogic instance = SingletonUnity<PlayerModelPageRootLogic>.Instance;
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			List<GameItem> curItemList = null;
			EQUIP_PACK_TYPE curType = EQUIP_PACK_TYPE.BACKPACK;
			bool canShowItem;
			bool canClickItem;
			if (this.mCurTapType == GAME_MENU_TAP_TYPE.PLAYERINFO_TAP)
			{
				canShowItem = true;
				canClickItem = true;
				instance.ResetOnClickItem(new PlayerModelPageRootLogic.OnClickItemDelegate(this.OnClickEquipItem));
				curItemList = ItemContainerTool.GetEquipItemList(playerData.EquipPack);
				curType = EQUIP_PACK_TYPE.BACKPACK;
			}
			else
			{
				canShowItem = true;
				canClickItem = true;
				if (this.mCurTapType == GAME_MENU_TAP_TYPE.EQUIP_BACKPACK_TAB)
				{
					instance.ResetOnClickItem(new PlayerModelPageRootLogic.OnClickItemDelegate(this.OnClickEquipItem));
					curItemList = ItemContainerTool.GetEquipItemList(playerData.EquipPack);
					curType = EQUIP_PACK_TYPE.BACKPACK;
				}
				else if (this.mCurTapType == GAME_MENU_TAP_TYPE.BADGE_BACKPACK_TAB)
				{
					instance.ResetOnClickItem(new PlayerModelPageRootLogic.OnClickItemDelegate(this.OnClickEquipItem));
					curItemList = playerData.BadgeEquipPack.ItemList;
					curType = EQUIP_PACK_TYPE.BADGE;
				}
				else if (this.mCurTapType == GAME_MENU_TAP_TYPE.ITEM_BACKPACK_TAP)
				{
					instance.ResetOnClickItem(new PlayerModelPageRootLogic.OnClickItemDelegate(this.OnClickEquipItem));
					curItemList = ItemContainerTool.GetEquipItemList(playerData.EquipPack);
					curType = EQUIP_PACK_TYPE.BACKPACK;
				}
				else if (this.mCurTapType == GAME_MENU_TAP_TYPE.FASHION_TAB)
				{
					instance.ResetOnClickItem(new PlayerModelPageRootLogic.OnClickItemDelegate(this.OnClickEquipItem));
					curItemList = ItemContainerTool.GetFashionEquipItemList(playerData.FashionEquipPack);
					curType = EQUIP_PACK_TYPE.FASHION;
				}
			}
			this.ResetModelVisual();
			instance.Reset(this.mPlayerModelVisual, curItemList, string.Empty, playerData.MainPlayerAttrData.ComboValue.ToString(), canShowItem, canClickItem, curType);
		}
	}

	// Token: 0x0600442F RID: 17455 RVA: 0x001528BC File Offset: 0x00150ABC
	public void OnClickEquipItem(GameItem item)
	{
		if (item != null)
		{
			if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP || item.ItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
			{
				if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					item.ItemLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item.ItemData.SubType);
				}
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
				{
					SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, ITEM_SHOW_TYPE.EQUIPPACK, false, UI_PAGE_TYPE.INVALID, false, false);
				}, null);
			}
			else
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
				{
					SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, ITEM_SHOW_TYPE.EQUIPPACK, false, UI_PAGE_TYPE.INVALID, false, false);
				}, null);
			}
		}
	}

	// Token: 0x06004430 RID: 17456 RVA: 0x00152990 File Offset: 0x00150B90
	private void OnEnable()
	{
		this.mCurTapType = GAME_MENU_TAP_TYPE.INVILAD;
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
			MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickPlayerInfoBtn), true, "CZ_left_Character", StrDictionary.GetDictionaryString("#{100601}", new object[0]), FUNCTION_TYPE.CHARACTER_INFO, null);
			MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickEquipBackPackBtn), true, "CZ_left_Equipment", StrDictionary.GetDictionaryString("#{100602}", new object[0]), FUNCTION_TYPE.CHARACTER_EQUIP, new DelegateDefine.NoParamReturnDelegate(playerData.IsHaveEquipTips));
			MenuTabBtnInfo menuTabBtnInfo3 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickFashionBackPackBtn), true, "CZ_left_Fashion", StrDictionary.GetDictionaryString("#{100603}", new object[0]), FUNCTION_TYPE.CHARACTER_FASHION, new DelegateDefine.NoParamReturnDelegate(playerData.IsHaveFashionEquipTips));
			MenuTabBtnInfo menuTabBtnInfo4 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickBadgeBtn), true, "CZ_left_Badge", StrDictionary.GetDictionaryString("#{100604}", new object[0]), FUNCTION_TYPE.CHARACTER_BADGE, new DelegateDefine.NoParamReturnDelegate(playerData.IsHaveBadgeTips));
			MenuTabBtnInfo menuTabBtnInfo5 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickItemBackPackBtn), true, "CZ_left_Item", StrDictionary.GetDictionaryString("#{100605}", new object[0]), FUNCTION_TYPE.CHARACTER_ITEM, new DelegateDefine.NoParamReturnDelegate(playerData.IsHaveItemTips));
			list.Add(menuTabBtnInfo);
			list.Add(menuTabBtnInfo2);
			list.Add(menuTabBtnInfo3);
			list.Add(menuTabBtnInfo4);
			list.Add(menuTabBtnInfo5);
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(list, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), false, null);
		}, null);
	}

	// Token: 0x06004431 RID: 17457 RVA: 0x001529B8 File Offset: 0x00150BB8
	public void UpdateEquipPack()
	{
		PlayerModelPageRootLogic instance = SingletonUnity<PlayerModelPageRootLogic>.Instance;
		if (this.mCurTapType == GAME_MENU_TAP_TYPE.EQUIP_BACKPACK_TAB || this.mCurTapType == GAME_MENU_TAP_TYPE.ITEM_BACKPACK_TAP || this.mCurTapType == GAME_MENU_TAP_TYPE.PLAYERINFO_TAP)
		{
			instance.ReShow(ItemContainerTool.GetEquipItemList(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack));
		}
		else if (this.mCurTapType == GAME_MENU_TAP_TYPE.BADGE_BACKPACK_TAB)
		{
			instance.ReShow(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BadgeEquipPack.ItemList);
		}
		else if (this.mCurTapType == GAME_MENU_TAP_TYPE.FASHION_TAB)
		{
			instance.ReShow(ItemContainerTool.GetFashionEquipItemList(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FashionEquipPack));
		}
		this.ResetModelVisual();
	}

	// Token: 0x06004432 RID: 17458 RVA: 0x00152A64 File Offset: 0x00150C64
	public void OnClickAutoEquipBtn()
	{
		ItemContainer equipBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipBackPack;
		ItemContainer equipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack;
		PROFESSION_TYPE profession = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession;
		List<GameItem> equipItemList = ItemContainerTool.GetEquipItemList(equipPack);
		GameItem gameItem = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.WEAPON)];
		GameItem gameItem2 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.HEAD)];
		GameItem gameItem3 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.BODY)];
		GameItem gameItem4 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.LEG)];
		GameItem gameItem5 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.BELT)];
		GameItem gameItem6 = equipItemList[ItemContainerTool.ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE.NECKLACE)];
		GameItem bestTargetEquipItem = ItemContainerTool.GetBestTargetEquipItem(equipBackPack, 0, profession);
		GameItem bestTargetEquipItem2 = ItemContainerTool.GetBestTargetEquipItem(equipBackPack, 1, profession);
		GameItem bestTargetEquipItem3 = ItemContainerTool.GetBestTargetEquipItem(equipBackPack, 2, profession);
		GameItem bestTargetEquipItem4 = ItemContainerTool.GetBestTargetEquipItem(equipBackPack, 3, profession);
		GameItem bestTargetEquipItem5 = ItemContainerTool.GetBestTargetEquipItem(equipBackPack, 4, profession);
		GameItem bestTargetEquipItem6 = ItemContainerTool.GetBestTargetEquipItem(equipBackPack, 5, profession);
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			if (bestTargetEquipItem != null && !bestTargetEquipItem.IsEmpty())
			{
				if (gameItem == null || gameItem.IsEmpty())
				{
					mainPlayer.EquipItem(bestTargetEquipItem, false);
				}
				else if (bestTargetEquipItem.GetItemCombatVal() > gameItem.GetItemCombatVal())
				{
					mainPlayer.EquipItem(bestTargetEquipItem, false);
				}
			}
			if (bestTargetEquipItem2 != null && !bestTargetEquipItem2.IsEmpty())
			{
				if (gameItem2 == null || gameItem2.IsEmpty())
				{
					mainPlayer.EquipItem(bestTargetEquipItem2, false);
				}
				else if (bestTargetEquipItem2.GetItemCombatVal() > gameItem2.GetItemCombatVal())
				{
					mainPlayer.EquipItem(bestTargetEquipItem2, false);
				}
			}
			if (bestTargetEquipItem3 != null && !bestTargetEquipItem3.IsEmpty())
			{
				if (gameItem3 == null || gameItem3.IsEmpty())
				{
					mainPlayer.EquipItem(bestTargetEquipItem3, false);
				}
				else if (bestTargetEquipItem3.GetItemCombatVal() > gameItem3.GetItemCombatVal())
				{
					mainPlayer.EquipItem(bestTargetEquipItem3, false);
				}
			}
			if (bestTargetEquipItem4 != null && !bestTargetEquipItem4.IsEmpty())
			{
				if (gameItem4 == null || gameItem4.IsEmpty())
				{
					mainPlayer.EquipItem(bestTargetEquipItem4, false);
				}
				else if (bestTargetEquipItem4.GetItemCombatVal() > gameItem4.GetItemCombatVal())
				{
					mainPlayer.EquipItem(bestTargetEquipItem4, false);
				}
			}
			if (bestTargetEquipItem5 != null && !bestTargetEquipItem5.IsEmpty())
			{
				if (gameItem5 == null || gameItem5.IsEmpty())
				{
					mainPlayer.EquipItem(bestTargetEquipItem5, false);
				}
				else if (bestTargetEquipItem5.GetItemCombatVal() > gameItem5.GetItemCombatVal())
				{
					mainPlayer.EquipItem(bestTargetEquipItem5, false);
				}
			}
			if (bestTargetEquipItem6 != null && !bestTargetEquipItem6.IsEmpty())
			{
				if (gameItem6 == null || gameItem6.IsEmpty())
				{
					mainPlayer.EquipItem(bestTargetEquipItem6, false);
				}
				else if (bestTargetEquipItem6.GetItemCombatVal() > gameItem6.GetItemCombatVal())
				{
					mainPlayer.EquipItem(bestTargetEquipItem6, false);
				}
			}
		}
	}

	// Token: 0x06004433 RID: 17459 RVA: 0x00152D40 File Offset: 0x00150F40
	private bool CheckCanEquip(GameItem item)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (!playerData.CheckLevel(item.ItemData.Level))
		{
			return false;
		}
		EquipData equipDataById = DataManager.GetEquipDataById(item.ItemId);
		return equipDataById == null || playerData.Profession == equipDataById.profession;
	}

	// Token: 0x040030B1 RID: 12465
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x040030B2 RID: 12466
	public GameObject AutoEquipBtn;

	// Token: 0x040030B3 RID: 12467
	private GAME_MENU_TAP_TYPE mCurTapType = GAME_MENU_TAP_TYPE.INVILAD;

	// Token: 0x040030B4 RID: 12468
	private bool isGoToEquipUp;

	// Token: 0x040030B5 RID: 12469
	private GameItem curitem;

	// Token: 0x040030B6 RID: 12470
	private FakeObjLogic mPlayerModelVisual;

	// Token: 0x040030B7 RID: 12471
	private MenuTopRootLogic mMenuTopRoot;
}
