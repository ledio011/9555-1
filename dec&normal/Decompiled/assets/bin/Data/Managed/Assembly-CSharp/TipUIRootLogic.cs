using System;

// Token: 0x020009CE RID: 2510
public class TipUIRootLogic : SingletonUnity<TipUIRootLogic>
{
	// Token: 0x0600476D RID: 18285 RVA: 0x0016C3D0 File Offset: 0x0016A5D0
	public void Reset(TIP_EVENT tipEvent)
	{
		this.mCurEvent = tipEvent;
		if (tipEvent == TIP_EVENT.TEAM)
		{
			this.TipLabel.text = tipEvent.ToString();
		}
	}

	// Token: 0x0600476E RID: 18286 RVA: 0x0016C40C File Offset: 0x0016A60C
	public void OnClickBtn()
	{
		TIP_EVENT tip_EVENT = this.mCurEvent;
		if (tip_EVENT == TIP_EVENT.TEAM)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.TeamRootNew, delegate(bool isSuccess, object param)
			{
				if (isSuccess)
				{
					SingletonUnity<TeamUIRootNewLogic>.Instance.Reset();
					SingletonUnity<TeamUIRootNewLogic>.Instance.OnClickApplyBtn();
				}
			}, null);
		}
	}

	// Token: 0x040034AB RID: 13483
	public UILabel TipLabel;

	// Token: 0x040034AC RID: 13484
	private TIP_EVENT mCurEvent;
}
