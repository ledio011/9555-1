using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000203 RID: 515
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		// Token: 0x0600128D RID: 4749 RVA: 0x00045290 File Offset: 0x00043490
		public DefaultDependencyAttribute(LoadHint loadHintArgument)
		{
			this.hint = loadHintArgument;
		}

		// Token: 0x040009BF RID: 2495
		private LoadHint hint;
	}
}
