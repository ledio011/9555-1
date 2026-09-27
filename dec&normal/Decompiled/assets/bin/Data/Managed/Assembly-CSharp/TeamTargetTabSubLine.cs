using System;
using UnityEngine;

// Token: 0x020009C7 RID: 2503
public class TeamTargetTabSubLine : MonoBehaviour
{
	// Token: 0x17000FB5 RID: 4021
	// (get) Token: 0x0600473E RID: 18238 RVA: 0x0016B3D0 File Offset: 0x001695D0
	public string Key
	{
		get
		{
			return this.mKey;
		}
	}

	// Token: 0x0600473F RID: 18239 RVA: 0x0016B3D8 File Offset: 0x001695D8
	public void Reset(string title, string key, DelegateDefine.StringGameObjectDelegate clickFunc)
	{
		this.TitleLabel.text = title;
		this.mKey = key;
		this.onClickTab = clickFunc;
	}

	// Token: 0x06004740 RID: 18240 RVA: 0x0016B3F4 File Offset: 0x001695F4
	public void OnClickTab()
	{
		if (this.onClickTab != null)
		{
			this.onClickTab(this.mKey, base.gameObject);
		}
	}

	// Token: 0x0400346D RID: 13421
	public UILabel TitleLabel;

	// Token: 0x0400346E RID: 13422
	private string mKey;

	// Token: 0x0400346F RID: 13423
	private DelegateDefine.StringGameObjectDelegate onClickTab;
}
