using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000087 RID: 135
	internal abstract class Assertion : CompositeExpression
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		public Assertion()
		{
			base.Expressions.Add(null);
			base.Expressions.Add(null);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		public Expression TrueExpression
		{
			get
			{
				return base.Expressions[0];
			}
			set
			{
				base.Expressions[0] = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000C600 File Offset: 0x0000A800
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000C610 File Offset: 0x0000A810
		public Expression FalseExpression
		{
			get
			{
				return base.Expressions[1];
			}
			set
			{
				base.Expressions[1] = value;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C620 File Offset: 0x0000A820
		public override void GetWidth(out int min, out int max)
		{
			base.GetWidth(out min, out max, 2);
			if (this.TrueExpression == null || this.FalseExpression == null)
			{
				min = 0;
			}
		}
	}
}
