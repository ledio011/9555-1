using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x0200010A RID: 266
public class ItemContainerTool
{
	// Token: 0x060009A8 RID: 2472 RVA: 0x00046448 File Offset: 0x00044648
	public static List<GameItem> GetConsignSellItem(ItemContainer Container)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < Container.ContainerSize; i++)
		{
			GameItem itemByIndex = Container.GetItemByIndex(i);
			if (!itemByIndex.IsEmpty())
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(itemByIndex.ItemId);
				if (!itemByIndex.BindFlag && itemDataByID.ConsignPrice > 0)
				{
					list.Add(itemByIndex);
				}
			}
		}
		return ItemContainerTool.SortItemList(list);
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x000464BC File Offset: 0x000446BC
	public static List<GameItem> GetTargetTypeItem(ItemContainer Container, bool IsAll, GameDefine.ITEM_TYPE TargetType = GameDefine.ITEM_TYPE.INVALID, bool isUnBind = false, PROFESSION_TYPE prof = PROFESSION_TYPE.INVALID)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < Container.ContainerSize; i++)
		{
			GameItem itemByIndex = Container.GetItemByIndex(i);
			if (!itemByIndex.IsEmpty())
			{
				if (!isUnBind || !itemByIndex.BindFlag)
				{
					if (IsAll)
					{
						list.Add(itemByIndex);
					}
					else
					{
						ItemData itemDataByID = DataManager.GetItemDataByID(itemByIndex.ItemId);
						if (itemDataByID.Type == TargetType)
						{
							if (prof == PROFESSION_TYPE.INVALID)
							{
								list.Add(itemByIndex);
							}
							else if (TargetType == GameDefine.ITEM_TYPE.EQUIP)
							{
								EquipData equipDataById = DataManager.GetEquipDataById(itemByIndex.ItemId);
								if (equipDataById.profession == prof)
								{
									list.Add(itemByIndex);
								}
							}
						}
					}
				}
			}
		}
		return ItemContainerTool.SortItemList(list);
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0004657C File Offset: 0x0004477C
	public static List<GameItem> GetTargetItemByID(ItemContainer Container, string needid)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < Container.ContainerSize; i++)
		{
			GameItem itemByIndex = Container.GetItemByIndex(i);
			if (!itemByIndex.IsEmpty() && itemByIndex.ItemId.Equals(needid))
			{
				list.Add(itemByIndex);
			}
		}
		return ItemContainerTool.SortItemList(list);
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x000465D8 File Offset: 0x000447D8
	public static List<GameItem> GetTargetTypeItemLevel(ItemContainer Container, GameDefine.ITEM_TYPE TargetType = GameDefine.ITEM_TYPE.INVALID, int level = 0)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < Container.ContainerSize; i++)
		{
			GameItem itemByIndex = Container.GetItemByIndex(i);
			if (!itemByIndex.IsEmpty())
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(itemByIndex.ItemId);
				if (itemDataByID.Type == TargetType && itemDataByID.Level <= level)
				{
					list.Add(itemByIndex);
				}
			}
		}
		return ItemContainerTool.SortItemList(list);
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x00046648 File Offset: 0x00044848
	public static List<GameItem> GetTargetPotionItemLevel(ItemContainer Container, int level = 0)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < Container.ContainerSize; i++)
		{
			GameItem itemByIndex = Container.GetItemByIndex(i);
			if (!itemByIndex.IsEmpty())
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(itemByIndex.ItemId);
				if ((itemDataByID.Type == GameDefine.ITEM_TYPE.POTION || itemDataByID.Type == GameDefine.ITEM_TYPE.POTION_2) && itemDataByID.Level <= level)
				{
					list.Add(itemByIndex);
				}
			}
		}
		return ItemContainerTool.SortItemList(list);
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x000466C4 File Offset: 0x000448C4
	public static bool isContainItem(List<GameItem> exclude, GameItem item)
	{
		if (exclude != null && exclude.Count > 0)
		{
			for (int i = 0; i < exclude.Count; i++)
			{
				if (exclude[i].IndexId == item.IndexId)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x00046714 File Offset: 0x00044914
	public static List<GameItem> GetSubItem(ItemContainer Container, GameDefine.ITEM_TYPE TargetType, int SubType, List<GameItem> exclude, PROFESSION_TYPE prof = PROFESSION_TYPE.INVALID)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < Container.ContainerSize; i++)
		{
			GameItem itemByIndex = Container.GetItemByIndex(i);
			if (!itemByIndex.IsEmpty())
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(itemByIndex.ItemId);
				if (itemDataByID.Type == TargetType && itemDataByID.SubType == SubType && !ItemContainerTool.isContainItem(exclude, itemByIndex))
				{
					if (prof == PROFESSION_TYPE.INVALID)
					{
						list.Add(itemByIndex);
					}
					else if (TargetType == GameDefine.ITEM_TYPE.EQUIP)
					{
						EquipData equipDataById = DataManager.GetEquipDataById(itemByIndex.ItemId);
						if (equipDataById.profession == prof)
						{
							list.Add(itemByIndex);
						}
					}
				}
			}
		}
		return ItemContainerTool.SortItemList(list);
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x000467C4 File Offset: 0x000449C4
	public static GameItem GetBestTargetEquipItem(ItemContainer Container, int SubType, PROFESSION_TYPE prof)
	{
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		GameItem gameItem = null;
		int num = int.MinValue;
		for (int i = 0; i < Container.ContainerSize; i++)
		{
			GameItem itemByIndex = Container.GetItemByIndex(i);
			if (!itemByIndex.IsEmpty())
			{
				ItemData itemData = itemByIndex.ItemData;
				if (itemData.Level <= level && itemData.Type == GameDefine.ITEM_TYPE.EQUIP && itemData.SubType == SubType)
				{
					EquipData equipDataById = DataManager.GetEquipDataById(itemData.ID);
					if (equipDataById.profession == prof && itemByIndex.GetItemCombatVal() > num)
					{
						num = itemByIndex.GetItemCombatVal();
						gameItem = itemByIndex;
					}
				}
			}
		}
		if (gameItem != null && !gameItem.IsEmpty())
		{
			return gameItem;
		}
		return null;
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00046894 File Offset: 0x00044A94
	public static List<GameItem> GetTargetTypeItem(ItemContainer Container, int SubType, PROFESSION_TYPE prof, GameItem curequip = null)
	{
		List<GameItem> list = new List<GameItem>();
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		for (int i = 0; i < Container.ContainerSize; i++)
		{
			GameItem itemByIndex = Container.GetItemByIndex(i);
			if (itemByIndex != null && !itemByIndex.IsEmpty())
			{
				ItemData itemData = itemByIndex.ItemData;
				if (itemData.Level <= level && itemData.Type == GameDefine.ITEM_TYPE.EQUIP && itemData.SubType == SubType)
				{
					EquipData equipDataById = DataManager.GetEquipDataById(itemData.ID);
					if (SubType == 0)
					{
						EquipData equipDataById2 = DataManager.GetEquipDataById(curequip.ItemId);
						if (equipDataById2.WeaponType == equipDataById.WeaponType)
						{
							list.Add(itemByIndex);
						}
					}
					else if (equipDataById.profession == prof)
					{
						list.Add(itemByIndex);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00046974 File Offset: 0x00044B74
	public static List<GameItem> GetOtherTypeItem(ItemContainer Container, GameDefine.ITEM_TYPE TargetType = GameDefine.ITEM_TYPE.INVALID, bool isUnBind = false)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < Container.ContainerSize; i++)
		{
			GameItem itemByIndex = Container.GetItemByIndex(i);
			if (!itemByIndex.IsEmpty())
			{
				if (!isUnBind || !itemByIndex.BindFlag)
				{
					ItemData itemDataByID = DataManager.GetItemDataByID(itemByIndex.ItemId);
					if (itemDataByID.Type != TargetType)
					{
						list.Add(itemByIndex);
					}
				}
			}
		}
		return ItemContainerTool.SortItemList(list);
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x000469EC File Offset: 0x00044BEC
	public static List<GameItem> SortItemList(List<GameItem> itemList)
	{
		itemList.Sort(delegate(GameItem block1, GameItem block2)
		{
			if (block1.IsEmpty() && !block2.IsEmpty())
			{
				return 1;
			}
			if (!block1.IsEmpty() && block2.IsEmpty())
			{
				return -1;
			}
			if (block1.IsEmpty() && block2.IsEmpty())
			{
				return 0;
			}
			if (block1.ItemData.ItemType > block2.ItemData.ItemType)
			{
				return 1;
			}
			if (block1.ItemData.ItemType < block2.ItemData.ItemType)
			{
				return -1;
			}
			if (block1.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				if (block1.ItemData.SubType > block2.ItemData.SubType)
				{
					return 1;
				}
				if (block1.ItemData.SubType < block2.ItemData.SubType)
				{
					return -1;
				}
				if (block1.ItemData.Level > block2.ItemData.Level)
				{
					return -1;
				}
				if (block1.ItemData.Level < block2.ItemData.Level)
				{
					return 1;
				}
				if (block1.GetItemQuality() > block2.GetItemQuality())
				{
					return -1;
				}
				if (block1.GetItemQuality() < block2.GetItemQuality())
				{
					return 1;
				}
				return block1.ItemId.CompareTo(block2.ItemId);
			}
			else
			{
				if (block1.ItemData.Level > block2.ItemData.Level)
				{
					return -1;
				}
				if (block1.ItemData.Level < block2.ItemData.Level)
				{
					return 1;
				}
				return block1.ItemId.CompareTo(block2.ItemId);
			}
		});
		return itemList;
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x00046A20 File Offset: 0x00044C20
	public static List<GameItem> SortBadgeItemList(List<GameItem> itemList)
	{
		itemList.Sort(delegate(GameItem block1, GameItem block2)
		{
			if (block1.IsEmpty() && !block2.IsEmpty())
			{
				return 1;
			}
			if (!block1.IsEmpty() && block2.IsEmpty())
			{
				return -1;
			}
			if (block1.IsEmpty() && block2.IsEmpty())
			{
				return 0;
			}
			BadgeData badgeDataById = DataManager.GetBadgeDataById(block1.ItemId);
			BadgeData badgeDataById2 = DataManager.GetBadgeDataById(block2.ItemId);
			if (badgeDataById.Color < badgeDataById2.Color)
			{
				return -1;
			}
			if (badgeDataById.Color > badgeDataById2.Color)
			{
				return 1;
			}
			if (badgeDataById.BadgeType < badgeDataById2.BadgeType)
			{
				return -1;
			}
			if (badgeDataById.BadgeType > badgeDataById2.BadgeType)
			{
				return 1;
			}
			if (badgeDataById.Lv < badgeDataById2.Lv)
			{
				return -1;
			}
			if (badgeDataById.Lv > badgeDataById2.Lv)
			{
				return 1;
			}
			return 0;
		});
		return itemList;
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x00046A54 File Offset: 0x00044C54
	public static List<GameItem> GetBadgeEquipItemList(List<GameItem> oldlist)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < ItemContainer.BADGE_EQUIPPACK_SIZE; i++)
		{
			list.Add(null);
		}
		for (int j = 0; j < oldlist.Count; j++)
		{
			if (!oldlist[j].IsEmpty())
			{
				int num = oldlist[j].Parm[0];
				if (num < list.Count)
				{
					list[num] = oldlist[j];
				}
			}
		}
		return list;
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x00046ADC File Offset: 0x00044CDC
	public static List<GameItem> GetFashionEquipItemList(List<GameItem> oldlist)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < ItemContainer.FASHION_EQUIPPACK_SIZE; i++)
		{
			list.Add(null);
		}
		for (int j = 0; j < oldlist.Count; j++)
		{
			if (!oldlist[j].IsEmpty())
			{
				ItemData itemData = oldlist[j].ItemData;
				int num = ItemContainerTool.ChangeFashionEquipTypeToIndex((EQUIP_BACKPACK_TYPE)itemData.SubType);
				if (num < list.Count)
				{
					list[num] = oldlist[j];
				}
			}
		}
		return list;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x00046B70 File Offset: 0x00044D70
	public static List<GameItem> GetEquipItemList(List<GameItem> oldlist)
	{
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < ItemContainer.EQUIPPACK_SIZE; i++)
		{
			list.Add(new GameItem());
			list[i].EquipType = ItemContainerTool.ChangeIndexToEquipType(i);
		}
		for (int j = 0; j < oldlist.Count; j++)
		{
			if (!oldlist[j].IsEmpty())
			{
				ItemData itemData = oldlist[j].ItemData;
				int num = ItemContainerTool.ChangeEquipTypeToIndex((EQUIP_BACKPACK_TYPE)itemData.SubType);
				if (num < list.Count)
				{
					list[num] = oldlist[j];
				}
			}
		}
		return list;
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x00046C1C File Offset: 0x00044E1C
	public static List<GameItem> GetEquipItemList(ItemContainer container)
	{
		List<GameItem> list = new List<GameItem>();
		List<ItemData> list2 = new List<ItemData>();
		for (int i = 0; i < container.ContainerSize; i++)
		{
			if (!container.GetItemByIndex(i).IsEmpty())
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(container.GetItemByIndex(i).ItemId);
				list2.Add(itemDataByID);
			}
			else
			{
				list2.Add(null);
			}
		}
		List<int> list3 = new List<int>();
		for (int j = 0; j < container.ContainerSize; j++)
		{
			list3.Clear();
			for (int k = 0; k < list2.Count; k++)
			{
				if (list2[k] != null && list2[k].SubType == (int)ItemContainerTool.ChangeIndexToEquipType(j))
				{
					list3.Add(k);
				}
			}
			if (list3.Count != 0)
			{
				for (int l = 0; l < list3.Count; l++)
				{
					list.Add(container.GetItemByIndex(list3[l]));
					list2[list3[l]] = null;
				}
			}
			else
			{
				list.Add(new GameItem());
				list[j].EquipType = ItemContainerTool.ChangeIndexToEquipType(j);
			}
		}
		return list;
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00046D68 File Offset: 0x00044F68
	public static List<GameItem> GetFashionEquipItemList(ItemContainer container)
	{
		List<GameItem> list = new List<GameItem>();
		List<ItemData> list2 = new List<ItemData>();
		for (int i = 0; i < container.ContainerSize; i++)
		{
			if (!container.GetItemByIndex(i).IsEmpty())
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(container.GetItemByIndex(i).ItemId);
				list2.Add(itemDataByID);
			}
			else
			{
				list2.Add(null);
			}
		}
		List<int> list3 = new List<int>();
		for (int j = 0; j < container.ContainerSize; j++)
		{
			list3.Clear();
			for (int k = 0; k < list2.Count; k++)
			{
				if (list2[k] != null && list2[k].SubType == (int)ItemContainerTool.ChangeIndexToFashionEquipType(j))
				{
					list3.Add(k);
				}
			}
			if (list3.Count != 0)
			{
				for (int l = 0; l < list3.Count; l++)
				{
					list.Add(container.GetItemByIndex(list3[l]));
					list2[list3[l]] = null;
				}
			}
			else
			{
				list.Add(new GameItem());
				list[j].EquipType = ItemContainerTool.ChangeIndexToFashionEquipType(j);
			}
		}
		return list;
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00046EB4 File Offset: 0x000450B4
	public static EQUIP_BACKPACK_TYPE ChangeIndexToFashionEquipType(int index)
	{
		switch (index)
		{
		case 0:
			return EQUIP_BACKPACK_TYPE.HEAD;
		case 1:
			return EQUIP_BACKPACK_TYPE.BODY;
		case 2:
			return EQUIP_BACKPACK_TYPE.WEAPON;
		case 3:
			return EQUIP_BACKPACK_TYPE.LEG;
		default:
			return EQUIP_BACKPACK_TYPE.COUNT;
		}
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00046EE8 File Offset: 0x000450E8
	public static int ChangeFashionEquipTypeToIndex(EQUIP_BACKPACK_TYPE index)
	{
		switch (index)
		{
		case EQUIP_BACKPACK_TYPE.WEAPON:
			return 2;
		case EQUIP_BACKPACK_TYPE.HEAD:
			return 0;
		case EQUIP_BACKPACK_TYPE.BODY:
			return 1;
		case EQUIP_BACKPACK_TYPE.LEG:
			return 3;
		default:
			return 0;
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x00046F1C File Offset: 0x0004511C
	public static EQUIP_BACKPACK_TYPE ChangeIndexToEquipType(int index)
	{
		switch (index)
		{
		case 0:
			return EQUIP_BACKPACK_TYPE.HEAD;
		case 1:
			return EQUIP_BACKPACK_TYPE.BODY;
		case 2:
			return EQUIP_BACKPACK_TYPE.BELT;
		case 3:
			return EQUIP_BACKPACK_TYPE.LEG;
		case 4:
			return EQUIP_BACKPACK_TYPE.NECKLACE;
		case 5:
			return EQUIP_BACKPACK_TYPE.WEAPON;
		default:
			return EQUIP_BACKPACK_TYPE.COUNT;
		}
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x00046F5C File Offset: 0x0004515C
	public static int ChangeEquipTypeToIndex(EQUIP_BACKPACK_TYPE index)
	{
		switch (index)
		{
		case EQUIP_BACKPACK_TYPE.WEAPON:
			return 5;
		case EQUIP_BACKPACK_TYPE.HEAD:
			return 0;
		case EQUIP_BACKPACK_TYPE.BODY:
			return 1;
		case EQUIP_BACKPACK_TYPE.LEG:
			return 3;
		case EQUIP_BACKPACK_TYPE.BELT:
			return 2;
		case EQUIP_BACKPACK_TYPE.NECKLACE:
			return 4;
		default:
			return 0;
		}
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00046F9C File Offset: 0x0004519C
	public static GameItem ChangeNetItemToGameItem(gameitem netItem)
	{
		GameItem gameItem = new GameItem();
		gameItem.ItemId = netItem.itemId;
		if (netItem.HasParm)
		{
			gameItem.SetParm(netItem.parm);
		}
		if (netItem.HasBindflag)
		{
			gameItem.BindFlag = netItem.bindflag;
		}
		else
		{
			gameItem.BindFlag = false;
		}
		if (netItem.HasStack)
		{
			gameItem.StackNum = (int)netItem.stack;
		}
		else
		{
			gameItem.StackNum = 1;
		}
		if (netItem.HasIndexId)
		{
			gameItem.IndexId = netItem.indexId;
		}
		else
		{
			gameItem.IndexId = -1L;
		}
		if (netItem.HasQuality)
		{
			gameItem.Quality = (EQUIP_QUALITY)netItem.quality;
		}
		else
		{
			gameItem.Quality = EQUIP_QUALITY.INVALID;
		}
		if (netItem.HasLevel)
		{
			gameItem.ItemLevel = (int)netItem.level;
		}
		else
		{
			gameItem.ItemLevel = 0;
		}
		if (netItem.HasAppraise)
		{
			gameItem.Appraise = (int)netItem.appraise;
		}
		else
		{
			gameItem.Appraise = 0;
		}
		if (netItem.HasRandom_attri)
		{
			gameItem.Random_AttriDic = netItem.random_attri;
		}
		else
		{
			gameItem.Random_AttriDic = null;
		}
		if (netItem.HasInlay)
		{
			gameItem.InlayDic = netItem.inlay;
		}
		else
		{
			gameItem.InlayDic = null;
		}
		return gameItem;
	}
}
