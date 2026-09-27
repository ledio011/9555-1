using System;
using UnityEngine;

// Token: 0x020009F4 RID: 2548
public class ServerAreaLogic : MonoBehaviour
{
	// Token: 0x060048FC RID: 18684 RVA: 0x00178564 File Offset: 0x00176764
	public void Reset(int id, DelegateDefine.OneIntParamDelegate clickfun = null)
	{
		this.AreaID = id;
		this.AreaNameLabel.text = StrDictionary.GetDictionaryString(GameDefine.SERVER_AREA_NAME[this.AreaID], new object[0]);
		this.ClickArea = clickfun;
	}

	// Token: 0x060048FD RID: 18685 RVA: 0x001785A8 File Offset: 0x001767A8
	public void RefershSelect(int selectid)
	{
		if (selectid == this.AreaID)
		{
			this.btnSp.spriteName = "CZ_huaDongBG_1";
		}
		else
		{
			this.btnSp.spriteName = "CZ_huaDongBG";
		}
	}

	// Token: 0x060048FE RID: 18686 RVA: 0x001785DC File Offset: 0x001767DC
	public void OnClickAreaItem()
	{
		if (this.ClickArea != null)
		{
			this.ClickArea(this.AreaID);
		}
	}

	// Token: 0x0400361F RID: 13855
	public int AreaID;

	// Token: 0x04003620 RID: 13856
	public UISprite btnSp;

	// Token: 0x04003621 RID: 13857
	public UILabel AreaNameLabel;

	// Token: 0x04003622 RID: 13858
	private DelegateDefine.OneIntParamDelegate ClickArea;
}
