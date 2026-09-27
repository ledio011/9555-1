using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008DF RID: 2271
public class FirstBuyRootLogic : SingletonUnity<FirstBuyRootLogic>
{
	// Token: 0x06003D59 RID: 15705 RVA: 0x00111F64 File Offset: 0x00110164
	public void EnableReset()
	{
		for (int i = 0; i < this.rewardItems.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.rewardItems[i].gameObject, false);
		}
		this.ResetFakeObjRoot();
		this.InitTexture();
	}

	// Token: 0x06003D5A RID: 15706 RVA: 0x00111FB0 File Offset: 0x001101B0
	public void InitTexture()
	{
		this.textureList.Clear();
		if (this.BgTexture.mainTexture == null)
		{
			this.textureList.Add(GameDefine.TextureFirstBuyBG);
		}
		if (this.fontTexture.mainTexture == null)
		{
			this.textureList.Add(GameDefine.TextureFirstBuy);
		}
		if (this.textureList.Count == 0)
		{
			return;
		}
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(this.textureList, new BundleManager.LoadTextureDicFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x06003D5B RID: 15707 RVA: 0x00112054 File Offset: 0x00110254
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		if (retdic.Count == 0)
		{
			return;
		}
		if (retdic.ContainsKey(GameDefine.TextureFirstBuyBG))
		{
			this.BgTexture.mainTexture = retdic[GameDefine.TextureFirstBuyBG];
		}
		if (retdic.ContainsKey(GameDefine.TextureFirstBuy))
		{
			this.fontTexture.mainTexture = retdic[GameDefine.TextureFirstBuy];
		}
	}

	// Token: 0x06003D5C RID: 15708 RVA: 0x001120BC File Offset: 0x001102BC
	private void UnloadTexture()
	{
		BundleManager.UnloadTexture(this.textureList);
	}

	// Token: 0x06003D5D RID: 15709 RVA: 0x001120CC File Offset: 0x001102CC
	public void Reset(ret_request_first_buy.request request)
	{
		this.curData = DataManager.GetFirstBuyDataById(request.ID);
		this.CurState = (int)request.state;
		this.ItemList.Clear();
		this.ShowModelID = string.Empty;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		switch (playerData.Profession)
		{
		case PROFESSION_TYPE.XD:
			if (!string.IsNullOrEmpty(this.curData.XDItemID1))
			{
				this.ItemList.Add(new GameItem(this.curData.XDItemID1, (EQUIP_QUALITY)this.curData.XDQuality1, this.curData.XDItemCount1));
			}
			this.ShowModelID = this.curData.XDShowModelID;
			break;
		case PROFESSION_TYPE.QJ:
			if (!string.IsNullOrEmpty(this.curData.QJItemID1))
			{
				this.ItemList.Add(new GameItem(this.curData.QJItemID1, (EQUIP_QUALITY)this.curData.QJQuality1, this.curData.QJItemCount1));
			}
			this.ShowModelID = this.curData.QJShowModelID;
			break;
		case PROFESSION_TYPE.NQS:
			if (!string.IsNullOrEmpty(this.curData.NQItemID1))
			{
				this.ItemList.Add(new GameItem(this.curData.NQItemID1, (EQUIP_QUALITY)this.curData.NQQuality1, this.curData.NQItemCount1));
			}
			this.ShowModelID = this.curData.NQShowModelID;
			break;
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
		if (this.CurState == 0)
		{
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{301113}", new object[0]);
		}
		else if (this.CurState == 1)
		{
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{300301}", new object[0]);
		}
		else
		{
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
		}
		this.showItemVisual();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "FirstBuy", "open");
	}

	// Token: 0x06003D5E RID: 15710 RVA: 0x00112580 File Offset: 0x00110780
	public void showItemVisual()
	{
		if (this.FakeItemObj != null)
		{
			Object.Destroy(this.FakeItemObj);
		}
		this.FakeItemObj = null;
		if (string.IsNullOrEmpty(this.ShowModelID))
		{
			return;
		}
		ShowModelData showModelDataById = DataManager.GetShowModelDataById(this.ShowModelID);
		if (UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadItem(showModelDataById.ModelName, new BundleManager.LoaditemsFinish(this.ItemLoadFinish), null, null));
		}
	}

	// Token: 0x06003D5F RID: 15711 RVA: 0x00112600 File Offset: 0x00110800
	private void ItemLoadFinish(string name, Object fakeobj, object param1 = null, object param2 = null)
	{
		if (fakeobj != null)
		{
			ShowModelData showModelDataById = DataManager.GetShowModelDataById(this.ShowModelID);
			this.FakeItemObj = (Object.Instantiate(fakeobj) as GameObject);
			if (this.FakeItemObj != null)
			{
				BundleManager.ResetAllShader(this.FakeItemObj.transform);
				NGUITools.SetLayer(this.FakeItemObj, SingletonUnity<FakeObjRootLogic>.Instance.gameObject.layer);
				this.FakeItemObj.transform.parent = SingletonUnity<FakeObjRootLogic>.Instance.MeshRoot.transform;
				this.FakeItemObj.transform.localPosition = showModelDataById.Position;
				this.FakeItemObj.transform.localRotation = Quaternion.Euler(showModelDataById.Rotation);
			}
		}
	}

	// Token: 0x06003D60 RID: 15712 RVA: 0x001126C4 File Offset: 0x001108C4
	private void ResetFakeObjRoot()
	{
		if (this.FakeItemObj != null)
		{
			Object.Destroy(this.FakeItemObj);
		}
		FakeObjRootLogic instance = SingletonUnity<FakeObjRootLogic>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeObjRoot");
			instance = SingletonUnity<FakeObjRootLogic>.Instance;
		}
		SingletonUnity<FakeObjRootLogic>.Instance.SetPicValue(1f);
		SingletonUnity<FakeObjRootLogic>.Instance.EnableFakeObjRoot();
		this.ModelPic.mainTexture = instance.ModelPic;
	}

	// Token: 0x06003D61 RID: 15713 RVA: 0x0011273C File Offset: 0x0011093C
	public void OnClickBuyBtn()
	{
		if (this.CurState == 1)
		{
			WaitResponseUIRootLogic.OpenWaitBox(264, 10f, 0f, null);
			require_first_buy_reward.request request = new require_first_buy_reward.request();
			request.ID = this.curData.ID;
			NetLogic.GetInstance().Send<Protocol.require_first_buy_reward>(request, null);
		}
		else if (this.CurState == 0)
		{
			this.OnClickCloseBtn();
			NoticeLogic.AddNotifyData("#{300011}", true, false);
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PopDiamondBuyRoot, delegate
			{
				SingletonUnity<PopDiamondBuyRootLogic>.Instance.EnableReset();
				ask_shop_list.request request2 = new ask_shop_list.request();
				request2.type = 4L;
				request2.subType = 1L;
				NetLogic.GetInstance().Send<Protocol.ask_shop_list>(request2, null);
				WaitResponseUIRootLogic.OpenWaitBox(143, 10f, 0f, null);
			}, null);
		}
	}

	// Token: 0x06003D62 RID: 15714 RVA: 0x001127DC File Offset: 0x001109DC
	public void UpdateInfo(string id)
	{
		if (id.Equals(this.curData.ID))
		{
			this.CurState = 2;
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
			this.OnClickCloseBtn();
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "FirstBuy", string.Format("require_{0}", id));
		}
	}

	// Token: 0x06003D63 RID: 15715 RVA: 0x00112848 File Offset: 0x00110A48
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FirstBuyRoot);
		if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
		}
	}

	// Token: 0x06003D64 RID: 15716 RVA: 0x00112890 File Offset: 0x00110A90
	private void OnDisable()
	{
		this.UnLoadFakeObj();
		this.UnloadTexture();
	}

	// Token: 0x06003D65 RID: 15717 RVA: 0x001128A0 File Offset: 0x00110AA0
	public void UnLoadFakeObj()
	{
		if (SingletonUnity<FakeObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeObjRootLogic>.Instance.DisableFakeObjRoot();
		}
		if (this.FakeItemObj != null)
		{
			Object.Destroy(this.FakeItemObj);
		}
	}

	// Token: 0x040028D0 RID: 10448
	public UISprite btnSp;

	// Token: 0x040028D1 RID: 10449
	public UILabel btnLabel;

	// Token: 0x040028D2 RID: 10450
	public List<RewardItem> rewardItems;

	// Token: 0x040028D3 RID: 10451
	private List<GameItem> ItemList = new List<GameItem>();

	// Token: 0x040028D4 RID: 10452
	public UIGrid parentGrid;

	// Token: 0x040028D5 RID: 10453
	private FirstBuyData curData;

	// Token: 0x040028D6 RID: 10454
	private int CurState;

	// Token: 0x040028D7 RID: 10455
	public GameObject FakeItemObj;

	// Token: 0x040028D8 RID: 10456
	public UITexture ModelPic;

	// Token: 0x040028D9 RID: 10457
	private string ShowModelID;

	// Token: 0x040028DA RID: 10458
	public UITexture BgTexture;

	// Token: 0x040028DB RID: 10459
	public UITexture fontTexture;

	// Token: 0x040028DC RID: 10460
	private List<string> textureList = new List<string>();
}
