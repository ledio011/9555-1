using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A09 RID: 2569
public class MysteryShopPageLogic : MonoBehaviour
{
	// Token: 0x060049B6 RID: 18870 RVA: 0x0017D434 File Offset: 0x0017B634
	public void EnableReset()
	{
		for (int i = 0; i < this.RewardList.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.RewardList[i].gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.BtnSp.gameObject, false);
		this.EndTimeLabel.text = string.Empty;
	}

	// Token: 0x060049B7 RID: 18871 RVA: 0x0017D498 File Offset: 0x0017B698
	public void RefershInfo(special_big_pack curPackinfo)
	{
		this.CurInfo = curPackinfo;
		this.curData = DataManager.GetBigPackageDataById(curPackinfo.ID);
		if (this.curData.MaxCount > 0)
		{
			int num = this.curData.MaxCount - (int)curPackinfo.remain_times;
			if (num < 0)
			{
				num = 0;
			}
			this.LimitTimesLabel.enabled = true;
			this.LimitTimesLabel.text = string.Format("{0} {1}/{2}", StrDictionary.GetDictionaryString("#{301101}", new object[0]), num, this.curData.MaxCount);
			if (num > 0)
			{
				this.LimitTimesLabel.color = Color.white;
			}
			else
			{
				this.LimitTimesLabel.color = Color.red;
			}
		}
		else
		{
			this.LimitTimesLabel.enabled = false;
		}
		this.reamainTime = 0L;
		this.tempCountTime = 0f;
		this.EndTimeLabel.enabled = false;
		this.timeNamelabel.enabled = false;
		if (!string.IsNullOrEmpty(this.curData.TimeList))
		{
			this.dayName = StrDictionary.GetDictionaryString("#{100241}", new object[0]);
			this.reamainTime = (long)TimeTools.GetShopItemTime(this.curData.GetCurTimeEnd()).TotalSeconds;
			if (this.reamainTime > 0L)
			{
				this.EndTimeLabel.text = TimeTools.GetFullTime(this.reamainTime);
				this.EndTimeLabel.enabled = true;
				this.timeNamelabel.enabled = true;
				this.hintinfolabel.enabled = true;
			}
			else
			{
				this.hintinfolabel.enabled = false;
			}
		}
		else
		{
			this.hintinfolabel.enabled = false;
		}
		if (string.IsNullOrEmpty(this.curData.ProductId))
		{
			this.IsDollorBuy = false;
			this.BtnLabel.text = GameMoneyHelper.GetMoneyValStr(this.curData.PriceCost, this.curData.PriceType);
		}
		else
		{
			this.IsDollorBuy = true;
			this.BtnLabel.text = string.Format("${0}", this.curData.Dollor);
		}
		if (this.CurInfo.state == 0L)
		{
			this.BtnSp.spriteName = GameDefine.BtnIconNew[0];
		}
		else
		{
			this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
		}
		UnityVersionUtil.SetActiveRecursive(this.BtnSp.gameObject, true);
		this.ShowRewards();
		this.InitTexture();
	}

	// Token: 0x060049B8 RID: 18872 RVA: 0x0017D714 File Offset: 0x0017B914
	public void InitTexture()
	{
		List<string> list = new List<string>();
		if (!string.IsNullOrEmpty(this.curData.TextureTitle1))
		{
			list.Add(this.curData.TextureTitle1);
		}
		if (!string.IsNullOrEmpty(this.curData.TextureTitle2))
		{
			list.Add(this.curData.TextureTitle2);
		}
		if (list.Count == 0)
		{
			return;
		}
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(list, new BundleManager.LoadTextureDicFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x060049B9 RID: 18873 RVA: 0x0017D7A8 File Offset: 0x0017B9A8
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		if (retdic.Count == 0)
		{
			return;
		}
		if (!string.IsNullOrEmpty(this.curData.TextureTitle1) && retdic.ContainsKey(this.curData.TextureTitle1))
		{
			this.bannerTexture.mainTexture = retdic[this.curData.TextureTitle1];
			this.bannerTexture.MakePixelPerfect();
		}
		if (!string.IsNullOrEmpty(this.curData.TextureTitle2) && retdic.ContainsKey(this.curData.TextureTitle2))
		{
			this.ShowTexture.mainTexture = retdic[this.curData.TextureTitle2];
			this.ShowTexture.MakePixelPerfect();
		}
	}

	// Token: 0x060049BA RID: 18874 RVA: 0x0017D868 File Offset: 0x0017BA68
	public void UpdateInfo(special_big_pack curPackinfo)
	{
		if (this.CurInfo.ID.Equals(curPackinfo.ID))
		{
			this.RefershInfo(curPackinfo);
		}
	}

	// Token: 0x060049BB RID: 18875 RVA: 0x0017D898 File Offset: 0x0017BA98
	public void OnClickBuyBtn()
	{
		if (this.CurInfo.state == 0L)
		{
			if (this.IsDollorBuy)
			{
				SingletonDontDestoryUnity<GameManager>.Instance.Billing(this.curData.ProductId);
				if (GameSettingData.IsTestBilling)
				{
					WaitResponseUIRootLogic.OpenWaitBox(266, 10f, 0f, null);
					check_purchase.request request = new check_purchase.request();
					request.productId = this.curData.ProductId;
					NetLogic.GetInstance().Send<Protocol.check_purchase>(request, null);
				}
			}
			else if (GameMoneyHelper.BeforeCheckBuy(this.curData.PriceType, this.curData.PriceCost))
			{
				WaitResponseUIRootLogic.OpenWaitBox(272, 10f, 0f, null);
				buy_big_pack.request request2 = new buy_big_pack.request();
				request2.ID = this.curData.ID;
				NetLogic.GetInstance().Send<Protocol.buy_big_pack>(request2, null);
			}
		}
	}

	// Token: 0x060049BC RID: 18876 RVA: 0x0017D974 File Offset: 0x0017BB74
	private void Update()
	{
		if (this.reamainTime > 0L)
		{
			this.tempCountTime += Time.deltaTime;
			if (this.tempCountTime >= 1f)
			{
				this.reamainTime -= 1L;
				this.tempCountTime -= 1f;
				this.EndTimeLabel.text = TimeTools.GetFullTime(this.reamainTime);
			}
			if (this.reamainTime <= 0L)
			{
				this.tempCountTime = 0f;
				this.EndTimeLabel.enabled = false;
				this.timeNamelabel.enabled = false;
				this.hintinfolabel.enabled = false;
			}
		}
	}

	// Token: 0x060049BD RID: 18877 RVA: 0x0017DA24 File Offset: 0x0017BC24
	public void ShowRewards()
	{
		this.ItemList.Clear();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		switch (playerData.Profession)
		{
		case PROFESSION_TYPE.XD:
			if (!string.IsNullOrEmpty(this.curData.XDItemID1))
			{
				this.ItemList.Add(new GameItem(this.curData.XDItemID1, (EQUIP_QUALITY)this.curData.XDQuality1, this.curData.XDItemCount1));
			}
			if (!string.IsNullOrEmpty(this.curData.XDItemID2))
			{
				this.ItemList.Add(new GameItem(this.curData.XDItemID2, (EQUIP_QUALITY)this.curData.XDQuality2, this.curData.XDItemCount2));
			}
			if (!string.IsNullOrEmpty(this.curData.XDItemID3))
			{
				this.ItemList.Add(new GameItem(this.curData.XDItemID3, (EQUIP_QUALITY)this.curData.XDQuality3, this.curData.XDItemCount3));
			}
			if (!string.IsNullOrEmpty(this.curData.XDItemID4))
			{
				this.ItemList.Add(new GameItem(this.curData.XDItemID4, (EQUIP_QUALITY)this.curData.XDQuality4, this.curData.XDItemCount4));
			}
			break;
		case PROFESSION_TYPE.QJ:
			if (!string.IsNullOrEmpty(this.curData.QJItemID1))
			{
				this.ItemList.Add(new GameItem(this.curData.QJItemID1, (EQUIP_QUALITY)this.curData.QJQuality1, this.curData.QJItemCount1));
			}
			if (!string.IsNullOrEmpty(this.curData.QJItemID2))
			{
				this.ItemList.Add(new GameItem(this.curData.QJItemID2, (EQUIP_QUALITY)this.curData.QJQuality2, this.curData.QJItemCount2));
			}
			if (!string.IsNullOrEmpty(this.curData.QJItemID3))
			{
				this.ItemList.Add(new GameItem(this.curData.QJItemID3, (EQUIP_QUALITY)this.curData.QJQuality3, this.curData.QJItemCount3));
			}
			if (!string.IsNullOrEmpty(this.curData.QJItemID4))
			{
				this.ItemList.Add(new GameItem(this.curData.QJItemID4, (EQUIP_QUALITY)this.curData.QJQuality4, this.curData.QJItemCount4));
			}
			break;
		case PROFESSION_TYPE.NQS:
			if (!string.IsNullOrEmpty(this.curData.NQItemID1))
			{
				this.ItemList.Add(new GameItem(this.curData.NQItemID1, (EQUIP_QUALITY)this.curData.NQQuality1, this.curData.NQItemCount1));
			}
			if (!string.IsNullOrEmpty(this.curData.NQItemID2))
			{
				this.ItemList.Add(new GameItem(this.curData.NQItemID2, (EQUIP_QUALITY)this.curData.NQQuality2, this.curData.NQItemCount2));
			}
			if (!string.IsNullOrEmpty(this.curData.NQItemID3))
			{
				this.ItemList.Add(new GameItem(this.curData.NQItemID3, (EQUIP_QUALITY)this.curData.NQQuality3, this.curData.NQItemCount3));
			}
			if (!string.IsNullOrEmpty(this.curData.NQItemID4))
			{
				this.ItemList.Add(new GameItem(this.curData.NQItemID4, (EQUIP_QUALITY)this.curData.NQQuality4, this.curData.NQItemCount4));
			}
			break;
		}
		if (!string.IsNullOrEmpty(this.curData.ItemID5))
		{
			this.ItemList.Add(new GameItem(this.curData.ItemID5, (EQUIP_QUALITY)this.curData.Quality5, this.curData.ItemCount5));
		}
		int num = this.ItemList.Count - this.RewardList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.RewardList[0].gameObject) as GameObject;
				RewardItem component = gameObject.GetComponent<RewardItem>();
				gameObject.name = string.Format("reward{0:D2}", this.RewardList.Count);
				gameObject.transform.parent = this.ParentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.RewardList.Add(component);
			}
		}
		for (int j = 0; j < this.RewardList.Count; j++)
		{
			if (j < this.ItemList.Count)
			{
				if (j < 2)
				{
					this.RewardList[j].transform.parent = this.ParentGrid.transform;
					this.RewardList[j].transform.localScale = Vector3.one;
				}
				else
				{
					this.RewardList[j].transform.parent = this.ParentGrid2.transform;
					this.RewardList[j].transform.localScale = Vector3.one;
				}
				UnityVersionUtil.SetActiveRecursive(this.RewardList[j].gameObject, true);
				int level = 0;
				if (this.ItemList[j].ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.ItemList[j].ItemData.SubType);
				}
				this.RewardList[j].UpdateItem(this.ItemList[j], level, false);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.RewardList[j].gameObject, false);
			}
		}
		this.ParentGrid.Reposition();
		this.ParentGrid2.Reposition();
	}

	// Token: 0x040036EB RID: 14059
	public UISprite BtnSp;

	// Token: 0x040036EC RID: 14060
	public UILabel BtnLabel;

	// Token: 0x040036ED RID: 14061
	public UILabel LimitTimesLabel;

	// Token: 0x040036EE RID: 14062
	public UILabel EndTimeLabel;

	// Token: 0x040036EF RID: 14063
	public UILabel timeNamelabel;

	// Token: 0x040036F0 RID: 14064
	public UILabel hintinfolabel;

	// Token: 0x040036F1 RID: 14065
	private string dayName;

	// Token: 0x040036F2 RID: 14066
	public UIGrid ParentGrid;

	// Token: 0x040036F3 RID: 14067
	public UIGrid ParentGrid2;

	// Token: 0x040036F4 RID: 14068
	public List<RewardItem> RewardList;

	// Token: 0x040036F5 RID: 14069
	private List<GameItem> ItemList = new List<GameItem>();

	// Token: 0x040036F6 RID: 14070
	private special_big_pack CurInfo;

	// Token: 0x040036F7 RID: 14071
	private BigPackageData curData;

	// Token: 0x040036F8 RID: 14072
	private long reamainTime;

	// Token: 0x040036F9 RID: 14073
	private bool IsDollorBuy;

	// Token: 0x040036FA RID: 14074
	private TimeSpan LimitTimeSpan;

	// Token: 0x040036FB RID: 14075
	public UITexture bannerTexture;

	// Token: 0x040036FC RID: 14076
	public UITexture ShowTexture;

	// Token: 0x040036FD RID: 14077
	private float tempCountTime;
}
