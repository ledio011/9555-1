using System;
using UnityEngine;

// Token: 0x0200096C RID: 2412
public class BackPackPageRootLogic : SingletonUnity<BackPackPageRootLogic>
{
	// Token: 0x060043E2 RID: 17378 RVA: 0x00150D60 File Offset: 0x0014EF60
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060043E3 RID: 17379 RVA: 0x00150D6C File Offset: 0x0014EF6C
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x060043E4 RID: 17380 RVA: 0x00150D8C File Offset: 0x0014EF8C
	private void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x17000FA6 RID: 4006
	// (get) Token: 0x060043E5 RID: 17381 RVA: 0x00150D98 File Offset: 0x0014EF98
	public BackPackRootLogic BackPackRoot
	{
		get
		{
			if (this.mBackPackRoot == null)
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.BackPackPageRootItem, new UIManager.OnLoadUIDelegate(this.OnLoadBackPackPageRoot), null);
			}
			return this.mBackPackRoot;
		}
	}

	// Token: 0x060043E6 RID: 17382 RVA: 0x00150DD0 File Offset: 0x0014EFD0
	protected override void Awake()
	{
		base.Awake();
		this.Init();
	}

	// Token: 0x060043E7 RID: 17383 RVA: 0x00150DE0 File Offset: 0x0014EFE0
	private void Init()
	{
		if (!this.mInitFlag)
		{
			this.mInitFlag = true;
			if (this.mBackPackRoot == null)
			{
				SingletonUnity<UIManager>.Instance.LoadUIItem(UIInfo.BackPackPageRootItem, new UIManager.OnLoadUIDelegate(this.OnLoadBackPackPageRoot), null);
			}
		}
	}

	// Token: 0x060043E8 RID: 17384 RVA: 0x00150E2C File Offset: 0x0014F02C
	private void OnLoadBackPackPageRoot(GameObject newObj, object param)
	{
		GameObject gameObject = Object.Instantiate(newObj) as GameObject;
		gameObject.transform.parent = this.ScaleUIRoot;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		this.mBackPackRoot = gameObject.GetComponent<BackPackRootLogic>();
		BackPackRootLogic backPackRootLogic = this.mBackPackRoot;
		backPackRootLogic.onClickItem = (BackPackRootLogic.OnClickItemDelegate)Delegate.Combine(backPackRootLogic.onClickItem, new BackPackRootLogic.OnClickItemDelegate(this.OnClickBackPackItem));
		this.mBackPackRoot.OffsetRoot.localPosition = new Vector3(200f, 0f, 0f);
		this.mBackPackRoot.InitCanSell(true);
	}

	// Token: 0x060043E9 RID: 17385 RVA: 0x00150EDC File Offset: 0x0014F0DC
	public void Reset(bool needResetPackPos, ITEM_CONTAINER_TYPE containerType)
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.BADGE_CLICK_ITEM)
		{
			FunctionTipsRootLogic.ClearHandTip();
		}
		this.mCurContainerType = containerType;
		if (this.BackPackRoot != null)
		{
			this.BackPackRoot.Show(needResetPackPos, this.mCurContainerType);
		}
	}

	// Token: 0x060043EA RID: 17386 RVA: 0x00150F24 File Offset: 0x0014F124
	private bool IsRootActive()
	{
		return UnityVersionUtil.IsActive(base.gameObject);
	}

	// Token: 0x060043EB RID: 17387 RVA: 0x00150F34 File Offset: 0x0014F134
	private bool IsBackPackRootActive()
	{
		return !(this.BackPackRoot == null) && UnityVersionUtil.IsActive(this.BackPackRoot.gameObject);
	}

	// Token: 0x060043EC RID: 17388 RVA: 0x00150F64 File Offset: 0x0014F164
	private bool IsEquipPackRootActive()
	{
		return SingletonUnity<PlayerModelPageRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PlayerModelPageRootLogic>.Instance.gameObject);
	}

	// Token: 0x060043ED RID: 17389 RVA: 0x00150F88 File Offset: 0x0014F188
	public void ResetBackPack()
	{
		if (this.IsRootActive() && this.IsBackPackRootActive())
		{
			this.BackPackRoot.UpdateBackPack();
		}
	}

	// Token: 0x060043EE RID: 17390 RVA: 0x00150FAC File Offset: 0x0014F1AC
	public void OnClickBackPackItem(GameItem item)
	{
		this.ShowItemInfo(item, ITEM_SHOW_TYPE.BACKPACK, (float)this.mItemInfoLeftOffsetPos, 0f);
	}

	// Token: 0x060043EF RID: 17391 RVA: 0x00150FC4 File Offset: 0x0014F1C4
	public void ShowItemInfo(GameItem item, ITEM_SHOW_TYPE showType, float xOffset, float yOffset)
	{
		if (item.ItemId.Equals(GameDefine.EmptyAddItemID))
		{
			if (this.mCurContainerType == ITEM_CONTAINER_TYPE.EQUIP_BACKPACK)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100659}", new object[0]), true, false);
				return;
			}
			if (this.mCurContainerType == ITEM_CONTAINER_TYPE.FASHION_BACKPACK)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_TOOL))
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100662}", new object[0]), true, false);
					return;
				}
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
				{
					SingletonUnity<ShopUIRootLogic>.Instance.OnClickBigSaleBtn(GameDefine.SHOP_TAB_TYPE.FASHION, GameDefine.UIBACKTYPE.FASHION, null);
				}, null);
			}
			else if (this.mCurContainerType == ITEM_CONTAINER_TYPE.BADGE_BACKPACK)
			{
				if (!this.CheckUnlockFunction(FUNCTION_TYPE.SHOP_TOOL))
				{
					NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100661}", new object[0]), true, false);
					return;
				}
				SingletonUnity<MenuBaseRootLogic>.Instance.OnClickBackBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ShopRoot, delegate
				{
					SingletonUnity<ShopUIRootLogic>.Instance.OnClickToolsBtn(GameDefine.SHOP_TAB_TYPE.ITEM, GameDefine.UIBACKTYPE.BADGE, "9602");
				}, null);
			}
			else if (this.mCurContainerType == ITEM_CONTAINER_TYPE.ITEM_BACKPACK)
			{
				NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100426}", new object[0]), true, false);
			}
			return;
		}
		else if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			EquipData equipDataById = DataManager.GetEquipDataById(item.ItemData.ID);
			if (!item.IsAppraise)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
				{
					SingletonUnity<ItemInfoRootLogicNew>.Instance.ResetAppraise(item, ITEM_SHOW_TYPE.BACKPACK);
				}, null);
				return;
			}
			if (item.ItemData.SubType == 0 || equipDataById.profession == SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Profession)
			{
				PlayerModelPageRootLogic curModelPage = SingletonUnity<PlayerModelPageRootLogic>.Instance;
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
				{
					int equipEnhanceLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item.ItemData.SubType);
					if (item != null)
					{
						item.ItemLevel = equipEnhanceLevel;
					}
					GameItem targetTypeEquip = curModelPage.GetTargetTypeEquip((EQUIP_BACKPACK_TYPE)item.ItemData.SubType);
					if (targetTypeEquip != null && !targetTypeEquip.IsEmpty())
					{
						targetTypeEquip.ItemLevel = equipEnhanceLevel;
					}
					SingletonUnity<ItemInfoRootLogicNew>.Instance.ResetCompareEquip(item, targetTypeEquip);
					if (showType == ITEM_SHOW_TYPE.BACKPACK)
					{
						this.CheckTutorialEvent();
					}
				}, null);
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
			{
				int equipEnhanceLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item.ItemData.SubType);
				if (item != null)
				{
					item.ItemLevel = equipEnhanceLevel;
				}
				SingletonUnity<ItemInfoRootLogicNew>.Instance.ResetCompareEquip(item, null);
			}, null);
			return;
		}
		else
		{
			if (item.ItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
				{
					SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, ITEM_SHOW_TYPE.BACKPACK, false, UI_PAGE_TYPE.INVALID, false, false);
				}, null);
				return;
			}
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ItemInfoRootNew, delegate
			{
				if (item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					int equipEnhanceLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(item.ItemData.SubType);
					item.ItemLevel = equipEnhanceLevel;
				}
				if (!string.IsNullOrEmpty(item.ItemData.JumpPath))
				{
					SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, showType, true, UI_PAGE_TYPE.BACK_PACK_ITEM, false, false);
				}
				else
				{
					SingletonUnity<ItemInfoRootLogicNew>.Instance.Reset(item, showType, false, UI_PAGE_TYPE.INVALID, false, false);
				}
				if (showType == ITEM_SHOW_TYPE.BACKPACK)
				{
					this.CheckTutorialEvent();
				}
			}, null);
			return;
		}
	}

	// Token: 0x060043F0 RID: 17392 RVA: 0x00151260 File Offset: 0x0014F460
	public bool CheckUnlockFunction(FUNCTION_TYPE curFunction)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		return playerCommonData.IsFunctionUnlock(curFunction);
	}

	// Token: 0x060043F1 RID: 17393 RVA: 0x00151288 File Offset: 0x0014F488
	public void OnCloseItemInfo()
	{
		this.Reset(false, this.mCurContainerType);
		if (SingletonUnity<PlayerModelPageRootLogic>.Exists && !UnityVersionUtil.IsActive(SingletonUnity<PlayerModelPageRootLogic>.Instance.gameObject))
		{
			if (this.mCurContainerType == ITEM_CONTAINER_TYPE.ITEM_BACKPACK || this.mCurContainerType == ITEM_CONTAINER_TYPE.EQUIP_BACKPACK)
			{
				SingletonUnity<PlayerModelPageRootLogic>.Instance.ReShow(ItemContainerTool.GetEquipItemList(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.EquipPack));
			}
			else if (this.mCurContainerType == ITEM_CONTAINER_TYPE.BADGE_BACKPACK)
			{
				SingletonUnity<PlayerModelPageRootLogic>.Instance.ReShow(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.BadgeEquipPack.ItemList);
			}
			SingletonUnity<PlayerInfoMenuRootLogic>.Instance.ResetModelVisual();
		}
	}

	// Token: 0x0400308C RID: 12428
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x0400308D RID: 12429
	public Transform ScaleUIRoot;

	// Token: 0x0400308E RID: 12430
	private BackPackRootLogic mBackPackRoot;

	// Token: 0x0400308F RID: 12431
	private int mItemInfoLeftOffsetPos = -180;

	// Token: 0x04003090 RID: 12432
	private int mItemInfoRightOffsetPos = 136;

	// Token: 0x04003091 RID: 12433
	private bool mInitFlag;

	// Token: 0x04003092 RID: 12434
	private ITEM_CONTAINER_TYPE mCurContainerType;

	// Token: 0x04003093 RID: 12435
	private ITEM_SHOW_TYPE mCurItemShowType;
}
