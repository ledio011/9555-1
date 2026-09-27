using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
[AddComponentMenu("NGUI/Examples/Typewriter Effect")]
[RequireComponent(typeof(UILabel))]
public class TypewriterEffect : MonoBehaviour
{
	// Token: 0x060000DD RID: 221 RVA: 0x00006668 File Offset: 0x00004868
	private void OnEnable()
	{
		this.mReset = true;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00006674 File Offset: 0x00004874
	private void Update()
	{
		if (this.mReset)
		{
			this.mOffset = 0;
			this.mReset = false;
			this.mLabel = base.GetComponent<UILabel>();
			this.mText = this.mLabel.processedText;
		}
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
		}
	}

	// Token: 0x040000F5 RID: 245
	public int charsPerSecond = 40;

	// Token: 0x040000F6 RID: 246
	private UILabel mLabel;

	// Token: 0x040000F7 RID: 247
	private string mText;

	// Token: 0x040000F8 RID: 248
	private int mOffset;

	// Token: 0x040000F9 RID: 249
	private float mNextChar;

	// Token: 0x040000FA RID: 250
	private bool mReset = true;
}
