using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A1E RID: 2590
public class ShopBigSaleRootLogic : SingletonUnity<ShopBigSaleRootLogic>
{
	// Token: 0x06004AB0 RID: 19120 RVA: 0x00189740 File Offset: 0x00187940
	public void EnableReset()
	{
		this.CurPage = 1;
		this.MaxPage = 1;
		this.ambientLight = RenderSettings.ambientLight;
		for (int i = 0; i < this.ShopItemList.Count; i++)
		{
			NGUITools.SetActive(this.ShopItemList[i].gameObject, false);
		}
		this.ItemInfoRoot.Reset();
	}

	// Token: 0x06004AB1 RID: 19121 RVA: 0x001897A4 File Offset: 0x001879A4
	public void Reset(ret_special_big_pack.request request)
	{
		this.SpePackList.Clear();
		this.SpePackList = new List<special_big_pack>(request.special_big_packs.Values);
		for (int i = this.SpePackList.Count - 1; i >= 0; i--)
		{
			BigPackageData bigPackageDataById = DataManager.GetBigPackageDataById(this.SpePackList[i].ID);
			if (bigPackageDataById != null)
			{
				if (bigPackageDataById.SellType != 2 || this.SpePackList[i].state != 0L)
				{
					this.SpePackList.RemoveAt(i);
				}
			}
			else
			{
				this.SpePackList.RemoveAt(i);
			}
		}
		this.SpePackList.Sort(delegate(special_big_pack x, special_big_pack y)
		{
			BigPackageData bigPackageDataById2 = DataManager.GetBigPackageDataById(x.ID);
			BigPackageData bigPackageDataById3 = DataManager.GetBigPackageDataById(y.ID);
			if (bigPackageDataById2.sortID == bigPackageDataById3.sortID)
			{
				return int.Parse(x.ID) - int.Parse(y.ID);
			}
			return bigPackageDataById2.sortID - bigPackageDataById3.sortID;
		});
		int num = Mathf.Min(this.SpePackList.Count, this.PageMax) - this.ShopItemList.Count;
		int count = this.ShopItemList.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.ShopItemList[0].gameObject) as GameObject;
				gameObject.name = string.Format("shangPing_{0}", count + j);
				this.ItemGrid.AddChild(gameObject.transform);
				gameObject.transform.localScale = Vector3.one;
				this.ShopItemList.Add(gameObject.GetComponent<ShopBigSaleItemLogic>());
			}
		}
		this.MaxPage = (this.SpePackList.Count - 1) / this.PageMax + 1;
		if (this.MaxPage <= 0)
		{
			this.MaxPage = 1;
		}
		this.ShowPage(1);
	}

	// Token: 0x06004AB2 RID: 19122 RVA: 0x00189964 File Offset: 0x00187B64
	public void ShowPage(int page)
	{
		this.CurPage = page;
		List<special_big_pack> list = new List<special_big_pack>();
		for (int i = (page - 1) * this.PageMax; i < page * this.PageMax; i++)
		{
			if (i < this.SpePackList.Count)
			{
				list.Add(this.SpePackList[i]);
			}
		}
		for (int j = 0; j < this.ShopItemList.Count; j++)
		{
			if (j < list.Count)
			{
				NGUITools.SetActive(this.ShopItemList[j].gameObject, true);
				this.ShopItemList[j].Reset(list[j], new ShopBigSaleItemLogic.OnClickShopBigSaleItemDelegate(this.OnClickItemBtn));
			}
			else
			{
				NGUITools.SetActive(this.ShopItemList[j].gameObject, false);
			}
		}
		this.ItemGrid.Reposition();
		this.PageLabel.text = string.Format("{0}/{1}", this.CurPage, this.MaxPage);
		if (list.Count > 0)
		{
			this.ShopItemList[0].OnClickItemBtn();
		}
	}

	// Token: 0x06004AB3 RID: 19123 RVA: 0x00189A98 File Offset: 0x00187C98
	public void OnClickItemBtn(special_big_pack selectinfo, GameObject itemobj)
	{
		if (this.CurSelectInfo != null && this.CurSelectInfo.ID.Equals(selectinfo))
		{
			return;
		}
		this.CurSelectInfo = selectinfo;
		this.CurBigPackData = DataManager.GetBigPackageDataById(this.CurSelectInfo.ID);
		this.UpdateSelectObj();
		this.ItemInfoRoot.RefershInfo(this.CurSelectInfo);
		this.UpdateBuyLabel();
	}

	// Token: 0x06004AB4 RID: 19124 RVA: 0x00189B04 File Offset: 0x00187D04
	public void UpdateBuyLabel()
	{
		if (string.IsNullOrEmpty(this.CurBigPackData.ProductId))
		{
			this.IsDollorBuy = false;
			this.BuyBtnLabel.text = GameMoneyHelper.GetMoneyValStr(this.CurBigPackData.PriceCost, this.CurBigPackData.PriceType);
		}
		else
		{
			this.IsDollorBuy = true;
			this.BuyBtnLabel.text = string.Format("${0}", this.CurBigPackData.Dollor);
		}
		if (this.CurSelectInfo.state != 0L)
		{
			this.BuyBtnSp.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			this.BuyBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
	}

	// Token: 0x06004AB5 RID: 19125 RVA: 0x00189BB8 File Offset: 0x00187DB8
	public void OnClickBuyBtn()
	{
		if (this.CurSelectInfo == null)
		{
			return;
		}
		if (this.CurSelectInfo.state == 0L)
		{
			if (this.IsDollorBuy)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.Billing(this.CurBigPackData.ProductId);
				if (GameSettingData.IsTestBilling)
				{
					WaitResponseUIRootLogic.OpenWaitBox(266, 10f, 0f, null);
					check_purchase.request request = new check_purchase.request();
					request.productId = this.CurBigPackData.ProductId;
					NetLogic.GetInstance().Send<Protocol.check_purchase>(request, null);
				}
			}
			else if (GameMoneyHelper.BeforeCheckBuy(this.CurBigPackData.PriceType, this.CurBigPackData.PriceCost))
			{
				WaitResponseUIRootLogic.OpenWaitBox(272, 10f, 0f, null);
				buy_big_pack.request request2 = new buy_big_pack.request();
				request2.ID = this.CurBigPackData.ID;
				NetLogic.GetInstance().Send<Protocol.buy_big_pack>(request2, null);
			}
		}
	}

	// Token: 0x06004AB6 RID: 19126 RVA: 0x00189CA0 File Offset: 0x00187EA0
	public void UpdateSelectObj()
	{
		for (int i = 0; i < this.ShopItemList.Count; i++)
		{
			if (UnityVersionUtil.IsActive(this.ShopItemList[i].gameObject))
			{
				this.ShopItemList[i].UpdateSelect(this.CurSelectInfo);
			}
		}
	}

	// Token: 0x06004AB7 RID: 19127 RVA: 0x00189CFC File Offset: 0x00187EFC
	public void OnClickLeftBtn()
	{
		if (this.CurPage > 1)
		{
			this.CurPage--;
			this.ShowPage(this.CurPage);
		}
	}

	// Token: 0x06004AB8 RID: 19128 RVA: 0x00189D30 File Offset: 0x00187F30
	public void OnClickRightBtn()
	{
		if (this.CurPage < this.MaxPage)
		{
			this.CurPage++;
			this.ShowPage(this.CurPage);
		}
	}

	// Token: 0x06004AB9 RID: 19129 RVA: 0x00189D60 File Offset: 0x00187F60
	public void ResetNormalLight()
	{
		RenderSettings.ambientLight = this.ambientLight;
	}

	// Token: 0x06004ABA RID: 19130 RVA: 0x00189D70 File Offset: 0x00187F70
	private void OnDisable()
	{
		this.ResetNormalLight();
		this.ItemInfoRoot.UnLoadFakeObj();
	}

	// Token: 0x06004ABB RID: 19131 RVA: 0x00189D84 File Offset: 0x00187F84
	public void UpdateInfo(string id)
	{
		for (int i = 0; i < this.SpePackList.Count; i++)
		{
			if (this.SpePackList[i].ID.Equals(id))
			{
				this.SpePackList[i].state = 2L;
				if (this.CurSelectInfo.ID.Equals(id))
				{
					this.CurSelectInfo = this.SpePackList[i];
					this.UpdateBuyLabel();
				}
				for (int j = 0; j < this.ShopItemList.Count; j++)
				{
					this.ShopItemList[j].updateinfo(this.SpePackList[i]);
				}
				break;
			}
		}
	}

	// Token: 0x04003850 RID: 14416
	public List<ShopBigSaleItemLogic> ShopItemList;

	// Token: 0x04003851 RID: 14417
	private int PageMax = 8;

	// Token: 0x04003852 RID: 14418
	private List<special_big_pack> SpePackList = new List<special_big_pack>();

	// Token: 0x04003853 RID: 14419
	public UIGrid ItemGrid;

	// Token: 0x04003854 RID: 14420
	private int CurPage;

	// Token: 0x04003855 RID: 14421
	private int MaxPage;

	// Token: 0x04003856 RID: 14422
	private special_big_pack CurSelectInfo;

	// Token: 0x04003857 RID: 14423
	public ShopBigSaleItemInfo ItemInfoRoot;

	// Token: 0x04003858 RID: 14424
	public UISprite BuyBtnSp;

	// Token: 0x04003859 RID: 14425
	public UILabel BuyBtnLabel;

	// Token: 0x0400385A RID: 14426
	private BigPackageData CurBigPackData;

	// Token: 0x0400385B RID: 14427
	private bool IsDollorBuy;

	// Token: 0x0400385C RID: 14428
	private Color ambientLight;

	// Token: 0x0400385D RID: 14429
	public UILabel PageLabel;
}
