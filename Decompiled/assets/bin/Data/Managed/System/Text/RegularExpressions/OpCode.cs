using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200007A RID: 122
	internal enum OpCode : ushort
	{
		// Token: 0x040009BD RID: 2493
		False,
		// Token: 0x040009BE RID: 2494
		True,
		// Token: 0x040009BF RID: 2495
		Position,
		// Token: 0x040009C0 RID: 2496
		String,
		// Token: 0x040009C1 RID: 2497
		Reference,
		// Token: 0x040009C2 RID: 2498
		Character,
		// Token: 0x040009C3 RID: 2499
		Category,
		// Token: 0x040009C4 RID: 2500
		NotCategory,
		// Token: 0x040009C5 RID: 2501
		Range,
		// Token: 0x040009C6 RID: 2502
		Set,
		// Token: 0x040009C7 RID: 2503
		In,
		// Token: 0x040009C8 RID: 2504
		Open,
		// Token: 0x040009C9 RID: 2505
		Close,
		// Token: 0x040009CA RID: 2506
		Balance,
		// Token: 0x040009CB RID: 2507
		BalanceStart,
		// Token: 0x040009CC RID: 2508
		IfDefined,
		// Token: 0x040009CD RID: 2509
		Sub,
		// Token: 0x040009CE RID: 2510
		Test,
		// Token: 0x040009CF RID: 2511
		Branch,
		// Token: 0x040009D0 RID: 2512
		Jump,
		// Token: 0x040009D1 RID: 2513
		Repeat,
		// Token: 0x040009D2 RID: 2514
		Until,
		// Token: 0x040009D3 RID: 2515
		FastRepeat,
		// Token: 0x040009D4 RID: 2516
		Anchor,
		// Token: 0x040009D5 RID: 2517
		Info
	}
}
