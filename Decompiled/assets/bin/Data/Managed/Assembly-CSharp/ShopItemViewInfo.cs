using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A21 RID: 2593
public class ShopItemViewInfo : MonoBehaviour
{
	// Token: 0x06004AC6 RID: 19142 RVA: 0x0018AA38 File Offset: 0x00188C38
	public void Reset()
	{
		this.ambientLight = RenderSettings.ambientLight;
	}

	// Token: 0x06004AC7 RID: 19143 RVA: 0x0018AA48 File Offset: 0x00188C48
	public void UpdateSelectItem(shop_item curSelectItem)
	{
		if (curSelectItem == null)
		{
			return;
		}
		ItemData itemDataByID = DataManager.GetItemDataByID(curSelectItem.ItemID);
		if (itemDataByID.Type == GameDefine.ITEM_TYPE.FASHION_EQUIP || itemDataByID.CanShowModel)
		{
			this.ResetModelVisual(itemDataByID, false);
		}
		else if (itemDataByID.Type == GameDefine.ITEM_TYPE.EXCHANGE)
		{
			MountData mountDataById = DataManager.GetMountDataById(itemDataByID.Function.ToString());
			this.ResetCarModelVisual(mountDataById, DataManager.GetColorDataById(mountDataById.DefaultColorId));
		}
	}

	// Token: 0x06004AC8 RID: 19144 RVA: 0x0018AAC0 File Offset: 0x00188CC0
	private void ResetCarModelVisual(MountData mountData, ColorData datacolor)
	{
		this.ResetFakeCarObjRoot();
		SingletonUnity<FakeCarObjRootLogic>.Instance.InitFakeCarObj(mountData, datacolor);
	}

	// Token: 0x06004AC9 RID: 19145 RVA: 0x0018AAD4 File Offset: 0x00188CD4
	private void ResetModelVisual(ItemData curItemdata, bool isReset = false)
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
		if (!isReset)
		{
			EquipData equipDataById = DataManager.GetEquipDataById(curItemdata.ID);
			switch (curItemdata.SubType)
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

	// Token: 0x06004ACA RID: 19146 RVA: 0x0018AD2C File Offset: 0x00188F2C
	public bool CheckWeaponIsSame()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer fashionEquipPack = playerData.FashionEquipPack;
		ItemContainer equipPack = playerData.EquipPack;
		EquipData equipWeaponData = equipPack.GetEquipWeaponData(EQUIP_BACKPACK_TYPE.WEAPON);
		EquipData equipWeaponData2 = fashionEquipPack.GetEquipWeaponData(EQUIP_BACKPACK_TYPE.WEAPON);
		return equipWeaponData == null || equipWeaponData2 == null || equipWeaponData.WeaponType == equipWeaponData2.WeaponType;
	}

	// Token: 0x06004ACB RID: 19147 RVA: 0x0018AD84 File Offset: 0x00188F84
	public void SetCarLight()
	{
		float num = 0.78431374f;
		RenderSettings.ambientLight = new Color(num, num, num, 1f);
	}

	// Token: 0x06004ACC RID: 19148 RVA: 0x0018ADAC File Offset: 0x00188FAC
	public void ResetNormalLight()
	{
		RenderSettings.ambientLight = this.ambientLight;
	}

	// Token: 0x06004ACD RID: 19149 RVA: 0x0018ADBC File Offset: 0x00188FBC
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

	// Token: 0x06004ACE RID: 19150 RVA: 0x0018AE4C File Offset: 0x0018904C
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

	// Token: 0x06004ACF RID: 19151 RVA: 0x0018AEFC File Offset: 0x001890FC
	private void OnDragModelBtn(GameObject btn, Vector2 delta)
	{
		if (this.mCurFakeObj != null && this.mCurFakeObj.FakeObj != null)
		{
			this.mCurFakeObj.FakeObj.transform.localEulerAngles -= delta.x * Vector3.up;
		}
	}

	// Token: 0x06004AD0 RID: 19152 RVA: 0x0018AF5C File Offset: 0x0018915C
	public void OnDragCarModelPic(GameObject btn, Vector2 delta)
	{
		if (this.CarMeshRoot != null)
		{
			this.CarMeshRoot.transform.localEulerAngles -= new Vector3(0f, delta.x, 0f);
		}
	}

	// Token: 0x06004AD1 RID: 19153 RVA: 0x0018AFAC File Offset: 0x001891AC
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

	// Token: 0x06004AD2 RID: 19154 RVA: 0x0018AFD0 File Offset: 0x001891D0
	private void OnDisable()
	{
		this.UnLoadFakeObj();
	}

	// Token: 0x06004AD3 RID: 19155 RVA: 0x0018AFD8 File Offset: 0x001891D8
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

	// Token: 0x04003873 RID: 14451
	public UIEventListener RotateModelBtnListener;

	// Token: 0x04003874 RID: 14452
	private FakeObjLogic mCurFakeObj;

	// Token: 0x04003875 RID: 14453
	public UITexture ModelPic;

	// Token: 0x04003876 RID: 14454
	public UITexture CarModelPic;

	// Token: 0x04003877 RID: 14455
	private Transform CarMeshRoot;

	// Token: 0x04003878 RID: 14456
	private Color ambientLight;
}
