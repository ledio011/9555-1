using System;
using UnityEngine;

// Token: 0x020009D1 RID: 2513
public class TopRightGoldLogic : MonoBehaviour
{
	// Token: 0x06004776 RID: 18294 RVA: 0x0016C590 File Offset: 0x0016A790
	private void OnEnable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
		this.UpdateMoney();
	}

	// Token: 0x06004777 RID: 18295 RVA: 0x0016C5C4 File Offset: 0x0016A7C4
	private void OnDisable()
	{
		UIUpdateEvent.UpdateMoneyEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateMoneyEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateMoney));
	}

	// Token: 0x06004778 RID: 18296 RVA: 0x0016C5F4 File Offset: 0x0016A7F4
	private void UpdateMoney()
	{
		this.GoldLable.text = GameMoneyHelper.GetGold().ToString();
		this.CashLabel.text = GameMoneyHelper.GetCash().ToString();
	}

	// Token: 0x040034B7 RID: 13495
	public UILabel GoldLable;

	// Token: 0x040034B8 RID: 13496
	public UILabel CashLabel;
}
