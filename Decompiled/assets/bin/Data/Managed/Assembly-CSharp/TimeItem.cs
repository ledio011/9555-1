using System;
using UnityEngine;

// Token: 0x020009CD RID: 2509
public class TimeItem : MonoBehaviour
{
	// Token: 0x06004769 RID: 18281 RVA: 0x0016C2DC File Offset: 0x0016A4DC
	public void Reset(long curtime, int index, int select, DelegateDefine.OneIntParamDelegate onclickitem)
	{
		this.CurTime = curtime;
		this.CurIndex = index;
		this.OnClickItem = onclickitem;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		TimeSpan localShowTime = TimeTools.GetLocalShowTime(this.CurTime, playerCommonData.TimeOffset);
		this.TimeLabel.text = string.Format("{0:D2}:{1:D2}", localShowTime.Hours, localShowTime.Minutes);
		this.UpdateSelect(select);
	}

	// Token: 0x0600476A RID: 18282 RVA: 0x0016C350 File Offset: 0x0016A550
	public void OnClickBtn()
	{
		if (this.OnClickItem != null)
		{
			this.OnClickItem(this.CurIndex);
		}
	}

	// Token: 0x0600476B RID: 18283 RVA: 0x0016C370 File Offset: 0x0016A570
	public void UpdateSelect(int select)
	{
		if (this.CurIndex == select)
		{
			this.SelectSp.enabled = true;
			this.btnSp.spriteName = "CZ_huaDongBG_1";
		}
		else
		{
			this.SelectSp.enabled = false;
			this.btnSp.spriteName = "CZ_huaDongBG_2_XuanDing";
		}
	}

	// Token: 0x040034A5 RID: 13477
	public UISprite btnSp;

	// Token: 0x040034A6 RID: 13478
	public UILabel TimeLabel;

	// Token: 0x040034A7 RID: 13479
	public UISprite SelectSp;

	// Token: 0x040034A8 RID: 13480
	private long CurTime;

	// Token: 0x040034A9 RID: 13481
	private int CurIndex;

	// Token: 0x040034AA RID: 13482
	private DelegateDefine.OneIntParamDelegate OnClickItem;
}
