using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000967 RID: 2407
public class ColorItemLogic : MonoBehaviour
{
	// Token: 0x0600439B RID: 17307 RVA: 0x0014EBF0 File Offset: 0x0014CDF0
	public void Reset(color curcolor, string selectid, DelegateDefine.StringGameObjectDelegate func)
	{
		if (curcolor.state == 0L)
		{
			this.LockSp.enabled = true;
		}
		else
		{
			this.LockSp.enabled = false;
		}
		ColorData colorDataById = DataManager.GetColorDataById(curcolor.ID);
		this.ColorSp.color = colorDataById.CUIColor;
		this.mCurcolorData = curcolor;
		this.onClickColorIcon = func;
		if (selectid.Equals(curcolor.ID))
		{
			this.selectSp.enabled = true;
		}
		else
		{
			this.selectSp.enabled = false;
		}
	}

	// Token: 0x0600439C RID: 17308 RVA: 0x0014EC80 File Offset: 0x0014CE80
	public void refersh(string selectid)
	{
		if (selectid.Equals(this.mCurcolorData.ID))
		{
			this.selectSp.enabled = true;
		}
		else
		{
			this.selectSp.enabled = false;
		}
	}

	// Token: 0x0600439D RID: 17309 RVA: 0x0014ECC0 File Offset: 0x0014CEC0
	public void OnClickColorBtn()
	{
		if (this.onClickColorIcon != null)
		{
			this.onClickColorIcon(this.mCurcolorData.ID, base.gameObject);
		}
	}

	// Token: 0x0400303D RID: 12349
	public UISprite ColorSp;

	// Token: 0x0400303E RID: 12350
	public UISprite LockSp;

	// Token: 0x0400303F RID: 12351
	public UISprite selectSp;

	// Token: 0x04003040 RID: 12352
	public DelegateDefine.StringGameObjectDelegate onClickColorIcon;

	// Token: 0x04003041 RID: 12353
	private color mCurcolorData;
}
