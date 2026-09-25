using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200007F RID: 127
	[Serializable]
	public sealed class GUISettings
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000FC04 File Offset: 0x0000DE04
		public Color cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
		}

		// Token: 0x0600060B RID: 1547
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float Internal_GetCursorFlashSpeed();

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000FC0C File Offset: 0x0000DE0C
		public float cursorFlashSpeed
		{
			get
			{
				if (this.m_CursorFlashSpeed >= 0f)
				{
					return this.m_CursorFlashSpeed;
				}
				return GUISettings.Internal_GetCursorFlashSpeed();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
		}

		// Token: 0x0400015C RID: 348
		[SerializeField]
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x0400015D RID: 349
		[SerializeField]
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x0400015E RID: 350
		[SerializeField]
		private Color m_CursorColor = Color.white;

		// Token: 0x0400015F RID: 351
		[SerializeField]
		private float m_CursorFlashSpeed = -1f;

		// Token: 0x04000160 RID: 352
		[SerializeField]
		private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);
	}
}
