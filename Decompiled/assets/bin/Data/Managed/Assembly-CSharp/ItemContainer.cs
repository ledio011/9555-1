using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class ItemContainer
{
	// Token: 0x06000986 RID: 2438 RVA: 0x0004570C File Offset: 0x0004390C
	public ItemContainer(int size, ITEM_CONTAINER_TYPE type)
	{
		this.mContainerSize = size;
		this.mContainerType = type;
		for (int i = 0; i < this.mContainerSize; i++)
		{
			this.mItemList.Add(new GameItem());
			this.mItemList[i].ContainerType = this.mContainerType;
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00045780 File Offset: 0x00043980
	public ItemContainer(List<GameItem> list, ITEM_CONTAINER_TYPE type)
	{
		if (list != null)
		{
			this.mContainerSize = list.Count;
		}
		else
		{
			this.mContainerSize = 0;
		}
		this.mContainerType = type;
		this.mItemList = list;
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x06000989 RID: 2441 RVA: 0x00045824 File Offset: 0x00043A24
	public List<GameItem> ItemList
	{
		get
		{
			return this.mItemList;
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x0600098A RID: 2442 RVA: 0x0004582C File Offset: 0x00043A2C
	public int ContainerSize
	{
		get
		{
			return this.mContainerSize;
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x0600098B RID: 2443 RVA: 0x00045834 File Offset: 0x00043A34
	// (set) Token: 0x0600098C RID: 2444 RVA: 0x0004583C File Offset: 0x00043A3C
	public ITEM_CONTAINER_TYPE ContainType
	{
		get
		{
			return this.mContainerType;
		}
		set
		{
			this.mContainerType = value;
		}
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00045848 File Offset: 0x00043A48
	public void AddContainerSize(int addNum)
	{
		this.mContainerSize += addNum;
		for (int i = 0; i < addNum; i++)
		{
			this.mItemList.Add(new GameItem());
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00045888 File Offset: 0x00043A88
	public List<GameItem> GetItemByItemId(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return null;
		}
		List<GameItem> list = new List<GameItem>();
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i].ItemId.Equals(id))
			{
				list.Add(this.mItemList[i]);
			}
		}
		return list;
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x000458F4 File Offset: 0x00043AF4
	public GameItem GetItemByItemId2(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return null;
		}
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i].ItemId.Equals(id))
			{
				return this.mItemList[i];
			}
		}
		return null;
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x00045954 File Offset: 0x00043B54
	public GameItem GetItemByIndex(int index)
	{
		if (index >= 0 && index < this.mItemList.Count)
		{
			return this.mItemList[index];
		}
		return null;
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x00045988 File Offset: 0x00043B88
	public GameItem GetItemByIndexId(long indexId)
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i].IndexId == indexId)
			{
				return this.mItemList[i];
			}
		}
		return this.GetItemByIndex(this.GetFirstEmptyItemIndex());
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x000459E4 File Offset: 0x00043BE4
	public GameItem GetItemNoEmptyByIndexId(long indexId)
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i].IndexId == indexId)
			{
				return this.mItemList[i];
			}
		}
		return null;
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00045A34 File Offset: 0x00043C34
	public int GetItemCount()
	{
		int num = 0;
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (!this.mItemList[i].IsEmpty())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00045A7C File Offset: 0x00043C7C
	public int GetContainerEmptyNum()
	{
		return this.mContainerSize - this.GetItemCount();
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x00045A8C File Offset: 0x00043C8C
	public bool IsFull()
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i].IsEmpty())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x00045AD0 File Offset: 0x00043CD0
	public int GetItemStackNumById(string id)
	{
		int num = 0;
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i].ItemId.Equals(id))
			{
				num += this.mItemList[i].StackNum;
			}
		}
		return num;
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x00045B2C File Offset: 0x00043D2C
	public EquipData GetEquipWeaponData(EQUIP_BACKPACK_TYPE target)
	{
		EquipData result = null;
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i] != null && !string.IsNullOrEmpty(this.mItemList[i].ItemId))
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(this.mItemList[i].ItemId);
				if (itemDataByID != null && (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP || itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP) && itemDataByID.SubType == (int)target)
				{
					return DataManager.GetEquipDataById(itemDataByID.ID);
				}
			}
		}
		return result;
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x00045BD4 File Offset: 0x00043DD4
	public string GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE target, PROFESSION_TYPE profession, bool isFashion = false)
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i] != null && !string.IsNullOrEmpty(this.mItemList[i].ItemId))
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(this.mItemList[i].ItemId);
				if (itemDataByID != null && (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP || itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP) && itemDataByID.SubType == (int)target)
				{
					EquipData equipDataById = DataManager.GetEquipDataById(itemDataByID.ID);
					return equipDataById.ModelId;
				}
			}
		}
		if (isFashion)
		{
			return string.Empty;
		}
		if (profession == PROFESSION_TYPE.XD)
		{
			switch (target)
			{
			case EQUIP_BACKPACK_TYPE.WEAPON:
				return GameDefine.XD_DefaultModel[0];
			case EQUIP_BACKPACK_TYPE.HEAD:
				return GameDefine.XD_DefaultModel[1];
			case EQUIP_BACKPACK_TYPE.BODY:
				return GameDefine.XD_DefaultModel[2];
			case EQUIP_BACKPACK_TYPE.LEG:
				return GameDefine.XD_DefaultModel[3];
			default:
				return string.Empty;
			}
		}
		else if (profession == PROFESSION_TYPE.QJ)
		{
			switch (target)
			{
			case EQUIP_BACKPACK_TYPE.WEAPON:
				return GameDefine.QJ_DefaultModel[0];
			case EQUIP_BACKPACK_TYPE.HEAD:
				return GameDefine.QJ_DefaultModel[1];
			case EQUIP_BACKPACK_TYPE.BODY:
				return GameDefine.QJ_DefaultModel[2];
			case EQUIP_BACKPACK_TYPE.LEG:
				return GameDefine.QJ_DefaultModel[3];
			default:
				return string.Empty;
			}
		}
		else
		{
			switch (target)
			{
			case EQUIP_BACKPACK_TYPE.WEAPON:
				return GameDefine.NQS_DefaultModel[0];
			case EQUIP_BACKPACK_TYPE.HEAD:
				return GameDefine.NQS_DefaultModel[1];
			case EQUIP_BACKPACK_TYPE.BODY:
				return GameDefine.NQS_DefaultModel[2];
			case EQUIP_BACKPACK_TYPE.LEG:
				return GameDefine.NQS_DefaultModel[3];
			default:
				return string.Empty;
			}
		}
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x00045D60 File Offset: 0x00043F60
	public GameItem GetEnhanceItem()
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i] != null)
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(this.mItemList[i].ItemId);
				if (itemDataByID != null && itemDataByID.Type == GameDefine.ITEM_TYPE.ENHANCE_ITEM && itemDataByID.ID == "3001")
				{
					return this.mItemList[i];
				}
			}
		}
		return null;
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00045DE8 File Offset: 0x00043FE8
	public bool IsHaveWeapon()
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i] != null)
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(this.mItemList[i].ItemId);
				if (itemDataByID != null && itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP && itemDataByID.SubType == 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00045E5C File Offset: 0x0004405C
	public GameItem getWeapon()
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i] != null)
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(this.mItemList[i].ItemId);
				if (itemDataByID != null && itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP && itemDataByID.SubType == 0)
				{
					return this.mItemList[i];
				}
			}
		}
		return null;
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00045ED8 File Offset: 0x000440D8
	public GameItem GetEquipByEquipType(EQUIP_BACKPACK_TYPE targetType)
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i] != null)
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(this.mItemList[i].ItemId);
				if (itemDataByID != null && itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP && itemDataByID.SubType == (int)targetType)
				{
					return this.mItemList[i];
				}
			}
		}
		return null;
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x00045F58 File Offset: 0x00044158
	public int GetFirstEmptyItemIndex()
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i].IsEmpty())
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x00045F9C File Offset: 0x0004419C
	public GameItem GetFirstNoEmptyItem()
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			if (this.mItemList[i] != null)
			{
				ItemData itemDataByID = DataManager.GetItemDataByID(this.mItemList[i].ItemId);
				if (itemDataByID != null && itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					return this.mItemList[i];
				}
			}
		}
		return null;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x00046010 File Offset: 0x00044210
	public bool AddItem(GameItem gameItem)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(gameItem.ItemId);
		if (itemDataByID == null)
		{
			Debug.Log("itemData == null");
			return false;
		}
		if (!this.IsContainerHaveEnoughSize(gameItem))
		{
			Debug.Log("No Space For Item");
			return false;
		}
		int firstEmptyItemIndex;
		if (itemDataByID.Stack > 1)
		{
			List<GameItem> itemByItemId = this.GetItemByItemId(itemDataByID.ID);
			if (itemByItemId != null && itemByItemId.Count > 0)
			{
				for (int i = 0; i < itemByItemId.Count; i++)
				{
					if (!itemByItemId[i].IsFull())
					{
						if (itemByItemId[i].GetItemLeftSpace() >= gameItem.StackNum)
						{
							itemByItemId[i].StackNum += gameItem.StackNum;
							return true;
						}
						gameItem.StackNum -= itemByItemId[i].GetItemLeftSpace();
						itemByItemId[i].StackNum += itemByItemId[i].GetItemLeftSpace();
					}
				}
			}
			while (gameItem.StackNum > 0)
			{
				firstEmptyItemIndex = this.GetFirstEmptyItemIndex();
				if (firstEmptyItemIndex == -1)
				{
					Debug.Log("Container Is Full");
					return false;
				}
				if (gameItem.StackNum <= itemDataByID.Stack)
				{
					this.mItemList[firstEmptyItemIndex] = gameItem;
					this.mItemList[firstEmptyItemIndex].ItemId = gameItem.ItemId;
					this.mItemList[firstEmptyItemIndex].StackNum = gameItem.StackNum;
					return true;
				}
				this.mItemList[firstEmptyItemIndex].ItemId = gameItem.ItemId;
				this.mItemList[firstEmptyItemIndex].StackNum = itemDataByID.Stack;
				gameItem.StackNum -= itemDataByID.Stack;
			}
			Debug.Log("Container Is Full");
			return false;
		}
		firstEmptyItemIndex = this.GetFirstEmptyItemIndex();
		this.mItemList[firstEmptyItemIndex].SetItem(gameItem);
		return true;
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x000461F4 File Offset: 0x000443F4
	public void OtherPlayerAddItem(GameItem gameItem)
	{
		if (DataManager.GetItemDataByID(gameItem.ItemId) == null)
		{
			return;
		}
		if (!this.IsContainerHaveEnoughSize(gameItem))
		{
			return;
		}
		int firstEmptyItemIndex = this.GetFirstEmptyItemIndex();
		this.mItemList[firstEmptyItemIndex] = gameItem;
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x00046238 File Offset: 0x00044438
	public bool IsContainerHaveEnoughSize(GameItem gameItem)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(gameItem.ItemId);
		if (itemDataByID == null)
		{
			Debug.Log("itemData == null");
			return false;
		}
		if (itemDataByID.Stack <= 1)
		{
			return this.GetFirstEmptyItemIndex() != -1;
		}
		if (gameItem.StackNum <= this.GetContainerEmptyNum() * itemDataByID.Stack)
		{
			return true;
		}
		int num = gameItem.StackNum - this.GetContainerEmptyNum() * itemDataByID.Stack;
		List<GameItem> itemByItemId = this.GetItemByItemId(itemDataByID.ID);
		for (int i = 0; i < itemByItemId.Count; i++)
		{
			num -= itemByItemId[i].GetItemLeftSpace();
			if (num <= 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x000462EC File Offset: 0x000444EC
	public bool RemoveItem(int index)
	{
		if (!this.mItemList[index].IsEmpty())
		{
			this.mItemList[index].Reset();
			return true;
		}
		return false;
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x00046324 File Offset: 0x00044524
	public bool RemoveItem(GameItem item)
	{
		int index = this.mItemList.IndexOf(item);
		return this.RemoveItem(index);
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00046348 File Offset: 0x00044548
	public void ClearContainer()
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			this.mItemList[i].Reset();
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00046384 File Offset: 0x00044584
	public void PrintContainer()
	{
		for (int i = 0; i < this.mItemList.Count; i++)
		{
			Debug.Log("============" + i);
			this.PrintItem(this.mItemList[i]);
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x000463D4 File Offset: 0x000445D4
	public void PrintItem(GameItem item)
	{
		if (item.IsEmpty())
		{
			Debug.Log("Empty");
		}
		else
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(item.ItemId);
			Debug.Log(string.Concat(new object[]
			{
				"ItemName : ",
				itemDataByID.Name,
				"  :: ItemNum : ",
				item.StackNum
			}));
		}
	}

	// Token: 0x0400086C RID: 2156
	public static int ITEM_BACKPACK_SIZE = 100;

	// Token: 0x0400086D RID: 2157
	public static int BACKPACK_MAXSIZE = 100;

	// Token: 0x0400086E RID: 2158
	public static int EQUIPPACK_SIZE = 6;

	// Token: 0x0400086F RID: 2159
	public static int STORAGEPACK_MAXSIZE = 100;

	// Token: 0x04000870 RID: 2160
	public static int STORAGEPACK_SIZE = 100;

	// Token: 0x04000871 RID: 2161
	public static int EQUIP_BACKPACK_SIZE = 100;

	// Token: 0x04000872 RID: 2162
	public static int BADGE_BACKPACK_SIZE = 100;

	// Token: 0x04000873 RID: 2163
	public static int BADGE_EQUIPPACK_SIZE = 5;

	// Token: 0x04000874 RID: 2164
	public static int FASHION_BACKPACK_SIZE = 100;

	// Token: 0x04000875 RID: 2165
	public static int FASHION_EQUIPPACK_SIZE = 4;

	// Token: 0x04000876 RID: 2166
	private List<GameItem> mItemList = new List<GameItem>();

	// Token: 0x04000877 RID: 2167
	private int mContainerSize;

	// Token: 0x04000878 RID: 2168
	private ITEM_CONTAINER_TYPE mContainerType = ITEM_CONTAINER_TYPE.INVALID;
}
