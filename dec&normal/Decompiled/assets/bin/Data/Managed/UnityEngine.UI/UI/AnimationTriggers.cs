using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class AnimationTriggers
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005120 File Offset: 0x00003320
		public string normalTrigger
		{
			get
			{
				return this.m_NormalTrigger;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005128 File Offset: 0x00003328
		public string highlightedTrigger
		{
			get
			{
				return this.m_HighlightedTrigger;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005130 File Offset: 0x00003330
		public string pressedTrigger
		{
			get
			{
				return this.m_PressedTrigger;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005138 File Offset: 0x00003338
		public string disabledTrigger
		{
			get
			{
				return this.m_DisabledTrigger;
			}
		}

		// Token: 0x04000088 RID: 136
		private const string kDefaultNormalAnimName = "Normal";

		// Token: 0x04000089 RID: 137
		private const string kDefaultSelectedAnimName = "Highlighted";

		// Token: 0x0400008A RID: 138
		private const string kDefaultPressedAnimName = "Pressed";

		// Token: 0x0400008B RID: 139
		private const string kDefaultDisabledAnimName = "Disabled";

		// Token: 0x0400008C RID: 140
		[SerializeField]
		[FormerlySerializedAs("normalTrigger")]
		private string m_NormalTrigger = "Normal";

		// Token: 0x0400008D RID: 141
		[SerializeField]
		[FormerlySerializedAs("m_SelectedTrigger")]
		[FormerlySerializedAs("highlightedTrigger")]
		private string m_HighlightedTrigger = "Highlighted";

		// Token: 0x0400008E RID: 142
		[SerializeField]
		[FormerlySerializedAs("pressedTrigger")]
		private string m_PressedTrigger = "Pressed";

		// Token: 0x0400008F RID: 143
		[SerializeField]
		[FormerlySerializedAs("disabledTrigger")]
		private string m_DisabledTrigger = "Disabled";
	}
}
