using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200013A RID: 314
	[StructLayout(0)]
	public sealed class WaitForSeconds : YieldInstruction
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x0001B0A4 File Offset: 0x000192A4
		public WaitForSeconds(float seconds)
		{
			this.m_Seconds = seconds;
		}

		// Token: 0x040004F6 RID: 1270
		internal float m_Seconds;
	}
}
