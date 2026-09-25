using System;
using System.Text;
using UnityEngine;

// Token: 0x020000D7 RID: 215
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x17000148 RID: 328
	// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00030460 File Offset: 0x0002E660
	public bool isValid
	{
		get
		{
			return this.textLabel != null && this.textLabel.ambigiousFont != null;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00030488 File Offset: 0x0002E688
	// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00030490 File Offset: 0x0002E690
	public float scrollValue
	{
		get
		{
			return this.mScroll;
		}
		set
		{
			value = Mathf.Clamp01(value);
			if (this.isValid && this.mScroll != value)
			{
				if (this.scrollBar != null)
				{
					this.scrollBar.value = value;
				}
				else
				{
					this.mScroll = value;
					this.UpdateVisibleText();
				}
			}
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000304EC File Offset: 0x0002E6EC
	protected float lineHeight
	{
		get
		{
			return (!(this.textLabel != null)) ? 20f : ((float)(this.textLabel.fontSize + this.textLabel.spacingY));
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00030524 File Offset: 0x0002E724
	protected int scrollHeight
	{
		get
		{
			if (!this.isValid)
			{
				return 0;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			return Mathf.Max(0, this.mTotalLines - num);
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00030568 File Offset: 0x0002E768
	public void Clear()
	{
		this.mParagraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0003057C File Offset: 0x0002E77C
	private void Start()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<UILabel>();
		}
		if (this.scrollBar != null)
		{
			EventDelegate.Add(this.scrollBar.onChange, new EventDelegate.Callback(this.OnScrollBar));
		}
		this.textLabel.overflowMethod = UILabel.Overflow.ClampContent;
		if (this.style == UITextList.Style.Chat)
		{
			this.textLabel.pivot = UIWidget.Pivot.BottomLeft;
			this.scrollValue = 1f;
		}
		else
		{
			this.textLabel.pivot = UIWidget.Pivot.TopLeft;
			this.scrollValue = 0f;
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00030620 File Offset: 0x0002E820
	private void Update()
	{
		if (this.isValid && (this.textLabel.width != this.mLastWidth || this.textLabel.height != this.mLastHeight))
		{
			this.mLastWidth = this.textLabel.width;
			this.mLastHeight = this.textLabel.height;
			this.Rebuild();
		}
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0003068C File Offset: 0x0002E88C
	public void OnScroll(float val)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			val *= this.lineHeight;
			this.scrollValue = this.mScroll - val / (float)scrollHeight;
		}
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x000306C4 File Offset: 0x0002E8C4
	public void OnDrag(Vector2 delta)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			float num = delta.y / this.lineHeight;
			this.scrollValue = this.mScroll + num / (float)scrollHeight;
		}
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00030700 File Offset: 0x0002E900
	private void OnScrollBar()
	{
		this.mScroll = UIProgressBar.current.value;
		this.UpdateVisibleText();
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00030718 File Offset: 0x0002E918
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00030724 File Offset: 0x0002E924
	protected void Add(string text, bool updateVisible)
	{
		UITextList.Paragraph paragraph;
		if (this.mParagraphs.size < this.paragraphHistory)
		{
			paragraph = new UITextList.Paragraph();
		}
		else
		{
			paragraph = this.mParagraphs[0];
			this.mParagraphs.RemoveAt(0);
		}
		paragraph.text = text;
		this.mParagraphs.Add(paragraph);
		this.Rebuild();
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x00030788 File Offset: 0x0002E988
	protected void Rebuild()
	{
		if (this.isValid)
		{
			this.textLabel.UpdateNGUIText();
			NGUIText.rectHeight = 1000000;
			this.mTotalLines = 0;
			for (int i = 0; i < this.mParagraphs.size; i++)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[i];
				string text;
				NGUIText.WrapText(paragraph.text, out text);
				paragraph.lines = text.Split(new char[]
				{
					'\n'
				});
				this.mTotalLines += paragraph.lines.Length;
			}
			this.mTotalLines = 0;
			int j = 0;
			int size = this.mParagraphs.size;
			while (j < size)
			{
				this.mTotalLines += this.mParagraphs.buffer[j].lines.Length;
				j++;
			}
			if (this.scrollBar != null)
			{
				UIScrollBar uiscrollBar = this.scrollBar as UIScrollBar;
				if (uiscrollBar != null)
				{
					uiscrollBar.barSize = ((this.mTotalLines != 0) ? (1f - (float)this.scrollHeight / (float)this.mTotalLines) : 1f);
				}
			}
			this.UpdateVisibleText();
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x000308CC File Offset: 0x0002EACC
	protected void UpdateVisibleText()
	{
		if (this.isValid)
		{
			if (this.mTotalLines == 0)
			{
				this.textLabel.text = string.Empty;
				return;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			int num2 = Mathf.Max(0, this.mTotalLines - num);
			int num3 = Mathf.RoundToInt(this.mScroll * (float)num2);
			if (num3 < 0)
			{
				num3 = 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num4 = 0;
			int size = this.mParagraphs.size;
			while (num > 0 && num4 < size)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[num4];
				int num5 = 0;
				int num6 = paragraph.lines.Length;
				while (num > 0 && num5 < num6)
				{
					string text = paragraph.lines[num5];
					if (num3 > 0)
					{
						num3--;
					}
					else
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append("\n");
						}
						stringBuilder.Append(text);
						num--;
					}
					num5++;
				}
				num4++;
			}
			this.textLabel.text = stringBuilder.ToString();
		}
	}

	// Token: 0x040005E4 RID: 1508
	public UILabel textLabel;

	// Token: 0x040005E5 RID: 1509
	public UIProgressBar scrollBar;

	// Token: 0x040005E6 RID: 1510
	public UITextList.Style style;

	// Token: 0x040005E7 RID: 1511
	public int paragraphHistory = 50;

	// Token: 0x040005E8 RID: 1512
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x040005E9 RID: 1513
	protected BetterList<UITextList.Paragraph> mParagraphs = new BetterList<UITextList.Paragraph>();

	// Token: 0x040005EA RID: 1514
	protected float mScroll;

	// Token: 0x040005EB RID: 1515
	protected int mTotalLines;

	// Token: 0x040005EC RID: 1516
	protected int mLastWidth;

	// Token: 0x040005ED RID: 1517
	protected int mLastHeight;

	// Token: 0x020000D8 RID: 216
	public enum Style
	{
		// Token: 0x040005EF RID: 1519
		Text,
		// Token: 0x040005F0 RID: 1520
		Chat
	}

	// Token: 0x020000D9 RID: 217
	protected class Paragraph
	{
		// Token: 0x040005F1 RID: 1521
		public string text;

		// Token: 0x040005F2 RID: 1522
		public string[] lines;
	}
}
