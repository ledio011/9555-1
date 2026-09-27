using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008E2 RID: 2274
public class LevelPackLineItem : MonoBehaviour
{
	// Token: 0x06003D79 RID: 15737 RVA: 0x0011357C File Offset: 0x0011177C
	public void refershInfo(level_pack curinfo, LevelPackageData curdata)
	{
		if (curinfo.ID.Equals(this.curInfo.ID))
		{
			this.UpdateInfo(curinfo, curdata);
		}
	}

	// Token: 0x06003D7A RID: 15738 RVA: 0x001135AC File Offset: 0x001117AC
	public void UpdateInfo(level_pack curinfo, LevelPackageData curdata)
	{
		this.curInfo = curinfo;
		this.curData = curdata;
		this.InfoLabel.text = StrDictionary.GetDictionaryString("#{300401}", new object[]
		{
			this.curData.LvTarget
		});
		if (this.curInfo.state == 1L)
		{
			this.btnSp.spriteName = GameDefine.BtnIconNew[0];
		}
		else
		{
			this.btnSp.spriteName = GameDefine.BtnIconNew[1];
		}
		if (this.curInfo.state == 2L)
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
			this.completeFlag.enabled = true;
		}
		else
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
			this.completeFlag.enabled = false;
		}
		this.ItemList.Clear();
		if (!string.IsNullOrEmpty(this.curData.ItemID1))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID1, (EQUIP_QUALITY)this.curData.Quality1, this.curData.ItemCount1));
		}
		if (!string.IsNullOrEmpty(this.curData.ItemID2))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID2, (EQUIP_QUALITY)this.curData.Quality2, this.curData.ItemCount2));
		}
		if (!string.IsNullOrEmpty(this.curData.ItemID3))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID3, (EQUIP_QUALITY)this.curData.Quality3, this.curData.ItemCount3));
		}
		if (!string.IsNullOrEmpty(this.curData.ItemID4))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID4, (EQUIP_QUALITY)this.curData.Quality4, this.curData.ItemCount4));
		}
		if (!string.IsNullOrEmpty(this.curData.ItemID5))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID5, (EQUIP_QUALITY)this.curData.Quality5, this.curData.ItemCount5));
		}
		int num = this.ItemList.Count - this.rewardItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.rewardItems[0].gameObject) as GameObject;
				RewardItem component = gameObject.GetComponent<RewardItem>();
				gameObject.name = string.Format("reward{0:D2}", this.rewardItems.Count);
				gameObject.transform.parent = this.parentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.rewardItems.Add(component);
			}
		}
		for (int j = 0; j < this.rewardItems.Count; j++)
		{
			if (j < this.ItemList.Count)
			{
				int level = 0;
				if (this.ItemList[j].ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.ItemList[j].ItemData.SubType);
				}
				this.rewardItems[j].UpdateItem(this.ItemList[j], level, false);
				UnityVersionUtil.SetActiveRecursive(this.rewardItems[j].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.rewardItems[j].gameObject, false);
			}
		}
		this.parentGrid.Reposition();
	}

	// Token: 0x06003D7B RID: 15739 RVA: 0x00113988 File Offset: 0x00111B88
	public void OnClickBtn()
	{
		if (this.curInfo.state == 1L)
		{
			WaitResponseUIRootLogic.OpenWaitBox(262, 10f, 0f, null);
			require_level_reward.request request = new require_level_reward.request();
			request.ID = this.curInfo.ID;
			NetLogic.GetInstance().Send<Protocol.require_level_reward>(request, null);
		}
		else if (this.curInfo.state == 0L)
		{
			NoticeLogic.AddNotifyData("#{300404}", true, false);
		}
	}

	// Token: 0x040028F4 RID: 10484
	public UILabel InfoLabel;

	// Token: 0x040028F5 RID: 10485
	public List<RewardItem> rewardItems;

	// Token: 0x040028F6 RID: 10486
	private level_pack curInfo;

	// Token: 0x040028F7 RID: 10487
	private LevelPackageData curData;

	// Token: 0x040028F8 RID: 10488
	public UILabel BtnLabel;

	// Token: 0x040028F9 RID: 10489
	public UISprite btnSp;

	// Token: 0x040028FA RID: 10490
	public UIGrid parentGrid;

	// Token: 0x040028FB RID: 10491
	private List<GameItem> ItemList = new List<GameItem>();

	// Token: 0x040028FC RID: 10492
	public UISprite completeFlag;
}
