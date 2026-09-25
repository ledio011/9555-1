using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000093 RID: 147
	internal class NonBacktrackingGroup : Group
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000D60C File Offset: 0x0000B80C
		public override void Compile(ICompiler cmp, bool reverse)
		{
			LinkRef linkRef = cmp.NewLink();
			cmp.EmitSub(linkRef);
			base.Compile(cmp, reverse);
			cmp.EmitTrue();
			cmp.ResolveLink(linkRef);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000D63C File Offset: 0x0000B83C
		public override bool IsComplex()
		{
			return true;
		}
	}
}
