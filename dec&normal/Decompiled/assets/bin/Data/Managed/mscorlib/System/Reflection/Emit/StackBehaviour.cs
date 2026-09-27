using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020001BF RID: 447
	[ComVisible(true)]
	[Serializable]
	public enum StackBehaviour
	{
		// Token: 0x0400084D RID: 2125
		Pop0,
		// Token: 0x0400084E RID: 2126
		Pop1,
		// Token: 0x0400084F RID: 2127
		Pop1_pop1,
		// Token: 0x04000850 RID: 2128
		Popi,
		// Token: 0x04000851 RID: 2129
		Popi_pop1,
		// Token: 0x04000852 RID: 2130
		Popi_popi,
		// Token: 0x04000853 RID: 2131
		Popi_popi8,
		// Token: 0x04000854 RID: 2132
		Popi_popi_popi,
		// Token: 0x04000855 RID: 2133
		Popi_popr4,
		// Token: 0x04000856 RID: 2134
		Popi_popr8,
		// Token: 0x04000857 RID: 2135
		Popref,
		// Token: 0x04000858 RID: 2136
		Popref_pop1,
		// Token: 0x04000859 RID: 2137
		Popref_popi,
		// Token: 0x0400085A RID: 2138
		Popref_popi_popi,
		// Token: 0x0400085B RID: 2139
		Popref_popi_popi8,
		// Token: 0x0400085C RID: 2140
		Popref_popi_popr4,
		// Token: 0x0400085D RID: 2141
		Popref_popi_popr8,
		// Token: 0x0400085E RID: 2142
		Popref_popi_popref,
		// Token: 0x0400085F RID: 2143
		Push0,
		// Token: 0x04000860 RID: 2144
		Push1,
		// Token: 0x04000861 RID: 2145
		Push1_push1,
		// Token: 0x04000862 RID: 2146
		Pushi,
		// Token: 0x04000863 RID: 2147
		Pushi8,
		// Token: 0x04000864 RID: 2148
		Pushr4,
		// Token: 0x04000865 RID: 2149
		Pushr8,
		// Token: 0x04000866 RID: 2150
		Pushref,
		// Token: 0x04000867 RID: 2151
		Varpop,
		// Token: 0x04000868 RID: 2152
		Varpush,
		// Token: 0x04000869 RID: 2153
		Popref_popi_pop1
	}
}
