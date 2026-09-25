using System;
using System.Collections.Generic;

// Token: 0x02000A3A RID: 2618
public class NumRootLogic : SingletonUnity<NumRootLogic>
{
	// Token: 0x06004C51 RID: 19537 RVA: 0x0019DD3C File Offset: 0x0019BF3C
	public void Reset(DelegateDefine.OneIntParamDelegate clicknumfun)
	{
		this.backFun = clicknumfun;
		for (int i = 0; i < this.numItems.Count; i++)
		{
			this.numItems[i].Reset(new DelegateDefine.OneIntParamDelegate(this.OnClickItem));
		}
	}

	// Token: 0x06004C52 RID: 19538 RVA: 0x0019DD8C File Offset: 0x0019BF8C
	public void OnClickItem(int num)
	{
		if (num < 10)
		{
			if (this.backFun != null)
			{
				this.backFun(num);
			}
		}
		else if (num == 99)
		{
			this.OnClickClose();
		}
	}

	// Token: 0x06004C53 RID: 19539 RVA: 0x0019DDCC File Offset: 0x0019BFCC
	public void OnClickClose()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NumRoot);
	}

	// Token: 0x04003A08 RID: 14856
	public UIGrid ItemGrid;

	// Token: 0x04003A09 RID: 14857
	public List<NumItemLogic> numItems;

	// Token: 0x04003A0A RID: 14858
	private int curNum;

	// Token: 0x04003A0B RID: 14859
	private DelegateDefine.OneIntParamDelegate backFun;
}
