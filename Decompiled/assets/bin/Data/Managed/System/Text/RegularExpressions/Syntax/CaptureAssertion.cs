using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200008A RID: 138
	internal class CaptureAssertion : Assertion
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000C810 File Offset: 0x0000AA10
		public CaptureAssertion(Literal l)
		{
			this.literal = l;
		}

		// Token: 0x170000A4 RID: 164
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000C820 File Offset: 0x0000AA20
		public CapturingGroup CapturingGroup
		{
			set
			{
				this.group = value;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000C82C File Offset: 0x0000AA2C
		public override void Compile(ICompiler cmp, bool reverse)
		{
			if (this.group == null)
			{
				this.Alternate.Compile(cmp, reverse);
				return;
			}
			int index = this.group.Index;
			LinkRef linkRef = cmp.NewLink();
			if (base.FalseExpression == null)
			{
				cmp.EmitIfDefined(index, linkRef);
				base.TrueExpression.Compile(cmp, reverse);
			}
			else
			{
				LinkRef linkRef2 = cmp.NewLink();
				cmp.EmitIfDefined(index, linkRef2);
				base.TrueExpression.Compile(cmp, reverse);
				cmp.EmitJump(linkRef);
				cmp.ResolveLink(linkRef2);
				base.FalseExpression.Compile(cmp, reverse);
			}
			cmp.ResolveLink(linkRef);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		public override bool IsComplex()
		{
			if (this.group == null)
			{
				return this.Alternate.IsComplex();
			}
			return (base.TrueExpression != null && base.TrueExpression.IsComplex()) || (base.FalseExpression != null && base.FalseExpression.IsComplex()) || base.GetFixedWidth() <= 0;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000C934 File Offset: 0x0000AB34
		private ExpressionAssertion Alternate
		{
			get
			{
				if (this.alternate == null)
				{
					this.alternate = new ExpressionAssertion();
					this.alternate.TrueExpression = base.TrueExpression;
					this.alternate.FalseExpression = base.FalseExpression;
					this.alternate.TestExpression = this.literal;
				}
				return this.alternate;
			}
		}

		// Token: 0x04000A18 RID: 2584
		private ExpressionAssertion alternate;

		// Token: 0x04000A19 RID: 2585
		private CapturingGroup group;

		// Token: 0x04000A1A RID: 2586
		private Literal literal;
	}
}
