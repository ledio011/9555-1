using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008FA RID: 2298
public class TowerWipeOutRootLogic : SingletonUnity<TowerWipeOutRootLogic>
{
	// Token: 0x06003EAA RID: 16042 RVA: 0x0012021C File Offset: 0x0011E41C
	public void Reset(int curFloor, int TargetFloor)
	{
		this.mWipingFlag = false;
		Dictionary<string, item> dictionary = new Dictionary<string, item>();
		int num = 0;
		for (int i = curFloor; i <= TargetFloor; i++)
		{
			ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(DataManager.GetTowerDataByFloorID(i).ShowRewardID);
			TowerData towerDataByFloorID = DataManager.GetTowerDataByFloorID(i);
			num += towerDataByFloorID.ExistTime;
			for (int j = 0; j < showRewardDataByID.ItemIdList.Count; j++)
			{
				if (dictionary.ContainsKey(showRewardDataByID.ItemIdList[j]))
				{
					dictionary[showRewardDataByID.ItemIdList[j]].itemCount += (long)showRewardDataByID.CountList[j];
				}
				else
				{
					item item = new item();
					item.itemId = showRewardDataByID.ItemIdList[j];
					item.quality = (long)showRewardDataByID.QualityList[j];
					item.itemCount = (long)showRewardDataByID.CountList[j];
					dictionary.Add(item.itemId, item);
				}
			}
		}
		this.ShowRewardItem.ShowRewards(dictionary);
		this.WipeOutTargetLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{101525}", new object[0]), TargetFloor + 1);
		this.NeedTimeLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{101580}", new object[0]), new TimeSpan(0, 0, num));
		this.curNeedDiamond = num / 60;
		this.WipeOutCostLabel.text = GameMoneyHelper.GetMoneyValStr(this.curNeedDiamond, GameDefine.MONEY_TYPE.DIAMOND);
		NGUITools.SetActive(this.GetRewardBtn, false);
	}

	// Token: 0x06003EAB RID: 16043 RVA: 0x001203CC File Offset: 0x0011E5CC
	public void ResetWipingPage(int curFloor, int targetFloor, int restTime)
	{
		this.remainStr = StrDictionary.GetDictionaryString("#{101580}", new object[0]);
		this.mWipingFlag = true;
		this.mRestTime = (float)restTime;
		this.WipeOutTargetLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{101525}", new object[0]), targetFloor + 1);
		this.NeedTimeLabel.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{101580}", new object[0]), new TimeSpan(0, 0, restTime));
		Dictionary<string, item> dictionary = new Dictionary<string, item>();
		int num = 0;
		for (int i = curFloor; i <= targetFloor; i++)
		{
			ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(DataManager.GetTowerDataByFloorID(i).ShowRewardID);
			TowerData towerDataByFloorID = DataManager.GetTowerDataByFloorID(i);
			num += towerDataByFloorID.ExistTime;
			for (int j = 0; j < showRewardDataByID.ItemIdList.Count; j++)
			{
				if (dictionary.ContainsKey(showRewardDataByID.ItemIdList[j]))
				{
					dictionary[showRewardDataByID.ItemIdList[j]].itemCount += (long)showRewardDataByID.CountList[j];
				}
				else
				{
					item item = new item();
					item.itemId = showRewardDataByID.ItemIdList[j];
					item.quality = (long)showRewardDataByID.QualityList[j];
					item.itemCount = (long)showRewardDataByID.CountList[j];
					dictionary.Add(item.itemId, item);
				}
			}
		}
		this.ShowRewardItem.ShowRewards(dictionary);
		this.curNeedDiamond = num / 60;
		this.WipeOutCostLabel.text = GameMoneyHelper.GetMoneyValStr(this.curNeedDiamond, GameDefine.MONEY_TYPE.DIAMOND);
		NGUITools.SetActive(this.NormalWipeOutBtn, false);
		this.SpecialWipeOutBtn.transform.localPosition = new Vector3(0f, this.SpecialWipeOutBtn.transform.localPosition.y, 0f);
		NGUITools.SetActive(this.GetRewardBtn, false);
	}

	// Token: 0x06003EAC RID: 16044 RVA: 0x001205E0 File Offset: 0x0011E7E0
	public void ResetWipeOutRewardPage(int curFloor, int targetFloor)
	{
		Dictionary<string, item> dictionary = new Dictionary<string, item>();
		int num = 0;
		for (int i = curFloor; i <= targetFloor; i++)
		{
			ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(DataManager.GetTowerDataByFloorID(i).ShowRewardID);
			TowerData towerDataByFloorID = DataManager.GetTowerDataByFloorID(i);
			num += towerDataByFloorID.ExistTime;
			for (int j = 0; j < showRewardDataByID.ItemIdList.Count; j++)
			{
				if (dictionary.ContainsKey(showRewardDataByID.ItemIdList[j]))
				{
					dictionary[showRewardDataByID.ItemIdList[j]].itemCount += (long)showRewardDataByID.CountList[j];
				}
				else
				{
					item item = new item();
					item.itemId = showRewardDataByID.ItemIdList[j];
					item.quality = (long)showRewardDataByID.QualityList[j];
					item.itemCount = (long)showRewardDataByID.CountList[j];
					dictionary.Add(item.itemId, item);
				}
			}
		}
		this.ShowRewardItem.ShowRewards(dictionary);
		this.mGrantRewardType = 0;
		this.WipeOutTargetLabel.text = string.Format("WipeOut Reward:", new object[0]);
		NGUITools.SetActive(this.NeedTimeLabel.gameObject, false);
		NGUITools.SetActive(this.NormalWipeOutBtn.gameObject, false);
		NGUITools.SetActive(this.SpecialWipeOutBtn.gameObject, false);
		NGUITools.SetActive(this.GetRewardBtn, true);
		this.mCanGet = true;
		if (this.mCanGet)
		{
			this.GetRewardSprite.spriteName = "CZ_anNiu_1";
		}
		else
		{
			this.GetRewardSprite.spriteName = "CZ_anNiu_2+";
		}
	}

	// Token: 0x06003EAD RID: 16045 RVA: 0x00120794 File Offset: 0x0011E994
	public void ResetGetSpecialRewardPage(int floorId, bool canGet)
	{
		this.curFloorId = floorId;
		TowerData towerDataByFloorID = DataManager.GetTowerDataByFloorID(floorId);
		if (towerDataByFloorID == null)
		{
			this.OnClickCloseBtn();
			return;
		}
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(towerDataByFloorID.SpecialRewardId);
		this.ShowRewardItem.ShowRewards(showRewardDataByID.ItemIdList, showRewardDataByID.QualityList, showRewardDataByID.CountList);
		this.mGrantRewardType = 1;
		this.WipeOutTargetLabel.text = StrDictionary.GetDictionaryString("#{101531}", new object[]
		{
			floorId + 1
		});
		NGUITools.SetActive(this.NeedTimeLabel.gameObject, false);
		NGUITools.SetActive(this.NormalWipeOutBtn.gameObject, false);
		NGUITools.SetActive(this.SpecialWipeOutBtn.gameObject, false);
		NGUITools.SetActive(this.GetRewardBtn, true);
		this.mCanGet = canGet;
		if (canGet)
		{
			this.GetRewardSprite.spriteName = "CZ_anNiu_1";
		}
		else
		{
			this.GetRewardSprite.spriteName = "CZ_anNiu_2+";
		}
	}

	// Token: 0x06003EAE RID: 16046 RVA: 0x00120884 File Offset: 0x0011EA84
	public void OnClickWipeOutNormalBtn()
	{
		tower_wipe_out.request request = new tower_wipe_out.request();
		request.wipeType = 0L;
		NetLogic.GetInstance().Send<Protocol.tower_wipe_out>(request, null);
		this.OnClickCloseBtn();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tower", "wipe_out", "wipe_out_time");
	}

	// Token: 0x06003EAF RID: 16047 RVA: 0x001208CC File Offset: 0x0011EACC
	public void OnClickWipeOutSpecialBtn()
	{
		if (!GameMoneyHelper.BeforeCheckBuyTop(GameDefine.MONEY_TYPE.DIAMOND, this.curNeedDiamond))
		{
			return;
		}
		tower_wipe_out.request request = new tower_wipe_out.request();
		request.wipeType = 1L;
		NetLogic.GetInstance().Send<Protocol.tower_wipe_out>(request, null);
		WaitResponseUIRootLogic.OpenWaitBox(208, 10f, 0f, null);
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("tower", "wipe_out", "wipe_out_buy");
	}

	// Token: 0x06003EB0 RID: 16048 RVA: 0x00120934 File Offset: 0x0011EB34
	public void OnClickGetRewardBtn()
	{
		if (!this.mCanGet)
		{
			return;
		}
		grant_tower_reward.request request = new grant_tower_reward.request();
		request.type = (long)this.mGrantRewardType;
		request.id = (long)this.curFloorId;
		NetLogic.GetInstance().Send<Protocol.grant_tower_reward>(request, null);
		this.OnClickCloseBtn();
	}

	// Token: 0x06003EB1 RID: 16049 RVA: 0x00120980 File Offset: 0x0011EB80
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TowerWipeOutRootLogic);
	}

	// Token: 0x06003EB2 RID: 16050 RVA: 0x00120994 File Offset: 0x0011EB94
	private void Update()
	{
		if (this.mWipingFlag)
		{
			this.curTime = (int)(this.mRestTime - Time.deltaTime);
			if (this.curTime != (int)this.mRestTime)
			{
				this.NeedTimeLabel.text = string.Format("{0}:{1}", this.remainStr, new TimeSpan(0, 0, this.curTime));
			}
			this.mRestTime -= Time.deltaTime;
		}
	}

	// Token: 0x04002A5B RID: 10843
	public ShowRewardItems ShowRewardItem;

	// Token: 0x04002A5C RID: 10844
	public UILabel WipeOutTargetLabel;

	// Token: 0x04002A5D RID: 10845
	public UILabel NeedTimeLabel;

	// Token: 0x04002A5E RID: 10846
	public UILabel WipeOutCostLabel;

	// Token: 0x04002A5F RID: 10847
	public GameObject NormalWipeOutBtn;

	// Token: 0x04002A60 RID: 10848
	public GameObject SpecialWipeOutBtn;

	// Token: 0x04002A61 RID: 10849
	public GameObject GetRewardBtn;

	// Token: 0x04002A62 RID: 10850
	public UISprite GetRewardSprite;

	// Token: 0x04002A63 RID: 10851
	private bool mWipingFlag;

	// Token: 0x04002A64 RID: 10852
	private float mRestTime;

	// Token: 0x04002A65 RID: 10853
	private bool mCanGet;

	// Token: 0x04002A66 RID: 10854
	private int curNeedDiamond;

	// Token: 0x04002A67 RID: 10855
	private int mGrantRewardType;

	// Token: 0x04002A68 RID: 10856
	private int curFloorId;

	// Token: 0x04002A69 RID: 10857
	private string remainStr;

	// Token: 0x04002A6A RID: 10858
	private int curTime;
}
