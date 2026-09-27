using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000971 RID: 2417
public class PlayerModelPageRootLogic : SingletonUnity<PlayerModelPageRootLogic>
{
	// Token: 0x06004436 RID: 17462 RVA: 0x00153158 File Offset: 0x00151358
	private void Start()
	{
		if (!this.mInitFlag)
		{
			UIEventListener rotateModelBtnListener = this.RotateModelBtnListener;
			rotateModelBtnListener.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(rotateModelBtnListener.onDrag, new UIEventListener.VectorDelegate(this.OnDragModelBtn));
			this.mInitFlag = true;
			this.Init();
		}
	}

	// Token: 0x06004437 RID: 17463 RVA: 0x001531A4 File Offset: 0x001513A4
	private void Init()
	{
		for (int i = 0; i < this.ItemShowList.Count; i++)
		{
			ItemUILogic itemUILogic = this.ItemShowList[i];
			itemUILogic.onClickItem = (ItemUILogic.OnClickItemDelegate)Delegate.Combine(itemUILogic.onClickItem, new ItemUILogic.OnClickItemDelegate(this.OnClickItem));
		}
	}

	// Token: 0x06004438 RID: 17464 RVA: 0x001531FC File Offset: 0x001513FC
	private void UpdateFahsionFlag(bool isShow)
	{
		if (isShow)
		{
			this.ShowFashionSprite.spriteName = "CZ_shiZhuang_XianShi";
			this.ShowFashionLabel.text = StrDictionary.GetDictionaryString("close", new object[0]);
		}
		else
		{
			this.ShowFashionSprite.spriteName = "CZ_shiZhuang_BuXianShi";
			this.ShowFashionLabel.text = StrDictionary.GetDictionaryString("open", new object[0]);
		}
	}

	// Token: 0x06004439 RID: 17465 RVA: 0x0015326C File Offset: 0x0015146C
	public void Hide()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.mCurFakeObj.FakeObj, false);
	}

	// Token: 0x0600443A RID: 17466 RVA: 0x001532A8 File Offset: 0x001514A8
	public void Show(FakeObjLogic curFakeObj, List<GameItem> curItemList, string topStr, string bottomStr, bool canShowItem, bool canClickItem, EQUIP_PACK_TYPE curType)
	{
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		}
		UnityVersionUtil.SetActiveRecursive(curFakeObj.FakeObj.gameObject, curType != EQUIP_PACK_TYPE.BADGE);
		this.Reset(curFakeObj, curItemList, topStr, bottomStr, canShowItem, canClickItem, curType);
	}

	// Token: 0x0600443B RID: 17467 RVA: 0x001532FC File Offset: 0x001514FC
	public void ReShow(List<GameItem> itemList)
	{
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			UnityVersionUtil.SetActiveRecursive(base.gameObject, true);
		}
		if (this.CanShowItemFlag)
		{
			this.UpdateEquipPack(itemList);
		}
		else
		{
			this.HideEquipPack();
		}
	}

	// Token: 0x0600443C RID: 17468 RVA: 0x00153344 File Offset: 0x00151544
	public void ResetOnClickItem(PlayerModelPageRootLogic.OnClickItemDelegate func)
	{
		this.onClickItem = func;
	}

	// Token: 0x0600443D RID: 17469 RVA: 0x00153350 File Offset: 0x00151550
	public void UpdateComboValue(int value)
	{
		if (this.mCurEquipPackType != EQUIP_PACK_TYPE.BADGE)
		{
			this.BottomLabel.text = value.ToString();
		}
	}

	// Token: 0x0600443E RID: 17470 RVA: 0x00153370 File Offset: 0x00151570
	public static void UpdateCombo(int value)
	{
		if (SingletonUnity<PlayerModelPageRootLogic>.Exists)
		{
			SingletonUnity<PlayerModelPageRootLogic>.Instance.UpdateComboValue(value);
		}
	}

	// Token: 0x0600443F RID: 17471 RVA: 0x00153388 File Offset: 0x00151588
	public void Reset(FakeObjLogic curFakeObj, List<GameItem> curItemList, string topStr, string bottomStr, bool canShowItem, bool canClickItem, EQUIP_PACK_TYPE curType)
	{
		this.TopLabel.text = topStr;
		this.BottomLabel.text = bottomStr;
		this.mCurFakeObj = curFakeObj;
		this.mCurItemDataList = curItemList;
		this.CanShowItemFlag = canShowItem;
		this.CanClickItemFlag = canClickItem;
		this.mCurEquipPackType = curType;
		if (UnityVersionUtil.IsActive(curFakeObj.FakeObj.gameObject) != (curType != EQUIP_PACK_TYPE.BADGE))
		{
			UnityVersionUtil.SetActiveRecursive(curFakeObj.FakeObj.gameObject, curType != EQUIP_PACK_TYPE.BADGE);
		}
		if (this.CanShowItemFlag)
		{
			this.UpdateEquipPack(this.mCurItemDataList);
		}
		else
		{
			this.HideEquipPack();
		}
	}

	// Token: 0x06004440 RID: 17472 RVA: 0x0015342C File Offset: 0x0015162C
	public GameItem GetTargetTypeEquip(EQUIP_BACKPACK_TYPE targetType)
	{
		if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BACKPACK)
		{
			if (this.mCurItemDataList[ItemContainerTool.ChangeEquipTypeToIndex(targetType)].IsEmpty())
			{
				return null;
			}
			return this.mCurItemDataList[ItemContainerTool.ChangeEquipTypeToIndex(targetType)];
		}
		else
		{
			if (this.mCurEquipPackType != EQUIP_PACK_TYPE.FASHION)
			{
				return null;
			}
			if (this.mCurItemDataList[ItemContainerTool.ChangeFashionEquipTypeToIndex(targetType)].IsEmpty())
			{
				return null;
			}
			return this.mCurItemDataList[ItemContainerTool.ChangeFashionEquipTypeToIndex(targetType)];
		}
	}

	// Token: 0x06004441 RID: 17473 RVA: 0x001534B0 File Offset: 0x001516B0
	public int GetTargetTypeEquipScore(EQUIP_BACKPACK_TYPE targetType)
	{
		if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BACKPACK)
		{
			GameItem gameItem = this.mCurItemDataList[ItemContainerTool.ChangeEquipTypeToIndex(targetType)];
			if (gameItem == null || gameItem.IsEmpty())
			{
				return 0;
			}
			return gameItem.GetItemScore();
		}
		else
		{
			if (this.mCurEquipPackType != EQUIP_PACK_TYPE.FASHION)
			{
				return 0;
			}
			GameItem gameItem2 = this.mCurItemDataList[ItemContainerTool.ChangeFashionEquipTypeToIndex(targetType)];
			if (gameItem2 == null || gameItem2.IsEmpty())
			{
				return 0;
			}
			return gameItem2.GetItemScore();
		}
	}

	// Token: 0x06004442 RID: 17474 RVA: 0x00153530 File Offset: 0x00151730
	public int GetTargetTypeEquipCombatVal(EQUIP_BACKPACK_TYPE targetType)
	{
		if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BACKPACK)
		{
			GameItem gameItem = this.mCurItemDataList[ItemContainerTool.ChangeEquipTypeToIndex(targetType)];
			if (gameItem == null || gameItem.IsEmpty())
			{
				return 0;
			}
			return gameItem.GetItemCombatVal();
		}
		else
		{
			if (this.mCurEquipPackType != EQUIP_PACK_TYPE.FASHION)
			{
				return 0;
			}
			GameItem gameItem2 = this.mCurItemDataList[ItemContainerTool.ChangeFashionEquipTypeToIndex(targetType)];
			if (gameItem2 == null || gameItem2.IsEmpty())
			{
				return 0;
			}
			return gameItem2.GetItemCombatVal();
		}
	}

	// Token: 0x06004443 RID: 17475 RVA: 0x001535B0 File Offset: 0x001517B0
	private void HideEquipPack()
	{
		for (int i = 0; i < this.ItemShowList.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.ItemShowList[i].gameObject, false);
		}
	}

	// Token: 0x06004444 RID: 17476 RVA: 0x001535F0 File Offset: 0x001517F0
	private void UpdateBageItem()
	{
		int[] array = new int[3];
		int[] array2 = new int[3];
		int num = 1;
		int num2 = 0;
		for (int i = 0; i < this.mCurItemDataList.Count; i++)
		{
			GameItem gameItem = this.mCurItemDataList[i];
			if (gameItem != null && !gameItem.IsEmpty())
			{
				ItemData itemData = gameItem.ItemData;
				BadgeData badgeDataById = DataManager.GetBadgeDataById(gameItem.ItemId);
				array[badgeDataById.Color]++;
				array2[badgeDataById.Color] += badgeDataById.Lv;
				this.BadgeAttributeName[num].text = GameDefine.GetAttributeName_S(badgeDataById.Status1);
				this.BadgeAttributeICON[num].spriteName = GameDefine.GetAttributeIcon(badgeDataById.Status1);
				this.BadgeAttributeValue[num].text = GameDefine.GetAttributeValueStr(badgeDataById.Status1, badgeDataById.Value1);
				num2 += gameItem.GetItemCombatVal();
				num++;
			}
		}
		int num3 = num;
		for (int j = 0; j < this.BadgeAttributeObj.Count; j++)
		{
			NGUITools.SetActive(this.BadgeAttributeObj[j], j < num3);
		}
		int num4 = -1;
		int num5 = 0;
		for (int k = 0; k < array.Length; k++)
		{
			if (array[k] >= 3)
			{
				num4 = k;
				num5 = array2[k];
				break;
			}
		}
		List<int> list = new List<int>();
		if (num4 > -1)
		{
			for (int l = 0; l < this.mCurItemDataList.Count; l++)
			{
				GameItem gameItem2 = this.mCurItemDataList[l];
				if (gameItem2 != null && !gameItem2.IsEmpty())
				{
					ItemData itemData2 = gameItem2.ItemData;
					BadgeData badgeDataById2 = DataManager.GetBadgeDataById(gameItem2.ItemId);
					if (badgeDataById2.Color == num4)
					{
						list.Add(l);
					}
				}
			}
		}
		for (int m = 0; m < this.ItemShowList.Count; m++)
		{
			if (list.Contains(m))
			{
				this.ItemsEffectBgs[m].enabled = true;
			}
			else
			{
				this.ItemsEffectBgs[m].enabled = false;
			}
		}
		for (int n = 0; n < this.IconEffect.Length; n++)
		{
			if (n < list.Count)
			{
				this.IconEffect[n].transform.parent = this.ItemShowList[list[n]].transform;
				this.IconEffect[n].transform.localPosition = Vector3.zero;
				UnityVersionUtil.SetActiveRecursive(this.IconEffect[n], true);
			}
			else
			{
				this.IconEffect[n].transform.parent = this.effectParent;
				this.IconEffect[n].transform.localPosition = Vector3.zero;
				UnityVersionUtil.SetActiveRecursive(this.IconEffect[n], false);
			}
		}
		NGUITools.SetActive(this.BadgeAttributeObj[0], num4 > -1);
		if (num4 == 1)
		{
			ConfigData configDataByKey = DataManager.GetConfigDataByKey("badge_parm_2");
			ConfigData configDataByKey2 = DataManager.GetConfigDataByKey("badge_attribute_2");
			if (configDataByKey != null)
			{
				int value = Mathf.FloorToInt((float)num5 / configDataByKey.Valuef);
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(configDataByKey2.Valuei);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(configDataByKey2.Valuei);
				this.BadgeAttributeValue[0].text = GameDefine.GetAttributeValueStr(configDataByKey2.Valuei, value);
			}
			else
			{
				float num6 = Mathf.Pow((float)num5, 1.25f) / 3.94822f / 1000f;
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(1010);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(1010);
				this.BadgeAttributeValue[0].text = string.Format("+{0:P1}", num6);
			}
		}
		else if (num4 == 0)
		{
			ConfigData configDataByKey3 = DataManager.GetConfigDataByKey("badge_parm_1");
			ConfigData configDataByKey4 = DataManager.GetConfigDataByKey("badge_attribute_1");
			if (configDataByKey3 != null)
			{
				int value2 = Mathf.FloorToInt((float)num5 / configDataByKey3.Valuef);
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(configDataByKey4.Valuei);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(configDataByKey4.Valuei);
				this.BadgeAttributeValue[0].text = GameDefine.GetAttributeValueStr(configDataByKey4.Valuei, value2);
			}
			else
			{
				float num7 = Mathf.Pow((float)num5, 0.5555f) / 1.8411f / 100f;
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(1012);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(1012);
				this.BadgeAttributeValue[0].text = string.Format("+{0:P1}", num7);
			}
		}
		else if (num4 == 2)
		{
			ConfigData configDataByKey5 = DataManager.GetConfigDataByKey("badge_parm_3");
			ConfigData configDataByKey6 = DataManager.GetConfigDataByKey("badge_attribute_3");
			if (configDataByKey5 != null)
			{
				int value3 = Mathf.FloorToInt((float)num5 / configDataByKey5.Valuef);
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(configDataByKey6.Valuei);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(configDataByKey6.Valuei);
				this.BadgeAttributeValue[0].text = GameDefine.GetAttributeValueStr(configDataByKey6.Valuei, value3);
			}
			else
			{
				float num8 = Mathf.Pow((float)num5, 0.5f) / 1.73205f * 0.5f;
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(1011);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(1011);
				this.BadgeAttributeValue[0].text = string.Format("+{0:F1}", num8);
			}
		}
		this.BottomLabel.text = num2.ToString();
		this.BadgeGrid.Reposition();
	}

	// Token: 0x06004445 RID: 17477 RVA: 0x00153C68 File Offset: 0x00151E68
	public void CloseIconEffect()
	{
		for (int i = 0; i < this.ItemsEffectBgs.Length; i++)
		{
			this.ItemsEffectBgs[i].enabled = false;
			UnityVersionUtil.SetActiveRecursive(this.IconEffect[i], false);
		}
	}

	// Token: 0x06004446 RID: 17478 RVA: 0x00153CAC File Offset: 0x00151EAC
	private void UpdateEquipPack(List<GameItem> itemList)
	{
		this.mCurItemDataList = itemList;
		Vector3[] array = null;
		if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BACKPACK)
		{
			array = this.mEquipPackPosList;
		}
		else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.FASHION)
		{
			array = this.mFashionPackPosList;
		}
		else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BADGE)
		{
			array = this.mBadgePackPosList;
		}
		for (int i = 0; i < this.ItemShowList.Count; i++)
		{
			if (i < this.mCurItemDataList.Count)
			{
				if (!UnityVersionUtil.IsActive(this.ItemShowList[i].gameObject))
				{
					UnityVersionUtil.SetActiveRecursive(this.ItemShowList[i].gameObject, true);
				}
				if (this.mCurItemDataList[i] != null && !this.mCurItemDataList[i].IsEmpty())
				{
					this.ItemShowList[i].UpdateItemUI(this.mCurItemDataList[i], false);
				}
				else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BADGE)
				{
					this.ItemShowList[i].SetItemEmpty(ITEM_CONTAINER_TYPE.BADGE_EQUIPPACK, EQUIP_BACKPACK_TYPE.COUNT);
				}
				else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BACKPACK)
				{
					this.ItemShowList[i].SetItemEmpty(ITEM_CONTAINER_TYPE.EQUIPPACK, ItemContainerTool.ChangeIndexToEquipType(i));
				}
				else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.FASHION)
				{
					this.ItemShowList[i].SetItemEmpty(ITEM_CONTAINER_TYPE.EQUIPPACK, ItemContainerTool.ChangeIndexToFashionEquipType(i));
				}
				this.ItemShowList[i].transform.localPosition = array[i];
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ItemShowList[i].gameObject, false);
			}
		}
		this.CloseIconEffect();
		if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BACKPACK)
		{
			NGUITools.SetActive(this.ShowFashionBtn, false);
			NGUITools.SetActive(this.BadgeObj, false);
			NGUITools.SetActive(this.ComboObj, true);
			this.ComboObj.transform.localPosition = this.mModelComboObjPos;
			this.TopLabel.enabled = true;
		}
		else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.FASHION)
		{
			NGUITools.SetActive(this.ShowFashionBtn, true);
			NGUITools.SetActive(this.BadgeObj, false);
			NGUITools.SetActive(this.ComboObj, true);
			this.ComboObj.transform.localPosition = this.mModelComboObjPos;
			this.TopLabel.enabled = true;
			this.UpdateFahsionFlag(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsShowFashion);
		}
		else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BADGE)
		{
			NGUITools.SetActive(this.ShowFashionBtn, false);
			NGUITools.SetActive(this.BadgeObj, true);
			NGUITools.SetActive(this.ComboObj, true);
			this.ComboObj.transform.localPosition = this.mBadgeComboObjPos;
			this.TopLabel.enabled = false;
			this.UpdateBageItem();
		}
	}

	// Token: 0x06004447 RID: 17479 RVA: 0x00153F7C File Offset: 0x0015217C
	private void OnDragModelBtn(GameObject btn, Vector2 delta)
	{
		this.mCurFakeObj.FakeObj.transform.localEulerAngles -= delta.x * Vector3.up;
	}

	// Token: 0x06004448 RID: 17480 RVA: 0x00153FBC File Offset: 0x001521BC
	private void OnEnable()
	{
		if (SingletonUnity<FakeObjRootLogic>.Exists)
		{
			SingletonUnity<FakeObjRootLogic>.Instance.EnableFakeObjRoot();
			SingletonUnity<FakeObjRootLogic>.Instance.SetPicValue(0.65f);
			this.ModelPic.mainTexture = SingletonUnity<FakeObjRootLogic>.Instance.ModelPic;
		}
		for (int i = 0; i < this.ItemsEffectBgs.Length; i++)
		{
			this.ItemsEffectBgs[i].enabled = false;
		}
	}

	// Token: 0x06004449 RID: 17481 RVA: 0x00154028 File Offset: 0x00152228
	private void OnDisable()
	{
		if (SingletonUnity<FakeObjRootLogic>.Exists)
		{
			SingletonUnity<FakeObjRootLogic>.Instance.DisableFakeObjRoot();
		}
	}

	// Token: 0x0600444A RID: 17482 RVA: 0x00154040 File Offset: 0x00152240
	public void OnClicktishiBtn()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate(bool bSuccess, object param)
		{
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{100631}", "#{100632}", null, new object[0]);
		}, null);
	}

	// Token: 0x0600444B RID: 17483 RVA: 0x00154070 File Offset: 0x00152270
	public void OnClickItem(GameItem item, ItemUILogic curUIItem)
	{
		if (item != null)
		{
			if (this.onClickItem != null)
			{
				this.onClickItem(item);
			}
		}
		else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BACKPACK)
		{
			for (int i = 0; i < this.ItemShowList.Count; i++)
			{
				if (curUIItem == this.ItemShowList[i])
				{
					switch (ItemContainerTool.ChangeIndexToEquipType(i))
					{
					case EQUIP_BACKPACK_TYPE.WEAPON:
						break;
					case EQUIP_BACKPACK_TYPE.HEAD:
						NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100659}", new object[0]), true, false);
						break;
					case EQUIP_BACKPACK_TYPE.BODY:
						NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100659}", new object[0]), true, false);
						break;
					case EQUIP_BACKPACK_TYPE.LEG:
						NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100659}", new object[0]), true, false);
						break;
					case EQUIP_BACKPACK_TYPE.BELT:
						NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100659}", new object[0]), true, false);
						break;
					case EQUIP_BACKPACK_TYPE.NECKLACE:
						NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100660}", new object[0]), true, false);
						break;
					default:
						Debug.Log("No Item");
						break;
					}
				}
			}
		}
		else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.FASHION)
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
		else if (this.mCurEquipPackType == EQUIP_PACK_TYPE.BADGE)
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
	}

	// Token: 0x0600444C RID: 17484 RVA: 0x0015428C File Offset: 0x0015248C
	public bool CheckUnlockFunction(FUNCTION_TYPE curFunction)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		return playerCommonData.IsFunctionUnlock(curFunction);
	}

	// Token: 0x0600444D RID: 17485 RVA: 0x001542B4 File Offset: 0x001524B4
	public void OnClickShowFashionBtn()
	{
		if (SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(221, 0.5f))
		{
			PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
			playerData.IsShowFashion = !playerData.IsShowFashion;
			this.UpdateFahsionFlag(playerData.IsShowFashion);
			change_show_type.request request = new change_show_type.request();
			request.showType = ((!playerData.IsShowFashion) ? 0L : 1L);
			NetLogic.GetInstance().Send<Protocol.change_show_type>(request, null);
			SingletonUnity<PlayerInfoMenuRootLogic>.Instance.ResetModelVisual();
		}
		else
		{
			NoticeLogic.AddNotifyData("#{100272}", true, false);
		}
	}

	// Token: 0x0600444E RID: 17486 RVA: 0x00154348 File Offset: 0x00152548
	public int GetBadgeEquipCombatVal()
	{
		int num = 0;
		for (int i = 0; i < this.mCurItemDataList.Count; i++)
		{
			if (this.mCurItemDataList[i] != null && !this.mCurItemDataList[i].IsEmpty())
			{
				num += this.mCurItemDataList[i].GetItemCombatVal();
			}
		}
		return num;
	}

	// Token: 0x040030BF RID: 12479
	public PlayerModelPageRootLogic.OnClickItemDelegate onClickItem;

	// Token: 0x040030C0 RID: 12480
	public bool CanClickItemFlag;

	// Token: 0x040030C1 RID: 12481
	public bool CanShowItemFlag;

	// Token: 0x040030C2 RID: 12482
	public UIEventListener RotateModelBtnListener;

	// Token: 0x040030C3 RID: 12483
	public List<ItemUILogic> ItemShowList;

	// Token: 0x040030C4 RID: 12484
	public List<UILabel> BadgeAttributeName;

	// Token: 0x040030C5 RID: 12485
	public List<UISprite> BadgeAttributeICON;

	// Token: 0x040030C6 RID: 12486
	public List<UILabel> BadgeAttributeValue;

	// Token: 0x040030C7 RID: 12487
	public List<GameObject> BadgeAttributeObj;

	// Token: 0x040030C8 RID: 12488
	private List<GameItem> mCurItemDataList;

	// Token: 0x040030C9 RID: 12489
	private PROFESSION_TYPE TargetProfession;

	// Token: 0x040030CA RID: 12490
	private string TargetModelName;

	// Token: 0x040030CB RID: 12491
	private FakeObjLogic mCurFakeObj;

	// Token: 0x040030CC RID: 12492
	public UILabel TopLabel;

	// Token: 0x040030CD RID: 12493
	public UILabel BottomLabel;

	// Token: 0x040030CE RID: 12494
	public UITexture ModelPic;

	// Token: 0x040030CF RID: 12495
	public GameObject ShowFashionBtn;

	// Token: 0x040030D0 RID: 12496
	public UISprite ShowFashionSprite;

	// Token: 0x040030D1 RID: 12497
	public UILabel ShowFashionLabel;

	// Token: 0x040030D2 RID: 12498
	public GameObject BadgeObj;

	// Token: 0x040030D3 RID: 12499
	public GameObject ComboObj;

	// Token: 0x040030D4 RID: 12500
	private Vector3 mModelComboObjPos = new Vector3(64.5f, -183f, 0f);

	// Token: 0x040030D5 RID: 12501
	private Vector3 mBadgeComboObjPos = new Vector3(64.5f, -69f, 0f);

	// Token: 0x040030D6 RID: 12502
	public UIGrid BadgeGrid;

	// Token: 0x040030D7 RID: 12503
	private Vector3[] mEquipPackPosList = new Vector3[]
	{
		new Vector3(187f, 88f, 0f),
		new Vector3(-51f, 0f, 0f),
		new Vector3(187f, 0f, 0f),
		new Vector3(-51f, -88f, 0f),
		new Vector3(187f, -88f, 0f),
		new Vector3(-51f, 88f, 0f)
	};

	// Token: 0x040030D8 RID: 12504
	private Vector3[] mFashionPackPosList = new Vector3[]
	{
		new Vector3(-44f, 44f, 0f),
		new Vector3(175f, 44f, 0f),
		new Vector3(-44f, -44f, 0f),
		new Vector3(175f, -44f, 0f)
	};

	// Token: 0x040030D9 RID: 12505
	private Vector3[] mBadgePackPosList = new Vector3[]
	{
		new Vector3(60f, 150f, 0f),
		new Vector3(150f, 75f, 0f),
		new Vector3(120f, -14f, 0f),
		new Vector3(-3f, -14f, 0f),
		new Vector3(-29f, 75f, 0f)
	};

	// Token: 0x040030DA RID: 12506
	private EQUIP_PACK_TYPE mCurEquipPackType;

	// Token: 0x040030DB RID: 12507
	private bool mInitFlag;

	// Token: 0x040030DC RID: 12508
	public GameObject[] IconEffect;

	// Token: 0x040030DD RID: 12509
	public UISprite[] ItemsEffectBgs;

	// Token: 0x040030DE RID: 12510
	public Transform effectParent;

	// Token: 0x02000AF6 RID: 2806
	// (Invoke) Token: 0x06005061 RID: 20577
	public delegate void OnClickItemDelegate(GameItem item);
}
