using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000096 RID: 150
	internal class Reference : Expression
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		public Reference(bool ignore)
		{
			this.ignore = ignore;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		public CapturingGroup CapturingGroup
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000F704 File Offset: 0x0000D904
		public bool IgnoreCase
		{
			get
			{
				return this.ignore;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F70C File Offset: 0x0000D90C
		public override void Compile(ICompiler cmp, bool reverse)
		{
			cmp.EmitReference(this.group.Index, this.ignore, reverse);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F728 File Offset: 0x0000D928
		public override void GetWidth(out int min, out int max)
		{
			min = 0;
			max = int.MaxValue;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F734 File Offset: 0x0000D934
		public override bool IsComplex()
		{
			return true;
		}

		// Token: 0x04000A30 RID: 2608
		private CapturingGroup group;

		// Token: 0x04000A31 RID: 2609
		private bool ignore;
	}
}
