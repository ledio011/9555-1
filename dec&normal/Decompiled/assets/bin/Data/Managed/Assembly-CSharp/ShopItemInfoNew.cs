using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A1F RID: 2591
public class ShopItemInfoNew : MonoBehaviour
{
	// Token: 0x06004ABE RID: 19134 RVA: 0x00189EAC File Offset: 0x001880AC
	public void Reset()
	{
		NGUITools.SetActive(this.ModelViewObj.gameObject, false);
		NGUITools.SetActive(this.ItemViewObj.gameObject, false);
		NGUITools.SetActive(this.StatusBtnSp.gameObject, false);
		NGUITools.SetActive(this.ViewBtnSp.gameObject, false);
		this.ModelViewObj.Reset();
		this.IsShowAttInfo = true;
	}

	// Token: 0x06004ABF RID: 19135 RVA: 0x00189F10 File Offset: 0x00188110
	public void RefershInfo(shop_item shopitem)
	{
		this.curSelectItem = shopitem;
		ItemData itemDataByID = DataManager.GetItemDataByID(this.curSelectItem.ItemID);
		if (itemDataByID != null)
		{
			if (itemDataByID.CanShowModel || itemDataByID.Type == GameDefine.ITEM_TYPE.EXCHANGE)
			{
				NGUITools.SetActive(this.StatusBtnSp.gameObject, true);
				NGUITools.SetActive(this.ViewBtnSp.gameObject, true);
			}
			else
			{
				NGUITools.SetActive(this.StatusBtnSp.gameObject, false);
				NGUITools.SetActive(this.ViewBtnSp.gameObject, false);
				this.IsShowAttInfo = true;
				NGUITools.SetActive(this.ModelViewObj.gameObject, false);
			}
		}
		if (this.IsShowAttInfo)
		{
			if (!UnityVersionUtil.IsActive(this.ItemViewObj.gameObject))
			{
				NGUITools.SetActive(this.ItemViewObj.gameObject, true);
			}
			this.ItemViewObj.UpdateSelectItem(this.curSelectItem);
		}
		else
		{
			if (!UnityVersionUtil.IsActive(this.ModelViewObj.gameObject))
			{
				NGUITools.SetActive(this.ModelViewObj.gameObject, true);
			}
			this.ModelViewObj.UpdateSelectItem(this.curSelectItem);
		}
		this.SelectTable();
	}

	// Token: 0x06004AC0 RID: 19136 RVA: 0x0018A038 File Offset: 0x00188238
	public void OnClickStatusBtn()
	{
		if (this.IsShowAttInfo)
		{
			return;
		}
		this.IsShowAttInfo = true;
		NGUITools.SetActive(this.ModelViewObj.gameObject, false);
		NGUITools.SetActive(this.ItemViewObj.gameObject, true);
		this.ItemViewObj.UpdateSelectItem(this.curSelectItem);
		this.SelectTable();
	}

	// Token: 0x06004AC1 RID: 19137 RVA: 0x0018A094 File Offset: 0x00188294
	public void OnClickViewBtn()
	{
		if (!this.IsShowAttInfo)
		{
			return;
		}
		this.IsShowAttInfo = false;
		NGUITools.SetActive(this.ModelViewObj.gameObject, true);
		NGUITools.SetActive(this.ItemViewObj.gameObject, false);
		this.ModelViewObj.UpdateSelectItem(this.curSelectItem);
		this.SelectTable();
	}

	// Token: 0x06004AC2 RID: 19138 RVA: 0x0018A0F0 File Offset: 0x001882F0
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

	// Token: 0x0400385F RID: 14431
	public ShopItemSubInfo ItemViewObj;

	// Token: 0x04003860 RID: 14432
	public ShopItemViewInfo ModelViewObj;

	// Token: 0x04003861 RID: 14433
	public UISprite StatusBtnSp;

	// Token: 0x04003862 RID: 14434
	public UISprite ViewBtnSp;

	// Token: 0x04003863 RID: 14435
	private bool IsShowAttInfo;

	// Token: 0x04003864 RID: 14436
	private shop_item curSelectItem;
}
