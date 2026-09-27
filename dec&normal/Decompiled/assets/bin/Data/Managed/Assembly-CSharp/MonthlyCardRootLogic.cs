using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200098C RID: 2444
public class MonthlyCardRootLogic : SingletonUnity<MonthlyCardRootLogic>
{
	// Token: 0x06004525 RID: 17701 RVA: 0x0015A680 File Offset: 0x00158880
	public void EnableReset()
	{
		this.curvipinfo = null;
		for (int i = 0; i < this.RewardItems.Count; i++)
		{
			NGUITools.SetActive(this.RewardItems[i].gameObject, false);
		}
	}

	// Token: 0x06004526 RID: 17702 RVA: 0x0015A6C8 File Offset: 0x001588C8
	public void Reset(ret_require_vip_info.request request)
	{
		if (request.HasVip)
		{
			this.curvipinfo = request.vip;
		}
		if (this.curvipinfo == null)
		{
			return;
		}
		this.CurData = DataManager.GetMonthlyCardDataById(this.curvipinfo.id);
		this.ChangeBtnState();
		this.InitTexture();
		this.ItemList.Clear();
		if (!string.IsNullOrEmpty(this.CurData.ItemID1))
		{
			this.ItemList.Add(new GameItem(this.CurData.ItemID1, (EQUIP_QUALITY)this.CurData.Quality1, this.CurData.ItemCount1));
		}
		if (!string.IsNullOrEmpty(this.CurData.ItemID2))
		{
			this.ItemList.Add(new GameItem(this.CurData.ItemID2, (EQUIP_QUALITY)this.CurData.Quality2, this.CurData.ItemCount2));
		}
		if (!string.IsNullOrEmpty(this.CurData.ItemID3))
		{
			this.ItemList.Add(new GameItem(this.CurData.ItemID3, (EQUIP_QUALITY)this.CurData.Quality3, this.CurData.ItemCount3));
		}
		if (!string.IsNullOrEmpty(this.CurData.ItemID4))
		{
			this.ItemList.Add(new GameItem(this.CurData.ItemID4, (EQUIP_QUALITY)this.CurData.Quality4, this.CurData.ItemCount4));
		}
		if (!string.IsNullOrEmpty(this.CurData.ItemID5))
		{
			this.ItemList.Add(new GameItem(this.CurData.ItemID5, (EQUIP_QUALITY)this.CurData.Quality5, this.CurData.ItemCount5));
		}
		int num = this.ItemList.Count - this.RewardItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.RewardItems[0].gameObject) as GameObject;
				RewardItem component = gameObject.GetComponent<RewardItem>();
				gameObject.name = string.Format("reward{0:D2}", this.RewardItems.Count);
				gameObject.transform.parent = this.ParentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.RewardItems.Add(component);
			}
		}
		if (this.ItemList.Count > 3)
		{
			this.ParentGrid.transform.localPosition = new Vector3(-81f, 19f, 0f);
			this.ParentGrid2.transform.localPosition = new Vector3(-81f, -44f, 0f);
		}
		else
		{
			this.ParentGrid.transform.localPosition = new Vector3(-81f, -14f, 0f);
		}
		for (int j = 0; j < this.RewardItems.Count; j++)
		{
			if (j < this.ItemList.Count)
			{
				if (this.ItemList.Count > 3)
				{
					if (j < 2)
					{
						this.RewardItems[j].transform.parent = this.ParentGrid.transform;
						this.RewardItems[j].transform.localScale = Vector3.one;
					}
					else
					{
						this.RewardItems[j].transform.parent = this.ParentGrid2.transform;
						this.RewardItems[j].transform.localScale = Vector3.one;
					}
				}
				else
				{
					this.RewardItems[j].transform.parent = this.ParentGrid.transform;
					this.RewardItems[j].transform.localScale = Vector3.one;
				}
				int level = 0;
				if (this.ItemList[j].ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.ItemList[j].ItemData.SubType);
				}
				this.RewardItems[j].UpdateItem(this.ItemList[j], level, false);
				UnityVersionUtil.SetActiveRecursive(this.RewardItems[j].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.RewardItems[j].gameObject, false);
			}
		}
		this.ParentGrid.Reposition();
		this.ParentGrid2.Reposition();
	}

	// Token: 0x06004527 RID: 17703 RVA: 0x0015AB74 File Offset: 0x00158D74
	public void RefershInfo(ret_require_vip_reward.request request)
	{
		if (request.HasVip)
		{
			this.curvipinfo = request.vip;
		}
		if (this.curvipinfo == null)
		{
			return;
		}
		this.CurData = DataManager.GetMonthlyCardDataById(this.curvipinfo.id);
		this.ChangeBtnState();
	}

	// Token: 0x06004528 RID: 17704 RVA: 0x0015ABC0 File Offset: 0x00158DC0
	public void UpdateInfo(string id)
	{
		if (this.curvipinfo != null && this.curvipinfo.id.Equals(id))
		{
			this.curvipinfo.state = 1L;
			this.ChangeBtnState();
		}
	}

	// Token: 0x06004529 RID: 17705 RVA: 0x0015AC04 File Offset: 0x00158E04
	public void ChangeBtnState()
	{
		this.BtnAnima.ResetToBeginning();
		this.BtnAnima.enabled = false;
		if (this.curvipinfo.state == -1L)
		{
			this.BtnLabel.text = string.Format("${0}", this.CurData.Dollor);
			this.BtnSp.spriteName = GameDefine.BtnIconNew[0];
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{300705}", new object[0]);
		}
		else if (this.curvipinfo.state == 0L)
		{
			this.BtnAnima.enabled = true;
			this.BtnAnima.PlayForward();
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
			this.BtnSp.spriteName = GameDefine.BtnIconNew[0];
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{300706}", new object[]
			{
				this.curvipinfo.count
			});
		}
		else
		{
			this.BtnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
			this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
			this.InfoLabel.text = StrDictionary.GetDictionaryString("#{300706}", new object[]
			{
				this.curvipinfo.count
			});
		}
	}

	// Token: 0x0600452A RID: 17706 RVA: 0x0015AD70 File Offset: 0x00158F70
	public void InitTexture()
	{
		List<string> list = new List<string>();
		if (!string.IsNullOrEmpty(this.CurData.BGName))
		{
			list.Add(this.CurData.BGName);
		}
		if (!string.IsNullOrEmpty(this.CurData.FlagName))
		{
			list.Add(this.CurData.FlagName);
		}
		if (!string.IsNullOrEmpty(this.CurData.PricePicName))
		{
			list.Add(this.CurData.PricePicName);
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

	// Token: 0x0600452B RID: 17707 RVA: 0x0015AE2C File Offset: 0x0015902C
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		if (retdic.Count == 0)
		{
			return;
		}
		if (!string.IsNullOrEmpty(this.CurData.BGName) && retdic.ContainsKey(this.CurData.BGName))
		{
			this.bgtexture.mainTexture = retdic[this.CurData.BGName];
		}
		if (!string.IsNullOrEmpty(this.CurData.FlagName) && retdic.ContainsKey(this.CurData.FlagName))
		{
			this.flagtexture.mainTexture = retdic[this.CurData.FlagName];
			this.flagtexture2.mainTexture = retdic[this.CurData.FlagName];
		}
		if (!string.IsNullOrEmpty(this.CurData.PricePicName) && retdic.ContainsKey(this.CurData.PricePicName))
		{
			this.pricetexture.mainTexture = retdic[this.CurData.PricePicName];
		}
	}

	// Token: 0x0600452C RID: 17708 RVA: 0x0015AF38 File Offset: 0x00159138
	public void OnClickBtn()
	{
		if (this.curvipinfo == null)
		{
			return;
		}
		if (this.curvipinfo.state == -1L)
		{
			SingletonDontDestoryUnity<GameManager>.Instance.Billing(this.CurData.ProductId);
			if (GameSettingData.IsTestBilling)
			{
				WaitResponseUIRootLogic.OpenWaitBox(266, 10f, 0f, null);
				check_purchase.request request = new check_purchase.request();
				request.productId = this.CurData.ProductId;
				NetLogic.GetInstance().Send<Protocol.check_purchase>(request, null);
			}
		}
		else if (this.curvipinfo.state == 0L)
		{
			WaitResponseUIRootLogic.OpenWaitBox(300, 10f, 0f, null);
			NetLogic.GetInstance().Send<Protocol.require_vip_reward>(null, null);
		}
	}

	// Token: 0x0400320B RID: 12811
	public UITexture bgtexture;

	// Token: 0x0400320C RID: 12812
	public UITexture flagtexture;

	// Token: 0x0400320D RID: 12813
	public UITexture flagtexture2;

	// Token: 0x0400320E RID: 12814
	public UITexture pricetexture;

	// Token: 0x0400320F RID: 12815
	public UILabel InfoLabel;

	// Token: 0x04003210 RID: 12816
	public UISprite BtnSp;

	// Token: 0x04003211 RID: 12817
	public UILabel BtnLabel;

	// Token: 0x04003212 RID: 12818
	public TweenScale BtnAnima;

	// Token: 0x04003213 RID: 12819
	private MonthlyCardData CurData;

	// Token: 0x04003214 RID: 12820
	private List<GameItem> ItemList = new List<GameItem>();

	// Token: 0x04003215 RID: 12821
	public List<RewardItem> RewardItems;

	// Token: 0x04003216 RID: 12822
	public UIGrid ParentGrid;

	// Token: 0x04003217 RID: 12823
	public UIGrid ParentGrid2;

	// Token: 0x04003218 RID: 12824
	private vip curvipinfo;
}
