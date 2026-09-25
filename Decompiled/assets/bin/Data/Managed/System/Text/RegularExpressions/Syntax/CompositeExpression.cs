using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200008D RID: 141
	internal abstract class CompositeExpression : Expression
	{
		// Token: 0x060002DF RID: 735 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		public CompositeExpression()
		{
			this.expressions = new ExpressionCollection();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000CF04 File Offset: 0x0000B104
		protected ExpressionCollection Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000CF0C File Offset: 0x0000B10C
		protected void GetWidth(out int min, out int max, int count)
		{
			min = int.MaxValue;
			max = 0;
			bool flag = true;
			for (int i = 0; i < count; i++)
			{
				Expression expression = this.Expressions[i];
				if (expression != null)
				{
					flag = false;
					int num;
					int num2;
					expression.GetWidth(out num, out num2);
					if (num < min)
					{
						min = num;
					}
					if (num2 > max)
					{
						max = num2;
					}
				}
			}
			if (flag)
			{
				min = (max = 0);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000CF84 File Offset: 0x0000B184
		public override bool IsComplex()
		{
			foreach (object obj in this.Expressions)
			{
				Expression expression = (Expression)obj;
				if (expression.IsComplex())
				{
					return true;
				}
			}
			return base.GetFixedWidth() <= 0;
		}

		// Token: 0x04000A24 RID: 2596
		private ExpressionCollection expressions;
	}
}
