using System;
using UnityEngine;

// Token: 0x020009F5 RID: 2549
public class ServerLineLogic : MonoBehaviour
{
	// Token: 0x06004900 RID: 18688 RVA: 0x00178604 File Offset: 0x00176804
	public void Reset(int index, DelegateDefine.OneIntParamDelegate linefun = null)
	{
		this.lineindex = index;
		this.LineLabel.text = string.Format("{0}-{1}", index * 10 + 1, (index + 1) * 10);
		this.ClickLine = linefun;
	}

	// Token: 0x06004901 RID: 18689 RVA: 0x00178640 File Offset: 0x00176840
	public void RefershSelect(int selectid)
	{
		if (selectid == this.lineindex)
		{
			this.btnSp.spriteName = "CZ_huaDongBG_1";
		}
		else
		{
			this.btnSp.spriteName = "CZ_huaDongBG";
		}
	}

	// Token: 0x06004902 RID: 18690 RVA: 0x00178674 File Offset: 0x00176874
	public void OnClickLineItem()
	{
		if (this.ClickLine != null)
		{
			this.ClickLine(this.lineindex);
		}
	}

	// Token: 0x04003623 RID: 13859
	public int lineindex;

	// Token: 0x04003624 RID: 13860
	public UISprite btnSp;

	// Token: 0x04003625 RID: 13861
	public UILabel LineLabel;

	// Token: 0x04003626 RID: 13862
	private DelegateDefine.OneIntParamDelegate ClickLine;
}
