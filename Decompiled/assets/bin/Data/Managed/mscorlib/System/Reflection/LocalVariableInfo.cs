using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001CC RID: 460
	[ComVisible(true)]
	public class LocalVariableInfo
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x00042214 File Offset: 0x00040414
		internal LocalVariableInfo()
		{
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0004221C File Offset: 0x0004041C
		public override string ToString()
		{
			if (this.is_pinned)
			{
				return string.Format("{0} ({1}) (pinned)", this.type, this.position);
			}
			return string.Format("{0} ({1})", this.type, this.position);
		}

		// Token: 0x040008B6 RID: 2230
		internal Type type;

		// Token: 0x040008B7 RID: 2231
		internal bool is_pinned;

		// Token: 0x040008B8 RID: 2232
		internal ushort position;
	}
}
