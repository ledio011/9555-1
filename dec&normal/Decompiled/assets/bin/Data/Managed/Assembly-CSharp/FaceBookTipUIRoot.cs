using System;

// Token: 0x02000911 RID: 2321
public class FaceBookTipUIRoot : SingletonUnity<FaceBookTipUIRoot>
{
	// Token: 0x06003FDB RID: 16347 RVA: 0x0012AE10 File Offset: 0x00129010
	public void Reset(DelegateDefine.NoParamDelegate yes = null, DelegateDefine.NoParamDelegate no = null)
	{
		this.onClickYes = yes;
		this.onClickNo = no;
	}

	// Token: 0x06003FDC RID: 16348 RVA: 0x0012AE20 File Offset: 0x00129020
	private void OnEnable()
	{
		this.onClickNo = null;
		this.onClickYes = null;
	}

	// Token: 0x06003FDD RID: 16349 RVA: 0x0012AE30 File Offset: 0x00129030
	public void OnClickYes()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FaceBookTipUIRoot);
		if (this.onClickYes != null)
		{
			this.onClickYes();
		}
	}

	// Token: 0x06003FDE RID: 16350 RVA: 0x0012AE58 File Offset: 0x00129058
	public void OnClickNo()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FaceBookTipUIRoot);
		if (this.onClickNo != null)
		{
			this.onClickNo();
		}
	}

	// Token: 0x04002BBC RID: 11196
	public UISprite CanCancelBtn;

	// Token: 0x04002BBD RID: 11197
	private DelegateDefine.NoParamDelegate onClickNo;

	// Token: 0x04002BBE RID: 11198
	private DelegateDefine.NoParamDelegate onClickYes;
}
