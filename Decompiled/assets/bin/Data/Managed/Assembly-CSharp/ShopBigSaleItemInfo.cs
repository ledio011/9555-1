using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A1C RID: 2588
public class ShopBigSaleItemInfo : MonoBehaviour
{
	// Token: 0x06004A95 RID: 19093 RVA: 0x001883F4 File Offset: 0x001865F4
	public void Reset()
	{
		NGUITools.SetActive(this.ModelViewObj.gameObject, false);
		NGUITools.SetActive(this.ItemViewObj.gameObject, false);
		NGUITools.SetActive(this.StatusBtnSp.gameObject, false);
		NGUITools.SetActive(this.ViewBtnSp.gameObject, false);
		this.IsShowAttInfo = true;
		this.ambientLight = RenderSettings.ambientLight;
	}

	// Token: 0x06004A96 RID: 19094 RVA: 0x00188458 File Offset: 0x00186658
	public void RefershInfo(special_big_pack iteminfo)
	{
		if (iteminfo == null)
		{
			return;
		}
		this.curSelectItem = iteminfo;
		this.curData = DataManager.GetBigPackageDataById(this.curSelectItem.ID);
		this.UpdateItemList();
		if (this.curData.showmodeltype == 0)
		{
			NGUITools.SetActive(this.StatusBtnSp.gameObject, false);
			NGUITools.SetActive(this.ViewBtnSp.gameObject, false);
			this.IsShowAttInfo = true;
			NGUITools.SetActive(this.ModelViewObj.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(this.StatusBtnSp.gameObject, true);
			NGUITools.SetActive(this.ViewBtnSp.gameObject, true);
		}
		if (this.IsShowAttInfo)
		{
			if (!UnityVersionUtil.IsActive(this.ItemViewObj.gameObject))
			{
				NGUITools.SetActive(this.ItemViewObj.gameObject, true);
			}
			this.UpdateItemInfo();
		}
		else
		{
			if (!UnityVersionUtil.IsActive(this.ModelViewObj.gameObject))
			{
				NGUITools.SetActive(this.ModelViewObj.gameObject, true);
			}
			this.UpdateViewInfo();
		}
		this.SelectTable();
	}

	// Token: 0x06004A97 RID: 19095 RVA: 0x00188570 File Offset: 0x00186770
	public void UpdateViewInfo()
	{
		if (this.curData.showmodeltype == 1)
		{
			this.ResetModelVisual(this.ItemList);
		}
		else if (this.curData.showmodeltype == 2)
		{
			for (int i = 0; i < this.ItemList.Count; i++)
			{
				if (this.ItemList[i].ItemData.Type == GameDefine.ITEM_TYPE.EXCHANGE)
				{
					MountData mountDataById = DataManager.GetMountDataById(this.ItemList[i].ItemData.Function.ToString());
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
	}

	// Token: 0x06004A98 RID: 19096 RVA: 0x00188640 File Offset: 0x00186840
	private void ResetCarModelVisual(MountData mountData, ColorData datacolor)
	{
		this.ResetFakeCarObjRoot();
		SingletonUnity<FakeCarObjRootLogic>.Instance.InitFakeCarObj(mountData, datacolor);
	}

	// Token: 0x06004A99 RID: 19097 RVA: 0x00188654 File Offset: 0x00186854
	private void ResetModelVisual(List<GameItem> mItemList)
	{
		this.ResetFakeObjRoot();
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer fashionEquipPack = playerData.FashionEquipPack;
		ItemContainer equipPack = playerData.EquipPack;
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		if (playerData.IsShowFashion)
		{
			text = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.HEAD, playerData.Profession, true);
			if (string.IsNullOrEmpty(text))
			{
				text = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.HEAD, playerData.Profession, false);
			}
			if (this.CheckWeaponIsSame())
			{
				text2 = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, playerData.Profession, true);
				if (string.IsNullOrEmpty(text2))
				{
					text2 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, playerData.Profession, false);
				}
			}
			else
			{
				text2 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, playerData.Profession, false);
			}
			text3 = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.BODY, playerData.Profession, true);
			if (string.IsNullOrEmpty(text3))
			{
				text3 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.BODY, playerData.Profession, false);
			}
			text4 = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.LEG, playerData.Profession, true);
			if (string.IsNullOrEmpty(text4))
			{
				text4 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.LEG, playerData.Profession, false);
			}
		}
		else
		{
			text = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.HEAD, playerData.Profession, false);
			text2 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, playerData.Profession, false);
			text3 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.BODY, playerData.Profession, false);
			text4 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.LEG, playerData.Profession, false);
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
					text2 = equipDataById.ModelId;
					break;
				case 1:
					text = equipDataById.ModelId;
					break;
				case 2:
					text3 = equipDataById.ModelId;
					break;
				case 3:
					text4 = equipDataById.ModelId;
					break;
				}
			}
		}
		if (this.mCurFakeObj == null || this.mCurFakeObj.FakeObj == null)
		{
			this.mCurFakeObj = new FakeObjLogic();
			this.mCurFakeObj.InitFakeObject(text2, text, text3, text4, playerData.Profession, SingletonUnity<FakeObjRootLogic>.Instance.MeshRoot, null, "FakeObj");
		}
		else
		{
			this.mCurFakeObj.CheckFakeObject(text2, text, text3, text4, null);
			this.mCurFakeObj.PlayAnim("idle", playerData.CharacterModelData.ModelFirstType);
		}
	}

	// Token: 0x06004A9A RID: 19098 RVA: 0x001888F0 File Offset: 0x00186AF0
	public bool CheckWeaponIsSame()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer fashionEquipPack = playerData.FashionEquipPack;
		ItemContainer equipPack = playerData.EquipPack;
		EquipData equipWeaponData = equipPack.GetEquipWeaponData(EQUIP_BACKPACK_TYPE.WEAPON);
		EquipData equipWeaponData2 = fashionEquipPack.GetEquipWeaponData(EQUIP_BACKPACK_TYPE.WEAPON);
		return equipWeaponData == null || equipWeaponData2 == null || equipWeaponData.WeaponType == equipWeaponData2.WeaponType;
	}

	// Token: 0x06004A9B RID: 19099 RVA: 0x00188948 File Offset: 0x00186B48
	public void SetCarLight()
	{
		float num = 0.78431374f;
		RenderSettings.ambientLight = new Color(num, num, num, 1f);
	}

	// Token: 0x06004A9C RID: 19100 RVA: 0x00188970 File Offset: 0x00186B70
	public void ResetNormalLight()
	{
		RenderSettings.ambientLight = this.ambientLight;
	}

	// Token: 0x06004A9D RID: 19101 RVA: 0x00188980 File Offset: 0x00186B80
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
		SingletonUnity<FakeObjRootLogic>.Instance.SetPicValue(0.8f);
		SingletonUnity<FakeObjRootLogic>.Instance.EnableFakeObjRoot();
		this.ModelPic.mainTexture = instance.ModelPic;
		this.ModelPic.enabled = true;
		this.CarModelPic.enabled = false;
	}

	// Token: 0x06004A9E RID: 19102 RVA: 0x00188A10 File Offset: 0x00186C10
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

	// Token: 0x06004A9F RID: 19103 RVA: 0x00188AC0 File Offset: 0x00186CC0
	private void OnDragModelBtn(GameObject btn, Vector2 delta)
	{
		if (this.mCurFakeObj != null && this.mCurFakeObj.FakeObj != null)
		{
			this.mCurFakeObj.FakeObj.transform.localEulerAngles -= delta.x * Vector3.up;
		}
	}

	// Token: 0x06004AA0 RID: 19104 RVA: 0x00188B20 File Offset: 0x00186D20
	public void OnDragCarModelPic(GameObject btn, Vector2 delta)
	{
		if (this.CarMeshRoot != null)
		{
			this.CarMeshRoot.transform.localEulerAngles -= new Vector3(0f, delta.x, 0f);
		}
	}

	// Token: 0x06004AA1 RID: 19105 RVA: 0x00188B70 File Offset: 0x00186D70
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

	// Token: 0x06004AA2 RID: 19106 RVA: 0x00188B94 File Offset: 0x00186D94
	private void OnDisable()
	{
		this.UnLoadFakeObj();
	}

	// Token: 0x06004AA3 RID: 19107 RVA: 0x00188B9C File Offset: 0x00186D9C
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

	// Token: 0x06004AA4 RID: 19108 RVA: 0x00188BFC File Offset: 0x00186DFC
	public void UpdateItemList()
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
	}

	// Token: 0x06004AA5 RID: 19109 RVA: 0x00188FDC File Offset: 0x001871DC
	public void UpdateItemInfo()
	{
		this.NameLabel.text = StrDictionary.GetDictionaryString(this.curData.Name, new object[0]);
		this.NameLabel.color = GameDefine.GetColorByQuality(3);
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
		this.parentGrid.Reposition();
	}

	// Token: 0x06004AA6 RID: 19110 RVA: 0x001891A4 File Offset: 0x001873A4
	public void OnClickStatusBtn()
	{
		if (this.IsShowAttInfo)
		{
			return;
		}
		this.IsShowAttInfo = true;
		NGUITools.SetActive(this.ModelViewObj.gameObject, false);
		NGUITools.SetActive(this.ItemViewObj.gameObject, true);
		this.UpdateItemInfo();
		this.SelectTable();
	}

	// Token: 0x06004AA7 RID: 19111 RVA: 0x001891F4 File Offset: 0x001873F4
	public void OnClickViewBtn()
	{
		if (!this.IsShowAttInfo)
		{
			return;
		}
		this.IsShowAttInfo = false;
		NGUITools.SetActive(this.ModelViewObj.gameObject, true);
		NGUITools.SetActive(this.ItemViewObj.gameObject, false);
		this.UpdateViewInfo();
		this.SelectTable();
	}

	// Token: 0x06004AA8 RID: 19112 RVA: 0x00189244 File Offset: 0x00187444
	private void SelectTable()
	{
		if (this.IsShowAttInfo)
		{
			this.StatusBtnSp.spriteName = GameDefine.BtnIcon[0];
			this.ViewBtnSp.spriteName = GameDefine.BtnIcon[1];
		}
		else
		{
			this.StatusBtnSp.spriteName = GameDefine.BtnIcon[1];
			this.ViewBtnSp.spriteName = GameDefine.BtnIcon[0];
		}
	}

	// Token: 0x04003830 RID: 14384
	public GameObject ItemViewObj;

	// Token: 0x04003831 RID: 14385
	public GameObject ModelViewObj;

	// Token: 0x04003832 RID: 14386
	public UISprite StatusBtnSp;

	// Token: 0x04003833 RID: 14387
	public UISprite ViewBtnSp;

	// Token: 0x04003834 RID: 14388
	private bool IsShowAttInfo;

	// Token: 0x04003835 RID: 14389
	private special_big_pack curSelectItem;

	// Token: 0x04003836 RID: 14390
	private BigPackageData curData;

	// Token: 0x04003837 RID: 14391
	public List<RewardItem> rewardItems;

	// Token: 0x04003838 RID: 14392
	private List<GameItem> ItemList = new List<GameItem>();

	// Token: 0x04003839 RID: 14393
	public UIGrid parentGrid;

	// Token: 0x0400383A RID: 14394
	public UIEventListener RotateModelBtnListener;

	// Token: 0x0400383B RID: 14395
	private FakeObjLogic mCurFakeObj;

	// Token: 0x0400383C RID: 14396
	public UITexture ModelPic;

	// Token: 0x0400383D RID: 14397
	public UITexture CarModelPic;

	// Token: 0x0400383E RID: 14398
	private Transform CarMeshRoot;

	// Token: 0x0400383F RID: 14399
	private Color ambientLight;

	// Token: 0x04003840 RID: 14400
	public UILabel NameLabel;
}
