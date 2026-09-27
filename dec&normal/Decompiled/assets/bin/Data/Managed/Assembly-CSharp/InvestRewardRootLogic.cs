using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008E1 RID: 2273
public class InvestRewardRootLogic : SingletonUnity<InvestRewardRootLogic>
{
	// Token: 0x06003D6C RID: 15724 RVA: 0x00112BA8 File Offset: 0x00110DA8
	protected override void Awake()
	{
		base.Awake();
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06003D6D RID: 15725 RVA: 0x00112BF0 File Offset: 0x00110DF0
	public void EnableReset()
	{
		for (int i = 0; i < this.LineItems.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.LineItems[i].gameObject, false);
		}
		NGUITools.SetActive(this.BuyBtn.gameObject, false);
		this.CompleteFlag.enabled = false;
		this.InitTexture();
	}

	// Token: 0x06003D6E RID: 15726 RVA: 0x00112C54 File Offset: 0x00110E54
	public void InitTexture()
	{
		if (this.Texturebanner.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(GameDefine.TextrueBannerInvest, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06003D6F RID: 15727 RVA: 0x00112CA4 File Offset: 0x00110EA4
	private void TextureLoadFinish(string name, Texture textureObj)
	{
		this.Texturebanner.mainTexture = textureObj;
	}

	// Token: 0x06003D70 RID: 15728 RVA: 0x00112CB4 File Offset: 0x00110EB4
	public void Reset(ret_request_invest_pack.request request)
	{
		this.InvestDataList.Clear();
		this.invest_Dic = request.invest_pack;
		this.invest_list = new List<invest_pack>(request.invest_pack.Values);
		if (this.invest_Dic.ContainsKey("0") && this.invest_Dic["0"].state == -1L)
		{
			NGUITools.SetActive(this.BuyBtn.gameObject, true);
			this.CompleteFlag.enabled = false;
			InvestData investDataBuyId = DataManager.GetInvestDataBuyId("0");
			if (string.IsNullOrEmpty(investDataBuyId.Dollor))
			{
				this.BtnLabel.text = string.Format("$19.99", new object[0]);
			}
			else
			{
				this.BtnLabel.text = string.Format("${0}", investDataBuyId.Dollor);
			}
		}
		else
		{
			NGUITools.SetActive(this.BuyBtn.gameObject, false);
			this.CompleteFlag.enabled = true;
		}
		for (int i = 0; i < this.invest_list.Count; i++)
		{
			this.InvestDataList.Add(DataManager.GetInvestDataBuyId(this.invest_list[i].ID));
		}
		this.InvestDataList.Sort((InvestData x, InvestData y) => x.LvTarget - y.LvTarget);
		int num = Mathf.Min(this.InvestDataList.Count, this.lineMinCount) - this.LineItems.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.LineItems[0].gameObject) as GameObject;
				InvestLineItem component = gameObject.GetComponent<InvestLineItem>();
				gameObject.name = string.Format("{0:D2}", this.LineItems.Count);
				gameObject.transform.parent = this.uiWrapContent.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				this.LineItems.Add(component);
			}
		}
		for (int k = 0; k < this.LineItems.Count; k++)
		{
			if (k < this.InvestDataList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[k].gameObject, true);
				this.LineItems[k].UpdateInfo(this.invest_Dic[this.InvestDataList[k].ID], this.InvestDataList[k], k);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[k].gameObject, false);
			}
		}
		this.uiWrapContent.minIndex = 1 - this.InvestDataList.Count;
		this.WrapContentBottomWidget.height = this.InvestDataList.Count * this.uiWrapContent.itemSize;
		if (this.InvestDataList.Count == 1)
		{
			this.uiWrapContent.maxIndex = 1;
		}
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Invest", "open");
	}

	// Token: 0x06003D71 RID: 15729 RVA: 0x0011301C File Offset: 0x0011121C
	public void UpdateInfo(string id)
	{
		if (this.invest_Dic.ContainsKey(id))
		{
			this.invest_Dic[id].state = 2L;
		}
		for (int i = 0; i < this.InvestDataList.Count; i++)
		{
			if (this.InvestDataList[i].ID.Equals(id))
			{
				for (int j = 0; j < this.LineItems.Count; j++)
				{
					this.LineItems[j].refershinfo(this.invest_Dic[id], this.InvestDataList[i], i);
				}
				SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Invest", string.Format("require_{0}", id));
				break;
			}
		}
	}

	// Token: 0x06003D72 RID: 15730 RVA: 0x001130F0 File Offset: 0x001112F0
	public void UpdateInfo(ret_buy_invest_pack.request request)
	{
		this.InvestDataList.Clear();
		this.invest_Dic = request.invest_pack;
		this.invest_list = new List<invest_pack>(request.invest_pack.Values);
		if (this.invest_Dic.ContainsKey("0") && this.invest_Dic["0"].state == -1L)
		{
			NGUITools.SetActive(this.BuyBtn.gameObject, true);
			this.CompleteFlag.enabled = false;
			InvestData investDataBuyId = DataManager.GetInvestDataBuyId("0");
			this.BtnLabel.text = string.Format("${0}", investDataBuyId.Dollor);
		}
		else
		{
			NGUITools.SetActive(this.BuyBtn.gameObject, false);
			this.CompleteFlag.enabled = true;
		}
		for (int i = 0; i < this.invest_list.Count; i++)
		{
			this.InvestDataList.Add(DataManager.GetInvestDataBuyId(this.invest_list[i].ID));
		}
		this.InvestDataList.Sort((InvestData x, InvestData y) => x.LvTarget - y.LvTarget);
		int num = Mathf.Min(this.InvestDataList.Count, this.lineMinCount) - this.LineItems.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.LineItems[0].gameObject) as GameObject;
				InvestLineItem component = gameObject.GetComponent<InvestLineItem>();
				gameObject.name = string.Format("{0:D2}", this.LineItems.Count);
				gameObject.transform.parent = this.uiWrapContent.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				this.LineItems.Add(component);
			}
		}
		for (int k = 0; k < this.LineItems.Count; k++)
		{
			if (k < this.InvestDataList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[k].gameObject, true);
				this.LineItems[k].UpdateInfo(this.invest_Dic[this.InvestDataList[k].ID], this.InvestDataList[k], k);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.LineItems[k].gameObject, false);
			}
		}
		this.uiWrapContent.minIndex = 1 - this.InvestDataList.Count;
		this.WrapContentBottomWidget.height = this.InvestDataList.Count * this.uiWrapContent.itemSize;
		if (this.InvestDataList.Count == 1)
		{
			this.uiWrapContent.maxIndex = 1;
		}
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Invest", "invest_buy");
	}

	// Token: 0x06003D73 RID: 15731 RVA: 0x00113428 File Offset: 0x00111628
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		InvestLineItem itemLogic = this.LineItems[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x06003D74 RID: 15732 RVA: 0x00113450 File Offset: 0x00111650
	private void ResetItemLine(InvestLineItem itemLogic, int idx)
	{
		if (idx < this.InvestDataList.Count)
		{
			itemLogic.UpdateInfo(this.invest_Dic[this.InvestDataList[idx].ID], this.InvestDataList[idx], idx);
		}
	}

	// Token: 0x06003D75 RID: 15733 RVA: 0x001134A0 File Offset: 0x001116A0
	public void OnClickBuyInvestBtn()
	{
		if (this.invest_Dic.ContainsKey("0") && this.invest_Dic["0"].state == -1L)
		{
			InvestData investDataBuyId = DataManager.GetInvestDataBuyId("0");
			if (!string.IsNullOrEmpty(investDataBuyId.ProductId))
			{
				SingletonDontDestoryUnity<GameManager>.Instance.Billing(investDataBuyId.ProductId);
				if (GameSettingData.IsTestBilling)
				{
					WaitResponseUIRootLogic.OpenWaitBox(266, 10f, 0f, null);
					check_purchase.request request = new check_purchase.request();
					request.productId = investDataBuyId.ProductId;
					NetLogic.GetInstance().Send<Protocol.check_purchase>(request, null);
				}
			}
		}
	}

	// Token: 0x040028E6 RID: 10470
	public UIWrapContentNew uiWrapContent;

	// Token: 0x040028E7 RID: 10471
	private int lineMinCount = 6;

	// Token: 0x040028E8 RID: 10472
	public UIWidget WrapContentBottomWidget;

	// Token: 0x040028E9 RID: 10473
	public UIScrollView uiScrollView;

	// Token: 0x040028EA RID: 10474
	public List<InvestLineItem> LineItems;

	// Token: 0x040028EB RID: 10475
	private List<invest_pack> invest_list;

	// Token: 0x040028EC RID: 10476
	private List<InvestData> InvestDataList = new List<InvestData>();

	// Token: 0x040028ED RID: 10477
	private Dictionary<string, invest_pack> invest_Dic;

	// Token: 0x040028EE RID: 10478
	public UITexture Texturebanner;

	// Token: 0x040028EF RID: 10479
	public UISprite BuyBtn;

	// Token: 0x040028F0 RID: 10480
	public UILabel BtnLabel;

	// Token: 0x040028F1 RID: 10481
	public UISprite CompleteFlag;
}
