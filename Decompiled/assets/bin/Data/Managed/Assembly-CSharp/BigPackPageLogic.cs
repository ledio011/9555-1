using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008D5 RID: 2261
public class BigPackPageLogic : MonoBehaviour
{
	// Token: 0x06003CE4 RID: 15588 RVA: 0x0010CE38 File Offset: 0x0010B038
	private void Update()
	{
		if (this.reamainTime > 0L)
		{
			this.tempCountTime += Time.deltaTime;
			if (this.tempCountTime >= 1f)
			{
				this.reamainTime -= 1L;
				this.tempCountTime -= 1f;
				this.TimeLabel.text = TimeTools.GetFullTime(this.reamainTime);
			}
			if (this.reamainTime <= 0L)
			{
				this.tempCountTime = 0f;
				this.TimeLabel.enabled = false;
				this.timeNamelabel.enabled = false;
			}
		}
	}

	// Token: 0x06003CE5 RID: 15589 RVA: 0x0010CEDC File Offset: 0x0010B0DC
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

	// Token: 0x06003CE6 RID: 15590 RVA: 0x0010CF70 File Offset: 0x0010B170
	private void TextureLoadFinish(Dictionary<string, Texture> retdic)
	{
		if (retdic == null || retdic.Count == 0)
		{
			return;
		}
		if (!string.IsNullOrEmpty(this.curData.TextureTitle1) && retdic.ContainsKey(this.curData.TextureTitle1))
		{
			this.fontTexture.mainTexture = retdic[this.curData.TextureTitle1];
			this.fontTexture.SetDimensions(retdic[this.curData.TextureTitle1].width, retdic[this.curData.TextureTitle1].height);
		}
		else
		{
			this.fontTexture.mainTexture = null;
		}
		if (!string.IsNullOrEmpty(this.curData.TextureTitle2) && retdic.ContainsKey(this.curData.TextureTitle2))
		{
			this.BgTexture.mainTexture = retdic[this.curData.TextureTitle2];
			this.BgTexture.SetDimensions(retdic[this.curData.TextureTitle2].width, retdic[this.curData.TextureTitle2].height);
		}
		else
		{
			this.BgTexture.mainTexture = null;
		}
	}

	// Token: 0x06003CE7 RID: 15591 RVA: 0x0010D0B0 File Offset: 0x0010B2B0
	public void RefershInfo(special_big_pack curPackinfo, Color oriColor)
	{
		this.ambientLight = oriColor;
		this.CurInfo = curPackinfo;
		this.CurID = this.CurInfo.ID;
		this.curData = DataManager.GetBigPackageDataById(this.CurInfo.ID);
		this.InitTexture();
		this.CurState = (int)this.CurInfo.state;
		this.reamainTime = 0L;
		this.tempCountTime = 0f;
		this.TimeLabel.enabled = false;
		this.timeNamelabel.enabled = false;
		if (!string.IsNullOrEmpty(this.curData.TimeList))
		{
			this.dayName = StrDictionary.GetDictionaryString("#{100241}", new object[0]);
			this.reamainTime = (long)TimeTools.GetShopItemTime(this.curData.GetCurTimeEnd()).TotalSeconds;
			if (this.reamainTime > 0L)
			{
				this.TimeLabel.text = TimeTools.GetFullTime(this.reamainTime);
				this.TimeLabel.enabled = true;
				this.timeNamelabel.enabled = true;
			}
		}
		if (string.IsNullOrEmpty(this.curData.ProductId))
		{
			this.IsDollorBuy = false;
		}
		else
		{
			this.IsDollorBuy = true;
		}
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
		int num = this.ItemList.Count - this.rewardItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.rewardItems[0].gameObject) as GameObject;
				RewardItem component = gameObject.GetComponent<RewardItem>();
				gameObject.name = string.Format("reward{0:D2}", this.rewardItems.Count);
				gameObject.transform.parent = this.parentGrid1.transform;
				gameObject.transform.localScale = Vector3.one;
				this.rewardItems.Add(component);
			}
		}
		for (int j = 0; j < this.rewardItems.Count; j++)
		{
			if (j < this.ItemList.Count)
			{
				if (j < 2)
				{
					this.rewardItems[j].transform.parent = this.parentGrid1.transform;
					this.rewardItems[j].transform.localScale = Vector3.one;
				}
				else
				{
					this.rewardItems[j].transform.parent = this.parentGrid2.transform;
					this.rewardItems[j].transform.localScale = Vector3.one;
				}
				UnityVersionUtil.SetActiveRecursive(this.rewardItems[j].gameObject, true);
				int level = 0;
				if (this.ItemList[j].ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.ItemList[j].ItemData.SubType);
				}
				this.rewardItems[j].UpdateItem(this.ItemList[j], level, false);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.rewardItems[j].gameObject, false);
			}
		}
		this.parentGrid1.Reposition();
		this.parentGrid2.Reposition();
		if (this.CurState == 0)
		{
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{300601}", new object[0]);
		}
		else
		{
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{301116}", new object[0]);
		}
		if (this.curData.showmodeltype == 1)
		{
			this.ResetModelVisual(this.ItemList);
		}
		else if (this.curData.showmodeltype == 2)
		{
			for (int k = 0; k < this.ItemList.Count; k++)
			{
				if (this.ItemList[k].ItemData.Type == GameDefine.ITEM_TYPE.EXCHANGE)
				{
					MountData mountDataById = DataManager.GetMountDataById(this.ItemList[k].ItemData.Function.ToString());
					this.ResetCarModelVisual(mountDataById, DataManager.GetColorDataById(mountDataById.DefaultColorId));
					break;
				}
			}
		}
		else
		{
			this.ModelPic.enabled = false;
			this.CarModelPic.enabled = false;
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "BigSales", "open");
	}

	// Token: 0x06003CE8 RID: 15592 RVA: 0x0010D900 File Offset: 0x0010BB00
	public void OnClickBuyBtn()
	{
		if (this.CurState == 0)
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

	// Token: 0x06003CE9 RID: 15593 RVA: 0x0010D9D8 File Offset: 0x0010BBD8
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BigPackRoot);
		if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
		}
	}

	// Token: 0x06003CEA RID: 15594 RVA: 0x0010DA20 File Offset: 0x0010BC20
	public void UpdateInfo(string id)
	{
		if (this.CurID.Equals(id))
		{
			this.CurState = 1;
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{301116}", new object[0]);
		}
	}

	// Token: 0x06003CEB RID: 15595 RVA: 0x0010DA58 File Offset: 0x0010BC58
	private void ResetCarModelVisual(MountData mountData, ColorData datacolor)
	{
		this.ResetFakeCarObjRoot();
		SingletonUnity<FakeCarObjRootLogic>.Instance.InitFakeCarObj(mountData, datacolor);
	}

	// Token: 0x06003CEC RID: 15596 RVA: 0x0010DA6C File Offset: 0x0010BC6C
	private void ResetModelVisual(List<GameItem> mItemList)
	{
		this.ResetFakeObjRoot();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer equipPack = playerData.EquipPack;
		string headId = string.Empty;
		string weaponId = string.Empty;
		string bodyId = string.Empty;
		string legId = string.Empty;
		switch (playerData.Profession)
		{
		case PROFESSION_TYPE.XD:
			weaponId = GameDefine.XD_DefaultModel[0];
			headId = GameDefine.XD_DefaultModel[1];
			bodyId = GameDefine.XD_DefaultModel[2];
			legId = GameDefine.XD_DefaultModel[3];
			break;
		case PROFESSION_TYPE.QJ:
			weaponId = GameDefine.QJ_DefaultModel[0];
			headId = GameDefine.QJ_DefaultModel[1];
			bodyId = GameDefine.QJ_DefaultModel[2];
			legId = GameDefine.QJ_DefaultModel[3];
			break;
		case PROFESSION_TYPE.NQS:
			weaponId = GameDefine.NQS_DefaultModel[0];
			headId = GameDefine.NQS_DefaultModel[1];
			bodyId = GameDefine.NQS_DefaultModel[2];
			legId = GameDefine.NQS_DefaultModel[3];
			break;
		}
		for (int i = 0; i < mItemList.Count; i++)
		{
			ItemData itemData = mItemList[i].ItemData;
			if (itemData.Type == GameDefine.ITEM_TYPE.EQUIP || itemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
			{
				EquipData equipDataById = DataManager.GetEquipDataById(itemData.ID);
				switch (itemData.SubType)
				{
				case 0:
					weaponId = equipDataById.ModelId;
					break;
				case 1:
					headId = equipDataById.ModelId;
					break;
				case 2:
					bodyId = equipDataById.ModelId;
					break;
				case 3:
					legId = equipDataById.ModelId;
					break;
				}
			}
		}
		if (this.mCurFakeObj != null)
		{
			this.mCurFakeObj.DestroyFakeObj();
		}
		if (this.mCurFakeObj == null || this.mCurFakeObj.FakeObj == null)
		{
			this.mCurFakeObj = new FakeObjLogic();
			this.mCurFakeObj.InitFakeObject(weaponId, headId, bodyId, legId, playerData.Profession, SingletonUnity<FakeObjRootLogic>.Instance.MeshRoot, null, "FakeObj");
		}
		else
		{
			this.mCurFakeObj.CheckFakeObject(weaponId, headId, bodyId, legId, null);
			this.mCurFakeObj.PlayAnim("idle", playerData.CharacterModelData.ModelFirstType);
		}
	}

	// Token: 0x06003CED RID: 15597 RVA: 0x0010DC8C File Offset: 0x0010BE8C
	public void SetCarLight()
	{
		float num = 0.78431374f;
		RenderSettings.ambientLight = new Color(num, num, num, 1f);
	}

	// Token: 0x06003CEE RID: 15598 RVA: 0x0010DCB4 File Offset: 0x0010BEB4
	public void ResetNormalLight()
	{
		RenderSettings.ambientLight = this.ambientLight;
	}

	// Token: 0x06003CEF RID: 15599 RVA: 0x0010DCC4 File Offset: 0x0010BEC4
	private void ResetFakeObjRoot()
	{
		this.ResetNormalLight();
		this.RotateModelBtnListener.onDrag = new UIEventListener.VectorDelegate(this.OnDragModelBtn);
		FakeObjRootLogic instance = SingletonUnity<FakeObjRootLogic>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeObjRoot");
			instance = SingletonUnity<FakeObjRootLogic>.Instance;
		}
		SingletonUnity<FakeObjRootLogic>.Instance.SetPicValue(0.9f);
		SingletonUnity<FakeObjRootLogic>.Instance.EnableFakeObjRoot();
		this.ModelPic.mainTexture = instance.ModelPic;
		this.ModelPic.enabled = true;
		this.CarModelPic.enabled = false;
	}

	// Token: 0x06003CF0 RID: 15600 RVA: 0x0010DD54 File Offset: 0x0010BF54
	private void ResetFakeCarObjRoot()
	{
		this.SetCarLight();
		this.RotateModelBtnListener.onDrag = new UIEventListener.VectorDelegate(this.OnDragCarModelPic);
		this.RotateModelBtnListener.onPress = new UIEventListener.BoolDelegate(this.OnPressCarModelPic);
		FakeCarObjRootLogic instance = SingletonUnity<FakeCarObjRootLogic>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeCarObjRoot");
			instance = SingletonUnity<FakeCarObjRootLogic>.Instance;
		}
		this.CarMeshRoot = instance.MeshRoot;
		SingletonUnity<FakeCarObjRootLogic>.Instance.EnableFakeObjRoot();
		SingletonUnity<FakeCarObjRootLogic>.Instance.PlayRotate();
		this.CarModelPic.mainTexture = instance.ModelPic;
		this.ModelPic.enabled = false;
		this.CarModelPic.enabled = true;
	}

	// Token: 0x06003CF1 RID: 15601 RVA: 0x0010DE04 File Offset: 0x0010C004
	private void OnDragModelBtn(GameObject btn, Vector2 delta)
	{
		if (this.mCurFakeObj != null && this.mCurFakeObj.FakeObj != null)
		{
			this.mCurFakeObj.FakeObj.transform.localEulerAngles -= delta.x * Vector3.up;
		}
	}

	// Token: 0x06003CF2 RID: 15602 RVA: 0x0010DE64 File Offset: 0x0010C064
	public void OnDragCarModelPic(GameObject btn, Vector2 delta)
	{
		if (this.CarMeshRoot != null)
		{
			this.CarMeshRoot.transform.localEulerAngles -= new Vector3(0f, delta.x, 0f);
		}
	}

	// Token: 0x06003CF3 RID: 15603 RVA: 0x0010DEB4 File Offset: 0x0010C0B4
	public void OnPressCarModelPic(GameObject btn, bool ispress)
	{
		if (ispress)
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.StopRotate();
		}
		else
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.PlayRotate();
		}
	}

	// Token: 0x06003CF4 RID: 15604 RVA: 0x0010DED8 File Offset: 0x0010C0D8
	private void OnDisable()
	{
		this.UnLoadFakeObj();
	}

	// Token: 0x06003CF5 RID: 15605 RVA: 0x0010DEE0 File Offset: 0x0010C0E0
	public void UnLoadFakeObj()
	{
		if (this.mCurFakeObj != null)
		{
			this.mCurFakeObj.DestroyFakeObj();
			this.mCurFakeObj = null;
		}
		if (SingletonUnity<FakeObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeObjRootLogic>.Instance.DisableFakeObjRoot();
		}
		if (SingletonUnity<FakeCarObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.DisableFakeObjRoot();
		}
	}

	// Token: 0x04002840 RID: 10304
	private special_big_pack CurInfo;

	// Token: 0x04002841 RID: 10305
	public UILabel btnLabel;

	// Token: 0x04002842 RID: 10306
	public UILabel timeNamelabel;

	// Token: 0x04002843 RID: 10307
	public UILabel TimeLabel;

	// Token: 0x04002844 RID: 10308
	public List<RewardItem> rewardItems;

	// Token: 0x04002845 RID: 10309
	private List<GameItem> ItemList = new List<GameItem>();

	// Token: 0x04002846 RID: 10310
	public UIGrid parentGrid1;

	// Token: 0x04002847 RID: 10311
	public UIGrid parentGrid2;

	// Token: 0x04002848 RID: 10312
	private BigPackageData curData;

	// Token: 0x04002849 RID: 10313
	private int CurState;

	// Token: 0x0400284A RID: 10314
	private string CurID;

	// Token: 0x0400284B RID: 10315
	private long reamainTime;

	// Token: 0x0400284C RID: 10316
	public UIEventListener RotateModelBtnListener;

	// Token: 0x0400284D RID: 10317
	private FakeObjLogic mCurFakeObj;

	// Token: 0x0400284E RID: 10318
	public UITexture ModelPic;

	// Token: 0x0400284F RID: 10319
	public UITexture CarModelPic;

	// Token: 0x04002850 RID: 10320
	private Transform CarMeshRoot;

	// Token: 0x04002851 RID: 10321
	private Color ambientLight;

	// Token: 0x04002852 RID: 10322
	public UITexture BgTexture;

	// Token: 0x04002853 RID: 10323
	public UITexture fontTexture;

	// Token: 0x04002854 RID: 10324
	private List<string> textureList = new List<string>();

	// Token: 0x04002855 RID: 10325
	private TimeSpan LimitTimeSpan;

	// Token: 0x04002856 RID: 10326
	private string dayName;

	// Token: 0x04002857 RID: 10327
	private bool IsDollorBuy;

	// Token: 0x04002858 RID: 10328
	private float tempCountTime;
}
