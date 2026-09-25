using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000086 RID: 134
	internal class AnchorInfo
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000C44C File Offset: 0x0000A64C
		public AnchorInfo(Expression expr, int width)
		{
			this.expr = expr;
			this.offset = 0;
			this.width = width;
			this.str = null;
			this.ignore = false;
			this.pos = Position.Any;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000C480 File Offset: 0x0000A680
		public AnchorInfo(Expression expr, int offset, int width, string str, bool ignore)
		{
			this.expr = expr;
			this.offset = offset;
			this.width = width;
			this.str = ((!ignore) ? str : str.ToLower());
			this.ignore = ignore;
			this.pos = Position.Any;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		public AnchorInfo(Expression expr, int offset, int width, Position pos)
		{
			this.expr = expr;
			this.offset = offset;
			this.width = width;
			this.pos = pos;
			this.str = null;
			this.ignore = false;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000C508 File Offset: 0x0000A708
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000C510 File Offset: 0x0000A710
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000C518 File Offset: 0x0000A718
		public int Length
		{
			get
			{
				return (this.str == null) ? 0 : this.str.Length;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000C538 File Offset: 0x0000A738
		public bool IsUnknownWidth
		{
			get
			{
				return this.width < 0;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000C544 File Offset: 0x0000A744
		public bool IsComplete
		{
			get
			{
				return this.Length == this.Width;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000C554 File Offset: 0x0000A754
		public string Substring
		{
			get
			{
				return this.str;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000C55C File Offset: 0x0000A75C
		public bool IgnoreCase
		{
			get
			{
				return this.ignore;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000C564 File Offset: 0x0000A764
		public Position Position
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000C56C File Offset: 0x0000A76C
		public bool IsSubstring
		{
			get
			{
				return this.str != null;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000C57C File Offset: 0x0000A77C
		public bool IsPosition
		{
			get
			{
				return this.pos != Position.Any;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000C58C File Offset: 0x0000A78C
		public Interval GetInterval(int start)
		{
			if (!this.IsSubstring)
			{
				return Interval.Empty;
			}
			return new Interval(start + this.Offset, start + this.Offset + this.Length - 1);
		}

		// Token: 0x04000A0F RID: 2575
		private Expression expr;

		// Token: 0x04000A10 RID: 2576
		private Position pos;

		// Token: 0x04000A11 RID: 2577
		private int offset;

		// Token: 0x04000A12 RID: 2578
		private string str;

		// Token: 0x04000A13 RID: 2579
		private int width;

		// Token: 0x04000A14 RID: 2580
		private bool ignore;
	}
}
