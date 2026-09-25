using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000076 RID: 118
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.All)]
	[Serializable]
	public sealed class CLSCompliantAttribute : Attribute
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00012950 File Offset: 0x00010B50
		public CLSCompliantAttribute(bool isCompliant)
		{
			this.is_compliant = isCompliant;
		}

		// Token: 0x040001C6 RID: 454
		private bool is_compliant;
	}
}
