using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x0200096A RID: 2410
public class PlayerCarRootLogic : SingletonUnity<PlayerCarRootLogic>
{
	// Token: 0x060043AA RID: 17322 RVA: 0x0014EF64 File Offset: 0x0014D164
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x060043AB RID: 17323 RVA: 0x0014EF70 File Offset: 0x0014D170
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x060043AC RID: 17324 RVA: 0x0014EFA0 File Offset: 0x0014D1A0
	private void BreakTutorial()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.CloseTutorial();
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x17000FA5 RID: 4005
	// (get) Token: 0x060043AD RID: 17325 RVA: 0x0014EFBC File Offset: 0x0014D1BC
	// (set) Token: 0x060043AE RID: 17326 RVA: 0x0014EFC4 File Offset: 0x0014D1C4
	public Dictionary<string, mount> CurMountInfoDic
	{
		get
		{
			return this.mCurMountInfoDic;
		}
		set
		{
			this.mCurMountInfoDic = value;
		}
	}

	// Token: 0x060043AF RID: 17327 RVA: 0x0014EFD0 File Offset: 0x0014D1D0
	public void EnableReset()
	{
		NGUITools.SetActive(this.NoCarTipObj, false);
		NGUITools.SetActive(this.HaveCarTipObj, false);
		NGUITools.SetActive(this.EquipedCarTipObj, false);
		NGUITools.SetActive(this.GetBtn.gameObject, false);
		NGUITools.SetActive(this.EquipBtn.gameObject, false);
	}

	// Token: 0x060043B0 RID: 17328 RVA: 0x0014F024 File Offset: 0x0014D224
	public void Reset(ret_mount_info.request request)
	{
		this.mCurMountInfoDic = request.mount_info;
		this.mCurMountInfoList = new List<mount>(this.mCurMountInfoDic.Values);
		this.mCurMountInfoList.Sort((mount pre, mount next) => pre.ID.CompareTo(next.ID));
		ItemContainer itemBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
		for (int i = this.mCurMountInfoList.Count - 1; i >= 0; i--)
		{
			if (this.mCurMountInfoList[i].state == 0L)
			{
				MountData mountDataById = DataManager.GetMountDataById(this.mCurMountInfoList[i].ID);
				if (mountDataById.NeedShow == 0)
				{
					this.mCurMountInfoList.RemoveAt(i);
				}
				else if (!string.IsNullOrEmpty(mountDataById.StartTime) && !TimeTools.IsTimeRange(mountDataById.StartTimeList))
				{
					List<GameItem> itemByItemId = itemBackPack.GetItemByItemId(mountDataById.ItemID);
					if (itemByItemId == null || itemByItemId.Count == 0)
					{
						this.mCurMountInfoList.RemoveAt(i);
					}
				}
			}
		}
		int num = this.mCurMountInfoList.Count - this.CarIconList.Count;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = Object.Instantiate(this.CarIconList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.CarIconList[0].transform.parent;
				gameObject.gameObject.name = string.Format("{0:D2}", this.CarIconList.Count);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				this.CarIconList.Add(gameObject.GetComponent<PlayerCarIconLogic>());
			}
		}
		this.CarIconRootGride.Reposition();
		for (int k = 0; k < this.CarIconList.Count; k++)
		{
			if (k < this.mCurMountInfoList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.CarIconList[k].gameObject, true);
				this.CarIconList[k].Reset(this.mCurMountInfoList[k], new DelegateDefine.StringGameObjectDelegate(this.OnClickCarIcon));
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.CarIconList[k].gameObject, false);
			}
		}
		vp_Timer.In(Time.deltaTime * 2f, delegate()
		{
			this.setbegin();
		}, null);
	}

	// Token: 0x060043B1 RID: 17329 RVA: 0x0014F2D0 File Offset: 0x0014D4D0
	public void setbegin()
	{
		bool flag = false;
		for (int i = 0; i < this.mCurMountInfoList.Count; i++)
		{
			if (this.mCurMountInfoList[i].state == 2L)
			{
				this.CarIconList[i].OnClickBtn();
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			this.CarIconList[0].OnClickBtn();
		}
	}

	// Token: 0x060043B2 RID: 17330 RVA: 0x0014F344 File Offset: 0x0014D544
	private void ResetFakeCarObjRoot()
	{
		FakeCarObjRootLogic instance = SingletonUnity<FakeCarObjRootLogic>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeCarObjRoot");
			instance = SingletonUnity<FakeCarObjRootLogic>.Instance;
		}
		this.MeshRoot = instance.MeshRoot;
		SingletonUnity<FakeCarObjRootLogic>.Instance.EnableFakeObjRoot();
		SingletonUnity<FakeCarObjRootLogic>.Instance.PlayRotate();
		this.CarModelPic.mainTexture = instance.ModelPic;
	}

	// Token: 0x060043B3 RID: 17331 RVA: 0x0014F3A8 File Offset: 0x0014D5A8
	public void UpdateCarPage(ret_mount_equip.request request)
	{
		this.mCurMountInfoDic = request.mount_info;
		this.mCurMountInfoList = new List<mount>(this.mCurMountInfoDic.Values);
		this.mCurMountInfoList.Sort((mount pre, mount next) => pre.ID.CompareTo(next.ID));
		for (int i = 0; i < this.CarIconList.Count; i++)
		{
			if (this.CarIconList[i].CurCarId.Equals(request.ID))
			{
				this.CarIconList[i].OnClickBtn();
				break;
			}
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Vehicle", string.Format("Car_{0}", request.ID), "equip");
	}

	// Token: 0x060043B4 RID: 17332 RVA: 0x0014F478 File Offset: 0x0014D678
	public void UpdateCarPage(ret_buy_car_shop.request request)
	{
		if (request.HasMountId)
		{
			if (this.mCurMountInfoDic.ContainsKey(request.mountId))
			{
				this.mCurMountInfoDic[request.mountId].state = request.state;
				this.mCurMountInfoList = new List<mount>(this.mCurMountInfoDic.Values);
				this.mCurMountInfoList.Sort((mount pre, mount next) => pre.ID.CompareTo(next.ID));
			}
			for (int i = 0; i < this.CarIconList.Count; i++)
			{
				if (this.CarIconList[i].CurCarId.Equals(request.mountId))
				{
					this.CarIconList[i].OnClickBtn();
					break;
				}
			}
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Vehicle", string.Format("Car_{0}", request.mountId), "Buy");
		}
	}

	// Token: 0x060043B5 RID: 17333 RVA: 0x0014F578 File Offset: 0x0014D778
	public void UpdateCarPage(string carId)
	{
		for (int i = 0; i < this.CarIconList.Count; i++)
		{
			if (this.CarIconList[i].CurCarId.Equals(carId))
			{
				this.CarIconList[i].OnClickBtn();
			}
		}
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Vehicle", string.Format("Car_{0}", carId), "get");
	}

	// Token: 0x060043B6 RID: 17334 RVA: 0x0014F5F0 File Offset: 0x0014D7F0
	public void OnClickCarIcon(string id, GameObject iconObj)
	{
		if (string.IsNullOrEmpty(id))
		{
			return;
		}
		this.ChoosedTargetPic.transform.parent = iconObj.transform;
		this.ChoosedTargetPic.transform.localPosition = Vector3.zero;
		this.CenterOnChild.CenterOn(iconObj.transform);
		MountData mountDataById = DataManager.GetMountDataById(id);
		this.CarNameLabel.text = StrDictionary.GetDictionaryString(mountDataById.CarName, new object[0]);
		this.CarDescriptionLabel.text = StrDictionary.GetDictionaryString(mountDataById.Desc, new object[0]);
		this.AttFlag[0].spriteName = GameDefine.GetAttributeIcon(mountDataById.Status1);
		this.AttFlag[1].spriteName = GameDefine.GetAttributeIcon(mountDataById.Status2);
		this.AttFlag[2].spriteName = GameDefine.GetAttributeIcon(mountDataById.Status3);
		this.AttFlag[3].spriteName = GameDefine.GetAttributeIcon(mountDataById.Status4);
		this.AttrNameLabel[0].text = GameDefine.GetAttributeName_S(mountDataById.Status1);
		this.AttrNameLabel[1].text = GameDefine.GetAttributeName_S(mountDataById.Status2);
		this.AttrNameLabel[2].text = GameDefine.GetAttributeName_S(mountDataById.Status3);
		this.AttrNameLabel[3].text = GameDefine.GetAttributeName_S(mountDataById.Status4);
		this.AttrValLabel[0].text = GameDefine.GetAttributeValueStr(mountDataById.Status1, mountDataById.Value1);
		this.AttrValLabel[1].text = GameDefine.GetAttributeValueStr(mountDataById.Status2, mountDataById.Value2);
		this.AttrValLabel[2].text = GameDefine.GetAttributeValueStr(mountDataById.Status3, mountDataById.Value3);
		this.AttrValLabel[3].text = GameDefine.GetAttributeValueStr(mountDataById.Status4, mountDataById.Value4);
		this.curMountInfo = this.mCurMountInfoDic[id];
		this.mCurMountData = mountDataById;
		this.SteerAngleLabel.text = string.Format("{0}", this.mCurMountData.MaxSteerAngle);
		this.SteerAngleSlider.value = ((float)this.MaxSteerValue - this.mCurMountData.MaxSteerAngle) / (float)this.MaxSteerValue;
		this.MaxSpeedLabel.text = string.Format("{0}", this.mCurMountData.MaxSp);
		this.MaxSpeedSlider.value = (float)this.mCurMountData.MaxSpeed / (float)this.MaxSpeedValue;
		this.HpLabel.text = string.Format("{0}", this.mCurMountData.MaxHP);
		this.HpSlider.value = (float)this.mCurMountData.MaxHP / (float)this.MaxHPValue;
		this.AccLabel.text = string.Format("{0}", this.mCurMountData.MaxAcce);
		this.AccSlider.value = (float)this.mCurMountData.MaxAcceleration / (float)this.MaxAccValue;
		long state = this.curMountInfo.state;
		if (state >= 0L && state <= 3L)
		{
			switch ((int)state)
			{
			case 0:
				NGUITools.SetActive(this.NoCarTipObj, true);
				NGUITools.SetActive(this.HaveCarTipObj, false);
				NGUITools.SetActive(this.EquipedCarTipObj, false);
				NGUITools.SetActive(this.GetBtn.gameObject, true);
				NGUITools.SetActive(this.EquipBtn.gameObject, false);
				this.UpdateBtnTips();
				break;
			case 1:
				NGUITools.SetActive(this.NoCarTipObj, false);
				NGUITools.SetActive(this.HaveCarTipObj, true);
				NGUITools.SetActive(this.EquipedCarTipObj, false);
				NGUITools.SetActive(this.GetBtn.gameObject, false);
				NGUITools.SetActive(this.EquipBtn.gameObject, true);
				break;
			case 2:
				NGUITools.SetActive(this.NoCarTipObj, false);
				NGUITools.SetActive(this.HaveCarTipObj, false);
				NGUITools.SetActive(this.EquipedCarTipObj, true);
				NGUITools.SetActive(this.GetBtn.gameObject, false);
				NGUITools.SetActive(this.EquipBtn.gameObject, false);
				break;
			case 3:
			{
				NGUITools.SetActive(this.NoCarTipObj, false);
				NGUITools.SetActive(this.HaveCarTipObj, true);
				NGUITools.SetActive(this.EquipedCarTipObj, false);
				NGUITools.SetActive(this.GetBtn.gameObject, false);
				NGUITools.SetActive(this.EquipBtn.gameObject, true);
				change_mount_state.request request = new change_mount_state.request();
				request.ID = this.curMountInfo.ID;
				NetLogic.GetInstance().Send<Protocol.change_mount_state>(request, null);
				break;
			}
			}
		}
		this.curColorSelectId = this.curMountInfo.select;
		this.curColorData = DataManager.GetColorDataById(this.curMountInfo.select);
		this.ResetCarModelVisual(mountDataById, this.curColorData);
		this.ShowColorInfo();
	}

	// Token: 0x060043B7 RID: 17335 RVA: 0x0014FAB4 File Offset: 0x0014DCB4
	public void ShowColorInfo()
	{
		this.curColorlist.Clear();
		this.mCurColorInfoDic = this.curMountInfo.colors;
		this.curColorlist = new List<color>(this.mCurColorInfoDic.Values);
		this.curColorlist.Sort(delegate(color pre, color next)
		{
			if (pre.ID.Length != next.ID.Length)
			{
				return pre.ID.Length - next.ID.Length;
			}
			return pre.ID.CompareTo(next.ID);
		});
		int num = this.curColorlist.Count - this.ColorIconList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.ColorIconList[0].gameObject) as GameObject;
				gameObject.transform.parent = this.ColorIconList[0].transform.parent;
				gameObject.gameObject.name = string.Format("Color{0:D2}", this.ColorIconList.Count);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				this.ColorIconList.Add(gameObject.GetComponent<ColorItemLogic>());
			}
			this.ColorGride.Reposition();
		}
		for (int j = 0; j < this.ColorIconList.Count; j++)
		{
			if (j < this.curColorlist.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.ColorIconList[j].gameObject, true);
				this.ColorIconList[j].Reset(this.curColorlist[j], this.curColorSelectId, new DelegateDefine.StringGameObjectDelegate(this.OnClickColorItem));
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.ColorIconList[j].gameObject, false);
			}
		}
		NGUITools.SetActive(this.buycolorBtn, false);
		this.ChangeCarColor();
	}

	// Token: 0x060043B8 RID: 17336 RVA: 0x0014FC8C File Offset: 0x0014DE8C
	public void OnClickColorItem(string id, GameObject colorObj)
	{
		if (this.curColorSelectId.Equals(id))
		{
			Debug.Log("id ==:" + id);
			return;
		}
		this.curColorSelectId = id;
		color color = this.curMountInfo.colors[id];
		this.curColorData = DataManager.GetColorDataById(color.ID);
		if (this.curMountInfo.state == 2L)
		{
			if (color.state == 0L)
			{
				NGUITools.SetActive(this.buycolorBtn, true);
				this.ColorPriceSp.spriteName = GameMoneyHelper.GetMoneyIcon((long)this.curColorData.PriceType);
				this.ColorPriceLabel.text = this.curColorData.PriceCost.ToString();
			}
			else
			{
				NGUITools.SetActive(this.buycolorBtn, false);
				if (!this.curMountInfo.select.Equals(id))
				{
					this.ChangeColor();
				}
			}
		}
		else
		{
			NGUITools.SetActive(this.buycolorBtn, false);
		}
		for (int i = 0; i < this.ColorIconList.Count; i++)
		{
			this.ColorIconList[i].refersh(this.curColorSelectId);
		}
		this.ChangeCarColor();
	}

	// Token: 0x060043B9 RID: 17337 RVA: 0x0014FDBC File Offset: 0x0014DFBC
	public void OnClickColorbuyBtn()
	{
		this.OkBuyColor();
	}

	// Token: 0x060043BA RID: 17338 RVA: 0x0014FDC4 File Offset: 0x0014DFC4
	public void OkBuyColor()
	{
		if (GameMoneyHelper.BeforeCheckBuy(this.curColorData.PriceType, this.curColorData.PriceCost))
		{
			WaitResponseUIRootLogic.OpenWaitBox(241, 10f, 0f, null);
			mount_use_color.request request = new mount_use_color.request();
			request.mountId = this.curMountInfo.ID;
			request.colorId = this.curColorSelectId;
			NetLogic.GetInstance().Send<Protocol.mount_use_color>(request, null);
		}
	}

	// Token: 0x060043BB RID: 17339 RVA: 0x0014FE38 File Offset: 0x0014E038
	public void ChangeColor()
	{
		WaitResponseUIRootLogic.OpenWaitBox(241, 10f, 0f, null);
		mount_use_color.request request = new mount_use_color.request();
		request.mountId = this.curMountInfo.ID;
		request.colorId = this.curColorSelectId;
		NetLogic.GetInstance().Send<Protocol.mount_use_color>(request, null);
	}

	// Token: 0x060043BC RID: 17340 RVA: 0x0014FE8C File Offset: 0x0014E08C
	public void buyColorSuccess(ret_mount_use_color.request request)
	{
		this.mCurMountInfoDic = request.mount_info;
		this.mCurMountInfoList = new List<mount>(this.mCurMountInfoDic.Values);
		this.mCurMountInfoList.Sort((mount pre, mount next) => pre.ID.CompareTo(next.ID));
		if (this.curMountInfo.ID.Equals(request.mountId))
		{
			this.curMountInfo = this.mCurMountInfoDic[request.mountId];
		}
		this.ShowColorInfo();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Vehicle", string.Format("Car_{0}", request.mountId), string.Format("color_{0}", request.colorId));
	}

	// Token: 0x060043BD RID: 17341 RVA: 0x0014FF4C File Offset: 0x0014E14C
	public void ChangeCarColor()
	{
		ColorData colorDataById = DataManager.GetColorDataById(this.curColorSelectId);
		if (SingletonUnity<FakeCarObjRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<FakeCarObjRootLogic>.Instance.gameObject))
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.ChangeColor(colorDataById);
		}
	}

	// Token: 0x060043BE RID: 17342 RVA: 0x0014FF90 File Offset: 0x0014E190
	private void ResetCarModelVisual(MountData mountData, ColorData colorda)
	{
		SingletonUnity<FakeCarObjRootLogic>.Instance.InitFakeCarObj(mountData, colorda);
	}

	// Token: 0x060043BF RID: 17343 RVA: 0x0014FFA0 File Offset: 0x0014E1A0
	public void OnDragPic(GameObject btn, Vector2 delta)
	{
		if (this.MeshRoot != null)
		{
			this.MeshRoot.transform.localEulerAngles -= new Vector3(0f, delta.x, 0f);
		}
	}

	// Token: 0x060043C0 RID: 17344 RVA: 0x0014FFF0 File Offset: 0x0014E1F0
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

	// Token: 0x060043C1 RID: 17345 RVA: 0x00150014 File Offset: 0x0014E214
	public void OnClickEquipBtn()
	{
		mount_equip.request request = new mount_equip.request();
		request.ID = this.mCurMountData.ID;
		NetLogic.GetInstance().Send<Protocol.mount_equip>(request, null);
		WaitResponseUIRootLogic.OpenWaitBox(236, 10f, 0f, null);
		if (Singleton<ObjManager>.Instance.MainPlayer != null)
		{
			Singleton<ObjManager>.Instance.MainPlayer.ChangeMountCar(this.mCurMountData.ID, this.curMountInfo.select);
			Singleton<ObjManager>.Instance.MainPlayer.MountId = this.mCurMountData.ID;
		}
	}

	// Token: 0x060043C2 RID: 17346 RVA: 0x001500B0 File Offset: 0x0014E2B0
	private void UpdateBtnTips()
	{
		bool flag = false;
		ItemContainer itemBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(itemBackPack, false, GameDefine.ITEM_TYPE.EXCHANGE, false, PROFESSION_TYPE.INVALID);
		if (targetTypeItem == null || targetTypeItem.Count == 0)
		{
			flag = false;
		}
		for (int i = 0; i < targetTypeItem.Count; i++)
		{
			if (targetTypeItem[i].ItemData.Function.ToString().Equals(this.mCurMountData.ID))
			{
				flag = true;
				break;
			}
		}
		NGUITools.SetActive(this.BtnHaveTips, flag);
		if (flag)
		{
			this.GetBtnLabel.text = StrDictionary.GetDictionaryString("#{101405}", new object[0]);
			this.BuyPriceLabel.text = string.Empty;
			this.GetBtn.transform.localPosition = new Vector3(246f, -111.1f, 0f);
		}
		else if (this.mCurMountData.PriceType != -1)
		{
			this.GetBtnLabel.text = StrDictionary.GetDictionaryString("#{300601}", new object[0]);
			this.BuyPriceLabel.text = GameMoneyHelper.GetMoneyValStr(this.mCurMountData.Price, this.mCurMountData.PriceType);
			this.GetBtn.transform.localPosition = new Vector3(305f, -111.1f, 0f);
		}
		else
		{
			this.GetBtnLabel.text = StrDictionary.GetDictionaryString("#{101405}", new object[0]);
			this.BuyPriceLabel.text = string.Empty;
			this.GetBtn.transform.localPosition = new Vector3(246f, -111.1f, 0f);
		}
	}

	// Token: 0x060043C3 RID: 17347 RVA: 0x00150274 File Offset: 0x0014E474
	public void OnClickGetBtn()
	{
		ItemContainer itemBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(itemBackPack, false, GameDefine.ITEM_TYPE.EXCHANGE, false, PROFESSION_TYPE.INVALID);
		bool flag = false;
		GameItem exchangeItem = null;
		if (targetTypeItem == null || targetTypeItem.Count == 0)
		{
			flag = false;
		}
		else
		{
			for (int i = 0; i < targetTypeItem.Count; i++)
			{
				if (targetTypeItem[i].ItemData.Function.ToString().Equals(this.mCurMountData.ID))
				{
					flag = true;
					exchangeItem = targetTypeItem[i];
					break;
				}
			}
		}
		if (flag)
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{101408}", new object[]
			{
				exchangeItem.ItemData.MName
			}), StrDictionary.GetDictionaryString("#{101405}", new object[0]), delegate
			{
				use_item.request request2 = new use_item.request();
				request2.indexId = exchangeItem.IndexId;
				NetLogic.GetInstance().Send<Protocol.use_item>(request2, null);
				WaitResponseUIRootLogic.OpenWaitBox(115, 10f, 0f, null);
			}, null, null, null);
			if (TutorialManager.CurStep == TUTORIAL_STEP.CAR_CLICK_ACQUIRE)
			{
				this.CheckTutorialEvent();
			}
		}
		else
		{
			if (this.mCurMountData.PriceType == -1)
			{
				this.ShowGetPath();
			}
			else if (GameMoneyHelper.BeforeCheckBuy(this.mCurMountData.PriceType, this.mCurMountData.Price))
			{
				WaitResponseUIRootLogic.OpenWaitBox(323, 10f, 0f, null);
				buy_car_shop.request request = new buy_car_shop.request();
				request.mountId = this.mCurMountData.ID;
				NetLogic.GetInstance().Send<Protocol.buy_car_shop>(request, null);
			}
			this.BreakTutorial();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.CAR);
		}
	}

	// Token: 0x060043C4 RID: 17348 RVA: 0x00150414 File Offset: 0x0014E614
	public void ShowGetPath()
	{
		switch (this.mCurMountData.GetPath)
		{
		case 2:
			if (this.CheckPathOpen(FUNCTION_TYPE.GIFT_7DAY))
			{
				NoticeLogic.AddNotifyData(this.mCurMountData.GetDesc, true, false);
				this.OnClickCloseBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.CommercialUIRoot, delegate
				{
					SingletonUnity<CommercialUIRootLogic>.Instance.OnClickWeekBtn();
				}, null);
			}
			break;
		case 3:
			if (this.CheckPathOpen(FUNCTION_TYPE.SHOP))
			{
				GameMoneyHelper.ShowItemProduct(this.mCurMountData.ItemID, GameDefine.SHOP_TYPE.EQUIP_SHOP);
			}
			break;
		case 4:
			if (this.CheckPathOpen(FUNCTION_TYPE.LOTTO))
			{
				this.OnClickCloseBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.SlotUIRoot, delegate
				{
					SingletonUnity<SlotUIRootLogic>.Instance.EnableReset();
					WaitResponseUIRootLogic.OpenWaitBox(242, 10f, 0f, null);
					NetLogic.GetInstance().Send<Protocol.request_slot_info>(null, null);
				}, null);
			}
			break;
		case 5:
			if (this.CheckPathOpen(FUNCTION_TYPE.MYSTERYSHOP))
			{
				this.OnClickCloseBtn();
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MysteryShopRoot, delegate
				{
					SingletonUnity<MysteryShopRootLogic>.Instance.EnableReset();
					WaitResponseUIRootLogic.OpenWaitBox(274, 10f, 0f, null);
					NetLogic.GetInstance().Send<Protocol.request_special_big_pack>(null, null);
					SingletonUnity<MysteryShopRootLogic>.Instance.TargetID = "2";
				}, null);
			}
			break;
		case 6:
			if (string.IsNullOrEmpty(this.mCurMountData.StartTime))
			{
				if (this.CheckPathOpen(FUNCTION_TYPE.BIGSALES) && SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.Big_PackFlag)
				{
					this.OnClickCloseBtn();
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BigPackRoot, delegate
					{
						SingletonUnity<BigPackRootLogic>.Instance.EnableReset();
						WaitResponseUIRootLogic.OpenWaitBox(260, 10f, 0f, null);
						NetLogic.GetInstance().Send<Protocol.request_big_pack>(null, null);
					}, null);
				}
			}
			else if (TimeTools.IsTimeRange(this.mCurMountData.StartTimeList, this.mCurMountData.EndTimeList))
			{
				if (this.CheckPathOpen(FUNCTION_TYPE.BIGSALES) && SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.Big_PackFlag)
				{
					this.OnClickCloseBtn();
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BigPackRoot, delegate
					{
						SingletonUnity<BigPackRootLogic>.Instance.EnableReset();
						WaitResponseUIRootLogic.OpenWaitBox(260, 10f, 0f, null);
						NetLogic.GetInstance().Send<Protocol.request_big_pack>(null, null);
					}, null);
				}
			}
			else
			{
				NoticeLogic.AddNotifyData("#{202001}", true, false);
			}
			break;
		}
	}

	// Token: 0x060043C5 RID: 17349 RVA: 0x00150668 File Offset: 0x0014E868
	private bool CheckPathOpen(FUNCTION_TYPE curtype)
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (!playerCommonData.IsFunctionUnlock(curtype))
		{
			int num = (int)curtype;
			int condition = DataManager.GetFunctionDataById(num.ToString()).Condition;
			NoticeLogic.AddNotifyData(StrDictionary.GetDictionaryString("#{100154}", new object[]
			{
				condition
			}), true, false);
			return false;
		}
		return true;
	}

	// Token: 0x060043C6 RID: 17350 RVA: 0x001506C4 File Offset: 0x0014E8C4
	public bool IsBackPackHaveCarTicket()
	{
		ItemContainer itemBackPack = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ItemBackPack;
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(itemBackPack, false, GameDefine.ITEM_TYPE.EXCHANGE, false, PROFESSION_TYPE.INVALID);
		bool result = false;
		if (targetTypeItem == null || targetTypeItem.Count == 0)
		{
			result = false;
		}
		else
		{
			for (int i = 0; i < targetTypeItem.Count; i++)
			{
				if (targetTypeItem[i].ItemData.Function.ToString().Equals(this.mCurMountData.ID))
				{
					result = true;
					GameItem gameItem = targetTypeItem[i];
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x060043C7 RID: 17351 RVA: 0x00150764 File Offset: 0x0014E964
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(null, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), true, StrDictionary.GetDictionaryString("#{100121}", new object[0]));
		}, null);
		this.CarPicDragListener.onDrag = new UIEventListener.VectorDelegate(this.OnDragPic);
		this.CarPicDragListener.onPress = new UIEventListener.BoolDelegate(this.OnPressCarModelPic);
		this.ResetFakeCarObjRoot();
		this.ambientLight = RenderSettings.ambientLight;
		RenderSettings.ambientLight = new Color(0f, 0f, 0f, 1f);
	}

	// Token: 0x060043C8 RID: 17352 RVA: 0x001507EC File Offset: 0x0014E9EC
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerCarRoot);
		if (SingletonUnity<FakeCarObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeCarObjRootLogic>.Instance.DisableFakeObjRoot();
		}
		RenderSettings.ambientLight = this.ambientLight;
	}

	// Token: 0x060043C9 RID: 17353 RVA: 0x0015083C File Offset: 0x0014EA3C
	public void OnClickLeftBtn()
	{
		int num = 0;
		for (int i = 0; i < this.CarIconList.Count; i++)
		{
			if (this.CarIconList[i].CurCarId.Equals(this.curMountInfo.ID))
			{
				num = i;
			}
		}
		if (num > 0)
		{
			this.OnClickCarIcon(this.CarIconList[num - 1].CurCarId, this.CarIconList[num - 1].gameObject);
		}
	}

	// Token: 0x060043CA RID: 17354 RVA: 0x001508C4 File Offset: 0x0014EAC4
	public void OnClickRightBtn()
	{
		int num = 0;
		for (int i = 0; i < this.CarIconList.Count; i++)
		{
			if (this.CarIconList[i].CurCarId.Equals(this.curMountInfo.ID))
			{
				num = i;
			}
		}
		if (num < this.CarIconList.Count - 1)
		{
			this.OnClickCarIcon(this.CarIconList[num + 1].CurCarId, this.CarIconList[num + 1].gameObject);
		}
	}

	// Token: 0x0400304B RID: 12363
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x0400304C RID: 12364
	public UILabel CarNameLabel;

	// Token: 0x0400304D RID: 12365
	public UILabel CarDescriptionLabel;

	// Token: 0x0400304E RID: 12366
	public UISprite[] AttFlag;

	// Token: 0x0400304F RID: 12367
	public UILabel[] AttrNameLabel;

	// Token: 0x04003050 RID: 12368
	public UILabel[] AttrValLabel;

	// Token: 0x04003051 RID: 12369
	public List<PlayerCarIconLogic> CarIconList;

	// Token: 0x04003052 RID: 12370
	public UICenterOnChild CenterOnChild;

	// Token: 0x04003053 RID: 12371
	public UIGrid CarIconRootGride;

	// Token: 0x04003054 RID: 12372
	public UISprite ChoosedTargetPic;

	// Token: 0x04003055 RID: 12373
	public GameObject HaveCarTipObj;

	// Token: 0x04003056 RID: 12374
	public GameObject NoCarTipObj;

	// Token: 0x04003057 RID: 12375
	public GameObject EquipedCarTipObj;

	// Token: 0x04003058 RID: 12376
	public UIWidget EquipBtn;

	// Token: 0x04003059 RID: 12377
	public GameObject buycolorBtn;

	// Token: 0x0400305A RID: 12378
	public UISprite ColorPriceSp;

	// Token: 0x0400305B RID: 12379
	public UILabel ColorPriceLabel;

	// Token: 0x0400305C RID: 12380
	public UIWidget GetBtn;

	// Token: 0x0400305D RID: 12381
	public UITexture CarModelPic;

	// Token: 0x0400305E RID: 12382
	public Transform MeshRoot;

	// Token: 0x0400305F RID: 12383
	public UIEventListener CarPicDragListener;

	// Token: 0x04003060 RID: 12384
	public GameObject BtnHaveTips;

	// Token: 0x04003061 RID: 12385
	private List<mount> mCurMountInfoList;

	// Token: 0x04003062 RID: 12386
	private Dictionary<string, mount> mCurMountInfoDic;

	// Token: 0x04003063 RID: 12387
	private Dictionary<string, color> mCurColorInfoDic;

	// Token: 0x04003064 RID: 12388
	private MountData mCurMountData;

	// Token: 0x04003065 RID: 12389
	public UIGrid ColorGride;

	// Token: 0x04003066 RID: 12390
	public List<ColorItemLogic> ColorIconList;

	// Token: 0x04003067 RID: 12391
	private List<color> curColorlist = new List<color>();

	// Token: 0x04003068 RID: 12392
	private string curColorSelectId = string.Empty;

	// Token: 0x04003069 RID: 12393
	private mount curMountInfo;

	// Token: 0x0400306A RID: 12394
	private ColorData curColorData;

	// Token: 0x0400306B RID: 12395
	public UILabel SteerAngleLabel;

	// Token: 0x0400306C RID: 12396
	public UISlider SteerAngleSlider;

	// Token: 0x0400306D RID: 12397
	public UILabel MaxSpeedLabel;

	// Token: 0x0400306E RID: 12398
	public UISlider MaxSpeedSlider;

	// Token: 0x0400306F RID: 12399
	public UILabel HpLabel;

	// Token: 0x04003070 RID: 12400
	public UISlider HpSlider;

	// Token: 0x04003071 RID: 12401
	public UILabel AccLabel;

	// Token: 0x04003072 RID: 12402
	public UISlider AccSlider;

	// Token: 0x04003073 RID: 12403
	private int MaxHPValue = 50000;

	// Token: 0x04003074 RID: 12404
	private int MaxAtkValue = 50000;

	// Token: 0x04003075 RID: 12405
	private int MaxSpeedValue = 6000;

	// Token: 0x04003076 RID: 12406
	private int MaxSteerValue = 30;

	// Token: 0x04003077 RID: 12407
	private int MaxAccValue = 3000;

	// Token: 0x04003078 RID: 12408
	public UILabel BuyPriceLabel;

	// Token: 0x04003079 RID: 12409
	public UILabel GetBtnLabel;

	// Token: 0x0400307A RID: 12410
	private Color ambientLight;
}
