using System;
using UnityEngine;

// Token: 0x020008B6 RID: 2230
public class MyTypewriterEffect : MonoBehaviour
{
	// Token: 0x06003C25 RID: 15397 RVA: 0x001070D8 File Offset: 0x001052D8
	private void OnEnable()
	{
		this.mReset = true;
	}

	// Token: 0x06003C26 RID: 15398 RVA: 0x001070E4 File Offset: 0x001052E4
	private void Update()
	{
		if (this.mOffset < this.mText.Length && this.mNextChar <= RealTime.time)
		{
			this.charsPerSecond = Mathf.Max(1, this.charsPerSecond);
			float num = 1f / (float)this.charsPerSecond;
			char c = this.mText.get_Chars(this.mOffset);
			if (c == '.' || c == '\n' || c == '!' || c == '?')
			{
				num *= 4f;
			}
			NGUIText.ParseSymbol(this.mText, ref this.mOffset);
			this.mNextChar = RealTime.time + num;
			this.mLabel.text = this.mText.Substring(0, ++this.mOffset);
			if (this.mOffset >= this.mText.Length && this.onFinished != null)
			{
				this.onFinished();
			}
		}
	}

	// Token: 0x06003C27 RID: 15399 RVA: 0x001071E4 File Offset: 0x001053E4
	public void ShowAll()
	{
		this.mLabel.text = this.mText;
		this.mOffset = this.mText.Length;
		if (this.onFinished != null)
		{
			this.onFinished();
		}
	}

	// Token: 0x06003C28 RID: 15400 RVA: 0x0010722C File Offset: 0x0010542C
	public void Reset(string str, DelegateDefine.NoParamDelegate func)
	{
		if (this.mLabel == null)
		{
			this.mLabel = base.GetComponent<UILabel>();
		}
		this.mOffset = 0;
		this.mText = str;
		this.onFinished = func;
	}

	// Token: 0x04002754 RID: 10068
	public int charsPerSecond = 40;

	// Token: 0x04002755 RID: 10069
	private UILabel mLabel;

	// Token: 0x04002756 RID: 10070
	private string mText;

	// Token: 0x04002757 RID: 10071
	private int mOffset;

	// Token: 0x04002758 RID: 10072
	private float mNextChar;

	// Token: 0x04002759 RID: 10073
	private bool mReset = true;

	// Token: 0x0400275A RID: 10074
	private DelegateDefine.NoParamDelegate onFinished;
}
