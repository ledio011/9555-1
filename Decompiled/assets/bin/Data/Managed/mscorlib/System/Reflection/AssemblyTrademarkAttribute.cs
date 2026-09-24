using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200018A RID: 394
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		// Token: 0x06000EAE RID: 3758 RVA: 0x000395FC File Offset: 0x000377FC
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.name = trademark;
		}

		// Token: 0x0400060B RID: 1547
		private string name;
	}
}
