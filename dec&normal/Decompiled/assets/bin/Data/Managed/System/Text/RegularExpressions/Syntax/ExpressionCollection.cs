using System;
using System.Collections;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000090 RID: 144
	internal class ExpressionCollection : CollectionBase
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x0000D160 File Offset: 0x0000B360
		public void Add(Expression e)
		{
			base.List.Add(e);
		}

		// Token: 0x170000AD RID: 173
		public Expression this[int i]
		{
			get
			{
				return (Expression)base.List[i];
			}
			set
			{
				base.List[i] = value;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000D194 File Offset: 0x0000B394
		protected override void OnValidate(object o)
		{
		}
	}
}
