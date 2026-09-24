using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000080 RID: 128
	[ExecuteInEditMode]
	[Serializable]
	public sealed class GUISkin : ScriptableObject
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x0000FC34 File Offset: 0x0000DE34
		public GUISkin()
		{
			this.m_CustomStyles = new GUIStyle[1];
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000FC54 File Offset: 0x0000DE54
		internal void OnEnable()
		{
			this.Apply();
			foreach (GUIStyle guistyle in this.styles.Values)
			{
				guistyle.CreateObjectReferences();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
				if (GUISkin.current == this)
				{
					GUIStyle.SetDefaultFont(this.m_Font);
				}
				this.Apply();
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0000FCF4 File Offset: 0x0000DEF4
		public GUIStyle box
		{
			get
			{
				return this.m_box;
			}
			set
			{
				this.m_box = value;
				this.Apply();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000FD04 File Offset: 0x0000DF04
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		public GUIStyle label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.Apply();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000FD24 File Offset: 0x0000DF24
		public GUIStyle textField
		{
			get
			{
				return this.m_textField;
			}
			set
			{
				this.m_textField = value;
				this.Apply();
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000FD34 File Offset: 0x0000DF34
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0000FD3C File Offset: 0x0000DF3C
		public GUIStyle textArea
		{
			get
			{
				return this.m_textArea;
			}
			set
			{
				this.m_textArea = value;
				this.Apply();
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0000FD54 File Offset: 0x0000DF54
		public GUIStyle button
		{
			get
			{
				return this.m_button;
			}
			set
			{
				this.m_button = value;
				this.Apply();
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000FD64 File Offset: 0x0000DF64
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		public GUIStyle toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
				this.Apply();
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0000FD84 File Offset: 0x0000DF84
		public GUIStyle window
		{
			get
			{
				return this.m_window;
			}
			set
			{
				this.m_window = value;
				this.Apply();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0000FD94 File Offset: 0x0000DF94
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0000FD9C File Offset: 0x0000DF9C
		public GUIStyle horizontalSlider
		{
			get
			{
				return this.m_horizontalSlider;
			}
			set
			{
				this.m_horizontalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		public GUIStyle horizontalSliderThumb
		{
			get
			{
				return this.m_horizontalSliderThumb;
			}
			set
			{
				this.m_horizontalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		public GUIStyle verticalSlider
		{
			get
			{
				return this.m_verticalSlider;
			}
			set
			{
				this.m_verticalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public GUIStyle verticalSliderThumb
		{
			get
			{
				return this.m_verticalSliderThumb;
			}
			set
			{
				this.m_verticalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0000FDFC File Offset: 0x0000DFFC
		public GUIStyle horizontalScrollbar
		{
			get
			{
				return this.m_horizontalScrollbar;
			}
			set
			{
				this.m_horizontalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000FE0C File Offset: 0x0000E00C
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0000FE14 File Offset: 0x0000E014
		public GUIStyle horizontalScrollbarThumb
		{
			get
			{
				return this.m_horizontalScrollbarThumb;
			}
			set
			{
				this.m_horizontalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000FE24 File Offset: 0x0000E024
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0000FE2C File Offset: 0x0000E02C
		public GUIStyle horizontalScrollbarLeftButton
		{
			get
			{
				return this.m_horizontalScrollbarLeftButton;
			}
			set
			{
				this.m_horizontalScrollbarLeftButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000FE3C File Offset: 0x0000E03C
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000FE44 File Offset: 0x0000E044
		public GUIStyle horizontalScrollbarRightButton
		{
			get
			{
				return this.m_horizontalScrollbarRightButton;
			}
			set
			{
				this.m_horizontalScrollbarRightButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000FE54 File Offset: 0x0000E054
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0000FE5C File Offset: 0x0000E05C
		public GUIStyle verticalScrollbar
		{
			get
			{
				return this.m_verticalScrollbar;
			}
			set
			{
				this.m_verticalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0000FE6C File Offset: 0x0000E06C
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0000FE74 File Offset: 0x0000E074
		public GUIStyle verticalScrollbarThumb
		{
			get
			{
				return this.m_verticalScrollbarThumb;
			}
			set
			{
				this.m_verticalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000FE84 File Offset: 0x0000E084
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0000FE8C File Offset: 0x0000E08C
		public GUIStyle verticalScrollbarUpButton
		{
			get
			{
				return this.m_verticalScrollbarUpButton;
			}
			set
			{
				this.m_verticalScrollbarUpButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000FE9C File Offset: 0x0000E09C
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		public GUIStyle verticalScrollbarDownButton
		{
			get
			{
				return this.m_verticalScrollbarDownButton;
			}
			set
			{
				this.m_verticalScrollbarDownButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0000FEBC File Offset: 0x0000E0BC
		public GUIStyle scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
			set
			{
				this.m_ScrollView = value;
				this.Apply();
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0000FECC File Offset: 0x0000E0CC
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public GUIStyle[] customStyles
		{
			get
			{
				return this.m_CustomStyles;
			}
			set
			{
				this.m_CustomStyles = value;
				this.Apply();
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
		public GUISettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		internal static GUIStyle error
		{
			get
			{
				if (GUISkin.ms_Error == null)
				{
					GUISkin.ms_Error = new GUIStyle();
				}
				return GUISkin.ms_Error;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000FF08 File Offset: 0x0000E108
		internal void Apply()
		{
			if (this.m_CustomStyles == null)
			{
				Debug.Log("custom styles is null");
			}
			this.BuildStyleCache();
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000FF28 File Offset: 0x0000E128
		private void BuildStyleCache()
		{
			if (this.m_box == null)
			{
				this.m_box = new GUIStyle();
			}
			if (this.m_button == null)
			{
				this.m_button = new GUIStyle();
			}
			if (this.m_toggle == null)
			{
				this.m_toggle = new GUIStyle();
			}
			if (this.m_label == null)
			{
				this.m_label = new GUIStyle();
			}
			if (this.m_window == null)
			{
				this.m_window = new GUIStyle();
			}
			if (this.m_textField == null)
			{
				this.m_textField = new GUIStyle();
			}
			if (this.m_textArea == null)
			{
				this.m_textArea = new GUIStyle();
			}
			if (this.m_horizontalSlider == null)
			{
				this.m_horizontalSlider = new GUIStyle();
			}
			if (this.m_horizontalSliderThumb == null)
			{
				this.m_horizontalSliderThumb = new GUIStyle();
			}
			if (this.m_verticalSlider == null)
			{
				this.m_verticalSlider = new GUIStyle();
			}
			if (this.m_verticalSliderThumb == null)
			{
				this.m_verticalSliderThumb = new GUIStyle();
			}
			if (this.m_horizontalScrollbar == null)
			{
				this.m_horizontalScrollbar = new GUIStyle();
			}
			if (this.m_horizontalScrollbarThumb == null)
			{
				this.m_horizontalScrollbarThumb = new GUIStyle();
			}
			if (this.m_horizontalScrollbarLeftButton == null)
			{
				this.m_horizontalScrollbarLeftButton = new GUIStyle();
			}
			if (this.m_horizontalScrollbarRightButton == null)
			{
				this.m_horizontalScrollbarRightButton = new GUIStyle();
			}
			if (this.m_verticalScrollbar == null)
			{
				this.m_verticalScrollbar = new GUIStyle();
			}
			if (this.m_verticalScrollbarThumb == null)
			{
				this.m_verticalScrollbarThumb = new GUIStyle();
			}
			if (this.m_verticalScrollbarUpButton == null)
			{
				this.m_verticalScrollbarUpButton = new GUIStyle();
			}
			if (this.m_verticalScrollbarDownButton == null)
			{
				this.m_verticalScrollbarDownButton = new GUIStyle();
			}
			if (this.m_ScrollView == null)
			{
				this.m_ScrollView = new GUIStyle();
			}
			this.styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
			this.styles["box"] = this.m_box;
			this.m_box.name = "box";
			this.styles["button"] = this.m_button;
			this.m_button.name = "button";
			this.styles["toggle"] = this.m_toggle;
			this.m_toggle.name = "toggle";
			this.styles["label"] = this.m_label;
			this.m_label.name = "label";
			this.styles["window"] = this.m_window;
			this.m_window.name = "window";
			this.styles["textfield"] = this.m_textField;
			this.m_textField.name = "textfield";
			this.styles["textarea"] = this.m_textArea;
			this.m_textArea.name = "textarea";
			this.styles["horizontalslider"] = this.m_horizontalSlider;
			this.m_horizontalSlider.name = "horizontalslider";
			this.styles["horizontalsliderthumb"] = this.m_horizontalSliderThumb;
			this.m_horizontalSliderThumb.name = "horizontalsliderthumb";
			this.styles["verticalslider"] = this.m_verticalSlider;
			this.m_verticalSlider.name = "verticalslider";
			this.styles["verticalsliderthumb"] = this.m_verticalSliderThumb;
			this.m_verticalSliderThumb.name = "verticalsliderthumb";
			this.styles["horizontalscrollbar"] = this.m_horizontalScrollbar;
			this.m_horizontalScrollbar.name = "horizontalscrollbar";
			this.styles["horizontalscrollbarthumb"] = this.m_horizontalScrollbarThumb;
			this.m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
			this.styles["horizontalscrollbarleftbutton"] = this.m_horizontalScrollbarLeftButton;
			this.m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
			this.styles["horizontalscrollbarrightbutton"] = this.m_horizontalScrollbarRightButton;
			this.m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
			this.styles["verticalscrollbar"] = this.m_verticalScrollbar;
			this.m_verticalScrollbar.name = "verticalscrollbar";
			this.styles["verticalscrollbarthumb"] = this.m_verticalScrollbarThumb;
			this.m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
			this.styles["verticalscrollbarupbutton"] = this.m_verticalScrollbarUpButton;
			this.m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
			this.styles["verticalscrollbardownbutton"] = this.m_verticalScrollbarDownButton;
			this.m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
			this.styles["scrollview"] = this.m_ScrollView;
			this.m_ScrollView.name = "scrollview";
			if (this.m_CustomStyles != null)
			{
				for (int i = 0; i < this.m_CustomStyles.Length; i++)
				{
					if (this.m_CustomStyles[i] != null)
					{
						this.styles[this.m_CustomStyles[i].name] = this.m_CustomStyles[i];
					}
				}
			}
			GUISkin.error.stretchHeight = true;
			GUISkin.error.normal.textColor = Color.red;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001046C File Offset: 0x0000E66C
		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle guistyle = this.FindStyle(styleName);
			if (guistyle != null)
			{
				return guistyle;
			}
			Debug.LogWarning(string.Concat(new object[]
			{
				"Unable to find style '",
				styleName,
				"' in skin '",
				base.name,
				"' ",
				Event.current.type
			}));
			return GUISkin.error;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000104D8 File Offset: 0x0000E6D8
		public GUIStyle FindStyle(string styleName)
		{
			if (this == null)
			{
				Debug.LogError("GUISkin is NULL");
				return null;
			}
			if (this.styles == null)
			{
				this.BuildStyleCache();
			}
			GUIStyle result;
			if (this.styles.TryGetValue(styleName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00010524 File Offset: 0x0000E724
		internal void MakeCurrent()
		{
			GUISkin.current = this;
			GUIStyle.SetDefaultFont(this.font);
			if (GUISkin.m_SkinChanged != null)
			{
				GUISkin.m_SkinChanged();
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001054C File Offset: 0x0000E74C
		public IEnumerator GetEnumerator()
		{
			if (this.styles == null)
			{
				this.BuildStyleCache();
			}
			return this.styles.Values.GetEnumerator();
		}

		// Token: 0x04000161 RID: 353
		[SerializeField]
		private Font m_Font;

		// Token: 0x04000162 RID: 354
		[SerializeField]
		private GUIStyle m_box;

		// Token: 0x04000163 RID: 355
		[SerializeField]
		private GUIStyle m_button;

		// Token: 0x04000164 RID: 356
		[SerializeField]
		private GUIStyle m_toggle;

		// Token: 0x04000165 RID: 357
		[SerializeField]
		private GUIStyle m_label;

		// Token: 0x04000166 RID: 358
		[SerializeField]
		private GUIStyle m_textField;

		// Token: 0x04000167 RID: 359
		[SerializeField]
		private GUIStyle m_textArea;

		// Token: 0x04000168 RID: 360
		[SerializeField]
		private GUIStyle m_window;

		// Token: 0x04000169 RID: 361
		[SerializeField]
		private GUIStyle m_horizontalSlider;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private GUIStyle m_horizontalSliderThumb;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private GUIStyle m_verticalSlider;

		// Token: 0x0400016C RID: 364
		[SerializeField]
		private GUIStyle m_verticalSliderThumb;

		// Token: 0x0400016D RID: 365
		[SerializeField]
		private GUIStyle m_horizontalScrollbar;

		// Token: 0x0400016E RID: 366
		[SerializeField]
		private GUIStyle m_horizontalScrollbarThumb;

		// Token: 0x0400016F RID: 367
		[SerializeField]
		private GUIStyle m_horizontalScrollbarLeftButton;

		// Token: 0x04000170 RID: 368
		[SerializeField]
		private GUIStyle m_horizontalScrollbarRightButton;

		// Token: 0x04000171 RID: 369
		[SerializeField]
		private GUIStyle m_verticalScrollbar;

		// Token: 0x04000172 RID: 370
		[SerializeField]
		private GUIStyle m_verticalScrollbarThumb;

		// Token: 0x04000173 RID: 371
		[SerializeField]
		private GUIStyle m_verticalScrollbarUpButton;

		// Token: 0x04000174 RID: 372
		[SerializeField]
		private GUIStyle m_verticalScrollbarDownButton;

		// Token: 0x04000175 RID: 373
		[SerializeField]
		private GUIStyle m_ScrollView;

		// Token: 0x04000176 RID: 374
		[SerializeField]
		internal GUIStyle[] m_CustomStyles;

		// Token: 0x04000177 RID: 375
		[SerializeField]
		private GUISettings m_Settings = new GUISettings();

		// Token: 0x04000178 RID: 376
		internal static GUIStyle ms_Error;

		// Token: 0x04000179 RID: 377
		private Dictionary<string, GUIStyle> styles;

		// Token: 0x0400017A RID: 378
		internal static GUISkin.SkinChangedDelegate m_SkinChanged;

		// Token: 0x0400017B RID: 379
		internal static GUISkin current;

		// Token: 0x02000081 RID: 129
		// (Invoke) Token: 0x06000645 RID: 1605
		internal delegate void SkinChangedDelegate();
	}
}
