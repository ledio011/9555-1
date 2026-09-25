using System;
using UnityEngine;

// Token: 0x02000909 RID: 2313
public class EquipItemLogic : MonoBehaviour
{
	// Token: 0x06003F75 RID: 16245 RVA: 0x00127AC0 File Offset: 0x00125CC0
	private void Awake()
	{
	}

	// Token: 0x06003F76 RID: 16246 RVA: 0x00127AC4 File Offset: 0x00125CC4
	public void InitEuipInfo(GameItem item, bool IsSelect)
	{
		this.mCurItem = item;
		EquipData equipDataById = DataManager.GetEquipDataById(this.mCurItem.ItemId);
		ItemData itemDataByID = DataManager.GetItemDataByID(this.mCurItem.ItemId);
		if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			UnityVersionUtil.SetActiveRecursive(this.QualityIcon.gameObject, true);
			this.QualityIcon.spriteName = this.mCurItem.GetItemQuality().ToString();
		}
		else if (itemDataByID.Quality != -1)
		{
			UnityVersionUtil.SetActiveRecursive(this.QualityIcon.gameObject, true);
			this.QualityIcon.spriteName = ((EQUIP_QUALITY)itemDataByID.Quality).ToString();
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.QualityIcon.gameObject, false);
		}
		this.NameLab.text = itemDataByID.MName;
		this.NameLab.color = GameDefine.GetColorByQuality(item.GetItemQuality());
		this.Icon.spriteName = itemDataByID.BackPackIcon;
		this.LvLab.text = string.Format("LV.{0}", this.mCurItem.ItemLevel);
		this.Tog.value = IsSelect;
	}

	// Token: 0x06003F77 RID: 16247 RVA: 0x00127BF4 File Offset: 0x00125DF4
	public void UpdateInfo(GameItem item)
	{
		this.mCurItem = item;
		EquipData equipDataById = DataManager.GetEquipDataById(this.mCurItem.ItemId);
		ItemData itemDataByID = DataManager.GetItemDataByID(this.mCurItem.ItemId);
		UnityVersionUtil.SetActiveRecursive(this.QualityIcon.gameObject, true);
		if (itemDataByID.Type == GameDefine.ITEM_TYPE.EQUIP)
		{
			this.QualityIcon.spriteName = this.mCurItem.GetItemQuality().ToString();
		}
		else
		{
			this.QualityIcon.spriteName = itemDataByID.QualityType.ToString();
		}
		this.NameLab.text = itemDataByID.MName;
		this.NameLab.color = GameDefine.GetColorByQuality(item.GetItemQuality());
		this.Icon.spriteName = itemDataByID.BackPackIcon;
		this.LvLab.text = string.Format("LV.{0}", this.mCurItem.ItemLevel);
		this.Tog.value = true;
	}

	// Token: 0x06003F78 RID: 16248 RVA: 0x00127CF0 File Offset: 0x00125EF0
	public void ResetInfo()
	{
		this.mCurItem = null;
		UnityVersionUtil.SetActiveRecursive(this.QualityIcon.gameObject, false);
		this.NameLab.text = string.Empty;
		this.Icon.spriteName = string.Empty;
		this.LvLab.text = string.Empty;
		this.Tog.value = false;
		this.Tog.enabled = false;
	}

	// Token: 0x06003F79 RID: 16249 RVA: 0x00127D60 File Offset: 0x00125F60
	public void OnClickEquipItem()
	{
		if (this.OnClick != null && this.mCurItem != null)
		{
			this.OnClick(this.mCurItem);
		}
	}

	// Token: 0x04002B4E RID: 11086
	public EquipItemLogic.OnClickItem OnClick;

	// Token: 0x04002B4F RID: 11087
	public GameItem mCurItem;

	// Token: 0x04002B50 RID: 11088
	public UILabel NameLab;

	// Token: 0x04002B51 RID: 11089
	public UILabel LvLab;

	// Token: 0x04002B52 RID: 11090
	public UISprite Icon;

	// Token: 0x04002B53 RID: 11091
	public UISprite QualityIcon;

	// Token: 0x04002B54 RID: 11092
	public UIToggle Tog;

	// Token: 0x02000AEC RID: 2796
	// (Invoke) Token: 0x06005039 RID: 20537
	public delegate void OnClickItem(GameItem date);
}
