using System;
using SprotoType;
using UnityEngine;

// Token: 0x020008DD RID: 2269
public class DailyRewardNewItem : MonoBehaviour
{
	// Token: 0x06003D4F RID: 15695 RVA: 0x001119D8 File Offset: 0x0010FBD8
	public void UpdateInfo(daily_reward curinfo)
	{
		this.curInfo = curinfo;
		this.curData = DataManager.GetDailyActiveRewardDataById(curinfo.ID);
		this.TargetLabel.text = string.Empty + this.curData.Score;
		long state = curinfo.state;
		if (state >= 0L && state <= 2L)
		{
			switch ((int)state)
			{
			case 0:
				NGUITools.SetActive(this.ComFlagObj, false);
				NGUITools.SetActive(this.TargetLabel.gameObject, true);
				NGUITools.SetActive(this.CanGetObj, false);
				break;
			case 1:
				NGUITools.SetActive(this.ComFlagObj, false);
				NGUITools.SetActive(this.TargetLabel.gameObject, true);
				NGUITools.SetActive(this.CanGetObj, true);
				break;
			case 2:
				NGUITools.SetActive(this.ComFlagObj, true);
				NGUITools.SetActive(this.TargetLabel.gameObject, false);
				NGUITools.SetActive(this.CanGetObj, false);
				break;
			}
		}
		this.curItemData = DataManager.GetItemDataByID(this.curData.ItemID);
		GameItem curitem = new GameItem(this.curData.ItemID, this.curItemData.QualityType, this.curData.ItemCount);
		this.UpdateItem(curitem);
	}

	// Token: 0x06003D50 RID: 15696 RVA: 0x00111B20 File Offset: 0x0010FD20
	public void UpdateItem(GameItem curitem)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(curitem.ItemId);
		this.curItemData = itemDataByID;
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
	}

	// Token: 0x06003D51 RID: 15697 RVA: 0x00111BF0 File Offset: 0x0010FDF0
	public void OnClickGetBtn()
	{
		if (this.curInfo.state == 1L)
		{
			WaitResponseUIRootLogic.OpenWaitBox(265, 10f, 0f, null);
			require_daily_active_reward.request request = new require_daily_active_reward.request();
			request.ID = this.curInfo.ID;
			NetLogic.GetInstance().Send<Protocol.require_daily_active_reward>(request, null);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OpenBoxRoot, delegate
			{
				SingletonUnity<OpenBoxRootLogic>.Instance.Reset();
			}, null);
		}
		else
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(this.curData.ItemID);
			GameItem gameItem = new GameItem(this.curData.ItemID, itemDataByID.QualityType, this.curData.ItemCount);
			int level = 0;
			if (gameItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
			{
				level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(gameItem.ItemData.SubType);
			}
			ItemInfoRootLogicNew.ShowItemTips(gameItem, level, false, UI_PAGE_TYPE.INVALID);
		}
	}

	// Token: 0x040028BE RID: 10430
	public UISprite iconSprite;

	// Token: 0x040028BF RID: 10431
	public UISprite qualitySprite;

	// Token: 0x040028C0 RID: 10432
	public UILabel itemCountLabel;

	// Token: 0x040028C1 RID: 10433
	private ItemData curItemData;

	// Token: 0x040028C2 RID: 10434
	public UILabel TargetLabel;

	// Token: 0x040028C3 RID: 10435
	private daily_reward curInfo;

	// Token: 0x040028C4 RID: 10436
	private DailyActiveRewardData curData;

	// Token: 0x040028C5 RID: 10437
	public GameObject ComFlagObj;

	// Token: 0x040028C6 RID: 10438
	public GameObject CanGetObj;
}
