using System;
using SprotoType;
using UnityEngine;

// Token: 0x020008D9 RID: 2265
public class DailyActiveRewardItem : MonoBehaviour
{
	// Token: 0x06003D34 RID: 15668 RVA: 0x0011027C File Offset: 0x0010E47C
	public void UpdateInfo(daily_reward curinfo)
	{
		this.curInfo = curinfo;
		this.curData = DataManager.GetDailyActiveRewardDataById(curinfo.ID);
		if (this.curData == null)
		{
			return;
		}
		this.TargetLabel.text = string.Empty + this.curData.Score;
		long state = curinfo.state;
		if (state >= 0L && state <= 2L)
		{
			switch ((int)state)
			{
			case 0:
				UnityVersionUtil.SetActiveRecursive(this.ComFlagObj, false);
				UnityVersionUtil.SetActiveRecursive(this.TargetLabel.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.CanGetObj, false);
				this.BgSp.enabled = true;
				break;
			case 1:
				UnityVersionUtil.SetActiveRecursive(this.ComFlagObj, false);
				UnityVersionUtil.SetActiveRecursive(this.TargetLabel.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.CanGetObj, true);
				this.BgSp.enabled = false;
				break;
			case 2:
				UnityVersionUtil.SetActiveRecursive(this.ComFlagObj, true);
				UnityVersionUtil.SetActiveRecursive(this.TargetLabel.gameObject, false);
				UnityVersionUtil.SetActiveRecursive(this.CanGetObj, false);
				this.BgSp.enabled = false;
				break;
			}
		}
	}

	// Token: 0x06003D35 RID: 15669 RVA: 0x001103B0 File Offset: 0x0010E5B0
	public void OnClickGetBtn()
	{
		if (this.curData == null)
		{
			return;
		}
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

	// Token: 0x04002898 RID: 10392
	public UILabel TargetLabel;

	// Token: 0x04002899 RID: 10393
	public UISprite BgSp;

	// Token: 0x0400289A RID: 10394
	private daily_reward curInfo;

	// Token: 0x0400289B RID: 10395
	private DailyActiveRewardData curData;

	// Token: 0x0400289C RID: 10396
	public GameObject ComFlagObj;

	// Token: 0x0400289D RID: 10397
	public GameObject CanGetObj;
}
