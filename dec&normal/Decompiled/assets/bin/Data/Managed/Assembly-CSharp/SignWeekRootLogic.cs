using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020008EA RID: 2282
public class SignWeekRootLogic : SingletonUnity<SignWeekRootLogic>
{
	// Token: 0x06003DAE RID: 15790 RVA: 0x00115B74 File Offset: 0x00113D74
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.SignWeekRoot);
		if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.NextPop(false);
		}
	}

	// Token: 0x06003DAF RID: 15791 RVA: 0x00115BC8 File Offset: 0x00113DC8
	public void Showcarday()
	{
		this.needshowday = 3;
	}

	// Token: 0x06003DB0 RID: 15792 RVA: 0x00115BD4 File Offset: 0x00113DD4
	public void EnableReset()
	{
		if (SingletonUnity<AutoPopUIRoot>.Exists && UnityVersionUtil.IsActive(SingletonUnity<AutoPopUIRoot>.Instance.gameObject))
		{
			SingletonUnity<AutoPopUIRoot>.Instance.RemoveUI(GameDefine.AUTOPOPTYPE.SIGNWEEK);
		}
		this.ModelPic.enabled = false;
		this.CarModelPic.enabled = false;
		this.needshowday = -1;
		this.SignDataList = DataManager.GetSignInWeekDataList();
		int num = this.SignDataList.Count - this.SevenDaysItems.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.SevenDaysItems[0].gameObject) as GameObject;
				WeekDayRewardItem component = gameObject.GetComponent<WeekDayRewardItem>();
				gameObject.name = string.Format("dayitem{0:D2}", this.SevenDaysItems.Count);
				gameObject.transform.parent = this.SevenDayGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.SevenDaysItems.Add(component);
			}
			this.SevenDayGrid.Reposition();
		}
		for (int j = 0; j < this.SevenDaysItems.Count; j++)
		{
			if (j < this.SignDataList.Count)
			{
				this.GetDayBestItem(j);
				this.SevenDaysItems[j].UpdateItem(this.curbestItem, j + 1, new DelegateDefine.OneIntParamDelegate(this.OnClickDayItem));
				if (j == this.SignDataList.Count - 1)
				{
					this.SevenDaysItems[j].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
				}
			}
			UnityVersionUtil.SetActiveRecursive(this.SevenDaysItems[j].gameObject, false);
		}
		for (int k = 0; k < this.rewardItemList.Count; k++)
		{
			UnityVersionUtil.SetActiveRecursive(this.rewardItemList[k].gameObject, false);
		}
		UnityVersionUtil.SetActiveRecursive(this.dayinfoLabel.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.BestItem.gameObject, false);
		this.curShowDay = -1;
		this.curSign = -2;
		this.ambientLight = RenderSettings.ambientLight;
	}

	// Token: 0x06003DB1 RID: 15793 RVA: 0x00115E14 File Offset: 0x00114014
	public void SetCarLight()
	{
		float num = 0.78431374f;
		RenderSettings.ambientLight = new Color(num, num, num, 1f);
	}

	// Token: 0x06003DB2 RID: 15794 RVA: 0x00115E3C File Offset: 0x0011403C
	public void ResetNormalLight()
	{
		RenderSettings.ambientLight = this.ambientLight;
	}

	// Token: 0x06003DB3 RID: 15795 RVA: 0x00115E4C File Offset: 0x0011404C
	private void ResetFakeObjRoot()
	{
		this.ResetNormalLight();
		if (this.FakeItemObj != null)
		{
			Object.DestroyImmediate(this.FakeItemObj, true);
		}
		this.RotateModelBtnListener.onDrag = new UIEventListener.VectorDelegate(this.OnDragModelBtn);
		this.RotateModelBtnListener.onPress = null;
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

	// Token: 0x06003DB4 RID: 15796 RVA: 0x00115F04 File Offset: 0x00114104
	private void ResetFakeCarObjRoot()
	{
		this.SetCarLight();
		if (this.FakeItemObj != null)
		{
			Object.DestroyImmediate(this.FakeItemObj, true);
		}
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

	// Token: 0x06003DB5 RID: 15797 RVA: 0x00115FD0 File Offset: 0x001141D0
	private void ShowModelVisual(int dayi)
	{
		this.curSigninWeekData = this.SignDataList[dayi];
		this.GetDayBestItem(dayi);
		GameItem gameItem = this.curbestItem;
		if (gameItem.IsEmpty())
		{
			return;
		}
		ItemData itemData = gameItem.ItemData;
		if (itemData.Type == GameDefine.ITEM_TYPE.EQUIP || itemData.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP)
		{
			this.ResetModelVisual(itemData);
		}
		else if (itemData.Type == GameDefine.ITEM_TYPE.EXCHANGE)
		{
			MountData mountDataById = DataManager.GetMountDataById(itemData.Function.ToString());
			this.ResetCarModelVisual(mountDataById, DataManager.GetColorDataById(mountDataById.DefaultColorId));
		}
		else
		{
			this.ResetItemModelVisual(itemData.Type);
		}
	}

	// Token: 0x06003DB6 RID: 15798 RVA: 0x00116078 File Offset: 0x00114278
	private void ResetItemModelVisual(GameDefine.ITEM_TYPE curType)
	{
		if (this.mCurFakeObj != null)
		{
			this.mCurFakeObj.DestroyFakeObj();
			this.mCurFakeObj = null;
		}
		this.ResetFakeObjRoot();
		if (this.curshowdata != null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadItem(this.curshowdata.ModelName, new BundleManager.LoaditemsFinish(this.ItemLoadFinish), null, null));
		}
	}

	// Token: 0x06003DB7 RID: 15799 RVA: 0x001160E8 File Offset: 0x001142E8
	private void ItemLoadFinish(string name, Object fakeobj, object param1 = null, object param2 = null)
	{
		if (fakeobj != null)
		{
			this.FakeItemObj = (Object.Instantiate(fakeobj) as GameObject);
			if (this.FakeItemObj != null)
			{
				BundleManager.ResetAllShader(this.FakeItemObj.transform);
				NGUITools.SetLayer(this.FakeItemObj, SingletonUnity<FakeObjRootLogic>.Instance.MeshRoot.gameObject.layer);
				this.FakeItemObj.transform.parent = SingletonUnity<FakeObjRootLogic>.Instance.MeshRoot.transform;
				this.FakeItemObj.transform.localPosition = this.curshowdata.Position;
				this.FakeItemObj.transform.localRotation = Quaternion.Euler(this.curshowdata.Rotation);
			}
		}
	}

	// Token: 0x06003DB8 RID: 15800 RVA: 0x001161AC File Offset: 0x001143AC
	private void ResetCarModelVisual(MountData mountData, ColorData datacolor)
	{
		this.ResetFakeCarObjRoot();
		SingletonUnity<FakeCarObjRootLogic>.Instance.InitFakeCarObj(mountData, datacolor);
	}

	// Token: 0x06003DB9 RID: 15801 RVA: 0x001161C0 File Offset: 0x001143C0
	private void ResetModelVisual(ItemData curItemdata)
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
		EquipData equipDataById = DataManager.GetEquipDataById(curItemdata.ID);
		switch (curItemdata.SubType)
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
		SingletonUnity<FakeObjRootLogic>.Instance.MoveShowPart((int)playerData.Profession, curItemdata.SubType);
	}

	// Token: 0x06003DBA RID: 15802 RVA: 0x001163AC File Offset: 0x001145AC
	public void Reset(ret_request_sign_week_info.request request)
	{
		this.curSign = (int)request.cur_sign;
		this.curSignState = request.cur_sign_state;
		this.completeState = request.complete;
		this.RefershUI();
		if (this.needshowday != -1)
		{
			this.OnClickDayItem(this.needshowday);
		}
		else if (this.curSignState)
		{
			this.OnClickDayItem(this.curSign);
		}
		else if (this.curSign < 7)
		{
			this.OnClickDayItem(this.curSign + 1);
		}
		else
		{
			this.OnClickDayItem(7);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Sign7", "open");
	}

	// Token: 0x06003DBB RID: 15803 RVA: 0x0011645C File Offset: 0x0011465C
	public void OnClickDayItem(int daynum)
	{
		this.curShowDay = daynum;
		this.UpdateBtnInfo();
		this.CalshowDayList(this.curShowDay - 1);
		this.ShowDayInfo();
		this.ShowModelVisual(this.curShowDay - 1);
	}

	// Token: 0x06003DBC RID: 15804 RVA: 0x00116498 File Offset: 0x00114698
	public void UpdateInfo(ret_sign_week.request request)
	{
		if (this.curSignState)
		{
			LocalDataSaveManager.SetRewardFlag();
		}
		this.curSign = (int)request.cur_sign;
		this.curSignState = request.cur_sign_state;
		if (this.curSign == 7 && !this.curSignState)
		{
			this.completeState = true;
		}
		this.RefershUI();
		this.ShowRewardUI();
		if (this.curSignState)
		{
			this.OnClickDayItem(this.curSign);
		}
		else if (this.curSign < 7)
		{
			this.OnClickDayItem(this.curSign + 1);
		}
		else
		{
			this.OnClickDayItem(7);
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Welfare", "Sign7", string.Format("require_{0}", this.curSign));
	}

	// Token: 0x06003DBD RID: 15805 RVA: 0x00116564 File Offset: 0x00114764
	public void ShowRewardUI()
	{
		this.CalshowDayList(this.curSign - 1);
		this.ShowDayItemList.Insert(0, this.curbestItem);
		SimpleRewardRootLogic.AddRewards(this.ShowDayItemList);
	}

	// Token: 0x06003DBE RID: 15806 RVA: 0x00116594 File Offset: 0x00114794
	public void RefershUI()
	{
		for (int i = 0; i < this.SevenDaysItems.Count; i++)
		{
			UnityVersionUtil.SetActiveRecursive(this.SevenDaysItems[i].gameObject, true);
			if (this.completeState)
			{
				this.SevenDaysItems[i].UpdataStateInfo(DayItemState.ISGET);
			}
			else if (i == this.curSign - 1)
			{
				if (this.curSignState)
				{
					this.SevenDaysItems[i].UpdataStateInfo(DayItemState.CURSIGN);
				}
				else
				{
					this.SevenDaysItems[i].UpdataStateInfo(DayItemState.ISGET);
				}
			}
			else if (i < this.curSign - 1)
			{
				this.SevenDaysItems[i].UpdataStateInfo(DayItemState.ISGET);
			}
			else
			{
				this.SevenDaysItems[i].UpdataStateInfo(DayItemState.NONE);
			}
		}
		this.SevenDayGrid.Reposition();
		this.DaySlider.value = (float)(this.curSign - 1) / 6f;
	}

	// Token: 0x06003DBF RID: 15807 RVA: 0x0011669C File Offset: 0x0011489C
	public void UpdateBtnInfo()
	{
		if (this.completeState)
		{
			this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
			UnityVersionUtil.SetActiveRecursive(this.dayinfoLabel.gameObject, false);
		}
		else if (this.curShowDay == this.curSign)
		{
			if (!this.curSignState)
			{
				this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
				this.btnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
				UnityVersionUtil.SetActiveRecursive(this.dayinfoLabel.gameObject, false);
			}
			else
			{
				this.BtnSp.spriteName = GameDefine.BtnIconNew[0];
				this.btnLabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
				UnityVersionUtil.SetActiveRecursive(this.dayinfoLabel.gameObject, true);
				this.dayinfoLabel.text = StrDictionary.GetDictionaryString("#{300904}", new object[0]);
			}
		}
		else if (this.curShowDay < this.curSign)
		{
			this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{300403}", new object[0]);
			UnityVersionUtil.SetActiveRecursive(this.dayinfoLabel.gameObject, false);
		}
		else
		{
			this.BtnSp.spriteName = GameDefine.BtnIconNew[1];
			this.btnLabel.text = StrDictionary.GetDictionaryString("#{300402}", new object[0]);
			if (this.curShowDay == this.curSign + 1)
			{
				UnityVersionUtil.SetActiveRecursive(this.dayinfoLabel.gameObject, true);
				this.dayinfoLabel.text = StrDictionary.GetDictionaryString("#{300902}", new object[0]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.dayinfoLabel.gameObject, false);
			}
		}
	}

	// Token: 0x06003DC0 RID: 15808 RVA: 0x0011688C File Offset: 0x00114A8C
	public void OnClickRecevieBtn()
	{
		if (this.curShowDay != this.curSign)
		{
			return;
		}
		if (!this.completeState && this.curSignState)
		{
			WaitResponseUIRootLogic.OpenWaitBox(255, 10f, 0f, null);
			sign_week.request request = new sign_week.request();
			request.day = (long)this.curSign;
			NetLogic.GetInstance().Send<Protocol.sign_week>(null, null);
		}
	}

	// Token: 0x06003DC1 RID: 15809 RVA: 0x001168F8 File Offset: 0x00114AF8
	public void ShowDayInfo()
	{
		int level = 0;
		if (this.curbestItem.ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.curbestItem.ItemData.SubType);
		}
		this.BestItem.UpdateItem(this.curbestItem, level, false);
		UnityVersionUtil.SetActiveRecursive(this.BestItem.gameObject, true);
		int num = this.ShowDayItemList.Count - this.rewardItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.rewardItemList[0].gameObject) as GameObject;
				RewardItem component = gameObject.GetComponent<RewardItem>();
				gameObject.name = string.Format("dailybuyitem{0:D2}", this.rewardItemList.Count);
				gameObject.transform.parent = this.DayGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				this.rewardItemList.Add(component);
			}
		}
		for (int j = 0; j < this.rewardItemList.Count; j++)
		{
			if (j < this.ShowDayItemList.Count)
			{
				int level2 = 0;
				if (this.ShowDayItemList[j].ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					level2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.MainPlayerAttrData.GetEquipEnhanceLevel(this.ShowDayItemList[j].ItemData.SubType);
				}
				this.rewardItemList[j].UpdateItem(this.ShowDayItemList[j], level2, false);
				UnityVersionUtil.SetActiveRecursive(this.rewardItemList[j].gameObject, true);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.rewardItemList[j].gameObject, false);
			}
		}
		this.DayGrid.Reposition();
	}

	// Token: 0x06003DC2 RID: 15810 RVA: 0x00116AF0 File Offset: 0x00114CF0
	private void OnDragModelBtn(GameObject btn, Vector2 delta)
	{
		if (this.mCurFakeObj != null && this.mCurFakeObj.FakeObj != null)
		{
			this.mCurFakeObj.FakeObj.transform.localEulerAngles -= delta.x * Vector3.up;
		}
	}

	// Token: 0x06003DC3 RID: 15811 RVA: 0x00116B50 File Offset: 0x00114D50
	public void OnDragCarModelPic(GameObject btn, Vector2 delta)
	{
		if (this.CarMeshRoot != null)
		{
			this.CarMeshRoot.transform.localEulerAngles -= new Vector3(0f, delta.x, 0f);
		}
	}

	// Token: 0x06003DC4 RID: 15812 RVA: 0x00116BA0 File Offset: 0x00114DA0
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

	// Token: 0x06003DC5 RID: 15813 RVA: 0x00116BC4 File Offset: 0x00114DC4
	private void OnDisable()
	{
		this.ResetNormalLight();
		this.UnLoadFakeObj();
	}

	// Token: 0x06003DC6 RID: 15814 RVA: 0x00116BD4 File Offset: 0x00114DD4
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
		if (this.FakeItemObj != null)
		{
			Object.Destroy(this.FakeItemObj);
		}
	}

	// Token: 0x06003DC7 RID: 15815 RVA: 0x00116C54 File Offset: 0x00114E54
	public void CalshowDayList(int dayi)
	{
		this.ShowDayItemList.Clear();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		switch (playerData.Profession)
		{
		case PROFESSION_TYPE.XD:
			this.curbestItem = new GameItem(this.SignDataList[dayi].XDItemID1, (EQUIP_QUALITY)this.SignDataList[dayi].XDItemQuality1, this.SignDataList[dayi].XDItemCount1);
			break;
		case PROFESSION_TYPE.QJ:
			this.curbestItem = new GameItem(this.SignDataList[dayi].QJItemID1, (EQUIP_QUALITY)this.SignDataList[dayi].QJItemQuality1, this.SignDataList[dayi].QJItemCount1);
			break;
		case PROFESSION_TYPE.NQS:
			this.curbestItem = new GameItem(this.SignDataList[dayi].NQSItemID1, (EQUIP_QUALITY)this.SignDataList[dayi].NQSItemQuality1, this.SignDataList[dayi].NQSItemCount1);
			break;
		}
		if (!string.IsNullOrEmpty(this.SignDataList[dayi].ItemID2))
		{
			this.ShowDayItemList.Add(new GameItem(this.SignDataList[dayi].ItemID2, (EQUIP_QUALITY)this.SignDataList[dayi].ItemQuality2, this.SignDataList[dayi].ItemCount2));
		}
		if (!string.IsNullOrEmpty(this.SignDataList[dayi].ItemID3))
		{
			this.ShowDayItemList.Add(new GameItem(this.SignDataList[dayi].ItemID3, (EQUIP_QUALITY)this.SignDataList[dayi].ItemQuality3, this.SignDataList[dayi].ItemCount3));
		}
		if (!string.IsNullOrEmpty(this.SignDataList[dayi].ItemID4))
		{
			this.ShowDayItemList.Add(new GameItem(this.SignDataList[dayi].ItemID4, (EQUIP_QUALITY)this.SignDataList[dayi].ItemQuality4, this.SignDataList[dayi].ItemCount4));
		}
		if (!string.IsNullOrEmpty(this.SignDataList[dayi].ItemID5))
		{
			this.ShowDayItemList.Add(new GameItem(this.SignDataList[dayi].ItemID5, (EQUIP_QUALITY)this.SignDataList[dayi].ItemQuality5, this.SignDataList[dayi].ItemCount5));
		}
	}

	// Token: 0x06003DC8 RID: 15816 RVA: 0x00116ED8 File Offset: 0x001150D8
	public void GetDayBestItem(int dayi)
	{
		this.curshowdata = null;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		switch (playerData.Profession)
		{
		case PROFESSION_TYPE.XD:
			this.curbestItem = new GameItem(this.SignDataList[dayi].XDItemID1, (EQUIP_QUALITY)this.SignDataList[dayi].XDItemQuality1, this.SignDataList[dayi].XDItemCount1);
			if (!string.IsNullOrEmpty(this.SignDataList[dayi].XDShowModelID))
			{
				this.curshowdata = DataManager.GetShowModelDataById(this.SignDataList[dayi].XDShowModelID);
			}
			break;
		case PROFESSION_TYPE.QJ:
			this.curbestItem = new GameItem(this.SignDataList[dayi].QJItemID1, (EQUIP_QUALITY)this.SignDataList[dayi].QJItemQuality1, this.SignDataList[dayi].QJItemCount1);
			if (!string.IsNullOrEmpty(this.SignDataList[dayi].QJShowModelID))
			{
				this.curshowdata = DataManager.GetShowModelDataById(this.SignDataList[dayi].QJShowModelID);
			}
			break;
		case PROFESSION_TYPE.NQS:
			this.curbestItem = new GameItem(this.SignDataList[dayi].NQSItemID1, (EQUIP_QUALITY)this.SignDataList[dayi].NQSItemQuality1, this.SignDataList[dayi].NQSItemCount1);
			if (!string.IsNullOrEmpty(this.SignDataList[dayi].NQSShowModelID))
			{
				this.curshowdata = DataManager.GetShowModelDataById(this.SignDataList[dayi].NQSShowModelID);
			}
			break;
		}
	}

	// Token: 0x0400294A RID: 10570
	public UIEventListener RotateModelBtnListener;

	// Token: 0x0400294B RID: 10571
	private FakeObjLogic mCurFakeObj;

	// Token: 0x0400294C RID: 10572
	public UITexture ModelPic;

	// Token: 0x0400294D RID: 10573
	public UISlider DaySlider;

	// Token: 0x0400294E RID: 10574
	private int curSign;

	// Token: 0x0400294F RID: 10575
	private bool curSignState;

	// Token: 0x04002950 RID: 10576
	private bool completeState;

	// Token: 0x04002951 RID: 10577
	private List<SignInWeekData> SignDataList;

	// Token: 0x04002952 RID: 10578
	private SignInWeekData curSigninWeekData;

	// Token: 0x04002953 RID: 10579
	private ShowModelData curshowdata;

	// Token: 0x04002954 RID: 10580
	public UIGrid SevenDayGrid;

	// Token: 0x04002955 RID: 10581
	public List<WeekDayRewardItem> SevenDaysItems;

	// Token: 0x04002956 RID: 10582
	private List<GameItem> SevenShowItemList = new List<GameItem>();

	// Token: 0x04002957 RID: 10583
	public RewardItem BestItem;

	// Token: 0x04002958 RID: 10584
	public UIGrid DayGrid;

	// Token: 0x04002959 RID: 10585
	public List<RewardItem> rewardItemList;

	// Token: 0x0400295A RID: 10586
	private List<GameItem> ShowDayItemList = new List<GameItem>();

	// Token: 0x0400295B RID: 10587
	private GameItem curbestItem;

	// Token: 0x0400295C RID: 10588
	public UISprite BtnSp;

	// Token: 0x0400295D RID: 10589
	public UILabel btnLabel;

	// Token: 0x0400295E RID: 10590
	public UILabel dayinfoLabel;

	// Token: 0x0400295F RID: 10591
	public UITexture CarModelPic;

	// Token: 0x04002960 RID: 10592
	private Transform CarMeshRoot;

	// Token: 0x04002961 RID: 10593
	private int curShowDay;

	// Token: 0x04002962 RID: 10594
	private int needshowday;

	// Token: 0x04002963 RID: 10595
	private Color ambientLight;

	// Token: 0x04002964 RID: 10596
	private GameObject FakeItemObj;
}
