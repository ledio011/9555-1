using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000092 RID: 146
	internal class Literal : Expression
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000D558 File Offset: 0x0000B758
		public Literal(string str, bool ignore)
		{
			this.str = str;
			this.ignore = ignore;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000D570 File Offset: 0x0000B770
		public static void CompileLiteral(string str, ICompiler cmp, bool ignore, bool reverse)
		{
			if (str.Length == 0)
			{
				return;
			}
			if (str.Length == 1)
			{
				cmp.EmitCharacter(str[0], false, ignore, reverse);
			}
			else
			{
				cmp.EmitString(str, ignore, reverse);
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public override void Compile(ICompiler cmp, bool reverse)
		{
			Literal.CompileLiteral(this.str, cmp, this.ignore, reverse);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		public override void GetWidth(out int min, out int max)
		{
			min = (max = this.str.Length);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		public override AnchorInfo GetAnchorInfo(bool reverse)
		{
			return new AnchorInfo(this, 0, this.str.Length, this.str, this.ignore);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000D600 File Offset: 0x0000B800
		public override bool IsComplex()
		{
			return false;
		}

		// Token: 0x04000A27 RID: 2599
		private string str;

		// Token: 0x04000A28 RID: 2600
		private bool ignore;
	}
}
