using System;
using System.Collections;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000088 RID: 136
	internal class BackslashNumber : Reference
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000C644 File Offset: 0x0000A844
		public BackslashNumber(bool ignore, bool ecma) : base(ignore)
		{
			this.ecma = ecma;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C654 File Offset: 0x0000A854
		public bool ResolveReference(string num_str, Hashtable groups)
		{
			if (this.ecma)
			{
				int num = 0;
				for (int i = 1; i < num_str.Length; i++)
				{
					if (groups[num_str.Substring(0, i)] != null)
					{
						num = i;
					}
				}
				if (num != 0)
				{
					base.CapturingGroup = (CapturingGroup)groups[num_str.Substring(0, num)];
					this.literal = num_str.Substring(num);
					return true;
				}
			}
			else if (num_str.Length == 1)
			{
				return false;
			}
			int num2 = 0;
			int num3 = Parser.ParseOctal(num_str, ref num2);
			if (num3 == -1)
			{
				return false;
			}
			if (num3 > 255 && this.ecma)
			{
				num3 /= 8;
				num2--;
			}
			num3 &= 255;
			this.literal = (char)num3 + num_str.Substring(num2);
			return true;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000C730 File Offset: 0x0000A930
		public override void Compile(ICompiler cmp, bool reverse)
		{
			if (base.CapturingGroup != null)
			{
				base.Compile(cmp, reverse);
			}
			if (this.literal != null)
			{
				Literal.CompileLiteral(this.literal, cmp, base.IgnoreCase, reverse);
			}
		}

		// Token: 0x04000A15 RID: 2581
		private string literal;

		// Token: 0x04000A16 RID: 2582
		private bool ecma;
	}
}
