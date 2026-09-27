using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	public struct Navigation
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000F368 File Offset: 0x0000D568
		public Navigation.Mode mode
		{
			get
			{
				return this.m_Mode;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000F370 File Offset: 0x0000D570
		public Selectable selectOnUp
		{
			get
			{
				return this.m_SelectOnUp;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000F378 File Offset: 0x0000D578
		public Selectable selectOnDown
		{
			get
			{
				return this.m_SelectOnDown;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000F380 File Offset: 0x0000D580
		public Selectable selectOnLeft
		{
			get
			{
				return this.m_SelectOnLeft;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000F388 File Offset: 0x0000D588
		public Selectable selectOnRight
		{
			get
			{
				return this.m_SelectOnRight;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000F390 File Offset: 0x0000D590
		public static Navigation defaultNavigation
		{
			get
			{
				return new Navigation
				{
					m_Mode = Navigation.Mode.Automatic
				};
			}
		}

		// Token: 0x040001C8 RID: 456
		[FormerlySerializedAs("mode")]
		[SerializeField]
		private Navigation.Mode m_Mode;

		// Token: 0x040001C9 RID: 457
		[FormerlySerializedAs("selectOnUp")]
		[SerializeField]
		private Selectable m_SelectOnUp;

		// Token: 0x040001CA RID: 458
		[SerializeField]
		[FormerlySerializedAs("selectOnDown")]
		private Selectable m_SelectOnDown;

		// Token: 0x040001CB RID: 459
		[SerializeField]
		[FormerlySerializedAs("selectOnLeft")]
		private Selectable m_SelectOnLeft;

		// Token: 0x040001CC RID: 460
		[SerializeField]
		[FormerlySerializedAs("selectOnRight")]
		private Selectable m_SelectOnRight;

		// Token: 0x02000075 RID: 117
		[Flags]
		public enum Mode
		{
			// Token: 0x040001CE RID: 462
			None = 0,
			// Token: 0x040001CF RID: 463
			Horizontal = 1,
			// Token: 0x040001D0 RID: 464
			Vertical = 2,
			// Token: 0x040001D1 RID: 465
			Automatic = 3,
			// Token: 0x040001D2 RID: 466
			Explicit = 4
		}
	}
}
