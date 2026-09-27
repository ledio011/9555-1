using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000200 RID: 512
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Method)]
	[Serializable]
	public class CompilationRelaxationsAttribute : Attribute
	{
		// Token: 0x06001289 RID: 4745 RVA: 0x00045234 File Offset: 0x00043434
		public CompilationRelaxationsAttribute(int relaxations)
		{
			this.relax = relaxations;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00045244 File Offset: 0x00043444
		public CompilationRelaxationsAttribute(CompilationRelaxations relaxations)
		{
			this.relax = (int)relaxations;
		}

		// Token: 0x040009B9 RID: 2489
		private int relax;
	}
}
