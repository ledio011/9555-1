using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000095 RID: 149
	internal class PositionAssertion : Expression
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000F660 File Offset: 0x0000D860
		public PositionAssertion(Position pos)
		{
			this.pos = pos;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F670 File Offset: 0x0000D870
		public override void Compile(ICompiler cmp, bool reverse)
		{
			cmp.EmitPosition(this.pos);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000F680 File Offset: 0x0000D880
		public override void GetWidth(out int min, out int max)
		{
			min = (max = 0);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000F698 File Offset: 0x0000D898
		public override bool IsComplex()
		{
			return false;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F69C File Offset: 0x0000D89C
		public override AnchorInfo GetAnchorInfo(bool revers)
		{
			switch (this.pos)
			{
			case Position.StartOfString:
			case Position.StartOfLine:
			case Position.StartOfScan:
				return new AnchorInfo(this, 0, 0, this.pos);
			default:
				return new AnchorInfo(this, 0);
			}
		}

		// Token: 0x04000A2F RID: 2607
		private Position pos;
	}
}
