using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	public class Match : Group
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000AB10 File Offset: 0x00008D10
		private Match()
		{
			this.regex = null;
			this.machine = null;
			this.text_length = 0;
			this.groups = new GroupCollection(1, 1);
			this.groups.SetValue(this, 0);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000AB48 File Offset: 0x00008D48
		internal Match(Regex regex, IMachine machine, string text, int text_length, int n_groups, int index, int length) : base(text, index, length)
		{
			this.regex = regex;
			this.machine = machine;
			this.text_length = text_length;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000AB6C File Offset: 0x00008D6C
		internal Match(Regex regex, IMachine machine, string text, int text_length, int n_groups, int index, int length, int n_caps) : base(text, index, length, n_caps)
		{
			this.regex = regex;
			this.machine = machine;
			this.text_length = text_length;
			this.groups = new GroupCollection(n_groups, regex.Gap);
			this.groups.SetValue(this, 0);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public static Match Empty
		{
			get
			{
				return Match.empty;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public virtual GroupCollection Groups
		{
			get
			{
				return this.groups;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public Match NextMatch()
		{
			if (this == Match.Empty)
			{
				return Match.Empty;
			}
			int num = (!this.regex.RightToLeft) ? (base.Index + base.Length) : base.Index;
			if (base.Length == 0)
			{
				num += ((!this.regex.RightToLeft) ? 1 : -1);
			}
			return this.machine.Scan(this.regex, base.Text, num, this.text_length);
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000AC64 File Offset: 0x00008E64
		internal Regex Regex
		{
			get
			{
				return this.regex;
			}
		}

		// Token: 0x040009B2 RID: 2482
		private Regex regex;

		// Token: 0x040009B3 RID: 2483
		private IMachine machine;

		// Token: 0x040009B4 RID: 2484
		private int text_length;

		// Token: 0x040009B5 RID: 2485
		private GroupCollection groups;

		// Token: 0x040009B6 RID: 2486
		private static Match empty = new Match();
	}
}
