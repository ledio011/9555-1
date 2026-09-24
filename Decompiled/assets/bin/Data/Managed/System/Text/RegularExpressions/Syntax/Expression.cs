using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200008E RID: 142
	internal abstract class Expression
	{
		// Token: 0x060002E4 RID: 740
		public abstract void Compile(ICompiler cmp, bool reverse);

		// Token: 0x060002E5 RID: 741
		public abstract void GetWidth(out int min, out int max);

		// Token: 0x060002E6 RID: 742 RVA: 0x0000D008 File Offset: 0x0000B208
		public int GetFixedWidth()
		{
			int num;
			int num2;
			this.GetWidth(out num, out num2);
			if (num == num2)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000D02C File Offset: 0x0000B22C
		public virtual AnchorInfo GetAnchorInfo(bool reverse)
		{
			return new AnchorInfo(this, this.GetFixedWidth());
		}

		// Token: 0x060002E8 RID: 744
		public abstract bool IsComplex();
	}
}
