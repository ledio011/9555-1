using System;

namespace UnityEngine
{
	// Token: 0x02000122 RID: 290
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class TooltipAttribute : PropertyAttribute
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x00019750 File Offset: 0x00017950
		public TooltipAttribute(string tooltip)
		{
			this.tooltip = tooltip;
		}

		// Token: 0x040004B1 RID: 1201
		public readonly string tooltip;
	}
}
