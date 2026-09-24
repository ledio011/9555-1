using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008E6 RID: 2278
public class RetrieveLineLogic : MonoBehaviour
{
	// Token: 0x06003D9A RID: 15770 RVA: 0x00114AB8 File Offset: 0x00112CB8
	public void UpdateInfo(retrieve_info curinfo, RetrieveData curdata)
	{
		this.curInfo = curinfo;
		this.curData = curdata;
		this.TitleLabel.text = StrDictionary.GetDictionaryString(this.curData.Name, new object[0]);
		this.normal_Price = (long)this.curData.PriceCost2 + (long)this.curData.AddCost2 * curinfo.count;
		if (this.normal_Price > (long)this.curData.MaxCost2)
		{
			this.normal_Price = (long)this.curData.MaxCost2;
		}
		this.normalPrice.text = GameMoneyHelper.GetMoneyValStr(this.normal_Price, (long)this.curData.PriceType2);
		this.per_Price = (long)this.curData.PriceCost1 + (long)this.curData.AddCost1 * curinfo.count;
		if (this.per_Price > (long)this.curData.MaxCost1)
		{
			this.per_Price = (long)this.curData.MaxCost1;
		}
		this.perPrice.text = GameMoneyHelper.GetMoneyValStr(this.per_Price, (long)this.curData.PriceType1);
		if (this.curInfo.state == 0L)
		{
			this.normalbtnSp.color = new Color(0f, 0.8745098f, 1f);
			this.perbtnSp.spriteName = GameDefine.BtnIconNew[0];
		}
		else if (this.curInfo.state == 1L)
		{
			this.normalbtnSp.color = Color.white;
			this.perbtnSp.spriteName = GameDefine.BtnIconNew[1];
		}
		if (this.curInfo.state == 2L || this.curInfo.state == 1L)
		{
			this.completeFlag.enabled = true;
			UnityVersionUtil.SetActiveRecursive(this.normalbtnSp.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.perbtnSp.gameObject, false);
		}
		else
		{
			this.completeFlag.enabled = false;
			UnityVersionUtil.SetActiveRecursive(this.normalbtnSp.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.perbtnSp.gameObject, true);
		}
		string id = DataManager.GetAdaptDataByID(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.Level).DorpKeyDic[this.curData.ShowRewardID];
		ShowRewardData showRewardDataByID = DataManager.GetShowRewardDataByID(id);
		if (showRewardDataByID != null)
		{
			List<int> counts = new List<int>(showRewardDataByID.CountList);
			if (curdata.isDanceOrExp)
			{
			}
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, true);
			this.SetRewardItem(new List<string>(showRewardDataByID.ItemIdList), new List<EQUIP_QUALITY>(showRewardDataByID.QualityList), counts);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.ShowRewardItemsScripts.gameObject, false);
		}
	}

	// Token: 0x06003D9B RID: 15771 RVA: 0x00114D70 File Offset: 0x00112F70
	private void SetRewardItem(List<string> itemIds, List<EQUIP_QUALITY> qualitys, List<int> counts)
	{
		this.ShowRewardItemsScripts.ShowRewards(itemIds, qualitys, counts);
	}

	// Token: 0x06003D9C RID: 15772 RVA: 0x00114D80 File Offset: 0x00112F80
	public void OnClickNormalBtn()
	{
		if (this.curInfo.state == 0L && GameMoneyHelper.BeforeCheckBuy(this.curData.PriceType2, (int)this.normal_Price))
		{
			WaitResponseUIRootLogic.OpenWaitBox(279, 10f, 0f, null);
			request_retrieve.request request = new request_retrieve.request();
			request.ID = this.curInfo.ID;
			request.Type = (long)this.curData.PriceType2;
			NetLogic.GetInstance().Send<Protocol.request_retrieve>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Retrive", string.Format("require_{0}_normal", this.curInfo.ID));
		}
	}

	// Token: 0x06003D9D RID: 15773 RVA: 0x00114E2C File Offset: 0x0011302C
	public void OnClickPerfectBtn()
	{
		if (this.curInfo.state == 0L && GameMoneyHelper.BeforeCheckBuy(this.curData.PriceType1, (int)this.per_Price))
		{
			WaitResponseUIRootLogic.OpenWaitBox(279, 10f, 0f, null);
			request_retrieve.request request = new request_retrieve.request();
			request.ID = this.curInfo.ID;
			request.Type = (long)this.curData.PriceType1;
			NetLogic.GetInstance().Send<Protocol.request_retrieve>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Retrive", string.Format("require_{0}_perfect", this.curInfo.ID));
		}
	}

	// Token: 0x04002922 RID: 10530
	public UILabel TitleLabel;

	// Token: 0x04002923 RID: 10531
	private retrieve_info curInfo;

	// Token: 0x04002924 RID: 10532
	private RetrieveData curData;

	// Token: 0x04002925 RID: 10533
	public UISprite normalbtnSp;

	// Token: 0x04002926 RID: 10534
	public UISprite perbtnSp;

	// Token: 0x04002927 RID: 10535
	public UILabel normalPrice;

	// Token: 0x04002928 RID: 10536
	public UILabel perPrice;

	// Token: 0x04002929 RID: 10537
	public ShowRewardItems ShowRewardItemsScripts;

	// Token: 0x0400292A RID: 10538
	public UISprite completeFlag;

	// Token: 0x0400292B RID: 10539
	private long normal_Price;

	// Token: 0x0400292C RID: 10540
	private long per_Price;
}
