using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public struct FontData : ISerializationCallbackReceiver
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00006770 File Offset: 0x00004970
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006774 File Offset: 0x00004974
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_FontSize = Mathf.Clamp(this.m_FontSize, 0, 300);
			this.m_MinSize = Mathf.Clamp(this.m_MinSize, 0, 300);
			this.m_MaxSize = Mathf.Clamp(this.m_MaxSize, 0, 300);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000067C8 File Offset: 0x000049C8
		public static FontData defaultFontData
		{
			get
			{
				return new FontData
				{
					m_FontSize = 14,
					m_LineSpacing = 1f,
					m_MinSize = 10,
					m_MaxSize = 40,
					m_RichText = true
				};
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006814 File Offset: 0x00004A14
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000681C File Offset: 0x00004A1C
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006828 File Offset: 0x00004A28
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00006830 File Offset: 0x00004A30
		public int fontSize
		{
			get
			{
				return this.m_FontSize;
			}
			set
			{
				this.m_FontSize = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000683C File Offset: 0x00004A3C
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00006844 File Offset: 0x00004A44
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontStyle;
			}
			set
			{
				this.m_FontStyle = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006850 File Offset: 0x00004A50
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00006858 File Offset: 0x00004A58
		public bool bestFit
		{
			get
			{
				return this.m_BestFit;
			}
			set
			{
				this.m_BestFit = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006864 File Offset: 0x00004A64
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000686C File Offset: 0x00004A6C
		public int minSize
		{
			get
			{
				return this.m_MinSize;
			}
			set
			{
				this.m_MinSize = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00006878 File Offset: 0x00004A78
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00006880 File Offset: 0x00004A80
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000688C File Offset: 0x00004A8C
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00006894 File Offset: 0x00004A94
		public TextAnchor alignment
		{
			get
			{
				return this.m_Alignment;
			}
			set
			{
				this.m_Alignment = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000068A0 File Offset: 0x00004AA0
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000068A8 File Offset: 0x00004AA8
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000068B4 File Offset: 0x00004AB4
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000068BC File Offset: 0x00004ABC
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_HorizontalOverflow;
			}
			set
			{
				this.m_HorizontalOverflow = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000068C8 File Offset: 0x00004AC8
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000068D0 File Offset: 0x00004AD0
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_VerticalOverflow;
			}
			set
			{
				this.m_VerticalOverflow = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000068DC File Offset: 0x00004ADC
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000068E4 File Offset: 0x00004AE4
		public float lineSpacing
		{
			get
			{
				return this.m_LineSpacing;
			}
			set
			{
				this.m_LineSpacing = value;
			}
		}

		// Token: 0x040000F2 RID: 242
		[FormerlySerializedAs("font")]
		[SerializeField]
		private Font m_Font;

		// Token: 0x040000F3 RID: 243
		[SerializeField]
		[FormerlySerializedAs("fontSize")]
		private int m_FontSize;

		// Token: 0x040000F4 RID: 244
		[FormerlySerializedAs("fontStyle")]
		[SerializeField]
		private FontStyle m_FontStyle;

		// Token: 0x040000F5 RID: 245
		[SerializeField]
		private bool m_BestFit;

		// Token: 0x040000F6 RID: 246
		[SerializeField]
		private int m_MinSize;

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		private int m_MaxSize;

		// Token: 0x040000F8 RID: 248
		[SerializeField]
		[FormerlySerializedAs("alignment")]
		private TextAnchor m_Alignment;

		// Token: 0x040000F9 RID: 249
		[FormerlySerializedAs("richText")]
		[SerializeField]
		private bool m_RichText;

		// Token: 0x040000FA RID: 250
		[SerializeField]
		private HorizontalWrapMode m_HorizontalOverflow;

		// Token: 0x040000FB RID: 251
		[SerializeField]
		private VerticalWrapMode m_VerticalOverflow;

		// Token: 0x040000FC RID: 252
		[SerializeField]
		private float m_LineSpacing;
	}
}
