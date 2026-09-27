using System;
using UnityEngine;

// Token: 0x020008EC RID: 2284
public class WeekDayRewardItem : MonoBehaviour
{
	// Token: 0x06003DD4 RID: 15828 RVA: 0x0011743C File Offset: 0x0011563C
	public void UpdataStateInfo(DayItemState itemstate)
	{
		switch (itemstate)
		{
		case DayItemState.ISGET:
			UnityVersionUtil.SetActiveRecursive(this.Comflag.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.GetSpObj, false);
			break;
		case DayItemState.CURSIGN:
			UnityVersionUtil.SetActiveRecursive(this.Comflag.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.GetSpObj, true);
			break;
		case DayItemState.CAN_REPLENISH:
			UnityVersionUtil.SetActiveRecursive(this.Comflag.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.GetSpObj, false);
			break;
		case DayItemState.NONE:
			UnityVersionUtil.SetActiveRecursive(this.Comflag.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.GetSpObj, false);
			break;
		}
	}

	// Token: 0x06003DD5 RID: 15829 RVA: 0x001174F0 File Offset: 0x001156F0
	public void UpdateItem(GameItem curitem, int dayi, DelegateDefine.OneIntParamDelegate clickbtn)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(curitem.ItemId);
		this.curItemData = itemDataByID;
		this.item = curitem;
		this.clickFun = clickbtn;
		this.curDay = dayi;
		if (itemDataByID != null)
		{
			if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				this.qualitySprite.spriteName = curitem.GetItemQuality().ToString();
			}
			else
			{
				this.qualitySprite.spriteName = itemDataByID.QualityType.ToString();
			}
			this.iconSprite.spriteName = itemDataByID.BackPackIcon;
			if (curitem.StackNum > 1)
			{
				this.itemCountLabel.text = string.Format("x{0}", curitem.StackNum);
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
		this.DayLabel.text = StrDictionary.GetDictionaryString("#{300901}", new object[]
		{
			dayi
		});
	}

	// Token: 0x06003DD6 RID: 15830 RVA: 0x001175FC File Offset: 0x001157FC
	public void OnClickDayItem()
	{
		if (this.clickFun != null)
		{
			this.clickFun(this.curDay);
		}
	}

	// Token: 0x0400296D RID: 10605
	public UISprite iconSprite;

	// Token: 0x0400296E RID: 10606
	public UISprite qualitySprite;

	// Token: 0x0400296F RID: 10607
	public UILabel itemCountLabel;

	// Token: 0x04002970 RID: 10608
	private ItemData curItemData;

	// Token: 0x04002971 RID: 10609
	private GameItem item;

	// Token: 0x04002972 RID: 10610
	public UISprite Comflag;

	// Token: 0x04002973 RID: 10611
	public UILabel DayLabel;

	// Token: 0x04002974 RID: 10612
	public GameObject GetSpObj;

	// Token: 0x04002975 RID: 10613
	private DelegateDefine.OneIntParamDelegate clickFun;

	// Token: 0x04002976 RID: 10614
	private int curDay;
}
