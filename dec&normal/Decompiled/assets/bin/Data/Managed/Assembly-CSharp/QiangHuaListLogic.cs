using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200090E RID: 2318
public class QiangHuaListLogic : MonoBehaviour
{
	// Token: 0x06003FAC RID: 16300 RVA: 0x0012926C File Offset: 0x0012746C
	public void Init()
	{
		for (int i = 0; i < this.EquipList.Count; i++)
		{
			EquipItemLogic equipItemLogic = this.EquipList[i];
			equipItemLogic.OnClick = (EquipItemLogic.OnClickItem)Delegate.Combine(equipItemLogic.OnClick, new EquipItemLogic.OnClickItem(this.OnClickEquipItem));
		}
	}

	// Token: 0x06003FAD RID: 16301 RVA: 0x001292C4 File Offset: 0x001274C4
	public void UpdateEquipInfo(GameItem gameItem)
	{
		for (int i = 0; i < this.EquipList.Count; i++)
		{
			if (this.EquipList[i].mCurItem != null && !this.EquipList[i].mCurItem.IsEmpty() && gameItem != null && !gameItem.IsEmpty() && this.EquipList[i].mCurItem.ItemData.SubType == gameItem.ItemData.SubType)
			{
				this.EquipList[i].UpdateInfo(gameItem);
				break;
			}
		}
	}

	// Token: 0x06003FAE RID: 16302 RVA: 0x00129374 File Offset: 0x00127574
	public void OnClickEquipItem(GameItem item)
	{
		if (this.onClickEquipItem != null)
		{
			this.onClickEquipItem(item);
		}
	}

	// Token: 0x06003FAF RID: 16303 RVA: 0x00129390 File Offset: 0x00127590
	public void ResetEuipListInfo(List<GameItem> list, GameItem item, bool resetpos)
	{
		if (item == null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				EquipData equipDataById = DataManager.GetEquipDataById(list[i].ItemId);
				if (equipDataById != null && equipDataById.EquipType == EQUIP_BACKPACK_TYPE.WEAPON)
				{
					item = list[i];
					break;
				}
			}
		}
		if (item != null)
		{
			int num = 0;
			for (int j = 0; j < list.Count; j++)
			{
				if (!list[j].IsEmpty())
				{
					this.EquipList[num].InitEuipInfo(list[j], list[j] == item);
					num++;
				}
			}
			for (int k = num; k < this.EquipList.Count; k++)
			{
				this.EquipList[k].ResetInfo();
			}
			if (resetpos)
			{
				this.ScrollView.ResetPosition();
			}
			this.grid.Reposition();
		}
	}

	// Token: 0x04002B88 RID: 11144
	public List<EquipItemLogic> EquipList = new List<EquipItemLogic>();

	// Token: 0x04002B89 RID: 11145
	public UIGrid grid;

	// Token: 0x04002B8A RID: 11146
	public UIScrollView ScrollView;

	// Token: 0x04002B8B RID: 11147
	public EquipItemLogic.OnClickItem onClickEquipItem;
}
