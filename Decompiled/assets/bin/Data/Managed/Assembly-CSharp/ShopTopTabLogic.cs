using System;
using UnityEngine;

// Token: 0x02000A23 RID: 2595
public class ShopTopTabLogic : MonoBehaviour
{
	// Token: 0x06004AFB RID: 19195 RVA: 0x0018C530 File Offset: 0x0018A730
	public void Reset(int tabnum, DelegateDefine.OneIntParamDelegate clickfun = null)
	{
		this.curTab = tabnum;
		this.NameLabel.text = StrDictionary.GetDictionaryString(GameDefine.SHOP_TAB_NAME[this.curTab], new object[0]);
		this.ClickTab = clickfun;
	}

	// Token: 0x06004AFC RID: 19196 RVA: 0x0018C574 File Offset: 0x0018A774
	public void RefershSelect(int selectid)
	{
		if (selectid == this.curTab)
		{
			this.BGSp.spriteName = "CZ_huaDongBG_1";
		}
		else
		{
			this.BGSp.spriteName = "CZ_huaDongBG";
		}
	}

	// Token: 0x06004AFD RID: 19197 RVA: 0x0018C5A8 File Offset: 0x0018A7A8
	public void OnClickTabBtn()
	{
		if (this.ClickTab != null)
		{
			this.ClickTab(this.curTab);
		}
	}

	// Token: 0x04003898 RID: 14488
	public UILabel NameLabel;

	// Token: 0x04003899 RID: 14489
	public UISprite BGSp;

	// Token: 0x0400389A RID: 14490
	private DelegateDefine.OneIntParamDelegate ClickTab;

	// Token: 0x0400389B RID: 14491
	private int curTab;
}
