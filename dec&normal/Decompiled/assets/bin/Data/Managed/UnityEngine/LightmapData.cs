using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000A5 RID: 165
	[StructLayout(0)]
	public sealed class LightmapData
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00011A34 File Offset: 0x0000FC34
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x00011A3C File Offset: 0x0000FC3C
		public Texture2D lightmapFar
		{
			get
			{
				return this.m_Lightmap;
			}
			set
			{
				this.m_Lightmap = value;
			}
		}

		// Token: 0x040002D3 RID: 723
		internal Texture2D m_Lightmap;

		// Token: 0x040002D4 RID: 724
		internal Texture2D m_IndirectLightmap;
	}
}
