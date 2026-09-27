using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000097 RID: 151
	internal class RegularExpression : Group
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000F738 File Offset: 0x0000D938
		public RegularExpression()
		{
			this.group_count = 0;
		}

		// Token: 0x170000B0 RID: 176
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000F748 File Offset: 0x0000D948
		public int GroupCount
		{
			set
			{
				this.group_count = value;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000F754 File Offset: 0x0000D954
		public override void Compile(ICompiler cmp, bool reverse)
		{
			int min;
			int max;
			this.GetWidth(out min, out max);
			cmp.EmitInfo(this.group_count, min, max);
			AnchorInfo anchorInfo = this.GetAnchorInfo(reverse);
			LinkRef linkRef = cmp.NewLink();
			cmp.EmitAnchor(reverse, anchorInfo.Offset, linkRef);
			if (anchorInfo.IsPosition)
			{
				cmp.EmitPosition(anchorInfo.Position);
			}
			else if (anchorInfo.IsSubstring)
			{
				cmp.EmitString(anchorInfo.Substring, anchorInfo.IgnoreCase, reverse);
			}
			cmp.EmitTrue();
			cmp.ResolveLink(linkRef);
			base.Compile(cmp, reverse);
			cmp.EmitTrue();
		}

		// Token: 0x04000A32 RID: 2610
		private int group_count;
	}
}
