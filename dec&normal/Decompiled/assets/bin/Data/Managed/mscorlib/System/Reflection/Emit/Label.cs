using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001A8 RID: 424
	[ComVisible(true)]
	[Serializable]
	public struct Label
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x0003D770 File Offset: 0x0003B970
		internal Label(int val)
		{
			this.label = val;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0003D77C File Offset: 0x0003B97C
		public override bool Equals(object obj)
		{
			bool flag = obj is Label;
			if (flag)
			{
				Label label = (Label)obj;
				flag = (this.label == label.label);
			}
			return flag;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0003D7B4 File Offset: 0x0003B9B4
		public override int GetHashCode()
		{
			return this.label.GetHashCode();
		}

		// Token: 0x040006D1 RID: 1745
		internal int label;
	}
}
