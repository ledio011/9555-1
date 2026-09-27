using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000089 RID: 137
	internal class BalancingGroup : CapturingGroup
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0000C764 File Offset: 0x0000A964
		public BalancingGroup()
		{
			this.balance = null;
		}

		// Token: 0x170000A3 RID: 163
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000C774 File Offset: 0x0000A974
		public CapturingGroup Balance
		{
			set
			{
				this.balance = value;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C780 File Offset: 0x0000A980
		public override void Compile(ICompiler cmp, bool reverse)
		{
			LinkRef linkRef = cmp.NewLink();
			cmp.EmitBalanceStart(base.Index, this.balance.Index, base.IsNamed, linkRef);
			int count = base.Expressions.Count;
			for (int i = 0; i < count; i++)
			{
				Expression expression;
				if (reverse)
				{
					expression = base.Expressions[count - i - 1];
				}
				else
				{
					expression = base.Expressions[i];
				}
				expression.Compile(cmp, reverse);
			}
			cmp.EmitBalance();
			cmp.ResolveLink(linkRef);
		}

		// Token: 0x04000A17 RID: 2583
		private CapturingGroup balance;
	}
}
