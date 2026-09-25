using System;
using UnityEngine;

// Token: 0x020008DE RID: 2270
public class DayRewardItem : MonoBehaviour
{
	// Token: 0x06003D54 RID: 15700 RVA: 0x00111CFC File Offset: 0x0010FEFC
	public void ResetPos(int listindex)
	{
		this.ComBg.enabled = false;
		this.Comflag.enabled = false;
	}

	// Token: 0x06003D55 RID: 15701 RVA: 0x00111D18 File Offset: 0x0010FF18
	public void UpdataStateInfo(DayItemState itemstate, bool isRepget = false)
	{
		switch (itemstate)
		{
		case DayItemState.ISGET:
			this.ComBg.enabled = true;
			this.Comflag.enabled = true;
			if (isRepget)
			{
				this.ReplenishSp.enabled = true;
			}
			else
			{
				this.ReplenishSp.enabled = false;
			}
			break;
		case DayItemState.CURSIGN:
			this.ComBg.enabled = false;
			this.Comflag.enabled = false;
			this.ReplenishSp.enabled = false;
			break;
		case DayItemState.CAN_REPLENISH:
			this.ComBg.enabled = false;
			this.Comflag.enabled = false;
			this.ReplenishSp.enabled = true;
			break;
		case DayItemState.NONE:
			this.ComBg.enabled = true;
			this.Comflag.enabled = false;
			this.ReplenishSp.enabled = false;
			break;
		}
	}

	// Token: 0x06003D56 RID: 15702 RVA: 0x00111E00 File Offset: 0x00110000
	public void UpdateItem(string itemId, int count)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(itemId);
		this.curItemData = itemDataByID;
		this.item = new GameItem(itemId, itemDataByID.QualityType, count);
		if (itemDataByID != null)
		{
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				this.qualitySprite.spriteName = itemDataByID.QualityType.ToString();
				Debug.LogWarning("Sign 30 have equip item!");
			}
			else
			{
				this.qualitySprite.spriteName = itemDataByID.QualityType.ToString();
			}
			this.iconSprite.spriteName = itemDataByID.BackPackIcon;
			if (count > 1)
			{
				this.itemCountLabel.text = string.Format("x{0}", count);
			}
			else
			{
				this.itemCountLabel.text = string.Empty;
			}
		}
		else
		{
			NGUITools.SetActive(base.gameObject, false);
		}
	}

	// Token: 0x06003D57 RID: 15703 RVA: 0x00111EE0 File Offset: 0x001100E0
	public void OnClickShowItems()
	{
		if (this.curItemData == null)
		{
			return;
		}
		int level = 0;
		if (this.item.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.item.ItemData.SubType);
		}
		ItemInfoRootLogicNew.ShowItemTips(this.item, level, false, UI_PAGE_TYPE.INVALID);
	}

	// Token: 0x040028C8 RID: 10440
	public UISprite iconSprite;

	// Token: 0x040028C9 RID: 10441
	public UISprite qualitySprite;

	// Token: 0x040028CA RID: 10442
	public UILabel itemCountLabel;

	// Token: 0x040028CB RID: 10443
	private ItemData curItemData;

	// Token: 0x040028CC RID: 10444
	private GameItem item;

	// Token: 0x040028CD RID: 10445
	public UISprite ComBg;

	// Token: 0x040028CE RID: 10446
	public UISprite Comflag;

	// Token: 0x040028CF RID: 10447
	public UISprite ReplenishSp;
}
