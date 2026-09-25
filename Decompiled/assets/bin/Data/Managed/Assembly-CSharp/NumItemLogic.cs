using System;
using UnityEngine;

// Token: 0x02000A39 RID: 2617
public class NumItemLogic : MonoBehaviour
{
	// Token: 0x06004C4E RID: 19534 RVA: 0x0019DD08 File Offset: 0x0019BF08
	public void Reset(DelegateDefine.OneIntParamDelegate clickitem = null)
	{
		this.ClickFun = clickitem;
	}

	// Token: 0x06004C4F RID: 19535 RVA: 0x0019DD14 File Offset: 0x0019BF14
	public void OnClickItem()
	{
		if (this.ClickFun != null)
		{
			this.ClickFun(this.curnum);
		}
	}

	// Token: 0x04003A05 RID: 14853
	public UILabel infolabel;

	// Token: 0x04003A06 RID: 14854
	public int curnum;

	// Token: 0x04003A07 RID: 14855
	private DelegateDefine.OneIntParamDelegate ClickFun;
}
