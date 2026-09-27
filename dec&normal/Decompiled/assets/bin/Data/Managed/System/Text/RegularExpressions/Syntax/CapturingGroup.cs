using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200008B RID: 139
	internal class CapturingGroup : Group, IComparable
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000C990 File Offset: 0x0000AB90
		public CapturingGroup()
		{
			this.gid = 0;
			this.name = null;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		public int Index
		{
			get
			{
				return this.gid;
			}
			set
			{
				this.gid = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		public bool IsNamed
		{
			get
			{
				return this.name != null;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public override void Compile(ICompiler cmp, bool reverse)
		{
			cmp.EmitOpen(this.gid);
			base.Compile(cmp, reverse);
			cmp.EmitClose(this.gid);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000CA04 File Offset: 0x0000AC04
		public override bool IsComplex()
		{
			return true;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000CA08 File Offset: 0x0000AC08
		public int CompareTo(object other)
		{
			return this.gid - ((CapturingGroup)other).gid;
		}

		// Token: 0x04000A1B RID: 2587
		private int gid;

		// Token: 0x04000A1C RID: 2588
		private string name;
	}
}
