using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200006E RID: 110
	internal class InterpreterFactory : IMachineFactory
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000A334 File Offset: 0x00008534
		public InterpreterFactory(ushort[] pattern)
		{
			this.pattern = pattern;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A344 File Offset: 0x00008544
		public IMachine NewInstance()
		{
			return new Interpreter(this.pattern);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000A354 File Offset: 0x00008554
		public int GroupCount
		{
			get
			{
				return (int)this.pattern[1];
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000A360 File Offset: 0x00008560
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000A368 File Offset: 0x00008568
		public int Gap
		{
			get
			{
				return this.gap;
			}
			set
			{
				this.gap = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000A374 File Offset: 0x00008574
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000A37C File Offset: 0x0000857C
		public IDictionary Mapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000A388 File Offset: 0x00008588
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000A390 File Offset: 0x00008590
		public string[] NamesMapping
		{
			get
			{
				return this.namesMapping;
			}
			set
			{
				this.namesMapping = value;
			}
		}

		// Token: 0x040009A4 RID: 2468
		private IDictionary mapping;

		// Token: 0x040009A5 RID: 2469
		private ushort[] pattern;

		// Token: 0x040009A6 RID: 2470
		private string[] namesMapping;

		// Token: 0x040009A7 RID: 2471
		private int gap;
	}
}
