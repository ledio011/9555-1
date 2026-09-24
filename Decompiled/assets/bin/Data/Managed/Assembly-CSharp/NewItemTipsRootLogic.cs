using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200095F RID: 2399
public class NewItemTipsRootLogic : SingletonUnity<NewItemTipsRootLogic>
{
	// Token: 0x0600432D RID: 17197 RVA: 0x0014B6E0 File Offset: 0x001498E0
	public static void AddNewItem(GameItem newitem)
	{
		ItemData itemData = newitem.ItemData;
		ItemContainer itemContainer = null;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (itemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			itemContainer = playerData.GetItemContainer(ITEM_CONTAINER_TYPE.EQUIPPACK);
		}
		else if (itemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			itemContainer = playerData.GetItemContainer(ITEM_CONTAINER_TYPE.FASHION_EQUIPPACK);
		}
		if (newitem.Parm[5] != 0 || itemContainer == null)
		{
			return;
		}
		EquipData equipDataById = DataManager.GetEquipDataById(newitem.ItemId);
		if (equipDataById.profession != playerData.Profession && newitem.ItemData.SubType != 0)
		{
			return;
		}
		if (!playerData.CheckLevel(itemData.Level))
		{
			return;
		}
		bool flag = false;
		int @class = equipDataById.Class;
		if (itemContainer != null)
		{
			List<GameItem> itemList = itemContainer.ItemList;
			int num = -1;
			for (int i = 0; i < itemList.Count; i++)
			{
				GameItem gameItem = itemList[i];
				if (!gameItem.IsEmpty())
				{
					if (gameItem.ItemData.SubType == newitem.ItemData.SubType)
					{
						int itemQuality = (int)gameItem.GetItemQuality();
						int level = gameItem.ItemData.Level;
						EquipData equipDataById2 = DataManager.GetEquipDataById(gameItem.ItemId);
						if (equipDataById2 != null)
						{
							num = equipDataById2.Class;
						}
						break;
					}
				}
			}
			if (num < @class)
			{
				flag = true;
			}
		}
		if (flag)
		{
			bool flag2 = true;
			for (int j = NewItemTipsRootLogic.NewItemList.Count - 1; j > -1; j--)
			{
				GameItem gameItem2 = NewItemTipsRootLogic.NewItemList[j];
				if (gameItem2.ContainerType == newitem.ContainerType && gameItem2.ItemData.SubType == newitem.ItemData.SubType)
				{
					int num2 = -1;
					EquipData equipDataById3 = DataManager.GetEquipDataById(gameItem2.ItemId);
					if (equipDataById3 != null)
					{
						num2 = equipDataById3.Class;
					}
					if (@class > num2)
					{
						NewItemTipsRootLogic.NewItemList.RemoveAt(j);
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
			}
			if (flag2)
			{
				NewItemTipsRootLogic.NewItemList.Add(newitem);
			}
		}
		NewItemTipsRootLogic.ListisEmpty = (NewItemTipsRootLogic.NewItemList.Count <= 0);
		if (!NewItemTipsRootLogic.ListisEmpty && (!SingletonUnity<NewItemTipsRootLogic>.Exists || UnityVersionUtil.IsActive(SingletonUnity<NewItemTipsRootLogic>.Instance.gameObject)))
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewItemTipsRoot, null, null);
		}
	}

	// Token: 0x0600432E RID: 17198 RVA: 0x0014B948 File Offset: 0x00149B48
	public static void CloseNewItemTips()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewItemTipsRoot);
	}

	// Token: 0x0600432F RID: 17199 RVA: 0x0014B95C File Offset: 0x00149B5C
	private void OnEnable()
	{
		this.tempshowtime = 0f;
		this.temptime = 0f;
		NewItemTipsRootLogic.ListisEmpty = (NewItemTipsRootLogic.NewItemList.Count <= 0);
		this.isShowitemFlag = false;
		UnityVersionUtil.SetActiveRecursive(this.ShowObj, false);
	}

	// Token: 0x06004330 RID: 17200 RVA: 0x0014B9A8 File Offset: 0x00149BA8
	private bool CheckNeedShow()
	{
		return !SingletonUnity<DialogMissionUIRoot>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<DialogMissionUIRoot>.Instance.gameObject);
	}

	// Token: 0x06004331 RID: 17201 RVA: 0x0014B9CC File Offset: 0x00149BCC
	private void Update()
	{
		if (!NewItemTipsRootLogic.ListisEmpty && !this.isShowitemFlag)
		{
			this.temptime += Time.deltaTime;
			if (this.temptime > this.deltime_showitem)
			{
				this.temptime = 0f;
				this.ShowItemInfo();
			}
		}
		if (this.isShowitemFlag)
		{
			this.tempshowtime += Time.deltaTime;
			if (this.tempshowtime >= this.showtime)
			{
				this.NextItemShow();
			}
		}
	}

	// Token: 0x06004332 RID: 17202 RVA: 0x0014BA58 File Offset: 0x00149C58
	public void ShowItemInfo()
	{
		this.isShowitemFlag = true;
		this.curGameItem = NewItemTipsRootLogic.NewItemList[0];
		NewItemTipsRootLogic.NewItemList.RemoveAt(0);
		NewItemTipsRootLogic.ListisEmpty = (NewItemTipsRootLogic.NewItemList.Count <= 0);
		this.itemLogic.UpdateNewItemUI(this.curGameItem);
		this.curitemData = this.curGameItem.ItemData;
		this.nameLabel.text = this.curitemData.MName;
		this.nameLabel.color = GameDefine.GetColorByQuality(this.curGameItem.GetItemQuality());
		UnityVersionUtil.SetActiveRecursive(this.ShowObj, true);
		if (this.curitemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{100637}", new object[0]);
			EquipData equipDataById = DataManager.GetEquipDataById(this.curGameItem.ItemId);
			this.AppraisePrice = equipDataById.GetAppraisePrice(this.curGameItem.GetItemQuality());
			if (this.AppraisePrice != 0)
			{
				this.scoreLabel.text = GameMoneyHelper.GetMoneyValStr(this.AppraisePrice, GameDefine.MONEY_TYPE.GOLD);
			}
			else
			{
				this.scoreLabel.text = string.Empty;
			}
		}
		else if (this.curitemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{100614}", new object[0]);
			this.scoreLabel.text = string.Empty;
		}
	}

	// Token: 0x06004333 RID: 17203 RVA: 0x0014BBCC File Offset: 0x00149DCC
	public void OnClickPutonBtn()
	{
		this.OnClickEquipBtn();
		this.NextItemShow();
	}

	// Token: 0x06004334 RID: 17204 RVA: 0x0014BBDC File Offset: 0x00149DDC
	public void OnClickEquipBtn()
	{
		if (this.curGameItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			EquipData equipDataById = DataManager.GetEquipDataById(this.curGameItem.ItemId);
			if (GameMoneyHelper.GetMoneyNum(1) < (long)this.AppraisePrice)
			{
				NoticeLogic.AddNotifyData("#{100640}", true, false);
				return;
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(116, 0.5f))
			{
				Singleton<ObjManager>.Instance.MainPlayer.EquipItem(this.curGameItem, true);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100272}", true, false);
			}
		}
		else if (this.curGameItem.ItemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			if (SingletonDontDestoryUnity<GameManager>.Instance.CanSendToServer(221, 0.5f))
			{
				ItemContainer fashionEquipPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FashionEquipPack;
				Singleton<ObjManager>.Instance.MainPlayer.EquipFashionItem(this.curGameItem);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100272}", true, false);
			}
		}
	}

	// Token: 0x06004335 RID: 17205 RVA: 0x0014BCD8 File Offset: 0x00149ED8
	private bool CheckCanEquip()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (!playerData.CheckLevel(this.curGameItem.ItemData.Level))
		{
			NoticeLogic.AddNotifyData("#{100642}", true, false);
			return false;
		}
		return true;
	}

	// Token: 0x06004336 RID: 17206 RVA: 0x0014BD1C File Offset: 0x00149F1C
	public void NextItemShow()
	{
		this.tempshowtime = 0f;
		UnityVersionUtil.SetActiveRecursive(this.ShowObj, false);
		this.isShowitemFlag = false;
		if (NewItemTipsRootLogic.ListisEmpty)
		{
			NewItemTipsRootLogic.CloseNewItemTips();
		}
	}

	// Token: 0x06004337 RID: 17207 RVA: 0x0014BD4C File Offset: 0x00149F4C
	public void OnClickCloseBtn()
	{
		this.NextItemShow();
	}

	// Token: 0x04002FB3 RID: 12211
	private static bool ListisEmpty = true;

	// Token: 0x04002FB4 RID: 12212
	public static List<GameItem> NewItemList = new List<GameItem>();

	// Token: 0x04002FB5 RID: 12213
	public UILabel nameLabel;

	// Token: 0x04002FB6 RID: 12214
	public UILabel scoreLabel;

	// Token: 0x04002FB7 RID: 12215
	public ItemUILogic itemLogic;

	// Token: 0x04002FB8 RID: 12216
	public UILabel BtnLabel;

	// Token: 0x04002FB9 RID: 12217
	public GameObject ShowObj;

	// Token: 0x04002FBA RID: 12218
	private bool isShowitemFlag;

	// Token: 0x04002FBB RID: 12219
	private float temptime;

	// Token: 0x04002FBC RID: 12220
	private float deltime_showitem = 0.5f;

	// Token: 0x04002FBD RID: 12221
	private GameItem curGameItem;

	// Token: 0x04002FBE RID: 12222
	private ItemData curitemData;

	// Token: 0x04002FBF RID: 12223
	private float showtime = 60f;

	// Token: 0x04002FC0 RID: 12224
	private float tempshowtime;

	// Token: 0x04002FC1 RID: 12225
	private int AppraisePrice;
}
