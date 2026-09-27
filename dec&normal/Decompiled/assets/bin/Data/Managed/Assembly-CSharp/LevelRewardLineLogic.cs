using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008E4 RID: 2276
public class LevelRewardLineLogic : MonoBehaviour
{
	// Token: 0x06003D87 RID: 15751 RVA: 0x00113F14 File Offset: 0x00112114
	public void UpdateInfo(LevelRewardData curdata, int index, level_reward curinfo)
	{
		this.CurData = curdata;
		this.CurIndex = index;
		ShowRewardData showRewardData = null;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		switch (index)
		{
		case 0:
			this.TargetLevel = curdata.TargetLevel1;
			this.gettype = curdata.GetType1;
			this.pricetype = curdata.PriceType1;
			this.pricenum = curdata.PriceNum1;
			this.infoLabel.text = StrDictionary.GetDictionaryString(curdata.Info1, new object[]
			{
				curdata.TargetLevel1
			});
			switch (playerData.Profession)
			{
			case PROFESSION_TYPE.XD:
				showRewardData = DataManager.GetShowRewardDataByID(curdata.XDShowReward1);
				break;
			case PROFESSION_TYPE.QJ:
				showRewardData = DataManager.GetShowRewardDataByID(curdata.QJShowReward1);
				break;
			case PROFESSION_TYPE.NQS:
				showRewardData = DataManager.GetShowRewardDataByID(curdata.NQSShowReward1);
				break;
			}
			break;
		case 1:
			this.TargetLevel = curdata.TargetLevel2;
			this.gettype = curdata.GetType2;
			this.pricetype = curdata.PriceType2;
			this.pricenum = curdata.PriceNum2;
			this.infoLabel.text = StrDictionary.GetDictionaryString(curdata.Info2, new object[]
			{
				curdata.TargetLevel2
			});
			switch (playerData.Profession)
			{
			case PROFESSION_TYPE.XD:
				showRewardData = DataManager.GetShowRewardDataByID(curdata.XDShowReward2);
				break;
			case PROFESSION_TYPE.QJ:
				showRewardData = DataManager.GetShowRewardDataByID(curdata.QJShowReward2);
				break;
			case PROFESSION_TYPE.NQS:
				showRewardData = DataManager.GetShowRewardDataByID(curdata.NQSShowReward2);
				break;
			}
			break;
		case 2:
			this.TargetLevel = curdata.TargetLevel3;
			this.gettype = curdata.GetType3;
			this.pricetype = curdata.PriceType3;
			this.pricenum = curdata.PriceNum3;
			this.infoLabel.text = StrDictionary.GetDictionaryString(curdata.Info3, new object[]
			{
				curdata.TargetLevel3
			});
			switch (playerData.Profession)
			{
			case PROFESSION_TYPE.XD:
				showRewardData = DataManager.GetShowRewardDataByID(curdata.XDShowReward3);
				break;
			case PROFESSION_TYPE.QJ:
				showRewardData = DataManager.GetShowRewardDataByID(curdata.QJShowReward3);
				break;
			case PROFESSION_TYPE.NQS:
				showRewardData = DataManager.GetShowRewardDataByID(curdata.NQSShowReward3);
				break;
			}
			break;
		}
		if (showRewardData != null)
		{
			NGUITools.SetActive(this.ShowRewardroot.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardData.ItemIdList), new List<EQUIP_QUALITY>(showRewardData.QualityList), new List<int>(showRewardData.CountList));
		}
		else
		{
			NGUITools.SetActive(this.ShowRewardroot.gameObject, false);
		}
		this.isComplete = ((curinfo.state & 1L << (index & 31)) != 0L);
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		if (this.isComplete)
		{
			this.CompleteFlag.enabled = true;
			this.GetLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
			this.GetBtnSp.spriteName = GameDefine.BtnIconNew[1];
		}
		else
		{
			this.CompleteFlag.enabled = false;
			if (this.gettype == 0)
			{
				this.GetLabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
			}
			else
			{
				this.GetLabel.text = GameMoneyHelper.GetMoneyValStr(this.pricenum, this.pricetype);
			}
			if (level >= this.TargetLevel)
			{
				this.GetBtnSp.spriteName = GameDefine.BtnIconNew[0];
			}
			else
			{
				this.GetBtnSp.spriteName = GameDefine.BtnIconNew[1];
			}
		}
	}

	// Token: 0x06003D88 RID: 15752 RVA: 0x001142BC File Offset: 0x001124BC
	private void SetRewardItem(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		this.ShowRewardroot.ShowRewards(itemIds, qualitys, counts);
	}

	// Token: 0x06003D89 RID: 15753 RVA: 0x001142CC File Offset: 0x001124CC
	public void OnClickReceiveBtn()
	{
		if (this.isComplete)
		{
			return;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level < this.TargetLevel)
		{
			NoticeLogic.AddNotifyData("#{100834}", true, false);
			return;
		}
		if (this.gettype == 1)
		{
			if (GameMoneyHelper.BeforeCheckBuy(this.pricetype, this.pricenum))
			{
				WaitResponseUIRootLogic.OpenWaitBox(297, 10f, 0f, null);
				receive_level_reward.request request = new receive_level_reward.request();
				request.ID = this.CurData.ID;
				request.index = (long)this.CurIndex;
				NetLogic.GetInstance().Send<Protocol.receive_level_reward>(request, null);
			}
		}
		else
		{
			WaitResponseUIRootLogic.OpenWaitBox(297, 10f, 0f, null);
			receive_level_reward.request request2 = new receive_level_reward.request();
			request2.ID = this.CurData.ID;
			request2.index = (long)this.CurIndex;
			NetLogic.GetInstance().Send<Protocol.receive_level_reward>(request2, null);
		}
	}

	// Token: 0x04002907 RID: 10503
	public ShowRewardItems ShowRewardroot;

	// Token: 0x04002908 RID: 10504
	public UISprite GetBtnSp;

	// Token: 0x04002909 RID: 10505
	public UILabel GetLabel;

	// Token: 0x0400290A RID: 10506
	public UILabel infoLabel;

	// Token: 0x0400290B RID: 10507
	public UISprite CompleteFlag;

	// Token: 0x0400290C RID: 10508
	private int TargetLevel;

	// Token: 0x0400290D RID: 10509
	private int gettype;

	// Token: 0x0400290E RID: 10510
	private int pricetype;

	// Token: 0x0400290F RID: 10511
	private int pricenum;

	// Token: 0x04002910 RID: 10512
	private bool isComplete;

	// Token: 0x04002911 RID: 10513
	private LevelRewardData CurData;

	// Token: 0x04002912 RID: 10514
	private int CurIndex;
}
