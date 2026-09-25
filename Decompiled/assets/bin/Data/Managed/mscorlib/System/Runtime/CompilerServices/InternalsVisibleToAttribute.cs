using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000204 RID: 516
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		// Token: 0x0600128E RID: 4750 RVA: 0x000452A0 File Offset: 0x000434A0
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this.assemblyName = assemblyName;
		}

		// Token: 0x040009C0 RID: 2496
		private string assemblyName;

		// Token: 0x040009C1 RID: 2497
		private bool all_visible = true;
	}
}
