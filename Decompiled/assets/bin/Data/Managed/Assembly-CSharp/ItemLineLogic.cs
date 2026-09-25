using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000949 RID: 2377
public class ItemLineLogic : MonoBehaviour
{
	// Token: 0x06004280 RID: 17024 RVA: 0x001445A0 File Offset: 0x001427A0
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06004281 RID: 17025 RVA: 0x001445A8 File Offset: 0x001427A8
	public void Init()
	{
		if (!this.InitFlag)
		{
			this.InitFlag = true;
			for (int i = 0; i < this.ItemObjList.Count; i++)
			{
				ItemUILogic itemUILogic = this.ItemObjList[i];
				itemUILogic.onClickItem = (ItemUILogic.OnClickItemDelegate)Delegate.Combine(itemUILogic.onClickItem, new ItemUILogic.OnClickItemDelegate(this.OnClickItem));
			}
		}
	}

	// Token: 0x06004282 RID: 17026 RVA: 0x00144610 File Offset: 0x00142810
	private void OnClickItem(GameItem curItem, ItemUILogic curUIItem)
	{
		if (this.onClickItem != null)
		{
			this.onClickItem(curItem, curUIItem);
		}
	}

	// Token: 0x06004283 RID: 17027 RVA: 0x0014462C File Offset: 0x0014282C
	public void Reset(List<GameItem> itemList, bool needShowEmpty, int curLineIndex, int startIndex, int maxIndex)
	{
		PlayerModelPageRootLogic instance = SingletonUnity<PlayerModelPageRootLogic>.Instance;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.CurIndex = curLineIndex;
		for (int i = 0; i < this.ItemObjList.Count; i++)
		{
			if (itemList[i] != null)
			{
				if (itemList[i].IsEmpty() && !needShowEmpty)
				{
					NGUITools.SetActive(this.ItemObjList[i].gameObject, false);
				}
				else
				{
					NGUITools.SetActive(this.ItemObjList[i].gameObject, true);
					if (startIndex + i < maxIndex)
					{
						if (instance != null && itemList[i].ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
						{
							EquipData equipDataById = DataManager.GetEquipDataById(itemList[i].ItemId);
							if (equipDataById.profession == playerData.Profession)
							{
								this.ItemObjList[i].UpdateItemUI(itemList[i], itemList[i].GetItemCombatVal() > instance.GetTargetTypeEquipCombatVal((EQUIP_BACKPACK_TYPE)itemList[i].ItemData.SubType));
							}
							else
							{
								this.ItemObjList[i].UpdateItemUI(itemList[i], false);
							}
						}
						else
						{
							this.ItemObjList[i].UpdateItemUI(itemList[i], false);
						}
					}
					else
					{
						this.ItemObjList[i].SetItemLock();
					}
				}
			}
			else if (needShowEmpty)
			{
				NGUITools.SetActive(this.ItemObjList[i].gameObject, true);
				if (startIndex + i < maxIndex)
				{
					this.ItemObjList[i].SetItemEmpty(ITEM_CONTAINER_TYPE.ITEM_BACKPACK, EQUIP_BACKPACK_TYPE.COUNT);
				}
				else
				{
					this.ItemObjList[i].SetItemLock();
				}
			}
			else
			{
				NGUITools.SetActive(this.ItemObjList[i].gameObject, false);
			}
		}
	}

	// Token: 0x06004284 RID: 17028 RVA: 0x00144818 File Offset: 0x00142A18
	[ContextMenu("ResetItemLine")]
	public void ResetPosition()
	{
		if (this.ItemObjList.Count == 0)
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				GameObject gameObject = base.transform.GetChild(i).gameObject;
				this.ItemObjList.Add(gameObject.GetComponent<ItemUILogic>());
			}
		}
		int count = this.ItemObjList.Count;
		int num;
		if (count % 2 == 0)
		{
			num = -(count / 2 - 1) * this.CellWidth - this.CellWidth / 2;
		}
		else
		{
			num = -((count + 1) / 2 - 1) * this.CellWidth;
		}
		for (int j = 0; j < this.ItemObjList.Count; j++)
		{
			this.ItemObjList[j].transform.localPosition = new Vector3((float)(num + j * this.CellWidth), 0f, 0f);
		}
	}

	// Token: 0x04002E88 RID: 11912
	public ItemUILogic.OnClickItemDelegate onClickItem;

	// Token: 0x04002E89 RID: 11913
	public List<ItemUILogic> ItemObjList = new List<ItemUILogic>();

	// Token: 0x04002E8A RID: 11914
	public int CellWidth;

	// Token: 0x04002E8B RID: 11915
	public int CellHeight;

	// Token: 0x04002E8C RID: 11916
	public int CurIndex;

	// Token: 0x04002E8D RID: 11917
	private bool InitFlag;
}
