using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000098 RID: 152
	internal class Repetition : CompositeExpression
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000F7EC File Offset: 0x0000D9EC
		public Repetition(int min, int max, bool lazy)
		{
			base.Expressions.Add(null);
			this.min = min;
			this.max = max;
			this.lazy = lazy;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000F818 File Offset: 0x0000DA18
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000F828 File Offset: 0x0000DA28
		public Expression Expression
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000F838 File Offset: 0x0000DA38
		public int Minimum
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000F840 File Offset: 0x0000DA40
		public override void Compile(ICompiler cmp, bool reverse)
		{
			if (this.Expression.IsComplex())
			{
				LinkRef linkRef = cmp.NewLink();
				cmp.EmitRepeat(this.min, this.max, this.lazy, linkRef);
				this.Expression.Compile(cmp, reverse);
				cmp.EmitUntil(linkRef);
			}
			else
			{
				LinkRef linkRef2 = cmp.NewLink();
				cmp.EmitFastRepeat(this.min, this.max, this.lazy, linkRef2);
				this.Expression.Compile(cmp, reverse);
				cmp.EmitTrue();
				cmp.ResolveLink(linkRef2);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		public override void GetWidth(out int min, out int max)
		{
			this.Expression.GetWidth(out min, out max);
			min *= this.min;
			if (max == 2147483647 || this.max == 65535)
			{
				max = int.MaxValue;
			}
			else
			{
				max *= this.max;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000F928 File Offset: 0x0000DB28
		public override AnchorInfo GetAnchorInfo(bool reverse)
		{
			int fixedWidth = base.GetFixedWidth();
			if (this.Minimum == 0)
			{
				return new AnchorInfo(this, fixedWidth);
			}
			AnchorInfo anchorInfo = this.Expression.GetAnchorInfo(reverse);
			if (anchorInfo.IsPosition)
			{
				return new AnchorInfo(this, anchorInfo.Offset, fixedWidth, anchorInfo.Position);
			}
			if (!anchorInfo.IsSubstring)
			{
				return new AnchorInfo(this, fixedWidth);
			}
			if (anchorInfo.IsComplete)
			{
				string substring = anchorInfo.Substring;
				StringBuilder stringBuilder = new StringBuilder(substring);
				for (int i = 1; i < this.Minimum; i++)
				{
					stringBuilder.Append(substring);
				}
				return new AnchorInfo(this, 0, fixedWidth, stringBuilder.ToString(), anchorInfo.IgnoreCase);
			}
			return new AnchorInfo(this, anchorInfo.Offset, fixedWidth, anchorInfo.Substring, anchorInfo.IgnoreCase);
		}

		// Token: 0x04000A33 RID: 2611
		private int min;

		// Token: 0x04000A34 RID: 2612
		private int max;

		// Token: 0x04000A35 RID: 2613
		private bool lazy;
	}
}
