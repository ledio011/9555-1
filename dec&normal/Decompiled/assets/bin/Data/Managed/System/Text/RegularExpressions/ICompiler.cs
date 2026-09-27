using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000067 RID: 103
	internal interface ICompiler
	{
		// Token: 0x060001B7 RID: 439
		IMachineFactory GetMachineFactory();

		// Token: 0x060001B8 RID: 440
		void EmitFalse();

		// Token: 0x060001B9 RID: 441
		void EmitTrue();

		// Token: 0x060001BA RID: 442
		void EmitCharacter(char c, bool negate, bool ignore, bool reverse);

		// Token: 0x060001BB RID: 443
		void EmitCategory(Category cat, bool negate, bool reverse);

		// Token: 0x060001BC RID: 444
		void EmitNotCategory(Category cat, bool negate, bool reverse);

		// Token: 0x060001BD RID: 445
		void EmitRange(char lo, char hi, bool negate, bool ignore, bool reverse);

		// Token: 0x060001BE RID: 446
		void EmitSet(char lo, BitArray set, bool negate, bool ignore, bool reverse);

		// Token: 0x060001BF RID: 447
		void EmitString(string str, bool ignore, bool reverse);

		// Token: 0x060001C0 RID: 448
		void EmitPosition(Position pos);

		// Token: 0x060001C1 RID: 449
		void EmitOpen(int gid);

		// Token: 0x060001C2 RID: 450
		void EmitClose(int gid);

		// Token: 0x060001C3 RID: 451
		void EmitBalanceStart(int gid, int balance, bool capture, LinkRef tail);

		// Token: 0x060001C4 RID: 452
		void EmitBalance();

		// Token: 0x060001C5 RID: 453
		void EmitReference(int gid, bool ignore, bool reverse);

		// Token: 0x060001C6 RID: 454
		void EmitIfDefined(int gid, LinkRef tail);

		// Token: 0x060001C7 RID: 455
		void EmitSub(LinkRef tail);

		// Token: 0x060001C8 RID: 456
		void EmitTest(LinkRef yes, LinkRef tail);

		// Token: 0x060001C9 RID: 457
		void EmitBranch(LinkRef next);

		// Token: 0x060001CA RID: 458
		void EmitJump(LinkRef target);

		// Token: 0x060001CB RID: 459
		void EmitRepeat(int min, int max, bool lazy, LinkRef until);

		// Token: 0x060001CC RID: 460
		void EmitUntil(LinkRef repeat);

		// Token: 0x060001CD RID: 461
		void EmitIn(LinkRef tail);

		// Token: 0x060001CE RID: 462
		void EmitInfo(int count, int min, int max);

		// Token: 0x060001CF RID: 463
		void EmitFastRepeat(int min, int max, bool lazy, LinkRef tail);

		// Token: 0x060001D0 RID: 464
		void EmitAnchor(bool reverse, int offset, LinkRef tail);

		// Token: 0x060001D1 RID: 465
		void EmitBranchEnd();

		// Token: 0x060001D2 RID: 466
		void EmitAlternationEnd();

		// Token: 0x060001D3 RID: 467
		LinkRef NewLink();

		// Token: 0x060001D4 RID: 468
		void ResolveLink(LinkRef link);
	}
}
