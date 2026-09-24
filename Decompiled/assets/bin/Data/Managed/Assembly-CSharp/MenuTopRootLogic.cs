using System;
using UnityEngine;

// Token: 0x02000958 RID: 2392
public class MenuTopRootLogic : MonoBehaviour
{
	// Token: 0x060042EF RID: 17135 RVA: 0x00148B40 File Offset: 0x00146D40
	private void OnEnable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
		this.UpdateMoney();
	}

	// Token: 0x060042F0 RID: 17136 RVA: 0x00148B74 File Offset: 0x00146D74
	private void OnDisable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
	}

	// Token: 0x060042F1 RID: 17137 RVA: 0x00148BA4 File Offset: 0x00146DA4
	public void ResetBackBtn(DelegateDefine.NoParamDelegate func)
	{
		this.onClickBackBtn = func;
	}

	// Token: 0x060042F2 RID: 17138 RVA: 0x00148BB0 File Offset: 0x00146DB0
	public void UpdateMoney()
	{
		this.DiamondLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetDiamond());
		this.GoldLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetGold());
		this.CashLabel.text = string.Format("{0:N0}", GameMoneyHelper.GetCash());
	}

	// Token: 0x060042F3 RID: 17139 RVA: 0x00148C1C File Offset: 0x00146E1C
	public void OnClickBackBtn()
	{
		if (this.onClickBackBtn != null)
		{
			this.onClickBackBtn();
		}
	}

	// Token: 0x060042F4 RID: 17140 RVA: 0x00148C34 File Offset: 0x00146E34
	public void OnClickAddDiamondBtn()
	{
	}

	// Token: 0x060042F5 RID: 17141 RVA: 0x00148C38 File Offset: 0x00146E38
	public void OnClickGoldBtn()
	{
	}

	// Token: 0x060042F6 RID: 17142 RVA: 0x00148C3C File Offset: 0x00146E3C
	public void OnClickCashBtn()
	{
	}

	// Token: 0x04002F57 RID: 12119
	public UILabel DiamondLabel;

	// Token: 0x04002F58 RID: 12120
	public UILabel GoldLabel;

	// Token: 0x04002F59 RID: 12121
	public UILabel CashLabel;

	// Token: 0x04002F5A RID: 12122
	private DelegateDefine.NoParamDelegate onClickBackBtn;
}
