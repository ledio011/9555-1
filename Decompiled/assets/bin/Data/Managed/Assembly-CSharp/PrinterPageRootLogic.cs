using System;

// Token: 0x02000975 RID: 2421
public class PrinterPageRootLogic : SingletonUnity<PrinterPageRootLogic>
{
	// Token: 0x0600445E RID: 17502 RVA: 0x00154750 File Offset: 0x00152950
	public void Reset(string[] pages, DelegateDefine.NoParamDelegate onFinished)
	{
		this.mPageStr = pages;
		this.onPrintFinish = onFinished;
		this.TypeWriter.Reset(this.mPageStr[this.mCurPage], new DelegateDefine.NoParamDelegate(this.OnPageShowFinished));
		this.isFinishePage = false;
	}

	// Token: 0x0600445F RID: 17503 RVA: 0x0015478C File Offset: 0x0015298C
	private void OnPageShowFinished()
	{
		this.isFinishePage = true;
	}

	// Token: 0x06004460 RID: 17504 RVA: 0x00154798 File Offset: 0x00152998
	public void OnClickShowAllBtn()
	{
		if (!this.isFinishePage)
		{
			this.TypeWriter.ShowAll();
			this.isFinishePage = true;
		}
		else
		{
			this.mCurPage++;
			if (this.mCurPage < this.mPageStr.Length)
			{
				this.TypeWriter.Reset(this.mPageStr[this.mCurPage], new DelegateDefine.NoParamDelegate(this.OnPageShowFinished));
				this.isFinishePage = false;
			}
			else
			{
				this.OnClickSkipBtn();
			}
		}
	}

	// Token: 0x06004461 RID: 17505 RVA: 0x00154820 File Offset: 0x00152A20
	public void OnClickSkipBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PrinterPageRoot);
		if (this.onPrintFinish != null)
		{
			this.onPrintFinish();
		}
	}

	// Token: 0x040030F0 RID: 12528
	public MyTypewriterEffect TypeWriter;

	// Token: 0x040030F1 RID: 12529
	private string[] mPageStr;

	// Token: 0x040030F2 RID: 12530
	private DelegateDefine.NoParamDelegate onPrintFinish;

	// Token: 0x040030F3 RID: 12531
	private int mCurPage;

	// Token: 0x040030F4 RID: 12532
	private bool isFinishePage;
}
