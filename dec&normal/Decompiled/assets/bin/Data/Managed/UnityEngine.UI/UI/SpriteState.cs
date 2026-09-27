using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200008B RID: 139
	[Serializable]
	public struct SpriteState
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001321C File Offset: 0x0001141C
		public Sprite highlightedSprite
		{
			get
			{
				return this.m_HighlightedSprite;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00013224 File Offset: 0x00011424
		public Sprite pressedSprite
		{
			get
			{
				return this.m_PressedSprite;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001322C File Offset: 0x0001142C
		public Sprite disabledSprite
		{
			get
			{
				return this.m_DisabledSprite;
			}
		}

		// Token: 0x04000240 RID: 576
		[FormerlySerializedAs("highlightedSprite")]
		[FormerlySerializedAs("m_SelectedSprite")]
		[SerializeField]
		private Sprite m_HighlightedSprite;

		// Token: 0x04000241 RID: 577
		[SerializeField]
		[FormerlySerializedAs("pressedSprite")]
		private Sprite m_PressedSprite;

		// Token: 0x04000242 RID: 578
		[SerializeField]
		[FormerlySerializedAs("disabledSprite")]
		private Sprite m_DisabledSprite;
	}
}
