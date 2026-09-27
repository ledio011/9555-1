using System;

namespace UnityEngine
{
	// Token: 0x02000123 RID: 291
	public struct Touch
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00019760 File Offset: 0x00017960
		public int fingerId
		{
			get
			{
				return this.m_FingerId;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00019768 File Offset: 0x00017968
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00019770 File Offset: 0x00017970
		public int tapCount
		{
			get
			{
				return this.m_TapCount;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00019778 File Offset: 0x00017978
		public TouchPhase phase
		{
			get
			{
				return this.m_Phase;
			}
		}

		// Token: 0x040004B2 RID: 1202
		private int m_FingerId;

		// Token: 0x040004B3 RID: 1203
		private Vector2 m_Position;

		// Token: 0x040004B4 RID: 1204
		private Vector2 m_RawPosition;

		// Token: 0x040004B5 RID: 1205
		private Vector2 m_PositionDelta;

		// Token: 0x040004B6 RID: 1206
		private float m_TimeDelta;

		// Token: 0x040004B7 RID: 1207
		private int m_TapCount;

		// Token: 0x040004B8 RID: 1208
		private TouchPhase m_Phase;
	}
}
