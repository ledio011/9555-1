using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public struct ColorBlock
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000615C File Offset: 0x0000435C
		public Color normalColor
		{
			get
			{
				return this.m_NormalColor;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006164 File Offset: 0x00004364
		public Color highlightedColor
		{
			get
			{
				return this.m_HighlightedColor;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000616C File Offset: 0x0000436C
		public Color pressedColor
		{
			get
			{
				return this.m_PressedColor;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006174 File Offset: 0x00004374
		public Color disabledColor
		{
			get
			{
				return this.m_DisabledColor;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000617C File Offset: 0x0000437C
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00006184 File Offset: 0x00004384
		public float colorMultiplier
		{
			get
			{
				return this.m_ColorMultiplier;
			}
			set
			{
				this.m_ColorMultiplier = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00006190 File Offset: 0x00004390
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00006198 File Offset: 0x00004398
		public float fadeDuration
		{
			get
			{
				return this.m_FadeDuration;
			}
			set
			{
				this.m_FadeDuration = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000061A4 File Offset: 0x000043A4
		public static ColorBlock defaultColorBlock
		{
			get
			{
				return new ColorBlock
				{
					m_NormalColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue),
					m_HighlightedColor = new Color32(245, 245, 245, byte.MaxValue),
					m_PressedColor = new Color32(200, 200, 200, byte.MaxValue),
					m_DisabledColor = new Color32(200, 200, 200, 128),
					colorMultiplier = 1f,
					fadeDuration = 0.1f
				};
			}
		}

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		[FormerlySerializedAs("normalColor")]
		private Color m_NormalColor;

		// Token: 0x040000D1 RID: 209
		[SerializeField]
		[FormerlySerializedAs("m_SelectedColor")]
		[FormerlySerializedAs("highlightedColor")]
		private Color m_HighlightedColor;

		// Token: 0x040000D2 RID: 210
		[FormerlySerializedAs("pressedColor")]
		[SerializeField]
		private Color m_PressedColor;

		// Token: 0x040000D3 RID: 211
		[SerializeField]
		[FormerlySerializedAs("disabledColor")]
		private Color m_DisabledColor;

		// Token: 0x040000D4 RID: 212
		[Range(1f, 5f)]
		[SerializeField]
		private float m_ColorMultiplier;

		// Token: 0x040000D5 RID: 213
		[SerializeField]
		[FormerlySerializedAs("fadeDuration")]
		private float m_FadeDuration;
	}
}
