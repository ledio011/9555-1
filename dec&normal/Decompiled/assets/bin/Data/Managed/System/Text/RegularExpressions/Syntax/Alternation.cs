using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000085 RID: 133
	internal class Alternation : CompositeExpression
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000C378 File Offset: 0x0000A578
		public ExpressionCollection Alternatives
		{
			get
			{
				return base.Expressions;
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C380 File Offset: 0x0000A580
		public void AddAlternative(Expression e)
		{
			this.Alternatives.Add(e);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C390 File Offset: 0x0000A590
		public override void Compile(ICompiler cmp, bool reverse)
		{
			LinkRef linkRef = cmp.NewLink();
			foreach (object obj in this.Alternatives)
			{
				Expression expression = (Expression)obj;
				LinkRef linkRef2 = cmp.NewLink();
				cmp.EmitBranch(linkRef2);
				expression.Compile(cmp, reverse);
				cmp.EmitJump(linkRef);
				cmp.ResolveLink(linkRef2);
				cmp.EmitBranchEnd();
			}
			cmp.EmitFalse();
			cmp.ResolveLink(linkRef);
			cmp.EmitAlternationEnd();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000C434 File Offset: 0x0000A634
		public override void GetWidth(out int min, out int max)
		{
			base.GetWidth(out min, out max, this.Alternatives.Count);
		}
	}
}
