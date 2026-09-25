using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200008F RID: 143
	internal class ExpressionAssertion : Assertion
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000D03C File Offset: 0x0000B23C
		public ExpressionAssertion()
		{
			base.Expressions.Add(null);
		}

		// Token: 0x170000AA RID: 170
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000D050 File Offset: 0x0000B250
		public bool Reverse
		{
			set
			{
				this.reverse = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000D05C File Offset: 0x0000B25C
		public bool Negate
		{
			set
			{
				this.negate = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000D068 File Offset: 0x0000B268
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000D078 File Offset: 0x0000B278
		public Expression TestExpression
		{
			get
			{
				return base.Expressions[2];
			}
			set
			{
				base.Expressions[2] = value;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000D088 File Offset: 0x0000B288
		public override void Compile(ICompiler cmp, bool reverse)
		{
			LinkRef linkRef = cmp.NewLink();
			LinkRef linkRef2 = cmp.NewLink();
			if (!this.negate)
			{
				cmp.EmitTest(linkRef, linkRef2);
			}
			else
			{
				cmp.EmitTest(linkRef2, linkRef);
			}
			this.TestExpression.Compile(cmp, this.reverse);
			cmp.EmitTrue();
			if (base.TrueExpression == null)
			{
				cmp.ResolveLink(linkRef2);
				cmp.EmitFalse();
				cmp.ResolveLink(linkRef);
			}
			else
			{
				cmp.ResolveLink(linkRef);
				base.TrueExpression.Compile(cmp, reverse);
				if (base.FalseExpression == null)
				{
					cmp.ResolveLink(linkRef2);
				}
				else
				{
					LinkRef linkRef3 = cmp.NewLink();
					cmp.EmitJump(linkRef3);
					cmp.ResolveLink(linkRef2);
					base.FalseExpression.Compile(cmp, reverse);
					cmp.ResolveLink(linkRef3);
				}
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000D154 File Offset: 0x0000B354
		public override bool IsComplex()
		{
			return true;
		}

		// Token: 0x04000A25 RID: 2597
		private bool reverse;

		// Token: 0x04000A26 RID: 2598
		private bool negate;
	}
}
